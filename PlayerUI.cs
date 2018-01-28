using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI {
	
	Vector2 location;
//	private int UI_LAYER = 4;
	private Pipe[] nextTiles;
	private Material mat;
	private Material light_mat;
	private GameObject scoreText;
	private TextMesh scoreTextMesh;
	private GameObject overlay1;
	private GameObject overlay2;
	private bool isDestroyed = false;

	public PlayerUI(Vector2 m_location, int numDrops, Material m_light_mat, Material m_mat) {
		nextTiles = new Pipe[numDrops];
		location = m_location;
		light_mat = m_light_mat;
		mat = m_mat;
		CreateScore (location, numDrops);
		CreateOverlay (location, numDrops, light_mat, mat);
		CreateNextDrops(location, numDrops);
	}

	private void CreateScore(Vector2 location, int numDrops) {
		scoreText = new GameObject();

		scoreTextMesh = scoreText.AddComponent<TextMesh>();
		scoreText.transform.position = new Vector3 (location.x - Utilities.tileSize, location.y + Utilities.tileSize*3f, 0);
		scoreTextMesh.fontSize = 72;
		scoreTextMesh.text = "Score:";
	}

	private void CreateOverlay(Vector2 location, int numDrops, Material light_mat, Material mat) {
		overlay1 = GameObject.CreatePrimitive (PrimitiveType.Cube);
		overlay1.AddComponent<Rigidbody> ();
		overlay1.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		overlay1.GetComponent<Renderer> ().material = light_mat;
		overlay1.transform.position = new Vector3 (location.x, location.y, 0);
		overlay1.transform.localScale = new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness);

		overlay2 = GameObject.CreatePrimitive (PrimitiveType.Cube);
		overlay2.AddComponent<Rigidbody> ();
		overlay2.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		overlay2.GetComponent<Renderer> ().material = mat;
		overlay2.transform.position = new Vector3 (location.x, location.y + Utilities.tileSize*1.5f, 0);
		overlay2.transform.localScale = new Vector3 (Utilities.tileSize, Utilities.tileSize*2, Utilities.thickness);
	}

	private void CreateNextDrops(Vector2 location, int numDrops) {
		for (int i = 0; i < numDrops; i++) {
//			Material thisMat = mat;
//			if (i == 0) {
//				thisMat = light_mat;
//			}
			nextTiles[i] = new Pipe(location + new Vector2(0, i * Utilities.tileSize), Utilities.getRandomDropTile(), Utilities.arrow_mat);
		}
	}

	public void UpdateNextDrops(TILE_TYPE[] m_nextTiles) {
		if (isDestroyed) {
			return;
		}
		for (int i = 0; i < nextTiles.Length; i++) {
			nextTiles[i].setTileType(m_nextTiles[i]);
		}
	}

	public void setScore(int score) {
		if (isDestroyed) {
			return;
		}
		scoreTextMesh.text = "Score: " + score;
	}

	public void DestroyInternals() {
		isDestroyed = true;
		for (var i = 0; i < nextTiles.Length; i++) {
			if (nextTiles [i] != null) {
				nextTiles [i].DestroyInternals ();
			}
			nextTiles [i] = null;
		}
		MonoBehaviour.Destroy(overlay1);
		MonoBehaviour.Destroy(overlay2);
		MonoBehaviour.Destroy(scoreTextMesh);
		MonoBehaviour.Destroy(scoreText);
	}
}

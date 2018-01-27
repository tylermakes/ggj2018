using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI {
	
	Vector2 location;
//	private int UI_LAYER = 4;
	private Pipe[] nextTiles;
	private Material mat;
	private Material light_mat;

	public PlayerUI(Vector2 m_location, int numDrops, Material m_light_mat, Material m_mat) {
//		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
//		// cube.tag = "Wall";
//		cube.AddComponent<BoxCollider> ();
//		//      cube.GetComponent<BoxCollider> ().isTrigger = true;
//		cube.AddComponent<Rigidbody> ();
//		cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
//		cube.transform.localScale = new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness);
//		cube.GetComponent<Renderer>().material = mat;
//		player_selector = cube;
		nextTiles = new Pipe[numDrops];
		location = m_location;
		light_mat = m_light_mat;
		mat = m_mat;
		CreateNextDrops(location, numDrops);
	}

	private void CreateNextDrops(Vector2 location, int numDrops) {
		for (int i = 0; i < numDrops; i++) {
			nextTiles[i] = new Pipe(location + new Vector2(0, i * Utilities.tileSize), Utilities.getRandomDropTile(), mat);
		}
	}

	public void UpdateNextDrops(TILE_TYPE[] m_nextTiles) {
		for (int i = 0; i < nextTiles.Length; i++) {
			nextTiles[i].setTileType(m_nextTiles[i]);
		}
	}
}

using UnityEngine;
using System.Collections;

public class Tile
{
	protected GameObject coreObject;
	protected TILE_TYPE tileType;

	public Tile (Vector3 location, TILE_TYPE m_tileType, Material mat) {
		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		// cube.tag = "Wall";
		//		cube.AddComponent<BoxCollider> ();
		//      cube.GetComponent<BoxCollider> ().isTrigger = true;
		cube.AddComponent<Rigidbody> ();
		cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		if (mat != null) {
			cube.GetComponent<Renderer> ().material = Utilities.p1_selector_mat;
		}
		cube.transform.position = location;
		cube.transform.localScale = new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness/2);
		coreObject = cube;
		tileType = m_tileType;
	}

	private void Clear() {
		coreObject.GetComponent<Renderer>().material.shader = new Shader();
	}
}


using UnityEngine;
using System.Collections;

public class Tile
{
	protected GameObject coreObject;
	public TILE_TYPE tileType;
	protected float offset = 1f;

	public Tile (Vector3 location, TILE_TYPE m_tileType, Material mat) {
		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		cube.AddComponent<Rigidbody> ();
		cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		if (mat != null) {
			cube.GetComponent<Renderer> ().material = mat;
		}
		cube.transform.position = location;
		cube.transform.localScale = new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness/2);
		coreObject = cube;
		tileType = m_tileType;
	}

	private void Clear() {
		coreObject.GetComponent<Renderer>().material.shader = new Shader();
	}

	public Vector3 getLocation() {
		return coreObject.transform.position;
	}

	// don't move any on the board! (this is used for the selector)
	public void setLocation(Vector3 location) {
		coreObject.transform.position = location + new Vector3(offset, offset, 0);
	}

	public void setColor(Color color) {
		coreObject.GetComponent<Renderer> ().material.color = color;
	}
}


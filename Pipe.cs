using UnityEngine;
using System.Collections;

public class Pipe : Tile
{

	public Pipe (Vector3 location, TILE_TYPE m_tileType, Material mat) : base(location, m_tileType, mat) {
		setTileType (m_tileType);
	}

	public void setTileType(TILE_TYPE m_tileType) {
		tileType = m_tileType;
		if (tileType == TILE_TYPE.DOWN_ARROW) {
			setDownArrow ();
		} else if (tileType == TILE_TYPE.UP_ARROW) {
			setUpArrow ();
		} else if (tileType == TILE_TYPE.RIGHT_ARROW) {
			setRightArrow ();
		} else if (tileType == TILE_TYPE.LEFT_ARROW) {
			setLeftArrow ();
		}
	}

	private void setRightArrow() {
		coreObject.GetComponent<Renderer>().material.shader = Utilities.arrowShader;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 180);
	}

	private void setLeftArrow() {
		coreObject.GetComponent<Renderer>().material.shader = Utilities.arrowShader;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void setUpArrow() {
		coreObject.GetComponent<Renderer>().material.shader = Utilities.arrowShader;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 270);
	}

	private void setDownArrow() {
		coreObject.GetComponent<Renderer>().material.shader = Utilities.arrowShader;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 90);
	}
}


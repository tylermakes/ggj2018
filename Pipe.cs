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
		} else if (tileType == TILE_TYPE.LEFT_ELBOW_DOWN) {
			setLeftElbowDownArrow ();
		} else if (tileType == TILE_TYPE.LEFT_ELBOW_UP) {
			setLeftElbowUpArrow ();
		} else if (tileType == TILE_TYPE.LEFT_ELBOW_LEFT) {
			setLeftElbowLeftArrow ();
		} else if (tileType == TILE_TYPE.LEFT_ELBOW_RIGHT) {
			setLeftElbowRightArrow ();
		} else if (tileType == TILE_TYPE.RIGHT_ELBOW_DOWN) {
			setRightElbowDownArrow ();
		} else if (tileType == TILE_TYPE.RIGHT_ELBOW_UP) {
			setRightElbowUpArrow ();
		} else if (tileType == TILE_TYPE.RIGHT_ELBOW_LEFT) {
			setRightElbowLeftArrow ();
		} else if (tileType == TILE_TYPE.RIGHT_ELBOW_RIGHT) {
			setRightElbowRightArrow ();
		}
	}

	private void setRightArrow() {
//		coreObject.GetComponent<Renderer>().material = Utilities.arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 180);
	}

	private void setLeftArrow() {
//		coreObject.GetComponent<Renderer>().material = Utilities.arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void setUpArrow() {
//		coreObject.GetComponent<Renderer>().material = Utilities.arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 270);
	}

	private void setDownArrow() {
//		coreObject.GetComponent<Renderer>().material = Utilities.arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 90);
	}

	private void setLeftElbowDownArrow() {
		coreObject.GetComponent<Renderer>().material = Utilities.angle_arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void setLeftElbowUpArrow() {
		coreObject.GetComponent<Renderer>().material = Utilities.angle_arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 180);
	}

	private void setLeftElbowLeftArrow() {
		coreObject.GetComponent<Renderer>().material = Utilities.angle_arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 270);
	}

	private void setLeftElbowRightArrow() {
		coreObject.GetComponent<Renderer>().material = Utilities.angle_arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 90);
	}

	private void setRightElbowDownArrow() {
		coreObject.GetComponent<Renderer>().material = Utilities.opp_angle_arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 0);
	}

	private void setRightElbowUpArrow() {
		coreObject.GetComponent<Renderer>().material = Utilities.opp_angle_arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 180);
	}

	private void setRightElbowLeftArrow() {
		coreObject.GetComponent<Renderer>().material = Utilities.opp_angle_arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 270);
	}

	private void setRightElbowRightArrow() {
		coreObject.GetComponent<Renderer>().material = Utilities.opp_angle_arrow_mat;
		coreObject.transform.rotation = Quaternion.Euler (0, 0, 90);
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
	public static float tileSize = 20f;
	public static float levelSpace = 10f; // z pace
	public static float buffer = 1f;
	public static float thickness = 0.2f;
	public static Shader arrowShader;
	public static int NEXT_TILES = 3;
	public static Material p1_selector_mat;
	public static Material p2_selector_mat;
	public static Material p1_light_mat;
	public static Material p2_light_mat;

	public Utilities ()
	{
	}

	public static Vector3 getLocationVector(float x, float y, int layer) {
		return new Vector3 (x * (tileSize + buffer), y * (tileSize + buffer), thickness * -layer);
	}

	public static Vector3 getLocationVector(Vector2 location, int layer) {
		return new Vector3 (location.x * (tileSize + buffer), location.y * (tileSize + buffer), thickness * -layer);
	}

	public static TILE_TYPE getRandomDropTile() {
		int rand = Random.Range (0, 4);
		TILE_TYPE tileType = TILE_TYPE.CLEAR;
		if (rand == 0) {
			tileType = TILE_TYPE.UP_ARROW;
		} else if (rand == 1) {
			tileType = TILE_TYPE.DOWN_ARROW;
		} else if (rand == 2) {
			tileType = TILE_TYPE.LEFT_ARROW;
		} else if (rand == 3) {
			tileType = TILE_TYPE.RIGHT_ARROW;
		}
		return tileType;
	}
}


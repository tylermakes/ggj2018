﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities
{
	public static int WALL_LAYER = 1;
	public static int PIPE_LAYER = 2;
	public static int PLAYER_LAYER = 4;
    public static int GAME_UI_LAYER = -50;
	public static int GAME_UI_TEXT_LAYER = -100;

	public static float rows = 10;
	public static float columns = 10;
	public static float tileSize = 20f;
	public static float levelSpace = 10f; // z pace
	public static float buffer = 0f;
	public static float thickness = 0.2f;
	public static Material arrow_mat;
	public static Material arrow_mat_white;
	public static Material angle_arrow_mat;
	public static Material opp_angle_arrow_mat;
	public static int NEXT_TILES = 3;
	public static Material p1_selector_mat;
	public static Material p2_selector_mat;
	public static Material p3_selector_mat;
	public static Material p4_selector_mat;
	public static Material p1_light_mat;
	public static Material p2_light_mat;
	public static Material p3_light_mat;
	public static Material p4_light_mat;
	public static Material text_packet_mat;
	public static Material text_packet2_mat;
	public static Material text_packet3_mat;
	public static Material board_mat;
    public static Material overlay_mat;
	public static bool isCreditMode = false;
	public static AudioSource audioSource;
	public static AudioClip collectedSound;

	public Utilities ()
	{
	}

    public static Vector2 getGridLocation(Vector3 position) {
		float x = Mathf.Floor(position.x / (tileSize + buffer));
		float y = Mathf.Floor(position.y / (tileSize + buffer));
        return new Vector2(x, y);
    }

    public static TILE_TYPE getGridType(Vector2 location, Pipe[][] pipes) {
        Pipe pipe = pipes[(int)location.x][(int)location.y];
		TILE_TYPE tileType = TILE_TYPE.CLEAR;
		if (pipe != null) {
			tileType = pipe.tileType;
		}
		return tileType;
    }

	public static Vector3 getLocationVector(float x, float y, int layer) {
		return new Vector3 (x * (tileSize + buffer), y * (tileSize + buffer), thickness * -layer);
	}

	public static Vector3 getLocationVector(Vector2 location, int layer) {
		return new Vector3 (location.x * (tileSize + buffer), location.y * (tileSize + buffer), thickness * -layer);
	}

    public static Vector3 getCenterLocationVector(Vector2 location, int layer) {
        return new Vector3((location.x) * (tileSize + buffer), (location.y + 1.5f) * (tileSize + buffer), thickness * -layer);
	}

//	public static bool pointIsInsideSphere(Vector3 point, Vector3 center, float radius) {
//		return Vector3.Distance(point, center) < radius;
//	}

	public static bool pointIsInsideSphere(Vector3 point, Vector3 center, float radius) {
		return point.x >= center.x && point.x <= center.x + radius && point.y >= center.y && point.y <= center.y + radius;
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
		} else if (rand == 4) {
			tileType = TILE_TYPE.LEFT_ELBOW_DOWN;
		} else if (rand == 5) {
			tileType = TILE_TYPE.LEFT_ELBOW_UP;
		} else if (rand == 6) {
			tileType = TILE_TYPE.LEFT_ELBOW_LEFT;
		} else if (rand == 7) {
			tileType = TILE_TYPE.LEFT_ELBOW_RIGHT;
		} else if (rand == 8) {
			tileType = TILE_TYPE.RIGHT_ELBOW_DOWN;
		} else if (rand == 9) {
			tileType = TILE_TYPE.RIGHT_ELBOW_UP;
		} else if (rand == 10) {
			tileType = TILE_TYPE.RIGHT_ELBOW_LEFT;
		} else if (rand == 11) {
			tileType = TILE_TYPE.RIGHT_ELBOW_RIGHT;
		}
		return tileType;
	}

	public static Material getRandomTextPacketMat() {
		int rand = Random.Range (0, 2);
		Material txtMat = text_packet2_mat;
		if (rand == 0) {
			txtMat = text_packet3_mat;
		}
		return txtMat;
	}

	public static void playSound(AudioClip sound) {
		audioSource.PlayOneShot(sound, 0.7F);
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController {

	GameObject player_selector;
	Vector2 location;
	static int KEYBOARD_DELAY = 5;
	static int DROP_DELAY = 10;
	DelayedTrigger upDelay = new DelayedTrigger(KEYBOARD_DELAY);
	DelayedTrigger downDelay = new DelayedTrigger(KEYBOARD_DELAY);
	DelayedTrigger leftDelay = new DelayedTrigger(KEYBOARD_DELAY);
	DelayedTrigger rightDelay = new DelayedTrigger(KEYBOARD_DELAY);
	DelayedTrigger dropDelay = new DelayedTrigger(DROP_DELAY);
	private int PLAYER_LAYER = 3;
	private TILE_TYPE[] nextDropType = new TILE_TYPE[Utilities.NEXT_TILES];
	private PlayerUI playerUI;
	private Vector2 limit;

	public PlayerController(PlayerUI ui, Vector2 startLocation, Material mat, Vector2 m_limit) {
		GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
		// cube.tag = "Wall";
		cube.AddComponent<BoxCollider> ();
		//      cube.GetComponent<BoxCollider> ().isTrigger = true;
		cube.AddComponent<Rigidbody> ();
		cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		cube.transform.localScale = new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness);
		cube.GetComponent<Renderer>().material = mat;
		player_selector = cube;

		playerUI = ui;
		for (int i = 0; i < nextDropType.Length; i++) {
			nextDropType [i] = Utilities.getRandomDropTile ();
		}
		location = startLocation;
		limit = m_limit;
		player_selector.transform.position = Utilities.getLocationVector(startLocation, PLAYER_LAYER);
	}

	public void triggerDown() {
		if (location.y > 0 && downDelay.trigger()) {
			location = location - new Vector2 (0, 1);
			dropDelay.reset ();
		}
	}

	public void triggerUp() {
		if (location.y < limit.y - 1 && upDelay.trigger()) {
			location = location + new Vector2 (0, 1);
			dropDelay.reset ();
		}
	}

	public void triggerLeft() {
		if (location.x > 0 && leftDelay.trigger()) {
			location = location - new Vector2 (1, 0);
			dropDelay.reset ();
		}
	}

	public void triggerRight() {
		if (location.x < limit.x - 1 && rightDelay.trigger()) {
			location = location + new Vector2 (1, 0);
			dropDelay.reset ();
		}
	}

	public bool canDrop() {
		return dropDelay.canUpdate ();
	}

	private TILE_TYPE takeNextDrop() {
		Debug.unityLogger.Log("======>", nextDropType[0]);
		Debug.unityLogger.Log("======", nextDropType[1]);
		Debug.unityLogger.Log("======", nextDropType[2]);
		TILE_TYPE next = nextDropType [0];
//		Debug.unityLogger.Log("dropping", nextDropType[0]);
		for (int i = 1; i < nextDropType.Length; i++) {
//			Debug.unityLogger.Log("--" + (i - 1), nextDropType[i - 1]);
//			Debug.unityLogger.Log("^^^" + i, nextDropType[i]);
			nextDropType[i - 1]  = nextDropType[i];
		}
		nextDropType [nextDropType.Length - 1] = Utilities.getRandomDropTile ();
//		Debug.unityLogger.Log("f--", nextDropType[nextDropType.Length - 1]);
		return next;
	}

	public DropData triggerDrop() {
		DropData dd = null;
		if (dropDelay.trigger ()) {
			dd = new DropData (location, takeNextDrop());
			playerUI.UpdateNextDrops (nextDropType);
		}
		return dd;
	}

	public void update() {
		upDelay.update();
		downDelay.update();
		leftDelay.update();
		rightDelay.update();
		dropDelay.update ();
		player_selector.transform.position = Utilities.getLocationVector(location, PLAYER_LAYER);
	}
}

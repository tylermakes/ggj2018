using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController {

	Pipe player_selector;
	Vector2 location;
	static int KEYBOARD_DELAY = 6;
	static int DROP_DELAY = 12;
	DelayedTrigger upDelay = new DelayedTrigger(KEYBOARD_DELAY);
	DelayedTrigger downDelay = new DelayedTrigger(KEYBOARD_DELAY);
	DelayedTrigger leftDelay = new DelayedTrigger(KEYBOARD_DELAY);
	DelayedTrigger rightDelay = new DelayedTrigger(KEYBOARD_DELAY);
	DelayedTrigger dropDelay = new DelayedTrigger(DROP_DELAY);
	private TILE_TYPE[] nextDropType = new TILE_TYPE[Utilities.NEXT_TILES];
	private PlayerUI playerUI;
	private Vector2 limit;
	private int score = 0;
	private AudioClip dropSound;
	private bool isDestroyed = false;

	public PlayerController(PlayerUI ui, Vector2 startLocation, Material mat, Vector2 m_limit, Color color, AudioClip m_dropSound) {
		player_selector = new Pipe(new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness), TILE_TYPE.CLEAR, mat);

		playerUI = ui;
		for (int i = 0; i < nextDropType.Length; i++) {
			nextDropType [i] = Utilities.getRandomDropTile ();
		}
		location = startLocation;
		limit = m_limit;
		dropSound = m_dropSound;
		player_selector.setLocation(Utilities.getLocationVector(startLocation, Utilities.PLAYER_LAYER));
		playerUI.UpdateNextDrops (nextDropType);
		player_selector.setTileType (nextDropType [0]);
		player_selector.setColor (color);
	}

	public void triggerDown() {
		if (location.y > 0 && downDelay.trigger()) {
			location = location - new Vector2 (0, 1);
//			dropDelay.reset ();
		}
	}

	public void triggerUp() {
		if (location.y < limit.y - 1 && upDelay.trigger()) {
			location = location + new Vector2 (0, 1);
//			dropDelay.reset ();
		}
	}

	public void triggerLeft() {
		if (location.x > 0 && leftDelay.trigger()) {
			location = location - new Vector2 (1, 0);
//			dropDelay.reset ();
		}
	}

	public void triggerRight() {
		if (location.x < limit.x - 1 && rightDelay.trigger()) {
			location = location + new Vector2 (1, 0);
//			dropDelay.reset ();
		}
	}

	public bool canDrop() {
		return dropDelay.canUpdate ();
	}

	private TILE_TYPE takeNextDrop() {
		TILE_TYPE next = nextDropType [0];
		for (int i = 1; i < nextDropType.Length; i++) {
			nextDropType[i - 1]  = nextDropType[i];
		}
		nextDropType [nextDropType.Length - 1] = Utilities.getRandomDropTile ();
		player_selector.setTileType (nextDropType[0]);
		return next;
	}

	public DropData triggerDrop() {
		DropData dd = null;
		if (dropDelay.trigger ()) {
			Utilities.playSound(dropSound);
			dd = new DropData (location, takeNextDrop());
			playerUI.UpdateNextDrops (nextDropType);
		}
		return dd;
	}

	public void triggerScore() {
		score++;
	}

	public void update() {
		if (isDestroyed) {
			return;
		}
		playerUI.setScore (score);
		upDelay.update();
		downDelay.update();
		leftDelay.update();
		rightDelay.update();
		dropDelay.update ();
		player_selector.setLocation(Utilities.getLocationVector(location, Utilities.PLAYER_LAYER));
	}

	public void DestroyInternals() {
		isDestroyed = true;
		player_selector.DestroyInternals ();
		player_selector = null;
	}
}

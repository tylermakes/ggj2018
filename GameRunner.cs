using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TILE_TYPE {
	CLEAR,
	EMITTER,
	PLAYER_ONE_GOAL,
	PLAYER_TWO_GOAL,
	PLAYER_THREE_GOAL,
	PLAYER_FOUR_GOAL,
	RIGHT_ARROW,
	LEFT_ARROW,
	UP_ARROW,
	DOWN_ARROW,
	LEFT_ELBOW_UP,
	LEFT_ELBOW_DOWN,
	LEFT_ELBOW_LEFT,
	LEFT_ELBOW_RIGHT,
	RIGHT_ELBOW_UP,
	RIGHT_ELBOW_DOWN,
	RIGHT_ELBOW_LEFT,
	RIGHT_ELBOW_RIGHT
}

[RequireComponent(typeof(AudioSource))]
public class GameRunner : MonoBehaviour
{
    static private float rows = 10;
	static private float columns = 10;
	public Material p1_selector_mat;
	public Material p2_selector_mat;
	public Material p3_selector_mat;
	public Material p4_selector_mat;
	public Material p1_ui_overlay_mat;
	public Material p2_ui_overlay_mat;
	public Material p3_ui_overlay_mat;
	public Material p4_ui_overlay_mat;
	public Material p1_light_mat;
	public Material p2_light_mat;
	public Material p3_light_mat;
	public Material p4_light_mat;
	public Material board_mat;
	public Material arrow_mat;
	public Material arrow_mat_white;
	public Material angle_arrow_mat;
	public Material opp_angle_arrow_mat;
	public Material emitter_mat;
	public Material p1_receiver_mat;
	public Material p2_receiver_mat;
	public Material p3_receiver_mat;
	public Material p4_receiver_mat;
	public Material text_packet_mat;
	public Material text_packet2_mat;
	public Material text_packet3_mat;
	public Vector2 p1_location = new Vector2 (0, 0);// set from game runner
	public Vector2 p2_location = new Vector2 (0, 0);
	public Vector2 p3_location = new Vector2 (0, 0);
	public Vector2 p4_location = new Vector2 (0, 0);
    private Vector2 limit;
	private PlayerController p1controller;
	private PlayerController p2controller;
	private PlayerController p3controller;
	private PlayerController p4controller;
	private PlayerUI p1UI;
	private PlayerUI p2UI;
	private PlayerUI p3UI;
	private PlayerUI p4UI;
    private GameUI gameUI;
	private Pipe[][] pipes = new Pipe[(int)rows][];
	private Tile[][] towers = new Tile[(int)rows][];
	private List<Tile> allTowers = new List<Tile>();
	private List<FluidEmitter> emitters = new List<FluidEmitter>();
	private List<Emission> emissions = new List<Emission>();
	public AudioClip[] dropSound;
	public AudioClip collectedSound;
	private HoldKey reset;

	private const int HOLD_KEY_TIME = 80;

    // Use this for initialization
	void Start () {
		Utilities.audioSource = GetComponent<AudioSource>();
		Utilities.collectedSound = collectedSound;
		Utilities.p1_selector_mat = p1_selector_mat;
		Utilities.p1_light_mat = p1_light_mat;
		Utilities.p2_selector_mat = p2_selector_mat;
		Utilities.p2_light_mat = p2_light_mat;
		Utilities.p3_selector_mat = p3_selector_mat;
		Utilities.p3_light_mat = p3_light_mat;
		Utilities.p4_selector_mat = p4_selector_mat;
		Utilities.p4_light_mat = p4_light_mat;
		Utilities.arrow_mat = arrow_mat;
		Utilities.arrow_mat_white = arrow_mat_white;
		Utilities.angle_arrow_mat = angle_arrow_mat;
		Utilities.opp_angle_arrow_mat = opp_angle_arrow_mat;
		Utilities.text_packet_mat = text_packet_mat;
		Utilities.text_packet2_mat = text_packet2_mat;
		Utilities.text_packet3_mat = text_packet3_mat;
		Utilities.board_mat = board_mat;
        limit = new Vector2(rows, columns);
		reset = new HoldKey (KeyCode.Q, HOLD_KEY_TIME);
		MakeCustomWalls ();
        // AddEmitter (Utilities.getLocationVector(0, 3, WALL_LAYER));
        gameUI = new GameUI();
		p1UI = new PlayerUI (new Vector2 (-Utilities.tileSize * 2, 0), Utilities.NEXT_TILES, p1_light_mat, p1_ui_overlay_mat); 
		p2UI = new PlayerUI (new Vector2 (Utilities.tileSize * (columns + 1), Utilities.tileSize * (rows - 3)), Utilities.NEXT_TILES, p2_light_mat, p2_ui_overlay_mat); 
		p3UI = new PlayerUI (new Vector2 (Utilities.tileSize * (columns + 1), 0), Utilities.NEXT_TILES, p3_light_mat, p3_ui_overlay_mat); 
		p4UI = new PlayerUI (new Vector2 (-Utilities.tileSize * 2, Utilities.tileSize * (rows - 3)), Utilities.NEXT_TILES, p4_light_mat, p4_ui_overlay_mat); 
		p1controller = new PlayerController (p1UI, p1_location, p1_selector_mat, limit, Color.red, dropSound[0]);
		p2controller = new PlayerController (p2UI, p2_location, p2_selector_mat, limit, Color.blue, dropSound[1]);
		p3controller = new PlayerController (p3UI, p3_location, p3_selector_mat, limit, Color.green, dropSound[2]);
		p4controller = new PlayerController (p4UI, p4_location, p4_selector_mat, limit, Color.magenta, dropSound[3]);
	}


	void MakeCustomWalls() {
		List<Vector2> p1Towers = new List<Vector2>();
		List<Vector2> p2Towers = new List<Vector2>();
		List<Vector2> p3Towers = new List<Vector2>();
		List<Vector2> p4Towers = new List<Vector2>();
		List<Vector2> emitterLocations = new List<Vector2>();
		List<Vector3> pipeLocations = new List<Vector3>();
		p1Towers.Add (new Vector2 (1, 1));
		p2Towers.Add (new Vector2 (rows - 2, columns - 2));
		p3Towers.Add (new Vector2 (rows - 2, 1));
		p4Towers.Add (new Vector2 (1, columns - 2));
		emitterLocations.Add(new Vector2 (4, 5));
		emitterLocations.Add(new Vector2 (6, 6));
		pipeLocations.Add(new Vector3 (6, 8, (float)TILE_TYPE.LEFT_ARROW));
		pipeLocations.Add(new Vector3 (5, 8, (float)TILE_TYPE.DOWN_ARROW));
		pipeLocations.Add(new Vector3 (5, 7, (float)TILE_TYPE.RIGHT_ARROW));
		pipeLocations.Add(new Vector3 (6, 7, (float)TILE_TYPE.UP_ARROW));
		pipeLocations.Add(new Vector3 (4, 8, (float)TILE_TYPE.LEFT_ARROW));
		pipeLocations.Add(new Vector3 (2, 8, (float)TILE_TYPE.DOWN_ARROW));
		pipeLocations.Add(new Vector3 (2, 6, (float)TILE_TYPE.RIGHT_ARROW));
		pipeLocations.Add(new Vector3 (4, 6, (float)TILE_TYPE.UP_ARROW));
		for (var i = 0; i < rows; i++)
		{
			pipes [i] = new Pipe[(int)columns];
			towers [i] = new Tile[(int)columns];
			for (var j = 0; j < columns; j++)
			{
				int x = i;
				int y = j;

				MakeWall (Utilities.getLocationVector(x, y, Utilities.WALL_LAYER),
					new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness/2));
			}
		}

		p1Towers.ForEach (t => {
			AddPlayerTower ((int)t.x, (int)t.y, TILE_TYPE.PLAYER_ONE_GOAL, p1_receiver_mat);
		});
		p2Towers.ForEach (t => {
			AddPlayerTower ((int)t.x, (int)t.y, TILE_TYPE.PLAYER_TWO_GOAL, p2_receiver_mat);
		});
		p3Towers.ForEach (t => {
			AddPlayerTower ((int)t.x, (int)t.y, TILE_TYPE.PLAYER_THREE_GOAL, p3_receiver_mat);
		});
		p4Towers.ForEach (t => {
			AddPlayerTower ((int)t.x, (int)t.y, TILE_TYPE.PLAYER_FOUR_GOAL, p4_receiver_mat);
		});
		emitterLocations.ForEach (t => {
			AddEmitter ((int)t.x, (int)t.y);
		});
		pipeLocations.ForEach (t => {
			AddPipe ((int)t.x, (int)t.y, (int)t.z);
		});
	}

//	void MakeWalls() {
//		Vector2 p1ReceiverLocation = new Vector2 (2, 2);
//		Vector2 p2ReceiverLocation = new Vector2 (rows - 3, columns - 3);
//		Vector2 p3ReceiverLocation = new Vector2 (rows - 3, columns - 3);
//		Vector2 p4ReceiverLocation = new Vector2 (rows - 3, columns - 3);
//		Vector2 emmiter1Location = new Vector2 (rows - 3, 2);
//		Vector2 emmiter2Location = new Vector2 (2, columns - 3);
//        for (var i = 0; i < rows; i++)
//		{
//			pipes [i] = new Pipe[(int)columns];
//			towers [i] = new Tile[(int)columns];
//            for (var j = 0; j < columns; j++)
//            {
//                int x = i;
//				int y = j;
//
//				MakeWall (Utilities.getLocationVector(x, y, WALL_LAYER),
//					new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness/2));
//				if (x == p1ReceiverLocation.x && y == p1ReceiverLocation.y) {
//					AddPlayerTower (x, y, TILE_TYPE.PLAYER_ONE_GOAL, p1_receiver_mat);
//				} else if (x == p2ReceiverLocation.x && y == p2ReceiverLocation.y) {
//					AddPlayerTower (x, y, TILE_TYPE.PLAYER_TWO_GOAL, p2_receiver_mat);
//				} else if (x == p3ReceiverLocation.x && y == p3ReceiverLocation.y) {
//					AddPlayerTower (x, y, TILE_TYPE.PLAYER_THREE_GOAL, p3_receiver_mat);
//				} else if (x == p4ReceiverLocation.x && y == p4ReceiverLocation.y) {
//					AddPlayerTower (x, y, TILE_TYPE.PLAYER_FOUR_GOAL, p4_receiver_mat);
//				} else if (x == emmiter1Location.x && y == emmiter1Location.y) {
//					AddEmitter (x, y);
//				} else if (x == emmiter2Location.x && y == emmiter2Location.y) {
//					AddEmitter (x, y);
//				} else {
//					AddPipe (x, y, (int)Utilities.getRandomDropTile());
//				}
//			}
//        }
//	}

	void AddPlayerTower(int x, int y, TILE_TYPE tileType, Material mat) {
		Tile tower = new Receiver (Utilities.getLocationVector (x, y, Utilities.PIPE_LAYER), tileType, mat);
		tower.Rotate ();
		towers [x] [y] = tower;
		allTowers.Add (tower);
	}

	void AddPipe(int x, int y, int t) {
		pipes [x][y] = new Pipe (Utilities.getLocationVector(x, y, Utilities.PIPE_LAYER), (TILE_TYPE)t, Utilities.arrow_mat);
	}

    void AddEmitter(int x, int y) {
		FluidEmitter fe = new FluidEmitter (Utilities.getLocationVector (x, y, Utilities.PIPE_LAYER), TILE_TYPE.EMITTER, emitter_mat, limit);
		towers [x] [y] = fe;
		emitters.Add(fe);
    }

    void MakeWall(Vector3 wallPosition, Vector3 wallScale) {
        var cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
        cube.AddComponent<Rigidbody> ();
        cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		cube.GetComponent<Renderer> ().material = board_mat;
        cube.transform.position = wallPosition;
        cube.transform.localScale = wallScale;
    }

	void ChangeTile(int x, int y, TILE_TYPE tileType) {
		Pipe p = pipes [x] [y];
		if (p == null) {
			AddPipe (x, y, (int)tileType);
		} else {
			p.setTileType (tileType);
		}
	}

	void HandlePlayerInput(PlayerController playerController, KeyCode joystickKeyCode, string hAxis, string vAxis) {
		double distance = 0.4;
		if (Input.GetAxis (vAxis) < -distance) {
			playerController.triggerDown ();
		}
		if (Input.GetAxis (vAxis) > distance) {
			playerController.triggerUp ();
		}
		if (Input.GetAxis (hAxis) < -distance) {
			playerController.triggerLeft ();
		}
		if (Input.GetAxis (hAxis) > distance) {
			playerController.triggerRight ();
		}
		if (Input.GetKey(joystickKeyCode)) {
			DropData dropData = playerController.triggerDrop ();
			if (dropData == null) {
				// not dropping
			} else {
				ChangeTile ((int)dropData.location.x, (int)dropData.location.y, dropData.tileType);
			}
		}
	}

    void Update ()
	{
		if (Input.GetKey(KeyCode.DownArrow)) {
//			p1controller.triggerDown ();
			p2controller.triggerDown ();
		}
		if (Input.GetKey(KeyCode.UpArrow)) {
//			p1controller.triggerUp ();
			p2controller.triggerUp ();
		}
		if (Input.GetKey(KeyCode.LeftArrow)) {
//			p1controller.triggerLeft ();
			p2controller.triggerLeft ();
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			//			p1controller.triggerRight ();
			p2controller.triggerRight ();
		}

		if (Input.GetKey(KeyCode.Space)) {
			DropData dropData = p2controller.triggerDrop ();
			if (dropData == null) {
				// not dropping
			} else {
				ChangeTile ((int)dropData.location.x, (int)dropData.location.y, dropData.tileType);
			}
		}
		HandlePlayerInput (p1controller, KeyCode.Joystick1Button1, "Horizontal1", "Vertical1");
		HandlePlayerInput (p2controller, KeyCode.Joystick2Button1, "Horizontal2", "Vertical2");
		HandlePlayerInput (p3controller, KeyCode.Joystick3Button1, "Horizontal3", "Vertical3");
		HandlePlayerInput (p4controller, KeyCode.Joystick4Button1, "Horizontal4", "Vertical4");

		if (reset.Update()) {
			Debug.unityLogger.Log("==QUITTING");
		}


//		Debug.unityLogger.Log("==",Input.GetAxis (axisHName));
//		Debug.unityLogger.Log("y==",Input.GetAxis (axisVName));

		foreach(FluidEmitter emitter in emitters) {
			// Check if pipes are around the emitter. If so, add emission in that direction.
			Emission em = emitter.Emit();
			if (em != null) {
				emissions.Add (em);
			}
			emitter.Update();
		}

		int i = 0;
		while (i < emissions.Count) {
			Emission em = emissions [i];
			em.Update (pipes);
			if (em.shouldDestroy) {
				em.DestroyInternals ();
				emissions.RemoveAt (i);
			} else {
				i++;
				Vector2 emissionGridLocation = em.getGridLocation ();
				allTowers.ForEach (aT => {
					Vector2 aTLocation = Utilities.getGridLocation (aT.getLocation ());
//					Debug.unityLogger.Log ("==AT:", aTLocation);
//					Debug.unityLogger.Log ("==ET:", emissionGridLocation);
					if (emissionGridLocation.x == aTLocation.x && emissionGridLocation.y == aTLocation.y) {
						em.setWon ();
						addScoreForPlayer(aT);
					}
				});
			}
		}

        gameUI.Update ();
		p1controller.update ();
		p2controller.update ();
		p3controller.update ();
		p4controller.update ();
    }

	void addScoreForPlayer(Tile tile) {
		switch (tile.tileType) {
		case TILE_TYPE.PLAYER_ONE_GOAL:
			p1controller.triggerScore ();
			break;
		case TILE_TYPE.PLAYER_TWO_GOAL:
			p2controller.triggerScore ();
			break;
		case TILE_TYPE.PLAYER_THREE_GOAL:
			p3controller.triggerScore ();
			break;
		case TILE_TYPE.PLAYER_FOUR_GOAL:
			p4controller.triggerScore ();
			break;
		}
	}

    void OnDestroy()
    {
    }
}

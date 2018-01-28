using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TILE_TYPE {
	CLEAR,
	EMITTER,
	PLAYER_ONE_GOAL,
	PLAYER_TWO_GOAL,
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

public class GameRunner : MonoBehaviour
{
    static private float rows = 10;
	static private float columns = 10;
	public Material p1_selector_mat;
	public Material p2_selector_mat;
	public Material p1_light_mat;
	public Material p2_light_mat;
	public Material arrow_mat;
	public Material angle_arrow_mat;
	public Material opp_angle_arrow_mat;
	public Material emitter_mat;
	public Material p1_receiver_mat;
	public Material p2_receiver_mat;
	public Vector2 p1_location = new Vector2 (0, 0);
	public Vector2 p2_location = new Vector2 (0, 0);
	private PlayerController p1controller;
	private PlayerController p2controller;
	private PlayerUI p1UI;
	private PlayerUI p2UI;
	private int WALL_LAYER = 1;
	private int PIPE_LAYER = 2;
	private Pipe[][] pipes = new Pipe[(int)rows][];
	private Tile[][] towers = new Tile[(int)rows][];
	private List<Tile> allTowers = new List<Tile>();
	private List<FluidEmitter> emitters = new List<FluidEmitter>();
	private List<Emission> emissions = new List<Emission>();

    // Use this for initialization
	void Start () {
		Utilities.p1_selector_mat = p1_selector_mat;
		Utilities.p1_light_mat = p1_light_mat;
		Utilities.p2_selector_mat = p2_selector_mat;
		Utilities.p2_light_mat = p2_light_mat;
		Utilities.arrow_mat = arrow_mat;
		Utilities.angle_arrow_mat = angle_arrow_mat;
		Utilities.opp_angle_arrow_mat = opp_angle_arrow_mat;
		MakeCustomWalls ();
        // AddEmitter (Utilities.getLocationVector(0, 3, WALL_LAYER));
		Vector2 limit = new Vector2 (rows, columns);
		p1UI = new PlayerUI (new Vector2 (-Utilities.tileSize * 2, 0), Utilities.NEXT_TILES, p1_light_mat, p1_selector_mat); 
		p2UI = new PlayerUI (new Vector2 (Utilities.tileSize * (columns + 1), Utilities.tileSize * (rows - 3)), Utilities.NEXT_TILES, p2_light_mat, p2_selector_mat); 
		p1controller = new PlayerController (p1UI, p1_location, p1_selector_mat, limit);
        p2controller = new PlayerController (p2UI, p2_location, p2_selector_mat, limit);
	}


	void MakeCustomWalls() {
		List<Vector2> p1Towers = new List<Vector2>();
		List<Vector2> p2Towers = new List<Vector2>();
		List<Vector2> emitterLocations = new List<Vector2>();
		List<Vector3> pipeLocations = new List<Vector3>();
		p1Towers.Add (new Vector2 (1, 2));
		p2Towers.Add (new Vector2 (rows - 1, columns - 1));
		emitterLocations.Add(new Vector2 (rows - 3, 2));
//		pipeLocations.Add(new Vector3 (1, 2, (float)TILE_TYPE.UP_ARROW));
		for (var i = 0; i < rows; i++)
		{
			pipes [i] = new Pipe[(int)columns];
			towers [i] = new Tile[(int)columns];
			for (var j = 0; j < columns; j++)
			{
				int x = i;
				int y = j;

				MakeWall (Utilities.getLocationVector(x, y, WALL_LAYER),
					new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness/2));
			}
		}

		p1Towers.ForEach (t => {
			AddPlayerTower ((int)t.x, (int)t.y, TILE_TYPE.PLAYER_ONE_GOAL, p1_receiver_mat);
		});
		p2Towers.ForEach (t => {
			AddPlayerTower ((int)t.x, (int)t.y, TILE_TYPE.PLAYER_TWO_GOAL, p2_receiver_mat);
		});
		emitterLocations.ForEach (t => {
			AddEmitter ((int)t.x, (int)t.y);
		});
		pipeLocations.ForEach (t => {
			AddPipe ((int)t.x, (int)t.y, (int)t.z);
		});
	}

//	if (x == p1ReceiverLocation.x && y == p1ReceiverLocation.y) {
//		towers [i] [j] = new Receiver (Utilities.getLocationVector (x, y, PIPE_LAYER), TILE_TYPE.PLAYER_ONE_GOAL, p1_receiver_mat);
//	} else if (x == p2ReceiverLocation.x && y == p2ReceiverLocation.y) {
//		towers [i] [j] = new Receiver (Utilities.getLocationVector (x, y, PIPE_LAYER), TILE_TYPE.PLAYER_TWO_GOAL, p2_receiver_mat);
//	} else if (x == emmiter1Location.x && y == emmiter1Location.y) {
//		AddEmitter (x, y);
//	} else if (x == emmiter2Location.x && y == emmiter2Location.y) {
//		AddEmitter (x, y);
//	} else {
//		pipes [i][j] = new Pipe (Utilities.getLocationVector(x, y, PIPE_LAYER), Utilities.getRandomDropTile(), null);
//	}
	void MakeWalls() {
		Vector2 p1ReceiverLocation = new Vector2 (2, 2);
		Vector2 p2ReceiverLocation = new Vector2 (rows - 3, columns - 3);
		Vector2 emmiter1Location = new Vector2 (rows - 3, 2);
		Vector2 emmiter2Location = new Vector2 (2, columns - 3);
        for (var i = 0; i < rows; i++)
		{
			pipes [i] = new Pipe[(int)columns];
			towers [i] = new Tile[(int)columns];
            for (var j = 0; j < columns; j++)
            {
                int x = i;
				int y = j;

				MakeWall (Utilities.getLocationVector(x, y, WALL_LAYER),
					new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness/2));
				if (x == p1ReceiverLocation.x && y == p1ReceiverLocation.y) {
					AddPlayerTower (x, y, TILE_TYPE.PLAYER_ONE_GOAL, p1_receiver_mat);
				} else if (x == p2ReceiverLocation.x && y == p2ReceiverLocation.y) {
					AddPlayerTower (x, y, TILE_TYPE.PLAYER_TWO_GOAL, p2_receiver_mat);
				} else if (x == emmiter1Location.x && y == emmiter1Location.y) {
					AddEmitter (x, y);
				} else if (x == emmiter2Location.x && y == emmiter2Location.y) {
					AddEmitter (x, y);
				} else {
					AddPipe (x, y, (int)Utilities.getRandomDropTile());
				}
			}
        }
	}

	void AddPlayerTower(int x, int y, TILE_TYPE tileType, Material mat) {
		Tile tower = new Receiver (Utilities.getLocationVector (x, y, PIPE_LAYER), tileType, mat);
		towers [x] [y] = tower;
		allTowers.Add (tower);
	}

	void AddPipe(int x, int y, int t) {
		pipes [x][y] = new Pipe (Utilities.getLocationVector(x, y, PIPE_LAYER), (TILE_TYPE)t, null);
	}

    void AddEmitter(int x, int y) {
		FluidEmitter fe = new FluidEmitter (Utilities.getLocationVector (x, y, PIPE_LAYER), TILE_TYPE.EMITTER, emitter_mat);
		towers [x] [y] = fe;
		emitters.Add(fe);
    }

    void MakeWall(Vector3 wallPosition, Vector3 wallScale) {
        var cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
        // cube.tag = "Wall";
//        cube.AddComponent<BoxCollider> ();
        cube.AddComponent<Rigidbody> ();
        cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
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
		string axisHName = "aHorizontal";
		string axisVName = "aVertical";
		double distance = 0.4;
		if (Input.GetAxis (axisVName) < -distance) {
			p1controller.triggerDown ();
		}
		if (Input.GetAxis (axisVName) > distance) {
			p1controller.triggerUp ();
		}
		if (Input.GetAxis (axisHName) < -distance) {
			p1controller.triggerLeft ();
		}
		if (Input.GetAxis (axisHName) > distance) {
			p1controller.triggerRight ();
		}
		if (Input.GetKey(KeyCode.Joystick1Button1)) {
			DropData dropData2 = p1controller.triggerDrop ();
			if (dropData2 == null) {
				// not dropping
			} else {
				ChangeTile ((int)dropData2.location.x, (int)dropData2.location.y, dropData2.tileType);
			}
		}

//		Debug.unityLogger.Log("==",Input.GetAxis (axisHName));
//		Debug.unityLogger.Log("y==",Input.GetAxis (axisVName));
		p1controller.update ();
		p2controller.update ();


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
				Debug.unityLogger.Log("==DESTROY");
				em.DestroyInternals ();
				emissions.RemoveAt (i);
			} else {
				i++;
				Vector2 emissionGridLocation = em.getGridLocation ();
				allTowers.ForEach (aT => {
					Vector2 aTLocation = Utilities.getGridLocation (aT.getLocation ());
					Debug.unityLogger.Log ("==AT:", aTLocation);
					Debug.unityLogger.Log ("==ET:", emissionGridLocation);
					if (emissionGridLocation.x == aTLocation.x && emissionGridLocation.y == aTLocation.y) {
						em.setWon ();
					}
				});
			}
		}
    }

    void OnDestroy()
    {
    }
}

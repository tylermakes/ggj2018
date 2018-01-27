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

    // Use this for initialization
	void Start () {
		Utilities.p1_selector_mat = p1_selector_mat;
		Utilities.p1_light_mat = p1_light_mat;
		Utilities.p2_selector_mat = p2_selector_mat;
		Utilities.p2_light_mat = p2_light_mat;
		Utilities.arrow_mat = arrow_mat;
		Utilities.angle_arrow_mat = angle_arrow_mat;
		Utilities.opp_angle_arrow_mat = opp_angle_arrow_mat;
		MakeWalls ();
		Vector2 limit = new Vector2 (rows, columns);
		p1UI = new PlayerUI (new Vector2 (-Utilities.tileSize * 2, 0), Utilities.NEXT_TILES, p1_light_mat, p1_selector_mat); 
		p2UI = new PlayerUI (new Vector2 (Utilities.tileSize * (columns + 1), Utilities.tileSize * (rows - 3)), Utilities.NEXT_TILES, p2_light_mat, p2_selector_mat); 
		p1controller = new PlayerController (p1UI, p1_location, p1_selector_mat, limit);
		p2controller = new PlayerController (p2UI, p2_location, p2_selector_mat, limit);
	}

	void MakeWalls() {
		Vector2 p1ReceiverLocation = new Vector2 (3, 3);
		Vector2 p2ReceiverLocation = new Vector2 (rows - 3, columns - 3);
        for (var i = 0; i < rows; i++)
        {
			pipes [i] = new Pipe[(int)columns];
            for (var j = 0; j < columns; j++)
            {
                int x = i;
                int y = j;
				MakeWall (Utilities.getLocationVector(x, y, WALL_LAYER),
					new Vector3 (Utilities.tileSize, Utilities.tileSize, Utilities.thickness/2));
				pipes [i][j] = new Pipe (Utilities.getLocationVector(x, y, PIPE_LAYER), Utilities.getRandomDropTile(), null);
            }
        }
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
				pipes [(int)dropData.location.x] [(int)dropData.location.y].setTileType (dropData.tileType);
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
				pipes [(int)dropData2.location.x] [(int)dropData2.location.y].setTileType (dropData2.tileType);
			}
		}

//		Debug.unityLogger.Log("==",Input.GetAxis (axisHName));
//		Debug.unityLogger.Log("y==",Input.GetAxis (axisVName));
		p1controller.update ();
		p2controller.update ();
    }

    void OnDestroy()
    {
    }
}

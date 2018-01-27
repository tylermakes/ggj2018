using UnityEngine;
using System.Collections;

public class Emission
{
	private const int MOVE_DELAY = 5;
	GameObject coreObject;
	private float emissionMoveSpeed = 25.0f;

	public Emission (Vector3 start_position)
	{
		var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.gameObject.name = "Emission";
		sphere.AddComponent<Rigidbody>();
		sphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		sphere.transform.position = start_position;
		sphere.transform.localScale = new Vector3(Utilities.tileSize, Utilities.tileSize, Utilities.thickness * 50);
		coreObject = sphere;
	}

//	MoveEmission(Time.deltaTime);
//	emitSecondsElapsed += Time.deltaTime;
//	if (emitSecondsElapsed > emitSeconds) {
//		emitSecondsElapsed -= emitSeconds;
//		Emit();
//	}
	public void Update(Pipe[][] pipes) {
		var pos = coreObject.transform.position;

		float moveDistance = Time.deltaTime * emissionMoveSpeed;
		Vector2 gridPos = Utilities.getGridLocation(pos);
		TILE_TYPE t = Utilities.getGridType(gridPos, pipes);

		switch (t) {
		case TILE_TYPE.LEFT_ARROW:
		case TILE_TYPE.LEFT_ELBOW_LEFT:
		case TILE_TYPE.RIGHT_ELBOW_LEFT:
			coreObject.transform.position = new Vector3(pos.x - moveDistance, Utilities.getLocationVector(gridPos, 0).y, pos.z);
			break;
		case TILE_TYPE.RIGHT_ARROW:
		case TILE_TYPE.LEFT_ELBOW_RIGHT:
		case TILE_TYPE.RIGHT_ELBOW_RIGHT:
			coreObject.transform.position = new Vector3(pos.x + moveDistance, Utilities.getLocationVector(gridPos, 0).y, pos.z);
			break;
		case TILE_TYPE.UP_ARROW:
		case TILE_TYPE.LEFT_ELBOW_UP:
		case TILE_TYPE.RIGHT_ELBOW_UP:
			coreObject.transform.position = new Vector3(Utilities.getLocationVector(gridPos, 0).x, pos.y + moveDistance, pos.z);
			break;
		case TILE_TYPE.DOWN_ARROW:
		case TILE_TYPE.LEFT_ELBOW_DOWN:
		case TILE_TYPE.RIGHT_ELBOW_DOWN:
			coreObject.transform.position = new Vector3(Utilities.getLocationVector(gridPos, 0).x, pos.y - moveDistance, pos.z);
			break;
		}
	}
}


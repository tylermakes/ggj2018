﻿using UnityEngine;
using System.Collections;

public class Emission
{
	private const int MOVE_DELAY = 5;
	GameObject coreObject;
	private float emissionMoveSpeed = 25.0f;
	public bool shouldDestroy = false;
    private float radiusDetectionSize = Utilities.tileSize / 10;
    private DIRECTION currentDirection = DIRECTION.UP;
    private Vector2 limit;

    public enum DIRECTION {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public Emission (Vector3 start_position, Vector2 limit)
	{
		var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.gameObject.name = "Emission";
		sphere.AddComponent<Rigidbody>();
		sphere.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		sphere.transform.position = start_position;
		sphere.transform.localScale = new Vector3(Utilities.tileSize, Utilities.tileSize, Utilities.thickness * 50);
		coreObject = sphere;
        this.limit = limit;
	}

//	MoveEmission(Time.deltaTime);
//	emitSecondsElapsed += Time.deltaTime;
//	if (emitSecondsElapsed > emitSeconds) {
//		emitSecondsElapsed -= emitSeconds;
//		Emit();
//	}
	public void Update(Pipe[][] pipes) {
		if (shouldDestroy) {
			// do nothing
			return;
		}

		var pos = coreObject.transform.position;

		float moveDistance = Time.deltaTime * emissionMoveSpeed;
		Vector2 gridPos = Utilities.getGridLocation(pos);

        // Move forward
        switch (currentDirection) {
            case DIRECTION.LEFT:
                coreObject.transform.position = new Vector3(pos.x - moveDistance, Utilities.getLocationVector(gridPos, 0).y, pos.z);
                break;
            case DIRECTION.RIGHT:
                coreObject.transform.position = new Vector3(pos.x + moveDistance, Utilities.getLocationVector(gridPos, 0).y, pos.z);
                break;
            case DIRECTION.UP:
                coreObject.transform.position = new Vector3(Utilities.getLocationVector(gridPos, 0).x, pos.y + moveDistance, pos.z);
                break;
            case DIRECTION.DOWN:
                coreObject.transform.position = new Vector3(Utilities.getLocationVector(gridPos, 0).x, pos.y - moveDistance, pos.z);
                break;
        }

        if (gridPos.y >= limit.y - 1 || gridPos.y < 0 || gridPos.x >= limit.x - 1 || gridPos.x < 0) {
            shouldDestroy = true;
            return;
        }

        TILE_TYPE currentTileType = Utilities.getGridType(gridPos, pipes);

        // Recenter and change direction if needed
        Vector3 centerOfGrid = Utilities.getCenterLocationVector(gridPos, 0);
        bool isInCenterOfGrid = Utilities.pointIsInsideSphere(coreObject.transform.position, centerOfGrid, radiusDetectionSize);
        if (isInCenterOfGrid) {
            bool isMovingVertically = currentDirection == DIRECTION.UP || currentDirection == DIRECTION.DOWN;
            bool willMoveHorizontally =
                currentTileType == TILE_TYPE.LEFT_ARROW
                || currentTileType == TILE_TYPE.LEFT_ELBOW_LEFT
                || currentTileType == TILE_TYPE.RIGHT_ELBOW_LEFT
                || currentTileType == TILE_TYPE.RIGHT_ARROW
                || currentTileType == TILE_TYPE.LEFT_ELBOW_RIGHT
                || currentTileType == TILE_TYPE.RIGHT_ELBOW_RIGHT;
            bool willMoveVertically =
                currentTileType == TILE_TYPE.UP_ARROW
                || currentTileType == TILE_TYPE.LEFT_ELBOW_UP
                || currentTileType == TILE_TYPE.RIGHT_ELBOW_UP
                || currentTileType == TILE_TYPE.DOWN_ARROW
                || currentTileType == TILE_TYPE.LEFT_ELBOW_DOWN
                || currentTileType == TILE_TYPE.RIGHT_ELBOW_DOWN;
            
            if (isMovingVertically && willMoveHorizontally) {
                // Readjust y position
                coreObject.transform.position = new Vector3(coreObject.transform.position.x, centerOfGrid.y, coreObject.transform.position.z);
            }
            if (!isMovingVertically && willMoveVertically) {
                // Readjust x position
                coreObject.transform.position = new Vector3(centerOfGrid.x, coreObject.transform.position.y, coreObject.transform.position.z);
            }
        }

        // Set Direction
        switch (currentTileType)
        {
            case TILE_TYPE.LEFT_ARROW:
            case TILE_TYPE.LEFT_ELBOW_LEFT:
            case TILE_TYPE.RIGHT_ELBOW_LEFT:
                currentDirection = DIRECTION.LEFT;
                break;
            case TILE_TYPE.RIGHT_ARROW:
            case TILE_TYPE.LEFT_ELBOW_RIGHT:
            case TILE_TYPE.RIGHT_ELBOW_RIGHT:
                currentDirection = DIRECTION.RIGHT;
                break;
            case TILE_TYPE.UP_ARROW:
            case TILE_TYPE.LEFT_ELBOW_UP:
            case TILE_TYPE.RIGHT_ELBOW_UP:
                currentDirection = DIRECTION.UP;
                break;
            case TILE_TYPE.DOWN_ARROW:
            case TILE_TYPE.LEFT_ELBOW_DOWN:
            case TILE_TYPE.RIGHT_ELBOW_DOWN:
                currentDirection = DIRECTION.DOWN;
                break;
            default:
                //shouldDestroy = true;
                break;
        }
	}

	public void setWon() {
		Debug.unityLogger.Log("==WON", Utilities.getGridLocation(coreObject.transform.position));
		shouldDestroy = true;
	}

	public Vector3 getGridLocation() {
		return Utilities.getGridLocation (coreObject.transform.position);
	}

	public void DestroyInternals() {
		MonoBehaviour.Destroy(coreObject);
	}
}


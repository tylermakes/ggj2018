using UnityEngine;
using System.Collections;

public class FluidEmitter : Tile
{
	private const int EMISSION_DELAY = 50;

	DelayedTrigger emissionDelay = new DelayedTrigger(EMISSION_DELAY);
	public FluidEmitter (Vector3 location, TILE_TYPE m_tileType, Material mat) : base(location, m_tileType, mat) {
//		var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//		cube.gameObject.name = "Emitter";
//		cube.AddComponent<Rigidbody>();
//		cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
//		cube.transform.position = position;
//		cube.transform.localScale = new Vector3(Utilities.tileSize, Utilities.tileSize, Utilities.thickness * 50);
	}

	public Emission Emit() {
		Emission returnedEmission = null;
		if (emissionDelay.trigger ()) {
			returnedEmission = new Emission (getLocation());
		}
		return returnedEmission;
	}

	public void Update() {
		emissionDelay.update ();
	}
}


using UnityEngine;
using System.Collections;

public class FluidEmitter : Tile
{
	private const int EMISSION_DELAY = 70;
    private Vector2 limit;

	DelayedTrigger emissionDelay = new DelayedTrigger(EMISSION_DELAY);
	public FluidEmitter (Vector3 location, TILE_TYPE m_tileType, Material mat, Vector2 limit) : base(location, m_tileType, mat) {
		this.limit = limit;
		Rotate ();
	}

	public Emission Emit() {
		Emission returnedEmission = null;
		if (emissionDelay.trigger ()) {
			returnedEmission = new Emission (getLocation(), limit, getRandomDirection());
		}
		return returnedEmission;
	}

	private Emission.DIRECTION getRandomDirection() {
		return (Emission.DIRECTION)Random.Range (0, System.Enum.GetValues (typeof(Emission.DIRECTION)).Length);
	}

	public void Update() {
		emissionDelay.update ();
	}
}


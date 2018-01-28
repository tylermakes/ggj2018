using UnityEngine;
using System.Collections;

public class FluidEmitter : Tile
{
	private const int EMISSION_DELAY = 100;
    private Vector2 limitx;

	DelayedTrigger emissionDelay = new DelayedTrigger(EMISSION_DELAY);
	public FluidEmitter (Vector3 location, TILE_TYPE m_tileType, Material mat, Vector2 limit) : base(location, m_tileType, mat) {
        this.limitx = limit;
	}

	public Emission Emit() {
		Emission returnedEmission = null;
		if (emissionDelay.trigger ()) {
            returnedEmission = new Emission (getLocation(), limitx);
		}
		return returnedEmission;
	}

	public void Update() {
		emissionDelay.update ();
	}
}


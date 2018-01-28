using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameMap
{

	public List<Vector2> p1Towers = new List<Vector2>();
	public List<Vector2> p2Towers = new List<Vector2>();
	public List<Vector2> p3Towers = new List<Vector2>();
	public List<Vector2> p4Towers = new List<Vector2>();
	public List<Vector2> emitterLocations = new List<Vector2>();
	public List<Vector3> pipeLocations = new List<Vector3>();

	public GameMap(List<Vector2> p1Towers, List<Vector2> p2Towers, List<Vector2> p3Towers, List<Vector2> p4Towers, List<Vector2> emitterLocations, List<Vector3>pipeLocations) {
		this.p1Towers = p1Towers;
		this.p2Towers = p2Towers;
		this.p3Towers = p3Towers;
		this.p4Towers = p4Towers;
		this.emitterLocations = emitterLocations;
		this.pipeLocations = pipeLocations;
	}
}


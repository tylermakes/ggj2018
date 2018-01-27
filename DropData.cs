using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropData
{
	public TILE_TYPE tileType;
	public Vector2 location;

	public DropData (Vector2 m_location, TILE_TYPE m_tileType)
	{
		location = m_location;
		tileType = m_tileType;
	}
}


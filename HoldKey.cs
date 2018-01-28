using System;
using System.Collections.Generic;
using UnityEngine;

public class HoldKey
{
	KeyCode keycode;
	int holdTime;
	int held = 0;

	public HoldKey (KeyCode m_keycode, int m_holdTime)
	{
		keycode = m_keycode;
		holdTime = m_holdTime;
	}

	public bool Update() {
		bool shouldTrigger = false;
		if (Input.GetKey(keycode)) {
			held++;
			if (held >= holdTime) {
				shouldTrigger = true;
				held = 0;
			}
		} else {
			held = 0;
		}
		return shouldTrigger;
	}
}


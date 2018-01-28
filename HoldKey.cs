using System;
using System.Collections.Generic;
using UnityEngine;

public class HoldKey
{
	KeyCode keycode;
	int holdTime;
	int held = 0;

	public HoldKey (KeyCode m_keycode, int holdTime)
	{
		keycode = m_keycode;
		holdTime = 50;
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


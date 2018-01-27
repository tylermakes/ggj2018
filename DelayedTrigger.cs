using System.Collections;
using System.Collections.Generic;

public class DelayedTrigger {

	private int delay;
	private int waiting;

	public DelayedTrigger(int m_delay) {
		delay = m_delay;
	}

	public bool trigger() {
		if (canUpdate()) {
			waiting = delay;
			return true;
		} else {
			return false;
		}
	}

	public void update() {
		if (waiting > 0) {
			waiting--;
		}
	}

	public bool canUpdate() {
		return waiting <= 0;
	}

	public void reset() {
		waiting = 0;
	}
}

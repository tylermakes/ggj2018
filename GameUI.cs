using UnityEngine;

public class GameUI {
    private GameObject timerText;
    private TextMesh timerTextMesh;

    private GameObject overlayText;
    private TextMesh overlayTextMesh;
    private GameObject overlay;
	private Vector2 timePos;

    public GameUI()
    {
		timePos = new Vector2(-Utilities.tileSize * 3, Utilities.tileSize * 2);
    }

	public void CreateTimer()
    {
		if (timerText == null) {
			timerText = new GameObject();

			timerTextMesh = timerText.AddComponent<TextMesh>();
			timerText.transform.position = new Vector3(timePos.x - Utilities.tileSize, timePos.y + Utilities.tileSize * 3f, Utilities.GAME_UI_TEXT_LAYER);
			timerTextMesh.fontSize = 72;
			timerTextMesh.text = "==";
		}
	}

    public void UpdateTime(float time) {
        timerTextMesh.text = time.ToString("N0");
	}

	public void DisplayOverlay() {
		if (overlay == null) {
			DestroyTimer ();
			var cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.name = "Background";
			cube.AddComponent<Rigidbody> ();
			cube.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			cube.GetComponent<Renderer> ().material = Utilities.overlay_mat;
			cube.transform.position = new Vector3 (Utilities.tileSize * Utilities.columns / 2, Utilities.tileSize * Utilities.rows / 2, Utilities.GAME_UI_LAYER);
			cube.transform.localScale = new Vector3 (Utilities.tileSize * Utilities.columns * 2, Utilities.tileSize * Utilities.rows * 2, Utilities.thickness / 2);
			overlay = cube;
		}
	}

	public void DisplayOverlayText() {
		if (overlayText == null) {
			overlayText = new GameObject ();
			overlayTextMesh = overlayText.AddComponent<TextMesh> ();
			overlayText.transform.position = new Vector3 (Utilities.tileSize * Utilities.columns / 4, Utilities.tileSize * (Utilities.rows - 4), Utilities.GAME_UI_TEXT_LAYER);
			overlayTextMesh.fontSize = 72;
		}
	}

	public void DisplayEndOverlay(int winnerNumber, int winnerScore) {
		DisplayOverlay ();
		DisplayOverlayText ();
		overlayTextMesh.text = "Winner: Player " + winnerNumber + " with Score: " + winnerScore;
	}

	public void DisplayStartOverlay(string startMessage) {
		DisplayOverlay ();
		DisplayOverlayText ();
		overlayTextMesh.text = startMessage;
	}

	public void DestroyTimer() {
		MonoBehaviour.Destroy(timerTextMesh);
		MonoBehaviour.Destroy(timerText);
	}

	public void DestroyOverlay() {
		MonoBehaviour.Destroy(overlayTextMesh);
		MonoBehaviour.Destroy(overlayText);
		MonoBehaviour.Destroy(overlay);
		overlayTextMesh = null;
		overlayText = null;
		overlay = null;
	}
}
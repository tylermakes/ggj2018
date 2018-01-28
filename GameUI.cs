using UnityEngine;

public class GameUI {
    private GameObject timerText;
    private TextMesh timerTextMesh;

    private GameObject overlayText;
    private TextMesh overlayTextMesh;
    private GameObject overlay;

    public GameUI()
    {
        Vector2 pos = new Vector2(-Utilities.tileSize * 3, Utilities.tileSize * 2);
        CreateTimer(pos);
    }

    private void CreateTimer(Vector2 location)
    {
        timerText = new GameObject();

        timerTextMesh = timerText.AddComponent<TextMesh>();
        timerText.transform.position = new Vector3(location.x - Utilities.tileSize, location.y + Utilities.tileSize * 3f, 0);
        timerTextMesh.fontSize = 72;
        timerTextMesh.text = "60";
    }

    public void UpdateTime(float time) {
       
        timerTextMesh.text = time.ToString("N0");
    }

    public void DisplayEndOverlay(int winnerNumber, int winnerScore) {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = "Background";
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        cube.GetComponent<Renderer>().material = Utilities.overlay_mat;
        cube.transform.position = new Vector3(Utilities.tileSize * Utilities.columns / 2, Utilities.tileSize * Utilities.rows / 2, Utilities.GAME_UI_LAYER);
        cube.transform.localScale = new Vector3(Utilities.tileSize * Utilities.columns * 2, Utilities.tileSize * Utilities.rows * 2, Utilities.thickness / 2);
        overlay = cube;

        overlayText = new GameObject();

        overlayTextMesh = timerText.AddComponent<TextMesh>();
        overlayText.transform.position = new Vector3(Utilities.tileSize * Utilities.columns / 2, Utilities.tileSize * Utilities.rows / 2, Utilities.GAME_UI_LAYER);
        overlayTextMesh.fontSize = 72;
        overlayTextMesh.text = "60";
    }

	public void DestroyInternals() {
		MonoBehaviour.Destroy(timerTextMesh);
		MonoBehaviour.Destroy(timerText);
	}
}
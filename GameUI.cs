using System;
using UnityEngine;

public class GameUI {
    private GameObject timerText;
    private TextMesh timerTextMesh;
    private Action gameEndAction;

    private float timeRemaining = 60.0f;

    public GameUI(Action gameEndAction)
    {
        Vector2 pos = new Vector2(-Utilities.tileSize * 3, Utilities.tileSize * 2);
        CreateTimer(pos);
        this.gameEndAction = gameEndAction;
    }

    private void CreateTimer(Vector2 location)
    {
        timerText = new GameObject();

        timerTextMesh = timerText.AddComponent<TextMesh>();
        timerText.transform.position = new Vector3(location.x - Utilities.tileSize, location.y + Utilities.tileSize * 3f, 0);
        timerTextMesh.fontSize = 72;
        timerTextMesh.text = "60";
    }

    public void Update() {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0) {
            // Game over
            timeRemaining = 0.0f;
            gameEndAction();
            return;
        }
        timerTextMesh.text = timeRemaining.ToString("N0");
    }

	public void DestroyInternals() {
		MonoBehaviour.Destroy(timerTextMesh);
		MonoBehaviour.Destroy(timerText);
	}
}
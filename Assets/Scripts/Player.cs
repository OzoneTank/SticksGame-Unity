using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	public float playerOffsetHeight = 2.0f;
	public float startStickRotationSpeed = 30f;
	public float stickRotationSpeedDelta = 0.5f;
	public GameObject stick;
	private GameState gameState = GameState.Generating;
	public int numOfSticksToSwitchColor = 10;
	public int numOfSticksSinceColorChange = 0;
	public Text score;
	public Vector3 stickOffset = new Vector3 (2, 0, 0);

	private GameObject currentStick;
	private float stickRotation = 0;
	private float stickRotationSpeed = 30f;
	private List<GameObject> sticks = new List<GameObject>();

	private enum GameState {
		Start,
		Generating,
		Holding,
		Dropping,
		GameOver
	}

	void Start() {
		sticks.AddRange (GameObject.FindGameObjectsWithTag ("Stick"));
		stickRotationSpeed = startStickRotationSpeed;
	}

	public void Restart() {
		SceneManager.LoadScene ("MainScene");
	}

	void Update () {
		switch (gameState) {
		case GameState.Generating:
			GenerateStick ();
			break;
		case GameState.Holding:
			RotateCurrentStick ();
			if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump") ||
				Input.touchCount > 0) {
				DropStick ();
			}
			break;
		}
		MoveOverHighestStick ();
	}

	private void RotateCurrentStick() {
		stickRotation += stickRotationSpeed * Time.deltaTime;
		stickRotation %= 360;

		currentStick.transform.RotateAround (transform.position, Vector3.up, stickRotationSpeed * Time.deltaTime);
	}

	private void DropStick() {
		currentStick.GetComponent<Rigidbody> ().isKinematic = false;
		gameState = GameState.Dropping;
	}

	public void Continue() {
		sticks.Add (currentStick);
		gameState = GameState.Generating;
		stickRotationSpeed += stickRotationSpeedDelta;
		numOfSticksSinceColorChange++;
		if (numOfSticksSinceColorChange > numOfSticksToSwitchColor) {
			numOfSticksSinceColorChange = 0;
			GameObject.Find ("Pedestal").GetComponent<Pedestal> ().GenerateNewColors ();
		}
		score.text = "" + (sticks.Count - 2);
	}

	public void GameOver() {
		gameState = GameState.GameOver;
	}

	private void GenerateStick() {
		Vector3 pos = transform.position + stickOffset;

		currentStick = Instantiate (stick, pos, transform.rotation) as GameObject;
		currentStick.transform.RotateAround (transform.position, Vector3.up, stickRotation);

		currentStick.GetComponent<Rigidbody> ().isKinematic = true;
		gameState = GameState.Holding;
	}

	private void MoveOverHighestStick() {
		float maxY = 0;
		for(int i = 0; i < sticks.Count; i++) {
			float y = sticks [i].transform.position.y;

			if (y > maxY) {
				maxY = y;
			}
		}

		Vector3 newPos = new Vector3 (0, maxY + playerOffsetHeight, 0);
		transform.position = Vector3.Lerp (transform.position, newPos, Time.deltaTime);
	}
}

using UnityEngine;
using System.Collections;

public class Stick : MonoBehaviour {

	public bool canTouchPedestal = false;
	private bool hasCollided = false;

	void Start() {
		gameObject.GetComponent<Renderer> ().material.color = new Color(
			Random.Range (0, 1.0f),
			Random.Range (0, 1.0f),
			Random.Range (0, 1.0f)
		);
	}
	void Update () {
		if (transform.position.y < 0) {
			GameOver ();
		}
	}

	private void GameOver(){
		GameObject.Find ("Player").GetComponent<Player> ().GameOver ();
	}

	private void Continue() {
		GameObject.Find ("Player").GetComponent<Player> ().Continue ();
	}

	void OnCollisionEnter(Collision collision) {
		if (!canTouchPedestal) {
			if (collision.gameObject.tag == "Pedestal") {
				GameOver ();
			} else if (!hasCollided) {
				Continue ();
			}
		}
		hasCollided = true;
	}
}

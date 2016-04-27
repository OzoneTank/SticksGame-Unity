using UnityEngine;
using System.Collections;

public class Pedestal : MonoBehaviour {

	private Color backgroundColor = Color.blue;
	private Color newBackgroundColor = Color.red;
	private Color color = Color.red;
	private Color newColor = Color.blue;
	public float colorLikenessThreshold = 0.01f;
	private Renderer pedestalRenderer;

	void Start() {
		pedestalRenderer = gameObject.GetComponent<Renderer> ();
		GenerateNewColors ();
	}

	void Update () {
		color = Color.Lerp (color, newColor, Time.deltaTime / 10);
		backgroundColor = Color.Lerp (backgroundColor, newBackgroundColor, Time.deltaTime / 10);
		pedestalRenderer.material.color = color;
		Camera.main.backgroundColor = backgroundColor;
	}

	public void GenerateNewColors() {
		newColor = GetRandomColor ();
		newBackgroundColor = GetRandomColor ();
	}

	private Color GetRandomColor() {
		return new Color (Random.Range (0, 1.0f),
			Random.Range (0, 1.0f),
			Random.Range (0, 1.0f));
	}
}

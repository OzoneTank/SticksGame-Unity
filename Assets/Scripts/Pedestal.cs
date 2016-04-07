using UnityEngine;
using System.Collections;

public class Pedestal : MonoBehaviour {

	private Renderer pedestalRenderer;
	private Color backgroundColor = Color.blue;
	private Color newBackgroundColor = Color.red;
	private Color color = Color.red;
	private Color newColor = Color.blue;
	public float colorLikenessThreshold = 0.01f;

	void Start() {
		pedestalRenderer = gameObject.GetComponent<Renderer> ();
		GenerateNewColors ();
	}
	
	// Update is called once per frame

	void Update () {
		color = Color.Lerp (color, newColor, Time.deltaTime / 10);
		backgroundColor = Color.Lerp (backgroundColor, newBackgroundColor, Time.deltaTime / 10);
		pedestalRenderer.material.color = color;
		Camera.main.backgroundColor = backgroundColor;
		/*if (ColorsMatch(color, newColor)) {
			newColor = GetRandomColor ();
		}
		if (ColorsMatch(backgroundColor, newBackgroundColor)) {
			newBackgroundColor = GetRandomColor ();
		}*/
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
	/*
	private bool ColorsMatch(Color colorA, Color colorB) {
		return Mathf.Abs (colorA.r - colorB.r) < colorLikenessThreshold &&
			Mathf.Abs (colorA.g - colorB.g) < colorLikenessThreshold &&
			Mathf.Abs (colorA.b - colorB.b) < colorLikenessThreshold;
			
	}*/
}

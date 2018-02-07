using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Component that animates a line based on a FrequencyRange
public class AudioVisualisation : MonoBehaviour {

	// TODO: Abstract this so output is taken from other sources
	public MicrophoneInput Input; 

	// Cache the renderer
	private LineRenderer LineRenderer;

	// Width in game units
	public float Width = 20f;
	// Height in game units
	public float Scale = 1f;

	void Start() {
		LineRenderer = GetComponent<LineRenderer> ();
	}

	void Update() {
		var data = Input.GetSpectrum ();
		var length = data.Length;
		// The line renderer can only do 1024 points, so if length > 1024 increase the step size
		Vector3[] lineCoordinates = new Vector3[length];

		for (var i = 0; i < length; i++) {
			lineCoordinates [i] = new Vector3 (Width * ((1f * i / length) - 0.5f), data[i] * Scale, 0);
		}
		LineRenderer.SetPositions (lineCoordinates);
	}
}

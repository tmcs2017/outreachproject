using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Component that animates a line based on a FrequencyRange
public class AudioVisualisation : MonoBehaviour {

	// TODO: Abstract this so output is taken from other sources
	public MicrophoneInput Input; 

	public AudioVisualisationSource Source;

	// Cache the renderer
	private LineRenderer LineRenderer;

	// Width in game units
	public float Width = 20f;
	// Height in game units
	public float Scale = 1f;

	public float[] SpectrumData;

	void Awake() {
		LineRenderer = GetComponent<LineRenderer> ();
	}

	void LoadMoleculeSpectrum() {
		var spectra = GameObject.FindObjectsOfType<VibrationalModeGraphic> ();
		var spectrum = spectra [0].ExcitationFrequency;
		for (var i = 1; i < spectra.Length; i++)
			spectrum = spectrum + spectra [i].ExcitationFrequency;
		SpectrumData = spectrum.FrequencyData;
	}
		

	void Update() {
		if (Source == AudioVisualisationSource.Microphone)
			SpectrumData = Input.GetSpectrum ();
		else if (Source == AudioVisualisationSource.Molecule) {
			if(SpectrumData.Length == 0)
			LoadMoleculeSpectrum ();
		}
		var length = SpectrumData.Length;
		// The line renderer can only do 1024 points, so if length > 1024 increase the step size
		Vector3[] lineCoordinates = new Vector3[length];

		for (var i = 0; i < length; i++) {
			lineCoordinates [i] = new Vector3 (Width * ((1f * i / length) - 0.5f), SpectrumData[i] * Scale, 0);
		}
		LineRenderer.SetPositions (lineCoordinates);
	}
}

public enum AudioVisualisationSource {
	Microphone,
	Molecule
}
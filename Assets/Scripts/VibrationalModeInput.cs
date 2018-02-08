using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component on a Vibrational Mode GameObject. Connects audio input and causes the vibrational mode to be excited
/// </summary>
public class VibrationalModeInput : MonoBehaviour {

	public FrequencyRange ExcitationFrequency;

	public float GaussianScale = 1f;
	public float GaussianStrength = 1f;

	void Start() {
		var modeObject = GetComponent<VibrationalModeGraphic> ();
		var microphoneInput = GameObject.FindObjectOfType<MicrophoneInput> ();
		var spectrum = new float[microphoneInput.Spectrum.Length];
		var maximumFrequency = microphoneInput.MaximumFrequency;
		var resonanceFrequency = modeObject.AudioFrequency;

		for (int i = 0; i < spectrum.Length; i++) {
			float freq = (1f * i / spectrum.Length) * maximumFrequency;
			spectrum [i] = GaussianScale * Mathf.Exp (-Mathf.Pow (GaussianStrength * (freq - resonanceFrequency), 2));
		}
		ExcitationFrequency = new FrequencyRange (spectrum, 0, maximumFrequency);
	}

	void Update() {
		var microphoneInput = MicrophoneInput.Instance;
		if (microphoneInput != null) {
			var newExcitation = FrequencyRange.Overlap (microphoneInput.Spectrum, ExcitationFrequency);
			var mode = GetComponent<VibrationalModeGraphic> ().VibrationalMode;
			mode.Excitation = 
				Mathf.MoveTowards(mode.Excitation, newExcitation, Time.deltaTime * 2f);
		}
	}
}

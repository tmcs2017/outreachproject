using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component on a Vibrational Mode GameObject. Connects audio input and causes the vibrational mode to be excited
/// </summary>
public class VibrationalModeInput : MonoBehaviour {

	private VibrationalModeGraphic ModeGraphic;

	void Start() {
		ModeGraphic = GetComponent<VibrationalModeGraphic> ();
		var microphoneInput = MicrophoneInput.Instance;
		// If there is no microphone input, remove this component
		if (!microphoneInput) {
			this.enabled = false;
			return;
		}
	}

	void Update() {
		var microphoneInput = MicrophoneInput.Instance;
		if (microphoneInput != null) {
			var newExcitation = FrequencyRange.Overlap (microphoneInput.Spectrum, ModeGraphic.ExcitationFrequency);
			var mode = ModeGraphic.VibrationalMode;
			mode.Excitation = 
				Mathf.MoveTowards(mode.Excitation, newExcitation, Time.deltaTime * 2f);
		}
	}
}

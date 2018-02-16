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
			var target = Mathf.Clamp01 (newExcitation);
			mode.Excitation = 
				Mathf.MoveTowards(mode.Excitation, target, (target > mode.Excitation ? 1f : 0.25f) * Time.deltaTime * 2f);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationalModeGraphic : MonoBehaviour {

	public VibrationalMode VibrationalMode;

	public float WavenumberToAudioFrequency = 0.14f;

	void OnSetMode(VibrationalMode mode) {
		this.VibrationalMode = mode;
	}

	public float AudioFrequency {
		get {
			return WavenumberToAudioFrequency * VibrationalMode.Wavenumber;
		}
	}

}

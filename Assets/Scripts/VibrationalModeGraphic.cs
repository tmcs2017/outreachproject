using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationalModeGraphic : MonoBehaviour {

	public VibrationalMode VibrationalMode;

	public FrequencyRange ExcitationFrequency;

	public float WavenumberToAudioFrequency = 0.14f;

	public float GaussianScale = 1f;
	public float GaussianStrength = 1f;

	void OnSetMode(VibrationalMode mode) {
		this.VibrationalMode = mode;
		var spectrum = new float[AppManager.Instance.NumberOfFrequencies];
		var maximumFrequency = AppManager.Instance.MaximumFrequency;
		var resonanceFrequency = AudioFrequency;
		for (int i = 0; i < spectrum.Length; i++) {
			float freq = (1f * i / spectrum.Length) * maximumFrequency;
			spectrum [i] = GaussianScale * Mathf.Exp (-Mathf.Pow (GaussianStrength * (freq - resonanceFrequency), 2));
		}
		ExcitationFrequency = new FrequencyRange (spectrum, 0, maximumFrequency);
	}

	public bool RoundNote = false;

	public float AudioFrequency {
		get {
			var freq = WavenumberToAudioFrequency * VibrationalMode.Wavenumber;
			// Convert to note
			if(RoundNote) {
				float pitch = (Mathf.Round(69 + 12 * (Mathf.Log(freq/440f) / Mathf.Log(2f)))); 
				freq = 440f*Mathf.Pow(2,(pitch-69)/12f); 
			}
			return freq;
		}
	}

}

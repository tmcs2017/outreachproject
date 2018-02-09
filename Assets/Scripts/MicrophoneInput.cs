using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MicrophoneInput : MonoBehaviour {

	public FFTWindow FourierWindow = FFTWindow.BlackmanHarris;

	public FrequencyRange Spectrum;

	private AudioSource AudioSource;

	public static MicrophoneInput Instance;

	void Awake() {
		Instance = this;
	}

	void Start() {

		// Cache components
		AudioSource = GetComponent<AudioSource>();
		AudioSource.clip = Microphone.Start(null, true, 10, AppManager.Instance.SampleRate);
		AudioSource.loop = true; // Set the AudioClip to loop
		while (!(Microphone.GetPosition(null) > 0)){} // Wait until the recording has started
		AudioSource.Play();
		ReadSpectrum ();

	}


	void Update() {
		ReadSpectrum ();
	}

	void ReadSpectrum(){
		// Get Sound Spectrum as a Fourier Series
		var rawSpectrum = new float[AppManager.Instance.BinSize];
		for (int i = 0; i < AppManager.Instance.BinSize; i++) {
			rawSpectrum [i] = 0;
		}
		AudioSource.GetSpectrumData(rawSpectrum, 0, FFTWindow.BlackmanHarris);
		while (!(Microphone.GetPosition(null) > 0)){}

		var spectrum = new float[AppManager.Instance.NumberOfFrequencies];
		Array.Copy (rawSpectrum, spectrum, AppManager.Instance.NumberOfFrequencies);
		Spectrum = new FrequencyRange (spectrum, 0, AppManager.Instance.MaximumFrequency);
		
	}

	public float[] GetSpectrum() {
		return Spectrum.FrequencyData;
	}
}

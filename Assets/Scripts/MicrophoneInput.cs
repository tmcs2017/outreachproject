using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MicrophoneInput : MonoBehaviour {

	public int BinSize = 4096;
	public FFTWindow FourierWindow = FFTWindow.BlackmanHarris;

	public FrequencyRange Spectrum;
	public int SampleRate;
	public bool AutomaticSampleRate = false;

	public float MaximumFrequency = 256f;

	private int NumberOfFrequencies = 0;

	private AudioSource AudioSource;

	public static MicrophoneInput Instance;

	void Start() {

		// Cache components
		AudioSource = GetComponent<AudioSource>();

		if(AutomaticSampleRate)
			SampleRate = AudioSettings.outputSampleRate;
		AudioSource.clip = Microphone.Start(null, true, 10, SampleRate);
		AudioSource.loop = true; // Set the AudioClip to loop
		while (!(Microphone.GetPosition(null) > 0)){} // Wait until the recording has started
		AudioSource.Play();
		NumberOfFrequencies = FrequencyToIndex (MaximumFrequency);
		ReadSpectrum ();

		Instance = this;


	}

	private float IndexToFrequency(int index) {
		return index * (SampleRate / 2f) / BinSize;;
	}

	private int FrequencyToIndex(float frequency) {
		return (int) (frequency / (SampleRate / 2f) * BinSize);
	}

	void Update() {
		ReadSpectrum ();
	}

	void ReadSpectrum(){
		// Get Sound Spectrum as a Fourier Series
		var rawSpectrum = new float[BinSize];
		for (int i = 0; i < BinSize; i++) {
			rawSpectrum [i] = 0;
		}
		AudioSource.GetSpectrumData(rawSpectrum, 0, FFTWindow.BlackmanHarris);
		while (!(Microphone.GetPosition(null) > 0)){}

		var spectrum = new float[NumberOfFrequencies];
		Array.Copy (rawSpectrum, spectrum, NumberOfFrequencies);
		Spectrum = new FrequencyRange (spectrum, 0, MaximumFrequency);
		
	}

	public float[] GetSpectrum() {
		return Spectrum.FrequencyData;
	}
}

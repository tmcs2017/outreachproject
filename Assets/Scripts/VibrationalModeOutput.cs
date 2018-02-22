using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VibrationalModeOutput : MonoBehaviour
{
	private VibrationalModeGraphic ModeObject;
	public AudioSource audioSource;

	int Position = 0;
	private int SampleRate;
	private float Frequency = 440;

	void Start()
	{
		ModeObject = GetComponent<VibrationalModeGraphic> ();
		SampleRate = AudioSettings.outputSampleRate;
		Frequency = ModeObject.AudioFrequency;
		audioSource = GetComponent<AudioSource>();
		audioSource.pitch = Frequency / 261.6f;
		//audioSource.Play();
		SetVolume (0);
	}

	public void Update() {
		if (ModeObject.VibrationalMode.Excitation == 1) {
			audioSource = GetComponent<AudioSource>();
			audioSource.Play();
		} else if (ModeObject.VibrationalMode.Excitation > 0.1) {
			SetVolume (ModeObject.VibrationalMode.Excitation);
		} else {
			SetVolume (0);
		}
	}

	void SetVolume(float volume) {
		GetComponent<AudioSource> ().volume = Mathf.Clamp01 (volume);
	}
}
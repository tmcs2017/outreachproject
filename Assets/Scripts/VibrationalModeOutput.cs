using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VibrationalModeOutput : MonoBehaviour
{
	private VibrationalModeGraphic ModeObject;

	int Position = 0;
	private int SampleRate;
	private float Frequency = 440;

	void Start()
	{
		ModeObject = GetComponent<VibrationalModeGraphic> ();
		SampleRate = AudioSettings.outputSampleRate;
		Frequency = ModeObject.AudioFrequency;
		AudioClip clip = AudioClip.Create("Audio", SampleRate * 2, 1, SampleRate, true, OnAudioRead, OnAudioSetPosition);
		AudioSource audioSource = GetComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.Play();
		SetVolume (0);
	}

	void OnAudioRead(float[] data)
	{
		int count = 0;
		while (count < data.Length)
		{
			data[count] = Mathf.Sign(Mathf.Sin(2 * Mathf.PI * Frequency * Position / SampleRate));
			Position++;
			count++;
		}
	}

	void OnAudioSetPosition(int newPosition)
	{
		Position = newPosition;
	}

	public void Update() {
		SetVolume (ModeObject.VibrationalMode.Excitation);
	}

	void SetVolume(float volume) {
		GetComponent<AudioSource> ().volume = Mathf.Clamp01 (volume);
	}
}
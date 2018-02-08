using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FrequencyRange {

	public FrequencyRange(float[] data, float min, float max) {
		this.FrequencyData = data;
		this.MinimumFrequency = min;
		this.MaximumFrequency = max;
	}

	public float[] FrequencyData;

	public float MinimumFrequency;

	public float MaximumFrequency;

	public int Length {
		get {
			return FrequencyData.Length;
		}
	}

	public static float Overlap(FrequencyRange reading, FrequencyRange area) {
		float v = 0f;
		float tot = 0f;
		for (var i = 0; i < reading.Length; i++) {
			var inputValue = reading.FrequencyData [i];
			var targetValue = area.FrequencyData [i];
			v += Mathf.Clamp01 (inputValue / targetValue) * targetValue;
			tot += targetValue;
		}
		return v / tot;
	}

	// Implements addition of frequency ranges
	public static FrequencyRange operator +(FrequencyRange c1, FrequencyRange c2)
	{
		var data = new float[c1.FrequencyData.Length];
		for (var i = 0; i < data.Length; i++)
			data [i] = c1.FrequencyData[i] + c2.FrequencyData [i];
		return new FrequencyRange (data, c1.MinimumFrequency, c1.MaximumFrequency);
	}
		
}

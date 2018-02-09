using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationKeyboard : MonoBehaviour {

	public RectTransform ButtonPrefab;
	public RectTransform ButtonParent;

	// Use this for initialization
	void Start () {
		var vibrationalModes = FindObjectsOfType<VibrationalModeGraphic> ();
		foreach (var mode in vibrationalModes) {
			var button = GameObject.Instantiate (ButtonPrefab, ButtonParent);
			button.gameObject.SetActive (true);
			button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
				foreach(var m in vibrationalModes) {
					m.VibrationalMode.Excitation = 0; 
				}
				mode.VibrationalMode.Excitation = 1f;
			});
		}
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoleculeSelectionManager : MonoBehaviour {

	public MoleculeDefinition SelectedMolecule = null;

	public RectTransform UIHighlightParent;
	public RectTransform UIHighlightPrefab;

	public RectTransform UIMoleculePanel;

	// Use this for initialization
	void Start () {
        RenderSettings.skybox = AppManager.Instance.CurrentMoleculeGroup.Skybox;
        DynamicGI.UpdateEnvironment();
		var box = this.GetComponent<SimulationBox> ();
		int i = 0;
        foreach (var definition in AppManager.Instance.CurrentMoleculeGroup.Definitions) {
			var particle = box.Particles [i];
			particle.GetComponent<MoleculeSelectionItem> ().SetMolecule (definition);
			i++;

			// Add UI;
			var ui = GameObject.Instantiate (UIHighlightPrefab, UIHighlightParent);
			ui.GetComponent<UIMoleculeHighlight> ().SetMolecule (particle);
			ui.GetComponent<UnityEngine.UI.Button> ().onClick.AddListener (() => {
				this.SelectMolecule(definition);
			});
			ui.gameObject.SetActive (true);
		}
	}

	void SelectMolecule(MoleculeDefinition definition) {
		this.SelectedMolecule = definition;
		if (definition != null) {
			UIMoleculePanel.gameObject.SetActive (true);
		} else {
			UIMoleculePanel.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	public void OpenMoleculeView() {
		AppManager.Instance.SelectedMolecule = this.SelectedMolecule;
		SceneManager.LoadScene ("Molecule View");
	}
}

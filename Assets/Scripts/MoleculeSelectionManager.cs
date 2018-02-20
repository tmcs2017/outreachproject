using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeSelectionManager : MonoBehaviour {

	public List<MoleculeDefinition> Definitions = new List<MoleculeDefinition>();

	// Use this for initialization
	void Start () {
		var box = this.GetComponent<SimulationBox> ();
		int i = 0;
		foreach (var definition in Definitions) {
			var particle = box.Particles [i];
			particle.GetComponent<MoleculeSelectionItem> ().SetMolecule (definition);
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

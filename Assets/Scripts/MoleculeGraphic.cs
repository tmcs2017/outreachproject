using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoleculeGraphic : MonoBehaviour {

	public Molecule Molecule;
	private List<AtomGraphic> Atoms;

	void Start() {
		Atoms = this.GetComponentsInChildren<AtomGraphic> ().ToList();
	}

	void OnSetMolecule(Molecule molecule) {
		this.Molecule = molecule;
	}

	void Update() {
		float time = Time.time;
		int atomIndex = 0;
		foreach (var atom in Atoms) {
			var position = atom.Atom.MolecularPosition;
			foreach (var mode in Molecule.VibrationalModes) {
				var displacement = mode.Displacements [atomIndex];
				position += mode.Excitation * 0.4f * Mathf.Cos (8f*Time.time) * displacement;
			}
			atom.transform.localPosition = position;
			atomIndex++;
		}
	}

}

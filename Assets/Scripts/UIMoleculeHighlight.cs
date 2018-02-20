using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoleculeHighlight : MonoBehaviour {

	public SimulationParticle Molecule;

	private Camera Camera;

	public void SetMolecule(SimulationParticle molecule) {
		this.Molecule = molecule;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var pos = Camera.main.WorldToScreenPoint (Molecule.transform.position);
		var radius = Mathf.Max (
			(pos - Camera.main.WorldToScreenPoint (Molecule.transform.position + Vector3.left * Molecule.Radius)).magnitude,
			(pos - Camera.main.WorldToScreenPoint (Molecule.transform.position + Vector3.right * Molecule.Radius)).magnitude,
			(pos - Camera.main.WorldToScreenPoint (Molecule.transform.position + Vector3.up * Molecule.Radius)).magnitude,
			(pos - Camera.main.WorldToScreenPoint (Molecule.transform.position + Vector3.down * Molecule.Radius)).magnitude
		);
		this.transform.position = pos;
	
		this.GetComponent<RectTransform> ().sizeDelta = Vector2.one * 3f * radius;
	}
}

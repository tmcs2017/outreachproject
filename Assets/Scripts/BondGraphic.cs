using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Component for an object with represents a Bond Graphic
public class BondGraphic : MonoBehaviour {

	public Transform BondPart1;
	public Transform BondPart2;

	public GameObject Atom1;
	public GameObject Atom2;

	public void SetAtoms(GameObject atom1, GameObject atom2) {
		Atom1 = atom1;
		Atom2 = atom2;
		BondPart1.GetComponent<Renderer>().material.color = atom1.GetComponent<Renderer>().material.color;
		BondPart2.GetComponent<Renderer>().material.color = atom2.GetComponent<Renderer>().material.color;
	}

	public void Update() {
		Vector3 Position1 = Atom1.transform.position;
		Vector3 Position2 = Atom2.transform.position;
		Vector3 Midpoint = 0.5f * (Position1 + Position2);
		float length = (Position1 - Midpoint).magnitude;
		//scale of the whole molecule
		float scale = Atom1.transform.parent.localScale[1];

		BondPart1.transform.position = 0.5f * (Position1 + Midpoint);
		BondPart2.transform.position = 0.5f * (Position2 + Midpoint);
		//uses the scale of the parent molecule to resize during zoom
		BondPart1.transform.localScale = new Vector3 (0.3f, length/(2f * scale), 0.3f);
		BondPart2.transform.localScale = new Vector3 (0.3f, length/(2f * scale), 0.3f);
		var rotation = Quaternion.FromToRotation (Vector3.up, (Position2 - Position1).normalized);
		BondPart1.transform.rotation = rotation;
		BondPart2.transform.rotation = rotation;
	}

}

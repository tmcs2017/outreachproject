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
		BondPart1.GetComponent<Renderer>().material.color = atom2.GetComponent<Renderer>().material.color;
		BondPart2.GetComponent<Renderer>().material.color = atom1.GetComponent<Renderer>().material.color;
	}

	public void Update() {
		var moleculeParent = Atom1.transform.parent;

		Vector3 Position1 = moleculeParent.InverseTransformPoint(Atom1.transform.position); // Local position of Atom 1
		Vector3 Position2 = moleculeParent.InverseTransformPoint(Atom2.transform.position); // Local position of Atom 2;
		Vector3 Midpoint = 0.5f * (Position1 + Position2); // Local midpoint of bond
		float length = (Position1 - Midpoint).magnitude; // Local distance between midpoint and atom posiiton

		this.transform.localPosition = Midpoint;
		this.transform.localScale = new Vector3 (0.4f, length, 0.4f);
	
		var rotation = Quaternion.FromToRotation (Vector3.up, (Position2 - Position1).normalized);
		this.transform.localRotation = rotation;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Component that configures the renderer for an Atom
public class AtomGraphic : MonoBehaviour {

	public Atom Atom;

	void OnSetAtom(Atom atom) {
		this.Atom = atom;
		var renderer = this.GetComponent<Renderer> ();
		renderer.material.color = atom.Element.Color;
		this.transform.localScale = Vector3.one * atom.Element.Radius;
	}

}

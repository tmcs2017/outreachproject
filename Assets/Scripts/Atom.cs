using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Represents an atom in a molecule
public class Atom {

	public Atom(Molecule molecule, AtomDefinition definition) {
		this.Molecule = molecule;
		this.MolecularPosition = definition.Position;
		this.Element = definition.Element;
	}
		
	/// Molecule in which this atom is contained
	public Molecule Molecule;

	/// 3D vector representing the position relative to some origin of the molecule
	public Vector3 MolecularPosition;

	public Element Element;

}

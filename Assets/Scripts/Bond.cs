using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Represents a bond in a Molecule
public class Bond {

	public Molecule Molecule;
	public Atom Atom1;
	public Atom Atom2;

	public Bond(Molecule molecule, BondDefinition def) : this(molecule, molecule.Atoms[def.AtomIndex1], molecule.Atoms[def.AtomIndex2]) {
	}

	public Bond(Molecule molecule, Atom atom1, Atom atom2) {
		this.Molecule = molecule;
		this.Atom1 = atom1;
		this.Atom2 = atom2;
	}

}

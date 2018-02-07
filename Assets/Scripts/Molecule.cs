using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molecule {

	public List<Atom> Atoms = new List<Atom>();
	public List<Bond> Bonds = new List<Bond>();
	public List<VibrationalMode> VibrationalModes = new List<VibrationalMode>();

	public Molecule(MoleculeDefinition definition) {
		
	}
		
}
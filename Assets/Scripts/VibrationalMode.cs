using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VibrationalMode  {

	public VibrationalMode(Molecule molecule, VibrationalModeDefinition def) {
		this.Molecule = molecule;
		this.Wavenumber = def.Wavenumber;
		this.Displacements = def.Displacements;
	}

	public Molecule Molecule;

	public float Wavenumber;

	public List<Vector3> Displacements;

	public float Excitation = 0f;
		
}

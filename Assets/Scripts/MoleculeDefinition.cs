using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Molecule", menuName = "Molecule")]
public class MoleculeDefinition : ScriptableObject {

	public List<AtomDefinition> Atoms = new List<AtomDefinition>();

	public List<VibrationalModeDefinition>  VibrationalModes = new List<VibrationalModeDefinition>();

	public List<BondDefinition> Bonds = new List<BondDefinition>();

}


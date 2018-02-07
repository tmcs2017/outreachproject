using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Represents a bond in a molecule definition
[System.Serializable]
public class BondDefinition {
	public int AtomIndex1;
	public int AtomIndex2;
	public int BondOrder;
}

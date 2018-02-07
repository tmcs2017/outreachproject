using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Represents an atom in a predefined molecule. This does not represent a certain atom (see Atom)
[System.Serializable]
public class AtomDefinition
{
	/// 3D vector representing the position relative to some origin of the molecule
	public Vector3 Position;

	public Element Element;

}

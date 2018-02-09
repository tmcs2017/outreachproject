using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a molecule at runtime
/// </summary>
public class MoleculeSpawn : MonoBehaviour {

	public MoleculeDefinition Definition;

	// Draws a ball-and-stick model in the editor, to represent what the molecule will look like
	public void OnDrawGizmos() {
		// Return if there is no definition to draw
		if (Definition == null)
			return;
		foreach (var atomic_position in Definition.Atoms) {
			var element = atomic_position.Element;
			var position = atomic_position.Position;
			// Convert position relative to molecule to position relative to global axes
			var worldPosition = this.transform.TransformPoint (position);
			// Set the color
			Gizmos.color = element.Color;
			// Draw the ball
			Gizmos.DrawSphere(worldPosition, element.Radius);
		}
		foreach (var bond in Definition.Bonds) {
			var atom1 = Definition.Atoms [bond.AtomIndex1];
			var atom2 = Definition.Atoms [bond.AtomIndex2];
			// Convert position relative to molecule to position relative to global axes
			var atomPosition1 = this.transform.TransformPoint (atom1.Position);
			var atomPosition2 = this.transform.TransformPoint (atom2.Position);
			// Set the color
			Gizmos.color = atom1.Element.Color;
			// Draw the line
			Gizmos.DrawLine(atomPosition1, atomPosition2);
		}
	}

	public void Start() {
		var molecule = AppManager.Instance.CreateMolecule (Definition);
		AppManager.Instance.CreateMoleculeGraphic (molecule, this.transform.position, this.transform.rotation);
	}

}

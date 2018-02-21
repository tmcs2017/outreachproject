using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoleculeSelectionItem : MonoBehaviour {

	public MoleculeDefinition Value;

	public void SetMolecule(MoleculeDefinition definition) {

		this.Value = definition;


		var molecule = AppManager.Instance.CreateMolecule (definition);
		var unityMolecule = AppManager.Instance.CreateMoleculeGraphic(molecule, this.transform.position, this.transform.rotation);
		unityMolecule.transform.parent = this.transform;

		var simulationParticle = GetComponent<SimulationParticle> ();

		simulationParticle.Radius = unityMolecule.GetComponentsInChildren<AtomGraphic> ().Select (atom => atom.transform.localPosition.magnitude).Max () * 1.1f;
	}

}

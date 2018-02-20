using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeSelectionItem : MonoBehaviour {

	public void SetMolecule(MoleculeDefinition definition) {
		var molecule = AppManager.Instance.CreateMolecule (definition);
		var unityMolecule = AppManager.Instance.CreateMoleculeGraphic(molecule, this.transform.position, this.transform.rotation);
		unityMolecule.transform.parent = this.transform;
	}

}

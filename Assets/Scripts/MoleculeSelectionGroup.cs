using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object which represents a set of molecules to be displayed for selection, i.e. the Atmosphere or Space view
/// </summary>
[CreateAssetMenu(fileName = "Selection Group", menuName = "Selection Group")]
public class MoleculeSelectionGroup : ScriptableObject {

    public List<MoleculeDefinition> Definitions = new List<MoleculeDefinition>();

    public Material Skybox;

}

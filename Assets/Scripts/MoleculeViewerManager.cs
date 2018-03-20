using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeViewerManager : MonoBehaviour {

    void Start()
    {
        RenderSettings.skybox = AppManager.Instance.CurrentMoleculeGroup.Skybox;
        DynamicGI.UpdateEnvironment();
    }
}

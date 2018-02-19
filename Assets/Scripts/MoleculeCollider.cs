using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeCollider : MonoBehaviour {
	public SphereCollider sphere;

	public void Start() {
		sphere = gameObject.AddComponent<SphereCollider>();
		sphere.center = Vector3.zero;
		sphere.radius = 3.0f;
	}

}

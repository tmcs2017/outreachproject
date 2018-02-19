using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RotateMolecule : MonoBehaviour {
	private float rotSpeed = 280;
	private bool dragging = false;

	void Update() {
		if (Input.GetMouseButtonDown(1) && !dragging){
			dragging = true;
		}
		if (Input.GetMouseButton(1) && dragging){
			float rotX = Input.GetAxis ("Mouse X") * rotSpeed * Mathf.Deg2Rad;
			float rotY = Input.GetAxis ("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

			transform.Rotate (Vector3.up, -rotX, Space.World);
			transform.Rotate (Vector3.right, rotY, Space.World);
		}
		if (Input.GetMouseButtonUp(1) && dragging){
			dragging = false;
		}
	}
}

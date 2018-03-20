using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RotateMolecule : MonoBehaviour {
	private float rotSpeed = 280;
	private bool dragging = false;

    public int MouseButton = 0;

	void Update() {
        if (Input.GetMouseButtonDown(MouseButton) && !dragging){
			dragging = true;
		}
        if (Input.GetMouseButton(MouseButton) && dragging){
			float rotX = Input.GetAxis ("Mouse X") * rotSpeed * Mathf.Deg2Rad;
			float rotY = Input.GetAxis ("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

			transform.Rotate (Vector3.up, -rotX, Space.World);
			transform.Rotate (Vector3.right, rotY, Space.World);
		}
        if (Input.GetMouseButtonUp(MouseButton) && dragging){
			dragging = false;
		}
	}
}

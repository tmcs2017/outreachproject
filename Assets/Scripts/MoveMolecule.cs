using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MoveMolecule : MonoBehaviour {
	private bool dragging = false;
	private float distance;
	private Ray ray, rayIni;
	private Vector3 rayPoint, rayPointIni, rayPointDiff;

	void Update() {
		if (Input.GetMouseButtonDown(0) && !dragging){
			distance = Vector3.Distance(transform.position, Camera.main.transform.position);
			rayIni = Camera.main.ScreenPointToRay(Input.mousePosition);
			rayPointIni = rayIni.GetPoint(distance);
			dragging = true;
		}
		if (Input.GetMouseButton(0) && dragging){
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			rayPoint = ray.GetPoint(distance);
			rayPointDiff = rayPoint - rayPointIni;
			rayPointDiff[2] = 0f;
			transform.position += rayPointDiff;
			rayPointIni = rayPoint;
		}
		if (Input.GetMouseButtonUp(0) && dragging){
			dragging = false;
		}
	}
}

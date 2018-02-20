using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumblingMotion : MonoBehaviour {

	public Vector3 CurrentAxis;
	public Vector2 NextAxis;

	// Muliplier applied to velocity
	public float Torque = 1f;

	float CurrentTime = 0f;

	// Time between switching directions
	public float AlternatingTime = 4f;

	// Use this for initialization
	void Start () {
		CurrentAxis = Random.onUnitSphere;
		NextAxis = Random.onUnitSphere;
	}
	
	// Update is called once per frame
	void Update () {
		CurrentTime += Time.deltaTime / AlternatingTime;
		if (CurrentTime >= 1f) {
			CurrentTime = Mathf.Repeat(CurrentTime, 1f); // Wraps value back to [0,1)
			CurrentAxis = NextAxis;
			NextAxis = Random.onUnitSphere;
		}
		this.GetComponent<Rigidbody> ().AddTorque (Torque * Vector3.Slerp (CurrentAxis, NextAxis, CurrentTime), ForceMode.Impulse);
	}
}

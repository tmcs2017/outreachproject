using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationParticle : MonoBehaviour {

	public SimulationBox Box;

	Vector2 CurrentDirection;
	Vector2 DesiredDirection;

	// Muliplier applied to velocity
	public float Velocity = 1f;

	float CurrentTime = 0f;

	// Time between switching directions
	public float AlternatingTime = 4f;

	// Use this for initialization
	void Start () {
		CurrentDirection = Random.insideUnitCircle;
		DesiredDirection = Random.insideUnitCircle;
	}
	
	// Update is called once per frame
	void Update () {
		CurrentTime += Time.deltaTime / AlternatingTime;
		if (CurrentTime >= 1f) {
			CurrentTime = Mathf.Repeat(CurrentTime, 1f); // Wraps value back to [0,1)
			CurrentDirection = DesiredDirection;
			DesiredDirection = Random.insideUnitCircle;
		}

		Vector2 force = Vector2.zero;
		force += Box.GetBoxContainmentForce (this.transform.localPosition);
		force += Velocity * Vector2.Lerp (CurrentDirection, DesiredDirection, CurrentTime); 
		force += Box.GetRepulsiveForce (this);
		this.GetComponent<Rigidbody2D> ().AddForce (force * Time.deltaTime, ForceMode2D.Impulse);
	}
}

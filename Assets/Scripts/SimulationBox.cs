using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Component that contains particles bouncing around in a cool manner
public class SimulationBox : MonoBehaviour {

	public List<SimulationParticle> Particles = new List<SimulationParticle>();

	public Vector2 BoxSize = Vector2.one;

	public float BoxContainmentStrength = 1f;
	public float RepulsionStrength = 1f;

	public RectTransform UIHighlightParent;
	public RectTransform UIHighlightPrefab;


	// Draw the box in the editor for easier visualisation
	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube (Vector3.zero, BoxSize);
		Gizmos.color = Color.white;
	}

	void Awake() {
		foreach (var particle in Particles)
			AddParticle (particle);
	}

	public void AddParticle(SimulationParticle particle)
	{
		if(!Particles.Contains(particle))
			Particles.Add (particle);
		particle.Box = this;
		var ui = GameObject.Instantiate (UIHighlightPrefab, UIHighlightParent);
		ui.GetComponent<UIMoleculeHighlight> ().SetMolecule (particle);
		ui.gameObject.SetActive (true);
	}

	// Randomly chooses a velocity that prevents the particle leaving the screen
	public Vector2 GetBoxContainmentForce(Vector2 particlePosition) {
		return - BoxContainmentStrength * new Vector2 (Mathf.Pow (particlePosition.x * 2f / BoxSize.x, 3f), Mathf.Pow (particlePosition.y * 2f / BoxSize.y, 3f));
	}

	public Vector2 GetRepulsiveForce(SimulationParticle particle) {
		Vector2 force = Vector2.zero;
		foreach(var otherParticle in Particles) {
			if (particle == otherParticle)
				continue;
			Vector2 seperation = otherParticle.transform.localPosition - particle.transform.localPosition;
			force += RepulsionStrength * -seperation / Mathf.Pow (seperation.magnitude/particle.Radius, 2f) - RepulsionStrength * 4f * seperation / Mathf.Pow (seperation.magnitude - particle.Radius, 4f);
		}
		return force;
	}

}

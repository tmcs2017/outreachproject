using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "Element")]
public class Element : ScriptableObject {

	public int AtomicNumber = 1;
	public Color Color = Color.white;
	public float Radius = 1f;

}

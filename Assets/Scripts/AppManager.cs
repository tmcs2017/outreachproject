using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// Singleton class, with one instance that exists in Assets/Resources. This is loading when the program starts
[CreateAssetMenu(fileName="Manager")]
public class AppManager : ScriptableObject {

	/// Global instance, use AppManager.Instance to access this object
	public static AppManager Instance = null;

	[Header("Audio Input")]
	public int BinSize = 4096;
	public float MaximumFrequency = 256f;
	public int SampleRate {
		get {
			return AudioSettings.outputSampleRate;
		}
	}
	[HideInInspector]
	public int NumberOfFrequencies;

	public float IndexToFrequency(int index) {
		return index * (SampleRate / 2f) / BinSize;;
	}

	public int FrequencyToIndex(float frequency) {
		return (int) (frequency / (SampleRate / 2f) * BinSize);
	}

	void Awake() {
		NumberOfFrequencies = FrequencyToIndex (MaximumFrequency);
		Debug.Log (NumberOfFrequencies);
	}

	[Header("Prefabs")]

	/// Prefabs used for constructing molecules. Assign this through the Editor
	public GameObject MoleculeGraphicPrefab;
	public GameObject AtomGraphicPrefab;
	public GameObject BondGraphicPrefab;
	public GameObject VibrationalModePrefab;

	/// Creates an ingame visualisation of a molecule at a certain position and rotation
	public void CreateMoleculeGraphic(Molecule molecule, Vector3 position, Quaternion rotation) {

		var moleculeObject = GameObject.Instantiate (MoleculeGraphicPrefab, position, rotation);

		moleculeObject.SendMessage ("OnSetMolecule", molecule, SendMessageOptions.DontRequireReceiver);

		Dictionary<Atom, GameObject> Atoms = new Dictionary<Atom, GameObject> ();

		foreach (var atom in molecule.Atoms) {

			var atomObject = GameObject.Instantiate (
				AtomGraphicPrefab,
				moleculeObject.transform.TransformPoint(atom.MolecularPosition),
				moleculeObject.transform.rotation,
				moleculeObject.transform
			);
			Atoms.Add (atom, atomObject);
			atomObject.SendMessage ("OnSetAtom", atom, SendMessageOptions.DontRequireReceiver);

		}

		foreach (var mode in molecule.VibrationalModes) {

			var modeObject = GameObject.Instantiate (
				VibrationalModePrefab,
				moleculeObject.transform
			);
			modeObject.SendMessage ("OnSetMode", mode, SendMessageOptions.DontRequireReceiver);

		}

		foreach (var bond in molecule.Bonds) {

			var bondObject = GameObject.Instantiate (
				BondGraphicPrefab,
				moleculeObject.transform
			);
			bondObject.SendMessage ("OnSetBond", bond, SendMessageOptions.DontRequireReceiver);
			bondObject.GetComponent<BondGraphic> ().SetAtoms (Atoms [bond.Atom1], Atoms [bond.Atom2]);
		}

		moleculeObject.AddComponent<MoleculeCollider> ();
		moleculeObject.AddComponent<RotateMolecule> ();
		moleculeObject.AddComponent<MoveMolecule> ();
		moleculeObject.AddComponent<ZoomMolecule> ();
	}

	/// Creates a molecule from a definition. NOTE: Does not actually create it in game, for this then call CreateMoleculeGraphic
	public Molecule CreateMolecule(MoleculeDefinition definition) {

		var molecule = new Molecule(definition);

		foreach (var atomDefinition in definition.Atoms) {
			var atom = new Atom (molecule, atomDefinition);
			molecule.Atoms.Add (atom);
		}

		foreach (var modeDefinition in definition.VibrationalModes) {
			var mode = new VibrationalMode (molecule, modeDefinition);
			molecule.VibrationalModes.Add (mode);
		}

		foreach (var bondDefinition in definition.Bonds) {
			var bond = new Bond (molecule, bondDefinition);
			molecule.Bonds.Add (bond);
		}

		return molecule;
	}

	private static AppManager FindAppManager() {
		return Resources.Load<AppManager>("Manager");
	}

	/// Sets the instance as soon as the game is run
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void OnBeforeSceneLoadRuntimeMethod()
	{
		AppManager.Instance = AppManager.FindAppManager ();
		if (AppManager.Instance == null) {
			Debug.LogError ("Failed to find AppManager at Resources/Manager");
			Application.Quit ();
		}
		AppManager.Instance.Awake ();
	}

}

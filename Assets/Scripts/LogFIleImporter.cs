using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.AssetImporters;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using System.Linq;
using System;


[ScriptedImporter(1, "log")]
public class LogFileImporter : ScriptedImporter
{
	public string MoleculeName = "";
	public string MoleculeDescription = "";

	public override void OnImportAsset(AssetImportContext ctx)
	{
		var lines = File.ReadAllLines (ctx.assetPath);

		int modeCoordinates = 0;
		int maxModeCoordinates = 0;

		var coordParser = new CoordinateParser ();
		var modeParser = new ModeParser ();

		MoleculeDefinition molecule = new MoleculeDefinition ();
		molecule.Name = MoleculeName;
		molecule.Description = MoleculeDescription;

		foreach (var line in lines) {
			
			coordParser.ParseLine (molecule, line);

			modeParser.ParseLine (molecule, line);

			if (molecule.VibrationalModes.Count > 0) {
				var regexBond = @" ! R\d+ +R\((\d+),(\d+)\)";
				var match = Regex.Match (line, regexBond);
				if (match.Success) {
					var atom1 = int.Parse (match.Groups [1].Value) - 1;
					var atom2 = int.Parse (match.Groups [2].Value) - 1;
					molecule.Bonds.Add (new BondDefinition () { AtomIndex1 = atom1, AtomIndex2 = atom2 });
				}
			}

		}

		ctx.AddObjectToAsset ("Data", molecule);
		ctx.SetMainObject (molecule);

	}

	class CoordinateParser {
		public string[] InitialLines = new string[] {
			" ---------------------------------------------------------------------",
			" Center     Atomic      Atomic             Coordinates (Angstroms)",
			" Number     Number       Type             X           Y           Z",
			" ---------------------------------------------------------------------"
		};

		public int index = 0;

		public bool ParseLine(MoleculeDefinition molecule, string line) {
			var regexCoordLine = "[ ]+(\\d+)[ ]+(\\d+)[ ]+(\\d+)[ ]+(-?\\d+.\\d+)[ ]+(-?\\d+.\\d+)[ ]+(-?\\d+.\\d+)";

			bool isLookingForActualInput = index > InitialLines.Length - 1;
			if (isLookingForActualInput) {
				var match = Regex.Match (line, regexCoordLine);
				if (match.Success) {
					var atomicNumber = int.Parse (match.Groups [2].Value.Trim ());

					var x = float.Parse (match.Groups [4].Value.Trim ());
					var y = float.Parse (match.Groups [5].Value.Trim ());
					var z = float.Parse (match.Groups [6].Value.Trim ());
					molecule.Atoms.Add(new AtomDefinition() {
						Position = new Vector3(x,y,z),
						Element = AssetDatabase.FindAssets ("t:Element")
							.Select(e => AssetDatabase.LoadAssetAtPath<Element>(AssetDatabase.GUIDToAssetPath(e)))
							.Where(e => e != null && e.AtomicNumber == atomicNumber).FirstOrDefault()
					});
					return true;
				} else {
					index = 0;
					return false;
				}
			} else {
				string desiredLine = InitialLines [index];
				if (line.Equals(desiredLine)) {
					index++;
					if (index == InitialLines.Length) {
						molecule.Atoms.Clear ();
					}
					return true;
				}
				index = 0;
				return false;
			}


		}

	}

	class ModeParser {
		public string[] InitialLines = new string[] {
			" Harmonic frequencies (cm**-1), IR intensities (KM/Mole), Raman scattering",
			" activities (A**4/AMU), depolarization ratios for plane and unpolarized",
			" incident light, reduced masses (AMU), force constants (mDyne/A),", 
			" and normal coordinates:"
		};

		public int index = 0;
		public int freqIndex = 0;

		private List<VibrationalModeDefinition> CurrentModes = new List<VibrationalModeDefinition>();

		public bool ParseLine(MoleculeDefinition molecule, string line) {

			string freqLineRegex = @" Frequencies --[ ]+(-?\d+.\d+)(?:[ ]+(-?\d+.\d+))?(?:[ ]+(-?\d+.\d+))?";

			string pointsLineRegex = @"[ ]+ \d+[ ]+ \d+[ ]+(-?\d+.\d+)[ ]+(-?\d+.\d+)[ ]+(-?\d+.\d+)(?:[ ]+(-?\d+.\d+)[ ]+(-?\d+.\d+)[ ]+(-?\d+.\d+))?(?:[ ]+(-?\d+.\d+)[ ]+(-?\d+.\d+)[ ]+(-?\d+.\d+))?";

			int blockSize = 7 + molecule.Atoms.Count;
			//var regexCoordLine = "[ ]+(\\d+)[ ]+(\\d+)[ ]+(\\d+)[ ]+(-?\\d+.\\d+)[ ]+(-?\\d+.\\d+)[ ]+(-?\\d+.\\d+)";

			bool isLookingForActualInput = index > InitialLines.Length - 1;
			if (isLookingForActualInput) {
				if (line.Count() == 0) {
					index = 0;
					return false;
				} else {
					if (freqIndex == 2) {
						var match = Regex.Match (line, freqLineRegex);
						int n = 3;
						if (!match.Groups [3].Success)
							n = 2;
						if (!match.Groups [2].Success)
							n = 1;
						Debug.Log ("Found " + n);
						CurrentModes.Clear ();
						for (var i = 1; i <= n; i++) {
							Debug.Log (match.Groups [i].Value);
							var freq = float.Parse (match.Groups [i].Value);
							var mode = new VibrationalModeDefinition ();
							mode.Wavenumber = freq;
							CurrentModes.Add (mode);
						}
					}
					if (freqIndex >= 7) {
						var match = Regex.Match (line, pointsLineRegex);
						int n = 3;
						if (!match.Groups [7].Success)
							n = 2;
						if (!match.Groups [4].Success)
							n = 1;
						for (var i = 0; i < n; i++) {
							var x = float.Parse (match.Groups [1 + i*3].Value);
							var y = float.Parse (match.Groups [2 + i*3].Value);
							var z = float.Parse (match.Groups [3 + i*3].Value);
							CurrentModes [i].Displacements.Add (new Vector3 (x, y, z));
						}
					}
					freqIndex++;
					if (freqIndex == blockSize) {
						freqIndex = 0;
						molecule.VibrationalModes.AddRange (CurrentModes);
						CurrentModes.Clear ();
					}
				}
				return true;

			} else {
				string desiredLine = InitialLines [index];
				if (line.Equals(desiredLine)) {
					index++;
					//if (index == InitialLines.Length) {
					//	molecule.Atoms.Clear ();
					//}
					return true;
				}
				index = 0;
				return false;
			}


		}

	}
}
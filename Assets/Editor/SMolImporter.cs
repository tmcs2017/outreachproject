using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.AssetImporters;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;

[ScriptedImporter(1, "smol")]
public class SMolImporter : ScriptedImporter
{
	public override void OnImportAsset(AssetImportContext ctx)
	{
		var lines = File.ReadAllLines (ctx.assetPath);
		bool vibFreqMode = false;
		int currentModeIndex = 0;

		var molecule = ScriptableObject.CreateInstance<MoleculeDefinition> ();

		var elementData = AssetDatabase.FindAssets ("t:Element");


		foreach (var line in lines) {

			if (line == "VIBFREQ") {
				vibFreqMode = true;
			}

			if (vibFreqMode) {
				var matchFreq = Regex.Match (line, "^ +(\\d+.\\d+) +[\\w\\?\\+\\-\\'\\\"]+ *$");
				if (matchFreq.Success) {
					var mode = new VibrationalModeDefinition ();
					mode.Wavenumber = float.Parse (matchFreq.Groups [1].Value.Trim ());
					molecule.VibrationalModes.Add (mode);
					Debug.Log ("Found Vibrational Frequency");
				} else if (molecule.VibrationalModes.Count > 0) {
					var matchNumbers = Regex.Matches (line, "(?: +-?\\d+.\\d+)");
					if (matchNumbers.Count > 0) {
						for (int i = 0; i < matchNumbers.Count / 3; i++) {
							molecule.VibrationalModes [currentModeIndex].Displacements.Add (new Vector3(
								float.Parse(matchNumbers[3*i].Groups[0].Value.Trim()),
								float.Parse(matchNumbers[3*i+1].Groups[0].Value.Trim()),
								float.Parse(matchNumbers[3*i+2].Groups[0].Value.Trim())
							));
							if (molecule.VibrationalModes [currentModeIndex].Displacements.Count == molecule.Atoms.Count)
								currentModeIndex++;
						}
					} else {
						vibFreqMode = false;
					}
				}
					
			} else {
				var match = Regex.Match (line, "^ (\\d)  ([- ]\\d+.\\d+)  ([- ]\\d+.\\d+)  ([- ]\\d+.\\d+)$");
				if (!match.Success)
					continue;
				
				// Found an atom
				var atomicNumber = int.Parse (match.Groups [1].Value.Trim ());



				var x = float.Parse (match.Groups [2].Value.Trim ());
				var y = float.Parse (match.Groups [3].Value.Trim ());
				var z = float.Parse (match.Groups [4].Value.Trim ());
				molecule.Atoms.Add(new AtomDefinition() {
					Position = new Vector3(x,y,z),
					Element = elementData.Select(e => AssetDatabase.LoadAssetAtPath<Element>(AssetDatabase.GUIDToAssetPath(e))).Where(e => e != null && e.AtomicNumber == atomicNumber).FirstOrDefault()
				});

			}
		}

		ctx.AddObjectToAsset ("Data", molecule);
		ctx.SetMainObject (molecule);
	}
}
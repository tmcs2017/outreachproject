using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FrequencyRange))]
public class FrequencyRangeDrawer : PropertyDrawer
{
	// Draw the property inside the given rect
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty(position, label, property);

		// Draw label
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		var viewRect = new Rect (position);

		EditorGUI.DrawRect (viewRect, new Color(0,0,0,0.1f));

		var propertyArray = property.FindPropertyRelative ("FrequencyData");
		var arraySize = propertyArray.arraySize;
		if (arraySize > 0) {
			int rectWidth = (int) viewRect.width;
			for (float x = 0; x < rectWidth; x++) {
				int spectrumIndex = (int) ((x / rectWidth) * arraySize);
				float height = 100*viewRect.height * propertyArray.GetArrayElementAtIndex (spectrumIndex).floatValue;
				if (height > viewRect.height - 1)
					height = viewRect.height - 1;
				var spectrumRect = new Rect (viewRect.xMin + x, viewRect.yMax - 1 - height, 1, height+1);
				EditorGUI.DrawRect (spectrumRect, new Color(1,0,0,0.5f));
			}
		}
		/*
		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		// Calculate rects
		var amountRect = new Rect(position.x, position.y, 30, position.height);
		var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
		var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

		// Draw fields - passs GUIContent.none to each so they are drawn without labels
		EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
		EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
		EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

		// Set indent back to what it was
		EditorGUI.indentLevel = indent;*/

		EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		return base.GetPropertyHeight (property, label) + 48f;;
	}
}
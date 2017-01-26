using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets._Project.Scripts.Editor;
using UnityEditor;

[CustomEditor(typeof(CityGenerator))]
public class CityGeneratorEditor : CustomEditorBase<CityGenerator>
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if(GUILayout.Button("Generate"))
		{
			Target.Generate();
		}
	}
}

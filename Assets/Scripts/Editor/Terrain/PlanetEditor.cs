using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		Planet planet = (Planet)target;

		if (GUILayout.Button("Create Planet"))
		{
			if (planet != null)
			{
				planet.DestroyPlanet();
			}

			planet.CreatePlanet();
		}

		if (GUILayout.Button("Destroy Planet"))
		{
			planet.DestroyPlanet();
		}
	}
}
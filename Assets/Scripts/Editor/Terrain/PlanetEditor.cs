using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Planet planet = (Planet)target;

		if (DrawDefaultInspector())
		{
			if (planet != null)
			{
				planet.DestroyPlanet();
			}

			planet.CreatePlanet();
		}

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
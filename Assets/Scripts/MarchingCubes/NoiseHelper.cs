using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseHelper
{
	public static float SdfProceduralPlanet(Vector3 position, float radius, float frequency, float amplitude, int octaves)
	{
		float result = 0f;

		FastNoiseLite fastNoiseLite = new FastNoiseLite();
		fastNoiseLite.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);

		for (int i = 0; i < octaves; i++)
		{
			result += fastNoiseLite.GetNoise(position.x * frequency, position.y * frequency, position.z * frequency) * amplitude;
			frequency *= 2f;
			amplitude *= .5f;
		}

		result += position.magnitude - radius;

		return result;
	}
}

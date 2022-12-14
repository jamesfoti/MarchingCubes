using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseHelper
{
	public static float SdfSphere(Vector3 position, float radius)
	{
		float result = position.magnitude - radius;
		return result;
	}

	public static float PerlinNoise3D(float x, float y, float z)
	{
		float result = -1;

		float ab = Mathf.PerlinNoise(x, y);
		float bc = Mathf.PerlinNoise(y, z);
		float ac = Mathf.PerlinNoise(x, z);

		float ba = Mathf.PerlinNoise(y, x);
		float cb = Mathf.PerlinNoise(z, y);
		float ca = Mathf.PerlinNoise(z, x);

		result = (ab + bc + ac + ba + cb + ca) / 6f;

		return result;
	}
}

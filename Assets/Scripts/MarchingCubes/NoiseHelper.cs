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
}

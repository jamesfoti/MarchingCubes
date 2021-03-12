using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData3D {
	private static Vector3 v1 = new Vector3(0, 0, 0);
	private static Vector3 v2 = new Vector3(1, 0, 0);
	private static Vector3 v3 = new Vector3(1, 1, 0);
	private static Vector3 v4 = new Vector3(0, 1, 0);
	private static Vector3 v5 = new Vector3(0, 0, 1);
	private static Vector3 v6 = new Vector3(1, 0, 1);
	private static Vector3 v7 = new Vector3(1, 1, 1);
	private static Vector3 v8 = new Vector3(0, 1, 1);
	public static Vector3[] corners = { v1, v2, v3, v4, v5, v6, v7, v8 };
}
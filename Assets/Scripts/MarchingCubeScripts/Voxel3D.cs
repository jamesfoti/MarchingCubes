using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel3D {
	public Vector3 position; // This will be lower left corner -> corners[0].
	public Vector3[] corners = new Vector3[8];
	public float[] densities = new float[8];

	public Voxel3D(Vector3[] corners, float[] densities) {
		this.corners = corners;
		this.densities = densities;
		this.position = this.corners[0];
	}
}
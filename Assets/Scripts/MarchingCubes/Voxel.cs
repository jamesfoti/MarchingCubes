using System;
using UnityEngine;

public class Voxel
{
	public float[] CornerDensities { get; set; } = new float[8];
	public Tuple<Vector3, Vector3>[] Edges { get; set; } = new Tuple<Vector3, Vector3>[12];

	private readonly Vector3[] _cornerVertices = new Vector3[8];

	public Voxel(Vector3 bottomLeftPosition, float size)
	{
		_cornerVertices[0] = bottomLeftPosition;
		_cornerVertices[1] = new Vector3(_cornerVertices[0].x + size, _cornerVertices[0].y, _cornerVertices[0].z);
		_cornerVertices[2] = new Vector3(_cornerVertices[0].x + size, _cornerVertices[0].y + size, _cornerVertices[0].z);
		_cornerVertices[3] = new Vector3(_cornerVertices[0].x, _cornerVertices[0].y + size, _cornerVertices[0].z);

		_cornerVertices[4] = new Vector3(_cornerVertices[0].x, _cornerVertices[0].y, _cornerVertices[0].z + size);
		_cornerVertices[5] = new Vector3(_cornerVertices[0].x + size, _cornerVertices[0].y, _cornerVertices[0].z + size);
		_cornerVertices[6] = new Vector3(_cornerVertices[0].x + size, _cornerVertices[0].y + size, _cornerVertices[0].z + size);
		_cornerVertices[7] = new Vector3(_cornerVertices[0].x, _cornerVertices[0].y + size, _cornerVertices[0].z + size);

		Edges[0] = Tuple.Create(_cornerVertices[0], _cornerVertices[1]);
		Edges[1] = Tuple.Create(_cornerVertices[1], _cornerVertices[2]);
		Edges[2] = Tuple.Create(_cornerVertices[2], _cornerVertices[3]);
		Edges[3] = Tuple.Create(_cornerVertices[3], _cornerVertices[0]);

		Edges[4] = Tuple.Create(_cornerVertices[4], _cornerVertices[5]);
		Edges[5] = Tuple.Create(_cornerVertices[5], _cornerVertices[6]);
		Edges[6] = Tuple.Create(_cornerVertices[6], _cornerVertices[7]);
		Edges[7] = Tuple.Create(_cornerVertices[7], _cornerVertices[4]);

		Edges[8] = Tuple.Create(_cornerVertices[4], _cornerVertices[0]);
		Edges[9] = Tuple.Create(_cornerVertices[5], _cornerVertices[1]);
		Edges[10] = Tuple.Create(_cornerVertices[6], _cornerVertices[2]);
		Edges[11] = Tuple.Create(_cornerVertices[7], _cornerVertices[3]);
	}
}


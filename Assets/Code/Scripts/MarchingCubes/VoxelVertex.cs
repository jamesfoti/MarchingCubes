using UnityEngine;

public struct VoxelVertex
{
	public Vector3 Position { get; set; }
	public float Density { get; set; }

	public VoxelVertex(Vector3 position, float density = 0f)
	{
		Position = position;
		Density = density;
	}
}

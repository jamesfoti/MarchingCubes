using System;
using UnityEngine;

public class Voxel
{
	public VoxelVertex[] VoxelVertices { get; set; } = new VoxelVertex[8];

	public Voxel(Vector3 bottomLeftPosition, float size)
	{
		VoxelVertices[0] = new VoxelVertex(bottomLeftPosition, 0f);
		VoxelVertices[1] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y, bottomLeftPosition.z));
		VoxelVertices[2] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y + size, bottomLeftPosition.z));
		VoxelVertices[3] = new VoxelVertex(new Vector3(bottomLeftPosition.x, bottomLeftPosition.y + size, bottomLeftPosition.z));
		VoxelVertices[4] = new VoxelVertex(new Vector3(bottomLeftPosition.x, bottomLeftPosition.y, bottomLeftPosition.z + size));
		VoxelVertices[5] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y, bottomLeftPosition.z + size));
		VoxelVertices[6] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y + size, bottomLeftPosition.z + size));
		VoxelVertices[7] = new VoxelVertex(new Vector3(bottomLeftPosition.x, bottomLeftPosition.y + size, bottomLeftPosition.z + size));
	}
}

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

public struct VoxelEdge
{
	public VoxelVertex Start { get; set; }
	public VoxelVertex End { get; set; }

	public VoxelEdge(VoxelVertex start, VoxelVertex end)
	{
		Start = start;
		End = end;
	}
}


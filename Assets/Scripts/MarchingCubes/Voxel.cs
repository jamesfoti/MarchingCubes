using System;
using UnityEngine;

public class Voxel
{
	public VoxelVertex[] VoxelVertex { get; set; } = new VoxelVertex[8];
	public VoxelEdge[] VoxelEdges { get; set; } = new VoxelEdge[12];

	public Voxel(Vector3 bottomLeftPosition, float size)
	{
		VoxelVertex[0] = new VoxelVertex(bottomLeftPosition, 0f);
		VoxelVertex[1] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y, bottomLeftPosition.z));
		VoxelVertex[2] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y + size, bottomLeftPosition.z));
		VoxelVertex[3] = new VoxelVertex(new Vector3(bottomLeftPosition.x, bottomLeftPosition.y + size, bottomLeftPosition.z));
		VoxelVertex[4] = new VoxelVertex(new Vector3(bottomLeftPosition.x, bottomLeftPosition.y, bottomLeftPosition.z + size));
		VoxelVertex[5] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y, bottomLeftPosition.z + size));
		VoxelVertex[6] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y + size, bottomLeftPosition.z + size));
		VoxelVertex[7] = new VoxelVertex(new Vector3(bottomLeftPosition.x, bottomLeftPosition.y + size, bottomLeftPosition.z + size));

		VoxelEdges[0] = new VoxelEdge(VoxelVertex[0], VoxelVertex[1]);
		VoxelEdges[1] = new VoxelEdge(VoxelVertex[1], VoxelVertex[2]);
		VoxelEdges[2] = new VoxelEdge(VoxelVertex[2], VoxelVertex[3]);
		VoxelEdges[3] = new VoxelEdge(VoxelVertex[3], VoxelVertex[0]);
		VoxelEdges[4] = new VoxelEdge(VoxelVertex[4], VoxelVertex[5]);
		VoxelEdges[5] = new VoxelEdge(VoxelVertex[5], VoxelVertex[6]);
		VoxelEdges[6] = new VoxelEdge(VoxelVertex[6], VoxelVertex[7]);
		VoxelEdges[7] = new VoxelEdge(VoxelVertex[7], VoxelVertex[4]);
		VoxelEdges[8] = new VoxelEdge(VoxelVertex[4], VoxelVertex[0]);
		VoxelEdges[9] = new VoxelEdge(VoxelVertex[5], VoxelVertex[1]);
		VoxelEdges[10] = new VoxelEdge(VoxelVertex[6], VoxelVertex[2]);
		VoxelEdges[11] = new VoxelEdge(VoxelVertex[7], VoxelVertex[3]);
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


using System;
using UnityEngine;

public class Voxel
{
	public VoxelVertex[] Vertices { get; set; } = new VoxelVertex[8];
	public VoxelEdge[] Edges { get; set; } = new VoxelEdge[12];

	public Voxel(Vector3 bottomLeftPosition, float size)
	{
		Vertices[0] = new VoxelVertex(bottomLeftPosition, 0f);
		Vertices[1] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y, bottomLeftPosition.z));
		Vertices[2] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y + size, bottomLeftPosition.z));
		Vertices[3] = new VoxelVertex(new Vector3(bottomLeftPosition.x, bottomLeftPosition.y + size, bottomLeftPosition.z));
		Vertices[4] = new VoxelVertex(new Vector3(bottomLeftPosition.x, bottomLeftPosition.y, bottomLeftPosition.z + size));
		Vertices[5] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y, bottomLeftPosition.z + size));
		Vertices[6] = new VoxelVertex(new Vector3(bottomLeftPosition.x + size, bottomLeftPosition.y + size, bottomLeftPosition.z + size));
		Vertices[7] = new VoxelVertex(new Vector3(bottomLeftPosition.x, bottomLeftPosition.y + size, bottomLeftPosition.z + size));

		Edges[0] = new VoxelEdge(Vertices[0], Vertices[1]);
		Edges[1] = new VoxelEdge(Vertices[1], Vertices[2]);
		Edges[2] = new VoxelEdge(Vertices[2], Vertices[3]);
		Edges[3] = new VoxelEdge(Vertices[3], Vertices[0]);
		Edges[4] = new VoxelEdge(Vertices[4], Vertices[5]);
		Edges[5] = new VoxelEdge(Vertices[5], Vertices[6]);
		Edges[6] = new VoxelEdge(Vertices[6], Vertices[7]);
		Edges[7] = new VoxelEdge(Vertices[7], Vertices[4]);
		Edges[8] = new VoxelEdge(Vertices[4], Vertices[0]);
		Edges[9] = new VoxelEdge(Vertices[5], Vertices[1]);
		Edges[10] = new VoxelEdge(Vertices[6], Vertices[2]);
		Edges[11] = new VoxelEdge(Vertices[7], Vertices[3]);
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


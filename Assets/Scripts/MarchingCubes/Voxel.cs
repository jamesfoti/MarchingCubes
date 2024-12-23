using UnityEngine;

public struct Voxel
{
	public VoxelVertex[] VoxelVertices { get; set; }

	public Voxel(Vector3 bottomLeftPosition, float size)
	{
		VoxelVertices = new VoxelVertex[8];
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






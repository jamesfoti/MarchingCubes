using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	public Vector3 bottomLeftPosition { get; private set; } = new Vector3();
	public Vector3 centerPosition { get; private set; } = new Vector3();

	private List<Voxel> voxels = new List<Voxel>();

	public void Initialize(Vector3 bottomLeftPosition, PlanetTerrainData terrainData)
	{
		float halfChunkSize = terrainData.chunkSize * .5f;
		float voxelSize = 1f / terrainData.chunkResolution;
		int numberOfCellsInWidth = terrainData.chunkSize * terrainData.chunkResolution;
		int numberOfCellsInHeight = terrainData.chunkSize * terrainData.chunkResolution;
		int numberOfCellsInDepth = terrainData.chunkSize * terrainData.chunkResolution;
		
		for (int y = 0; y < numberOfCellsInHeight; y++)
		{
			for (int x = 0; x < numberOfCellsInWidth; x++)
			{
				for (int z = 0; z < numberOfCellsInDepth; z++)
				{
					Vector3 bottomLeftCellPosition = new Vector3(x, y, z) * voxelSize + bottomLeftPosition;
					Voxel voxel = new Voxel(bottomLeftCellPosition, voxelSize);
					voxels.Add(voxel);
				}
			}
		}
	}

	public void CreateMesh()
	{
		SetDensities();
		Mesh mesh = MarchingCubesHelper.CreateMeshFromMarchingTheCubes(voxels, .5f);
		GetComponent<MeshFilter>().mesh = mesh;
	}

	public void SetDensities()
	{
		foreach (Voxel voxel in voxels)
		{
			voxel.VoxelVertex[0].Density = Random.Range(.0f, 1f); 
			voxel.VoxelVertex[1].Density = Random.Range(.0f, 1f);
			voxel.VoxelVertex[2].Density = Random.Range(.0f, 1f);
			voxel.VoxelVertex[3].Density = Random.Range(.0f, 1f);
			voxel.VoxelVertex[4].Density = Random.Range(.0f, 1f);
			voxel.VoxelVertex[5].Density = Random.Range(.0f, 1f);
			voxel.VoxelVertex[6].Density = Random.Range(.0f, 1f);
			voxel.VoxelVertex[7].Density = Random.Range(.0f, 1f);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(centerPosition, .1f);
		Handles.Label(centerPosition, "Chunk Origin \n Position");

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(bottomLeftPosition, .1f);
		Handles.Label(bottomLeftPosition, "Chunk Bottom \n Left Position");

		foreach (Voxel voxel in voxels)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawSphere(voxel.VoxelVertex[0].Position, .05f);
			Gizmos.DrawSphere(voxel.VoxelVertex[1].Position, .05f);
			Gizmos.DrawSphere(voxel.VoxelVertex[2].Position, .05f);
			Gizmos.DrawSphere(voxel.VoxelVertex[3].Position, .05f);
			Gizmos.DrawSphere(voxel.VoxelVertex[4].Position, .05f);
			Gizmos.DrawSphere(voxel.VoxelVertex[5].Position, .05f);
			Gizmos.DrawSphere(voxel.VoxelVertex[6].Position, .05f);
			Gizmos.DrawSphere(voxel.VoxelVertex[7].Position, .05f);

			/* Gizmos.color = Color.Lerp(Color.white, Color.black, voxel.d1);
			Gizmos.DrawWireSphere(voxel.v1, .2f);
			Handles.Label(voxel.v1, voxel.d1.ToString());

			Gizmos.color = Color.Lerp(Color.white, Color.black, voxel.d2);
			Gizmos.DrawWireSphere(voxel.v2, .2f);
			Handles.Label(voxel.v2, voxel.d2.ToString());

			Gizmos.color = Color.Lerp(Color.white, Color.black, voxel.d3);
			Gizmos.DrawWireSphere(voxel.v3, .2f);
			Handles.Label(voxel.v3, voxel.d3.ToString());

			Gizmos.color = Color.Lerp(Color.white, Color.black, voxel.d4);
			Gizmos.DrawWireSphere(voxel.v4, .2f);
			Handles.Label(voxel.v4, voxel.d4.ToString()); */
		}
	}
}

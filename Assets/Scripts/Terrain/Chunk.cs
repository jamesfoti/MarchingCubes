using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	public Vector3 BottomLeftPosition { get; private set; } = new Vector3();
	public Vector3 CenterPosition { get; private set; } = new Vector3();

	private readonly List<Voxel> _voxels = new List<Voxel>();

	private Planet _parentPlanet;

	public void Initialize(Vector3 bottomLeftPosition, Planet parentPlanet)
	{
		_parentPlanet = parentPlanet;

		float voxelSize = 1f / _parentPlanet.TerrainData.chunkResolution;
		int numberOfCellsInWidth = _parentPlanet.TerrainData.chunkSize * _parentPlanet.TerrainData.chunkResolution;
		int numberOfCellsInHeight = _parentPlanet.TerrainData.chunkSize * _parentPlanet.TerrainData.chunkResolution;
		int numberOfCellsInDepth = _parentPlanet.TerrainData.chunkSize * _parentPlanet.TerrainData.chunkResolution;
		
		for (int y = 0; y < numberOfCellsInHeight; y++)
		{
			for (int x = 0; x < numberOfCellsInWidth; x++)
			{
				for (int z = 0; z < numberOfCellsInDepth; z++)
				{
					Vector3 bottomLeftCellPosition = new Vector3(x, y, z) * voxelSize + bottomLeftPosition;
					Voxel voxel = new Voxel(bottomLeftCellPosition, voxelSize);
					_voxels.Add(voxel);
				}
			}
		}
	}

	public void CreateMesh()
	{
		SetDensities();
		Mesh mesh = MarchingCubesHelper.CreateMeshFromMarchingTheCubes(_voxels, _parentPlanet.TerrainData.isoLevel);
		GetComponent<MeshFilter>().mesh = mesh;
	}

	public void SetDensities()
	{
		foreach (Voxel voxel in _voxels)
		{
			voxel.VoxelVertex[0].Density = NoiseHelper.SdfSphere(voxel.VoxelVertex[0].Position, _parentPlanet.TerrainData.radiusInChunks * _parentPlanet.TerrainData.chunkSize); 
			voxel.VoxelVertex[1].Density = NoiseHelper.SdfSphere(voxel.VoxelVertex[1].Position, _parentPlanet.TerrainData.radiusInChunks * _parentPlanet.TerrainData.chunkSize);
			voxel.VoxelVertex[2].Density = NoiseHelper.SdfSphere(voxel.VoxelVertex[2].Position, _parentPlanet.TerrainData.radiusInChunks * _parentPlanet.TerrainData.chunkSize);
			voxel.VoxelVertex[3].Density = NoiseHelper.SdfSphere(voxel.VoxelVertex[3].Position, _parentPlanet.TerrainData.radiusInChunks * _parentPlanet.TerrainData.chunkSize);
			voxel.VoxelVertex[4].Density = NoiseHelper.SdfSphere(voxel.VoxelVertex[4].Position, _parentPlanet.TerrainData.radiusInChunks * _parentPlanet.TerrainData.chunkSize);
			voxel.VoxelVertex[5].Density = NoiseHelper.SdfSphere(voxel.VoxelVertex[5].Position, _parentPlanet.TerrainData.radiusInChunks * _parentPlanet.TerrainData.chunkSize);
			voxel.VoxelVertex[6].Density = NoiseHelper.SdfSphere(voxel.VoxelVertex[6].Position, _parentPlanet.TerrainData.radiusInChunks * _parentPlanet.TerrainData.chunkSize);
			voxel.VoxelVertex[7].Density = NoiseHelper.SdfSphere(voxel.VoxelVertex[7].Position, _parentPlanet.TerrainData.radiusInChunks * _parentPlanet.TerrainData.chunkSize);
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(CenterPosition, .1f);
		Handles.Label(CenterPosition, "Chunk Origin \n Position");

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(BottomLeftPosition, .1f);
		Handles.Label(BottomLeftPosition, "Chunk Bottom \n Left Position");

		foreach (Voxel voxel in _voxels)
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

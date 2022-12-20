using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	public Vector3 BottomLeftPosition { get; private set; } = new Vector3();

	private readonly List<Voxel> _voxels = new List<Voxel>();
	private Planet _parentPlanet;

	public void Initialize(Vector3 bottomLeftPosition, Planet parentPlanet)
	{
		_parentPlanet = parentPlanet;

		float voxelSize = 1f / _parentPlanet.TerrainData.ChunkResolution;
		int numberOfCellsInWidth = _parentPlanet.TerrainData.ChunkSize * _parentPlanet.TerrainData.ChunkResolution;
		int numberOfCellsInHeight = _parentPlanet.TerrainData.ChunkSize * _parentPlanet.TerrainData.ChunkResolution;
		int numberOfCellsInDepth = _parentPlanet.TerrainData.ChunkSize * _parentPlanet.TerrainData.ChunkResolution;
		
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
		SetVoxelDensities();
		Mesh mesh = MarchingCubesHelper.CreateMeshFromMarchingTheCubes(_voxels, _parentPlanet.TerrainData.IsoLevel);
		GetComponent<MeshFilter>().mesh = mesh;
	}

	public void ClearMesh()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		GetComponent<MeshFilter>().mesh = mesh;
	}

	public void ClearSharedMesh()
	{
		Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
		mesh.Clear();
		GetComponent<MeshFilter>().sharedMesh = mesh;
	}

	private void SetVoxelDensities()
	{
		foreach (Voxel voxel in _voxels)
		{
			for (int i = 0; i < voxel.Vertices.Length; i++)
			{
				Vector3 position = voxel.Vertices[i].Position;

				float radius = _parentPlanet.TerrainData.RadiusInChunks * _parentPlanet.TerrainData.ChunkSize;

				float density = NoiseHelper.SdfProceduralPlanet(position, radius, _parentPlanet.TerrainData.Frequency, _parentPlanet.TerrainData.Amplitude, _parentPlanet.TerrainData.Octaves);

				voxel.Vertices[i].Density = density;
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		foreach (Voxel voxel in _voxels)
		{
			for (int i = 0; i < voxel.Vertices.Length; i++)
			{
				Gizmos.color = Color.green;
				//Gizmos.DrawSphere(voxel.Vertices[i].Position, .05f);

				Gizmos.color = Color.Lerp(Color.white, Color.black, voxel.Vertices[i].Density);
				Gizmos.DrawWireSphere(voxel.Vertices[i].Position, .2f);
				Handles.Label(voxel.Vertices[i].Position, voxel.Vertices[i].Density.ToString());
			}
		}
	}
}

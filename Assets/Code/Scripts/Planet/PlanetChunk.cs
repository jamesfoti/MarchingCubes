using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlanetChunk : MonoBehaviour
{
	private readonly List<Voxel> _voxels = new List<Voxel>();
	private Planet _parentPlanet;

	public void Initialize(Vector3 bottomLeftPosition, Planet parentPlanet)
	{
		_parentPlanet = parentPlanet;

		float voxelSize = 1f / _parentPlanet.PlanetShapeSettings.ChunkResolution;
		int numberOfCellsInWidth = _parentPlanet.PlanetShapeSettings.ChunkSize * _parentPlanet.PlanetShapeSettings.ChunkResolution;
		int numberOfCellsInHeight = _parentPlanet.PlanetShapeSettings.ChunkSize * _parentPlanet.PlanetShapeSettings.ChunkResolution;
		int numberOfCellsInDepth = _parentPlanet.PlanetShapeSettings.ChunkSize * _parentPlanet.PlanetShapeSettings.ChunkResolution;
		
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
		Mesh mesh = MarchingCubes.CreateMeshFromMarchingTheCubes(_voxels, _parentPlanet.PlanetShapeSettings.IsoLevel, _parentPlanet.PlanetShapeSettings.InterpolationType);
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
			for (int i = 0; i < voxel.VoxelVertices.Length; i++)
			{
				Vector3 position = voxel.VoxelVertices[i].Position;
				
				float xSample = position.x + _parentPlanet.NoiseSettings.Offset.x;
				float ySample = position.y + _parentPlanet.NoiseSettings.Offset.y;
				float zSample = position.z + _parentPlanet.NoiseSettings.Offset.z;

				float noise = _parentPlanet.FastNoiseLite.GetNoise(xSample, ySample, zSample);

				voxel.VoxelVertices[i].Density = position.magnitude - _parentPlanet.PlanetShapeSettings.RadiusInRealWorld + (_parentPlanet.NoiseSettings.NoiseScale * noise);
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		foreach (Voxel voxel in _voxels)
		{
			for (int i = 0; i < voxel.VoxelVertices.Length; i++)
			{
				Gizmos.color = Color.Lerp(Color.white, Color.black, voxel.VoxelVertices[i].Density);
				Gizmos.DrawWireSphere(voxel.VoxelVertices[i].Position, .2f);
				Handles.Label(voxel.VoxelVertices[i].Position, voxel.VoxelVertices[i].Density.ToString());
			}
		}
	}
}

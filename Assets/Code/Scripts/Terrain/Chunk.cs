using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	private readonly List<Voxel> _voxels = new List<Voxel>();
	private Planet _parentPlanet;

	public void Initialize(Vector3 bottomLeftPosition, Planet parentPlanet)
	{
		_parentPlanet = parentPlanet;

		float voxelSize = 1f / _parentPlanet.ChunkResolution;
		int numberOfCellsInWidth = _parentPlanet.ChunkSize * _parentPlanet.ChunkResolution;
		int numberOfCellsInHeight = _parentPlanet.ChunkSize * _parentPlanet.ChunkResolution;
		int numberOfCellsInDepth = _parentPlanet.ChunkSize * _parentPlanet.ChunkResolution;
		
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
		Mesh mesh = MarchingCubesHelper.CreateMeshFromMarchingTheCubes(_voxels, _parentPlanet.IsoLevel, _parentPlanet.InterpolationType);
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
		FastNoiseLite fastNoiseLite = new FastNoiseLite();
		fastNoiseLite.SetNoiseType(_parentPlanet.NoiseData.NoiseType);
		fastNoiseLite.SetFractalType(_parentPlanet.NoiseData.FractalType);
		fastNoiseLite.SetSeed(_parentPlanet.NoiseData.Seed);
		fastNoiseLite.SetFrequency(_parentPlanet.NoiseData.Frequency);
		fastNoiseLite.SetFractalOctaves(_parentPlanet.NoiseData.Octaves);
		fastNoiseLite.SetFractalLacunarity(_parentPlanet.NoiseData.Lacunarity);
		fastNoiseLite.SetFractalGain(_parentPlanet.NoiseData.Gain);
		fastNoiseLite.SetFractalWeightedStrength(_parentPlanet.NoiseData.WeightedStrength);

		Vector3 offset = _parentPlanet.NoiseData.Offset;
		float noiseScale = _parentPlanet.NoiseData.NoiseScale;
		float radius = _parentPlanet.RadiusInRealWorld;

		foreach (Voxel voxel in _voxels)
		{
			for (int i = 0; i < voxel.VoxelVertices.Length; i++)
			{
				Vector3 position = voxel.VoxelVertices[i].Position;
				
				float xSample = position.x + offset.x;
				float ySample = position.y + offset.y;
				float zSample = position.z + offset.z;

				float noise = fastNoiseLite.GetNoise(xSample, ySample, zSample);

				voxel.VoxelVertices[i].Density = position.magnitude - radius + (noiseScale * noise);
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

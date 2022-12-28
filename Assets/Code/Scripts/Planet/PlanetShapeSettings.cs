using System;
using UnityEngine;

[Serializable]
public class PlanetShapeSettings
{
	[SerializeField] private PlanetChunk _planetChunkPrefab;
	[Range(-100f, 100f)] [SerializeField] private float _isoLevel = 0f;
	[SerializeField] private MarchingCubes.InterpolationType _interpolationType = MarchingCubes.InterpolationType.None;
	[Range(1, 100)] [SerializeField] private int _chunkResolution = 1;
	[Range(1, 100)] [SerializeField] private int _chunkSize = 5;
	[Range(1, 100)] [SerializeField] private int _radiusInChunks = 1;

	public PlanetChunk PlanetChunkPrefab
	{
		get => _planetChunkPrefab;
	}

	public float IsoLevel
	{
		get => _isoLevel;
	}

	public MarchingCubes.InterpolationType InterpolationType
	{
		get => _interpolationType;
	}

	public int ChunkResolution
	{
		get => _chunkResolution;
	}

	public int ChunkSize
	{
		get => _chunkSize;
	}

	public int RadiusInChunks
	{
		get => _radiusInChunks;
	}

	public int RadiusInRealWorld
	{
		get => _radiusInChunks * _chunkSize;
	}
}

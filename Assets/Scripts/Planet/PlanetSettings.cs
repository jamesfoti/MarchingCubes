using System;
using UnityEngine;

[Serializable]
public class PlanetSettings
{
	[SerializeField] private PlanetChunk _planetChunkPrefab;
	[Range(-100f, 100f)] [SerializeField] private float _isoLevel = 0f;
	[SerializeField] private MarchingCubes.InterpolationType _interpolationType = MarchingCubes.InterpolationType.None;
	[SerializeField] private bool _isFlatShaded = true;
	[Range(1, 100)] [SerializeField] private int _chunkResolution = 1;
	[Range(1, 100)] [SerializeField] private int _chunkSize = 5;
	[Range(1, 100)] [SerializeField] private int _radiusInChunks = 1;
	[SerializeField] private FastNoiseLite.NoiseType _noiseType = FastNoiseLite.NoiseType.OpenSimplex2;
	[SerializeField] private FastNoiseLite.FractalType _fractalType = FastNoiseLite.FractalType.None;
	[Range(-100, 100)][SerializeField] private int _seed = 0;
	[Range(0f, 1f)][SerializeField] private float _frequency = .02f;
	[Range(1f, 10f)][SerializeField] private float _lacunarity = 2f;
	[Range(1, 10)][SerializeField] private int _octaves = 3;
	[Range(-10f, 10f)][SerializeField] private float _fractalGain = .5f;
	[Range(0f, 1f)][SerializeField] private float _fractalWeightedStrength = 0f;
	[Range(1f, 100f)][SerializeField] private float _noiseScale = 1f;
	[SerializeField] private Vector3 _offset = Vector3.zero;

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

	public bool IsFlatShaded
	{
		get => _isFlatShaded;
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

	public FastNoiseLite.NoiseType NoiseType
	{
		get => _noiseType;
	}

	public FastNoiseLite.FractalType FractalType
	{
		get => _fractalType;
	}

	public int Seed
	{
		get => _seed;
	}

	public float Frequency
	{
		get => _frequency;
	}

	public float Lacunarity
	{
		get => _lacunarity;
	}

	public int Octaves
	{
		get => _octaves;
	}

	public float FractalGain
	{
		get => _fractalGain;
	}

	public float FractalWeightedStrength
	{
		get => _fractalWeightedStrength;
	}

	public float NoiseScale
	{
		get => _noiseScale;
	}

	public Vector3 Offset
	{
		get => _offset;
	}
}

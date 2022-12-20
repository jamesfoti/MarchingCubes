using System;
using UnityEngine;

[Serializable]
public struct PlanetTerrainData
{
	[SerializeField] private float _isoLevel;
	[SerializeField] private float _frequency;
	[SerializeField] private float _amplitude;
	[SerializeField] private int _octaves;
	[SerializeField] private int _chunkResolution;
	[SerializeField] private int _chunkSize;
	[SerializeField] private int _radiusInChunks;
	[SerializeField] private Vector3 _center;

	public float IsoLevel
	{
		get => _isoLevel;
	}

	public float Frequency
	{
		get => _frequency;
	}

	public float Amplitude
	{
		get => _amplitude;
	}

	public int Octaves
	{
		get => _octaves;
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

	public Vector3 Center
	{
		get => _center;
	}
}
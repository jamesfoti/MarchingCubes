using System;
using UnityEngine;

[Serializable]
public class NoiseSettings 
{
	[SerializeField] private FastNoiseLite.NoiseType _noiseType = FastNoiseLite.NoiseType.OpenSimplex2;
	[SerializeField] private FastNoiseLite.FractalType _fractalType = FastNoiseLite.FractalType.None;
	[Range(-100, 100)] [SerializeField] private int _seed = 0;
	[Range(0f, 1f)] [SerializeField] private float _frequency = .02f;
	[Range(1f, 10f)] [SerializeField] private float _lacunarity = 2f;
	[Range(1, 10)] [SerializeField] private int _octaves = 3;
	[Range(-10f, 10f)] [SerializeField] private float _gain = .5f;
	[Range(0f, 1f)] [SerializeField] private float _weightedStrength = 0f;
	[Range(1f, 100f)] [SerializeField] private float _noiseScale = 1f;
	[SerializeField] private Vector3 _offset = Vector3.zero;

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

	public float Gain
	{
		get => _gain;
	}

	public float WeightedStrength
	{
		get => _weightedStrength;
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

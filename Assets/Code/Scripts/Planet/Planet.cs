using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
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
	[SerializeField] private PlanetChunk _planetChunkPrefab;
	[Range(-100f, 100f)] [SerializeField] private float _isoLevel = 0f;
	[SerializeField] private MarchingCubes.InterpolationType _interpolationType = MarchingCubes.InterpolationType.None;
	[Range(1, 100)] [SerializeField] private int _chunkResolution = 1;
	[Range(1, 100)] [SerializeField] private int _chunkSize = 5;
	[Range(1, 100)] [SerializeField] private int _radiusInChunks = 1;
	[SerializeField] private bool _isAutoUpdate = false;
	private readonly Dictionary<Vector3, PlanetChunk> _planetChunks = new Dictionary<Vector3, PlanetChunk>();

	public float NoiseScale
	{
		get => _noiseScale;
	}

	public Vector3 Offset
	{
		get => _offset;
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

	public int RadiusInRealWorld { get; private set; } = 0;

	public FastNoiseLite FastNoiseLite { get; private set; } = new FastNoiseLite();

	public bool IsAutoUpdate
	{
		get => _isAutoUpdate;
	}

	private void Start()
	{
		CreatePlanet();
	}

	public void CreatePlanet()
	{
		SetUpFastNoiseLite();

		RadiusInRealWorld = _radiusInChunks * _chunkSize;
		
		for (int y = -_radiusInChunks; y < _radiusInChunks; y++)
		{
			for (int x = -_radiusInChunks; x < _radiusInChunks; x++)
			{
				for (int z = -_radiusInChunks; z < _radiusInChunks; z++)
				{
					Vector3 bottomLeftPosition = new Vector3(x * _chunkSize, y * _chunkSize, z * _chunkSize);

					PlanetChunk chunk = Instantiate(_planetChunkPrefab);
					chunk.Initialize(bottomLeftPosition, this);
					chunk.CreateMesh();
					chunk.transform.parent = transform;
					chunk.gameObject.name = $"BottomLeft = {bottomLeftPosition}";
					_planetChunks.Add(bottomLeftPosition, chunk);
				}
			}
		}
	}

	public void DestroyPlanet()
	{
		foreach (KeyValuePair<Vector3, PlanetChunk> entry in _planetChunks)
		{
			PlanetChunk chunk = entry.Value;

			if (Application.isPlaying)
			{
				chunk.ClearMesh();
				Destroy(chunk.gameObject);
			}
			else
			{
				chunk.ClearSharedMesh();
				DestroyImmediate(chunk.gameObject);
			}
		}

		_planetChunks.Clear();
	}

	private void SetUpFastNoiseLite()
	{
		FastNoiseLite.SetNoiseType(_noiseType);
		FastNoiseLite.SetFractalType(_fractalType);
		FastNoiseLite.SetSeed(_seed);
		FastNoiseLite.SetFrequency(_frequency);
		FastNoiseLite.SetFractalOctaves(_octaves);
		FastNoiseLite.SetFractalLacunarity(_lacunarity);
		FastNoiseLite.SetFractalGain(_gain);
		FastNoiseLite.SetFractalWeightedStrength(_weightedStrength);
	}
}

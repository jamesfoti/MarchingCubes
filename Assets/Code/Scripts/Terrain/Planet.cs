using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	[SerializeField] private Chunk _chunkPrefab;
	[SerializeField] private NoiseData _noiseData;
	[Range(-100f, 100f)] [SerializeField] private float _isoLevel = 0f;
	[Range(1, 100)] [SerializeField] private int _chunkResolution = 1;
	[Range(1, 100)] [SerializeField] private int _chunkSize = 5;
	[Range(1, 100)] [SerializeField] private int _radiusInChunks = 1;
	[SerializeField] private bool _isAutoUpdate = false;

	private Dictionary<Vector3, Chunk> _chunks = new Dictionary<Vector3, Chunk>();

	public NoiseData NoiseData
	{
		get => _noiseData;
	}

	public float IsoLevel
	{
		get => _isoLevel;
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

	public int RadiusInRealWorld { get; private set; } = 0;

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
		RadiusInRealWorld = RadiusInChunks * ChunkSize;

		for (int y = -RadiusInChunks; y < RadiusInChunks; y++)
		{
			for (int x = -RadiusInChunks; x < RadiusInChunks; x++)
			{
				for (int z = -RadiusInChunks; z < RadiusInChunks; z++)
				{
					Vector3 bottomLeftPosition = new Vector3(x * ChunkSize, y * ChunkSize, z * ChunkSize);

					Chunk chunk = Instantiate(_chunkPrefab);
					chunk.Initialize(bottomLeftPosition, this);
					chunk.CreateMesh();
					chunk.transform.parent = transform;
					chunk.gameObject.name = $"BottomLeft = {bottomLeftPosition}";
					_chunks.Add(bottomLeftPosition, chunk);
				}
			}
		}
	}

	public void DestroyPlanet()
	{
		foreach (KeyValuePair<Vector3, Chunk> entry in _chunks)
		{
			Chunk chunk = entry.Value;

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

		_chunks.Clear();
	}
}

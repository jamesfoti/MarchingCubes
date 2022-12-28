using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	[SerializeField] private NoiseSettings _noiseSettings = new NoiseSettings();
	[SerializeField] private PlanetShapeSettings _planetShapeSettings = new PlanetShapeSettings();
	[SerializeField] private bool _isAutoUpdate = false;
	private readonly Dictionary<Vector3, PlanetChunk> _planetChunks = new Dictionary<Vector3, PlanetChunk>();

	public NoiseSettings NoiseSettings
	{
		get => _noiseSettings;
	}

	public PlanetShapeSettings PlanetShapeSettings
	{
		get => _planetShapeSettings;
	}

	public bool IsAutoUpdate
	{
		get => _isAutoUpdate;
	}

	public FastNoiseLite FastNoiseLite { get; private set; } = new FastNoiseLite();

	private void Start()
	{
		CreatePlanet();
	}

	public void CreatePlanet()
	{
		SetUpFastNoiseLite();
		
		for (int y = -_planetShapeSettings.RadiusInChunks; y < _planetShapeSettings.RadiusInChunks; y++)
		{
			for (int x = -_planetShapeSettings.RadiusInChunks; x < _planetShapeSettings.RadiusInChunks; x++)
			{
				for (int z = -_planetShapeSettings.RadiusInChunks; z < _planetShapeSettings.RadiusInChunks; z++)
				{
					Vector3 bottomLeftPosition = new Vector3(x * _planetShapeSettings.ChunkSize, y * _planetShapeSettings.ChunkSize, z * _planetShapeSettings.ChunkSize);

					PlanetChunk chunk = Instantiate(_planetShapeSettings.PlanetChunkPrefab);
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
		FastNoiseLite.SetNoiseType(_noiseSettings.NoiseType);
		FastNoiseLite.SetFractalType(_noiseSettings.FractalType);
		FastNoiseLite.SetSeed(_noiseSettings.Seed);
		FastNoiseLite.SetFrequency(_noiseSettings.Frequency);
		FastNoiseLite.SetFractalOctaves(_noiseSettings.Octaves);
		FastNoiseLite.SetFractalLacunarity(_noiseSettings.Lacunarity);
		FastNoiseLite.SetFractalGain(_noiseSettings.Gain);
		FastNoiseLite.SetFractalWeightedStrength(_noiseSettings.WeightedStrength);
	}
}

using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	[SerializeField] private PlanetSettings _planetSettings = new PlanetSettings();
	[SerializeField] private bool _isAutoUpdate = false;
	private readonly Dictionary<Vector3, PlanetChunk> _planetChunks = new Dictionary<Vector3, PlanetChunk>();

	public PlanetSettings PlanetSettings
	{
		get => _planetSettings;
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

		for (int y = -_planetSettings.RadiusInChunks; y < _planetSettings.RadiusInChunks; y++)
		{
			for (int x = -_planetSettings.RadiusInChunks; x < _planetSettings.RadiusInChunks; x++)
			{
				for (int z = -_planetSettings.RadiusInChunks; z < _planetSettings.RadiusInChunks; z++)
				{
					Vector3 bottomLeftPosition = new Vector3(x * _planetSettings.ChunkSize, y * _planetSettings.ChunkSize, z * _planetSettings.ChunkSize);

					PlanetChunk chunk = Instantiate(_planetSettings.PlanetChunkPrefab);
					chunk.Initialize(bottomLeftPosition, this);
					chunk.SetVoxelDensities();
					chunk.CreateMesh();
					chunk.transform.parent = transform;
					chunk.gameObject.name = $"BottomLeft = {bottomLeftPosition}";
					_planetChunks.Add(bottomLeftPosition, chunk);
				}
			}
		}

		float minDensity = 0f;
		float maxDensity = 0f;
		foreach (KeyValuePair<Vector3, PlanetChunk> entry in _planetChunks)
		{
			PlanetChunk planetChunk = entry.Value;

			foreach (Voxel voxel in planetChunk.Voxels)
			{
				foreach (VoxelVertex voxelVertex in voxel.VoxelVertices)
				{
					if (voxelVertex.Density < minDensity)
					{
						minDensity = voxelVertex.Density;
					}

					if (voxelVertex.Density > maxDensity)
					{
						maxDensity = voxelVertex.Density;
					}
				}
			}
		}

		Debug.Log("Smallest density value = " + minDensity);
		Debug.Log("Largest density value = " + maxDensity);
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
		FastNoiseLite.SetNoiseType(_planetSettings.NoiseType);
		FastNoiseLite.SetFractalType(_planetSettings.FractalType);
		FastNoiseLite.SetSeed(_planetSettings.Seed);
		FastNoiseLite.SetFrequency(_planetSettings.Frequency);
		FastNoiseLite.SetFractalOctaves(_planetSettings.Octaves);
		FastNoiseLite.SetFractalLacunarity(_planetSettings.Lacunarity);
		FastNoiseLite.SetFractalGain(_planetSettings.FractalGain);
		FastNoiseLite.SetFractalWeightedStrength(_planetSettings.FractalWeightedStrength);
	}
}

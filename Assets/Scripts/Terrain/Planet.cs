using System;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	[SerializeField] private PlanetTerrainData _terrainData = new PlanetTerrainData();

	[SerializeField] private Chunk _chunkPrefab;

	private Dictionary<Vector3, Chunk> _chunks = new Dictionary<Vector3, Chunk>();

	public PlanetTerrainData TerrainData
	{
		get => _terrainData;
	}

	private void Start()
	{
		CreatePlanet();
	}

	public void CreatePlanet()
	{
		for (int y = -TerrainData.RadiusInChunks; y < TerrainData.RadiusInChunks; y++)
		{
			for (int x = -TerrainData.RadiusInChunks; x < TerrainData.RadiusInChunks; x++)
			{
				for (int z = -TerrainData.RadiusInChunks; z < TerrainData.RadiusInChunks; z++)
				{
					Vector3 bottomLeftPosition = new Vector3(x * TerrainData.ChunkSize, y * TerrainData.ChunkSize, z * TerrainData.ChunkSize) + TerrainData.Center;

					Chunk chunk = Instantiate(_chunkPrefab);
					chunk.Initialize(bottomLeftPosition, this);
					chunk.CreateMesh();
					chunk.transform.parent = transform;
					chunk.gameObject.name = $"BottomLeft = {chunk.BottomLeftPosition}";
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

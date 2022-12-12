using System;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	public PlanetTerrainData TerrainData = new PlanetTerrainData();

	[SerializeField]
	private Chunk chunkPrefab;
	private Dictionary<Vector3, Chunk> _chunks = new Dictionary<Vector3, Chunk>();

	private void Start()
	{
		CreatePlanet();
	}

	public void CreatePlanet()
	{
		for (int y = -TerrainData.radiusInChunks; y < TerrainData.radiusInChunks; y++)
		{
			for (int x = -TerrainData.radiusInChunks; x < TerrainData.radiusInChunks; x++)
			{
				for (int z = -TerrainData.radiusInChunks; z < TerrainData.radiusInChunks; z++)
				{
					Vector3 bottomLeftPosition = new Vector3(x * TerrainData.chunkSize, y * TerrainData.chunkSize, z * TerrainData.chunkSize) + TerrainData.center;

					Chunk chunk = Instantiate(chunkPrefab);
					chunk.Initialize(bottomLeftPosition, this);
					chunk.CreateMesh();
					chunk.transform.parent = transform;
					chunk.gameObject.name = $"BottomLeft = {chunk.BottomLeftPosition},  Center = {chunk.CenterPosition}";
					_chunks.Add(bottomLeftPosition, chunk);
				}
			}
		}
	}
}

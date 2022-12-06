using System;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	public Dictionary<Vector3, Chunk> chunks { get; private set; } = new Dictionary<Vector3, Chunk>();

	public PlanetTerrainData planetTerrainData = new PlanetTerrainData();

	[SerializeField]
	private Chunk chunkPrefab;

	private void Start()
	{
		CreatePlanet();
	}

	public void CreatePlanet()
	{
		for (int y = -planetTerrainData.radiusInChunks; y < planetTerrainData.radiusInChunks; y++)
		{
			for (int x = -planetTerrainData.radiusInChunks; x < planetTerrainData.radiusInChunks; x++)
			{
				for (int z = -planetTerrainData.radiusInChunks; z < planetTerrainData.radiusInChunks; z++)
				{
					Vector3 bottomLeftPosition = new Vector3(x * planetTerrainData.chunkSize, y * planetTerrainData.chunkSize, z * planetTerrainData.chunkSize) + planetTerrainData.center;

					Chunk chunk = Instantiate(chunkPrefab);
					chunk.Initialize(bottomLeftPosition, planetTerrainData);
					chunk.CreateMesh();
					chunk.transform.parent = transform;
					chunk.gameObject.name = $"BottomLeft = {chunk.bottomLeftPosition},  Center = {chunk.centerPosition}";
					chunks.Add(bottomLeftPosition, chunk);
				}
			}
		}
	}
}

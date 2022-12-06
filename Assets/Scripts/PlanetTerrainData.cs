using System;
using UnityEngine;

[Serializable]
public struct PlanetTerrainData
{
	[Range(0f, 1f)]
	public float isoLevel;
	[Range(1, 100)]
	public int chunkResolution;
	[Range(1, 100)]
	public int chunkSize;
	[Range(1, 100)]
	public int radiusInChunks;
	public Vector3 center;
}
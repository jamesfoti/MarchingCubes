using System;
using UnityEngine;

public class VoxelVertex
{
	public Vector3 Position { get; set; }
	public float Density { get; set; }
	public bool HasBeenTerraformed {  get; set; }
	public bool IsSolidCave { get; set; }
	public bool HasReachedRealWorldRadius { get; internal set; }

	public VoxelVertex(Vector3 position, float density = 0f, bool hasBeenTerraFormed = false)
	{
		Position = position;
		Density = density;
		HasBeenTerraformed = hasBeenTerraFormed;
	}

	public void TerraformRemove(float density)
	{
		Density = density;
		HasBeenTerraformed = true;
	}

}

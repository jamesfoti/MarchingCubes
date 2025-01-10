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

	public void TerraformRemove(float density, Vector3 brushCenter, float brushRadius)
	{
		Density = density;
		HasBeenTerraformed = true;


		// TODO: figure this out:
		//Vector3 offset = Position - brushCenter;
		//float sqrDst = Vector3.Dot(offset, offset);
		//float distance = Mathf.Sqrt(sqrDst);
		//float brushWeight = Mathf.SmoothStep(brushRadius * .7f, brushRadius, distance);
		//Density += density * Time.deltaTime * brushWeight;
	}

	public void TerraformRemove(float brushRadius, Vector3 brushCenter)
	{
		Vector3Int v3Int = new Vector3Int(Mathf.FloorToInt(brushCenter.x), Mathf.FloorToInt(brushCenter.y), Mathf.FloorToInt(brushCenter.z));
		Vector3 offset = Position - v3Int;
		float squareDistance = Vector3.Dot(offset, offset);


		float distance = Mathf.Sqrt(squareDistance);
		float brushWeight = Mathf.SmoothStep(brushRadius, brushRadius * .07f, distance);
		Density += brushWeight;
		HasBeenTerraformed = true;
	}

}

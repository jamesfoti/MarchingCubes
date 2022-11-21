using System;
using System.Collections.Generic;
using UnityEngine;

public class VoxelDemo : MonoBehaviour
{
	[SerializeField] private Color _offColor = Color.white;
	[SerializeField] private Color _onColor = Color.green;
	[SerializeField] private float _offDensityValue = 0f;
	[SerializeField] private float _onDensityValue = 1f;
	[SerializeField] private float _isoLevel = .5f;

	private Voxel _voxel;
	private float _voxelSize = 1f;
	private int _numberOfVerticesInVoxel = 8;
	
	private void Start()
	{
		Vector3 centerPosition = transform.position;

		float halfVoxelSize = _voxelSize * .5f;

		Vector3 bottomLeftVertex = new Vector3(centerPosition.x - _voxelSize * halfVoxelSize, centerPosition.y - _voxelSize * halfVoxelSize, centerPosition.z - halfVoxelSize);
		
		_voxel = new Voxel(bottomLeftVertex, _voxelSize);

		for (int i = 1; i <= _numberOfVerticesInVoxel; i++)
		{
			gameObject.transform.Find("v" + i).GetComponent<Renderer>().material.color = _offColor;
		}
	}

	private void Update()
	{
		DetectObjectClick();
	}

	private void DetectObjectClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				Material material = hit.transform.GetComponent<Renderer>().material;
				float densityValueToUse;

				if (material.color == _offColor)
				{
					material.color = _onColor;
					densityValueToUse = _onDensityValue;
				}
				else
				{
					material.color = _offColor;
					densityValueToUse = _offDensityValue;
				}

				switch (hit.transform.name)
				{
					case "v1":
						_voxel.CornerDensities[0] = densityValueToUse;
						break;
					case "v2":
						_voxel.CornerDensities[1] = densityValueToUse;
						break;
					case "v3":
						_voxel.CornerDensities[2] = densityValueToUse;
						break;
					case "v4":
						_voxel.CornerDensities[3] = densityValueToUse;
						break;
					case "v5":
						_voxel.CornerDensities[4] = densityValueToUse;
						break;
					case "v6":
						_voxel.CornerDensities[5] = densityValueToUse;
						break;
					case "v7":
						_voxel.CornerDensities[6] = densityValueToUse;
						break;
					case "v8":
						_voxel.CornerDensities[7] = densityValueToUse;
						break;
				}

				CreateMesh();
			}
		}
	}

	private void CreateMesh()
	{
		GetComponent<MeshFilter>().mesh = MarchingCubesHelper.CreateMeshFromMarchingTheCubes(new List<Voxel>() { _voxel }, _isoLevel);
	}

	private void OnDrawGizmosSelected()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;

		foreach (Vector3 vertex in mesh.vertices)
		{
			Debug.Log(vertex);
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(vertex, .05f);
		}
	}
}

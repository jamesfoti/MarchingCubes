using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VoxelDemo : MonoBehaviour
{
	[SerializeField] private Color _offColor = Color.white;
	[SerializeField] private Color _onColor = Color.green;
	[SerializeField] private float _offDensityValue = 0f;
	[SerializeField] private float _onDensityValue = 1f;
	[SerializeField] private float _isoLevel = .5f;
	[SerializeField] private InterpolationType _interpolationType = InterpolationType.None;
	private Voxel _voxel;
	private float _voxelSize = 1f;
	private int _numberOfVerticesInVoxel = 8;
	
	private void Start()
	{
		Vector3 centerPosition = transform.position;

		float halfVoxelSize = _voxelSize * .5f;

		Vector3 bottomLeftVertex = new Vector3(centerPosition.x - _voxelSize * halfVoxelSize, centerPosition.y - _voxelSize * halfVoxelSize, centerPosition.z - halfVoxelSize);
		
		_voxel = new Voxel(bottomLeftVertex, _voxelSize);

		for (int i = 0; i < _numberOfVerticesInVoxel; i++)
		{
			gameObject.transform.Find("v" + (i + 1)).GetComponent<Renderer>().material.color = _offColor;
			_voxel.VoxelVertices[i].Density = _offDensityValue;
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
						_voxel.VoxelVertices[0].Density = densityValueToUse;
						break;
					case "v2":
						_voxel.VoxelVertices[1].Density = densityValueToUse;
						break;
					case "v3":
						_voxel.VoxelVertices[2].Density = densityValueToUse;
						break;
					case "v4":
						_voxel.VoxelVertices[3].Density = densityValueToUse;
						break;
					case "v5":
						_voxel.VoxelVertices[4].Density = densityValueToUse;
						break;
					case "v6":
						_voxel.VoxelVertices[5].Density = densityValueToUse;
						break;
					case "v7":
						_voxel.VoxelVertices[6].Density = densityValueToUse;
						break;
					case "v8":
						_voxel.VoxelVertices[7].Density = densityValueToUse;
						break;
				}

				CreateMesh();
			}
		}
	}

	private void CreateMesh()
	{
		GetComponent<MeshFilter>().mesh = MarchingCubesHelper.CreateMeshFromMarchingTheCubes(new List<Voxel>() { _voxel }, _isoLevel, _interpolationType);
	}
}

using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{
    public float DragSpeed = 10f;
    public float RotationSpeed = 50f;
	public float editRadius = 1f;
	public Planet planet;

    private Vector3 _dragDirection = Vector3.zero;
    private Vector3 _rotationDirection = Vector3.zero;

	private void Update()
    {
        ReadTerraformingInput();

		ReadMovmentInput();
        Move();
	}

	public static bool SphereIntersectsBox(Vector3 sphereCentre, float sphereRadius, Vector3 boxCentre, Vector3 boxSize)
	{
		float closestX = Mathf.Clamp(sphereCentre.x, boxCentre.x - boxSize.x / 2, boxCentre.x + boxSize.x / 2);
		float closestY = Mathf.Clamp(sphereCentre.y, boxCentre.y - boxSize.y / 2, boxCentre.y + boxSize.y / 2);
		float closestZ = Mathf.Clamp(sphereCentre.z, boxCentre.z - boxSize.z / 2, boxCentre.z + boxSize.z / 2);

		float dx = closestX - sphereCentre.x;
		float dy = closestY - sphereCentre.y;
		float dz = closestZ - sphereCentre.z;

		float sqrDstToBox = dx * dx + dy * dy + dz * dz;
		return sqrDstToBox < sphereRadius * sphereRadius;
	}

	private List<PlanetChunk> OverlapSphereBasedOnChunks(Vector3 center, float radius)
	{
		List<PlanetChunk> chunksInOverlapShere = new List<PlanetChunk>();
		foreach (KeyValuePair<Vector3, PlanetChunk> vector3ChunkKeyValuePair in planet.PlanetChunks)
		{
			PlanetChunk planetChunk = vector3ChunkKeyValuePair.Value;

			foreach (Voxel voxel in planetChunk.Voxels)
			{
				foreach (VoxelVertex voxelVertex in voxel.VoxelVertices)
				{
					if (Vector3.Distance(center, voxelVertex.Position) <= radius)
					{
						chunksInOverlapShere.Add(planetChunk);
					}
				}
			}
		}


		return chunksInOverlapShere;
	}

	private void ReadTerraformingInput()
    {
		if (Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100))
			{
				if (hit.collider.gameObject.tag.Equals("PlanetChunk"))
				{
					List<PlanetChunk> chunks = OverlapSphereBasedOnChunks(hit.point, editRadius);
					foreach (PlanetChunk chunk in chunks)
					{
						foreach (Voxel voxel in chunk.Voxels)
						{
							foreach (VoxelVertex voxelVertex in voxel.VoxelVertices)
							{
								if (Vector3.Distance(hit.point, voxelVertex.Position) < editRadius)
								{
									voxelVertex.TerraformRemove(1f, hit.point, editRadius);
								}
							}
						}

						chunk.CreateMesh();
					}
				}
			}
			
			
		}
	}

    private void ReadMovmentInput()
	{
        if (Input.GetMouseButton(1))
        {
            _rotationDirection.x = Input.GetAxis("Mouse X");
            _rotationDirection.y = Input.GetAxis("Mouse Y");
        }

        if (Input.GetMouseButton(2))
        {
            _dragDirection.x = -Input.GetAxis("Mouse X");
            _dragDirection.y = -Input.GetAxis("Mouse Y");
        }

        _dragDirection.z = Input.GetAxis("Mouse ScrollWheel");
    }

    private void Move()
	{
        if (_dragDirection.sqrMagnitude > 0)
        {
            transform.Translate(_dragDirection * DragSpeed * Time.deltaTime);
        }

        if (_rotationDirection.sqrMagnitude > 0)
        {
			transform.Rotate(0f, _rotationDirection.x * RotationSpeed * Time.deltaTime, 0f, Space.World);
			transform.Rotate(-_rotationDirection.y * RotationSpeed * Time.deltaTime, 0f, 0f, Space.Self);
        }
    }
}
using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{
    public float DragSpeed = 10f;
    public float RotationSpeed = 50f; 

    private Vector3 _dragDirection = Vector3.zero;
    private Vector3 _rotationDirection = Vector3.zero;

	private void Update()
    {
        ReadTerraformingInput();

		ReadMovmentInput();
        Move();
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
					Debug.Log("Clicked on Planet Chunk");

					Collider[] colliders = Physics.OverlapSphere(hit.point, 1f);
					foreach (Collider collider in colliders)
					{
						PlanetChunk planetChunk = collider.gameObject.GetComponent<PlanetChunk>();
						Debug.Log(planetChunk);
                        planetChunk.SetVoxelDensities();
						foreach (Voxel voxel in planetChunk.Voxels)
                        {
                            foreach (VoxelVertex voxelVertex in voxel.VoxelVertices)
                            {
                                if (Vector3.Distance(hit.point, voxelVertex.Position) < 1f)
                                {
									//voxelVertex.Density = 0f;
                                    voxelVertex.TerraformRemove(0f);
								}
							}
                        }

						planetChunk.CreateMesh();
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Chunk : MonoBehaviour {

    public Vector3 origin;
    public float width;
    public float height;
    public float depth;
    public float surfaceLevel;
    private Vector3 centerOffset;

    private MeshFilter meshFilter;
    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();
    private List<Voxel3D> voxels = new List<Voxel3D>();

	private void Start() {
        centerOffset = new Vector3(width * .5f, height * .5f, width * .5f);
        meshFilter = GetComponent<MeshFilter>();
        SetVoxelData();
        CreateMesh();
	}

	public void March(Voxel3D voxel) {
        int configIndex = CubeData.GetConfigIndex(voxel.densities, surfaceLevel);

        int[] edgeList = CubeData.GetEdgeList(configIndex);

        foreach (int edgeIndex in edgeList) {
            if (edgeIndex == -1) return;

            Vector3 vertA = voxel.position + CubeData.edges[edgeIndex, 0]; // beginning of the edge
            Vector3 vertB = voxel.position + CubeData.edges[edgeIndex, 1]; // end of the edge

            // Midpoint without interpolation.
            Vector3 mid = (vertA + vertB) / 2f;

            vertices.Add(mid);
            triangles.Add(vertices.Count - 1);
        }
    }

    public void SetVoxelData() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                for (int z = 0; z < depth; z++) {
                    Vector3 bottomLeftCorner = new Vector3(x, y, z) - centerOffset + origin;
                    Vector3[] corners = new Vector3[8];
                    float[] densities = new float[8];

                    for (int i = 0; i < 8; i++) {
                        corners[i] = bottomLeftCorner + CubeData.corners[i];

                        // This is done b/c noise functions don't like whole numbers.
                        float nx = (corners[i].x / width) * 1f;
                        float ny = (corners[i].y / height) * 1f;
                        float nz = (corners[i].z / depth) * 1f;

                        densities[i] = Noise.PerlinNoise3D(nx, ny, nz);
                        Debug.Log(densities[i]);
					}

                    Voxel3D voxel = new Voxel3D(corners, densities);
                    voxels.Add(voxel);
                }
            }
        }
	}

    private float SetRectangularShapeData(float x, float y, float z) {
        // this function is used for testing purposes to make sure Marching Cube algo works.
        Vector3 minCoord = new Vector3(-width * .5f, -height * .5f, -depth * .5f) + origin;
        Vector3 maxCoord = new Vector3(width * .5f, height * .5f, depth * .5f) + origin;

        if (x == minCoord.x) return 1;
        else if (x == maxCoord.x) return 1;

        else if (y == minCoord.y) return 1;
        else if (y == maxCoord.y) return 1;

        else if (z == minCoord.z) return 1;
        else if (z == maxCoord.z) return 1;

        else return 0;
    }

    public void CreateMesh() {
        ClearMeshData();
        for (int i = 0; i < voxels.Count; i++) {
            March(voxels[i]);
		}
        UpdateMesh();
	}

    private void ClearMeshData() {
        vertices.Clear();
        triangles.Clear();
	}

    private void UpdateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

	private void OnGizmosSelected() {
		for (int i = 0; i < voxels.Count; i++) {
            Voxel3D currVoxel = voxels[i];
            for (int j = 0; j < currVoxel.corners.Length; j++) {
                Color color = Color.Lerp(Color.green, Color.white, currVoxel.densities[j]);
                Gizmos.color = color;
                Gizmos.DrawSphere(voxels[i].corners[j], .1f);
			}
		}
	}
}

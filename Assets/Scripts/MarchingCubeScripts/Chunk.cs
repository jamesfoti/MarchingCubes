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
                    Vector3 bottomLeftCorner = new Vector3(x, y, z) - centerOffset;
                    Vector3[] corners = new Vector3[8];
                    float[] densities = new float[8];

                    for (int i = 0; i < 8; i++) {
                        corners[i] = bottomLeftCorner + CubeData.corners[i];

                        // This is done b/c noise functions don't like whole numbers.
                        float nx = (corners[i].x / width) * 4;
                        float ny = (corners[i].y / height) * 4;
                        float nz = (corners[i].z / depth) * 4;

                        densities[i] = SetRectangularShapeData(corners[i].x, corners[i].y, corners[i].z);
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
        float xMin = -width * .5f;
        float xMax = width * .5f;

        float yMin = -height * .5f;
        float yMax = height * .5f;

        float zMin = -depth * .5f;
        float zMax = depth * .5f;

        if (x == xMin) return 1;
        else if (x == xMax) return 1;

        else if (y == yMin) return 1;
        else if (y == yMax) return 1;

        else if (z == zMin) return 1;
        else if (z == zMax) return 1;

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
}

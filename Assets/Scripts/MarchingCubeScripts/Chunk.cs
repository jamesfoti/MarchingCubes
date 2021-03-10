using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoiseTest;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Chunk : MonoBehaviour {

    public float width;
    public float height;
    public float depth;
    public float surfaceLevel;

    private OpenSimplexNoise noise = new OpenSimplexNoise();
    private MeshFilter meshFilter;
    private List<Vector3> traversedCorners = new List<Vector3>();
    private List<Vector3> vertices = new List<Vector3>();
    private List<int> triangles = new List<int>();
    private List<float> densities = new List<float>();

    private void Start() {
        meshFilter = GetComponent<MeshFilter>();
        CreateMesh();

    }

    private void CreateMesh() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                for (int z = 0; z < depth; z++) {
                    Vector3 centerOffset = new Vector3(width * .5f, height * .5f, depth * .5f);
                    Vector3 bottomLeftCorner = new Vector3(x, y, z) - centerOffset;
                    
                    float[] cubeDensities = new float[8];
                    for (int i = 0; i < 8; i++) {
                        Vector3 corner = bottomLeftCorner + CubeData.corners[i];
                        cubeDensities[i] = GetDensity(corner.x, corner.y, corner.z);
                        Debug.Log(cubeDensities[i]);
                        densities.Add(cubeDensities[i]);
                        traversedCorners.Add(corner);
                    }
                    March(bottomLeftCorner, cubeDensities);
                }
            }
        }
        UpdateMesh();
    }

    public float GetDensity(float x, float y, float z) {
        // Get a terrain height using regular old Perlin noise.
        float thisHeight = (float)height * Mathf.PerlinNoise((float)x / 16f * 1.5f + 0.001f, (float)z / 16f * 1.5f + 0.001f);

        float point = 0;
        // We're only interested when point is within 0.5f of terrain surface. More than 0.5f less and it is just considered
        // solid terrain, more than 0.5f above and it is just air. Within that range, however, we want the exact value.
        if (y <= thisHeight - 0.5f)
            point = 0f;
        else if (y > thisHeight + 0.5f)
            point = 1f;
        else if (y > thisHeight)
            point = (float)y - thisHeight;
        else
            point = thisHeight - (float)y;

        // Set the value of this point in the terrainMap.
        return point;
    }

    void March(Vector3 position, float[] cubeDensities) {

        // Get the configuration index of this cube.
        int configIndex = CubeData.GetConfigIndex(cubeDensities, surfaceLevel);

        // If the configuration of this cube is 0 or 255 (completely inside the terrain or completely outside of it) we don't need to do anything.
        if (configIndex == 0 || configIndex == 255)
            return;

        // Loop through the edge indicies that we will use to extract the verticies that make an edge.
        int[] edgeList = CubeData.GetEdgeList(configIndex);
        foreach (int edgeIndex in edgeList) {
            // If the current edgeIndex is -1, there are no more indices and we can exit the function.
            if (edgeIndex == -1) return;

            // Find the corners that make up the mid point.
            Vector3 vertA = position + CubeData.edges[edgeIndex, 0];
            Vector3 vertB = position + CubeData.edges[edgeIndex, 1];

            // Find midpoint.
            Vector3 mid = (vertA + vertB) / 2f;

            // Add each midpoint to verticies and then add the index of the last vertice that we just added.
            vertices.Add(mid);
            triangles.Add(vertices.Count - 1);
        }
    }

    void UpdateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

	private void OnDrawGizmos() {
		for (int i = 0; i < densities.Count; i++) {
            //Color color = Color.Lerp(Color.white, Color.black, densities[i]);
            //Gizmos.color = color;
            //Gizmos.DrawSphere(traversedCorners[i], .1f);
		}
	}



}

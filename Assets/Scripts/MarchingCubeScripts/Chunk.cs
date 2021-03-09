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
	private List<Vector3> vertices = new List<Vector3>();
	private List<int> triangles = new List<int>();
	public List<float> densities = new List<float>();

	private void Start() {
		CreateMesh();
	}


	private void CreateMesh() {
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				for (int z = 0; z < depth; z++) {
					Vector3 centerOffset = new Vector3(width * .5f, height * .5f, depth * .5f);
					float[] cubeDensities = new float[8];
					for (int i = 0; i < 8; i++) {
						Vector3 corner = new Vector3(x, y, z) - centerOffset + CubeData.corners[i];
						cubeDensities[i] = SampleNoise(corner);
						densities.Add(cubeDensities[i]);
						vertices.Add(corner);
					}
				}
			}
		}
	}

	private void ClearMesh() {
		vertices.Clear();
		triangles.Clear();
	}

	private void UpdateMesh() {
		Mesh mesh = new Mesh();
		mesh.Clear();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		GetComponent<MeshFilter>().mesh = mesh;
	}

	private float SampleNoise(Vector3 pos) {
		float density = (float)noise.Evaluate(pos.x, pos.y, pos.z) + pos.y;
		return density;
	}

	private void March(float[] cubeDensities) {
		int configIndex = CubeData.GetConfigIndex(cubeDensities, surfaceLevel);
		int[] edgeList = CubeData.GetEdgeList(configIndex);
	}

	private void OnDrawGizmos() {
		for (int i = 0; i < vertices.Count; i++) {
			Gizmos.color = Color.Lerp(Color.white, Color.black, densities[i]);
			Gizmos.DrawSphere(vertices[i], .1f);
		}
	}



}

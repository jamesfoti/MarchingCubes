using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class Voxel3DDemo : MonoBehaviour {
	public int configIndex;

	public List<Vector3> vertices = new List<Vector3>();
	public List<int> triangles = new List<int>();
	private MeshFilter meshFilter;

	private void Start() {
		meshFilter = GetComponent<MeshFilter>();
	}

	private void Update() {
		ToggleDensity();
	}

	private void ClearMeshData() {
		vertices.Clear();
		triangles.Clear();
	}

	private void CreateMeshData() {
		int[] edgeList = CubeData.GetEdgeList(configIndex);

		foreach (int edgeIndex in edgeList) {
			if (edgeIndex == -1) return;
			Vector3 vert1 = CubeData.edges[edgeIndex, 0]; // beginning of the edge
			Vector3 vert2 = CubeData.edges[edgeIndex, 1]; // end of the edge

			Vector3 mid = (vert1 + vert2) / 2f;

			vertices.Add(mid);
			triangles.Add(vertices.Count - 1);
		}
	}

	private void UpdateMesh() {
		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.RecalculateNormals();
		meshFilter.mesh = mesh;
	}

	private void ToggleDensity() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0)) {
			string name = hit.transform.gameObject.name;
			string tag = hit.transform.gameObject.tag;
			Color isOn = hit.transform.GetComponent<MeshRenderer>().material.color;

			if (name == "v1") {
				if (isOn == Color.white) {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
					configIndex += 1;
				}
				else {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.white;
					configIndex -= 1;
				}
			}

			if (name == "v2") {
				if (isOn == Color.white) {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
					configIndex += 2;
				}
				else {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.white;
					configIndex -= 2;
				}
			}

			if (name == "v3") {
				if (isOn == Color.white) {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
					configIndex += 4;
				}
				else {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.white;
					configIndex -= 4;
				}
			}

			if (name == "v4") {
				if (isOn == Color.white) {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
					configIndex += 8;
				}
				else {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.white;
					configIndex -= 8;
				}
			}

			if (name == "v5") {
				if (isOn == Color.white) {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
					configIndex += 16;
				}
				else {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.white;
					configIndex -= 16;
				}
			}

			if (name == "v6") {
				if (isOn == Color.white) {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
					configIndex += 32;
				}
				else {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.white;
					configIndex -= 32;
				}
			}

			if (name == "v7") {
				if (isOn == Color.white) {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
					configIndex += 64;
				}
				else {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.white;
					configIndex -= 64;
				}
			}

			if (name == "v8") {
				if (isOn == Color.white) {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.green;
					configIndex += 128;
				}
				else {
					hit.transform.GetComponent<MeshRenderer>().material.color = Color.white;
					configIndex -= 128;
				}
			}
			ClearMeshData();
			CreateMeshData();
			UpdateMesh();
		}
	}
}
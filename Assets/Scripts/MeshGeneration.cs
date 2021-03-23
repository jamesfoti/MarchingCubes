using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneration : MonoBehaviour {

	public int width;
	public int depth;
	public ComputeShader shader;


	public void RunShader() {
		int numTris = 2 * width * depth;
		Triangle[] inputTris = new Triangle[numTris];
		Triangle[] outputTris = new Triangle[numTris];

		ComputeBuffer buffer = new ComputeBuffer(inputTris.Length, 16);
		buffer.SetData(inputTris);
		int kernel = shader.FindKernel("CSMain");
		shader.SetBuffer(kernel, "triangles", buffer);
		shader.Dispatch(kernel, inputTris.Length, 1, 1);
		buffer.GetData(outputTris);


	}
}

public struct Triangle {
	int index;
	Vector3 vertA;
	Vector3 vertB;
	Vector3 vertC;
}

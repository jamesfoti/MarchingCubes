using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BInaryManip : MonoBehaviour {

	private void Start() {
		int index = 0;
		for (int i = 0; i < 4; i+=2) {
			index |= 1 << i;
			
			Debug.Log(index);
		}
	}
}

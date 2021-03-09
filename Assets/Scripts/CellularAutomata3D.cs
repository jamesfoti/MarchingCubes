using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellularAutomata3D : MonoBehaviour {

	public int[,,] cells;
	int rowSize;
	int colSize;
	int depthSize;

	public CellularAutomata3D(int width, int height, int depth) {
		rowSize = height;
		colSize = width;
		depthSize = depth;
		
		cells = new int[rowSize, colSize, depthSize];
	}


	public int CountLivingNeighbors(int centerRow, int centerCol, int centerDepth) {
		int count = 0;
		int rowSize = cells.GetLength(0);
		int colSize = cells.GetLength(1);
		int depthSize = cells.GetLength(2);

		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				for (int k = -1; k < 2; k++) {
					int row = centerRow + i + rowSize;
					int col = centerCol + j + colSize;
					int depth = centerDepth + k + depthSize;

					if (cells[row, col, depth] == 1) {
						count++;
					}
				}
			}
		}

		if (cells[centerRow, centerCol, centerDepth] == 1) {
			count--;
		}

		return count;
	}
}
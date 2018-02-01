using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkRenderer : MonoBehaviour {

	private int worldHeight = 10;
	private int worldWidth = 10;

	[SerializeField] GameObject basicTile;

	private GameObject[,] parkTiles;

	// Use this for initialization
	void Start () {
		parkTiles = new GameObject[worldWidth,worldHeight];

		for (int x = 0; x < worldWidth; x++) {
			for (int y = 0; y < worldHeight; y++) {
				parkTiles[x,y] = Instantiate (basicTile, new Vector3 (x, y, 0), Quaternion.identity);
			}
		}
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGlossary : MonoBehaviour {

	public static TerrainGlossary Instance;

	[SerializeField] public Tilemap tileMap;

	[SerializeField] public TileBase grassTile;
	[SerializeField] public TileBase waterTile;
	[SerializeField] public TileBase sandTile;

	public TerrainGlossary() {
		Instance = this;
	}

	public void setTile(Vector3Int pos, TileBase tile) {
		tileMap.SetTile (pos, tile);
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Scripts;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class GrassTile : RandomTile, InteractableTile {

	public void interact (ToolBar.ToolType tool, Vector3Int pos) {
		Debug.Log ("YO WHATS UP IM A GRASS TILE");
		Debug.Log ("Using tool: " + tool);
		if(tool.Equals(ToolBar.ToolType.Shovel)) {
			TerrainGlossary.Instance.setTile (pos, TerrainGlossary.Instance.waterTile);
		}
	}

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Grass Tile")]
	public static void CreateGrassTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Grass Tile", "New Grass Tile", "asset", "Save Grass Tile", "Assets");
		if (path == "")
			return;

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GrassTile>(), path);
	}
	#endif

}



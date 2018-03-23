using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Scripts;
using Assets.Code.Interfaces;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DirtTile : RandomTile, IInteractable {

	public void Interact (ToolBar.ToolType tool, Vector3Int pos) {
		Debug.Log ("Using tool: " + tool);
		if(tool.Equals(ToolBar.ToolType.Shovel)) {
			TerrainGlossary.Instance.setTile (pos, TerrainGlossary.Instance.waterTile);
		} else if (tool.Equals(ToolBar.ToolType.Grass)) {
			TerrainGlossary.Instance.setTile (pos, TerrainGlossary.Instance.grassTile);
		}
	}

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Dirt Tile")]
	public static void CreateDirtTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Dirt Tile", "New Dirt Tile", "asset", "Save Dirt Tile", "Assets");
		if (path == "")
			return;

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DirtTile>(), path);
	}
	#endif

}



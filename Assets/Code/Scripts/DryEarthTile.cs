using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Code.Scripts;
using Assets.Code.Interfaces; 

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DryEarthTile : RandomTile, IInteractable {

	public void Interact (ToolBar.ToolType tool, Vector3Int pos) {
		Debug.Log ("Using tool: " + tool);
		if(tool.Equals(ToolBar.ToolType.Shovel)) {
			TerrainGlossary.Instance.setTile (pos, TerrainGlossary.Instance.dirtTile);
		}
	}

	#if UNITY_EDITOR
	[MenuItem("Assets/Create/Dry Earth Tile")]
	public static void CreateDryEarthTile()
	{
		string path = EditorUtility.SaveFilePanelInProject("Save Dry Earth Tile", "New Dry Earth Tile", "asset", "Save Dry Earth Tile", "Assets");
		if (path == "")
			return;

		AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DryEarthTile>(), path);
	}
	#endif

}



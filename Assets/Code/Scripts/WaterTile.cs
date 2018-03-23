using System.Collections;
using System.Collections.Generic;
using Assets.Code.Interfaces;
using UnityEngine;
using Assets.Code.Scripts;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class WaterTile : TerrainTile, IInteractable
{

	public void Interact (ToolBar.ToolType tool, Vector3Int pos) {
		Debug.Log ("YO WHATS UP IM A WATER TILE");
		Debug.Log ("Using tool: " + tool);
		if(tool.Equals(ToolBar.ToolType.Shovel)) {
			TerrainGlossary.Instance.setTile (pos, TerrainGlossary.Instance.dirtTile);
		}
	}

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Water Tile")]
    public static void CreateWaterTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Water Tile", "New Water Tile", "asset", "Save Water Tile", "Assets");
        if (path == "")
            return;

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WaterTile>(), path);
    }
#endif

#if UNITY_EDITOR
    [CustomEditor(typeof(WaterTile))]
    public class WaterTileEditor : Editor
    {
        private WaterTile tile { get { return (target as WaterTile); } }

        public void OnEnable()
        {
            if (tile.m_Sprites == null || tile.m_Sprites.Length != 15)
            {
                tile.m_Sprites = new Sprite[15];
                EditorUtility.SetDirty(tile);
            }
        }


        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Place sprites shown based on the contents of the sprite.");
            EditorGUILayout.Space();

            float oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 210;

            EditorGUI.BeginChangeCheck();
            tile.m_Sprites[0] = (Sprite)EditorGUILayout.ObjectField("Filled", tile.m_Sprites[0], typeof(Sprite), false, null);
            tile.m_Sprites[1] = (Sprite)EditorGUILayout.ObjectField("Three Sides", tile.m_Sprites[1], typeof(Sprite), false, null);
            tile.m_Sprites[2] = (Sprite)EditorGUILayout.ObjectField("Two Sides and One Corner", tile.m_Sprites[2], typeof(Sprite), false, null);
            tile.m_Sprites[3] = (Sprite)EditorGUILayout.ObjectField("Two Adjacent Sides", tile.m_Sprites[3], typeof(Sprite), false, null);
            tile.m_Sprites[4] = (Sprite)EditorGUILayout.ObjectField("Two Opposite Sides", tile.m_Sprites[4], typeof(Sprite), false, null);
            tile.m_Sprites[5] = (Sprite)EditorGUILayout.ObjectField("One Side and Two Corners", tile.m_Sprites[5], typeof(Sprite), false, null);
            tile.m_Sprites[6] = (Sprite)EditorGUILayout.ObjectField("One Side and One Lower Corner", tile.m_Sprites[6], typeof(Sprite), false, null);
            tile.m_Sprites[7] = (Sprite)EditorGUILayout.ObjectField("One Side and One Upper Corner", tile.m_Sprites[7], typeof(Sprite), false, null);
            tile.m_Sprites[8] = (Sprite)EditorGUILayout.ObjectField("One Side", tile.m_Sprites[8], typeof(Sprite), false, null);
            tile.m_Sprites[9] = (Sprite)EditorGUILayout.ObjectField("Four Corners", tile.m_Sprites[9], typeof(Sprite), false, null);
            tile.m_Sprites[10] = (Sprite)EditorGUILayout.ObjectField("Three Corners", tile.m_Sprites[10], typeof(Sprite), false, null);
            tile.m_Sprites[11] = (Sprite)EditorGUILayout.ObjectField("Two Adjacent Corners", tile.m_Sprites[11], typeof(Sprite), false, null);
            tile.m_Sprites[12] = (Sprite)EditorGUILayout.ObjectField("Two Opposite Corners", tile.m_Sprites[12], typeof(Sprite), false, null);
            tile.m_Sprites[13] = (Sprite)EditorGUILayout.ObjectField("One Corner", tile.m_Sprites[13], typeof(Sprite), false, null);
            tile.m_Sprites[14] = (Sprite)EditorGUILayout.ObjectField("Empty", tile.m_Sprites[14], typeof(Sprite), false, null);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);

            EditorGUIUtility.labelWidth = oldLabelWidth;
        }
    }
#endif

}



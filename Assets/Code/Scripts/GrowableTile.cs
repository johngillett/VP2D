using System;
using Assets.Code.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Code.Scripts
{
    [Serializable]
    public class GrowableTile : Tile, ITickable
    {
        public Sprite[] m_Sprites;
        public float m_MinGrowTicksPerStage = 10f;
        public float m_MaxGrowTicksPerStage = 10f;
        public Tile.ColliderType m_TileColliderType;
        private int _currentStage = 0;
        private int _currentTicks = 0;
        private int _currentGrowthTickGoal = 10;
        private Guid Id;


        public GrowableTile()
        {
            _currentStage = 0;
            _currentTicks = 0;
            Id = Guid.NewGuid();
        }

        public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go)
        {
            Clock.Instance.AddThingToTick(this);
            return true;
        }

        public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.identity;
            tileData.color = Color.white;
            if (m_Sprites != null && m_Sprites.Length > 0)
            {
                tileData.sprite = m_Sprites[_currentStage];
            }
        }

        public Guid GetUniqueId()
        {
            return Id;
        }

        public void HandleTicks(int numberOfTicks)
        {
            _currentTicks += numberOfTicks;

            if (_currentTicks >= _currentGrowthTickGoal)
            {
                Grow();
            }
        }

        private void Grow()
        {
            var random = new Random();
            _currentTicks = 0;
            if (_currentStage < m_Sprites.Length - 1)
            {
                _currentStage++;
                //this.sprite = m_Sprites[_currentStage];
                _currentGrowthTickGoal = random.Next((int)m_MinGrowTicksPerStage, (int)m_MaxGrowTicksPerStage);
            }
            else
            {
                _currentGrowthTickGoal = (int) m_MaxGrowTicksPerStage;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Growable Tile")]
        public static void CreateGrowableTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Growable Tile", "New Growable Tile", "asset", "Save Growable Tile", "Assets");
            if (path == "")
                return;

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GrowableTile>(), path);
        }
#endif
       
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GrowableTile))]
    public class GrowableTileEditor : Editor
    {
        private GrowableTile tile { get { return (target as GrowableTile); } }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            int count = EditorGUILayout.DelayedIntField("Number of stages", tile.m_Sprites != null ? tile.m_Sprites.Length : 0);
            if (count < 0)
                count = 0;

            if (tile.m_Sprites == null || tile.m_Sprites.Length != count)
            {
                Array.Resize<Sprite>(ref tile.m_Sprites, count);
            }

            if (count == 0)
                return;

            EditorGUILayout.LabelField("Place sprites shown based on the order of growth.");
            EditorGUILayout.Space();

            for (int i = 0; i < count; i++)
            {
                tile.m_Sprites[i] = (Sprite)EditorGUILayout.ObjectField("Sprite " + (i + 1), tile.m_Sprites[i], typeof(Sprite), false, null);
            }

            float minSpeed = EditorGUILayout.FloatField("Minimum Grow Ticks per Stage", tile.m_MinGrowTicksPerStage);
            float maxSpeed = EditorGUILayout.FloatField("Maximum Grow Ticks per Stage", tile.m_MaxGrowTicksPerStage);
            if (minSpeed < 0.0f)
                minSpeed = 0.0f;

            if (maxSpeed < 0.0f)
                maxSpeed = 0.0f;

            if (maxSpeed < minSpeed)
                maxSpeed = minSpeed;

            tile.m_MinGrowTicksPerStage = minSpeed;
            tile.m_MaxGrowTicksPerStage = maxSpeed;

            tile.m_TileColliderType = (Tile.ColliderType)EditorGUILayout.EnumPopup("Collider Type", tile.m_TileColliderType);
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);
        }
    }
#endif
}

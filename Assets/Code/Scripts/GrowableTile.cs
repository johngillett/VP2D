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
    public class GrowableTile : Tile, ITickable, IWaterable
    {
        public Sprite[] m_Sprites;
        public float m_MinGrowTicksPerStage = 10f;
        public float m_MaxGrowTicksPerStage = 10f;
        public int minimimumOptimalWater = 0;
        public int maximumOptimalWater = 100;
        public Tile.ColliderType m_TileColliderType;


        private int _currentStage;
        private int _currentGrowTicks;
        private int _currentGrowthTickGoal = 10;
        private Guid Id;
        private int _currentAmountOfWater;
        private int percentOfOptimalWaterRange;


        public GrowableTile()
        {
            _currentStage = 0;
            _currentGrowTicks = 0;
            _currentAmountOfWater = 50;
            Id = Guid.NewGuid();
        }

        public override bool StartUp(Vector3Int location, ITilemap tilemap, GameObject go)
        {
            Clock.Instance.AddThingToTick(this);
            percentOfOptimalWaterRange = (maximumOptimalWater - minimimumOptimalWater) / 10;
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
            _currentAmountOfWater -= numberOfTicks;

            // Only add a Grow Tick if Plant has optimal amount of water
            if (_currentAmountOfWater > minimimumOptimalWater && _currentAmountOfWater < maximumOptimalWater)
            {
                _currentGrowTicks += numberOfTicks;
            }

            // If plant strays more than a certain percent under Min optimal or over Max optimal, start regressing in growth
            if (_currentAmountOfWater < minimimumOptimalWater - percentOfOptimalWaterRange ||
                _currentAmountOfWater > maximumOptimalWater + percentOfOptimalWaterRange)
            {
                _currentGrowTicks -= numberOfTicks;
            }

            if (_currentGrowTicks >= _currentGrowthTickGoal)
            {
                Grow();
            }
            else if (_currentGrowTicks < 0)
            {
                Ungrow();
            }
        }

        private void Grow()
        {
            var random = new Random();
            _currentGrowTicks = 0;
            if (_currentStage < m_Sprites.Length - 1)
            {
                _currentStage++;
                _currentAmountOfWater -= percentOfOptimalWaterRange;
                //this.sprite = m_Sprites[_currentStage];
                _currentGrowthTickGoal = random.Next((int)m_MinGrowTicksPerStage, (int)m_MaxGrowTicksPerStage);
            }
            else
            {
                _currentGrowthTickGoal = (int) m_MaxGrowTicksPerStage;
            }
        }

        private void Ungrow()
        {
            var random = new Random();
            _currentGrowthTickGoal = random.Next((int)m_MinGrowTicksPerStage, (int)m_MaxGrowTicksPerStage);
            _currentGrowTicks = _currentGrowthTickGoal - 1;
            if (_currentStage > 0)
            {
                _currentStage--;
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

        public void HandleWater(int amountOfWater)
        {
            amountOfWater += amountOfWater;
        }
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

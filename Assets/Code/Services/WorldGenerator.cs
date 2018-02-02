using Assets.Code.Models;
using Assets.Code.Scripts;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.Services
{
	public class WorldGenerator : MonoBehaviour
	{

		[SerializeField]
		private TerrainTile waterTile;

		[SerializeField]
		private RandomTile grassTile;

		[SerializeField]
		private Tile sandTile;

	    [SerializeField] private Tilemap map;

		void Start () {
			GenerateEmptyWorld (map.size.x, map.size.y);
		}

		public void GenerateEmptyWorld(int width, int height)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
					switch (Random.Range (1, 4)) {
						case 1:
                            map.SetTile(new Vector3Int(x, y, 0), grassTile);

                            //ScriptableObject.Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                            break;
						case 2:
						    map.SetTile(new Vector3Int(x, y, 0), waterTile);
                            break;
						case 3:
						    map.SetTile(new Vector3Int(x, y, 0), sandTile);
                            //Instantiate(sandTile, new Vector3(x, y), Quaternion.identity);
                            break;
					}
                }
            }
        }
    }
}

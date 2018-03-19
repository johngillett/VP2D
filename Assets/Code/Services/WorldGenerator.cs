using Assets.Code.Models;
using Assets.Code.Scripts;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.Services
{
	public class WorldGenerator : MonoBehaviour
	{

		void Start () {
			GenerateEmptyWorld (TerrainGlossary.Instance.tileMap.size.x, TerrainGlossary.Instance.tileMap.size.y);
		}

		public void GenerateEmptyWorld(int width, int height)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
					switch (Random.Range (1, 4)) {
						case 1:
							TerrainGlossary.Instance.tileMap.SetTile(new Vector3Int(x, y, 0), TerrainGlossary.Instance.grassTile);

                            //ScriptableObject.Instantiate(grassTile, new Vector3(x, y), Quaternion.identity);
                            break;
						case 2:
							TerrainGlossary.Instance.tileMap.SetTile(new Vector3Int(x, y, 0), TerrainGlossary.Instance.waterTile);
                            break;
						case 3:
							TerrainGlossary.Instance.tileMap.SetTile(new Vector3Int(x, y, 0), TerrainGlossary.Instance.sandTile);
                            //Instantiate(sandTile, new Vector3(x, y), Quaternion.identity);
                            break;
					}
                }
            }
        }
    }
}

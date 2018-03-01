using Assets.Code.Extensions;
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

	    [SerializeField] private Tilemap tileMap;

		void Start () {
			GenerateEmptyWorld (tileMap.size.x, tileMap.size.y);
		}

		public void GenerateEmptyWorld(int width, int height)
        {
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    GenerateTileAt(x, y);
                }
            }
        }

	    private void GenerateTileAt(int x, int y)
	    {
	        var seed = Random.Range(1, 100);

	        if (seed <= 75)
	        {
	            tileMap.CreateTileAt(x, y, sandTile);
                return;
            }
	        if (seed <= 80 || NearbyTileContainsWater(x,y))
	        {
	            tileMap.CreateTileAt(x, y, waterTile);
                return;
	        }

            // Else
            tileMap.CreateTileAt(x, y, grassTile);
	    }

	    private bool NearbyTileContainsWater(int x, int y)
	    {
            var location = new Vector3Int(x, y, 0);
	        int mask = TileValue(tileMap, location + new Vector3Int(0, 1, 0)) ? 1 : 0;
	        mask += TileValue(tileMap, location + new Vector3Int(1, 1, 0)) ? 2 : 0;
	        mask += TileValue(tileMap, location + new Vector3Int(1, 0, 0)) ? 4 : 0;
	        mask += TileValue(tileMap, location + new Vector3Int(1, -1, 0)) ? 8 : 0;
	        mask += TileValue(tileMap, location + new Vector3Int(0, -1, 0)) ? 16 : 0;
	        mask += TileValue(tileMap, location + new Vector3Int(-1, -1, 0)) ? 32 : 0;
	        mask += TileValue(tileMap, location + new Vector3Int(-1, 0, 0)) ? 64 : 0;
	        mask += TileValue(tileMap, location + new Vector3Int(-1, 1, 0)) ? 128 : 0;

	        byte original = (byte)mask;
	        if ((original | 254) < 255) { mask = mask & 125; }
	        if ((original | 251) < 255) { mask = mask & 245; }
	        if ((original | 239) < 255) { mask = mask & 215; }
	        if ((original | 191) < 255) { mask = mask & 95; }

	        return mask > 0;
	    }

	    private bool TileValue(Tilemap tileMap, Vector3Int position)
	    {
	        TileBase tile = tileMap.GetTile(position);
	        return (tile != null && tile == this);
	    }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.Extensions
{
    public static class TilemapExtensions
    {
        public static void CreateTileAt(this Tilemap map, int x, int y, TileBase tile)
        {
            map.SetTile(new Vector3Int(x, y, 0), tile);
        }
    }
}

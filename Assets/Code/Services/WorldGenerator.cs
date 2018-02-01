using Assets.Code.Models;
using UnityEngine;

namespace Assets.Code.Services
{
    public class WorldGenerator
    {

        public static ParkTile[,] GenerateEmptyWorld(int width, int height)
        {
            var world = new ParkTile[width,height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    world[x, y] = new ParkTile(new Position(x, y));
                }
            }

            return world;
        }
    }
}

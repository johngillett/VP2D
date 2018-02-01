using Assets.Code.Models;
using UnityEngine;

namespace Assets.Code.Services
{
	public class WorldGenerator : MonoBehaviour
    {

		[SerializeField]
		private GameObject wildTile;

		[SerializeField]
		private GameObject dirtTile;

		[SerializeField]
		private GameObject grassTile;

		void Start () {
			GenerateEmptyWorld (10, 10);
		}

		public GameObject[,] GenerateEmptyWorld(int width, int height)
        {
			var world = new GameObject[width,height];

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
					GameObject tile = null;

					switch (Random.Range (1, 4)) {
						case 1:
							tile = wildTile;
							break;
						case 2:
							tile = dirtTile;
							break;
						case 3:
							tile = grassTile;
							break;
					}

					Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                }
            }

            return world;
        }
    }
}

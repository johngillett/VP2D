using Assets.Code.Services;
using UnityEngine;

namespace Assets.Code.Renderers
{
    public class ParkRenderer : MonoBehaviour {

        [SerializeField]
        int worldHeight = 10;

        [SerializeField]
        int worldWidth = 10;

        [SerializeField]
        GameObject basicTile;

        // Use this for initialization
        void Start ()
        {
            //var world = WorldGenerator.GenerateEmptyWorld(worldWidth, worldHeight);

        }

    }
}

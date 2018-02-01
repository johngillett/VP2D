using UnityEngine;

namespace Assets.Code.Models
{
	public class ParkTile : MonoBehaviour
    {
		[SerializeField] public int tileId;

		public enum TileType {Wild, Dirt, Grass};

		[SerializeField] public TileType tileType;

        public Position Position { get; set; }

		public void setPosition(int x, int y) {
			this.Position = new Position (x, y);
		}
    }
}

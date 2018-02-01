using UnityEngine;

namespace Assets.Code.Models
{
    public class ParkTile
    {
        public Position Position { get; set; }

        public ParkTile(Position position)
        {
            Position = position;
        }
    }
}

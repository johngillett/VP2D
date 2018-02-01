using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Code.Models;
using UnityEngine;

namespace Assets.Code.Extensions
{
    public static class PositionExtensions
    {
        public static Vector3 ToVector3(this Position position)
        {
            return new Vector3(position.X, position.Y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Assets.Code.Interfaces
{
    public interface IWaterable
    {
        void HandleWater(int amountOfWater);
    }
}

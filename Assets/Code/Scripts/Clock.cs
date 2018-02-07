using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Assets.Code.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.Scripts
{
    public class Clock : MonoBehaviour
    {
        public static Clock Instance;

        private List<ITickable> _thingsToTick;

        [SerializeField] private int _secondsPerTick;

        [SerializeField] private Tilemap _tilemap;

        public Clock()
        {
            Instance = this;
            _thingsToTick = new List<ITickable>();
        }

        void Start()
        {
            InvokeRepeating("Tick", 0, _secondsPerTick);
        }

        private void Tick()
        {
            foreach (var x in _thingsToTick)
            {
                x.HandleTicks(1);
            }
            _tilemap.RefreshAllTiles();
        }

        public void AddThingToTick(ITickable thingToAdd)
        {
            if (_thingsToTick.All(x => x.GetUniqueId() != thingToAdd.GetUniqueId()))
            {
                _thingsToTick.Add(thingToAdd);
            }
        }
    }
}

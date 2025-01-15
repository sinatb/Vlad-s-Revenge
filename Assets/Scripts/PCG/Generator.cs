using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PCG
{
    public class Generator : MonoBehaviour
    {
        public GameObject      roomPrefab;
        private GeneratorParams _gp;
        public void SetParams(GeneratorParams gp)
        {
            _gp = gp;
        }

        public List<Room> GenerateRooms()
        {
            var rooms = new List<Room>();
            for (var i=0; i<_gp.numRooms; i++)
            { 
                rooms.Add(GenerateRoom());
            }
            return rooms;
        }

        private Room GenerateRoom()
        {
            var grid = Util.GenerateNoiseGrid(_gp.roomWidth, _gp.roomHeight, _gp.roomDensity);
            grid = Util.ApplyRules(grid, _gp.ruleIterations);
            var r = Instantiate(roomPrefab).GetComponent<Room>();
            r.Init(grid);
            return r;
        }
    }
}
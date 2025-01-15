using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace PCG
{
    public class Generator : MonoBehaviour
    {
        public GameObject      roomPrefab;
        public GeneratorParams gp;

        public List<Room> GenerateRooms()
        {
            var rooms = new List<Room>();
            for (var i=0; i<gp.numRooms; i++)
            { 
                rooms.Add(GenerateRoom());
            }
            return rooms;
        }

        private Room GenerateRoom()
        {
            var grid = Util.GenerateNoiseGrid(gp.roomWidth, gp.roomHeight, gp.roomDensity);
            grid = Util.ApplyRules(grid, gp.ruleIterations);
            var r = Instantiate(roomPrefab).GetComponent<Room>();
            var style = Util.GetRandomItem(gp.styles);
            r.Init(grid, style);
            return r;
        }
    }
}
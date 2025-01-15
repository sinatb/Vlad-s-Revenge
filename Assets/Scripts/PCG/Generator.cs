using System.Collections.Generic;
using System.Linq;
using Difficulty;
using UnityEngine;
using UnityEngine.Serialization;

namespace PCG
{
    public class Generator : MonoBehaviour
    {
        public GameObject      roomPrefab;
        public GeneratorParams gp;
        public LevelDifficulty levelDifficulty;
        
        
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
            var difficulty = Util.GetRandomItem(levelDifficulty.settings).difficulty;
            r.Init(grid, style,difficulty);
            return r;
        }
    }
}
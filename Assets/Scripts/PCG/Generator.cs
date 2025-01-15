using System;
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

        private Dictionary<DifficultySettings,int> _difficultyCount;

        private void Awake()
        {
            _difficultyCount = new Dictionary<DifficultySettings, int>();
            foreach (var setting in levelDifficulty.settings)
            {
                _difficultyCount.Add(setting.difficulty, setting.count);
            }
        }

        private DifficultySettings GetRandomSetting()
        {
            var difficulty = Util.GetRandomItem(levelDifficulty.settings).difficulty;
            while(_difficultyCount[difficulty] == 0)
            {
                difficulty = Util.GetRandomItem(levelDifficulty.settings).difficulty;
            }
            _difficultyCount[difficulty]--;
            return difficulty;
        }
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
            var difficulty = GetRandomSetting();
            r.Init(grid, style,difficulty);
            return r;
        }
    }
}
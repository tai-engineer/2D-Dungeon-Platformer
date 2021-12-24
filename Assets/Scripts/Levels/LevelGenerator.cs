using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Common.Singleton;
namespace DP2D
{
    public class LevelGenerator : Singleton<LevelGenerator>
    {
        [SerializeField] RuleTile _tile;

        /// <summary>
        /// Rule tile does not apply on multiple tilemaps.
        /// Each room has to get tilemap from generator for modifications
        /// </summary>
        [SerializeField] Tilemap _tileMap;
        [SerializeField] Room _roomPreb;

        [SerializeField] int _levelWidth;
        [SerializeField] int _levelHeight;

        [Tooltip("Random number starting from 1 to set value." +
            "This value will be used to scale perlin noise")]
        [Min(1)] public int scale;
        [Tooltip("Random number starting from 0 to set value." +
            "This value will be used for x and y samples of perlin noise")]
        [Min(0)] public int seed;
        List<Room> _rooms;
        protected override void Awake()
        {
            base.Awake();
            _rooms = new List<Room>();
        }
        void Start()
        {
            _tileMap.ClearAllTiles();
            GeneratateLevel();
        }
        void GeneratateLevel()
        {
            for (int i = 0, y = 0; y < _levelHeight; y++)
            {
                for (int x = 0; x < _levelWidth; x++, i++)
                {
                    _rooms.Add(CreateRoom(x, y, i));
                    _rooms[i].Build(new Vector2Int(x * _rooms[i].width, y * _rooms[i].height));
                }
            }
        }
        Room CreateRoom(int x, int y, int i)
        {
            Room room = Instantiate(_roomPreb, transform);
            room.name = "Room_" + i;
            room.transform.localPosition = new Vector2(x * room.width, y * room.height);
            room.SetTile(_tile, _tileMap);
            return room;
        }

#if UNITY_EDITOR
        [ContextMenu("Re-Generate")]
        public void ReGenerate()
        {
            _tileMap.ClearAllTiles();
            _rooms.Clear();
            GeneratateLevel();
        }
#endif
    }
}

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
        [SerializeField, Min(10)] int _roomWidth;
        [SerializeField, Min(10)] int _roomHeight;
        [Tooltip("Random number starting from 1 to set value." +
            "This value will be used to scale perlin noise")]
        [Min(1)] public int scale;
        [Tooltip("Random number starting from 0 to set value." +
            "This value will be used for x and y samples of perlin noise")]
        [Min(0)] public int seed;
        Room[] _rooms;
        RoomData _roomData;
        protected override void Awake()
        {
            base.Awake();
            _rooms = new Room[_levelWidth * _levelHeight];
            _roomData = new RoomData(_roomWidth, _roomHeight);
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
                for (int x = 0; x < _levelWidth; x++)
                {
                    CreateRoom(x, y, i++);
                }
            }
        }
        void CreateRoom(int x, int y, int i)
        {
            Room room = _rooms[i] = Instantiate(_roomPreb, transform);
            room.name = "Room_" + i;
            room.transform.localPosition = new Vector2(x * _roomWidth, y * _roomHeight);
            room.width = _roomWidth;
            room.height = _roomHeight;
            room.data = _roomData;
            room.SetTile(_tile, _tileMap);
            room.Build(new Vector2Int(x * _roomWidth, y * _roomHeight));
        }

#if UNITY_EDITOR
        [ContextMenu("Re-Generate")]
        public void ReGenerate()
        {
            _tileMap.ClearAllTiles();
            GeneratateLevel();
        }
#endif
    }
}

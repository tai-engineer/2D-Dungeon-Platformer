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
        public int perlingScale;
        [Tooltip("Random number starting from 0 to set value." +
            "This value will be used for x and y samples of perlin noise")]
        public int perlingOffset;
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
            InitializeRooms();
            BuildRooms();
        }
        void InitializeRooms()
        {
            for (int i = 0, y = 0; y < _levelHeight; y++)
            {
                for (int x = 0; x < _levelWidth; x++)
                {
                    CreateRoom(x, y, i++);
                }
            }
        }
        void BuildRooms()
        {
            foreach(Room room in _rooms)
            {
                room.Build();
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
            room.offset = new Vector2Int(x * _roomWidth, y * _roomHeight);
            room.tile = _tile;
            room.tileMap = _tileMap;

            SetRoomExits(x, y, i);
        }
        void SetRoomExits(int x, int y, int i)
        {
            if (x > 0)
            {
                _rooms[i].leftExit = _rooms[i - 1].rightExit;
                if (x < _levelWidth - 1)
                {
                    _rooms[i].rightExit = new RoomExit(ExitDirection.LeftRight, _roomData);
                }
            }
            else
            {
                _rooms[i].rightExit = new RoomExit(ExitDirection.LeftRight, _roomData);
            }

            if(y > 0)
            {
                _rooms[i].bottomExit = _rooms[i - _levelWidth].topExit;
                if( y < _levelHeight - 1)
                {
                    _rooms[i].topExit = new RoomExit(ExitDirection.TopBottom, _roomData);
                }
            }
            else
            {
                _rooms[i].topExit = new RoomExit(ExitDirection.TopBottom, _roomData);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Re-Generate")]
        public void ReGenerate()
        {
            _tileMap.ClearAllTiles();
            InitializeRooms();
            BuildRooms();
        }
#endif
    }
}

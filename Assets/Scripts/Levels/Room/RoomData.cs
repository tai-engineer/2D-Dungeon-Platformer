using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class RoomData
    {
        public int width;
        public int height;

        public readonly int ceilingHeightMin;
        public readonly int groundHeightMin;
        public readonly int ceilingHeightMax;
        public readonly int groundHeightMax;
        public readonly int groundWidthMin;
        
        public readonly int emptyHeight;
        public RoomData(int width, int height)
        {
            this.width = width;
            this.height = height;

            ceilingHeightMin = 2;
            groundHeightMin = 2;
            ceilingHeightMax = 4;
            groundWidthMin = 6;

            emptyHeight = this.height / 3;

            groundHeightMax = this.height - emptyHeight - ceilingHeightMax;
        }
    }
    public enum ExitDirection { LeftRight, TopBottom}

    [System.Serializable]
    public class RoomExit
    {
        public ExitDirection direction;
        RoomData _roomData;

        [SerializeField] int _xMin;
        [SerializeField] int _xMax;
        [SerializeField] int _yMin;
        [SerializeField] int _yMax;
        public RoomExit(ExitDirection direction, RoomData roomData)
        {
            this.direction = direction;            
            _roomData = roomData;
            center = new Vector2Int(_roomData.width/2, _roomData.height/2);
            Init();
        }

        void Init()
        {
            switch (direction)
            {
                case ExitDirection.LeftRight:
                    _xMin = 0;
                    _xMax = 0;
                    _yMin = Random.Range(2, (_roomData.height / 2) - _roomData.groundHeightMin);
                    _yMax = Random.Range(2, (_roomData.height / 2) - _roomData.ceilingHeightMin);
                    break;

                case ExitDirection.TopBottom:
                    _xMin = Random.Range(2, _roomData.width / 2 - _roomData.groundWidthMin);
                    _xMax = Random.Range(2, _roomData.width / 2 - _roomData.groundWidthMin);
                    _yMin = 0;
                    _yMax = 0;
                    break;
            }
        }
        public int xMin => _xMin;
        public int xMax => _xMax;
        public int yMin => _yMin;
        public int yMax => _yMax;
        public Vector2Int center { get; private set; }
    }
}
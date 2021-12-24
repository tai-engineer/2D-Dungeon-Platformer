using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    internal class RoomData
    {
        internal int width;
        internal int height;

        internal readonly int ceilingHeightMin;
        internal readonly int groundHeightMin;
        internal readonly int ceilingHeightMax;
        internal readonly int groundHeightMax;
        internal readonly int groundWidthMin;
        internal readonly int ceilingWidthMin;
        
        internal readonly int emptyHeight;
        internal RoomData(int width, int height)
        {
            this.width = width;
            this.height = height;

            ceilingHeightMin = 2;
            groundHeightMin = 2;
            ceilingHeightMax = 4;
            groundWidthMin = 2;
            ceilingWidthMin = 2;

            emptyHeight = this.height / 3;

            groundHeightMax = this.height - emptyHeight - ceilingHeightMax;
        }
    }
    internal enum ExitDirection { Left, Top, Right, Bottom }
    internal class RoomExit
    {
        ExitDirection _direction;
        RoomData _roomData;

        int _xMin = 0;
        int _xMax = 0;
        int _yMin = 0;
        int _yMax = 0;
        internal RoomExit(ExitDirection direction, RoomData roomData)
        {
            _direction = direction;            
            _roomData = roomData;

            Init();
        }

        void Init()
        {
            switch (_direction)
            {
                case ExitDirection.Left:
                    _xMin = 0;
                    _xMax = 0;
                    _yMin = Random.Range(2, -_roomData.height / 2 + _roomData.groundHeightMin);
                    _yMax = Random.Range(2, _roomData.height / 2 - _roomData.groundHeightMin);
                    break;

                case ExitDirection.Top:
                    _xMin = Random.Range(_roomData.ceilingWidthMin, _roomData.width / 2);
                    _xMax = Random.Range(_roomData.width / 2, _roomData.width - _roomData.ceilingWidthMin);
                    _yMin = _roomData.height;
                    _yMax = _roomData.height;
                    break;
                case ExitDirection.Right:
                    _xMin = _roomData.width;
                    _xMax = _roomData.width;
                    _yMin = Random.Range(2, -_roomData.height / 2 + _roomData.groundHeightMin);
                    _yMax = Random.Range(2, _roomData.height / 2 - _roomData.groundHeightMin);
                    break;
                case ExitDirection.Bottom:
                    _xMin = Random.Range(_roomData.groundWidthMin, _roomData.width / 2);
                    _xMax = Random.Range(_roomData.width / 2, _roomData.width - _roomData.groundWidthMin);
                    _yMin = 0;
                    _yMax = 0;
                    break;
            }
        }
        internal int xMin => _xMin;
        internal int xMax => _xMax;
        internal int yMin => _yMin;
        internal int yMax => _yMax;
    }
}
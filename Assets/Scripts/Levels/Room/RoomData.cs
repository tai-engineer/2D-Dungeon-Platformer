using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    internal class RoomData
    {
        int _width;
        int _height;

        internal readonly int ceilingHeightMin;
        internal readonly int groundHeightMin;
        internal readonly int verticalExitWidthMin;
        internal readonly int horizontalExitWidthMin;
        internal readonly int ceilingHeightMax;
        
        internal readonly int emptyHeight;
        internal readonly int groundHeightMax;
        internal readonly int verticalExitWidthMax;
        internal readonly int horizontalExitWidthMax;
        internal RoomData(int width, int height)
        {
            _width = width;
            _height = height;

            ceilingHeightMin = 2;
            groundHeightMin = 2;
            verticalExitWidthMin = 4;
            horizontalExitWidthMin = 4;
            ceilingHeightMax = 4;

            emptyHeight = _height / 3;

            groundHeightMax = _height - emptyHeight - ceilingHeightMax;

            verticalExitWidthMax = Mathf.RoundToInt(_width * 0.25f); // 25% of width
            horizontalExitWidthMax = Mathf.RoundToInt(_height * 0.33f); // 30% of height
        }
    }
    internal struct RoomExit
    {
        Room _room;
        Vector2Int _center;

        internal RoomExit(Vector2Int center, Room room)
        {
            _center = center;
            _room = room;
        }
        internal int xMin => Random.Range(0, _center.x - 1);
        internal int xMax => Random.Range(_center.x + 1, _room.width);
        internal int yMin => Random.Range(0, _center.y - 1);
        internal int yMax => Random.Range(_center.x + 1, _room.height);
    }
}
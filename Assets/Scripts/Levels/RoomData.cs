using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP2D
{
    public class RoomData
    {
        int _width;
        int _height;

        public readonly int ceilingHeightMin;
        public readonly int groundHeightMin;
        public readonly int verticalExitWidthMin;
        public readonly int horizontalExitWidthMin;
        public readonly int ceilingHeightMax;

        public readonly int emptyHeight;
        public readonly int groundHeightMax;
        public readonly int verticalExitWidthMax;
        public readonly int horizontalExitWidthMax;

        readonly Vector2Int[] _exitCoords;
        public RoomData(int width, int height)
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

            _exitCoords = new Vector2Int[]
            {
                new Vector2Int(0, Mathf.RoundToInt(_height * 0.5f)),
                new Vector2Int(Mathf.RoundToInt(width * 0.5f), height),
                new Vector2Int(width, Mathf.RoundToInt(height * 0.5f)),
                new Vector2Int(Mathf.RoundToInt(width * 0.5f), 0),
                new Vector2Int(0, 0)
            };
        }
    }
}
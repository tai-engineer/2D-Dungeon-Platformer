using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DP2D
{
    public class Room
    {
        
    }

    public static class RoomData
    {
        public const int width = 32;
        public const int height = 18;

        static Vector2[] _exitPositions = new Vector2[]
        {
            new Vector2(0, height / 2),
            new Vector2(width / 2, height),
            new Vector2(width, height / 2),
            new Vector2(width / 2, 0)
        };

        public const int emptyHeight = 6;
        public const int verticalExitWidth = 8;
        public const int horizontalExitWidth = 4;

        public const int ceilingHeightMax = 4;
        public const int ceilingHeightMin = 2;

        public const int groundHeightMin = 2;
        public const int groundHeightMax = height - emptyHeight - ceilingHeightMax;
        public static Vector2 GetExitPosition(ExitDirection direction)
        {
            return _exitPositions[(int)direction];
        }
    }
    public enum ExitDirection
    { 
        LEFT, TOP, RIGHT, BOTTOM
    }
}

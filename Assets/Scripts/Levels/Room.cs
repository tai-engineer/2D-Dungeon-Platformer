using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DP2D
{
    public class Room: MonoBehaviour
    {
        //RoomExit _leftExit;
        //RoomExit _rightExit;
        //RoomExit _topExit;
        //RoomExit _bottomExit;

        [SerializeField, Min(10)] int _width;
        [SerializeField, Min(10)] int _height;

        Tilemap _tileMap;
        RuleTile _tile;
        int _seed;
        int _scale;
        public int width { get => _width; }
        public int height { get => _height; }

        RoomData _data;
        void Awake()
        {
            _data = new RoomData(_width, _height);
        }

        public void Initialize(int seed, int scale)
        {
            _seed = seed;
            _scale = scale;
        }
        public void SetTile(RuleTile tile, Tilemap tileMap)
        {
            _tile = tile;
            _tileMap = tileMap;
        }
        public void Build(Vector2Int tileOffset)
        {
            int groundMaxHeight = 1;
            int ceilingMaxHeight = 1;
            int ceilingAllowHeight;

            for (int x = 0; x < _width; x++)
            {
                if ((x % 2) == 0)
                {
                    groundMaxHeight = GetPerlin1DHeight(
                        x, _seed,
                        _data.groundHeightMin, _data.groundHeightMax);
                }
                for (int y = 0; y < _height; y++)
                {
                    SetGroundTiles(x, y, groundMaxHeight, tileOffset);

                    ceilingAllowHeight = _height - groundMaxHeight - _data.emptyHeight;
                    if (y == ceilingAllowHeight && (x % 2) == 0)
                    {
                        ceilingMaxHeight = GetPerlin1DHeight(
                            _seed, y,
                            _data.ceilingHeightMin, _data.ceilingHeightMax);
                    }
                    SetCeilingTiles(x, y, ceilingMaxHeight, tileOffset);
                }
            }
        }
        void SetGroundTiles(int x, int y, int allowHeight, Vector2Int offset)
        {
            if (y < allowHeight)
            {
                _tileMap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), _tile);
            }
        }
        void SetCeilingTiles(int x, int y, int allowHeight, Vector2Int offset)
        {
            if (y >= (_height - allowHeight))
            {
                _tileMap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), _tile);
            }
        }
        int GetPerlin1DHeight(int x, int y, int min, int max)
        {
            float xCoord = (float)x / _width * _scale;
            float yCoord = (float)y / _height * _scale;
            float noise = Mathf.PerlinNoise(xCoord, yCoord);
            float yRange = noise * max;
            yRange = Mathf.Clamp(yRange, min, max);
            return Mathf.RoundToInt(yRange);
        }
    }
}

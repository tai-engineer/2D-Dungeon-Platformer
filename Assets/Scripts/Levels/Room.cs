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
        [SerializeField] int _seed;
        [SerializeField] int _scale;
        public int width { get => _width; }
        public int height { get => _height; }

        RoomData _data;
        int _size;
        void Awake()
        {
            _data = new RoomData(_width, _height);
            _size = _width * height;
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

            for (int x = 0; x < _width; x++)
            {
                groundMaxHeight = GetPerlin1DHeight(
                        x, _seed,
                        _data.groundHeightMin, _data.groundHeightMax);
                ceilingMaxHeight = GetPerlin1DHeight(
                            _seed, _width - x,
                            _data.ceilingHeightMin, _data.ceilingHeightMax);
                for (int y = 0; y < _height / 2; y++)
                {
                    SetGroundTiles(x, y, groundMaxHeight, tileOffset);
                    SetCeilingTiles(x, _height - y, ceilingMaxHeight, tileOffset);
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
            float xCoord = (float)x / _size * _scale;
            float yCoord = (float)y / _size * _scale;
            float noise = Mathf.PerlinNoise(xCoord, yCoord);
            float yRange = noise * max;
            yRange = Mathf.Clamp(yRange, min, max);
            return Mathf.RoundToInt(yRange);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DP2D
{
    public class Room: MonoBehaviour
    {
        [SerializeField, Min(10)] int _width;
        [SerializeField, Min(10)] int _height;

        Tilemap _tileMap;
        RuleTile _tile;
        [SerializeField] int _seed;
        [SerializeField] int _scale;
        internal int width { get => _width; }
        internal int height { get => _height; }

        RoomData _data;
        int _size;

        RoomExit _leftExit;
        int _groundMaxHeight = 1;
        int _ceilingMaxHeight = 1;
        void Awake()
        {
            _data = new RoomData(_width, _height);
            _size = _width * height;

            _seed = Random.Range(0, LevelGenerator.Instance.seed);
            _scale = Random.Range(1, LevelGenerator.Instance.scale);

            _leftExit = new RoomExit(new Vector2Int(0, _height/2), this);
        }
        internal void SetTile(RuleTile tile, Tilemap tileMap)
        {
            _tile = tile;
            _tileMap = tileMap;
        }
        internal void Build(Vector2Int tileOffset)
        {

            for (int x = 0; x < _width; x++)
            {
                _groundMaxHeight = GetPerlin1DHeight(
                        x, _seed,
                        _data.groundHeightMin, _data.groundHeightMax);
                _ceilingMaxHeight = GetPerlin1DHeight(
                            _seed, _width - x,
                            _data.ceilingHeightMin, _data.ceilingHeightMax);
                
                for (int y = 0; y < _height / 2; y++)
                {
                    SetGroundTiles(x, y, tileOffset);
                    SetCeilingTiles(x, _height - y - 1, tileOffset);
                }
            }
        }
        void SetGroundTiles(int x, int y, Vector2Int offset)
        {
            if (x == 0 || x == _width - 1)
            {
                _groundMaxHeight = _height / 2;
            }
            if (y < _groundMaxHeight)
            {
                _tileMap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), _tile);
            }
        }
        void SetCeilingTiles(int x, int y, Vector2Int offset)
        {
            if (x == 0 || x == _width - 1)
            {
                _ceilingMaxHeight = _height - _groundMaxHeight;
            }
            if (y >= (_height - _ceilingMaxHeight))
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

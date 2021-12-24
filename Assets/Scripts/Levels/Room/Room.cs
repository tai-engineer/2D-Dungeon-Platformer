using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DP2D
{
    public class Room: MonoBehaviour
    {
        internal int width;
        internal int height;
        internal RoomData data;

        Tilemap _tileMap;
        RuleTile _tile;

        int _seed;
        int _scale;
        int _size;

        int _groundMaxHeight = 1;
        int _ceilingMaxHeight = 1;
        RoomExit _leftExit = null;
        RoomExit _topExit = null;
        RoomExit _rightExit = null;
        RoomExit _bottomExit = null;
        internal void SetTile(RuleTile tile, Tilemap tileMap)
        {
            _tile = tile;
            _tileMap = tileMap;
        }
        internal void Build(Vector2Int tileOffset)
        {
            _seed = Random.Range(0, LevelGenerator.Instance.seed);
            _scale = Random.Range(1, LevelGenerator.Instance.scale);
            _leftExit = new RoomExit(ExitDirection.Left, data);
            _rightExit = new RoomExit(ExitDirection.Right, data);
            for (int x = 0; x < width; x++)
            {
                _groundMaxHeight = GetPerlin1DHeight(
                        x, _seed,
                        data.groundHeightMin, data.groundHeightMax);
                _ceilingMaxHeight = GetPerlin1DHeight(
                            _seed, width - x,
                            data.ceilingHeightMin, data.ceilingHeightMax);
                
                for (int y = 0; y < height / 2; y++)
                {
                    SetGroundTiles(x, y, tileOffset);
                    SetCeilingTiles(x, height - y - 1, tileOffset);
                }
            }
        }
        void SetGroundTiles(int x, int y, Vector2Int offset)
        {
            if (x == 0 || x == width - 1)
            {
                if (x == 0 && _leftExit != null)
                    _groundMaxHeight = height / 2 + _leftExit.yMin;
                else if(x == width-1 && _rightExit != null)
                    _groundMaxHeight = height / 2 + _rightExit.yMin;
                else
                    _groundMaxHeight = height / 2;
            }
            if (y < _groundMaxHeight)
            {
                _tileMap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), _tile);
            }
        }
        void SetCeilingTiles(int x, int y, Vector2Int offset)
        {
            if (x == 0 || x == width - 1)
            {
                if (x == 0 && _leftExit != null)
                    _ceilingMaxHeight = height / 2 - _leftExit.yMax;
                else if (x == width - 1 && _rightExit != null)
                    _ceilingMaxHeight = height / 2 - _rightExit.yMax;
                else
                    _ceilingMaxHeight = height / 2;

            }
            if (y >= height - _ceilingMaxHeight)
            {
                _tileMap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), _tile);
            }
        }
        int GetPerlin1DHeight(int x, int y, int min, int max)
        {
            float xCoord = (float)x / width * height * _scale;
            float yCoord = (float)y / width * height * _scale;
            float noise = Mathf.PerlinNoise(xCoord, yCoord);
            float yRange = noise * max;
            yRange = Mathf.Clamp(yRange, min, max);
            return Mathf.RoundToInt(yRange);
        }
    }
}

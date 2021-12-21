using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace DP2D
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] RuleTile _tile;
        [SerializeField] Tilemap _tileMap;
        [SerializeField] int _scale;
        [SerializeField] int _seed;
        int _roomWidth, _roomHeight;
        void Awake()
        {
            _roomWidth = RoomData.width;
            _roomHeight = RoomData.height;
        }

        void Update()
        {
            _tileMap.ClearAllTiles();
            GeneratateLevel();
        }
        void GeneratateLevel()
        {
            SetTiles();
        }

        float GetPerlin1DHeight(int x, int y, int min, int max)
        {
            float xCoord = (float)x / _roomWidth * _scale;
            float yCoord = (float)y / _roomHeight * _scale;
            float noise = Mathf.PerlinNoise(xCoord, yCoord);
            float yRange = noise * max;
            yRange = Mathf.Clamp(yRange, min, max);
            return Mathf.RoundToInt(yRange);
        }

        void SetTiles()
        {
            int groundMaxHeight = 0;
            int ceilingMaxHeight = 0;
            int ceilingAllowHeight;
            for (int x = 0; x < _roomWidth;x++)
            {
                if ((x % 2) == 0)
                {
                    groundMaxHeight = (int)GetPerlin1DHeight(x, _seed, RoomData.groundHeightMin, RoomData.groundHeightMax);
                }
                for (int y = 0; y < _roomHeight;y++)
                {
                    SetGroundTiles(x, y, groundMaxHeight);

                    ceilingAllowHeight = _roomHeight - groundMaxHeight - RoomData.emptyHeight;
                    if (ceilingAllowHeight <= 0)
                        Debug.LogError("Invalid value");
                    if (y == ceilingAllowHeight && (x % 2) == 0)
                    {
                        ceilingMaxHeight = (int)GetPerlin1DHeight(_seed, y, RoomData.ceilingHeightMin, RoomData.ceilingHeightMax);
                    }
                    SetCeilingTiles(x, y, ceilingMaxHeight);
                }
            }
        }
        void SetGroundTiles(int x, int y, int height)
        {
            if (y < height)
            {
                _tileMap.SetTile(new Vector3Int(x, y, 0), _tile);
            }
        }
        void SetCeilingTiles(int x, int y, int height)
        {
            if (y >= (_roomHeight - height))
            {
                _tileMap.SetTile(new Vector3Int(x, y, 1), _tile);
            }
        }
    }
}

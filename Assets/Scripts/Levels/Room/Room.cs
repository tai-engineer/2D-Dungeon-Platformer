using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace DP2D
{
    [SelectionBase]
    public class Room: MonoBehaviour
    {
        public int width;
        public int height;
        public RoomData data;
        public Vector2Int offset;

        public Tilemap tileMap;
        public RuleTile tile;

        int _seed;
        int _scale;

        int _groundMaxHeight = 1;
        int _ceilingMaxHeight = 1;

        [SerializeField] RoomExit _leftExit;
        [SerializeField] RoomExit _rightExit;
        [SerializeField] RoomExit _topExit;
        [SerializeField] RoomExit _bottomExit;
        public RoomExit leftExit { get => _leftExit; set => _leftExit = value; }
        public RoomExit rightExit { get => _rightExit; set => _rightExit = value; }
        public RoomExit topExit { get => _topExit; set => _topExit = value; }
        public RoomExit bottomExit { get => _bottomExit; set => _bottomExit = value; }

        public void Build()
        {
            _seed = Random.Range(0, LevelGenerator.Instance.perlingOffset);
            _scale = Random.Range(0, LevelGenerator.Instance.perlingScale);

            for (int x = 0; x < width; x++)
            {
                _groundMaxHeight = GetPerlinHeight(
                        x, 0,
                        data.groundHeightMin, data.groundHeightMax);
                _ceilingMaxHeight = GetPerlinHeight(
                            x, 1,
                            data.ceilingHeightMin, data.ceilingHeightMax);
                
                for (int y = 0; y < height; y++)
                {
                    SetGroundTiles(x, y, offset);
                    SetCeilingTiles(x, y, offset);
                    SetLRExitTiles(x, y,offset);
                    SetTBExitTiles(x, y, offset);
                }
            }
        }
        void SetGroundTiles(int x, int y, Vector2Int offset)
        {
            if (x == 0 || x == width - 1)
            {
                _groundMaxHeight = height / 2;
            }
            if (y < _groundMaxHeight)
            {
                tileMap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), tile);
            }
        }
        void SetCeilingTiles(int x, int y, Vector2Int offset)
        {
            if (x == 0 || x == width - 1)
            {
                _ceilingMaxHeight = height / 2;
            }
            if (y >= height - 1 - _ceilingMaxHeight)
            {
                tileMap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), tile);
            }
        }
        void SetLRExitTiles(int x, int y, Vector2Int offset)
        {
            if (y <= data.groundHeightMin)
                return;
            if(x == 0)
            {
                if(y >= (_leftExit.center.y - _leftExit.yMin) && y <= (_leftExit.center.y + _leftExit.yMax))
                {
                    tileMap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), null);
                }
            }
            else if(x == width - 1)
            {
                if (y >= (_rightExit.center.y - _rightExit.yMin) && y <= (_rightExit.center.y + _rightExit.yMax))
                {
                    tileMap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), null);
                }
            }
        }
        void SetTBExitTiles(int x, int y, Vector2Int offset)
        {
            if (y < _groundMaxHeight)
            {
                if (_bottomExit.xMin == 0 && _bottomExit.xMax == 0)
                    return;
                if (x < (_bottomExit.center.x - _bottomExit.xMin) || x > (_bottomExit.center.x + _bottomExit.xMax))
                    return; 
            }
            else if (y > height - 1 - _ceilingMaxHeight)
            {
                if (_topExit.xMin == 0 && _topExit.xMax == 0)
                    return;
                if (x < (_topExit.center.x - _topExit.xMin) || x > (_topExit.center.x + _topExit.xMax))
                    return;
            }

            tileMap.SetTile(new Vector3Int(x + offset.x, y + offset.y, 0), null);
        }
        int GetPerlinHeight(int x, int y, int min, int max)
        {
            float xCoord = ((float)x / width) * _scale + _seed;
            float yCoord = ((float)y / height) * _scale + _seed;
            float noise = Mathf.PerlinNoise(xCoord, yCoord);
            float yRange = noise * max;
            yRange = Mathf.Clamp(yRange, min, max);
            return Mathf.RoundToInt(yRange);
        }
    }
}

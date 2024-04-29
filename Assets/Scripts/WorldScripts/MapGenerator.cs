using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gokboerue.Gameplay
{
	public class MapGenerator : MonoBehaviour
	{
		public Tilemap tilemap;
		public TileBase tile;

		[Tooltip("The width of each layer of the stack")]
		public int width;
		[Tooltip("The height of each layer of the stack")]
		public int height;

		[SerializeField]
		public List<MapSettings> mapSettings = new List<MapSettings>();

		List<int[,]> mapList = new List<int[,]>();


		public void GenerateMap()
		{
			ClearMap();
			mapList = new List<int[,]>();
			for (int i = 0; i < mapSettings.Count; i++)
			{
				int[,] map = new int[width, height];
                float seed;
                if (mapSettings[i].randomSeed)
                {
                    seed = Random.Range(0, 1000000);
                }
                else
                {
                    seed = mapSettings[i].seed;
                }

                switch (mapSettings[i].algorithm)
                {
                    case Algorithm.Perlin:
                        map = MapFunctions.GenerateArray(width, height, true);
                        map = MapFunctions.PerlinNoise(map, seed);
                        break;
                    case Algorithm.PerlinSmoothed:
                        map = MapFunctions.GenerateArray(width, height, true);
                        map = MapFunctions.PerlinNoiseSmooth(map, seed, mapSettings[i].interval);
                        break;
                    case Algorithm.PerlinCave:
                        map = MapFunctions.GenerateArray(width, height, true);
                        map = MapFunctions.PerlinNoiseCave(map, mapSettings[i].modifier, mapSettings[i].edgesAreWalls);
                        break;
                    case Algorithm.RandomWalkTop:
                        map = MapFunctions.GenerateArray(width, height, true);
                        map = MapFunctions.RandomWalkTop(map, seed);
                        break;
                    case Algorithm.RandomWalkTopSmoothed:
                        map = MapFunctions.GenerateArray(width, height, true);
                        map = MapFunctions.RandomWalkTopSmoothed(map, seed, mapSettings[i].interval);
                        break;
                    case Algorithm.RandomWalkCave:
                        map = MapFunctions.GenerateArray(width, height, false);
                        map = MapFunctions.RandomWalkCave(map, seed, mapSettings[i].clearAmount);
                        break;
                    case Algorithm.RandomWalkCaveCustom:
                        map = MapFunctions.GenerateArray(width, height, false);
                        map = MapFunctions.RandomWalkCaveCustom(map, seed, mapSettings[i].clearAmount);
                        break;
                    case Algorithm.CellularAutomataVonNeuman:
                        map = MapFunctions.GenerateCellularAutomata(width, height, seed, mapSettings[i].fillAmount, mapSettings[i].edgesAreWalls);
                        map = MapFunctions.SmoothVNCellularAutomata(map, mapSettings[i].edgesAreWalls, mapSettings[i].smoothAmount);
                        break;
                    case Algorithm.CellularAutomataMoore:
                        map = MapFunctions.GenerateCellularAutomata(width, height, seed, mapSettings[i].fillAmount, mapSettings[i].edgesAreWalls);
                        map = MapFunctions.SmoothMooreCellularAutomata(map, mapSettings[i].edgesAreWalls, mapSettings[i].smoothAmount);
                        break;
                    case Algorithm.DirectionalTunnel:
                        map = MapFunctions.GenerateArray(width, height, false);
                        map = MapFunctions.DirectionalTunnel(map, mapSettings[i].minPathWidth, mapSettings[i].maxPathWidth, mapSettings[i].maxPathChange, mapSettings[i].roughness, mapSettings[i].windyness);
                        break;
                }
                mapList.Add(map);
            }
            Vector2Int offset = new Vector2Int(-width / 2, (-height / 2) - 1);
			foreach (int[,] map in mapList)
			{
				MapFunctions.RenderMapWithOffset(map, tilemap, tile, offset);
				offset.y += -height + 1;
			}
        }

        public void ClearMap()
		{
			tilemap.ClearAllTiles();
		}
    }
}
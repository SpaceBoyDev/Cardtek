using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : AbstractatDungeonGenerator
{

   [SerializeField]
   protected SimpleRandomWalkConf _simpleRandomWalkConf;


   [Button]
   protected override void RunProceduralGeneration()
   {
      HashSet<Vector2Int> floorPositions = RunRandomWalk(_simpleRandomWalkConf,startPosition);
      _tilemapVisualizer.Clear();
      _tilemapVisualizer.PaintFloorTiles(floorPositions);
      WallGenerator.CreateWalls(floorPositions,_tilemapVisualizer);
   }

   protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkConf _simpleRandomWalkConf,Vector2Int position)
   {
      Vector2Int currentPosition = position;

      HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();

      for (int i = 0; i < _simpleRandomWalkConf.Iterations; i++)
      {
         var path = ProceduralGenerationAlgorithim.simpleRandomWalk(currentPosition,_simpleRandomWalkConf.WalkLength);
         floorPosition.UnionWith(path);
         if (_simpleRandomWalkConf.StartRandomlyEachIteration)
         {
            currentPosition = floorPosition.ElementAt(Random.Range(0, floorPosition.Count));
         }
      }

      return floorPosition;
   }
}

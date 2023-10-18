using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
   public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
   {
      HashSet<Vector2Int> basicWallPosition = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionList);

      foreach (var position in basicWallPosition)
      {
         tilemapVisualizer.PaintSingleBasicWall(position);
      }
   }

   private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
   {
      HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

      foreach (var varPosition in floorPositions)
      {

         foreach (var direction in directionList)
         {
            Vector2Int neighbourPosition = varPosition + direction;
            if (!floorPositions.Contains(neighbourPosition))
            {
               wallPositions.Add(neighbourPosition);
            }
         }
         
      }

      return wallPositions;
   }
}

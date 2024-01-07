using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int corridorLenght = 14, corridorCount = 5;

    [SerializeField]
    [Range(0.1f,1)]
    private float roomPercent = 0.8f;

    
    protected override void RunProceduralGeneration()
    {
        _tilemapVisualizer.Clear();
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        CreateCorridors(floorPositions,potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);
        
        roomPositions.UnionWith( CreateRoomsAtDeadEnd(deadEnds));
        
        floorPositions.UnionWith(roomPositions);

        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions,_tilemapVisualizer);
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighbourCount = 0;

            foreach (var direction in Direction2D.cardinalDirectionList)
            {
                if (floorPositions.Contains(direction + position))
                {
                    neighbourCount++;
                }
            }
            
            if (neighbourCount == 1)
            {
                deadEnds.Add(position);
            }

        }

        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        List<Vector2Int> roomToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomToCreate)
        {
            HashSet<Vector2Int> roomfloors = RunRandomWalk(_simpleRandomWalkConf,roomPosition);
            roomPositions.UnionWith(roomfloors);
        }

        return roomPositions;
    }
    
    private HashSet<Vector2Int> CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        foreach (var roomPosition in deadEnds)
        {
            HashSet<Vector2Int> roomfloors = RunRandomWalk(_simpleRandomWalkConf,roomPosition);
            roomPositions.UnionWith(roomfloors);
        }

        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPositions,HashSet<Vector2Int> potentialRoomPositions)
    {
        Vector2Int currentPosition = startPosition;
        potentialRoomPositions.Add(currentPosition);
        for (int i = 0; i < corridorCount; i++)
        {
            List<Vector2Int> corridor = ProceduralGenerationAlgorithim.randomWalkCorridor(currentPosition, corridorCount);
            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }
    }
}

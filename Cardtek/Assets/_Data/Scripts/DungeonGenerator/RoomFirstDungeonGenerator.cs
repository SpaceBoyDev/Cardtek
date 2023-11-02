using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;

    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;

    [SerializeField] 
    [Range(0, 10)] 
    private int offset = 1;

    [SerializeField]
    private bool privateWalkRooms = false;
    
    [SerializeField]
    private bool personalizedWalkRooms = false;
    
    List<Vector2Int> roomCenters = new List<Vector2Int>();

    [OdinSerialize] 
    private List<(Rooms, float)> personalizedRooms = new List<(Rooms, float)>();
    
    protected override void RunProceduralGeneration()
    {
        _tilemapVisualizer.Clear();
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomList = ProceduralGenerationAlgorithim.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition,new Vector3Int(dungeonWidth,dungeonHeight,0)),minRoomWidth,minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        
        
        foreach (var room in roomList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        

        if (privateWalkRooms)
        {
            floor = CreateRandomRooms(roomList);
        }
        else if(personalizedWalkRooms)
        {
            floor = CreatePersonalizedRooms(roomList);
        }
        else
        {
            floor = CreateSimpleRooms(roomList);
        }

      

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        
        floor.UnionWith(corridors);
        
        _tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor,_tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreatePersonalizedRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        for (int i = 0; i < roomList.Count; i++)
        {
            
            int randomNumber = Random.Range(0, personalizedRooms.Count);
            
            var roomBounds = personalizedRooms[randomNumber].Item1.floorTilemap.cellBounds;

            var tilemap = personalizedRooms[randomNumber].Item1.floorTilemap;

            // print("Original"+roomList[i].size);
            // print("Center"+roomBounds.size);
            
            foreach (Vector3Int c in roomBounds.allPositionsWithin)
            {
                    
                    
                    //Add it to the List if the local pos exist in the Tile map
                    if (tilemap.HasTile(c))
                    {
                        
                        floor.Add(new Vector2Int(roomCenters[i].x-c.x ,roomCenters[i].y-c.x));
            
                    }

            }
            
            
        }
      
     
        
        return floor;
    }

    private HashSet<Vector2Int> CreateRandomRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        for (int i = 0; i < roomList.Count; i++)
        {
            BoundsInt roomBounds = roomList[i];
            Vector2Int roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x),
                Mathf.RoundToInt(roomBounds.center.y));
            HashSet<Vector2Int> floorRoom = RunRandomWalk(_simpleRandomWalkConf, roomCenter);

            foreach (var floorPosition in floorRoom)
            {
                if (floorPosition.x >= (roomBounds.xMin + offset) && floorPosition.x <= (roomBounds.xMax - offset) &&
                    floorPosition.y >= (roomBounds.yMin - offset) && floorPosition.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(floorPosition);
                }
            }

        }

        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();

        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosesPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);

        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if(destination.y < position.y)
            {
                position += Vector2Int.down;
            }

            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if(destination.x < position.x)
            {
                position += Vector2Int.left;
            }

            corridor.Add(position);
        }

        return corridor;
    }

    private Vector2Int FindClosesPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2Int.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }

        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var room in roomList)
        {

            for (int col = offset; col < room.size.x - offset ; col++)
            {
                for (int row = offset; row < room.size.y - offset ; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }

            }
            
        }

        return floor;
    }
}

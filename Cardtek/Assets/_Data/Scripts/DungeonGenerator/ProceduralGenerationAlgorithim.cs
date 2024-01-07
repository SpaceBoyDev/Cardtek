using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProceduralGenerationAlgorithim : MonoBehaviour
{

    public static HashSet<Vector2Int> simpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add((startPosition));

        var previousposition = startPosition;

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousposition + Direction2D.randomCardinalDirection();

            path.Add(newPosition);

            previousposition = newPosition;
            
        }

        return path;
    }

    public static List<Vector2Int> randomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        Vector2Int direction = Direction2D.randomCardinalDirection();

        Vector2Int currentPosition = startPosition;
        corridor.Add(currentPosition);
        
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit,int minWidth,int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count() > 0)
        {
            BoundsInt room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5)
                {
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally( minHeight, roomsQueue, room);
                        
                    }else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                        
                    }else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                        
                    }
                    else if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally( minHeight, roomsQueue, room);
                        
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                
            }
        }
    
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        int XSplit = Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(XSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x+XSplit, room.min.y, room.min.z),new Vector3Int(room.size.x - XSplit, room.size.y, room.size.z));
        
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally( int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        int YSplit = Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, YSplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y+YSplit, room.min.z),new Vector3Int(room.size.x, room.size.y-YSplit, room.size.z));
        
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 0), //Right
        new Vector2Int(0, -1), //Down
        new Vector2Int(-1, 0) //Left
    };

    public static Vector2Int randomCardinalDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)];
    }
}

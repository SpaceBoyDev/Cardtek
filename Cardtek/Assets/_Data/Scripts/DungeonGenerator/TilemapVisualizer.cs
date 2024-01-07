using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap,wallTilemap;
    [SerializeField]
    private TileBase floorTile,wallTop;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions,floorTilemap,floorTile);
       
    }
    
    public void PaintRooms(IEnumerable<Vector2Int> floorPositions)
    {
        
        PaintTiles(floorPositions,floorTilemap,floorTile);
       
    }
    
    public void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position  in positions )
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        Vector3Int tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition,tile);
    }

    [Button]
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}

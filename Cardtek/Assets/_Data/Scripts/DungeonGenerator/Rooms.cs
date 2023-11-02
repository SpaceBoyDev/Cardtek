using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Rooms : MonoBehaviour
{
    [SerializeField]
    public Tilemap floorTilemap;
    
    [SerializeField]
    public TilemapRenderer TilemapRenderer;
    
    [SerializeField]
    private Grid grids;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    void prints()
    {
        BoundsInt bounds = floorTilemap.cellBounds;
        TileBase[] allTiles = floorTilemap.GetTilesBlock(bounds);
        int count = 0;
        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null) {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name +" NUM:"+count);
                    count++;
                } else {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
            }
        }        
       
       
    }
}

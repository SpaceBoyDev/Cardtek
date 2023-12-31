using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractatDungeonGenerator : MonoBehaviour
{
    [SerializeField] 
    protected TilemapVisualizer _tilemapVisualizer;

    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        _tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkConf", menuName = "BLT/SimpleRandomWalkConf")]
public class SimpleRandomWalkConf : ScriptableObject
{
    [Header("Generation")]
    [SerializeField]
    private int iterations = 10;
    public int Iterations {
        get {
            return iterations;
        }
    }
    
    [SerializeField]
    private int walkLength = 10;
    public int WalkLength {
        get {
            return walkLength;
        }
    }
    
    [SerializeField]
    private bool startRandomlyEachIteration = true;
    public bool StartRandomlyEachIteration {
        get {
            return startRandomlyEachIteration;
        }
    }
 
}

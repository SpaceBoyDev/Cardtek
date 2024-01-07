using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "BLT/PlayerConfiguration")]
public class PlayerConfiguration : ScriptableObject {
    
    [Header("Movement")]
    [SerializeField]
    private float playerSpeed = 3f;
    public float PlayerSpeed {
        get {
            return playerSpeed;
        }
    }
   
}

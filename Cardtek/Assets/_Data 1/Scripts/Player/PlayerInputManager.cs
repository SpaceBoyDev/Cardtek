using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    
    //Clase Singleton to control all the player inputs

    public static PlayerInputManager Instance;

    private Player playerInput;

    [Header("Debug")]
    private bool isInputAllowed = true;
    
    //Input actions
    private const string VERTICAL_MOVEMENT = "VerticalMovement";
    private const string HORIZONTAL_MOVEMENT = "HorizontalMovement";
    private const string SHOOT = "Shoot";
    private const string CHANGE_GUN_AXIS = "ChangeGun";
    private const string TIME_SPEED_UP = "AugmentTimeScale";
    private const string TIME_SPEED_DOWN = "ReduceTimeScale";

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this);
        }

    }

    private void Start()
    {
        playerInput = ReInput.players.GetPlayer(0);
    }

    public float GetVerticalMovement()
    {
        return playerInput.GetAxis(VERTICAL_MOVEMENT);
    }
    public float GetHorizontalMovement()
    {
        return playerInput.GetAxis(HORIZONTAL_MOVEMENT);
    }
    
    public float GetChangeGunAxis()
    {
        return playerInput.GetAxis(CHANGE_GUN_AXIS);
    }
    
    public bool GetShoot()
    {
        return playerInput.GetButtonDown(SHOOT);
    }
    
    public bool GetSpeedDown()
    {
        return playerInput.GetButton(TIME_SPEED_DOWN);
    }
    
    
    public bool GetSpeedUp()
    {
        return playerInput.GetButton(TIME_SPEED_UP);
    }
  
    public void changeInputAllowed(bool newState)
    {
            isInputAllowed = newState;
    }
}

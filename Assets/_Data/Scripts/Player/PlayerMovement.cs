using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float PlayerSpeed;

    private float VerticalAxis;

    private float HorizontalAxis;

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        VerticalAxis = PlayerInputManager.Instance.GetVerticalMovement();
       
        HorizontalAxis = PlayerInputManager.Instance.GetHorizontalMovement();


    }

    private void FixedUpdate()
    {
        MovePlayer();
 
    }

    private void MovePlayer()
    {

        Vector3 HorizontalMovement = new Vector3(HorizontalAxis * PlayerSpeed * Time.fixedDeltaTime, 0f);
        Vector3 VerticalMovement = new Vector3(0f, VerticalAxis * PlayerSpeed * Time.fixedDeltaTime);

        Vector3 combinedMovement = HorizontalMovement + VerticalMovement;
        
        combinedMovement = Mathf.Clamp01(combinedMovement.sqrMagnitude) * combinedMovement.normalized;

        Vector3 moveDirection = new Vector3(combinedMovement.x, combinedMovement.y);

        moveDirection = transform.position + moveDirection;
        
        transform.position = moveDirection;

    }
  
    
    
}

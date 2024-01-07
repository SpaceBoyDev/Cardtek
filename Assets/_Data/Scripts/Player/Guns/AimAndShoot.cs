using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{

    [SerializeField] private Transform gunPoint;
    
    private Transform tr;

    private void Start()
    {
        tr = GetComponent<Transform>();
        
        Debug.Log(tr.rotation);

    }

    private void FixedUpdate()
    {
        Rotate();
    }
    private void Update()
    {
        if (PlayerInputManager.Instance.GetShoot())
        {
            Shoot();
        }
    }

    private void Rotate()
    {

        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(tr.position);

        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;

        float angle = Mathf.Atan2(mousePos.y,mousePos.x) * Mathf.Rad2Deg;
        tr.rotation = Quaternion.Euler(new Vector3(0,0,angle-90));

    }

    private void Shoot()
    {
        
    }
}

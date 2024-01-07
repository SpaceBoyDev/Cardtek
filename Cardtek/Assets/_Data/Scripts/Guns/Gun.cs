using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{

    [SerializeField] private string Gun_Name;
    [SerializeField] private int actual_bullets;
    [SerializeField] private int total_bullets;
    [SerializeField] private int magazine_size;
    protected bool clickButton;

    public Gun()
    {
    }

    public Gun(string gunName, int actualBullets, int totalBullets, int magazineSize)
    {
        Gun_Name = gunName;
        actual_bullets = actualBullets;
        total_bullets = totalBullets;
        magazine_size = magazineSize;
    }

    private string GunName
    {
        get => Gun_Name;
        set => Gun_Name = value;
    }

    private int ActualBullets
    {
        get => actual_bullets;
        set => actual_bullets = value;
    }

    private int TotalBullets
    {
        get => total_bullets;
        set => total_bullets = value;
    }

    private int MagazineSize
    {
        get => magazine_size;
        set => magazine_size = value;
    }

    protected abstract void Shoot();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
        clickButton =  PlayerInputManager.Instance.GetShoot();
    }
}

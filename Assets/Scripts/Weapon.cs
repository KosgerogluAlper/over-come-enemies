using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Attack attack;
    [SerializeField] private Transform fireTransform;
    [SerializeField] private float fireRate;
    [SerializeField] private int maxammocount;
    [SerializeField] private AudioClip clip;

 
    private int currentAmmoCount;
    public int GetCurrentWeaponAmmoCount
    {
        get { return currentAmmoCount; }
        set { currentAmmoCount = value; }
    }
    public void Awake()
    {
        currentAmmoCount = maxammocount;
    }
    private void OnEnable()
    {
        if (attack != null)
        {




            attack.GetFireTransform = fireTransform;
            attack.MaxAmmoCount = maxammocount;
            attack.GetFireRate = fireRate;
            attack.GetAmmo = currentAmmoCount;
            attack.GetClipToPlay = clip;
        }
    }
}

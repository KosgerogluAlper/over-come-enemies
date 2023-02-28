using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;


public class Attack : MonoBehaviour
{
    [SerializeField] GameObject ExplosionEffect;
    [SerializeField] GameObject ammo; 
    [SerializeField] GameObject[] weapons;
    private int maxAmmoCount = 5;
    public bool isPlayer = false; 
    private int ammoCount = 0;   
    private float currentFireRate = 0f; 
    private  Transform firePosition; 
    float fireRate = 0.5f; 
    private AudioClip cliptoPlay;
    private AudioSource audioSource;
    GameManangement gameManangement;
   

    #region properties 
    public AudioClip GetClipToPlay
    {
        get { return cliptoPlay; }
        set { cliptoPlay = value; }
    }
    public int MaxAmmoCount
    {
        get { return maxAmmoCount; }
        set { maxAmmoCount = value; }
    }
    public float GetCurrentFireRate
    {
        get
        {
            return currentFireRate;
        }
        set
        {
            currentFireRate = value;
        }
    }
    public int GetAmmo
    {
        get
        {
            return ammoCount;
        }
        set
        {
            ammoCount = value;
            if (ammoCount > maxAmmoCount)
            {
                ammoCount = maxAmmoCount;
            }
        }
    }
    public float GetFireRate
    {
        get
        {
            return fireRate;
        }
        set
        {
            fireRate = value;
        }
    }
    public Transform GetFireTransform
    {
        get
        {
            return firePosition;
        }
        set
        {
            firePosition = value;
        }
    }
    #endregion

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameManangement = FindObjectOfType<GameManangement>();
    }
    void Update()
    {
        standby();
        Player›nput();
    }
    private void standby()
    {
        if (currentFireRate > 0f)
        {
            currentFireRate -= Time.deltaTime;
        }
    }

    private void Player›nput()
    {
        if (isPlayer)
        {
            switch (Input.inputString)
            {
                case "1":
                    {
                        gameManangement.guncontrol = true;
                        weapons[1].gameObject.GetComponent<Weapon>().GetCurrentWeaponAmmoCount = ammoCount;
                        weapons[0].gameObject.SetActive(true);
                        weapons[1].gameObject.SetActive(false);
                        break;
                    }
                case "2":
                    {
                        gameManangement.guncontrol = false;
                        weapons[0].gameObject.GetComponent<Weapon>().GetCurrentWeaponAmmoCount = ammoCount;
                        weapons[0].gameObject.SetActive(false);
                        weapons[1].gameObject.SetActive(true);
                        break;
                    }
            }
                ClickFire();
        }
    }
    private void ClickFire()
    {
        if (Input.GetMouseButtonDown(0) && currentFireRate <= 0)
        {
            if (ammoCount >= 0)
            {
             if(gameManangement.levelFinish==false)
                Fire();
            }
        }
    }
    public void Fire()
    {
        float targetRotation = 0f;
        float difference = 180f - transform.eulerAngles.y;
        Vector3 a = new Vector3(firePosition.position.x, firePosition.position.y, 1.4f);
        Vector3 b = new Vector3(firePosition.position.x, firePosition.position.y, -1f);
        GameObject bulletClone;
        if (difference <= 90)
        {
            targetRotation = 180f;
            bulletClone = Instantiate(ammo, a, Quaternion.Euler(0f, targetRotation, 0f));
            Instantiate(ExplosionEffect, firePosition.position, Quaternion.identity);
        }
        else if (difference > 90)
        {
            targetRotation = 0f; 
            bulletClone = Instantiate(ammo, b, Quaternion.Euler(0f, targetRotation, 0f));
            Instantiate(ExplosionEffect, firePosition.position, Quaternion.identity);
        }
        currentFireRate = fireRate;
        ammoCount--;
        audioSource.PlayOneShot(cliptoPlay);
    }


    public void FireEnemy()
    {
        float targetRotation = 180f;
        float difference = (180f - transform.eulerAngles.y)+2;
        if (difference <= 90)
        {
            targetRotation = 0f;
        }
        else if (difference > 90)
        {
            targetRotation = 180f;
        }
       Vector3 a = new Vector3(firePosition.position.x, firePosition.position.y, -1f);
        currentFireRate = fireRate;
        GameObject bulletClone = Instantiate(ammo, a, Quaternion.Euler(0f, 0f, targetRotation));
        Instantiate(ExplosionEffect, firePosition.position, Quaternion.identity);
        ammoCount--;
        audioSource.PlayOneShot(cliptoPlay);
    }
}

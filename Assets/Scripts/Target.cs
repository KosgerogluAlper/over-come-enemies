using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject gamefinish;
    [SerializeField] private int maxHealt = 2;
    [SerializeField] private GameObject hitFix;
    [SerializeField] private GameObject DeadFx;
    [SerializeField] private AudioClip cliptoPLay;
    private AudioSource audioSource;
    public int currentHealt;
    #region properties 
    public int GetHealt
    {
        get
        {
            return currentHealt;
        }
        set
        {
            currentHealt = value;
            if (currentHealt > maxHealt)
            {
                currentHealt = maxHealt;
            }
        }
    }
    public int MaxHealt
    {
        get
        {
            return maxHealt;
        }
    }
    #endregion

    public void Awake()
    {
        currentHealt = maxHealt;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>() == true)
        {
            currentHealt--;
            if (hitFix != null && currentHealt > 0)
            { 
                Instantiate(hitFix, transform.position, Quaternion.identity);
            }
            if (currentHealt <= 0)
            { 
                Die();
            }
            Destroy(other.gameObject);
        }
    }

    private void Die()
    { 
            AudioSource.PlayClipAtPoint(cliptoPLay, transform.position);
            if (DeadFx != null)
            {
                Instantiate(DeadFx, transform.position, Quaternion.identity);
            }
        Destroy(gameObject);
    }
}

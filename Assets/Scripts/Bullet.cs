using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    private void FixedUpdate()
    {
        transform.position += -transform.right * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<Target>() == false)
        {
            Destroy(gameObject);
        }
    }
}

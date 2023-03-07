using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPowerC : MonoBehaviour
{
    [SerializeField]  private AudioClip cliptoplay;
    [Header("Healt Settings")]
    public bool healtPowerUp = false;
    public int healtAmount = 1;
    [Header("Ammo Settings")]
    public bool ammoPowerUp = false;
    public int ammoAmount = 5;
    [Header("Transform Settings")]
    [Header("Scale Settings")]
    [SerializeField] private float period = 2f;
    [SerializeField] Vector3 scaleVector;
    private Vector3 startScale;
    [SerializeField] private float scalefactor;
    public void Awake()
    {
        startScale = transform.localScale;
        if (healtPowerUp == true && ammoPowerUp == true)
        {
            healtPowerUp = false;
            ammoPowerUp = false;
        }
        else if (healtPowerUp == true)
        {
            ammoPowerUp = false;
        }
        else if (ammoPowerUp)
        {
            healtPowerUp = false;
        }
    } 
    void Update()
    {
        transform.Rotate(0.5f, 0f, 0f);
        SinusWawe();
    }
    private void SinusWawe()
    {
        if (period <= 0)
        {
            period = 0.01f;
        }
        float cycles = Time.timeSinceLevelLoad / period;
        const float piX2 = Mathf.PI * 2;
        float sinusWawe = Mathf.Sin(cycles * piX2);
        scalefactor = sinusWawe / 2 + 0.5f;
        Vector3 offset = scalefactor * scaleVector;
        transform.localScale = startScale + offset;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(cliptoplay, transform.position);
            if (healtPowerUp)
            {
                other.gameObject.GetComponent<Target>().GetHealt += healtAmount;
            }
            else if (ammoPowerUp)
            {
                other.gameObject.GetComponent<Attack>().GetAmmo += ammoAmount;
            }
            Destroy(gameObject);
        }
    }
}

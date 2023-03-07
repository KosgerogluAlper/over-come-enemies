using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    AudioSource audiosourcue;
    private void Awake()
    {
        audiosourcue = GetComponent<AudioSource>();
    }
    void Update()
    {
        audiosourcue.Play();   
    }
}

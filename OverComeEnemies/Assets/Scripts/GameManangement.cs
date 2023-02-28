using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManangement : MonoBehaviour
{ 
    public GameObject LevelFinishPanel;
    public bool levelFinish = false;

    [SerializeField] GameObject GunPanel;
    [SerializeField] GameObject SmgPanel;
    public bool guncontrol=true;


    void Update()
    {
        Gun();
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount <= 0 || GameObject.FindGameObjectWithTag("Player")==null)
        {
            LevelFinishPanel.SetActive(true);
            levelFinish = true;
        }
        else
        {
            LevelFinishPanel.SetActive(false);
            levelFinish = false;
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }


    public void Gun()
    {
        if (guncontrol)
        {
            GunPanel.SetActive(true);
            SmgPanel.SetActive(false);
        }
        else
        {
            SmgPanel.SetActive(true);
            GunPanel.SetActive(false);
        }

    }

}

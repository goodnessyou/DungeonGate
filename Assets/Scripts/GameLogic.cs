using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public GameObject monsters;
    public AudioManager audioManager;
    public PlayerLogic playerLogic;
    public GameObject inGameMenu;
    public GameObject BoneFireMenu;
    private bool menuState = false;

    void Start()
    {
        //audioManager.Play("DarkAmbient");
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuState == false)
            {
                playerLogic.GetComponent<ThirdPersonMovement>().OnDisable();
                inGameMenu.SetActive(true);
                menuState = true;
                Cursor.visible = true;
                BoneFireMenu.SetActive(false);
            }
            else
            {
                playerLogic.GetComponent<ThirdPersonMovement>().OnEnable();
                inGameMenu.SetActive(false);
                menuState = false;
                Cursor.visible = false;
            }
        }
    }

    public void ResumeGame()
    {
        inGameMenu.SetActive(false);
        menuState = false;
        playerLogic.GetComponent<ThirdPersonMovement>().OnEnable();
        Cursor.visible = false;
    }
    public void MainMenu()
    {
        playerLogic.GetComponent<ThirdPersonMovement>().OnEnable();
        SceneManager.LoadScene(0);
    }
    public void RespawnAllMobs()
    {
        Enemy[] allMobs = monsters.GetComponentsInChildren<Enemy>();
        foreach(Enemy mob in allMobs) 
        {
            mob.gameObject.GetComponent<Enemy>().ReSpawn();
        }
    }

    public void RestOnFire()
    {
        Cursor.visible = true;
        BoneFireMenu.SetActive(true);
        playerLogic.GetComponent<ThirdPersonMovement>().OnDisable();
        playerLogic.Rest();
        RespawnAllMobs();
        
    }

    public void RestEnd()
    {
        playerLogic.Rest();
        
        Cursor.visible = false;
        BoneFireMenu.SetActive(false);
        playerLogic.GetComponent<ThirdPersonMovement>().OnEnable();
    }


}

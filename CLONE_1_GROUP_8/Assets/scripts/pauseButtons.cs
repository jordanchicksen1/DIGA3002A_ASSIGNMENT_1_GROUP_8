using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseButtons : MonoBehaviour
{
    public player player;
    public CharacterController playerCharacterController;
    public GameObject playerObject;
    public GameObject gameoverScreen;
    public GameObject playerCamHolder;

    //checkpoints
    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;
    public GameObject checkpoint4;

    //respawn
    public healthManager healthManager;
    public manaManager manaManager;
    public bossHealth bosshealth;

    //respawn camera stuff
    public GameObject room1Cam;
    public GameObject room2Cam;
    public GameObject room3Cam;
    public GameObject room4Cam;
    public GameObject room5Cam;
    public GameObject room6Cam;
    public GameObject room7Cam;
    public GameObject room8Cam;
    public GameObject room9Cam;
    public GameObject room10Cam;
    public GameObject room11Cam;
    public GameObject room12Cam;
    public GameObject room13Cam;
    public GameObject room14Cam;
    public GameObject room15Cam;
    public GameObject room16Cam;
    public GameObject bossHealthBar;
    public GameObject bossRoomDoor;

    //audio
    public GameObject dungeonSong;
    public GameObject bossSong;



    //tutorial letter
    public GameObject tutorialNote;
    public void ExitButton()
    {
        player.Pause();
    }


    //this is for gameover only!!
    public void Retry()
    {
        if(player.checkpointOne == true)
        {
            playerObject.transform.position = checkpoint1.transform.position;
            playerCharacterController.enabled = false;
            StartCoroutine(Respawn());
            healthManager.FullHeal();
            manaManager.FullMana();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("should respawn");
        }

        else if (player.checkpointTwo == true)
        {
            playerObject.transform.position = checkpoint2.transform.position;
            playerCharacterController.enabled = false;
            StartCoroutine(Respawn());
            healthManager.FullHeal();
            manaManager.FullMana();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("should respawn");
        }

        else if (player.checkpointThree == true)
        {
            playerObject.transform.position = checkpoint3.transform.position;
            playerCharacterController.enabled = false;
            StartCoroutine(Respawn());
            healthManager.FullHeal();
            manaManager.FullMana();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("should respawn");
        }

        else if (player.checkpointFour == true)
        {
            playerObject.transform.position = checkpoint4.transform.position;
            playerCharacterController.enabled = false;
            StartCoroutine(Respawn());
            healthManager.FullHeal();
            manaManager.FullMana();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("should respawn");
        }
        else
        {
            SceneManager.LoadScene("GAME");
        }
    }

    public void ExitLetter()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        tutorialNote.SetActive(false);
        player.isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0f);
        gameoverScreen.SetActive(false);
        playerCharacterController.enabled=true;
        playerCamHolder.SetActive(true);
        room1Cam.SetActive(false);
        room2Cam.SetActive(false);
        room3Cam.SetActive(false);
        room4Cam.SetActive(false);
        room5Cam.SetActive(false);
        room6Cam.SetActive(false);
        room7Cam.SetActive(false);
        room8Cam.SetActive(false);
        room9Cam.SetActive(false);
        room10Cam.SetActive(false);
        room11Cam.SetActive(false);
        room12Cam.SetActive(false);
        room13Cam.SetActive(false);    
        room14Cam.SetActive(false);
        room15Cam.SetActive(false);
        room16Cam.SetActive(false);
        bossRoomDoor.SetActive(false);
        bossHealthBar.SetActive(false);
        bosshealth.FullHealBoss();
        bossSong.SetActive(false);
        dungeonSong.SetActive(true);
        bosshealth.RevertAllStats();
        player.isPaused = false;
    }
}

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
    }
}

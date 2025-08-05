using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

     public void quitGame()
    {
        Application.Quit();
    }

     public void endGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
}

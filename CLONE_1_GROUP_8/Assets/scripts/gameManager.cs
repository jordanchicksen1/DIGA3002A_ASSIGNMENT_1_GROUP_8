using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject canvas;
    public VideoPlayer videoPlayer;

    public void Start()
    {
        videoPlayer.loopPointReached += OnVideoEnd;
    }

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

    public void introClip()
    {
        canvas.SetActive(false);
        videoPlayer.Play();
    }

    void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
         SceneManager.LoadSceneAsync(1);
    }

}

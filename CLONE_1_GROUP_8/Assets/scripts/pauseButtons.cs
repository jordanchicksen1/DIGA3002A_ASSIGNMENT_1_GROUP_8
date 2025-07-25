using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseButtons : MonoBehaviour
{
    public player player;

    public void ExitButton()
    {
        player.Pause();
    }
}

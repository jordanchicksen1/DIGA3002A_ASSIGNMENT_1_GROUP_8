using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class keyManager : MonoBehaviour
{
    public int key;
    public TextMeshProUGUI keyText;

    public void addKey()
    {
        key = key + 1;
        keyText.text = key.ToString();
    }

    public void subtractKey()
    {
        key = key - 1;
        keyText.text = key.ToString();
    }
}

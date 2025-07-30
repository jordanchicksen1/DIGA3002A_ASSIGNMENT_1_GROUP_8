using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellCounter : MonoBehaviour
{
    public int spellCount;

    public void AddSpell()
    {
        spellCount = spellCount + 1;
    }
}

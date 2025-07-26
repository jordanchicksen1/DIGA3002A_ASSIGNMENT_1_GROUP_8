using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class manaManager : MonoBehaviour
{
    public float maxMana = 100f;
    public float currentMana;
    public Image manaBar;
   



    public void Start()
    {
        currentMana = maxMana;
        updateManaBar();
    }

    public void Update()
    {
        
    }

    public void updateMana(float amount)
    {
        currentMana += amount;
        updateManaBar();

    }

    public void updateManaBar()
    {
        float targetFillAmount = currentMana / maxMana;
        manaBar.fillAmount = targetFillAmount;
    }

    [ContextMenu("UseFireSpell")]
    public void UseFireSpell()
    {
        currentMana = currentMana - 5f;
        updateManaBar();

    }

    [ContextMenu("UseWaterSpell")]
    public void UseWaterSpell()
    {
        currentMana = currentMana - 5f;
        updateManaBar();

    }

    [ContextMenu("DrankManaPotion")]
    public void DrankManaPotion()
    {
        currentMana = currentMana + 50f;
        updateManaBar();

    }
}

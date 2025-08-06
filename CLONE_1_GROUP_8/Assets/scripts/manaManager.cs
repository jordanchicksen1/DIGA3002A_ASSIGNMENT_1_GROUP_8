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
    public GameObject manaBarBase;
    public Image manaBarUpgradeOne;
    public GameObject manaBarLevelOne;
    public Image manaBarUpgradeTwo;
    public GameObject manaBarLevelTwo;
    public Image manaBarUpgradeThree;
    public GameObject manaBarLevelThree;
    public Image manaBarUpgradeFour;
    public GameObject manaBarLevelFour;
    public Image manaBarUpgradeFive;
    public GameObject manaBarLevelFive;

    public AudioSource playersounds2;



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
        manaBarUpgradeOne.fillAmount = targetFillAmount;
        manaBarUpgradeTwo.fillAmount = targetFillAmount;
        manaBarUpgradeThree.fillAmount = targetFillAmount;
        manaBarUpgradeFour.fillAmount = targetFillAmount;
        manaBarUpgradeFive.fillAmount = targetFillAmount;
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
        currentMana = currentMana - 10f;
        updateManaBar();

    }

    [ContextMenu("UseWindSpell")]
    public void UseWindSpell()
    {
        currentMana = currentMana - 5f;
        updateManaBar();

    }

    [ContextMenu("UseRockSpell")]
    public void UseRockSpell()
    {
        currentMana = currentMana - 15f;
        updateManaBar();

    }

    [ContextMenu("DrankManaPotion")]
    public void DrankManaPotion()
    {
        currentMana = currentMana + 50f;
        updateManaBar();
        playersounds2.Play();
    }

    [ContextMenu("FullMana")]

    public void FullMana()
    {
        currentMana = maxMana;
        updateManaBar();
        playersounds2.Play();
    }

    public void GotUpgradeOne()
    {
        maxMana = 120f;
        currentMana = maxMana;
        manaBarBase.SetActive(false);
        manaBarLevelOne.SetActive(true);
        updateManaBar();
    }

    public void GotUpgradeTwo()
    {
        maxMana = 140f;
        currentMana = maxMana;
        manaBarLevelOne.SetActive(false);
        manaBarLevelTwo.SetActive(true);
        updateManaBar();
    }

    public void GotUpgradeThree()
    {
        maxMana = 160f;
        currentMana = maxMana;
        manaBarLevelTwo.SetActive(false);
        manaBarLevelThree.SetActive(true);
        updateManaBar();
    }

    public void GotUpgradeFour()
    {
        maxMana = 180f;
        currentMana = maxMana;
        manaBarLevelThree.SetActive(false);
        manaBarLevelFour.SetActive(true);
        updateManaBar();
    }

    public void GotUpgradeFive()
    {
        maxMana = 200f;
        currentMana = maxMana;
        manaBarLevelFour.SetActive(false);
        manaBarLevelFive.SetActive(true);
        updateManaBar();
    }
}

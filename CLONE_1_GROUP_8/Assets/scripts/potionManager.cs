using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class potionManager : MonoBehaviour
{
    public int manaPotion;
    public TextMeshProUGUI manaPotionText;

    public int healthPotion;
    public TextMeshProUGUI healthPotionText;

    public void addManaPotion()
    {
        manaPotion = manaPotion + 1;
        manaPotionText.text = manaPotion.ToString();
    }

    public void subtractManaPotion()
    {
        manaPotion = manaPotion - 1;
        manaPotionText.text = manaPotion.ToString();
    }

    public void addHealthPotion()
    {
        healthPotion = healthPotion + 1;
        healthPotionText.text = healthPotion.ToString();
    }

    public void subtractHealthPotion()
    {
        healthPotion = healthPotion - 1;
        healthPotionText.text = healthPotion.ToString();
    }
}

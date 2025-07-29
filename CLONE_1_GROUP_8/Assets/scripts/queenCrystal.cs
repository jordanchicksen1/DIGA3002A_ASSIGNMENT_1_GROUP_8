using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queenCrystal : MonoBehaviour
{
    public int queenCrystalCount;
    public manaManager manaManager;

    public void AddQueenCrystal()
    {
        queenCrystalCount = queenCrystalCount + 1;
    }

    public void CheckCrystalCount()
    {
        if (queenCrystalCount == 1)
        {
            manaManager.GotUpgradeOne();
        }

        if (queenCrystalCount == 2)
        {
            manaManager.GotUpgradeTwo();
        }

        if (queenCrystalCount == 3)
        {
            manaManager.GotUpgradeThree();
        }

        if (queenCrystalCount == 4)
        {
            manaManager.GotUpgradeFour();
        }

        if (queenCrystalCount == 5)
        {
            manaManager.GotUpgradeFive();
        }
    }
}

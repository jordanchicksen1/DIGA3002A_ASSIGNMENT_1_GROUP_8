using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingCoin : MonoBehaviour
{
    public int kingCoinCount;
    public healthManager healthManager;

    public void AddKingCoin()
    {
        kingCoinCount = kingCoinCount + 1;
    }

    public void CheckCoinCount()
    {
        if(kingCoinCount == 1)
        {
            healthManager.GotUpgradeOne();
        }

        if(kingCoinCount == 2)
        {
            healthManager.GotUpgradeTwo();
        }

        if (kingCoinCount == 3)
        {
            healthManager.GotUpgradeThree();
        }

        if(kingCoinCount == 4)
        {
            healthManager.GotUpgradeFour();
        }

        if(kingCoinCount == 5)
        {
            healthManager.GotUpgradeFive();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckButtonPurchased : CheckPurchase
{
    public string productID;
    public GameObject unlockedButton;

    public override void UpdatePurchaseUI()
    {
        bool isPurchased = PlayerPrefs.GetInt(productID) == 1;

        if (isPurchased)
        {
            unlockedButton.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}

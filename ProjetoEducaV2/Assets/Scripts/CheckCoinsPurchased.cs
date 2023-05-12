using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCoinsPurchased : CheckPurchase
{
    public Text coinsText;
    public string prefix;
    public string subType;


    // Update is called once per frame
    public override void UpdatePurchaseUI()
    {
        coinsText.text = prefix + PlayerPrefs.GetInt(subType);
    }
}

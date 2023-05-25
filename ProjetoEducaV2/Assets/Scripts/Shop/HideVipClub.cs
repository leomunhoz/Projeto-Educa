using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideVipClub : CheckPurchase
{
    //public GameObject button;
    // Start is called before the first frame update
    void Awake()
    {
        UpdatePurchaseUI();
    }

    // Update is called once per frame
    public override void UpdatePurchaseUI()
    {
        if (PlayerPrefs.GetInt("VIPCLUB") == 1)
        {
           // button.SetActive(true);
            //gameObject.SetActive(false);
            
        }
    }

}

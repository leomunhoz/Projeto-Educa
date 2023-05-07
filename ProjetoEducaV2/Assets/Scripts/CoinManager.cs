using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coin = 0;
    public TMP_Text coinText;
    void Start()
    {
        coinText.text = coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

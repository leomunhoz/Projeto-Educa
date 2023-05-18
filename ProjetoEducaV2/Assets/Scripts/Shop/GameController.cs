using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(ShowInterstitial), 5);
    }

    void ShowInterstitial()
    {
        FindObjectOfType<AdsManager>().ShowInterstitial();
    }
}

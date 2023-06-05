using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(ShowInterstitial), 1.5f);
    }

 public  void ShowInterstitial()
    {
        FindObjectOfType<AdsManager>().ShowInterstitial();
    }
    public void ShowRewarded()
    {
        FindObjectOfType<AdsManager>().ShowRewarded();
    }
}

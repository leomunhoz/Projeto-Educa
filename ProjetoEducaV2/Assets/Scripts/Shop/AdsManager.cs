using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidInterstitialId = "Interstitial_Android";
    [SerializeField] string _iOsInterstitialId = "Interstitial_iOS";
    string _interstitialId;

    [SerializeField] string _androidRewardedId = "Rewarded_Android";
    [SerializeField] string _iOsRewardedId = "Rewarded_iOS";
    string _rewardedId;

    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    void Awake()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
        _interstitialId = _iOsInterstitialId;
        _rewardedId = _iOsRewardedId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
        _interstitialId = _androidInterstitialId;
        _rewardedId = _androidRewardedId;
#else
        Debug.LogWarning("ADS NOT AVAILABLE ON THIS PLATFORM!");
        return;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAd(_interstitialId);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    //-----------------------------------
    public void ShowInterstitial()
    {

        if (PlayerPrefs.GetInt("VIP") == 1) return;
            if (PlayerPrefs.GetInt("VIP") == 0)
        {
            ShowAd(_interstitialId);
        }
       
    }

    //-----------------------------------
    public void ShowRewarded()
    {
        ShowAd(_rewardedId);
    }

    //-----------------------------------
    // Load content to the Ad Unit:
    void LoadAd(string adUnitId)
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + adUnitId);
        Advertisement.Load(adUnitId, this);
    }

    // Show the loaded content in the Ad Unit:
    void ShowAd(string adUnitId)
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + adUnitId);
        Advertisement.Show(adUnitId, this);
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd(_adUnitId);

        if (_adUnitId.Equals(_rewardedId))
        {
            int count = PlayerPrefs.GetInt("COINS");
            count += 2;
            PlayerPrefs.SetInt("COINS", count);
            Debug.Log("TOTAL [COINS] QUANTITY: " + count);
            PlayerPrefs.Save();

            //loop all buttons on scene to update button state
            CheckPurchase[] purchaseUIs = FindObjectsOfType<CheckPurchase>();
            foreach (CheckPurchase ui in purchaseUIs)
            {
                ui.UpdatePurchaseUI();
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdBehaviour : MonoBehaviour
{
    private BannerView bannerView;

    private InterstitialAd interstitialView;

    private int retryCount;

    private string AdUnitIdBanner = "ca-app-pub-8586182765170580/3412185355";
    private string AdUnitIdInsterstitial = "ca-app-pub-8586182765170580/4888918555";
    private static string testDeviceId = "3B92BFA48FBC7A95";

    void Start()
    {
        GameController.onGameOver += CheckRetryCount;

        CreateBannerView();

        // if this user does not have bought the remove ads add on
        ShowBannerView();
        //else
        //HideBannerView();
    }

    void Destroy()
    {
        GameController.onGameOver -= CheckRetryCount;
        bannerView.Destroy();
    }

    private void CheckRetryCount()
    {
        int count = LoadRetryCount();

        if(count > 10)
        {
            SaveRetryCount(0);
        }
    }

    private static AdRequest CreateAdRequest()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice(testDeviceId)
            //.AddTestDevice("3DA3C2567BFA84A4")	
            .AddExtra("color_bg", "9B30FF")
            .Build();

        return request;
    }

    #region BannerView
    private void CreateBannerView()
    {
        bannerView = new BannerView(AdUnitIdBanner, AdSize.Banner, AdPosition.Top);
        AdRequest request = CreateAdRequest();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    private void ShowBannerView()
    {
        bannerView.Show();
    }

    private void HideBannerView()
    {
        bannerView.Hide();
    }
    #endregion

    #region InterstitialAd
    private void CreateInterstitialAd()
    {
        interstitialView = new InterstitialAd(AdUnitIdInsterstitial);
        AdRequest request = CreateAdRequest();
        // Load the banner with the request.
        interstitialView.LoadAd(request);
    }

    private void ShowInterstitialView()
    {
        interstitialView.Show();
    }
    #endregion

    #region PlayerPrefs
    private void SaveRetryCount(int _retryCount)
    {
        PlayerPrefs.SetInt("Retry", _retryCount);
    }

    private int LoadRetryCount()
    {
        if (PlayerPrefs.HasKey("Retry"))
        {
            return PlayerPrefs.GetInt("Retry");

        }

        return 0;
    }
    #endregion
}

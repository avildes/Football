using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdBehaviour : MonoBehaviour
{
    private BannerView bannerView;

    private InterstitialAd interstitialAd;

    private int retryCount;

    private string AdUnitIdBanner = "ca-app-pub-8586182765170580/3412185355";
    private string AdUnitIdInsterstitial = "ca-app-pub-8586182765170580/4888918555";
    private static string testDeviceId = "3DA3C2567BFA84A4";

	public static AdBehaviour Instance;

	void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(this);
	}
	
	void Start ()
	{
		if(Instance == null)
		{
			Instance = this;   
		}
		
		GameController.onGameOver += CheckRetryCount;
		
		if(interstitialAd == null) CreateInterstitialAd();
		if(bannerView == null) CreateBannerView();
        // if this user does not have bought the remove ads add on
        //else
		ShowBannerView();
        //HideBannerView();
    }

    void OnDestroy()
    {
        GameController.onGameOver -= CheckRetryCount;
        bannerView.Destroy();
    }

    void CheckRetryCount()
    {
        int count = LoadRetryCount();

		//Debug.Log (count);

        if(count > 10)
        {
			ShowInterstitialAd();
			SaveRetryCount(0);
        }
		else
		{
			count += 1;
			SaveRetryCount(count);
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
        interstitialAd = new InterstitialAd(AdUnitIdInsterstitial);
        AdRequest request = CreateAdRequest();
        // Load the banner with the request.
        interstitialAd.LoadAd(request);
    }

    private void ShowInterstitialAd()
    {
		if (interstitialAd.IsLoaded ())
			interstitialAd.Show();
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

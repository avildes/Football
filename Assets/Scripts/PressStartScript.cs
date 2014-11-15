using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class PressStartScript : MonoBehaviour {

	void Start ()
    {
        StartCoroutine(Flash());
	}
	
	void Update ()
    {
	    if(Input.touchCount == 1 || Input.GetKeyDown(KeyCode.Space))
        {
			BannerView bannerView = new BannerView("ca-app-pub-9464272243303537/2092688809", AdSize.Banner, AdPosition.Top);
			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder()
				.AddTestDevice(AdRequest.TestDeviceSimulator)
				.AddTestDevice("359D039B81477EF2")
				//.AddTestDevice("3DA3C2567BFA84A4")	
				.AddExtra("color_bg", "9B30FF")
				.Build();
			// Load the banner with the request.
			bannerView.LoadAd(request);
			
			bannerView.Show ();
            //Application.LoadLevel("game");
        }
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}

    IEnumerator Flash()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(.5f);
        

        StartCoroutine(Flash());
    }
}

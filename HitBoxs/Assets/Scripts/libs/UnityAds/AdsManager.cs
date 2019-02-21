using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : Singleton<AdsManager>{
	// Use this for initialization
	string gameId = "1605592"; //google
	private string googleGameId = "1605592"; //google
	private string iosGameId = "1605591"; //google
	//rewardedVideo
	public const string rewardedVideoPlacementId = "rewardedVideo";
	public const string videoPlacementId = "video";
	
	private ShowOptions options;
	private bool isShowAd = false;//是否显示广告
	private bool isTestAd = true;
	void InitData () {
		
		#if UNITY_EDITOR
			
		#elif UNITY_ANDROID
		gameId = googleGameId;

		#elif UNITY_IPHONE
		gameId = iosGameId;
		#endif
	}

	public void Init()
	{
		InitData ();
		if(Advertisement.isSupported)
		{
			Advertisement.Initialize (gameId, isTestAd);
		}
		if(options == null)
		{
			options = new ShowOptions();
        	options.resultCallback = HandleShowResult;
		}
	}
	
	public void ShowAd (string placementId = rewardedVideoPlacementId)
    {
    	if(isShowAd == false)
    	{
    		return;
    	}
		if (Advertisement.IsReady (placementId)) {
			Advertisement.Show (placementId, options);
		} else {
			Debug.Log ("error: ad is not ready");
		}
        
    }

    void HandleShowResult (ShowResult result)
    {
        if(result == ShowResult.Finished) {
        Debug.Log("Video completed - Offer a reward to the player");

        }else if(result == ShowResult.Skipped) {
            Debug.LogWarning("Video was skipped - Do NOT reward the player");

        }else if(result == ShowResult.Failed) {
            Debug.LogError("Video failed to show");
        }
    }

}

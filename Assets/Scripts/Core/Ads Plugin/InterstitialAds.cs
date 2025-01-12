using UnityEngine;
using UnityEngine.Advertisements;

namespace Core.Ads_Plugin
{
    public class InterstitialAds : IInterstitialAds, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private string androidAdUnitId = "Interstitial_Android";
        private string iosAdUnitId = "Interstitial_iOS";

       

        public void LoadInterstitialAd()
        {
            if (string.IsNullOrEmpty(androidAdUnitId))
            {
                Debug.LogError("Ad Unit ID is null or empty. Cannot load ad.");
                return;
            }

            Advertisement.Load(androidAdUnitId, this);
        }

        public void ShowInterstitialAd()
        {
            Advertisement.Show(androidAdUnitId, this);
            LoadInterstitialAd();
        }

        #region LoadCallbacks

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Interstitial Ad Loaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.LogError($"Failed to load ad for {placementId}. Error: {error}, Message: {message}");
        }

        #endregion

        #region ShowCallbacks

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.LogError($"Ad Show Failed for {placementId}. Error: {error}, Message: {message}");
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debug.Log($"Ad Show Started for {placementId}");
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            Debug.Log($"Ad Clicked for {placementId}");
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debug.Log("Interstitial Ad Completed");
        }

        #endregion
    }
}
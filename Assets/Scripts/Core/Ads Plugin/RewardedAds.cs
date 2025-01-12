using UnityEngine;
using UnityEngine.Advertisements;

namespace Core.Ads_Plugin
{
    public class RewardedAds : IRewardedAds, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string androidAdUnitId = "Rewarded_Android";
        [SerializeField] private string iosAdUnitId = "Rewarded_iOS";
        private string adUnitId;

        private void Awake()
        {
#if UNITY_IOS
            adUnitId = iosAdUnitId;
#elif UNITY_ANDROID
            adUnitId = androidAdUnitId;
#endif
        }

        public void LoadRewardedAd()
        {
            if (!Advertisement.isInitialized)
            {
                Debug.LogError("Unity Ads is not initialized!");
                return;
            }

            Advertisement.Load(adUnitId, this);
        }

        public void ShowRewardedAd()
        {
            Advertisement.Show(adUnitId, this);
            LoadRewardedAd();
        }

        #region LoadCallbacks

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log("Interstitial Ad Loaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
        }

        #endregion

        #region ShowCallbacks

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (placementId == adUnitId && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Ads Fully Watched .....");
            }
        }

        #endregion
    }
}
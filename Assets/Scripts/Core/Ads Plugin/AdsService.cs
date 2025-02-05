using UnityEngine;
using UnityEngine.Advertisements;

namespace Core.Ads_Plugin
{
    public class AdsService : IAdsService
    {
        private const string GAME_ID = "5749687";
        public IBannerAds _bannerAds { get; }
        public IInterstitialAds _interstitialAds { get; }
        public IRewardedAds _rewardedAds { get; }

        public AdsService(IBannerAds bannerAds, IInterstitialAds interstitialAds, IRewardedAds rewardedAds)
        {
            _bannerAds = bannerAds;
            _interstitialAds = interstitialAds;
            _rewardedAds = rewardedAds;
        }

        public void InitializeAds()
        {
            string gameId = GAME_ID; 
            Advertisement.Initialize(gameId, true, new InitializationListener(this));
        }
    }

    public class InitializationListener : IUnityAdsInitializationListener
    {
        private IAdsService _adsService;

        public InitializationListener(IAdsService adsService)
        {
            _adsService = adsService;
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads Initialization Complete.");
            _adsService._bannerAds.LoadBannerAd();
            _adsService._interstitialAds.LoadInterstitialAd();
            _adsService._rewardedAds.LoadRewardedAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}
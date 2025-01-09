using UnityEngine;
using UnityEngine.Advertisements;

namespace Core.Ads_Plugin
{
    public class AdsService : MonoBehaviour
    {
        public BannerAds bannerAds;
        public InterstitialAds interstitialAds;
        public RewardedAds rewardedAds;

        public static AdsService Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeAds();
        }

        private void InitializeAds()
        {
            string gameId = "6b52f7c5-0989-4039-9e5e-89f6b1c3169e";

            Advertisement.Initialize(gameId, true, new InitializationListener(this));
        }
    }

    public class InitializationListener : IUnityAdsInitializationListener
    {
        private AdsService _adsService;

        public InitializationListener(AdsService adsService)
        {
            _adsService = adsService;
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads Initialization Complete.");
            _adsService.bannerAds.LoadBannerAd();
            _adsService.interstitialAds.LoadInterstitialAd();
            _adsService.rewardedAds.LoadRewardedAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}
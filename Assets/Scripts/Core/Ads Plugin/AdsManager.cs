using UnityEngine;
using UnityEngine.Advertisements;

namespace Core.Ads_Plugin
{
    public class AdsManager : MonoBehaviour
    {
        public BannerAds bannerAds;
        public InterstitialAds interstitialAds;
        public RewardedAds rewardedAds;

        public static AdsManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Инициализация рекламы и ожидание завершения процесса
            InitializeAds();
        }

        private void InitializeAds()
        {
            string gameId = "your_game_id";  // Укажи свой Game ID

            // Инициализируем рекламу с обработкой коллбеков
            Advertisement.Initialize(gameId, true, new InitializationListener(this));
        }
    }

    // Коллбек для слушателя инициализации
    public class InitializationListener : IUnityAdsInitializationListener
    {
        private AdsManager _adsManager;

        public InitializationListener(AdsManager adsManager)
        {
            _adsManager = adsManager;
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads Initialization Complete.");
            // После инициализации загрузим рекламу
            _adsManager.bannerAds.LoadBannerAd();
            _adsManager.interstitialAds.LoadInterstitialAd();
            _adsManager.rewardedAds.LoadRewardedAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}
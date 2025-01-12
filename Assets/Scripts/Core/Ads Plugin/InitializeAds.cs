using UnityEngine;
using UnityEngine.Advertisements;

namespace Core.Ads_Plugin
{
    public class InitializeAds : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] private string androidGameId = "5749400";
        [SerializeField] private string iosGameId = "5749401";
        [SerializeField] private bool isTesting;
        private string gameId;

        private void Awake()
        {
#if UNITY_IOS
                gameId = iosGameId;
#elif UNITY_ANDROID
            gameId = androidGameId;
#elif UNITY_EDITOR
            gameId = androidGameId; // If you Havn't Switched the Platfrom...
#endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(gameId, isTesting, this);
            }
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Ads Initialized...");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
        }
    }
}
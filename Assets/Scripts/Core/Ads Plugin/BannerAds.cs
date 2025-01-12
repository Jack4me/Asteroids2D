using UnityEngine;
using UnityEngine.Advertisements;

namespace Core.Ads_Plugin
{
    public class BannerAds : IBannerAds
    {
        private string androidAdUnitId = "Banner_Android";
        private string iosAdUnitId = "Banner_iOS";


        public void LoadBannerAd()
        {
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = BannerLoaded,
                errorCallback = BannerLoadedError
            };
            Advertisement.Banner.Load(androidAdUnitId, options);
        }

        public void ShowBannerAd()
        {

            BannerOptions options = new BannerOptions
            {
                showCallback = BannerShown,
                clickCallback = BannerClicked,
                hideCallback = BannerHidden
            };
            Advertisement.Banner.Show(androidAdUnitId, options);
        }

        public void HideBannerAd()
        {
            Advertisement.Banner.Hide();
        }

        #region Show Callbacks

        private void BannerHidden()
        {
        }

        private void BannerClicked()
        {
        }

        private void BannerShown()
        {

        }

        #endregion

        #region Load Callbacks

        private void BannerLoadedError(string message)
        {
        }

        private void BannerLoaded()
        {
            Debug.Log("Banner Ad Loaded");

        }

        #endregion
    }
}
namespace Core.Ads_Plugin
{
    public interface IAdsService
    {
        void InitializeAds();
        public IBannerAds _bannerAds { get; }
        public IInterstitialAds _interstitialAds { get; }
        public IRewardedAds _rewardedAds { get; }
    }
}
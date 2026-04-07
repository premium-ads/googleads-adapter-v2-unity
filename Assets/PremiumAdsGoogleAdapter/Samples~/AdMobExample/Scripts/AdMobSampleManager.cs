// AdMob Sample Scene — demonstrates loading all 6 ad formats through PremiumAds adapter
//
// Requirements:
//   - Google Mobile Ads Unity Plugin installed (https://developers.google.com/admob/unity/quick-start)
//   - PremiumAds AdMob Adapter V2 (this package)
//   - Custom event configured in AdMob console pointing to:
//     Class Name: net.premiumads.sdk.adapter.PremiumAdsAdapter
//     Parameter:  Your PremiumAds ad unit ID

using System.Collections.Generic;
using GoogleMobileAds.Api;
using PremiumAds;
using UnityEngine;
using UnityEngine.UI;

namespace PremiumAds.Samples
{
    public class AdMobSampleManager : MonoBehaviour
    {
        [Header("AdMob Ad Unit IDs")]
        public string bannerAdUnitId = "ca-app-pub-2142338037257831/5013815038";
        public string interstitialAdUnitId = "ca-app-pub-2142338037257831/1616542060";
        public string rewardedAdUnitId = "ca-app-pub-2142338037257831/6768646189";
        public string rewardedInterstitialAdUnitId = "ca-app-pub-2142338037257831/9846792399";
        public string nativeAdUnitId = "ca-app-pub-2142338037257831/3433902069";
        public string appOpenAdUnitId = "ca-app-pub-2142338037257831/3283026116";

        [Header("UI")]
        public Button btnBanner;
        public Button btnInterstitial;
        public Button btnRewarded;
        public Button btnRewardedInterstitial;
        public Button btnAppOpen;
        public Text logText;

        private BannerView _bannerView;
        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardedAd;
        private RewardedInterstitialAd _rewardedInterstitialAd;
        private AppOpenAd _appOpenAd;

        private void Start()
        {
            Log("Initializing Google Mobile Ads SDK...");

            // Enable PremiumAds adapter debug logging
            PremiumAdsAdapter.SetDebug(true);

            SetButtonsInteractable(false);

            MobileAds.Initialize(initStatus =>
            {
                Log("MobileAds initialized.");
                foreach (var kvp in initStatus.getAdapterStatusMap())
                {
                    Log(kvp.Key + " | State: " + kvp.Value.InitializationState);
                }
                SetButtonsInteractable(true);
                Log("Ready! Tap a button to load ads.");
            });

            btnBanner.onClick.AddListener(LoadBanner);
            btnInterstitial.onClick.AddListener(LoadInterstitial);
            btnRewarded.onClick.AddListener(LoadRewarded);
            btnRewardedInterstitial.onClick.AddListener(LoadRewardedInterstitial);
            btnAppOpen.onClick.AddListener(LoadAppOpen);
        }

        private void SetButtonsInteractable(bool enabled)
        {
            btnBanner.interactable = enabled;
            btnInterstitial.interactable = enabled;
            btnRewarded.interactable = enabled;
            btnRewardedInterstitial.interactable = enabled;
            btnAppOpen.interactable = enabled;
        }

        // ── Banner ──
        public void LoadBanner()
        {
            Log("Loading banner...");
            _bannerView?.Destroy();
            _bannerView = new BannerView(bannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
            _bannerView.OnBannerAdLoaded += () => Log("Banner loaded");
            _bannerView.OnBannerAdLoadFailed += (LoadAdError err) => Log("Banner failed: " + err.GetMessage());
            _bannerView.OnAdImpressionRecorded += () => Log("Banner impression");
            _bannerView.OnAdClicked += () => Log("Banner clicked");
            _bannerView.LoadAd(new AdRequest());
        }

        // ── Interstitial ──
        public void LoadInterstitial()
        {
            Log("Loading interstitial...");
            InterstitialAd.Load(interstitialAdUnitId, new AdRequest(), (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Log("Interstitial failed: " + (error?.GetMessage() ?? "null ad"));
                    return;
                }
                Log("Interstitial loaded");
                _interstitialAd = ad;
                ad.OnAdFullScreenContentOpened += () => Log("Interstitial shown");
                ad.OnAdFullScreenContentClosed += () => Log("Interstitial dismissed");
                ad.Show();
            });
        }

        // ── Rewarded ──
        public void LoadRewarded()
        {
            Log("Loading rewarded...");
            RewardedAd.Load(rewardedAdUnitId, new AdRequest(), (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Log("Rewarded failed: " + (error?.GetMessage() ?? "null ad"));
                    return;
                }
                Log("Rewarded loaded");
                _rewardedAd = ad;
                ad.OnAdFullScreenContentOpened += () => Log("Rewarded shown");
                ad.OnAdFullScreenContentClosed += () => Log("Rewarded dismissed");
                ad.Show(reward => Log("Earned reward: " + reward.Amount + " " + reward.Type));
            });
        }

        // ── Rewarded Interstitial ──
        public void LoadRewardedInterstitial()
        {
            Log("Loading rewarded interstitial...");
            RewardedInterstitialAd.Load(rewardedInterstitialAdUnitId, new AdRequest(), (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Log("Rewarded interstitial failed: " + (error?.GetMessage() ?? "null ad"));
                    return;
                }
                Log("Rewarded interstitial loaded");
                _rewardedInterstitialAd = ad;
                ad.OnAdFullScreenContentOpened += () => Log("Rewarded interstitial shown");
                ad.OnAdFullScreenContentClosed += () => Log("Rewarded interstitial dismissed");
                ad.Show(reward => Log("Earned reward: " + reward.Amount + " " + reward.Type));
            });
        }

        // ── App Open ──
        public void LoadAppOpen()
        {
            Log("Loading app open...");
            AppOpenAd.Load(appOpenAdUnitId, new AdRequest(), (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Log("App open failed: " + (error?.GetMessage() ?? "null ad"));
                    return;
                }
                Log("App open loaded");
                _appOpenAd = ad;
                ad.OnAdFullScreenContentOpened += () => Log("App open shown");
                ad.OnAdFullScreenContentClosed += () => Log("App open dismissed");
                ad.Show();
            });
        }

        private void Log(string message)
        {
            string line = "[" + System.DateTime.Now.ToString("HH:mm:ss") + "] " + message;
            Debug.Log(line);
            if (logText != null)
            {
                logText.text += "\n" + line;
            }
        }

        private void OnDestroy()
        {
            _bannerView?.Destroy();
            _interstitialAd?.Destroy();
            _rewardedAd?.Destroy();
            _rewardedInterstitialAd?.Destroy();
            _appOpenAd?.Destroy();
        }
    }
}

# PremiumAds Google AdMob Adapter V2 — Unity

Unity plugin for PremiumAds mediation adapter on Google AdMob (Android + iOS).

## Requirements

- Unity 2022.3 LTS or newer (Unity 6 supported)
- [Google Mobile Ads Unity Plugin](https://developers.google.com/admob/unity/quick-start) (required)
- [External Dependency Manager for Unity (EDM4U)](https://github.com/googlesamples/unity-jar-resolver) — already included in Google Mobile Ads Unity Plugin

## Installation

1. Download `PremiumAdsGoogleAdapter.unitypackage` from [Releases](https://github.com/premium-ads/googleads-adapter-v2-unity/releases)
2. In Unity: **Assets → Import Package → Custom Package** → select the `.unitypackage`
3. Click **Import**
4. EDM4U will automatically resolve native dependencies on next build:
   - **Android:** `net.premiumads.sdk:admob-adapter-v2` (from JFrog)
   - **iOS:** `PremiumAdsGoogleAdapter` (from CocoaPods)

## Configure AdMob Custom Event

In the [AdMob console](https://apps.admob.com), configure custom event for each ad format:

| Field | Value |
|-------|-------|
| **Class Name** | `net.premiumads.sdk.adapter.PremiumAdsAdapter` |
| **Parameter** | Your PremiumAds ad unit ID |

The same class name works for all ad formats — Banner, Interstitial, Rewarded, Rewarded Interstitial, Native, App Open. iOS uses class name `PremiumAdsAdapter`.

## Usage

The adapter works automatically through Google Mobile Ads Unity Plugin — no extra C# code needed. Load ads as usual:

```csharp
using GoogleMobileAds.Api;

// Initialize
MobileAds.Initialize(initStatus => { });

// Load banner
var bannerView = new BannerView("ca-app-pub-xxxxx/xxxxx", AdSize.Banner, AdPosition.Bottom);
bannerView.LoadAd(new AdRequest());
```

### Optional: Enable debug logging

```csharp
using PremiumAds;

PremiumAdsAdapter.SetDebug(true);
```

Filter logs:
- **Android Logcat:** `tag:PremiumAdsAdapter`
- **iOS Xcode console:** `[PremiumAdsAdapter]`

## Sample

A sample scene demonstrating all 6 ad formats is included at:

`Assets/PremiumAdsGoogleAdapter/Samples~/AdMobExample/`

To use it:
1. Import the unitypackage
2. Open `AdMobSample.unity`
3. Build and run on a device

## Documentation

- [Integration Guide](https://docs.premiumads.net/v2.0/docs/google-admob)
- [Test Ad Units](https://docs.premiumads.net/v2.0/docs/enabling-test-ads)

## Building from source

```bash
./build-unitypackage.sh [unity-version]
# Example:
./build-unitypackage.sh 6000.4.1f1
```

The output `.unitypackage` will be in `dist/`.

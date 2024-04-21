using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Infrastructure.Services.Ads
{
    public class AdsService : IAdsService, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        private const string AndroidGameId = "5601977";
        private const string IOSGameId = "5601976";

        private const string AndroidAdUnitId = "Rewarded_iOS";
        private const string IOSAdUnitId = "Rewarded_Android";
        
        private string _gameId;
        private string _adUnitId;
        private bool _testMode = true;

        public event Action RewardedVideoReady;
        private Action _onVideoFinished;

        public void Initialize()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    _gameId = AndroidGameId;
                    _adUnitId = AndroidAdUnitId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    _gameId = IOSGameId;
                    _adUnitId = IOSAdUnitId;
                    break;
                case RuntimePlatform.Android:
                    _gameId = AndroidGameId;
                    _adUnitId = AndroidAdUnitId;
                    break;
                default:
                    Debug.Log("Unsupported platform for ads");
                    break;
            }

            Advertisement.Initialize(_gameId, _testMode, this);
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            Advertisement.Show(_adUnitId, this);
            _onVideoFinished = onVideoFinished;
        }
        
        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            Debug.Log($"OnUnityAdsAdLoaded {placementId}");

            if (placementId == _adUnitId) 
                RewardedVideoReady?.Invoke();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.SKIPPED:
                    Debug.Log($"OnUnityAddShopComplete {showCompletionState}");
                    break;
                case UnityAdsShowCompletionState.COMPLETED:
                    _onVideoFinished?.Invoke();
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.Log($"OnUnityAddShopComplete {showCompletionState}");
                    break;
                default:
                    Debug.Log($"OnUnityAddShopComplete {showCompletionState}");
                    break;
            }

            _onVideoFinished = null;
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string placementId) { }

        public void OnUnityAdsShowClick(string placementId) { }
    }
}
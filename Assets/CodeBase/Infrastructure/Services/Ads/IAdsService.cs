using System;

namespace CodeBase.Infrastructure.Services.Ads
{
    public interface IAdsService : IService
    {
        event Action RewardedVideoReady;
        void Initialize();
        void ShowRewardedVideo(Action onVideoFinished);
    }
}
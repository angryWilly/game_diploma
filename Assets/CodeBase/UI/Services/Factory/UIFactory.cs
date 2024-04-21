﻿using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Ads;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private const string UIRootPath = "UI/UIRoot";
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private readonly IAdsService _adsService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData,
            IPersistentProgressService progressService, IAdsService adsService)
        {
            _assets = assets;   
            _staticData = staticData;
            _progressService = progressService;
            _adsService = adsService;
        }
        public void CreateShop()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.Shop);
            ShopWindow window = Object.Instantiate(config.Prefab, _uiRoot) as ShopWindow;
            window.Construct(_adsService, _progressService);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assets.Instantiate(UIRootPath).transform;
    }
}
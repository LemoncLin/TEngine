using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    [Window(UILayer.UI, location: "MainMenuUI")]
    public class MainMenuUI : UIWindow
    {
        private MenuBarWidget _menuBar;
        private Transform _contentAreaRoot;
        private readonly Dictionary<int, UIWidget> _moduleWidgets = new Dictionary<int, UIWidget>();
        private int _currentTabIndex = -1;

        private static readonly MenuTabConfig[] TabConfigs =
        {
            new MenuTabConfig { tabId = 0, tabName = "全景看房", widgetAssetName = "PanoramaUI", iconKey = "Panorama", isPopup = false },
            new MenuTabConfig { tabId = 1, tabName = "沙盘导航", widgetAssetName = "ProjectNavigationUI", iconKey = "Navigation", isPopup = false },
            new MenuTabConfig { tabId = 2, tabName = "户型鉴赏", widgetAssetName = "UnitBrowsingUI", iconKey = "Unit", isPopup = false },
            new MenuTabConfig { tabId = 3, tabName = "周边配套", widgetAssetName = "SurroundingUI", iconKey = "Surrounding", isPopup = false },
            new MenuTabConfig { tabId = 4, tabName = "一键通话", widgetAssetName = "", iconKey = "Contact", isPopup = true },
        };

        protected override void ScriptGenerator()
        {
            _contentAreaRoot = FindChildComponent<Transform>("m_tf_ContentArea");
        }

        protected override void OnCreate()
        {
            var barGo = FindChild("m_tf_MenuBar");
            if (barGo != null)
            {
                _menuBar = new MenuBarWidget();
                _menuBar.SetConfig(TabConfigs);
                _menuBar.Create(this, barGo.gameObject);
                _menuBar.OnTabSelected += OnTabSelected;
                _menuBar.OnPopupTrigger += OnPopupTrigger;
            }

            GameEvent.Get<IMainMenuUI>().ShowMainMenu();
            OnTabSelected(0);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _moduleWidgets.Clear();
            GameEvent.Get<IMainMenuUI>().CloseMainMenu();
        }

        private void OnTabSelected(int tabIndex)
        {
            SwitchToModuleAsync(tabIndex).Forget();
        }

        private void OnPopupTrigger(int tabIndex)
        {
            GameModule.UI.ShowUIAsync<ContactUI>();
        }

        private async UniTaskVoid SwitchToModuleAsync(int tabIndex)
        {
            GameEvent.Get<IMainMenuUI>().OnModuleLoading(tabIndex);

            if (_currentTabIndex >= 0 && _moduleWidgets.TryGetValue(_currentTabIndex, out var currentWidget))
            {
                currentWidget.Visible = false;
            }

            var widget = await GetOrCreateModuleWidget(tabIndex);
            if (widget != null)
            {
                widget.Visible = true;
            }

            _currentTabIndex = tabIndex;
            GameEvent.Get<IMainMenuUI>().OnModuleLoaded(tabIndex);
        }

        private async UniTask<UIWidget> GetOrCreateModuleWidget(int tabIndex)
        {
            if (_moduleWidgets.TryGetValue(tabIndex, out var cached))
            {
                return cached;
            }

            var cfg = TabConfigs[tabIndex];
            if (string.IsNullOrEmpty(cfg.widgetAssetName))
            {
                return null;
            }

            var widget = await CreateModuleWidgetAsync(tabIndex, cfg.widgetAssetName);
            if (widget != null)
            {
                _moduleWidgets[tabIndex] = widget;
            }
            return widget;
        }

        private async UniTask<UIWidget> CreateModuleWidgetAsync(int tabIndex, string assetName)
        {
            switch (tabIndex)
            {
                case 0: return await CreateWidgetByPathAsync<PanoramaUI>(_contentAreaRoot, assetName, false);
                case 1: return await CreateWidgetByPathAsync<ProjectNavigationUI>(_contentAreaRoot, assetName, false);
                case 2: return await CreateWidgetByPathAsync<UnitBrowsingUI>(_contentAreaRoot, assetName, false);
                case 3: return await CreateWidgetByPathAsync<SurroundingUI>(_contentAreaRoot, assetName, false);
                default: return null;
            }
        }
    }
}

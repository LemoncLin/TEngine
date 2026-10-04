using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class MenuBarWidget : UIWidget
    {
        private MenuTabConfig[] _tabConfigs;
        private Button[] _tabButtons;
        private Image[] _tabImages;
        private Transform _tabContainer;
        private int _selectedIndex = -1;

        public Action<int> OnTabSelected;
        public Action<int> OnPopupTrigger;

        public void SetConfig(MenuTabConfig[] configs)
        {
            _tabConfigs = configs;
            _tabButtons = new Button[configs.Length];
            _tabImages = new Image[configs.Length];
        }

        protected override void ScriptGenerator()
        {
            _tabContainer = FindChildComponent<Transform>("m_tf_TabContainer");
        }

        protected override void OnCreate()
        {
            for (int i = 0; i < _tabConfigs.Length; i++)
            {
                var cfg = _tabConfigs[i];
                var btnPath = $"m_btn_{cfg.iconKey}";
                _tabButtons[i] = FindChildComponent<Button>(_tabContainer, btnPath);
                if (_tabButtons[i] != null)
                {
                    _tabImages[i] = _tabButtons[i].GetComponent<Image>();
                    var index = i;
                    _tabButtons[i].onClick.AddListener(() => OnTabClick(index));
                }
            }
        }

        private void OnTabClick(int index)
        {
            if (_tabConfigs[index].isPopup)
            {
                OnPopupTrigger?.Invoke(index);
                return;
            }
            SelectTab(index);
            OnTabSelected?.Invoke(index);
        }

        public void SelectTab(int index)
        {
            if (index == _selectedIndex) return;
            if (_selectedIndex >= 0) SetTabHighlight(_selectedIndex, false);
            _selectedIndex = index;
            SetTabHighlight(_selectedIndex, true);
        }

        private void SetTabHighlight(int index, bool selected)
        {
            if (index < 0 || index >= _tabButtons.Length) return;
            var img = _tabImages[index];
            if (img != null)
            {
                img.color = selected
                    ? new Color(0.2f, 0.6f, 1f)
                    : new Color(0.5f, 0.5f, 0.5f);
            }
        }
    }
}

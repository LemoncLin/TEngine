using TEngine;

namespace GameLogic
{
    /// <summary>
    /// VR 售楼宝常驻一级主菜单（底栏）。
    ///
    /// 生命周期与流程状态机解耦：由 ProcedureLaunch 打开后常驻，
    /// 任何流程的 OnEnter / OnLeave 都不得关闭本窗口。
    /// 本窗口只负责展示底栏、转发切换请求与弹出联系窗口，不负责加载功能内容——
    /// 功能内容由各自功能流程管理自己的 UIWindow。
    ///
    /// 层级约束：必须是 UILayer.Bottom。若写成 UILayer.UI 且功能窗口全屏，
    /// UIModule.OnSetWindowVisible 会把本窗口 Visible 置为 false，"常驻"随即失效。
    /// </summary>
    [Window(UILayer.Bottom, location: "MainMenuUI")]
    public class MainMenuUI : UIWindow
    {
        private MenuBarWidget _menuBar;

        /// <summary>
        /// 底栏标签配置。下标顺序必须与 VRProcedureBase.OnMenuTabSelected 的分支一致。
        /// </summary>
        private static readonly MenuTabConfig[] TabConfigs =
        {
            new MenuTabConfig { tabId = 0, tabName = "全景看房", iconKey = "Panorama", isPopup = false },
            new MenuTabConfig { tabId = 1, tabName = "沙盘导航", iconKey = "Navigation", isPopup = false },
            new MenuTabConfig { tabId = 2, tabName = "户型鉴赏", iconKey = "Unit", isPopup = false },
            new MenuTabConfig { tabId = 3, tabName = "周边配套", iconKey = "Surrounding", isPopup = false },
            // iconKey 必须与 MainMenuUI.prefab 中按钮节点名 m_btn_Contact 一致（MenuBarWidget 按 m_btn_{iconKey} 查找）
            new MenuTabConfig { tabId = 4, tabName = "登录界面", iconKey = "Contact", isPopup = true },
        };

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

                // 只做初始高亮，不发切换请求：首个功能流程由 ProcedureSplash 统一落地，
                // 此处若发请求会与过渡流程竞争
                _menuBar.SelectTab(0);
            }

            GameEvent.Get<IMainMenuUI>().ShowMainMenu();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameEvent.Get<IMainMenuUI>().CloseMainMenu();
        }

        /// <summary>
        /// 底栏标签点击。UI 层不直接切换状态机——框架规定只有流程自身可以 ChangeState
        /// （FsmState.ChangeState 为 protected，Fsm.ChangeState 为 internal，
        /// IFsm.Start 在运行中会抛异常），因此这里只广播请求，由活跃流程接收后执行。
        /// </summary>
        /// <param name="tabIndex">被点击的标签下标。</param>
        private void OnTabSelected(int tabIndex)
        {
            GameEvent.Get<IMainMenuUI>().OnTabSelected(tabIndex);
        }

        /// <summary>
        /// 弹窗类标签（登录界面）：直接开登录窗口，不参与流程切换。
        /// LoginUI 的 Window 特性未指定 location，UIModule 按 type.Name 加载 LoginUI.prefab。
        /// </summary>
        /// <param name="tabIndex">被点击的标签下标。</param>
        private void OnPopupTrigger(int tabIndex)
        {
            GameModule.UI.ShowUIAsync<LoginUI>();
        }
    }
}

namespace GameLogic
{
    /// <summary>
    /// 底栏标签配置项。仅描述标签自身的展示与行为，不涉及功能内容的加载方式。
    /// </summary>
    public struct MenuTabConfig
    {
        /// <summary>标签唯一标识，与下标保持一致。</summary>
        public int tabId;

        /// <summary>标签显示文案。</summary>
        public string tabName;

        /// <summary>图标键名，用于拼接按钮查找路径 m_btn_{iconKey}。</summary>
        public string iconKey;

        /// <summary>是否为弹窗类标签。为 true 时点击只弹出窗口，不触发流程切换。</summary>
        public bool isPopup;
    }
}

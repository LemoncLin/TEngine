using TEngine;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// 沙盘导航窗口。由 ProcedureProjectNavigation 在 OnEnter 打开、OnLeave 关闭。
    ///
    /// 层级约束：必须 UILayer.UI 且 fullScreen 为 false。
    /// fullScreen 为 true 时 UIModule.OnSetWindowVisible 会把更底层的常驻 MainMenuUI
    /// 置为 Visible = false，底栏随即消失，"主菜单常驻"失效。
    /// </summary>
    [Window(UILayer.UI, location: "ProjectNavigationUI", fullScreen: false)]
    public class ProjectNavigationUI : UIWindow
    {
        private Transform _modelDisplayRoot;

        protected override void ScriptGenerator()
        {
            _modelDisplayRoot = FindChildComponent<Transform>("m_tf_ModelRoot");
        }
    }
}

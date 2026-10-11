using TEngine;
using UnityEngine;

namespace GameLogic
{
    /// <summary>
    /// 户型鉴赏窗口。由 ProcedureUnitBrowsing 在 OnEnter 打开、OnLeave 关闭。
    ///
    /// 层级约束：必须 UILayer.UI 且 fullScreen 为 false。
    /// fullScreen 为 true 时 UIModule.OnSetWindowVisible 会把更底层的常驻 MainMenuUI
    /// 置为 Visible = false，底栏随即消失，"主菜单常驻"失效。
    /// </summary>
    [Window(UILayer.UI, location: "UnitBrowsingUI", fullScreen: false)]
    public class UnitBrowsingUI : UIWindow
    {
        private Transform _unitListRoot;
        private Transform _floorPlanRoot;

        protected override void ScriptGenerator()
        {
            _unitListRoot = FindChildComponent<Transform>("m_tf_UnitList");
            _floorPlanRoot = FindChildComponent<Transform>("m_tf_FloorPlan");
        }
    }
}

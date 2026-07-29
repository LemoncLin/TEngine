using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI, location: "UnitBrowsingUI")]
    public class UnitBrowsingUI : UIWindow
    {
        private Button _btnBack;
        private Transform _unitListRoot;
        private Image _imgFloorPlan;

        protected override void ScriptGenerator()
        {
            _btnBack = FindChildComponent<Button>("m_btn_Back");
            _unitListRoot = FindChildComponent<Transform>("m_tf_UnitList");
            _imgFloorPlan = FindChildComponent<Image>("m_img_FloorPlan");
        }

        protected override void RegisterEvent()
        {
            _btnBack.onClick.AddListener(OnClickBack);
        }

        private void OnClickBack()
        {
            VRProcedureBase.ChangeTo<ProcedureMainMenu>();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}

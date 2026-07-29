using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI, location: "SurroundingUI")]
    public class SurroundingUI : UIWindow
    {
        private Button _btnBack;
        private Transform _categoryTabRoot;
        private Transform _infoListRoot;

        protected override void ScriptGenerator()
        {
            _btnBack = FindChildComponent<Button>("m_btn_Back");
            _categoryTabRoot = FindChildComponent<Transform>("m_tf_CategoryTabs");
            _infoListRoot = FindChildComponent<Transform>("m_tf_InfoList");
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

using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI, location: "ProjectNavigationUI")]
    public class ProjectNavigationUI : UIWindow
    {
        private Button _btnBack;
        private Transform _modelDisplayRoot;

        protected override void ScriptGenerator()
        {
            _btnBack = FindChildComponent<Button>("m_btn_Back");
            _modelDisplayRoot = FindChildComponent<Transform>("m_tf_ModelRoot");
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

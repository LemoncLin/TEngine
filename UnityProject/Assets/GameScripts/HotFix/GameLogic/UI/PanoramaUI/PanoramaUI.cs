using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI, location: "PanoramaUI")]
    public class PanoramaUI : UIWindow
    {
        private Button _btnBack;
        private Transform _sceneListRoot;

        protected override void ScriptGenerator()
        {
            _btnBack = FindChildComponent<Button>("m_btn_Back");
            _sceneListRoot = FindChildComponent<Transform>("m_tf_SceneList");
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

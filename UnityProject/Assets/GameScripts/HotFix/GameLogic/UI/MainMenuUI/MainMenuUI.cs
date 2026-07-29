using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI, location: "MainMenuUI")]
    public class MainMenuUI : UIWindow
    {
        private Button _btnPanorama;
        private Button _btnNavigation;
        private Button _btnUnit;
        private Button _btnSurrounding;
        private Button _btnContact;

        protected override void ScriptGenerator()
        {
            _btnPanorama = FindChildComponent<Button>("m_btn_Panorama");
            _btnNavigation = FindChildComponent<Button>("m_btn_Navigation");
            _btnUnit = FindChildComponent<Button>("m_btn_Unit");
            _btnSurrounding = FindChildComponent<Button>("m_btn_Surrounding");
            _btnContact = FindChildComponent<Button>("m_btn_Contact");
        }

        protected override void RegisterEvent()
        {
            _btnPanorama.onClick.AddListener(OnClickPanorama);
            _btnNavigation.onClick.AddListener(OnClickNavigation);
            _btnUnit.onClick.AddListener(OnClickUnit);
            _btnSurrounding.onClick.AddListener(OnClickSurrounding);
            _btnContact.onClick.AddListener(OnClickContact);
        }

        private void OnClickPanorama()
        {
            VRProcedureBase.ChangeTo<ProcedurePanorama>();
        }

        private void OnClickNavigation()
        {
            VRProcedureBase.ChangeTo<ProcedureProjectNavigation>();
        }

        private void OnClickUnit()
        {
            VRProcedureBase.ChangeTo<ProcedureUnitBrowsing>();
        }

        private void OnClickSurrounding()
        {
            VRProcedureBase.ChangeTo<ProcedureSurrounding>();
        }

        private void OnClickContact()
        {
            GameModule.UI.ShowUIAsync<ContactUI>();
        }
    }
}

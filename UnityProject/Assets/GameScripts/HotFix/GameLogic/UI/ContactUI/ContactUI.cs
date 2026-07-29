using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.Top, location: "ContactUI", fullScreen: false)]
    public class ContactUI : UIWindow
    {
        private Button _btnClose;
        private Text _txtHotline;
        private Button _btnCall;
        private Button _btnOnlineConsult;

        protected override void ScriptGenerator()
        {
            _btnClose = FindChildComponent<Button>("m_btn_Close");
            _txtHotline = FindChildComponent<Text>("m_text_Hotline");
            _btnCall = FindChildComponent<Button>("m_btn_Call");
            _btnOnlineConsult = FindChildComponent<Button>("m_btn_OnlineConsult");
        }

        protected override void RegisterEvent()
        {
            _btnClose.onClick.AddListener(OnClickClose);
            _btnCall.onClick.AddListener(OnClickCall);
            _btnOnlineConsult.onClick.AddListener(OnClickOnlineConsult);
        }

        private void OnClickClose()
        {
            GameModule.UI.CloseUI<ContactUI>();
        }

        private void OnClickCall()
        {
            Log.Info("ContactUI: Call hotline triggered.");
        }

        private void OnClickOnlineConsult()
        {
            Log.Info("ContactUI: Online consult triggered.");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}

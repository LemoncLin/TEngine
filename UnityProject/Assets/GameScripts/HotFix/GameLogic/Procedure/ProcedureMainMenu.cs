using Cysharp.Threading.Tasks;
using TEngine;
using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;

namespace GameLogic
{
    public class ProcedureMainMenu : VRProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            LoadMainScene().Forget();
            GameModule.UI.ShowUIAsync<MainMenuUI>();
            GameEvent.Get<IMainMenuUI>().ShowMainMenu();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            if (!isShutdown)
            {
                GameModule.UI.CloseUI<MainMenuUI>();
                GameEvent.Get<IMainMenuUI>().CloseMainMenu();
            }
        }

        private async UniTaskVoid LoadMainScene()
        {
            await GameModule.Scene.LoadSceneAsync("MainMenuScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}

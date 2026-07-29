using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;

namespace GameLogic
{
    public class ProcedurePanorama : VRProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameModule.UI.ShowUIAsync<PanoramaUI>();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            if (!isShutdown)
            {
                GameModule.UI.CloseUI<PanoramaUI>();
            }
        }

        public void ReturnToMainMenu(ProcedureOwner procedureOwner)
        {
            ChangeState<ProcedureMainMenu>(procedureOwner);
        }
    }
}

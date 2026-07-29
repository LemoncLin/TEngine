using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;

namespace GameLogic
{
    public class ProcedureSurrounding : VRProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameModule.UI.ShowUIAsync<SurroundingUI>();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            if (!isShutdown)
            {
                GameModule.UI.CloseUI<SurroundingUI>();
            }
        }

        public void ReturnToMainMenu(ProcedureOwner procedureOwner)
        {
            ChangeState<ProcedureMainMenu>(procedureOwner);
        }
    }
}

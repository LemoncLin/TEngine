using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;

namespace GameLogic
{
    public class ProcedureUnitBrowsing : VRProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameModule.UI.ShowUIAsync<UnitBrowsingUI>();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            if (!isShutdown)
            {
                GameModule.UI.CloseUI<UnitBrowsingUI>();
            }
        }

        public void ReturnToMainMenu(ProcedureOwner procedureOwner)
        {
            ChangeState<ProcedureMainMenu>(procedureOwner);
        }
    }
}

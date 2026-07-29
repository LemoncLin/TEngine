using Cysharp.Threading.Tasks;
using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;

namespace GameLogic
{
    public class ProcedureSplash : VRProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            TransitionToMainMenu(procedureOwner).Forget();
        }

        private async UniTaskVoid TransitionToMainMenu(ProcedureOwner procedureOwner)
        {
            await UniTask.Delay(1000);
            ChangeState<ProcedureMainMenu>(procedureOwner);
        }
    }
}

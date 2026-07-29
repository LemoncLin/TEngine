using TEngine;
using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;

namespace GameLogic
{
    public abstract class VRProcedureBase : TEngine.ProcedureBase
    {
        private static ProcedureOwner _procedureOwner;
        private static VRProcedureBase _instance;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            _procedureOwner = procedureOwner;
            _instance = this;
        }

        public static bool IsReady => _instance != null && _procedureOwner != null;

        public static void ChangeTo<T>() where T : ProcedureBase
        {
            if (_instance != null && _procedureOwner != null)
            {
                _instance.ChangeState<T>(_procedureOwner);
            }
        }

        public void ReturnToMainMenu()
        {
            ChangeState<ProcedureMainMenu>(_procedureOwner);
        }
    }
}

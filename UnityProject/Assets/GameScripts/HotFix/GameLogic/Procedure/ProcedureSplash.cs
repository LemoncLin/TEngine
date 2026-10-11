using Cysharp.Threading.Tasks;
using TEngine;
using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;

namespace GameLogic
{
    /// <summary>
    /// 启动过渡流程：仅承担进入应用前的延时过渡，不涉及场景加载与主菜单开关。
    /// 基础场景与常驻主菜单由 ProcedureLaunch 初始化，本流程结束后直接进入首个功能流程。
    /// </summary>
    public class ProcedureSplash : VRProcedureBase
    {
        /// <summary>
        /// 过渡延时时长（毫秒）。
        /// </summary>
        private const int TransitionDelayMs = 1000;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            TransitionToFirstModule(procedureOwner).Forget();
        }

        private async UniTaskVoid TransitionToFirstModule(ProcedureOwner procedureOwner)
        {
            await UniTask.Delay(TransitionDelayMs);

            // 过渡延时期间底栏可能已请求切换到其它功能流程，此时本流程已离开，不再推进状态机
            if (GameModule.Procedure.CurrentProcedure != this)
            {
                return;
            }

            // 常驻主菜单已由 ProcedureLaunch 打开，此处直接落到首个功能流程
            ChangeState<ProcedurePanorama>(procedureOwner);
        }
    }
}

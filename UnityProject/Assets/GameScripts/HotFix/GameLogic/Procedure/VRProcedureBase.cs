using TEngine;
using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;

namespace GameLogic
{
    /// <summary>
    /// VR 售楼宝流程基类。统一承载「常驻底栏请求切换流程」的通路。
    ///
    /// 框架约束（决定了这条通路必须这样走）：
    /// - <c>FsmState.ChangeState</c> 为 protected：只有流程自身能切换；
    /// - <c>Fsm.ChangeState</c> 为 internal：跨程序集不可见；
    /// - <c>IFsm.Start&lt;T&gt;</c> 在状态机运行中会抛 "FSM is running"，且不触发旧状态 OnLeave。
    /// 因此 UI 层只负责发事件请求，实际的状态机切换由当前活跃流程接收后执行。
    /// </summary>
    public abstract class VRProcedureBase : TEngine.ProcedureBase
    {
        /// <summary>
        /// 本流程持有的状态机引用，OnEnter 保存、OnLeave 置空。
        /// </summary>
        private ProcedureOwner _procedureOwner;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            _procedureOwner = procedureOwner;
            // 订阅常驻底栏切换请求。进入时订阅、离开时注销，保证同一时刻只有活跃流程响应
            GameEventHelper.AddEvent.MainMenuUI.OnTabSelected(OnMenuTabSelected);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            // 先注销再清空，避免切换过程中收到重入请求
            GameEventHelper.RemoveEvent.MainMenuUI.OnTabSelected(OnMenuTabSelected);
            _procedureOwner = null;
            base.OnLeave(procedureOwner, isShutdown);
        }

        /// <summary>
        /// 响应常驻底栏的标签点击，把 tab 下标翻译为目标流程并执行切换。
        /// </summary>
        /// <param name="tabIndex">底栏标签下标，与 MainMenuUI 的 TabConfigs 顺序一致。</param>
        private void OnMenuTabSelected(int tabIndex)
        {
            if (_procedureOwner == null)
            {
                return;
            }

            // ProcedureLaunch 尚未完成 MainMenuScene 的 Single 加载，此时响应切换会让目标流程的
            // additive 子场景与 Single 主场景加载并发，子场景可能被 Single 模式冲掉。
            // 启动流程期间的切换请求一律丢弃，启动完成进入后续流程后再正常响应。
            if (this is ProcedureLaunch)
            {
                return;
            }

            switch (tabIndex)
            {
                case 0:
                    SwitchTo<ProcedurePanorama>();
                    break;
                case 1:
                    SwitchTo<ProcedureProjectNavigation>();
                    break;
                case 2:
                    SwitchTo<ProcedureUnitBrowsing>();
                    break;
                case 3:
                    SwitchTo<ProcedureSurrounding>();
                    break;
                    // 下标 4 为登录界面弹窗，由 MainMenuUI 直接打开 LoginUI，不参与流程切换
            }
        }

        /// <summary>
        /// 切换到目标流程。目标即当前流程时跳过，避免重复卸载/加载子场景与功能窗口。
        /// </summary>
        /// <typeparam name="T">目标流程类型。</typeparam>
        private void SwitchTo<T>() where T : ProcedureBase
        {
            if (_procedureOwner == null || GameModule.Procedure.CurrentProcedure is T)
            {
                return;
            }

            ChangeState<T>(_procedureOwner);
        }
    }
}

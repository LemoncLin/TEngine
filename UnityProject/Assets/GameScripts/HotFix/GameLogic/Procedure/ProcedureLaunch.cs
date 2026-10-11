using Cysharp.Threading.Tasks;
using TEngine;
using UnityEngine.SceneManagement;
using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;

namespace GameLogic
{
    /// <summary>
    /// 热更域启动流程：加载基础场景并打开常驻主菜单。
    /// 职责对齐主包 ProcedureLaunch 的"初始化"定位，只做环境准备，
    /// 不承担展示过渡（过渡由 ProcedureSplash 负责），也不负责功能内容。
    /// </summary>
    public class ProcedureLaunch : VRProcedureBase
    {
        /// <summary>
        /// 基础场景资源定位地址。承载常驻底栏所在场景，整个游戏周期仅以 Single 模式加载一次。
        /// </summary>
        private const string MainSceneLocation = "MainMenuScene";

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            // 框架红线：禁止 async void 与 Coroutine，异步初始化必须 UniTask + Forget
            LaunchAsync(procedureOwner).Forget();
        }

        /// <summary>
        /// 启动初始化：打开常驻主菜单 → 加载基础场景 → 切换到启动过渡流程。
        /// </summary>
        /// <param name="procedureOwner">流程状态机持有者。</param>
        private async UniTaskVoid LaunchAsync(ProcedureOwner procedureOwner)
        {
            // 顺序不可颠倒：必须先开 UI、后切场景。
            // UIModule 是惰性单例（GameModule.UI => UIModule.Instance 首次访问才触发 OnInit），
            // OnInit 会在「当前场景」执行 GameObject.Find("UIRoot") 并对其 DontDestroyOnLoad。
            // 若先以 Single 模式切走 main.unity，UIRoot 会随该场景销毁，
            // OnInit 必然报 "UIRoot not found !" 并 return，导致 Resource 加载器从未创建。
            // 这正是原 ProcedureMainMenu 用 .Forget() 让 UI 初始化抢在场景切换完成之前的原因。
            await GameModule.UI.ShowUIAsyncAwait<MainMenuUI>();

            // 至此 UIRoot 已 DontDestroyOnLoad，之后切换基础场景不再影响 UI 层
            await GameModule.Scene.LoadSceneAsync(MainSceneLocation, LoadSceneMode.Single);

            // 异步等待期间底栏可能已请求切换到功能流程，此时本流程已离开，不可再推进状态机
            if (GameModule.Procedure.CurrentProcedure == this)
            {
                ChangeState<ProcedureSplash>(procedureOwner);
            }
        }
    }
}

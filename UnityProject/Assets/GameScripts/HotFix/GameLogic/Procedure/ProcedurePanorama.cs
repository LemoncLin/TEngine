using Cysharp.Threading.Tasks;
using TEngine;
using UnityEngine.SceneManagement;
using ProcedureOwner = TEngine.IFsm<TEngine.IProcedureModule>;

namespace GameLogic
{
    /// <summary>
    /// 全景看房流程：进入时打开 PanoramaUI 并 additive 加载子场景，离开时镜像释放。
    /// 本流程不管理常驻主菜单 MainMenuUI，主菜单生命周期与状态机解耦。
    /// </summary>
    public class ProcedurePanorama : VRProcedureBase
    {
        /// <summary>
        /// 全景子场景资源定位地址。必须与 OnLeave 卸载地址完全一致。
        /// </summary>
        private const string SubSceneLocation = "PanoramaScene";

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            EnterAsync().Forget();
        }

        /// <summary>
        /// 进入流程：加载子场景后打开功能窗口，任一失败都不切换流程。
        /// 每个 await 之后都要核对流程归属，避免在本流程已离开后继续产生无人释放的资源。
        /// </summary>
        private async UniTaskVoid EnterAsync()
        {
            // SceneModule 对同一 location 重复 additive 加载会直接抛异常而非返回失败，必须先判重
            if (!GameModule.Scene.IsContainScene(SubSceneLocation))
            {
                await GameModule.Scene.LoadSceneAsync(SubSceneLocation, LoadSceneMode.Additive);
            }

            // 场景加载期间可能已被切走：OnLeave 执行时场景尚未加载完成，其卸载判断会落空，
            // 这里补做一次释放，保证子场景不会残留
            if (GameModule.Procedure.CurrentProcedure != this)
            {
                ReleaseOwned();
                return;
            }

            await GameModule.UI.ShowUIAsyncAwait<PanoramaUI>();

            // 窗口加载期间可能已被切走：OnLeave 的 CloseUI 可能早于窗口入栈完成，这里补关一次
            if (GameModule.Procedure.CurrentProcedure != this)
            {
                GameModule.UI.CloseUI<PanoramaUI>();
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            // 关机时 UI 框架与 SceneModule.Shutdown 会统一释放，此处只处理常规流程切换
            if (isShutdown)
            {
                return;
            }

            ReleaseOwned();
        }

        /// <summary>
        /// 释放本流程持有的资源：功能窗口 + 子场景。
        /// 常驻主菜单 MainMenuUI 不属于本流程，绝不关闭；
        /// CloseUI 与 IsContainScene/UnloadAsync 均自带存在性判断，重复调用安全。
        /// </summary>
        private void ReleaseOwned()
        {
            GameModule.UI.CloseUI<PanoramaUI>();

            if (GameModule.Scene.IsContainScene(SubSceneLocation))
            {
                GameModule.Scene.UnloadAsync(SubSceneLocation).Forget();
            }
        }
    }
}

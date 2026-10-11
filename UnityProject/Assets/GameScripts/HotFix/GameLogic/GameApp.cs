using System.Collections.Generic;
using System.Reflection;
using GameLogic;
#if ENABLE_OBFUZ
using Obfuz;
#endif
using TEngine;
#pragma warning disable CS0436


/// <summary>
/// 游戏App。
/// </summary>
#if ENABLE_OBFUZ
[ObfuzIgnore(ObfuzScope.TypeName | ObfuzScope.MethodName)]
#endif
public partial class GameApp
{
    private static List<Assembly> _hotfixAssembly;

    /// <summary>
    /// 热更域App主入口。
    /// </summary>
    /// <param name="objects"></param>
    public static void Entrance(object[] objects)
    {
        GameEventHelper.Init();
        _hotfixAssembly = (List<Assembly>)objects[0];
        Log.Warning("======= 看到此条日志代表你成功运行了热更新代码 =======");
        Log.Warning("======= Entrance GameApp  V2 热更测试成功 =======");
        Utility.Unity.AddDestroyListener(Release);
        Log.Warning("======= StartGameLogic =======");
        StartGameLogic();
    }

    private static void StartGameLogic()
    {
        // 注册热更域流程 — VR 售楼宝：启动初始化 → 启动过渡 → 功能流程直切。
        // RestartProcedure 会销毁主包启动链 FSM，并自动启动列表第一个流程（ProcedureLaunch）。
        // 主菜单 MainMenuUI 由 ProcedureLaunch 打开后常驻，不随任何流程切换关闭。
        GameModule.Procedure.RestartProcedure(
            new ProcedureLaunch(),
            new ProcedureSplash(),
            new ProcedurePanorama(),
            new ProcedureProjectNavigation(),
            new ProcedureUnitBrowsing(),
            new ProcedureSurrounding()
        );
    }

    private static void Release()
    {
        SingletonSystem.Release();
        Log.Warning("======= Release GameApp =======");
    }
}
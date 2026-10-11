
1. Procedure 流程是什么?
TEngine 的 Procedure 是一套**有限状态机**，基类 `ProcedureBase`。
每个流程就是一个独立类，代表游戏的一个阶段。
同一时间**只会有一个 Procedure 处于激活**。
生命周期：
``` 
// 进入这个状态时执行一次
protected override void OnEnter(){}

// 每帧执行，当前处于这个状态就会跑
protected override void OnUpdate(){}

// 离开这个状态前执行一次，清理资源/取消事件
protected override void OnLeave(){}

// 切换到目标流程
ChangeState<ProcedureXXX>();
```

2. 为什么要用 Procedure（流程状态机）？

游戏启动、版本更新、资源下载、加载、进大厅、进副本，**天然是串行阶段**。 不用状态机很容易写出：多层`if/else`、全局标记位、到处穿插协程，后期极难维护。

就是用生命周期的方式来管理游戏。



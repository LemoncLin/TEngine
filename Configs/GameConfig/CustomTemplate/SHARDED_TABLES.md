# 分片表说明（SHARDED_TABLES）

权威说明亦见：UnityProject/.codex/skills/luban-dev/references/tengine-integration.md。

## 导表模式

| 模式 | 脚本 | 产物要点 |
|------|------|----------|
| lazyload | gen_code_bin_to_project_lazyload.bat/.sh | cs-bin + bin-sharded，dataExporter=sharded |
| standard | gen_code_bin_to_project.bat/.sh | cs-bin + bin，标准 Tables |

产物目录：Assets/GameScripts/HotFix/GameProto、Assets/AssetRaw/Configs/bytes。

## 分片 tags

partition=field#partition_field=levelId
partition=range#partition_field=id#partition_size=1000
partition=count#partition_field=id#partition_size=1000

- 分片字段：导出的非空 int/long，禁止负值。
- range/count：map 主键必须是分片字段。
- field：可用于按关卡聚合的 list 表；不能作为生成 ref 的目标。
- range：按 key 算分片；count：排序后分组，另有路由 index。

## 运行时

- field：LoadTbXPartition / ReleaseTbXPartition
- range/count：accessor + ReleaseTbXPartition
- ClearPartitions：清分片与路由索引缓存，不清普通懒加载表，不自动释放 YooAsset 句柄

加载器模板见同目录 ConfigSystem.cs。

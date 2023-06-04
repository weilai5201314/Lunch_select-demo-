﻿# 大二下实训项目——随机选餐软件

> 软件名:食之味
> 
> 功能:随机选择午餐
> 
> writer:weilai
> 
> 指导老师:Mr Liu
> 
> 语言:C#

**注意：**

- 项目使用了 .net core，旨在跨平台开发。目前使用的是 .net 7 sdk，~~还在测试是否能在mac上运行~~，因为使用了windows特定库，跨平台失败，仅支持windows。


## 项目配置

**IDE** : Jetbrain Rider 2023.1

**外部下载依赖**

- .net 7 sdk
- Mysql net connector/net 8.0.32
- Mysql server 8.0.32

**idea内部下载**

- MySql.Data 8.0.33
- Newtonsoft.Json 13.0.3
- mysql-connector-net 1.0.0
- avalonia 11.0.0-preview8
  
## 其他事项

因学期时间有限，已到学期末，所以还有很多改动没有完成。

- 下载的avalonia准备用于大改UI。
- 数据库增加触发器，省去sql代码语句。
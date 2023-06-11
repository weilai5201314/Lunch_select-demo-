# 大二下实训项目——随机选餐软件

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
- 界面做的不是很协调（第一次用c#），然后第一次数据库开发，也没有在数据库添加触发器之类的东西，还有很多进步的空间。


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


## 环境配置

### 1.安装全部外部依赖

  安装viausl studio，并安装c#所有依赖。

  先安装外部依赖，然后开始初始化数据库，需要的库及表如下：

- 库名:test
- 表名:
  - users : ID 自增序列 + UserName varchar（20） + Password varchar（20） + tip varchar（20）
  - 菜单表 : id 自增序列 + 菜名 varchar（20）+ 口味 varchar(10)
  - usermenu : UserID 引用users的UserName列 + MenuID 引用菜单表的id列

### 2.在ide内安装内部依赖


## 其他事项

因学期时间有限，已到学期末，所以还有很多改动没来得及实现。

- 下载的avalonia准备用于大改UI。
- 数据库增加触发器，省去sql代码语句。

# [Crx.vNext.API](https://github.com/as260405901/Crx.vNext.API) 微服务架构

## 特点
#### 合理的分层设计
#### 模块化的功能开关，按需选择
#### 基于程序集基础类的依赖注入，杜绝过渡封装
#### 集成MiniProfiler至Swagger
#### AOP：方法间调用性能监控
#### AOP：方法出入参日志
#### AOP：全局异常日志
#### AOP：统一的接口响应格式，统一接口入参校验错误信息格式
#### 实现层完全解耦，反射注入，开发调试时自动编译、拷贝
#### 支持分布式Session
#### 支持服务注册发现
#### 支持服务限流、熔断
#### 集成IDS4实现授权鉴权
#### 支持 Docker 容器
#### 全局使用性能更高的 [System.Text.Json](https://github.com/dotnet/runtime/tree/master/src/libraries/System.Text.Json)
#### 已集成：AOP(面向切面编程)、CORS(跨域)、分布式Session(会话保持)、[Castle(动态代理)](https://github.com/castleproject/Core)、[Serilog(日志)](https://github.com/serilog/serilog)、[Swagger(接口文档)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)、[AutoFac(IoC 容器)](https://github.com/autofac/Autofac)、[Automapper(实体映射)](https://github.com/AutoMapper/AutoMapper)、[StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis/)、[System.Text.Json](https://github.com/dotnet/runtime/tree/master/src/libraries/System.Text.Json)、[Snowflake(雪花算法)](https://github.com/stulzq/snowflake-net)、[Dapper(ORM)](https://github.com/StackExchange/Dapper)、[Dapper.SimpleCRUD(ORM)](https://github.com/ericdc1/Dapper.SimpleCRUD/)、[MiniProfiler(接口性能)](https://github.com/MiniProfiler/dotnet)、[Consul(数据中心)](https://github.com/hashicorp/consul)、[Ocelot(网关)](https://github.com/ThreeMammals/Ocelot)、[IdentityServer4(授权中心)](https://github.com/IdentityServer/IdentityServer4)
#### 注：[Crx.vNext.Gateway(网关)](https://github.com/as260405901/Crx.vNext.API/tree/main/Crx.vNext.Gateway)需要[Crx.vNext.Authentication(授权中心)](https://github.com/as260405901/Crx.vNext.API/tree/main/Crx.vNext.Authentication)、[Consul(数据中心)](https://www.consul.io/)支持，请自行安装部署
#### 注：截止目前可作为高可用架构



## 预计集成功能
配置中心、Elasticsearch(分布式搜索分析引擎)、SkyAPM(分布式链路追踪)、Polloy(瞬态故障处理)、RabbitMQ(消息队列)、NCC.CAP(分布式定理)、数据库读写分离



## Docker 环境配置
#### .Net 6	SDK		docker pull mcr.microsoft.com/dotnet/sdk:6.0
#### .Net 5	SDK		docker pull mcr.microsoft.com/dotnet/sdk
#### .Net 6			docker pull mcr.microsoft.com/dotnet/aspnet:6.0
#### .Net 5			docker pull mcr.microsoft.com/dotnet/aspnet
#### Redis			docker pull redis
#### Consul			docker pull consul
#### Nginx			docker pull nginx


## 部署到 Docker 
1.启动 Redis、Consul 等依赖环境
2.分别生成 Release 版的 Crx.vNext.Repository.dll 和 Crx.vNext.Service.dll
3.发布 Crx.vNext.API
4.将 Crx.vNext.Repository.dll 和 Crx.vNext.Service.dll 拷贝到  Crx.vNext.API 发布目录中
5.修改 Crx.vNext.API 配置文件
6.将 Crx.vNext.API\bin\Release 文件夹上传至服务器
7.创建 Docker 镜像：docker build -t crx.vnext.api -f Dockerfile .
8.启动：docker run --name api1 -p 17687:17687 -d crx.vnext.api

#### 其他：
Redis 启动：docker run --name redis1 -p 6379:6379 -d redis
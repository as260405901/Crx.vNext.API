# [Crx.vNext.API](https://github.com/as260405901/Crx.vNext.API) 微服务架构

## 特点
#### 合理的分层设计
#### AOP 全局异常日志
#### AOP 统一的接口响应格式，统一接口入参校验错误信息格式
#### Service、Repository实现层完全解耦，反射注入
#### 支持分布式Session
#### 集成MiniProfiler到Swagger中
#### 全局使用性能更高的 [System.Text.Json](https://github.com/dotnet/runtime/tree/master/src/libraries/System.Text.Json)
#### 已集成：AOP(面向切面编程)、CORS(跨域)、分布式Session、[Serilog(日志)](https://github.com/serilog/serilog)、[Swagger(接口文档)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)、[AutoFac(IoC 容器)](https://github.com/autofac/Autofac)、[Automapper(实体映射)](https://github.com/AutoMapper/AutoMapper)、[StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis/)、[System.Text.Json](https://github.com/dotnet/runtime/tree/master/src/libraries/System.Text.Json)、[Snowflake(雪花算法)](https://github.com/stulzq/snowflake-net)、[Dapper(ORM)](https://github.com/StackExchange/Dapper)、[Dapper.SimpleCRUD(ORM)](https://github.com/ericdc1/Dapper.SimpleCRUD/)、[MiniProfiler(接口性能)](https://github.com/MiniProfiler/dotnet)
#### 注：截止目前可作为单体架构开发使用，集成JWT即可作为高可用架构开发使用


## 预计集成功能
[Castle(动态代理)](https://github.com/castleproject/Core)、Ocelot(网关)、IDS4(授权中心)、数据库读写分离、Polloy(瞬态故障处理)、Elasticsearch(分布式搜索分析引擎)、SkyAPM(分布式链路追踪)、RabbitMQ(消息队列)、NCC.CAP(分布式定理)


部分代码参考自[Blog.Core](https://github.com/anjoy8/Blog.Core)、[eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers)

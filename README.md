# [Crx.vNext.API](https://github.com/as260405901/Crx.vNext.API) 微服务架构

## 特点
#### 合理的分层设计
#### 全局异常日志
#### 统一的接口响应格式，统一接口入参校验错误信息格式
#### Service、Repository实现层完全解耦，无引用，生产时自动拷贝
#### Service、Repository实现层反射注入，无需手写注入
#### 利用UnitOfWork解决数据库事务问题
#### 全局使用性能更高的 [System.Text.Json](https://github.com/dotnet/runtime/tree/master/src/libraries/System.Text.Json)
#### 已集成：AOP、[Serilog](https://github.com/serilog/serilog)、[Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)、[AutoFac](https://github.com/autofac/Autofac)、[Automapper](https://github.com/AutoMapper/AutoMapper)、[StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis/)、[System.Text.Json](https://github.com/dotnet/runtime/tree/master/src/libraries/System.Text.Json)、雪花算法[Snowflake](https://github.com/stulzq/snowflake-net)、[Dapper](https://github.com/StackExchange/Dapper)、[Dapper.SimpleCRUD](https://github.com/ericdc1/Dapper.SimpleCRUD/)、UnitOfWork
#### 截止目前可作为单体架构开发使用


## 预计集成功能
CORS、Castle、NCC.CAP、Elasticsearch、IDS4 授权中心、MiniProfiler、Polloy、RabbitMQ、SkyAPM、Ocelot 网关、数据库读写分离


部分代码参考自[Blog.Core](https://github.com/anjoy8/Blog.Core)

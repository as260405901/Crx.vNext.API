# [Crx.vNext.API](https://github.com/as260405901/Crx.vNext.API) 微服务架构

## 特点
- **合理的分层设计**
- **模块化的功能开关，按需选择**
- **基于程序集基础类的依赖注入，杜绝过渡封装**
- **集成MiniProfiler至Swagger**
- **AOP：方法间调用性能监控**
- **AOP：方法出入参日志**
- **AOP：全局异常日志**
- **AOP：统一的接口响应格式，统一接口入参校验错误信息格式**
- **实现层完全解耦，反射注入，开发调试时自动编译、拷贝**
- **支持分布式Session**
- **支持服务注册发现**
- **支持服务限流、熔断**
- **集成IDS4实现授权鉴权**
- **支持 Docker 容器**
- **全局使用性能更高的 [System.Text.Json](https://github.com/dotnet/runtime/tree/master/src/libraries/System.Text.Json)**
- **集成：AOP(面向切面编程)、CORS(跨域)、分布式Session(会话保持)、[Castle(动态代理)](https://github.com/castleproject/Core)、[Serilog(日志)](https://github.com/serilog/serilog)、[Swagger(接口文档)](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)、[AutoFac(IoC 容器)](https://github.com/autofac/Autofac)、[Automapper(实体映射)](https://github.com/AutoMapper/AutoMapper)、[StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis/)、[System.Text.Json](https://github.com/dotnet/runtime/tree/master/src/libraries/System.Text.Json)、[Snowflake(雪花算法)](https://github.com/stulzq/snowflake-net)、[Dapper(ORM)](https://github.com/StackExchange/Dapper)、[Dapper.SimpleCRUD(ORM)](https://github.com/ericdc1/Dapper.SimpleCRUD/)、[MiniProfiler(接口性能)](https://github.com/MiniProfiler/dotnet)、[Consul(数据中心)](https://github.com/hashicorp/consul)、[Ocelot(网关)](https://github.com/ThreeMammals/Ocelot)、[IdentityServer4(授权中心)](https://github.com/IdentityServer/IdentityServer4)**

## 预计集成功能
配置中心、Elasticsearch(分布式搜索分析引擎)、SkyAPM(分布式链路追踪)、Polloy(瞬态故障处理)、RabbitMQ(消息队列)、NCC.CAP(分布式定理)、数据库读写分离

##系统架构
![系统架构](https://raw.githubusercontent.com/as260405901/Crx.vNext.API/main/Other/Framework.png)

## Docker 环境基础镜像
``` bash
.Net 5 SDK		docker pull mcr.microsoft.com/dotnet/sdk
.Net 5			docker pull mcr.microsoft.com/dotnet/aspnet
.Net 6 SDK		docker pull mcr.microsoft.com/dotnet/sdk:6.0
.Net 6			docker pull mcr.microsoft.com/dotnet/aspnet:6.0
Redis			docker pull redis
Consul			docker pull consul
Nginx			docker pull nginx
```

## 部署到 Docker
#### 1.自定义网段：
``` bash
docker network create --subnet=172.172.0.0/16 Crx.vNext.Network
```
网络划分： 
    Web服务：172.172.1.0~172.172.100.254
    基础服务：172.172.101.0~172.172.150.254
    其他服务：172.172.151.0~172.172.200.254
    注：每组服务站独立一组网段（内部包含web服务及其独立使用的其他服务地址），剩余网段预留
具体分配：
    Web服务：
        ServiceA_Api*：172.172.1.*
        ServiceB_Api*：172.172.2.*
        ServiceC_Api*：172.172.3.*
    Redis：
        Redis*：172.172.101.*
    Consul：
        Consul_Server*：172.172.102.*
        Consul_Client*：172.172.103.*
    Nginx：
        Consul*：172.172.104.1~172.172.104.50
        Gateway*：172.172.104.51~172.172.104.100
    Authentication：
        Ids4_*：172.172.105.*
    Gateway：
        Ocelot*：172.172.106.*       
#### 2.创建 Docker 镜像：
``` bash
docker build -t crx.vnext.api -f Dockerfile .
docker build -t crx.vnext.authentication -f Dockerfile .
docker build -t crx.vnext.gateway -f Dockerfile .
```
#### 3.创建实例：
**Redis：**
``` bash
docker run -d --network Crx.vNext.Network \
            --name Redis1 --ip 172.172.101.1 -p 6379:6379 \
            redis
```
**Consul 集群-服务端：**
``` bash
docker run -d --network Crx.vNext.Network \
            --restart=always -e 'CONSUL_LOCAL_CONFIG={"skip_leave_on_interrupt": true}' \
            --name Consul_Server1 -h Consul_Server1 --ip 172.172.102.1 \
            -p 8300:8300 -p 8301:8301 -p 8301:8301/udp -p 8302:8302 -p 8302:8302/udp \
            -p 8500:8500 -p 8600:8600 -p 8600:8600/udp \
            consul agent -client=0.0.0.0 -ui -server -data-dir=/tmp/data-dir \
            -bootstrap-expect=2 -bind=172.172.102.1 -node=Consul_Server1

docker run -d --network Crx.vNext.Network \
            --restart=always -e 'CONSUL_LOCAL_CONFIG={"skip_leave_on_interrupt": true}' \
            --name Consul_Server2 -h Consul_Server2 --ip 172.172.102.2 \
            -p 8310:8300 -p 8311:8301 -p 8311:8301/udp -p 8312:8302 -p 8312:8302/udp \
            -p 8510:8500 -p 8610:8600 -p 8610:8600/udp \
            consul agent -client=0.0.0.0 -ui -server -data-dir=/tmp/data-dir \
            -node-id=$(uuidgen | awk '{print tolower($0)}') \
            -join=172.172.102.1 -bind=172.172.102.2 -node=Consul_Server2

docker run -d --network Crx.vNext.Network \
            --restart=always -e 'CONSUL_LOCAL_CONFIG={"skip_leave_on_interrupt": true}' \
            --name Consul_Server3 -h Consul_Server3 --ip 172.172.102.3 \
            -p 8320:8300 -p 8321:8301 -p 8321:8301/udp -p 8322:8302 -p 8322:8302/udp \
            -p 8520:8500 -p 8620:8600 -p 8620:8600/udp \
            consul agent -client=0.0.0.0 -ui -server -data-dir=/tmp/data-dir \
            -node-id=$(uuidgen | awk '{print tolower($0)}') \
            -join=172.172.102.1 -bind=172.172.102.3 -node=Consul_Server3
```
**Consul 集群-客户端：**
``` bash
docker run -d --network Crx.vNext.Network --restart=always \
            -e 'CONSUL_LOCAL_CONFIG={"skip_leave_on_interrupt": true}' \
            --name Consul_Client1 -h Consul_Client1 --ip 172.172.103.1 \
            -p 8350:8300 -p 8351:8301 -p 8351:8301/udp -p 8352:8302 -p 8352:8302/udp \
            -p 8550:8500 -p 8650:8600 -p 8650:8600/udp \
            consul agent -client=0.0.0.0 -node-id=$(uuidgen | awk '{print tolower($0)}') \
            -retry-join=172.172.102.1 -bind=172.172.103.1 -node=Consul_Client1

docker run -d --network Crx.vNext.Network --restart=always \
            -e 'CONSUL_LOCAL_CONFIG={"skip_leave_on_interrupt": true}' \
            --name Consul_Client2 -h Consul_Client2 --ip 172.172.103.2 \
            -p 8360:8300 -p 8361:8301 -p 8361:8301/udp -p 8362:8302 -p 8362:8302/udp \
            -p 8560:8500 -p 8660:8600 -p 8660:8600/udp \
            consul agent -client=0.0.0.0 -node-id=$(uuidgen | awk '{print tolower($0)}') \
            -retry-join=172.172.102.1 -bind=172.172.103.2 -node=Consul_Client2
```
**Nginx-Consul：**
``` bash
docker run -d --network Crx.vNext.Network \
            --name Nginx_Consul1 --ip 172.172.104.1 -p 18500:80 \
            -v /home/crx/Desktop/Config/Nginx/Log/Consul1/:/var/log/nginx/ \
            -v /home/crx/Desktop/Config/Nginx/nginx-consul.conf:/etc/nginx/nginx.conf \
            nginx
```
**Api-ServiceA：**
``` bash
docker run -d --network Crx.vNext.Network \
            --name ServiceA_Api1 --ip 172.172.1.1 -p 8101:80 \
            -v /home/crx/Desktop/Config/Api/ServiceA/appsettings1.json:/App/appsettings.json \
            -v /home/crx/Desktop/Config/Api/ServiceA/Log/1/:/App/Log/ \
            crx.vnext.api

docker run -d --network Crx.vNext.Network \
            --name ServiceA_Api2 --ip 172.172.1.2 -p 8102:80 \
            -v /home/crx/Desktop/Config/Api/ServiceA/appsettings2.json:/App/appsettings.json \
            -v /home/crx/Desktop/Config/Api/ServiceA/Log/2/:/App/Log/ \
            crx.vnext.api
```
**Api-ServiceB：**
``` bash
docker run -d --network Crx.vNext.Network \
            --name ServiceB_Api1 --ip 172.172.2.1 -p 8201:80 \
            -v /home/crx/Desktop/Config/Api/ServiceB/appsettings1.json:/App/appsettings.json \
            -v /home/crx/Desktop/Config/Api/ServiceB/Log/1/:/App/Log/ \
            crx.vnext.api

docker run -d --network Crx.vNext.Network \
            --name ServiceB_Api2 --ip 172.172.2.2 -p 8202:80 \
            -v /home/crx/Desktop/Config/Api/ServiceB/appsettings2.json:/App/appsettings.json \
            -v /home/crx/Desktop/Config/Api/ServiceB/Log/2/:/App/Log/ \
            crx.vnext.api
```
**Authentication：**
``` bash
docker run -d --network Crx.vNext.Network \
            --name Ids4_1 --ip 172.172.105.1 -p 18443:443 \
            crx.vnext.authentication

docker run -d --network Crx.vNext.Network \
            --name Ids4_2 --ip 172.172.105.2 -p 18444:443 \
            crx.vnext.authentication         
```
**Gateway：**
``` bash
docker run -d --network Crx.vNext.Network \
            --name Ocelot_1 --ip 172.172.106.1 -p 7080:80 \
            -v /home/crx/Desktop/Config/Gateway/configuration.json:/app/configuration.json \
            crx.vnext.gateway

docker run -d --network Crx.vNext.Network \
            --name Ocelot_2 --ip 172.172.106.2 -p 7081:80 \
            -v /home/crx/Desktop/Config/Gateway/configuration.json:/app/configuration.json \
            crx.vnext.gateway
```
**Nginx-Gateway：**
``` bash
docker run -d --network Crx.vNext.Network \
            --name Nginx_Ocelot1 --ip 172.172.104.51 -p 18080:80 \
            -v /home/crx/Desktop/Config/Nginx/Log/Gateway1/:/var/log/nginx/ \
            -v /home/crx/Desktop/Config/Nginx/nginx-gateway.conf:/etc/nginx/nginx.conf \
            nginx
```


## Docker 其他常用命令
``` bash
进入实例：
    docker exec -it ID /bin/bash
执行Docker批处理：
    docker-compose up
停用并删除全部容器：
    docker stop $(docker ps -q) & docker rm $(docker ps -aq)
删除全部镜像：
    docker rmi -f $(docker images)
```

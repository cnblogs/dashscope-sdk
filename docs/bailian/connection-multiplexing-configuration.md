> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# DashScope SDK连接复用配置

> 在调用大模型服务时，高并发场景下可能会出现请求超时、资源消耗大等问题。为解决这些问题，可通过连接复用优化网络连接的使用效率。

<meta property="og:title" content="DashScope SDK连接复用配置" />

## 连接复用 <span id="e5b80389damks" />

DashScope SDK 支持通过复用已有的连接来减少资源消耗，提高请求处理效率。

- <strong>Java SDK</strong>：内置连接池机制，支持配置连接数、超时时间等参数，默认启用。
- <strong>Python SDK</strong>：支持通过传入自定义 Session 实现连接复用，包括同步和异步两种方式。

## Java SDK <span id="118335a2de2s2" />

DashScope Java SDK 内置了连接池机制，默认启用。建议您根据具体业务合理调整连接池的连接数和超时时间，优化连接复用效果。

### 配置参数说明 <span id="8bda93ef11hjo" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.92%" }} /><col style={{ width: "29.11%" }} /><col style={{ width: "7.36%" }} /><col style={{ width: "6.1%" }} /><col style={{ width: "34.51%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>含义</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>单位</strong></p></th><th><p><strong>备注</strong></p></th></tr></thead><tbody><tr><td><p>connectTimeout</p></td><td><p>建立连接的超时时间。</p></td><td><p>120</p></td><td><p>秒</p></td><td rowSpan={3}><p>在低延迟场景中，通常需要设置较短的连接超时时间，以减少等待时间，提高响应速度。</p></td></tr><tr><td><p>readTimeout</p></td><td><p>读取数据的超时时间。</p></td><td><p>300</p></td><td><p>秒</p></td></tr><tr><td><p>writeTimeout</p></td><td><p>写入数据的超时时间。</p></td><td><p>60</p></td><td><p>秒</p></td></tr><tr><td><p>connectionIdleTimeout</p></td><td><p>连接池中空闲连接的超时时间。</p></td><td><p>300</p></td><td><p>秒</p></td><td><p>在高并发场景下，适当延长空闲连接超时时间，有利于避免频繁创建连接，从而降低资源消耗。</p></td></tr><tr><td><p>connectionPoolSize</p></td><td><p>连接池中的最大连接数。</p></td><td><p>32</p></td><td><p>个</p></td><td rowSpan={3}><p>在高并发场景下：</p> <ul><li><p>连接数过低时，可能导致请求阻塞或超时，或者频繁创建连接，增加资源消耗；</p></li> <li><p>连接数过高时，可能导致服务端负载过大。</p></li></ul> <p>建议根据业务需求调整配置。</p></td></tr><tr><td><p>maximumAsyncRequests</p></td><td><p>最大并发请求数。全局的并发请求数限制（包含所有主机）。需要小于或等于最大连接数，否则可能出现请求阻塞的情况。</p></td><td><p>32</p></td><td><p>个</p></td></tr><tr><td><p>maximumAsyncRequestsPerHost</p></td><td><p>单台主机的最大并发请求数。需要小于或等于最大并发请求数。</p></td><td><p>32</p></td><td><p>个</p></td></tr></tbody></table>

### 代码示例 <span id="9cdd979f382k9" />

运行代码前，请[配置API Key到环境变量](/zh/model-studio/first-api-call-to-qwen#fcceb7b5d2pqx)并[安装最新版SDK](/zh/model-studio/install-sdk#75aa2ffbb2afo)。

以下代码示例展示了如何配置连接池相关参数（如超时时间、最大连接数等），并调用大模型服务。您可以根据实际需求调整相关参数，以优化并发性能和资源利用率。

```java expandable
// 建议DashScope SDK的版本 >= 2.12.0
import java.time.Duration;
import java.util.Arrays;

import com.alibaba.dashscope.aigc.generation.Generation;
import com.alibaba.dashscope.aigc.generation.GenerationParam;
import com.alibaba.dashscope.aigc.generation.GenerationResult;
import com.alibaba.dashscope.common.Message;
import com.alibaba.dashscope.common.Role;
import com.alibaba.dashscope.exception.ApiException;
import com.alibaba.dashscope.exception.InputRequiredException;
import com.alibaba.dashscope.exception.NoApiKeyException;
import com.alibaba.dashscope.protocol.ConnectionConfigurations;
import com.alibaba.dashscope.utils.Constants;

public class Main {
    public static GenerationResult callWithMessage() throws ApiException, NoApiKeyException, InputRequiredException {
        Generation gen = new Generation();
        Message systemMsg = Message.builder()
                .role(Role.SYSTEM.getValue())
                .content("You are a helpful assistant.")
                .build();
        Message userMsg = Message.builder()
                .role(Role.USER.getValue())
                .content("你是谁？")
                .build();
        GenerationParam param = GenerationParam.builder()
                // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                // 若没有配置环境变量，请用百炼API Key将下行替换为：.apiKey("sk-xxx")
                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                // 此处以qwen-plus为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                .model("qwen-plus")
                .messages(Arrays.asList(systemMsg, userMsg))
                .resultFormat(GenerationParam.ResultFormat.MESSAGE)
                .build();

        System.out.println(userMsg.getContent());
        return gen.call(param);
    }
    public static void main(String[] args) {
        // 连接池配置
        Constants.connectionConfigurations = ConnectionConfigurations.builder()
                .connectTimeout(Duration.ofSeconds(10))  // 建立连接的超时时间, 默认 120s
                .readTimeout(Duration.ofSeconds(300)) // 读取数据的超时时间, 默认 300s
                .writeTimeout(Duration.ofSeconds(60)) // 写入数据的超时时间, 默认 60s
                .connectionIdleTimeout(Duration.ofSeconds(300)) // 连接池中空闲连接的超时时间, 默认 300s
                .connectionPoolSize(256) // 连接池中的最大连接数, 默认 32
                .maximumAsyncRequests(256)  // 最大并发请求数, 默认 32
                .maximumAsyncRequestsPerHost(256) // 单个主机的最大并发请求数, 默认 32
                .build();

        try {
            GenerationResult result = callWithMessage();
            System.out.println(result.getOutput().getChoices().get(0).getMessage().getContent());
        } catch (ApiException | NoApiKeyException | InputRequiredException e) {
            // 使用日志框架记录异常信息
            System.err.println("An error occurred while calling the generation service: " + e.getMessage());
        }
        System.exit(0);
    }
}
```

## Python SDK <span id="e4956fdf79ycl" />

DashScope Python SDK 支持通过传入自定义 Session 实现连接复用，提供[HTTP异步](/zh/model-studio/connection-multiplexing-configuration#41cb49b5d22i8)（协程）和[HTTP同步](/zh/model-studio/connection-multiplexing-configuration#21a3c6f345ipv)两种调用方式。

### HTTP异步调用方式 <span id="41cb49b5d22i8" />

在异步调用场景中，您可以通过 `aiohttp.ClientSession` 配合 `aiohttp.TCPConnector` 实现连接复用。`TCPConnector` 支持配置连接数限制等参数：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.82%" }} /><col style={{ width: "28.88%" }} /><col style={{ width: "9.41%" }} /><col style={{ width: "38.89%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>含义</strong></p></th><th><p><strong>默认值</strong></p></th><th><p><strong>备注</strong></p></th></tr></thead><tbody><tr><td><p>limit</p></td><td><p>总连接数限制</p></td><td><p>100</p></td><td><p>控制最大连接数。在高并发场景下，适当增加此值可以提高并发能力。</p></td></tr><tr><td><p>limit\_per\_host</p></td><td><p>每个主机的连接数限制</p></td><td><p>0（无限制）</p></td><td><p>限制对单个主机的最大连接数，避免对单一服务端造成过大压力。</p></td></tr><tr><td><p>ssl</p></td><td><p>SSL上下文配置</p></td><td><p>None</p></td><td><p>用于HTTPS连接的SSL证书验证配置。</p></td></tr></tbody></table>

#### 代码示例 <span id="3ea96514990hx" />

运行代码前，请[配置API Key到环境变量](/zh/model-studio/first-api-call-to-qwen#fcceb7b5d2pqx)并[安装最新版SDK](/zh/model-studio/install-sdk#75aa2ffbb2afo)。

以下代码示例展示了如何在异步场景下配置连接复用，并调用大模型服务：

```python expandable
import asyncio
import aiohttp
import ssl
import certifi
from dashscope import AioGeneration
import dashscope
import os

async def main():
    # 以下为华北2（北京）地域的URL，各地域的URL不同。
    dashscope.base_http_api_url = 'https://dashscope.aliyuncs.com/api/v1'

    # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    # 若没有配置环境变量，请用百炼API Key将下行替换为：dashscope.api_key = "sk-xxx"
    dashscope.api_key = os.getenv("DASHSCOPE_API_KEY")

    # 配置连接参数
    connector = aiohttp.TCPConnector(
        limit=100,           # 总连接数限制
        limit_per_host=30,   # 每个主机的连接数限制
        ssl=ssl.create_default_context(cafile=certifi.where()),
    )

    # 创建自定义Session并传入调用方法
    async with aiohttp.ClientSession(connector=connector) as session:
        response = await AioGeneration.call(
            model='qwen-plus',
            prompt='你好，请介绍一下你自己',
            session=session,  # 传入自定义 Session
        )
        print(response)

asyncio.run(main())
```

### HTTP同步调用方式 <span id="21a3c6f345ipv" />

在同步调用场景中，您可以通过 `requests.Session` 实现连接复用。在同一个 Session 内的多次请求会复用底层 TCP 连接，避免重复建立连接的开销。

#### 代码示例 <span id="e7906b9440daa" />

运行代码前，请[配置API Key到环境变量](/zh/model-studio/first-api-call-to-qwen#fcceb7b5d2pqx)并[安装最新版SDK](/zh/model-studio/install-sdk#75aa2ffbb2afo)。

以下代码示例展示了如何在同步场景下配置连接复用，并调用大模型服务：

```python
import requests
from dashscope import Generation
import dashscope
import os

# 以下为华北2（北京）地域的URL，各地域的URL不同。
dashscope.base_http_api_url = 'https://dashscope.aliyuncs.com/api/v1'

# 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
# 若没有配置环境变量，请用百炼API Key将下行替换为：dashscope.api_key = "sk-xxx"
dashscope.api_key = os.getenv("DASHSCOPE_API_KEY")

# 使用 with 语句确保 Session 正确关闭
with requests.Session() as session:
    response = Generation.call(
        model='qwen-plus',
        prompt='你好',
        session=session  # 传入自定义 Session
    )
    print(response)
```

如果需要在多次调用中复用同一个 Session，可以采用以下方式：

```python expandable
import requests
from dashscope import Generation
import dashscope
import os

# 以下为华北2（北京）地域的URL，各地域的URL不同。
dashscope.base_http_api_url = 'https://dashscope.aliyuncs.com/api/v1'

# 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
# 若没有配置环境变量，请用百炼API Key将下行替换为：dashscope.api_key = "sk-xxx"
dashscope.api_key = os.getenv("DASHSCOPE_API_KEY")

# 创建 Session 对象
session = requests.Session()

try:
    # 多次调用复用同一个 Session
    response1 = Generation.call(
        model='qwen-plus',
        prompt='你好',
        session=session
    )
    print(response1)

    response2 = Generation.call(
        model='qwen-plus',
        prompt='介绍一下你自己',
        session=session
    )
    print(response2)
finally:
    # 确保 Session 正确关闭
    session.close()
```

## 最佳实践 <span id="7d5ba8ddefncl" />

- <strong>Java SDK</strong>：根据业务并发量合理配置 `connectionPoolSize`、`maximumAsyncRequests` 等参数，避免连接数过高或过低。
- <strong>Python SDK</strong>：推荐使用 `with` 语句自动管理 Session 的生命周期，确保资源正确释放。
- <strong>选择合适的调用方式</strong>：如果您的应用是异步架构（如使用 asyncio、FastAPI 等），建议使用异步调用方式；如果是传统同步架构，使用同步调用方式即可。

## 错误码 <span id="97e145aeabbwf" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

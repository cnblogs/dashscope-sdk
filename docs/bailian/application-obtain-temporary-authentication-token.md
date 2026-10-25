> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 生成临时API Key

> 在浏览器、移动 App 等不可信环境中调用模型服务时，通过安全的后端服务生成临时API Key，避免永久API Key 泄露。

<meta property="og:title" content="生成临时API Key" />

<Tip>
  临时API Key 继承生成它的API Key 所拥有的全部权限，包括对特定模型或知识库的访问限制。
</Tip>

## 前提条件 <span id="prerequisite-title" />

在[密钥管理（北京）](https://bailian.console.aliyun.com/?tab=model#/api-key)、[密钥管理（新加坡）](https://bailian.console.aliyun.com/?tab=model#/api-key)或[密钥管理（弗吉尼亚）](https://bailian.console.aliyun.com/us-east-1?tab=model#/api-key)或[密钥管理（中国香港）](https://bailian.console.aliyun.com/cn-hongkong?tab=globalset#/efm/api_key)页面创建永久API Key，并将其配置为环境变量 `DASHSCOPE_API_KEY`。配置方法请参见[配置API Key 到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## 请求示例 <span id="a983ecd685kjl" />

临时API Key 默认有效期为 60 秒，支持通过 `expire_in_seconds` 参数设置有效期（TTL），范围为 \[1, 1800] 秒。

```bash
curl -X POST "https://dashscope.aliyuncs.com/api/v1/tokens?expire_in_seconds=1800" \
-H "Authorization: Bearer $DASHSCOPE_API_KEY"
```

> 各地域的API Key 不同。

> 以下示例使用新加坡地域的 Endpoint。如需使用北京地域，请将URL替换为：[https://dashscope.aliyuncs.com/api/v1/tokens?expire\_in\_seconds=1800](https://dashscope.aliyuncs.com/api/v1/tokens?expire_in_seconds=1800)

## 响应示例 <span id="a8b797d312yzo" />

<Tabs>
  <Tab title="正常响应示例">
    ```json
    {
        "token":"st-****",
        "expires_at":1744080369
    }
    ```

    ### 响应参数 <span id="6995707ed2t3w" />

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.62%" }} /><col style={{ width: "14.49%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "35.56%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>说明</strong></p></th><th><p><strong>示例</strong></p></th></tr></thead><tbody><tr><td><p>token</p></td><td><p>String</p></td><td><p>生成的临时API Key。</p></td><td><p>st-\*\*\*\*</p></td></tr><tr><td><p>expires\_at</p></td><td><p>Number</p></td><td><p>过期时间，UNIX 时间戳，单位为秒。</p></td><td><p>1744080369</p></td></tr></tbody></table>
  </Tab>

  <Tab title="错误响应示例">
    ```json
    {
        "code":"InvalidApiKey",
        "message":"Invalid API-key provided.",
        "request_id":"902fee3b-f7f0-9a8c-96a1-6b4ea25af114"
    }
    ```

    ### 响应参数 <span id="cd5ad1efc01q6" />

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.62%" }} /><col style={{ width: "14.49%" }} /><col style={{ width: "33.33%" }} /><col style={{ width: "35.56%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>参数类型</strong></p></th><th><p><strong>说明</strong></p></th><th><p><strong>示例</strong></p></th></tr></thead><tbody><tr><td><p>code</p></td><td><p>String</p></td><td><p>错误码。更多错误原因及解决方法，请前往<a href="/zh/model-studio/error-code">错误码</a>页面查询。</p></td><td><p>InvalidApiKey：无效API Key</p></td></tr><tr><td><p>message</p></td><td><p>String</p></td><td><p>错误信息。</p></td><td><p>Invalid API-key provided.</p></td></tr><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>请求 ID。</p></td><td><p>902fee3b-f7f0-9a8c-96a1-6b4ea25af114</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 常见问题

<strong>问：能否手动删除已创建的临时API Key？</strong>

答：不能。临时API Key 具有固定的生命周期，到期后自动失效，无法提前删除。

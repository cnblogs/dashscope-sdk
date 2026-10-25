> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# OpenAI Embedding接口兼容

> 阿里云百炼的Embedding模型兼容OpenAI接口规范。将原有 OpenAI 应用迁移至阿里云百炼只需调整三个参数：

<meta property="og:title" content="OpenAI Embedding接口兼容" />

- base\_url：替换为`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`
- api\_key：替换为[阿里云百炼 API Key](/zh/model-studio/get-api-key)
- model：替换为以下模型列表中的模型名称

<Tip>
  阿里云百炼为华北2（北京）、新加坡、中国香港地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## 支持的模型 <span id="04b110d5870wm" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "16.89%" }} />

    <col style={{ width: "16.8%" }} />

    <col style={{ width: "6.85%" }} />

    <col style={{ width: "9.75%" }} />

    <col style={{ width: "11.39%" }} />

    <col style={{ width: "24.03%" }} />

    <col style={{ width: "14.29%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>模型名称</strong>
      </th>

      <th>
        <strong>向量维度</strong>
      </th>

      <th>
        <strong>最大行数</strong>
      </th>

      <th>
        <strong>单行最大</strong>[Token](/zh/model-studio/billing-for-model-studio)<strong>数</strong>
      </th>

      <th>
        <strong>单价（每千输入Token）</strong>
      </th>

      <th>
        <strong>支持语种</strong>
      </th>

      <th>
        <strong>免费额度</strong>[（注）](/zh/model-studio/new-free-quota#977b13081ab56)
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        qwen3.7-text-embedding
      </td>

      <td>
        2560、2,048、1,536、1,024（默认）、768、512、256
      </td>

      <td>
        20
      </td>

      <td>
        128,000
      </td>

      <td>
        0.0005元

        [Batch调用](/zh/model-studio/batch-interfaces-compatible-with-openai)：0.00025元
      </td>

      <td>
        中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等201种主流语种与方言
      </td>

      <td rowSpan={4}>
        各100万Token

        有效期：百炼开通后90天内
      </td>
    </tr>

    <tr>
      <td>
        qwen3.7-text-embedding-flash
      </td>

      <td>
        1,024（默认）、768、512、256
      </td>

      <td>
        20
      </td>

      <td>
        128,000
      </td>

      <td>
        0.000125元

        [Batch调用](/zh/model-studio/batch-interfaces-compatible-with-openai)：0.000063元
      </td>

      <td>
        中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等201种主流语种与方言
      </td>
    </tr>

    <tr>
      <td>
        text-embedding-v4

        > 属于[Qwen3-Embedding](https://qwenlm.github.io/zh/blog/qwen3-embedding/)系列
      </td>

      <td>
        2,048、1,536、1,024（默认）、768、512、256、128、64
      </td>

      <td rowSpan={2}>
        10
      </td>

      <td rowSpan={2}>
        8,192
      </td>

      <td rowSpan={2}>
        0.0005元

        [Batch调用](/zh/model-studio/batch-interfaces-compatible-with-openai)：0.00025元
      </td>

      <td>
        中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等100+主流语种及多种编程语言
      </td>
    </tr>

    <tr>
      <td>
        text-embedding-v3
      </td>

      <td>
        1,024（默认）、768、512、256、128或64
      </td>

      <td>
        中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语等50+主流语种
      </td>
    </tr>

    <tr>
      <td>
        text-embedding-v2
      </td>

      <td rowSpan={2}>
        1,536
      </td>

      <td rowSpan={2}>
        25
      </td>

      <td rowSpan={2}>
        2,048
      </td>

      <td rowSpan={2}>
        0.0007元

        [Batch调用](/zh/model-studio/batch-interfaces-compatible-with-openai)：0.00035元
      </td>

      <td>
        中文、英语、西班牙语、法语、葡萄牙语、印尼语、日语、韩语、德语、俄罗斯语
      </td>

      <td rowSpan={2}>
        各50万Token

        有效期：百炼开通后90天内
      </td>
    </tr>

    <tr>
      <td>
        text-embedding-v1
      </td>

      <td>
        中文、英语、西班牙语、法语、葡萄牙语、印尼语
      </td>
    </tr>
  </tbody>
</table>

> 多模态Embedding模型（例如qwen3-vl-embedding、tongyi-embedding-vision系列）不支持OpenAI兼容接口。请参见[多模态向量](/zh/model-studio/multimodal-embedding-api-reference)。

<Note>
  OpenAI兼容接口不支持稀疏向量输出。使用 `text-embedding-v4` 时，如需设置 `output_type=sparse`，请调用 DashScope 接口。通过 OpenAI 兼容接口传入 `output_type=sparse` 会返回 HTTP 200，但 embedding 结果为空。
</Note>

## 模型调用 <span id="aad9ffd00arml" />

### 调用示例 <span id="t4GMg" />

本章节提供Python（OpenAI SDK）和cURL（HTTP接口）的字符串输入调用示例，更多编程语言或输入方式示例请参考：[文本与多模态向量化](/zh/model-studio/embedding)。

<Tabs>
  <Tab title="使用OpenAI SDK调用">
    使用OpenAI SDK调用服务，您还需安装[OpenAI SDK](/zh/model-studio/install-sdk)。

    ```python
    import os
    from openai import OpenAI

    client = OpenAI(
        api_key=os.getenv("DASHSCOPE_API_KEY"),  # 如果您没有配置环境变量，请在此处用您的API Key进行替换
        base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"  # 百炼服务的base_url
    )

    completion = client.embeddings.create(
        model="text-embedding-v4",
        input='衣服的质量杠杠的，很漂亮，不枉我等了这么久啊，喜欢，以后还来这里买',
        dimensions=1024, # 指定向量维度（仅 text-embedding-v3及 text-embedding-v4支持该参数）
        encoding_format="float"
    )

    print(completion.model_dump_json())
    ```
  </Tab>

  <Tab title="使用HTTP接口调用">
    ### 提交接口调用 <span id="712f34105ala9" />

    ```http
    POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/embeddings
    ```

    ### 命令行调用 <span id="7d406f850epst" />

    ```bash
    curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/embeddings' \
    --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
    --header 'Content-Type: application/json' \
    --data '{
        "model": "text-embedding-v4",
        "input": "衣服的质量杠杠的，很漂亮，不枉我等了这么久啊，喜欢，以后还来这里买",
        "encoding_format": "float"
    }'
    ```
  </Tab>
</Tabs>

运行代码可以获得以下结果：

<Accordion title="运行结果">
  ```json
  {
    "data": [
      {
        "embedding": [
          0.0023064255,
          -0.009327292,
          ....
          -0.0028842222
        ],
        "index": 0,
        "object": "embedding"
      }
    ],
    "model":"text-embedding-v4",
    "object":"list",
    "usage":{"total_tokens":23},
    "id":"f62c2ae7-0906-9758-ab34-47c5764f07e2"
  }
  ```
</Accordion>

### 异常响应示例 <span id="0ea417000ckjs" />

在访问请求出错的情况下，输出的结果中会通过`code`和`message`指明出错原因。

```json
{
    "error": {
        "message": "Incorrect API key provided. ",
        "type": "invalid_request_error",
        "param": null,
        "code": "invalid_api_key"
    }
}
```

## API参考 <span id="3cd262793blik" />

[通用文本向量接口API详情](/zh/model-studio/text-embedding-synchronous-api)

## 错误码 <span id="efc117f59a2gm" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

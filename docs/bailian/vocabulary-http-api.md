> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 定制热词HTTP API参考

> 通过HTTP API管理定制热词列表，包括创建、查询、更新和删除热词列表。

<meta property="og:title" content="定制热词HTTP API参考" />

<strong>用户指南：</strong>[提升识别准确率](/zh/model-studio/improve-asr-accuracy)。热词列表数量上限等使用限制详见[热词限制与计费](/zh/model-studio/improve-asr-accuracy#hw10_limit_h2)。

<Tip>
  新加坡地域的子业务空间暂不支持热词功能。
</Tip>

## 接口地址 <span id="fe508c8badxjg" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/customization`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/audio/asr/customization`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

## 请求头 <span id="f5b7af17168sz" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24.99%" }} /><col style={{ width: "8.54%" }} /><col style={{ width: "10.32%" }} /><col style={{ width: "56.15%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>是否必选</strong></p></th><th><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td><p>Authorization</p></td><td><p>string</p></td><td><p>是</p></td><td><p>鉴权令牌，格式为<code>Bearer \<your\_api\_key></code>，使用时，将“<code>\<your\_api\_key></code>”替换为实际的API Key。</p></td></tr><tr><td><p>Content-Type</p></td><td><p>string</p></td><td><p>是</p></td><td><p>请求体的媒体类型，固定为<code>application/json</code>。</p></td></tr></tbody></table>

## 创建热词列表 <span id="e441257ef2stq" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="2a1c410015otp" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        定制热词模型，固定为`speech-biasing`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          操作类型，固定为`create_vocabulary`。

          <strong>target\_model</strong> `string`<strong>（必选）</strong>

          使用热词列表的语音识别模型，必须与后续调用语音识别接口时使用的模型一致。

          <strong>prefix</strong> `string`<strong>（必选）</strong>

          热词列表自定义前缀，仅允许数字和小写字母，长度不超过10个字符。

          <strong>vocabulary</strong> `array[object]`<strong>（必选）</strong>

          热词列表数组。

          <Accordion title="属性" defaultOpen>
            <strong>text</strong> `string`<strong>（必选）</strong>

            热词文本。

            热词文本的语言必须在所选模型的支持范围内，不同模型支持的语言各不相同。

            热词用于提升识别的准确率，请使用实际词语而非任意字符组合。

            长度限制：含非 ASCII 字符时不超过 15 个字符；纯 ASCII 时空格分隔片段不超过 7 个。

            <strong>weight</strong> `integer`<strong>（必选）</strong>

            热词权重。常用值：4。

            取值范围：\[1, 5]。

            如果效果不明显，可以适当增加权重，但权重过大可能产生负面效果，导致其他词语识别不准确。

            <strong>lang</strong> `string`（可选）

            待识别音频的语言代码。设置后，系统将对指定语种进行热词识别增强。如果无法提前确定语种，可不设置，模型会自动识别语种。

            取值范围（因模型而异）：

            - Paraformer：

              - zh: 中文
              - en: 英文
              - ja: 日语
              - yue: 粤语
              - ko: 韩语
              - de：德语
              - fr：法语
              - ru：俄语
            - Fun-ASR：

              - zh: 中文
              - en: 英文
              - ja: 日语
          </Accordion>
        </Accordion>
      </td>

      <td>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        新加坡地域和北京地域的API Key不同，详情请参见[获取与配置 API Key](/zh/model-studio/get-api-key)。

        ```bash
        curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/customization \
        -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
        -H "Content-Type: application/json" \
        -d '{
            "model": "speech-biasing",
            "input": {
                "action": "create_vocabulary",
                "target_model": "fun-asr",
                "prefix": "testpfx",
                "vocabulary": [
                  {"text": "赛德克巴莱", "weight": 4}
                ]
            }
        }'
        ```
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 返回体 <span id="0e1c44902ea1e" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>vocabulary\_id</strong>`string`

          创建的热词列表ID。
        </Accordion>

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          创建的热词列表数量，固定为1。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "output": {
                "vocabulary_id": "vocab-testpfx-5112c3de3705486baxxxxxxx"
            },
            "usage": {
                "count": 1
            },
            "request_id": "aee47022-2352-40fe-acfa-xxxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 批量查询热词列表 <span id="0d6d710380ffx" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="aff9713c97xur" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        定制热词模型，固定为`speech-biasing`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          操作类型，固定为`list_vocabulary`。

          <strong>prefix</strong> `string`（可选）

          热词列表自定义前缀，如果设定则只返回指定前缀的热词列表。

          <strong>page\_index</strong> `integer`

          页码索引，从0开始计数。

          默认值：0。

          <strong>page\_size</strong> `integer`

          每页包含数据条数。

          默认值：10。
        </Accordion>
      </td>

      <td>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        新加坡地域和北京地域的API Key不同，详情请参见[获取与配置 API Key](/zh/model-studio/get-api-key)。

        ```bash
        curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/customization \
        -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
        -H "Content-Type: application/json" \
        -d '{
            "model": "speech-biasing",
            "input": {
                "action": "list_vocabulary",
                "prefix": "testpfx",
                "page_index": 0,
                "page_size": 10
            }
        }'
        ```
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 返回体 <span id="f9bba1e5bcz2z" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>vocabulary\_list</strong>`array[object]`

          查询到的热词列表。

          <Accordion title="属性" defaultOpen>
            <strong>vocabulary\_id</strong>`string`

            热词列表ID。

            <strong>gmt\_create</strong>`string`

            创建时间。

            <strong>gmt\_modified</strong>`string`

            修改时间。

            <strong>status</strong>`string`

            状态：

            - OK：可调用
            - UNDEPLOYED：不可调用。
          </Accordion>
        </Accordion>

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          固定为1。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "output": {
                "vocabulary_list": [
                    {
                        "gmt_create": "2026-03-02 18:07:38",
                        "gmt_modified": "2026-03-02 18:07:38",
                        "status": "OK",
                        "vocabulary_id": "vocab-ciotest-8e74bef2accf4xxxxxxxx"
                    },
                    {
                        "gmt_create": "2026-02-27 19:04:48",
                        "gmt_modified": "2026-02-28 13:40:40",
                        "status": "OK",
                        "vocabulary_id": "vocab-sifasr-f483ad46e1844fxxxxxxxx"
                    }
                ]
            },
            "usage": {
                "count": 1
            },
            "request_id": "81d51a05-8cdd-45c0-973f-xxxxxxxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 查询热词列表 <span id="2833029dbddju" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="3cc2e8ade02sf" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        定制热词模型，固定为`speech-biasing`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          操作类型，固定为`query_vocabulary`。

          <strong>vocabulary\_id</strong> `string`<strong>（必选）</strong>

          需要查询的热词列表ID。
        </Accordion>
      </td>

      <td>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        新加坡地域和北京地域的API Key不同，详情请参见[获取与配置 API Key](/zh/model-studio/get-api-key)。

        ```bash
        curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/customization \
        -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
        -H "Content-Type: application/json" \
        -d '{
            "model": "speech-biasing",
            "input": {
                "action": "query_vocabulary",
                "vocabulary_id": "vocab-testpfx-xxxx"
            }
        }'
        ```
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 返回体 <span id="06b012ec7cxrk" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据。

        <Accordion title="属性" defaultOpen>
          <strong>gmt\_create</strong>`string`

          创建时间。

          <strong>gmt\_modified</strong>`string`

          修改时间。

          <strong>status</strong>`string`

          状态：

          - OK：可调用
          - UNDEPLOYED：不可调用。

          <strong>target\_model</strong> `string`

          使用热词列表的语音识别模型，必须与后续调用语音识别接口时使用的模型一致。

          <strong>vocabulary</strong>`array[object]`

          查询到的热词列表。

          <Accordion title="属性" defaultOpen>
            <strong>text</strong> `string`

            热词文本。

            <strong>weight</strong> `integer`

            热词权重。

            <strong>lang</strong> `string`

            待识别音频语种。
          </Accordion>
        </Accordion>

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          固定为1。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "output": {
            "gmt_create": "2025-12-19 11:47:11",
            "gmt_modified": "2025-12-19 11:47:11",
            "status": "OK",
            "target_model": "fun-asr",
            "vocabulary": [
              {
                "lang": "zh",
                "text": "赛德克巴莱",
                "weight": 4
              }
            ]
          },
          "usage": {
            "count": 1
          },
          "request_id": "3d461d3f-b2c4-4de5-xxxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 更新热词列表 <span id="a3353cde38ni9" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="37efc11199nce" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        定制热词模型，固定为`speech-biasing`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          操作类型，固定为`update_vocabulary`。

          <strong>vocabulary\_id</strong> `string`<strong>（必选）</strong>

          需要更新的热词列表ID。

          <strong>vocabulary</strong> `array[object]`<strong>（必选）</strong>

          新的热词列表，将完全替换原有内容。

          <Accordion title="属性" defaultOpen>
            <strong>text</strong> `string`<strong>（必选）</strong>

            热词文本。

            热词文本的语言必须在所选模型的支持范围内，不同模型支持的语言各不相同。

            热词用于提升识别的准确率，请使用实际词语而非任意字符组合。

            长度限制：含非 ASCII 字符时不超过 15 个字符；纯 ASCII 时空格分隔片段不超过 7 个。

            <strong>weight</strong> `integer`<strong>（必选）</strong>

            热词权重。常用值：4。

            取值范围：\[1, 5]。

            如果效果不明显，可以适当增加权重，但权重过大可能产生负面效果，导致其他词语识别不准确。

            <strong>lang</strong> `string`（可选）

            待识别音频的语言代码。设置后，系统将对指定语种进行热词识别增强。如果无法提前确定语种，可不设置，模型会自动识别语种。

            取值范围（因模型而异）：

            - Paraformer：

              - zh: 中文
              - en: 英文
              - ja: 日语
              - yue: 粤语
              - ko: 韩语
              - de：德语
              - fr：法语
              - ru：俄语
            - Fun-ASR：

              - zh: 中文
              - en: 英文
              - ja: 日语
          </Accordion>
        </Accordion>
      </td>

      <td>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        新加坡地域和北京地域的API Key不同，详情请参见[获取与配置 API Key](/zh/model-studio/get-api-key)。

        ```bash
        curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/customization \
        -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
        -H "Content-Type: application/json" \
        -d '{
            "model": "speech-biasing",
            "input": {
                "action": "update_vocabulary",
                "vocabulary_id": "vocab-testpfx-xxx",
                "vocabulary": [
                  {"text": "赛德克巴莱", "weight": 4, "lang": "zh"}
                ]
            }
        }'
        ```
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 返回体 <span id="c596236c58aau" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据，固定为空。

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          更新的热词列表数量，固定为1。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "output": {},
          "usage": {
            "count": 1
          },
          "request_id": "aee47022-2352-40fe-acfa-xxxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## 删除热词列表 <span id="66dd22f7e2fbv" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求体 <span id="ff6650790bvzh" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        定制热词模型，固定为`speech-biasing`。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数对象。

        <Accordion title="属性" defaultOpen>
          <strong>action</strong> `string`<strong>（必选）</strong>

          操作类型，固定为`delete_vocabulary`。

          <strong>vocabulary\_id</strong> `string`<strong>（必选）</strong>

          需要删除的热词列表ID。
        </Accordion>
      </td>

      <td>
        以下为华北2（北京）地域的配置，调用时请将"\{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。

        新加坡地域和北京地域的API Key不同，详情请参见[获取与配置 API Key](/zh/model-studio/get-api-key)。

        ```bash
        curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/asr/customization \
        -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
        -H "Content-Type: application/json" \
        -d '{
            "model": "speech-biasing",
            "input": {
                "action": "delete_vocabulary",
                "vocabulary_id": "vocab-testpfx-xxx"
            }
        }'
        ```
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 返回体 <span id="58f1118820aci" />

        <strong>request\_id</strong>`string`

        本次调用的唯一标识符。

        <strong>output</strong>`object`

        模型返回的数据，固定为空。

        <strong>usage</strong>`object`

        本次请求用量信息。

        <Accordion title="属性" defaultOpen>
          <strong>count</strong> `integer`

          删除的热词列表数量，固定为1。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "output": {},
          "usage": {
            "count": 1
          },
          "request_id": "aee47022-2352-40fe-acfa-xxxx"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

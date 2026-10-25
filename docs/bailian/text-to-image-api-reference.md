> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-文生图V1版API参考

> 本文介绍万相-文生图V1版模型的输入输出参数。

<meta property="og:title" content="万相-文生图V1版API参考" />

<strong>相关指南</strong>：[文本生成图像](/zh/model-studio/text-to-image)

<Tip>
  - 本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
  - 推荐使用全面升级的[文生图V2版模型](/zh/model-studio/text-to-image-v2-api-reference)。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）地域推出了业务空间专属域名 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议从 `https://dashscope.aliyuncs.com` 迁移至新域名。

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## 模型概览 <span id="4a3e46b825d2k" />

<strong>模型简介</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.681499%" }} /><col style={{ width: "82.318501%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型简介</strong></p></th></tr></thead><tbody><tr><td><p>wanx-v1</p></td><td><p>万相-文本生成图像大模型，主要功能包括：</p><ul><li><p>支持中英文双语输入。</p></li><li><p>支持多种图像风格。</p></li><li><p>支持输入参考图片，进行内容或风格迁移，实现更加丰富的风格、主题及派别。</p></li></ul></td></tr></tbody></table>

<strong>模型说明</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.073116%" }} /><col style={{ width: "13.498313%" }} /><col style={{ width: "21.934758%" }} /><col style={{ width: "19.12261%" }} /><col style={{ width: "30.371204%" }} /></colgroup><thead><tr><th rowSpan={2}><p><strong>模型名称</strong></p></th><th rowSpan={2}><p><strong>计费单价</strong></p></th><th colSpan={2}><p><strong>限流（主账号与RAM子账号共用）</strong></p></th><th rowSpan={2}><p><strong>免费额度</strong><a href="/zh/model-studio/new-free-quota">（查看）</a><strong /></p></th></tr><tr><th><p><strong>任务下发接口QPS限制</strong></p></th><th><p><strong>同时处理中任务数量</strong></p></th></tr></thead><tbody><tr><td><p>wanx-v1</p></td><td><p>0.16元/张</p></td><td><p>2</p></td><td><p>1</p></td><td><p>500张</p></td></tr></tbody></table>

更多说明请参见[模型计费及限流](/zh/model-studio/text-to-image-api-reference#b8457b7223zhp)。

## 前提条件 <span id="0e41425c577u4" />

文生图V1版模型API支持通过HTTP和DashScope SDK进行调用。

在调用前，您需要[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。目前，该SDK已支持Python和Java。

> 请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## HTTP调用 <span id="42703589880ts" />

图像模型处理时间较长，为了避免请求超时，HTTP调用仅支持异步获取模型结果。您需要发起两个请求：

1. <strong>创建任务获取任务ID</strong>：首先发起创建任务请求，该请求会返回任务ID（task\_id）。
2. <strong>根据任务ID查询结果</strong>：使用上一步获得的任务ID，查询任务状态及结果。任务成功执行时将返回图像URL，有效期24小时。

<Note>
  创建任务后，该任务将被加入到排队队列，等待调度执行。后续需要调用“根据任务ID查询结果接口”获取任务状态及结果。
</Note>

### 步骤1：创建任务获取任务ID <span id="76829ab923es8" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="afcd41d2b57zf" />

        ##### 请求头（Headers） <span id="79473e45c592d" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        <strong>X-DashScope-WorkSpace</strong>`string`（可选）

        阿里云百炼业务空间ID。示例值：llm-xxxx。

        您可以在此[获取Workspace ID](/zh/model-studio/obtain-the-app-id-and-workspace-id#732535cfc959h)。

        <Accordion title="详细说明">
          此参数根据阿里云百炼API Key进行填写。

          - 若为主账号API Key，可不填。不填则使用主账号权限，填写则使用对应的业务空间权限。
          - 若为RAM子账号API Key，则必填。RAM子账号一定归属于某个业务空间。

          业务空间必须具备访问模型的权限，才能调用API。若无权限，请参考[授权子业务空间模型调用、训练和部署](/zh/model-studio/use-workspace)。

          > 关于如何区分阿里云百炼主账号和RAM子账号，请参考[主账号管理](/zh/model-studio/business-space-management)。
        </Accordion>

        ##### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称。示例值：wanx-v1。

        <strong>input</strong>`object`<strong>（必选）</strong>

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong>`string`<strong>（必选）</strong>

          正向提示词，用来描述生成图像中期望包含的元素和视觉特点。

          支持中英文，长度不超过800个字符，每个汉字/字母占一个字符，超过部分会自动截断。

          示例值：一只坐着的橘黄色的猫，表情愉悦，活泼可爱，逼真准确。

          提示词的使用技巧请参见[文生图Prompt指南](/zh/model-studio/text-to-image-prompt)。

          <strong>negative\_prompt</strong>`string`（可选）

          反向提示词，用来描述不希望在画面中看到的内容，可以对画面进行限制。

          支持中英文，长度不超过500个字符，超过部分会自动截断。

          示例值：低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良等。

          <strong>ref\_img</strong>`string`<strong>（</strong>可选<strong>）</strong>

          参考图像（垫图）的URL地址。模型根据参考图像生成相似风格的图像。

          图像限制：

          - 图片格式：JPG、JPEG、PNG、BMP、TIFF、WEBP等常见格式。
          - 图像大小：不超过10 MB。
          - 图像分辨率：不低于256×256像素且不超过4096×4096像素。
          - URL地址中不能包含中文字符。
        </Accordion>

        <strong>parameters</strong>`object`（可选）

        图像处理参数。

        <Accordion title="属性" defaultOpen>
          <strong>style</strong>`string`（可选）

          输出图像的风格。目前支持以下风格取值：

          <Accordion title="枚举值" defaultOpen>
            - \<auto>：默认值，由模型随机输出图像风格。
            - \<photography>：摄影。
            - \<portrait>：人像写真。
            - \<3d cartoon>：3D卡通。
            - \<anime>：动画。
            - \<oil painting>：油画。
            - \<watercolor>：水彩。
            - \<sketch>：素描。
            - \<chinese painting>：中国画。
            - \<flat illustration>：扁平插画。
          </Accordion>

          <strong>size</strong>`string`（可选）

          输出图像的分辨率。目前支持4种图像分辨率：

          <Accordion title="枚举值" defaultOpen>
            - 1024\*1024：默认值。
            - 720\*1280
            - 768\*1152
            - 1280\*720
          </Accordion>

          <strong>n</strong>`integer`（可选）

          生成图片的数量。取值范围为1\~4张，默认为4。

          <strong>seed</strong>`integer`（可选）

          随机数种子，用于控制模型生成内容的随机性。seed参数取值范围是`[0, 2147483647]`。

          如果不提供，则算法自动生成一个随机数作为种子。如果给定了，则根据`n`的值分别为n张图片生成seed参数，例如n=4，算法将分别生成seed、seed+1、seed+2、seed+3作为参数的图片。

          如果您希望生成内容保持相对稳定，请使用相同的seed参数值。

          <strong>ref\_strength</strong>`float`<strong>（</strong>可选<strong>）</strong>

          控制输出图像与垫图（参考图）的相似度。

          取值范围为\[0.0, 1.0]。取值越大，代表生成的图像与参考图越相似。

          <strong>ref\_mode</strong>`string`<strong>（</strong>可选<strong>）</strong>

          基于垫图（参考图）生成图像的模式。目前支持的模式有：

          <Accordion title="枚举值" defaultOpen>
            - repaint：默认值，基于参考图的内容生成图像。
            - refonly：基于参考图的风格生成图像。
          </Accordion>
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="文字作画">
            <CodeGroup>
              ```bash 正向提示词
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis \
                  -H 'X-DashScope-Async: enable' \
                  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                  -H 'Content-Type: application/json' \
                  -d '{
                  "model": "wanx-v1",
                  "input": {
                      "prompt": "近景镜头，18岁的中国女孩，古代服饰，圆脸，正面看着镜头，民族优雅的服装，商业摄影，室外，电影级光照，半身特写，精致的淡妆，锐利的边缘。"
                  },
                  "parameters": {
                      "style": "<auto>",
                      "size": "1024*1024",
                      "n": 1
                  }
              }'
              ```

              ```bash 正向+反向提示词
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis \
                  -H 'X-DashScope-Async: enable' \
                  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                  -H 'Content-Type: application/json' \
                  -d '{
                  "model": "wanx-v1",
                  "input": {
                      "prompt": "近景镜头，18岁的中国女孩，古代服饰，圆脸，正面看着镜头，民族优雅的服装，商业摄影，室外，电影级光照，半身特写，精致的淡妆，锐利的边缘。",
                      "negative_prompt": "不要使用红色元素"
                  },
                  "parameters": {
                      "style": "<auto>",
                      "size": "1024*1024",
                      "n": 1
                  }
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="参考图生成">
            <CodeGroup>
              ```bash 基于参考图内容
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis \
                  -H 'X-DashScope-Async: enable' \
                  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                  -H 'Content-Type: application/json' \
                  -d '{
                  "model": "wanx-v1",
                  "input": {
                      "prompt": "一个英气的黑发女人，飞舞着金色的蝴蝶，背景中有若隐若现的水墨竹林，高细节，高质量。",
                      "ref_image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241031/rguyzt/girl.png"
                  },
                  "parameters": {
                      "style": "<auto>",
                      "size": "1024*1024",
                      "n": 1,
                      "ref_strength": 1.0,
                      "ref_mode": "repaint"
                  }
              }'
              ```

              ```bash 基于参考图风格
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis \
                  -H 'X-DashScope-Async: enable' \
                  -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                  -H 'Content-Type: application/json' \
                  -d '{
                  "model": "wanx-v1",
                  "input": {
                      "prompt": "有一只黑色的小猫",
                      "ref_image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241031/gpqnqy/house.png"
                  },
                  "parameters": {
                      "style": "<auto>",
                      "size": "1024*1024",
                      "n": 1,
                      "ref_strength": 0.7,
                      "ref_mode": "refonly"
                  }
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="dashscope CLI">
            ```bash
            export DASHSCOPE_API_KEY="your-api-key"
            # 将 {WorkspaceId} 替换为业务空间ID，cn-beijing 替换为对应地域（新加坡: ap-southeast-1, 美东: us-east-1）
            export DASHSCOPE_HTTP_BASE_URL="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"
            dashscope image-synthesis create -m wanx2.1-t2i-turbo -p "一只可爱的猫" -n 1
            ```

            <Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>

            完整地域表见 [Base URL 总览](/zh/model-studio/base-url)。
          </Tab>
        </Tabs>
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
        #### 响应参数 <span id="cd3063363egdc" />

        <strong>output</strong>`object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> `string`

          任务ID。查询有效期24小时。

          <strong>task\_status</strong> `string`

          任务状态。

          <Accordion title="枚举值" defaultOpen>
            - PENDING：任务排队中
            - RUNNING：任务处理中
            - SUCCEEDED：任务执行成功
            - FAILED：任务执行失败
            - CANCELED：任务已取消
            - UNKNOWN：任务不存在或状态未知
          </Accordion>
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。

        <strong>code</strong>`string`

        请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

        <strong>message</strong>`string`

        请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>

      <td>
        <Tabs>
          <Tab title="成功响应">
            请保存 task\_id，用于查询任务状态与结果。

            ```json
            {
                "output": {
                    "task_status": "PENDING",
                    "task_id": "0385dc79-5ff8-4d82-bcb6-xxxxxx"
                },
                "request_id": "4909100c-7b5a-9f92-bfe5-xxxxxx"
            }
            ```
          </Tab>

          <Tab title="异常响应">
            创建任务失败，请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "code": "InvalidApiKey",
                "message": "No API-key provided.",
                "request_id": "7438d53d-6eb8-4596-8835-xxxxxx"
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

### 步骤2：根据任务ID查询结果 <span id="420e6e77a17om" />

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="c7a5b38b94icr" />

        #### 请求头（Headers） <span id="6cbbceaeb6jyo" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        #### URL路径参数（Path parameters） <span id="aa6a7e4730pr2" />

        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            请将`86ecf553-d340-4e21-xxxxxxxxx`替换为真实的task\_id。

            > 若使用新加坡地域的模型，需将base\_url替换为https\://\{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/86ecf553-d340-4e21-xxxxxxxxx，其中\{WorkspaceId}需替换为真实的业务空间ID。

            ```bash
            curl -X GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/86ecf553-d340-4e21-xxxxxxxxx \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY"
            ```
          </Tab>
        </Tabs>
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
        #### 响应参数 <span id="3f1ae57ce6qd5" />

        <strong>output</strong>`object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> `string`

          任务ID。查询有效期24小时。

          <strong>task\_status</strong> `string`

          任务状态。

          <Accordion title="枚举值" defaultOpen>
            - PENDING：任务排队中
            - RUNNING：任务处理中
            - SUCCEEDED：任务执行成功
            - FAILED：任务执行失败
            - CANCELED：任务已取消
            - UNKNOWN：任务不存在或状态未知
          </Accordion>

          <strong>task\_metrics</strong> `object`

          任务结果统计。

          <Accordion title="属性" defaultOpen>
            <strong>TOTAL</strong> `integer`

            总的任务数。

            <strong>SUCCEEDED</strong> `integer`

            任务状态为成功的任务数。

            <strong>FAILED</strong> `integer`

            任务状态为失败的任务数。
          </Accordion>

          <strong>results</strong> `array of object`

          任务结果列表，包括图像URL、部分任务执行失败报错信息等。

          <Accordion title="数据结构">
            ```json
            {
                "results": [
                    {
                        "url": ""
                    },
                    {
                        "code": "",
                        "message": ""
                    }
                ]
            }
            ```
          </Accordion>

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          模型成功生成图片的数量。计费公式：费用 = 图片数量 × 单价。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            ```json
            {
                "request_id": "85eaba38-0185-99d7-8d16-4d9135238846",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-a0c6a421c010",
                    "task_status": "SUCCEEDED",
                    "results": [
                        {
                            "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/123/a1.png"
                        },
                        {
                            "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/123/b2.png"
                        }
                    ],
                    "task_metrics": {
                        "TOTAL": 2,
                        "SUCCEEDED": 2,
                        "FAILED": 0
                    }
                },
                "usage": {
                    "image_count": 2
                }
            }
            ```
          </Tab>

          <Tab title="任务执行失败">
            若任务执行失败，task\_status将置为 FAILED，并提供错误码和信息。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "e5d70b02-ebd3-98ce-9fe8-759d7d7b107d",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-a0c6a421c010",
                    "task_status": "FAILED",
                    "code": "InvalidParameter",
                    "message": "The size is not match the allowed size ['1024*1024', '720*1280', '1280*720']",
                    "task_metrics": {
                        "TOTAL": 4,
                        "SUCCEEDED": 0,
                        "FAILED": 4
                    }
                }
            }
            ```
          </Tab>

          <Tab title="任务部分失败">
            模型可以在一次任务中生成多张图片。只要有一张图片生成成功，任务状态将标记为`SUCCEEDED`，并且返回相应的图像URL。对于生成失败的图片，结果中会返回相应的失败原因。同时在usage统计中，只会对成功的结果计数。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "85eaba38-0185-99d7-8d16-4d9135238846",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-a0c6a421c010",
                    "task_status": "SUCCEEDED",
                    "results": [
                        {
                            "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/123/a1.png"
                        },
                        {
                            "code": "InternalError.Timeout",
                            "message": "An internal timeout error has occured during execution, please try again later or contact service support."
                        }
                    ],
                    "task_metrics": {
                        "TOTAL": 2,
                        "SUCCEEDED": 1,
                        "FAILED": 1
                    }
                },
                "usage": {
                    "image_count": 1
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## DashScope SDK调用 <span id="db17b3098c2iv" />

请先确认已安装最新版DashScope SDK，否则可能运行报错：[安装SDK](/zh/model-studio/install-sdk)。

DashScope SDK目前已支持Python和Java。

SDK与HTTP接口的参数名基本一致，参数结构根据不同语言的SDK封装而定。参数说明可参考[HTTP调用](/zh/model-studio/text-to-image-api-reference#42703589880ts)。

由于图像模型处理时间较长，底层服务采用异步方式提供。SDK在上层进行了封装，支持同步、异步两种调用方式。

### Python SDK调用 <span id="a3ad9a3b6d9if" />

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="3cd4969b00193" />

    <CodeGroup>
      ```python 文字作画 expandable
      from http import HTTPStatus
      from urllib.parse import urlparse, unquote
      from pathlib import PurePosixPath
      import requests
      import dashscope
      from dashscope import ImageSynthesis
      import os

      # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
      dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

      prompt = "近景镜头，18岁的中国女孩，古代服饰，圆脸，正面看着镜头，民族优雅的服装，商业摄影，室外，电影级光照，半身特写，精致的淡妆，锐利的边缘。"

      print('----sync call, please wait a moment----')
      rsp = ImageSynthesis.call(api_key=os.getenv("DASHSCOPE_API_KEY"),
                                model=ImageSynthesis.Models.wanx_v1,
                                prompt=prompt,
                                n=1,
                                style='<watercolor>',
                                size='1024*1024')
      print('response: %s' % rsp)
      if rsp.status_code == HTTPStatus.OK:
          # 在当前目录下保存图片
          for result in rsp.output.results:
              file_name = PurePosixPath(unquote(urlparse(result.url).path)).parts[-1]
              with open('./%s' % file_name, 'wb+') as f:
                  f.write(requests.get(result.url).content)
      else:
          print('sync_call Failed, status_code: %s, code: %s, message: %s' %
                (rsp.status_code, rsp.code, rsp.message))
      ```

      ```python 参考图生成 expandable
      from http import HTTPStatus
      from urllib.parse import urlparse, unquote
      from pathlib import PurePosixPath
      import requests
      import dashscope
      from dashscope import ImageSynthesis
      import os

      # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
      dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

      prompt = "近景镜头，18岁的中国女孩，古代服饰，圆脸，正面看着镜头，民族优雅的服装，商业摄影，室外，电影级光照，半身特写，精致的淡妆，锐利的边缘。"

      # 上传参考图方式：url链接和本地路径二选一
      # 若两者存在，ref_img参数优先级更高
      # 使用公网url链接
      ref_img = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241031/rguyzt/girl.png"
      # 使用本地文件路径
      sketch_image_url = './girl.png'

      print('----sync call, please wait a moment----')
      rsp = ImageSynthesis.call(api_key=os.getenv("DASHSCOPE_API_KEY"),
                                model=ImageSynthesis.Models.wanx_v1,
                                prompt=prompt,
                                n=1,
                                style='<auto>',
                                size='1024*1024',
                                ref_mode='repaint',
                                ref_strength=1.0,
                                # sketch_image_url=sketch_image_url,
                                ref_img=ref_img)
      print(rsp)
      if rsp.status_code == HTTPStatus.OK:
          print(rsp.output)
          # 保留图片到当前目录
          for result in rsp.output.results:
              file_name = PurePosixPath(unquote(urlparse(result.url).path)).parts[-1]
              with open('./%s' % file_name, 'wb+') as f:
                  f.write(requests.get(result.url).content)
      else:
          print('sync_call Failed, status_code: %s, code: %s, message: %s' %
                (rsp.status_code, rsp.code, rsp.message))
      ```
    </CodeGroup>

    ##### 响应示例 <span id="0c0533477emwb" />

    ```json
    {
    	"status_code": 200,
    	"request_id": "4126d9dd-e037-9f32-8d56-6d29ab3f9a06",
    	"code": null,
    	"message": "",
    	"output": {
    		"task_id": "b476bc4e-35c1-4c4e-a4d9-xxxxxxx",
    		"task_status": "SUCCEEDED",
    		"results": [{
    			"url": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxxx.png"
    		}],
    		"submit_time": "2024-11-01 09:50:56.081",
    		"scheduled_time": "2024-11-01 09:50:56.104",
    		"end_time": "2024-11-01 09:51:22.740",
    		"task_metrics": {
    			"TOTAL": 1,
    			"SUCCEEDED": 1,
    			"FAILED": 0
    		}
    	},
    	"usage": {
    		"image_count": 1
    	}
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="e5003f55e8kjn" />

    <CodeGroup>
      ```python 文字作画 expandable
      from http import HTTPStatus
      from urllib.parse import urlparse, unquote
      from pathlib import PurePosixPath
      import requests
      import dashscope
      from dashscope import ImageSynthesis
      import os

      # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
      dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

      prompt = "近景镜头，18岁的中国女孩，古代服饰，圆脸，正面看着镜头，民族优雅的服装，商业摄影，室外，电影级光照，半身特写，精致的淡妆，锐利的边缘。"

      def async_call():
          print('----create task----')
          task_info = create_async_task()
          print('----wait task done then save image----')
          wait_async_task(task_info)

      # 创建异步任务
      def create_async_task():
          rsp = ImageSynthesis.async_call(api_key=os.getenv("DASHSCOPE_API_KEY"),
                                          model=ImageSynthesis.Models.wanx_v1,
                                          prompt=prompt,
                                          n=1,
                                          style='<watercolor>',
                                          size='1024*1024')
          print(rsp)
          if rsp.status_code == HTTPStatus.OK:
              print(rsp.output)
          else:
              print('Failed, status_code: %s, code: %s, message: %s' %
                    (rsp.status_code, rsp.code, rsp.message))
          return rsp

      # 等待异步任务结束
      def wait_async_task(task):
          rsp = ImageSynthesis.wait(task, api_key=os.getenv("DASHSCOPE_API_KEY"))
          print(rsp)
          if rsp.status_code == HTTPStatus.OK:
              print(rsp.output)
              for result in rsp.output.results:
                  file_name = PurePosixPath(unquote(urlparse(result.url).path)).parts[-1]
                  with open('./%s' % file_name, 'wb+') as f:
                      f.write(requests.get(result.url).content)
          else:
              print('Failed, status_code: %s, code: %s, message: %s' %
                    (rsp.status_code, rsp.code, rsp.message))

      # 获取异步任务信息
      def fetch_task_status(task):
          status = ImageSynthesis.fetch(task, api_key=os.getenv("DASHSCOPE_API_KEY"))
          print(status)
          if status.status_code == HTTPStatus.OK:
              print(status.output.task_status)
          else:
              print('Failed, status_code: %s, code: %s, message: %s' %
                    (status.status_code, status.code, status.message))

      # 取消异步任务，只有处于PENDING状态的任务才可以取消
      def cancel_task(task):
          rsp = ImageSynthesis.cancel(task, api_key=os.getenv("DASHSCOPE_API_KEY"))
          print(rsp)
          if rsp.status_code == HTTPStatus.OK:
              print(rsp.output.task_status)
          else:
              print('Failed, status_code: %s, code: %s, message: %s' %
                    (rsp.status_code, rsp.code, rsp.message))

      if __name__ == '__main__':
          async_call()
      ```

      ```python 参考图生成 expandable
      from http import HTTPStatus
      from urllib.parse import urlparse, unquote
      from pathlib import PurePosixPath
      import requests
      import dashscope
      from dashscope import ImageSynthesis
      import os

      # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
      dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

      prompt = "近景镜头，18岁的中国女孩，古代服饰，圆脸，正面看着镜头，民族优雅的服装，商业摄影，室外，电影级光照，半身特写，精致的淡妆，锐利的边缘。"

      # 上传参考图方式：url链接和本地路径二选一
      # 若两者存在，ref_img参数优先级更高
      # 使用公网url链接
      ref_img = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241031/rguyzt/girl.png"
      # 使用本地文件路径
      sketch_image_url = './girl.png'

      def async_call():
          print('----create task----')
          task_info = create_async_task()
          print('----wait task done then save image----')
          wait_async_task(task_info)

      # 创建异步任务
      def create_async_task():
          rsp = ImageSynthesis.async_call(api_key=os.getenv("DASHSCOPE_API_KEY"),
                                          model=ImageSynthesis.Models.wanx_v1,
                                          prompt=prompt,
                                          n=1,
                                          style='<auto>',
                                          size='1024*1024',
                                          ref_mode='repaint',
                                          ref_strength=1.0,
                                          # sketch_image_url=sketch_image_url,
                                          ref_img=ref_img)
          print(rsp)
          if rsp.status_code == HTTPStatus.OK:
              print(rsp.output)
          else:
              print('create_async_task Failed, status_code: %s, code: %s, message: %s' %
                    (rsp.status_code, rsp.code, rsp.message))
          return rsp

      # 等待异步任务结束
      def wait_async_task(task):
          rsp = ImageSynthesis.wait(task, api_key=os.getenv("DASHSCOPE_API_KEY"))
          print(rsp)
          if rsp.status_code == HTTPStatus.OK:
              for result in rsp.output.results:
                  file_name = PurePosixPath(unquote(urlparse(result.url).path)).parts[-1]
                  with open('./%s' % file_name, 'wb+') as f:
                      f.write(requests.get(result.url).content)
          else:
              print('Failed, status_code: %s, code: %s, message: %s' %
                    (rsp.status_code, rsp.code, rsp.message))

      if __name__ == '__main__':
          async_call()
      ```
    </CodeGroup>

    ##### 响应示例 <span id="ae28dc8f72g6i" />

    1、创建任务的响应示例

    ```json
    {
    	"status_code": 200,
    	"request_id": "31b04171-011c-96bd-ac00-f0383b669cc7",
    	"code": "",
    	"message": "",
    	"output": {
    		"task_id": "4f90cf14-a34e-4eae-xxxxxxxx",
    		"task_status": "PENDING",
    		"results": []
    	},
    	"usage": null
    }
    ```

    2、查询任务结果的响应示例

    ```json
    {
    	"status_code": 200,
    	"request_id": "d861d3ba-4b29-9491-abad-266ef4fb2f08",
    	"code": null,
    	"message": "",
    	"output": {
    		"task_id": "4f90cf14-a34e-4eae-xxxxxxxx",
    		"task_status": "SUCCEEDED",
    		"results": [{
    			"url": "https://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/xxxx.png"
    		}],
    		"submit_time": "2024-10-31 20:40:35.631",
    		"scheduled_time": "2024-10-31 20:40:35.684",
    		"end_time": "2024-10-31 20:41:02.700",
    		"task_metrics": {
    			"TOTAL": 1,
    			"SUCCEEDED": 1,
    			"FAILED": 0
    		}
    	},
    	"usage": {
    		"image_count": 1
    	}
    }
    ```
  </Tab>
</Tabs>

### Java SDK调用 <span id="589b80853e6rn" />

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="b568661fa6mie" />

    <CodeGroup>
      ```java 文字作画 expandable
      // Copyright (c) Alibaba, Inc. and its affiliates.

      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisListResult;
      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
      import com.alibaba.dashscope.task.AsyncTaskListParam;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.utils.JsonUtils;
      import com.alibaba.dashscope.utils.Constants;

      public class Main {
          // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}
          public static void basicCall() throws ApiException, NoApiKeyException {
              String prompt = "近景镜头，18岁的中国女孩，古代服饰，圆脸，正面看着镜头，民族优雅的服装，商业摄影，室外，电影级光照，半身特写，精致的淡妆，锐利的边缘。";
              ImageSynthesisParam param =
                      ImageSynthesisParam.builder()
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model(ImageSynthesis.Models.WANX_V1)
                              .prompt(prompt)
                              .style("<watercolor>")
                              .n(1)
                              .size("1024*1024")
                              .build();

              ImageSynthesis imageSynthesis = new ImageSynthesis();
              ImageSynthesisResult result = null;
              try {
                  System.out.println("---sync call, please wait a moment----");
                  result = imageSynthesis.call(param);
              } catch (ApiException | NoApiKeyException e){
                  throw new RuntimeException(e.getMessage());
              }
              System.out.println(JsonUtils.toJson(result));
          }

          public static void listTask() throws ApiException, NoApiKeyException {
              ImageSynthesis is = new ImageSynthesis();
              AsyncTaskListParam param = AsyncTaskListParam.builder().build();
              ImageSynthesisListResult result = is.list(param);
              System.out.println(result);
          }

          public void fetchTask() throws ApiException, NoApiKeyException {
              String taskId = "your task id";
              ImageSynthesis is = new ImageSynthesis();
              // If set DASHSCOPE_API_KEY environment variable, apiKey can null.
              ImageSynthesisResult result = is.fetch(taskId, null);
              System.out.println(result.getOutput());
              System.out.println(result.getUsage());
          }

          public static void main(String[] args){
              try{
                  basicCall();
                  //listTask();
              }catch(ApiException|NoApiKeyException e){
                  System.out.println(e.getMessage());
              }
          }
      }
      ```

      ```java 相似图生成 expandable
      // Copyright (c) Alibaba, Inc. and its affiliates.

      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.utils.JsonUtils;
      import com.alibaba.dashscope.utils.Constants;

      import java.util.HashMap;

      public class Main {
          // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

          public void syncCall() {
              String prompt = "近景镜头，18岁的中国女孩，古代服饰，圆脸，正面看着镜头，民族优雅的服装，商业摄影，室外，电影级光照，半身特写，精致的淡妆，锐利的边缘。";
              //使用公网url链接
              String refImage = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241031/rguyzt/girl.png";
              HashMap<String,Object> parameters = new HashMap<>();
              parameters.put("ref_strength", 0.5);
              parameters.put("ref_mode", "repaint");

              ImageSynthesisParam param =
                      ImageSynthesisParam.builder()
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model(ImageSynthesis.Models.WANX_V1)
                              .prompt(prompt)
                              .style("<auto>")
                              .n(1)
                              .size("1024*1024")
                              .refImage(refImage)
                              .parameters(parameters)
                              .build();

              ImageSynthesis imageSynthesis = new ImageSynthesis();
              ImageSynthesisResult result = null;
              try {
                  System.out.println("---sync call, please wait a moment----");
                  result = imageSynthesis.call(param);
              } catch (ApiException|NoApiKeyException e){
                  throw new RuntimeException(e.getMessage());
              }
              System.out.println(JsonUtils.toJson(result));
          }

          public static void main(String[] args){
              Main text2Image = new Main();
              text2Image.syncCall();
          }

      }
      ```
    </CodeGroup>

    ##### 响应示例 <span id="2539143f9bnpo" />

    ```json
    {
    	"request_id": "150edcda-05d5-9ffe-8803-84626d1db623",
    	"output": {
    		"task_id": "f2098ff0-146e-404c-bb25-xxxxxxxx",
    		"task_status": "SUCCEEDED",
    		"results": [{
    			"url": "https://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/xxxx.png"
    		}],
    		"task_metrics": {
    			"TOTAL": 1,
    			"SUCCEEDED": 1,
    			"FAILED": 0
    		}
    	},
    	"usage": {
    		"image_count": 1
    	}
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="2fc30f223b9py" />

    <CodeGroup>
      ```java 文字作画 expandable
      // Copyright (c) Alibaba, Inc. and its affiliates.

      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.utils.JsonUtils;
      import com.alibaba.dashscope.utils.Constants;

      public class Main {
          // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

          public void asyncCall() {
              System.out.println("---create task----");
              String taskId = this.createAsyncTask();
              System.out.println("---wait task done then return image url----");
              this.waitAsyncTask(taskId);
          }

          /**
           * 创建异步任务
           * @return taskId
           */
          public String createAsyncTask() {
              String prompt = "近景镜头，18岁的中国女孩，古代服饰，圆脸，正面看着镜头，民族优雅的服装，商业摄影，室外，电影级光照，半身特写，精致的淡妆，锐利的边缘。";
              ImageSynthesisParam param =
                      ImageSynthesisParam.builder()
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model(ImageSynthesis.Models.WANX_V1)
                              .prompt(prompt)
                              .style("<watercolor>")
                              .n(1)
                              .size("1024*1024")
                              .build();

              ImageSynthesis imageSynthesis = new ImageSynthesis();
              ImageSynthesisResult result = null;
              try {
                  result = imageSynthesis.asyncCall(param);
              } catch (Exception e){
                  throw new RuntimeException(e.getMessage());
              }
              System.out.println(JsonUtils.toJson(result));
              String taskId = result.getOutput().getTaskId();
              System.out.println("taskId=" + taskId);
              return taskId;
          }

          /**
           * 等待异步任务结束
           * @param taskId 任务id
           * */
          public void waitAsyncTask(String taskId) {
              ImageSynthesis imageSynthesis = new ImageSynthesis();
              ImageSynthesisResult result = null;
              try {
                  //环境变量配置后，可在这里将apiKey设置为null
                  result = imageSynthesis.wait(taskId, null);
              } catch (ApiException | NoApiKeyException e){
                  throw new RuntimeException(e.getMessage());
              }
              System.out.println(JsonUtils.toJson(result));
              System.out.println(JsonUtils.toJson(result.getOutput()));
          }

          public static void main(String[] args){
              Main main = new Main();
              main.asyncCall();
          }
      }
      ```

      ```java 相似图生成 expandable
      // Copyright (c) Alibaba, Inc. and its affiliates.

      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
      import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
      import com.alibaba.dashscope.exception.ApiException;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.utils.JsonUtils;
      import com.alibaba.dashscope.utils.Constants;

      public class Main {
          // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          static {Constants.baseHttpApiUrl="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";}

          public void asyncCall() {
              System.out.println("---create task----");
              String taskId = this.createAsyncTask();
              System.out.println("---wait task done then return image url----");
              this.waitAsyncTask(taskId);
          }

          /**
           * 创建异步任务
           * @return taskId
           */
          public String createAsyncTask() {
              String prompt = "近景镜头，18岁的中国女孩，古代服饰，圆脸，正面看着镜头，民族优雅的服装，商业摄影，室外，电影级光照，半身特写，精致的淡妆，锐利的边缘。";
              String refImage = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241031/rguyzt/girl.png";
              ImageSynthesisParam param =
                      ImageSynthesisParam.builder()
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .model(ImageSynthesis.Models.WANX_V1)
                              .prompt(prompt)
                              .style("<auto>")
                              .n(1)
                              .size("1024*1024")
                              .refImage(refImage)
                              .build();

              ImageSynthesis imageSynthesis = new ImageSynthesis();
              ImageSynthesisResult result = null;
              try {
                  result = imageSynthesis.asyncCall(param);
              } catch (ApiException | NoApiKeyException e){
                  throw new RuntimeException(e.getMessage());
              }
              String taskId = result.getOutput().getTaskId();
              System.out.println("taskId=" + taskId);
              return taskId;
          }

          /**
           * 等待异步任务结束
           * @param taskId 任务id
           * */
          public void waitAsyncTask(String taskId) {
              ImageSynthesis imageSynthesis = new ImageSynthesis();
              ImageSynthesisResult result = null;
              try {
                  // If you have set the DASHSCOPE_API_KEY in the system environment variable, the apiKey can be null.
                  result = imageSynthesis.wait(taskId, null);
              } catch (ApiException|NoApiKeyException e){
                  throw new RuntimeException(e.getMessage());
              }

              System.out.println(JsonUtils.toJson(result.getOutput()));
          }

          public static void main(String[] args){
              Main text2Image = new Main();
              text2Image.asyncCall();
          }
      }
      ```
    </CodeGroup>

    ##### 响应示例 <span id="adf8613a57bqa" />

    1、创建任务的响应示例

    ```json
    {
    	"request_id": "5dbf9dc5-4f4c-9605-85ea-542f97709ba8",
    	"output": {
    		"task_id": "7277e20e-aa01-4709-xxxxxxxx",
    		"task_status": "PENDING"
    	}
    }
    ```

    2、查询任务结果的响应示例

    ```json
    {
    	"request_id": "c44213ba-7aa3-91e4-97c1-c527ade82597",
    	"output": {
    		"task_id": "7277e20e-aa01-4709-xxxxxxxx",
    		"task_status": "SUCCEEDED",
    		"results": [{
    			"url": "https://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/xxxx.png"
    		}],
    		"task_metrics": {
    			"TOTAL": 1,
    			"SUCCEEDED": 1,
    			"FAILED": 0
    		}
    	},
    	"usage": {
    		"image_count": 1
    	}
    }
    ```
  </Tab>
</Tabs>

## 图片输出存储域名 <span id="h7i8j9k0l1m2n" />

通义万相图片生成服务会将生成的图片存储在OSS中，生成结果URL中的域名根据地域不同而不同。如果您的企业网络环境有出口白名单限制，需要将以下OSS域名加入白名单才能正常获取生成的图片结果。

目前支持以下10个地域的出口域名：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30%" }} /><col style={{ width: "70%" }} /></colgroup><tbody><tr><td><p><strong>地域</strong></p></td><td><p><strong>OSS域名</strong></p></td></tr><tr><td><p>华北2（北京）</p></td><td><p><code>dashscope-result-bj.oss-cn-beijing.aliyuncs.com</code></p></td></tr><tr><td><p>华东1（杭州）</p></td><td><p><code>dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com</code></p></td></tr><tr><td><p>华东2（上海）</p></td><td><p><code>dashscope-result-sh.oss-cn-shanghai.aliyuncs.com</code></p></td></tr><tr><td><p>华北6（乌兰察布）</p></td><td><p><code>dashscope-result-wlcb.oss-cn-wulanchabu.aliyuncs.com</code></p></td></tr><tr><td><p>华北3（张家口）</p></td><td><p><code>dashscope-result-zjk.oss-cn-zhangjiakou.aliyuncs.com</code></p></td></tr><tr><td><p>华南1（深圳）</p></td><td><p><code>dashscope-result-sz.oss-cn-shenzhen.aliyuncs.com</code></p></td></tr><tr><td><p>华南2（河源）</p></td><td><p><code>dashscope-result-hy.oss-cn-heyuan.aliyuncs.com</code></p></td></tr><tr><td><p>西南1（成都）</p></td><td><p><code>dashscope-result-cd.oss-cn-chengdu.aliyuncs.com</code></p></td></tr><tr><td><p>华南3（广州）</p></td><td><p><code>dashscope-result-gz.oss-cn-guangzhou.aliyuncs.com</code></p></td></tr><tr><td><p>华北6（乌兰察布）ACDR</p></td><td><p><code>dashscope-result-wlcb-acdr-1.oss-cn-wulanchabu-acdr-1.aliyuncs.com</code></p></td></tr></tbody></table>

## 错误码 <span id="4533746f2c15t" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="6d3e655950tcy" />

### 模型计费及限流 <span id="b8457b7223zhp" />

<strong>免费额度</strong>

- 额度说明：免费额度是指模型成功生成的输出图片数量。输入图片及模型处理失败的情况不占用免费额度。
- 领取方式：开通阿里云百炼大模型服务后自动发放，有效期90天。
- 使用账号：阿里云主账号与其RAM子账号共享免费额度。
- 更多详情请参见[新人免费额度](/zh/model-studio/new-free-quota)。

<strong>限时免费</strong>

- 当计费为限时免费时，表示该模型处于公测阶段，免费额度用尽后不可使用。

<strong>计费说明</strong>

- 当计费有明确单价时，如0.2元/秒，表示该模型已商业化，免费额度用尽或过期后需付费使用。
- 计费项：只对模型成功生成的输出图片进行收费，其余情况暂不计费。
- 付费方式：由阿里云主账号统一付费。RAM子账号不能独立计量计费，必须由所属的主账号付费。如果您需要查询账单信息，请前往阿里云控制台[账单概览](https://billing-cost.console.aliyun.com/finance/month-bill/account)。
- 充值途径：您可以在阿里云控制台[费用与成本](https://billing-cost.console.aliyun.com/home?spm=a2c4g.11186623.0.0.2d543048F4KRQP)页面进行充值。
- 模型调用情况：您可以前往阿里云百炼的[模型监控](https://bailian.console.aliyun.com/cn-beijing/model/telemetry)查看模型调用量及调用次数。
- 更多计费问题请参见[计费项](/zh/model-studio/billing-for-model-studio)。

<strong>限流</strong>

- 限流说明：阿里云主账号与其RAM子账号共享限流限制。

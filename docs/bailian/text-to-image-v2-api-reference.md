> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-文生图V2版API参考

> 万相-文生图模型基于文本生成图像，支持多种艺术风格与写实摄影效果，满足多样化创意需求。

<meta property="og:title" content="万相-文生图V2版API参考" />

<strong>快速入口：</strong>在线体验（[北京](https://bailian.console.aliyun.com/model/experience/vision/imageGenerate?modelId=wan2.6-t2i) | [新加坡](https://bailian.console.aliyun.com/model/experience/vision/imageGenerate)| [弗吉尼亚](https://bailian.console.aliyun.com/us-east-1/model/experience/vision/imageGenerate?modelId=wan2.6-t2i)） | [万相官网](https://tongyi.aliyun.com/wan/generate/image/text-to-image) | [文生图使用指南](/zh/model-studio/text-to-image)

<Note>
  万相官网的功能与API支持的能力可能存在差异。本文档以API的实际能力为准，并会随功能更新及时同步。
</Note>

## 模型概览 <span id="4a3e46b825d2k" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "21.941355%" }} />

    <col style={{ width: "47.320526%" }} />

    <col style={{ width: "30.738119%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>模型名称</strong>
      </th>

      <th>
        <strong>模型简介</strong>
      </th>

      <th>
        <strong>输出图像格式</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        wan2.6-t2i `推荐`
      </td>

      <td>
        万相2.6

        支持在总像素面积与宽高比约束内，自由选尺寸（同wan2.5）
      </td>

      <td rowSpan={2}>
        图像分辨率：总像素在\[1280*1280, 1440*1440]之间

        图像宽高比：\[1:4, 4:1]<br />图像格式：png<br /><br />
      </td>
    </tr>

    <tr>
      <td>
        wan2.5-t2i-preview `推荐`
      </td>

      <td>
        万相2.5 preview

        支持在总像素面积与宽高比约束内，自由选尺寸

        > 例如，支持768\*2700，而2.2及以下版本单边上限 1400
      </td>
    </tr>

    <tr>
      <td>
        wan2.2-t2i-flash
      </td>

      <td>
        万相2.2极速版

        较2.1模型速度提升50%
      </td>

      <td rowSpan={5}>
        图像分辨率：宽高均在\[512, 1440]像素之间

        图像格式：png
      </td>
    </tr>

    <tr>
      <td>
        wan2.2-t2i-plus
      </td>

      <td>
        万相2.2专业版

        较2.1模型稳定性与成功率全面提升
      </td>
    </tr>

    <tr>
      <td>
        wanx2.1-t2i-turbo
      </td>

      <td>
        万相2.1极速版
      </td>
    </tr>

    <tr>
      <td>
        wanx2.1-t2i-plus
      </td>

      <td>
        万相2.1专业版
      </td>
    </tr>

    <tr>
      <td>
        wanx2.0-t2i-turbo
      </td>

      <td>
        万相2.0极速版
      </td>
    </tr>
  </tbody>
</table>

<Note>
  - 调用前，请查阅各地域支持的模型列表。
  - <strong>wan2.6模型</strong>：支持[HTTP同步调用](/zh/model-studio/text-to-image-v2-api-reference#39337f0fd0wzt)、[HTTP异步调用](/zh/model-studio/text-to-image-v2-api-reference#9c68ccc525nta)、[Dashscope Python SDK调用](/zh/model-studio/text-to-image-v2-api-reference#b3d1dbc5dbtgx)和[Dashscope Java SDK调用](/zh/model-studio/text-to-image-v2-api-reference#e8a7f6d4c9xyz)。
  - <strong>wan2.5及以下版本模型</strong>：支持[HTTP异步调用](/zh/model-studio/text-to-image-v2-api-reference#0d8029dcc8pxl)、[Dashscope Python SDK调用](/zh/model-studio/text-to-image-v2-api-reference#7afdfdd68emtf)和[Dashscope Java SDK调用](/zh/model-studio/text-to-image-v2-api-reference#589b80853e6rn)，不支持HTTP同步调用。
</Note>

## 前提条件 <span id="0e41425c577u4" />

在调用前，先[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。

<Tip>
  华北2（北京）、新加坡和美国（弗吉尼亚）地域拥有独立的 <strong>API Key</strong>与<strong>请求地址</strong>，不可混用，跨地域调用将导致鉴权失败或服务报错，详情请参见[选择地域、服务部署范围和接入域名](/zh/model-studio/regions)。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## HTTP同步调用（wan2.6） <span id="39337f0fd0wzt" />

<Tip>
  本章节接口为<strong>新版协议</strong>，仅支持 <strong>wan2.6</strong>模型。
</Tip>

一次请求即可获得结果，流程简单，推荐大多数场景使用。

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`
  </Tab>
</Tabs>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="576ce84612n7u" />

        ##### 请求头（Headers） <span id="bb0a157056fbk" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### 请求体（Request Body） <span id="45d1fa2260zn6" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。示例值：wan2.6-t2i。

        <Note>
          wan2.5及以下版本模型，HTTP调用请参见[HTTP异步调用](/zh/model-studio/text-to-image-v2-api-reference#0d8029dcc8pxl)。
        </Note>

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息。

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。当前<strong>仅支持单轮对话</strong>，即传入一组role、content参数，不支持多轮对话。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string` <strong>（必选）</strong>

            消息的角色。此参数必须设置为`user`。

            <strong>content</strong>`array` <strong>（必选）</strong>

            消息内容数组。

            <Accordion title="属性" defaultOpen>
              <strong>text</strong>`string`<strong>（必选）</strong>

              正向提示词，用于描述期望生成的图像内容、风格和构图。

              支持中英文，长度不超过2100个字符，每个汉字、字母、数字或符号计为一个字符，超过部分会自动截断。

              示例值：一只坐着的橘黄色的猫，表情愉悦，活泼可爱，逼真准确。

              <strong>注意</strong>：仅支持传入一个text，不传或传入多个将报错。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        图像处理参数。

        <Accordion title="属性" defaultOpen>
          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用于描述不希望在图像中出现的内容，对画面进行限制。

          支持中英文，长度不超过500个字符，超出部分将自动截断。

          示例值：低分辨率，低画质，肢体畸形，手指畸形，画面过饱和，蜡像感，人脸无细节，过度光滑，画面具有AI感。构图混乱。文字模糊，扭曲。

          <strong>size</strong> `string` （可选）

          输出图像的分辨率，格式为`宽*高`。

          - 默认值为 `1280*1280`。
          - 总像素在 \[1280*1280, 1440*1440] 之间且宽高比范围为 \[1:4, 4:1]。例如，768\*2700符合要求。

          示例值：1280\*1280。

          <Accordion title="常见比例推荐的分辨率" defaultOpen>
            - 1:1：1280\*1280
            - 3:4：1104\*1472
            - 4:3：1472\*1104
            - 9:16：960\*1696
            - 16:9：1696\*960
          </Accordion>

          <strong>n</strong> `integer` （可选）

          <Tip>
            n直接影响费用。费用 = 单价 × 图片张数，请在调用前确认[模型价格](/zh/model-studio/model-pricing#0006b52b83ua9)。
          </Tip>

          生成图片的数量。取值范围为1\~4张，默认为`4`。

          注意：按张计费，测试建议设为 1。

          <strong>prompt\_extend</strong> `bool` （可选）

          是否开启提示词智能改写。开启后，将使用大模型优化正向提示词，对较短的提示词有明显提升效果，但增加3-4秒耗时。

          - true：默认值，开启智能改写。
          - false：关闭智能改写。

          <Note>
            开启智能改写后，改写生成的提示词可能引入受版权保护的内容，从而触发内容审核，返回 `IPInfringementSuspect` 或 `DataInspectionFailed` 报错。

            遇到上述报错时，可将 `prompt_extend` 设置为 `false` 后重试。若提示词本身直接包含受版权保护的角色名或作品名，关闭智能改写仍会报错，需修改提示词本身。
          </Note>

          <strong>watermark</strong> `bool` （可选）

          是否添加水印标识，水印位于图片右下角，文案固定为“AI生成”。

          - false：默认值，不添加水印。
          - true：添加水印。

          <strong>seed</strong> `integer` （可选）

          随机数种子，取值范围`[0,2147483647]`。

          使用相同的`seed`参数值可使生成内容保持相对稳定。若不提供，算法将自动使用随机数种子。

          <strong>注意</strong>：模型生成过程具有概率性，即使使用相同的`seed`，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="文生图">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --data '{
                "model": "wan2.6-t2i",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "text": "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵"
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "prompt_extend": true,
                    "watermark": false,
                    "n": 1,
                    "negative_prompt": "",
                    "size": "1280*1280"
                }
            }'
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
        #### 响应参数 <span id="49792d3618l67" />

        <strong>output</strong> `object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>choices</strong> `array`

          模型生成的输出内容。

          <Accordion title="属性" defaultOpen>
            <strong>finish\_reason</strong> `string`

            任务停止原因，自然停止时为`stop`。

            <strong>message</strong> `object`

            模型返回的消息。

            <Accordion title="属性" defaultOpen>
              <strong>role</strong>`string`

              消息的角色，固定为`assistant`。

              <strong>content</strong>`array`

              <Accordion title="属性" defaultOpen>
                <strong>image</strong> `string`

                生成图像的 URL，图像格式为PNG。<strong>链接有效期为24小时</strong>，请及时下载并保存图像。

                <strong>type</strong> `string`

                输出的类型，固定为image。
              </Accordion>
            </Accordion>
          </Accordion>

          <strong>finished</strong> `boolean`

          任务是否结束。

          - true：已结束。
          - false：未结束。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          生成图像的张数。

          <strong>size</strong> `string`

          生成的图像分辨率。示例值：1280\*1280。

          <strong>input\_tokens</strong> `integer`

          输入token。文生图按图片张数计费，当前固定为0。

          <strong>output\_tokens</strong> `integer`

          输出token。文生图按图片张数计费，当前固定为0。

          <strong>total\_tokens</strong>`integer`

          总token。文生图按图片张数计费，当前固定为0。
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
          <Tab title="任务执行成功">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            ```json expandable
            {
                "output": {
                    "choices": [
                        {
                            "finish_reason": "stop",
                            "message": {
                                "content": [
                                    {
                                        "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxx.png?Expires=xxx",
                                        "type": "image"
                                    }
                                ],
                                "role": "assistant"
                            }
                        }
                    ],
                    "finished": true
                },
                "usage": {
                    "image_count": 1,
                    "input_tokens": 0,
                    "output_tokens": 0,
                    "size": "1280*1280",
                    "total_tokens": 0
                },
                "request_id": "815505c6-7c3d-49d7-b197-xxxxx"
            }
            ```
          </Tab>

          <Tab title="任务执行异常">
            如果因为某种原因导致任务执行失败，将返回相关信息，可以通过code和message字段明确指示错误原因。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "a4d78a5f-655f-9639-8437-xxxxxx",
                "code": "InvalidParameter",
                "message": "num_images_per_prompt must be 1"
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## HTTP异步调用（wan2.6） <span id="9c68ccc525nta" />

<Tip>
  本章节接口为<strong>新版协议</strong>，仅支持 <strong>wan2.6</strong>模型。
</Tip>

任务流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="06ddfb0e63h4p" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`
  </Tab>

  <Tab title="德国（法兰克福）">
    `POST https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Note>
  - 创建成功后，使用接口返回的 `task_id` 查询结果，task\_id 有效期为 24 小时。<strong>请勿重复创建任务</strong>，轮询获取即可。
  - 新手指引请参见[Postman](/zh/model-studio/first-call-to-image-and-video-api)。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="13f75b0cab2wu" />

        ##### 请求头（Headers） <span id="f0bc65e20eu5a" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="962410e014g1v" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。示例值：wan2.6-t2i。

        <Note>
          wan2.5及以下版本模型，HTTP调用请参见[HTTP异步调用。](/zh/model-studio/text-to-image-v2-api-reference#0d8029dcc8pxl)
        </Note>

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息。

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。当前<strong>仅支持单轮对话</strong>，即传入一组role、content参数，不支持多轮对话。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string` <strong>（必选）</strong>

            消息的角色。此参数必须设置为`user`。

            <strong>content</strong>`array` <strong>（必选）</strong>

            消息内容数组。

            <Accordion title="属性" defaultOpen>
              <strong>text</strong>`string`<strong>（必选）</strong>

              正向提示词，用于描述期望生成的图像内容、风格和构图。

              支持中英文，长度不超过2100个字符，每个汉字、字母、数字或符号计为一个字符，超过部分会自动截断。

              示例值：一间有着精致窗户的花店，漂亮的木质门，摆放着花朵。

              <strong>注意</strong>：仅支持传入一个text，不传或传入多个将报错。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        图像处理参数。

        <Accordion title="属性" defaultOpen>
          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用于描述不希望在图像中出现的内容，对画面进行限制。

          支持中英文，长度不超过500个字符，超出部分将自动截断。

          示例值：低分辨率，低画质，肢体畸形，手指畸形，画面过饱和，蜡像感，人脸无细节，过度光滑，画面具有AI感。构图混乱。文字模糊，扭曲。

          <strong>size</strong> `string` （可选）

          输出图像的分辨率，格式为`宽*高`。

          - 默认值为 `1280*1280`。
          - 总像素在 \[1280*1280, 1440*1440] 之间且宽高比范围为 \[1:4, 4:1]。例如，768\*2700符合要求。

          示例值：1280\*1280。

          <Accordion title="常见比例推荐的分辨率" defaultOpen>
            - 1:1：1280\*1280
            - 3:4：1104\*1472
            - 4:3：1472\*1104
            - 9:16：960\*1696
            - 16:9：1696\*960
          </Accordion>

          <strong>n</strong> `integer` （可选）

          <Tip>
            n直接影响费用。费用 = 单价 × 图片张数，请在调用前确认[模型价格](/zh/model-studio/model-pricing#0006b52b83ua9)。
          </Tip>

          生成图片的数量。取值范围为1\~4张，默认为`4`。

          注意：按张计费，测试建议设为 1。

          <strong>prompt\_extend</strong> `bool` （可选）

          是否开启prompt智能改写。开启后，将使用大模型优化正向提示词，对较短的提示词有明显提升效果，但增加3-4秒耗时。

          - true：默认值，开启智能改写。
          - false：不开启智能改写。

          <Note>
            开启智能改写后，改写生成的提示词可能引入受版权保护的内容，从而触发内容审核，返回 `IPInfringementSuspect` 或 `DataInspectionFailed` 报错。

            遇到上述报错时，可将 `prompt_extend` 设置为 `false` 后重试。若提示词本身直接包含受版权保护的角色名或作品名，关闭智能改写仍会报错，需修改提示词本身。
          </Note>

          <strong>watermark</strong> `bool` （可选）

          是否添加水印标识，水印位于图片右下角，文案固定为“AI生成”。

          - false：默认值，不添加水印。
          - true：添加水印。

          <strong>seed</strong> `integer` （可选）

          随机数种子，取值范围`[0,2147483647]`。

          使用相同的`seed`参数值可使生成内容保持相对稳定。若不提供，算法将自动使用随机数种子。

          <strong>注意</strong>：模型生成过程具有概率性，即使使用相同的`seed`，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="文生图">
            ```bash expandable
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'X-DashScope-Async: enable' \
            --data '{
                "model": "wan2.6-t2i",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "text": "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵"
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "prompt_extend": true,
                    "watermark": false,
                    "n": 1,
                    "negative_prompt": "",
                    "size": "1280*1280"
                }
            }'
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
        #### 响应参数 <span id="37cef0683eg79" />

        <strong>output</strong> `object`

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

### 步骤2：根据任务ID查询结果 <span id="0d606bc055ssc" />

<Tabs>
  <Tab title="华北2（北京）">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `GET https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>

  <Tab title="德国（法兰克福）">
    `GET https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Note>
  - <strong>轮询建议</strong>：图像生成过程耗时较长，建议采用<strong>轮询</strong>机制，并设置合理的查询间隔（如 10 秒）来获取结果。
  - <strong>任务状态流转</strong>：PENDING（排队中）→ RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
  - <strong>结果链接</strong>：任务成功后返回图像链接，有效期为 <strong>24 小时</strong>。建议在获取链接后立即下载并转存至永久存储（如[阿里云 OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss)）。
  - <strong>RPS 限制</strong>：查询接口默认RPS为20。如需更高频查询或事件通知，建议[配置异步任务回调](/zh/model-studio/async-task-api)。
  - <strong>更多操作</strong>：如需批量查询、取消任务等操作，请参见[管理异步任务](/zh/model-studio/manage-asynchronous-tasks)。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="8442f74589dej" />

        ##### 请求头（Headers） <span id="bb10a5aa136zm" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="09e80fbec7hk0" />

        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            将`{task_id}`完整替换为上一步接口返回的`task_id`的值。`task_id`查询有效期为24小时，并请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

            ```bash
            curl -X GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id} \
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
        #### 响应参数 <span id="e1be57396anjq" />

        <strong>output</strong> `object`

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

          <strong>轮询过程中的状态流转：</strong>

          - PENDING（排队中） → RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
          - 初次查询状态通常为 PENDING（排队中）或 RUNNING（处理中）。
          - 当状态变为 SUCCEEDED 时，响应中将包含生成的图像URL。
          - 若状态为 FAILED，请检查错误信息并重试。

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>finished</strong> `boolean`

          任务是否结束。

          - true：已结束。
          - false：未结束。

          <strong>choices</strong> `array`

          模型生成的输出内容。

          <Accordion title="属性" defaultOpen>
            <strong>finish\_reason</strong> `string`

            任务停止原因，正常完成时为 `stop`。

            <strong>message</strong> `object`

            模型返回的消息。

            <Accordion title="属性" defaultOpen>
              <strong>role</strong>`string`

              消息的角色，固定为`assistant`。

              <strong>content</strong>`array`

              <Accordion title="属性" defaultOpen>
                <strong>image</strong> `string`

                生成图像的 URL，图像格式为PNG。

                <strong>链接有效期为24小时</strong>，请及时下载并保存图像。

                <strong>type</strong> `string`

                输出的类型，固定为image。
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。<strong>只对成功的结果计数。</strong>

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          生成图像的张数。

          <strong>size</strong> `string`

          生成的图像分辨率。示例值：1280\*1280。

          <strong>input\_tokens</strong> `integer`

          输入token数量。当前固定为0。

          <strong>output\_tokens</strong> `integer`

          输出token数量。当前固定为0。

          <strong>total\_tokens</strong>`integer`

          总token数量。当前固定为0。
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
          <Tab title="任务执行成功">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            ```json expandable
            {
                "request_id": "2ddf53fa-699a-4267-9446-xxxxxx",
                "output": {
                    "task_id": "3cd3fa4e-53ee-4136-9cab-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-12-18 20:03:01.802",
                    "scheduled_time": "2025-12-18 20:03:01.834",
                    "end_time": "2025-12-18 20:03:29.260",
                    "finished": true,
                    "choices": [
                        {
                            "finish_reason": "stop",
                            "message": {
                                "role": "assistant",
                                "content": [
                                    {
                                        "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx.png?Expires=xxx",
                                        "type": "image"
                                    }
                                ]
                            }
                        }
                    ]
                },
                "usage": {
                    "size": "1280*1280",
                    "total_tokens": 0,
                    "image_count": 1,
                    "output_tokens": 0,
                    "input_tokens": 0
                }
            }
            ```
          </Tab>

          <Tab title="任务执行异常">
            如果因为某种原因导致任务执行失败，将返回相关信息，可以通过code和message字段明确指示错误原因。请参见[错误码](/zh/model-studio/error-code)进行解决。

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

## HTTP异步调用（wan2.5及以下版本模型） <span id="0d8029dcc8pxl" />

<Tip>
  此接口为<strong>旧版协议</strong>，仅支持<strong>wan2.5及以下版本模型</strong>。
</Tip>

由于文生图任务耗时较长（通常为1-2分钟），API采用异步调用。整个流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### 步骤1：创建任务获取任务ID <span id="db8cf70432oe3" />

<Tabs>
  <Tab title="北京">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis`
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis`
  </Tab>
</Tabs>

<Note>
  - 创建成功后，使用接口返回的 `task_id` 查询结果，task\_id 有效期为 24 小时。<strong>请勿重复创建任务</strong>，轮询获取即可。
  - 新手指引请参见[Postman](/zh/model-studio/first-call-to-image-and-video-api)。
</Note>

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

        ##### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong> `string` （必选）

        模型名称。文生图模型请参见[模型列表](/zh/model-studio/wanx-image-edit-api-reference)。

        示例值：wan2.5-t2i-preview。

        <Note>
          wan2.6模型的HTTP调用请参见[HTTP同步调用](/zh/model-studio/text-to-image-v2-api-reference#39337f0fd0wzt)、[HTTP异步调用](/zh/model-studio/text-to-image-v2-api-reference#9c68ccc525nta)。
        </Note>

        <strong>input</strong> `object` （必选）

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          正向提示词，用来描述生成图像中期望包含的元素和视觉特点。

          支持中英文，每个汉字/字母/标点符号占一个字符，超过部分会自动截断。长度限制因模型版本而异：

          - wan2.5-t2i-preview：长度不超过2000个字符。
          - wan2.2、wan2.1系列模型：长度不超过500个字符。
          - wanx2.0-t2i-turbo：长度不超过800个字符。

          示例值：一只坐着的橘黄色的猫，表情愉悦，活泼可爱，逼真准确。

          提示词的使用技巧请参见[文生图Prompt指南](/zh/model-studio/text-to-image-prompt)。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在画面中看到的内容，可以对画面进行限制。

          支持中英文，长度不超过500个字符，超过部分会自动截断。

          示例值：低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良等。
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        图像处理参数。如设置图像分辨率、开启prompt智能改写、添加水印等。

        <Accordion title="属性" defaultOpen>
          <strong>size</strong> `string` （可选）

          输出图像的分辨率，格式为`宽*高`。默认值和约束因模型版本而异：

          - wan2.5-t2i-preview：默认值为 `1280*1280`。总像素在 \[1280*1280, 1440*1440] 之间且宽高比范围为 \[1:4, 4:1]。例如，768\*2700符合要求。
          - wan2.2及以下版本模型：默认值为`1024*1024`。图像宽高在\[512, 1440]之间，最大分辨率为1440*1440。例如， 768*2700超单边限制，不支持。

          示例值：1280\*1280。

          <Accordion title="常见比例推荐的分辨率" defaultOpen>
            以下分辨率适用于wan2.5-t2i-preview

            - 1:1：1280\*1280
            - 3:4：1104\*1472
            - 4:3：1472\*1104
            - 9:16：960\*1696
            - 16:9：1696\*960
          </Accordion>

          <strong>n</strong> `integer` （可选）

          <Tip>
            n直接影响费用。费用 = 单价 × 图片张数，请在调用前确认[模型价格](/zh/model-studio/model-pricing#0006b52b83ua9)。
          </Tip>

          生成图片的数量。取值范围为1\~4张，默认为`4`。测试阶段建议设置为1，便于低成本验证。

          <strong>prompt\_extend</strong>`boolean` （可选）

          是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写。对于较短的prompt生成效果提升明显，但会增加耗时。

          - true：默认值，开启智能改写。
          - false：关闭智能改写。

          示例值：true。

          <Note>
            开启智能改写后，改写生成的提示词可能引入受版权保护的内容，从而触发内容审核，返回 `IPInfringementSuspect` 或 `DataInspectionFailed` 报错。

            遇到上述报错时，可将 `prompt_extend` 设置为 `false` 后重试。若提示词本身直接包含受版权保护的角色名或作品名，关闭智能改写仍会报错，需修改提示词本身。
          </Note>

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于图片右下角，文案固定为“AI生成”。

          - false：默认值，不添加水印。
          - true：添加水印。

          <strong>seed</strong> `integer` （可选）

          随机数种子，取值范围`[0,2147483647]`。

          使用相同的`seed`参数值可使生成内容保持相对稳定。若不提供，算法将自动使用随机数种子。

          <strong>注意</strong>：模型生成过程具有概率性，即使使用相同的`seed`，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="文生图">
            ```bash
            curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.5-t2i-preview",
                "input": {
                    "prompt": "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵"
                },
                "parameters": {
                    "size": "1280*1280",
                    "n": 1
                }
            }'
            ```
          </Tab>

          <Tab title="文生图（使用反向提示词）">
            通过 negative\_prompt 指定生成的图片避免出现“人物”元素。

            ```bash
            curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "wan2.2-t2i-flash",
                "input": {
                    "prompt": "雪地，白色小教堂，极光，冬日场景，柔和的光线。",
                    "negative_prompt": "人物"
                },
                "parameters": {
                    "size": "1024*1024",
                    "n": 1
                }
            }'
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
        #### 响应参数 <span id="cd3063363egdc" />

        <strong>output</strong> `object`

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

<Tabs>
  <Tab title="华北2（北京）">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Note>
  - <strong>轮询建议</strong>：图像生成过程耗时较长，建议采用<strong>轮询</strong>机制，并设置合理的查询间隔（如 10 秒）来获取结果。
  - <strong>任务状态流转</strong>：PENDING（排队中）→ RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
  - <strong>结果链接</strong>：任务成功后返回图像链接，有效期为 <strong>24 小时</strong>。建议在获取链接后立即下载并转存至永久存储（如[阿里云 OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss)）。
  - <strong>RPS 限制</strong>：查询接口默认RPS为20。如需更高频查询或事件通知，建议[配置异步任务回调](/zh/model-studio/async-task-api)。
  - <strong>更多操作</strong>：如需批量查询、取消任务等操作，请参见[管理异步任务](/zh/model-studio/manage-asynchronous-tasks)。
</Note>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="eb9c8d77b6xg8" />

        ##### 请求头（Headers） <span id="36a28b657f196" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="0e29e015d8opn" />

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
        #### 响应参数 <span id="f277d7425foyq" />

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

          <strong>轮询过程中的状态流转：</strong>

          - PENDING（排队中） → RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
          - 初次查询状态通常为 PENDING（排队中）或 RUNNING（处理中）。
          - 当状态变为 SUCCEEDED 时，响应中将包含生成的图像URL。
          - 若状态为 FAILED，请检查错误信息并重试。

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>results</strong> `array of object`

          任务结果列表，包括图像URL、prompt、部分任务执行失败报错信息等。

          <Accordion title="数据结构">
            ```json
            {
                "results": [
                    {
                        "orig_prompt": "",
                        "actual_prompt": "",
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

          <Accordion title="属性" defaultOpen>
            <strong>orig\_prompt</strong> `string`

            原始输入的prompt，对应请求参数`prompt`。

            <strong>actual\_prompt</strong> `string`

            开启 prompt 智能改写后，返回实际使用的优化后 prompt。若未开启该功能，则不返回此字段。

            <strong>url</strong> `string`

            图像URL地址。仅在 task\_status 为 SUCCEEDED 时返回。链接有效期24小时，可通过此URL下载图像。

            <strong>code</strong>`string`

            请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

            <strong>message</strong>`string`

            请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
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
            图像URL仅保留24小时，超时后会被自动清除，请及时保存生成的图像。

            ```json
            {
                "request_id": "f767d108-7d50-908b-a6d9-xxxxxx",
                "output": {
                    "task_id": "d492bffd-10b5-4169-b639-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-01-08 16:03:59.840",
                    "scheduled_time": "2025-01-08 16:03:59.863",
                    "end_time": "2025-01-08 16:04:10.660",
                    "results": [
                        {
                            "orig_prompt": "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵",
                            "actual_prompt": "一间有着精致雕花窗户的花店，漂亮的深色木质门上挂着铜制把手。店内摆放着各式各样的鲜花，包括玫瑰、百合和向日葵，色彩鲜艳，生机勃勃。背景是温馨的室内场景，透过窗户可以看到街道。高清写实摄影，中景构图。",
                            "url": "https://dashscope-result-wlcb.oss-cn-wulanchabu.aliyuncs.com/1.png"
                        }
                    ],
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

          <Tab title="任务执行失败">
            若任务执行失败，task\_status将置为 FAILED，并提供错误码和信息。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "e5d70b02-ebd3-98ce-9fe8-759d7d7b107d",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-xxxxxx",
                    "task_status": "FAILED",
                    "code": "InvalidParameter",
                    "message": "xxxxxx",
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
                "request_id": "85eaba38-0185-99d7-8d16-xxxxxx",
                "output": {
                    "task_id": "86ecf553-d340-4e21-af6e-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "results": [
                        {
                            "url": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/123/a1.png"
                        },
                        {
                            "code": "InternalError.Timeout",
                            "message": "An internal timeout error has occurred during execution, please try again later or contact service support."
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

          <Tab title="任务查询过期">
            task\_id查询有效期为 24 小时，超时后将无法查询，返回以下报错信息。

            ```json
            {
                "request_id": "a4de7c32-7057-9f82-8581-xxxxxx",
                "output": {
                    "task_id": "502a00b1-19d9-4839-a82f-xxxxxx",
                    "task_status": "UNKNOWN"
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## DashScope Python SDK调用 <span id="1751252d8bf7c" />

SDK 的参数命名与HTTP接口基本一致，参数结构根据语言特性进行封装。

由于文生图任务耗时较长，SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### wan2.6 <span id="b3d1dbc5dbtgx" />

<Tip>
  - 以下代码仅适合 <strong>wan2.6</strong> 模型。
  - 请确保 DashScope Python SDK 版本<strong>不低于</strong>`1.25.7`，再运行以下代码。更新请参考[安装SDK](/zh/model-studio/install-sdk)。
</Tip>

各地域的`base_url`和 API Key 不通用，以下示例以北京地域为例进行调用：

<Tabs>
  <Tab title="华北2（北京）">
    `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1`
  </Tab>

  <Tab title="德国（法兰克福）">
    `https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

> 全球部署范围（法兰克福地域）仅支持异步调用。

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="84e6e611eeumd" />

    ```python expandable
    import os
    import dashscope
    from dashscope.aigc.image_generation import ImageGeneration
    from dashscope.api_entities.dashscope_response import Message

    # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    message = Message(
        role="user",
        content=[
            {
                'text': '一间有着精致窗户的花店，漂亮的木质门，摆放着花朵'
            }
        ]
    )
    print("----sync call, please wait a moment----")
    rsp = ImageGeneration.call(
        model="wan2.6-t2i",
        api_key=api_key,
        messages=[message],
        negative_prompt="",
        prompt_extend=True,
        watermark=False,
        n=1,
        size="1280*1280"
    )
    print(rsp)
    ```

    ##### 响应示例 <span id="b0515f126ag6l" />

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "820dd0db-eb42-4e05-8d6a-1ddb4axxxxxx",
        "code": "",
        "message": "",
        "output": {
            "text": null,
            "finish_reason": null,
            "choices": [
                {
                    "finish_reason": "stop",
                    "message": {
                        "role": "assistant",
                        "content": [
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            }
                        ]
                    }
                }
            ],
            "audio": null,
            "finished": true
        },
        "usage": {
            "input_tokens": 0,
            "output_tokens": 0,
            "characters": 0,
            "image_count": 1,
            "size": "1280*1280",
            "total_tokens": 0
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="4ad5f44591826" />

    ```python expandable
    import os
    import dashscope
    from dashscope.aigc.image_generation import ImageGeneration
    from dashscope.api_entities.dashscope_response import Role, Message
    from http import HTTPStatus

    # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    # 创建异步任务
    def create_async_task():
        print("Creating async task...")
        message = Message(
            role="user",
            content=[{'text': '一间有着精致窗户的花店，漂亮的木质门，摆放着花朵'}]
        )
        response = ImageGeneration.async_call(
            model="wan2.6-t2i",
            api_key=api_key,
            messages=[message],
            negative_prompt="",
            prompt_extend=True,
            watermark=False,
            n=1,
            size="1280*1280"
        )

        if response.status_code == 200:
            print("Task created successfully:", response)
            return response
        else:
            raise Exception(f"Failed to create task: {response.code} - {response.message}")

    # 等待任务完成
    def wait_for_completion(task_response):
        print("Waiting for task completion...")
        status = ImageGeneration.wait(task=task_response, api_key=api_key)

        if status.output.task_status == "SUCCEEDED":
            print("Task succeeded!")
            print("Response:", status)
        else:
            raise Exception(f"Task failed with status: {status.output.task_status}")

    # 获取异步任务信息
    def fetch_task_status(task):
        print("Fetching task status...")
        status = ImageGeneration.fetch(task=task, api_key=api_key)

        if status.status_code == HTTPStatus.OK:
            print("Task status:", status.output.task_status)
            print("Response details:", status)
        else:
            print(f"Failed to fetch status: {status.code} - {status.message}")

    # 取消异步任务
    def cancel_task(task):
        print("Canceling task...")
        response = ImageGeneration.cancel(task=task, api_key=api_key)

        if response.status_code == HTTPStatus.OK:
            print("Task canceled successfully:", response.output.task_status)
        else:
            print(f"Failed to cancel task: {response.code} - {response.message}")

    # 主执行流程
    if __name__ == "__main__":
        task = create_async_task()
        wait_for_completion(task)
    ```

    ##### 响应示例 <span id="d5de68e40618u" />

    1、创建任务的响应示例

    ```json
    {
        "status_code": 200,
        "request_id": "c4f11410-ea42-4996-957d-9c82f9xxxxxx",
        "code": "",
        "message": "",
        "output": {
            "text": null,
            "finish_reason": null,
            "choices": null,
            "audio": null,
            "task_id": "f470bbfd-d955-4165-935b-d35b8eexxxxxx",
            "task_status": "PENDING"
        },
        "usage": {
            "input_tokens": 0,
            "output_tokens": 0,
            "characters": 0
        }
    }
    ```

    2、查询任务结果的响应示例

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "7e57e7e8-00b0-4534-9aff-fe31e0xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "text": null,
            "finish_reason": null,
            "choices": [
                {
                    "finish_reason": "stop",
                    "message": {
                        "role": "assistant",
                        "content": [
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            }
                        ]
                    }
                }
            ],
            "audio": null,
            "task_id": "f470bbfd-d955-4165-935b-d35b8exxxxxx",
            "task_status": "SUCCEEDED",
            "submit_time": "2026-01-09 17:18:17.901",
            "scheduled_time": "2026-01-09 17:18:17.941",
            "end_time": "2026-01-09 17:18:45.544",
            "finished": true
        },
        "usage": {
            "input_tokens": 0,
            "output_tokens": 0,
            "characters": 0,
            "size": "1280*1280",
            "total_tokens": 0,
            "image_count": 1
        }
    }
    ```
  </Tab>
</Tabs>

### wan2.5及以下版本模型 <span id="7afdfdd68emtf" />

<Tip>
  - 以下代码仅适合wan2.5及以下版本模型。
  - 请确保 DashScope Python SDK 版本<strong>不低于</strong>`1.25.2`，再运行以下代码。

    若版本过低，可能会触发 “url error, please check url!” 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

各地域的`base_url`和 API Key 不通用，以下示例以北京地域为例进行调用：

<Tabs>
  <Tab title="华北2（北京）">
    `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="de8be082068hw" />

    ```python expandable
    from http import HTTPStatus
    from urllib.parse import urlparse, unquote
    from pathlib import PurePosixPath
    import requests
    from dashscope import ImageSynthesis
    import os
    import dashscope

    # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    print('----sync call, please wait a moment----')
    rsp = ImageSynthesis.call(api_key=api_key,
                              model="wan2.5-t2i-preview",
                              prompt="一间有着精致窗户的花店，漂亮的木质门，摆放着花朵",
                              negative_prompt="",
                              n=1,
                              size='1280*1280',
                              prompt_extend=True,
                              watermark=False,
                              seed=12345)
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

    ##### 响应示例 <span id="177b27c9b28it" />

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "9d634fda-5fe9-9968-a908-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "d35658e4-483f-453b-b8dc-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [{
                "url": "https://dashscope-result-wlcb.oss-cn-wulanchabu.aliyuncs.com/1.png",
                "orig_prompt": "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵",
                "actual_prompt": "一间精致的花店，窗户上装饰着优雅的雕花，漂亮的木质门上挂着铜制把手。店内摆放着各种色彩鲜艳的花朵，如玫瑰、郁金香和百合等。背景是温馨的室内场景，光线柔和，营造出宁静舒适的氛围。高清写实摄影，近景中心构图。"
            }],
            "submit_time": "2025-01-08 19:36:01.521",
            "scheduled_time": "2025-01-08 19:36:01.542",
            "end_time": "2025-01-08 19:36:13.270",
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
    ##### 请求示例 <span id="b9b6d3bf42rc7" />

    ```python expandable
    from http import HTTPStatus
    from urllib.parse import urlparse, unquote
    from pathlib import PurePosixPath
    import requests
    from dashscope import ImageSynthesis
    import os
    import dashscope

    # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def async_call():
        print('----create task----')
        task_info = create_async_task()
        print('----wait task done then save image----')
        wait_async_task(task_info)

    # 创建异步任务
    def create_async_task():
        rsp = ImageSynthesis.async_call(api_key=api_key,
                                        model="wan2.5-t2i-preview",
                                        prompt="一间有着精致窗户的花店，漂亮的木质门，摆放着花朵",
                                        negative_prompt="",
                                        n=1,
                                        size='1280*1280',
                                        prompt_extend=True,
                                        watermark=False,
                                        seed=12345)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))
        return rsp

    # 等待异步任务结束
    def wait_async_task(task):
        rsp = ImageSynthesis.wait(task=task, api_key=api_key)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output)
            # save file to current directory
            for result in rsp.output.results:
                file_name = PurePosixPath(unquote(urlparse(result.url).path)).parts[-1]
                with open('./%s' % file_name, 'wb+') as f:
                    f.write(requests.get(result.url).content)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    # 获取异步任务信息
    def fetch_task_status(task):
        status = ImageSynthesis.fetch(task=task, api_key=api_key)
        print(status)
        if status.status_code == HTTPStatus.OK:
            print(status.output.task_status)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (status.status_code, status.code, status.message))

    # 取消异步任务，只有处于PENDING状态的任务才可以取消
    def cancel_task(task):
        rsp = ImageSynthesis.cancel(task=task, api_key=api_key)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.task_status)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        async_call()
    ```

    ##### 响应示例 <span id="4f9da575fdd1b" />

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

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "9d634fda-5fe9-9968-a908-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "d35658e4-483f-453b-b8dc-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [{
                "url": "https://dashscope-result-wlcb.oss-cn-wulanchabu.aliyuncs.com/xxx.png",
                "orig_prompt": "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵",
                "actual_prompt": "一间精致的花店，窗户上装饰着优雅的雕花，漂亮的木质门上挂着铜制把手。店内摆放着各种色彩鲜艳的花朵，如玫瑰、郁金香和百合等。背景是温馨的室内场景，光线柔和，营造出宁静舒适的氛围。高清写实摄影，近景中心构图。"
            }],
            "submit_time": "2025-01-08 19:36:01.521",
            "scheduled_time": "2025-01-08 19:36:01.542",
            "end_time": "2025-01-08 19:36:13.270",
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

## DashScope Java SDK调用 <span id="db17b3098c2iv" />

SDK 的参数命名与HTTP接口基本一致，参数结构根据语言特性进行封装。

由于文生图任务耗时较长，SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### wan2.6 <span id="e8a7f6d4c9xyz" />

<Tip>
  - 以下代码仅适合 <strong>wan2.6-t2i</strong> 模型。
  - 请确保 DashScope Java SDK 版本<strong>不低于</strong>`2.22.6`，再运行以下代码。
</Tip>

各地域的`base_url`和 API Key 不通用，以下示例以北京地域为例进行调用：

<Tabs>
  <Tab title="华北2（北京）">
    `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    `https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/api/v1`
  </Tab>

  <Tab title="德国（法兰克福）">
    `https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

> 全球部署范围（法兰克福地域）仅支持异步调用。

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="a8c9f7e6d5bcd" />

    ```java expandable
    import com.alibaba.dashscope.aigc.imagegeneration.*;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;
    import java.util.Collections;

    public class Main {

        static {
            // 以下为北京地域url，各地域的base_url不同
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void basicCall() throws ApiException, NoApiKeyException, UploadFileException {
            ImageGenerationMessage message = ImageGenerationMessage.builder()
                    .role("user")
                    .content(Collections.singletonList(
                            Collections.singletonMap("text", "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵")
                    )).build();

            ImageGenerationParam param = ImageGenerationParam.builder()
                    .apiKey(apiKey)
                    .model("wan2.6-t2i")
                    .n(1)
                    .size("1280*1280")
                    .negativePrompt("")
                    .promptExtend(true)
                    .watermark(false)
                    .messages(Collections.singletonList(message))
                    .build();

            ImageGeneration imageGeneration = new ImageGeneration();
            ImageGenerationResult result = null;
            try {
                System.out.println("---sync call, please wait a moment----");
                result = imageGeneration.call(param);
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                throw new RuntimeException(e.getMessage());
            }
            System.out.println(JsonUtils.toJson(result));
        }

        public static void main(String[] args) {
            try {
                basicCall();
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                System.out.println(e.getMessage());
            }
        }
    }
    ```

    ##### 响应示例 <span id="e8a9f7d6c5fgh" />

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "50b57166-eaaa-4f17-b1e0-35a5ca88672c",
        "code": "",
        "message": "",
        "output": {
            "choices": [
                {
                    "finish_reason": "stop",
                    "message": {
                        "role": "assistant",
                        "content": [
                            {
                                "image": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx",
                                "type": "image"
                            }
                        ]
                    }
                }
            ],
            "finished": true
        },
        "usage": {
            "input_tokens": 0,
            "output_tokens": 0,
            "image_count": 1,
            "size": "1280*1280",
            "total_tokens": 0
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="e8a9f7d6c5lmn" />

    ```java expandable
    import com.alibaba.dashscope.aigc.imagegeneration.*;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;
    import java.util.Collections;

    public class Main {

        static {
            // 以下为北京地域url，各地域的base_url不同
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void asyncCall() throws ApiException, NoApiKeyException, UploadFileException {
            ImageGenerationMessage message = ImageGenerationMessage.builder()
                    .role("user")
                    .content(Collections.singletonList(
                            Collections.singletonMap("text", "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵")
                    )).build();

            ImageGenerationParam param = ImageGenerationParam.builder()
                    .apiKey(apiKey)
                    .model("wan2.6-t2i")
                    .n(1)
                    .size("1280*1280")
                    .negativePrompt("")
                    .promptExtend(true)
                    .watermark(false)
                    .messages(Collections.singletonList(message))
                    .build();

            ImageGeneration imageGeneration = new ImageGeneration();
            ImageGenerationResult result = null;
            try {
                System.out.println("---async call, creating task----");
                result = imageGeneration.asyncCall(param);
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                throw new RuntimeException(e.getMessage());
            }
            System.out.println(JsonUtils.toJson(result));

            String taskId = result.getOutput().getTaskId();
            // 等待任务完成
            waitTask(taskId);
        }

        public static void waitTask(String taskId) throws ApiException, NoApiKeyException {
            ImageGeneration imageGeneration = new ImageGeneration();
            ImageGenerationResult result = imageGeneration.wait(taskId, apiKey);
            System.out.println(JsonUtils.toJson(result));
        }

        public static void main(String[] args) {
            try {
                asyncCall();
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                System.out.println(e.getMessage());
            }
        }
    }
    ```

    ##### 响应示例 <span id="d9f8a7e6b5qrs" />

    1、创建任务的响应示例

    ```json
    {
        "status_code": 200,
        "request_id": "9cd85950-2e26-4b2c-b562-1694cf9288e5",
        "code": "",
        "message": "",
        "output": {
            "task_id": "4c861fbe-af89-4a2f-8fc5-4bb15c3139ba",
            "task_status": "PENDING"
        },
        "usage": null
    }
    ```

    2、查询任务结果的响应示例

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "cbdf1424-306e-4a52-82f3-8bf5d8a99103",
        "code": "",
        "message": "",
        "output": {
            "choices": [
                {
                    "finish_reason": "stop",
                    "message": {
                        "role": "assistant",
                        "content": [
                            {
                                "image": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx",
                                "type": "image"
                            }
                        ]
                    }
                }
            ],
            "task_id": "4c861fbe-af89-4a2f-8fc5-4bb15c3139ba",
            "task_status": "SUCCEEDED",
            "submit_time": "2026-01-16 16:36:06.556",
            "scheduled_time": "2026-01-16 16:36:06.591",
            "end_time": "2026-01-16 16:36:25.190",
            "finished": true
        },
        "usage": {
            "input_tokens": 0,
            "output_tokens": 0,
            "size": "1280*1280",
            "total_tokens": 0,
            "image_count": 1
        }
    }
    ```
  </Tab>
</Tabs>

### wan2.5及以下版本模型 <span id="589b80853e6rn" />

<Tip>
  - 以下代码仅适合wan2.5及以下版本模型。
  - 请确保 DashScope Java SDK 版本<strong>不低于</strong>`2.22.2`，再运行以下代码。

    若版本过低，可能会触发 “url error, please check url!” 等错误。请参考[安装SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

各地域的`base_url`和 API Key 不通用，以下示例以北京地域为例进行调用：

<Tabs>
  <Tab title="华北2（北京）">
    `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="5ac3ea9f52tii" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisListResult;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
    import com.alibaba.dashscope.task.AsyncTaskListParam;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.util.HashMap;
    import java.util.Map;

    public class Main {

        static {
            // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void basicCall() throws ApiException, NoApiKeyException {
            // 设置parameters参数
            Map<String, Object> parameters = new HashMap<>();
            parameters.put("prompt_extend", true);
            parameters.put("watermark", false);
            parameters.put("seed", 12345);

            ImageSynthesisParam param =
                    ImageSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.5-t2i-preview")
                            .prompt("一间有着精致窗户的花店，漂亮的木质门，摆放着花朵")
                            .n(1)
                            .size("1280*1280")
                            .negativePrompt("")
                            .parameters(parameters)
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
            param.setApiKey(apiKey);
            ImageSynthesisListResult result = is.list(param);
            System.out.println(result);
        }

        public static void fetchTask(String taskId) throws ApiException, NoApiKeyException {
            ImageSynthesis is = new ImageSynthesis();
            // If set DASHSCOPE_API_KEY environment variable, apiKey can null.
            ImageSynthesisResult result = is.fetch(taskId, apiKey);
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

    ##### 响应示例 <span id="ecf553d14e183" />

    > url 有效期24小时，请及时下载图像。

    ```json
    {
        "request_id": "22f9c744-206c-9a78-899a-xxxxxx",
        "output": {
            "task_id": "4a0f8fc6-03fb-4c44-a13a-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [{
               "orig_prompt": "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵",
                "actual_prompt": "一间有着精致雕花窗户的花店，漂亮的深色木质门微微敞开。店内摆放着各式各样的鲜花，包括玫瑰、百合和向日葵，色彩鲜艳，香气扑鼻。背景是温馨的室内场景，光线柔和，透过窗户洒在花朵上。高清写实摄影，中景构图。",
                "url": "https://dashscope-result-wlcb.oss-cn-wulanchabu.aliyuncs.com/1.png"
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
    ##### 请求示例 <span id="f7b9a75fbbo26" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.util.HashMap;
    import java.util.Map;

    public class Main {

        static {
            // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        String apiKey = System.getenv("DASHSCOPE_API_KEY");

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
            // 设置parameters参数
            Map<String, Object> parameters = new HashMap<>();
            parameters.put("prompt_extend", true);
            parameters.put("watermark", false);
            parameters.put("seed", 12345);

            ImageSynthesisParam param =
                    ImageSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.5-t2i-preview")
                            .prompt("一间有着精致窗户的花店，漂亮的木质门，摆放着花朵")
                            .n(1)
                            .size("1280*1280")
                            .negativePrompt("")
                            .parameters(parameters)
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
                result = imageSynthesis.wait(taskId, apiKey);
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

    ##### 响应示例 <span id="228c690525jkh" />

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
        "request_id": "22f9c744-206c-9a78-899a-xxxxxx",
        "output": {
            "task_id": "4a0f8fc6-03fb-4c44-a13a-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [{
               "orig_prompt": "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵",
                "actual_prompt": "一间有着精致雕花窗户的花店，漂亮的深色木质门微微敞开。店内摆放着各式各样的鲜花，包括玫瑰、百合和向日葵，色彩鲜艳，香气扑鼻。背景是温馨的室内场景，光线柔和，透过窗户洒在花朵上。高清写实摄影，中景构图。",
                "url": "https://dashscope-result-wlcb.oss-cn-wulanchabu.aliyuncs.com/1.png"
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

## 使用限制 <span id="63a009e43a1iy" />

- <strong>数据时效</strong>：任务`task_id`和 图像`url`均只保留 24 小时，过期后将无法查询或下载。
- <strong>内容审核</strong>：输入的 `prompt` 和输出的图像均会经过内容安全审核，包含违规内容的请求将报错“IPInfringementSuspect”或“DataInspectionFailed”，具体参见[错误码](/zh/model-studio/error-code)。

## 计费与限流 <span id="5dafefe82bgrh" />

- 模型免费额度和计费单价请参见百炼控制台。
- 模型限流请参见[万相](/zh/model-studio/rate-limit#513e0a3df24v7)。
- 计费说明：按成功生成的 <strong>图像张数</strong> 计费。模型调用失败或处理错误不产生任何费用，也不消耗[新人免费额度](/zh/model-studio/new-free-quota)。

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

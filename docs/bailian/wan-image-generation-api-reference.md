> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-图像生成与编辑2.6 API参考

> 万相图像生成模型支持 图像编辑 、 图文混排输出 ，满足多样化生成与集成需求。

<meta property="og:title" content="万相-图像生成与编辑2.6 API参考" />

## 模型概览 <span id="4a3e46b825d2k" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "18.07%" }} /><col style={{ width: "37.46%" }} /><col style={{ width: "44.47%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型简介</strong></p></th><th><p><strong>输出图像规格</strong></p></th></tr></thead><tbody><tr><td><p>wan2.6-image</p></td><td><p>万相2.6 image</p> <p>支持图像编辑和图文混排输出</p> <p>如需纯文本生成图片（文生图），建议使用<a href="/zh/model-studio/text-to-image-v2-api-reference">wan2.6-t2i 模型（通义万相-文生图V2）</a>。</p></td><td><p>图片格式：PNG。</p> <p>图像分辨率和尺寸请参见<a href="/zh/model-studio/wan-image-generation-api-reference">size参数</a>。</p></td></tr></tbody></table>

<Note>
  调用前，请查阅各地域支持的[模型列表与价格](/zh/model-studio/model-pricing#e2540d71a2utl)。
</Note>

## 前提条件 <span id="0e41425c577u4" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

<Tip>
  华北2（北京）、新加坡和美国（弗吉尼亚）地域拥有独立的 <strong>API Key</strong>与<strong>请求地址</strong>，不可混用，跨地域调用将导致鉴权失败或服务报错，详情请参见[选择地域、服务部署范围和接入域名](/zh/model-studio/regions)。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## HTTP同步调用 <span id="dac5ce452cwz1" />

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

> 全球部署范围（法兰克福地域）仅支持[异步调用](/zh/model-studio/wan-image-generation-api-reference#42703589880ts)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="78f3155ff3r2l" />

        ##### 请求头（Headers） <span id="fd84180b66rpo" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Sse</strong>`string`（可选）

        用于启用流式输出。

        - 仅当 `parameters.enable_interleave=true` 时，<strong>必须</strong>将该字段设为 `enable`。
        - 其他情况下可不传或忽略。

        ##### 请求体（Request Body） <span id="7d7bb26156npj" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。设置为wan2.6-image。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息。

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。当前<strong>仅支持单轮对话</strong>，即传入一组role、content参数，不支持多轮对话。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string` <strong>（必选）</strong>

            消息的角色。此参数固定设置为`user`。

            <strong>content</strong>`array` <strong>（必选）</strong>

            消息内容数组。

            <Accordion title="属性" defaultOpen>
              <strong>text</strong>`string`<strong>（必选）</strong>

              正向提示词用于描述您期望生成的图像内容、风格和构图。

              支持中英文，长度不超过2000个字符，每个汉字、字母、数字或符号计为一个字符，超过部分会自动截断。

              示例值：参考这个风格的图片，生成番茄炒蛋。

              <strong>注意</strong>：`content`数组中，必须且只能包含一个含 `text` 字段的对象。

              <strong>image</strong>`string`（可选）

              输入图像的URL或Base64编码字符串。

              图像限制：

              - 图像格式：JPEG、JPG、PNG（不支持透明通道）、BMP、WEBP。
              - 图像分辨率：图像的宽高范围均为\[240, 8000]像素。
              - 文件大小：不超过10MB。

              图像数量限制：

              - 输入图像数量与`parameters.enable_interleave`参数有关。

                - 当`enable_interleave=true`时（图文混排输出），可输入<strong>0\~1</strong>张图像。
                - 当`enable_interleave=false`时（图像编辑），<strong>必须</strong>输入<strong>1\~4</strong>张图像。
              - 当输入多张图像时，需在`content`数组中传入多个`image`对象，并按照数组顺序定义图像顺序。

              支持的输入格式：

              1. 使用公网可访问URL

                 - 支持 HTTP 或 HTTPS 协议。
                 - 示例值：`http://wanx.alicdn.com/material/xxx.jpeg`。
              2. 传入 Base64 编码图像后的字符串

                 - 格式：data:\{MIME\_type};base64,\{base64\_data}
                 - 示例：data:image/jpeg;base64,GDU7MtCZzEbTbmRZ...（仅示意，实际需传入完整字符串）
                 - Base64 编码规范请参见[图像传入方式](/zh/model-studio/wan-image-edit#8db0e2215frua)。
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

          输出图片分辨率参数，支持参考输入图比例和直接指定两种方式。

          当`enable_interleave=false`（即图像编辑模式）时：

          - 方式一：参考输入图比例（推荐）

            可选的输出分辨率档位：`1K`（默认）、`2K`。

            - `1K`：输出总像素接近 1280\*1280，宽高比与最后一张输入图像一致。
            - `2K`：输出总像素接近 2048\*2048，宽高比与最后一张输入图像一致。
          - 方式二：指定生成图像的宽高像素值

            总像素在 \[768*768, 2048*2048] 之间，且宽高比范围为 \[1:4, 4:1]。

            > 实际输出图像的像素值为接近指定值的16的倍数。

          当`enable_interleave=true`（即图文混排输出模式）时：

          - 方式一：参考输入图比例（默认方式）

            - 若输入图像总像素 ≤ 1280\*1280，输出总像素和宽高比与输入图像一致。
            - 若输入图像总像素 > 1280*1280，输出总像素接近 1280*1280，宽高比与输入图像一致。
            - 若无输入图像，则为1280\*1280。
          - 方式二：指定生成图像的宽高像素值

            总像素在 \[768*768, 1280*1280] 之间，且宽高比范围为 \[1:4, 4:1]。

            > 实际输出图像的像素值为接近指定值的16的倍数。

          <Accordion title="常见比例推荐的分辨率" defaultOpen>
            * 1:1：1280\*1280
            * 2:3：800\*1200
            * 3:2：1200\*800
            * 3:4：960\*1280
            * 4:3：1280\*960
            * 9:16：720\*1280
            * 16:9：1280\*720
            * 21:9：1344\*576
          </Accordion>

          <strong>enable\_interleave</strong> `bool` （可选）

          控制生图模式：

          - false：默认值，表示图像编辑模式（支持多图输入及主体一致性生成）。

            - 用途：基于1～4张输入图像进行编辑、风格迁移或主体一致性生成。
            - 输入：必须提供至少1张参考图像。
            - 输出：可生成1至4张结果图像。
          - true ：表示启用图文混排输出模式（仅支持传入一张图像或不传图像）。

            - 用途：根据文本描述生成图文并茂的内容，或进行纯文本生成图像（文生图）。

              纯文生图场景建议优先使用[wan2.6-t2i 模型（通义万相-文生图V2）](/zh/model-studio/text-to-image-v2-api-reference)。
            - 输入：可以不提供图像（文生图），或提供最多1张参考图像。
            - 输出：生成包含文本和图像的混合内容。

          <strong>n</strong> `integer` （可选）

          <Tip>
            n直接影响费用。费用 = 单价 × 成功生成的图片张数，请在调用前确认[模型价格](/zh/model-studio/model-pricing#e2540d71a2utl)。
          </Tip>

          指定生成图片的数量。该参数的取值范围与含义取决于 enable\_interleave（模式开关）的状态：

          - 当 `enable_interleave=false`（图像编辑模式）：

            - 作用：直接控制生成图像的数量。
            - 取值范围：1～4，默认值为 4。
            - 建议在测试阶段将此值设置为 1，以便低成本验证效果。
          - 当 `enable_interleave=true`（图文混排模式）：

            - 限制：此参数默认为1，且必须固定为1。若设置为其他值，接口将报错。
            - 说明：在此模式下，如需控制生成图像的数量上限，请使用 `max_images` 参数。

          <strong>max\_images</strong> `integer` （可选）

          <Tip>
            max\_images影响费用。费用 = 单价 × 成功生成的图片张数，请在调用前确认[模型价格](/zh/model-studio/model-pricing#e2540d71a2utl)。
          </Tip>

          仅在图文混排模式（即 `enable_interleave=true`）下生效。

          - 作用：指定模型在单次回复中生成图像的<strong>最大数量</strong>。
          - 取值范围：1～5，默认值为 5。
          - 注意：该参数仅代表“数量上限”。实际生成的图像数量由模型推理决定，可能会少于设定值（例如：设置为 5，模型可能根据内容仅生成 3 张）。

          <strong>prompt\_extend</strong> `bool` （可选）

          仅在图像编辑模式（即`enable_interleave = false`）下生效。

          是否开启 Prompt（提示词）智能改写功能。该功能仅对正向提示词进行优化与润色，不会改变反向提示词。

          - true：默认值，开启智能改写。
          - false：关闭智能改写，使用原始提示词。

          <strong>stream</strong> `bool` （可选）

          控制返回结果是否为流式输出。在图像混排模式（即 `enable_interleave = true`）下，<strong>必须</strong>设置为`true`。

          - false：默认值，非流式输出。
          - true：流式输出。

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
          <Tab title="图像编辑">
            ```bash expandable
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --data '{
                "model": "wan2.6-image",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "text": "参考图1的风格和图2的背景，生成番茄炒蛋"
                                },
                                {
                                    "image": "https://cdn.wanx.aliyuncs.com/tmp/pressure/umbrella1.png"
                                },
                                {
                                    "image": "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp"
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "prompt_extend": true,
                    "watermark": false,
                    "n": 1,
                    "enable_interleave": false,
                    "size": "1K"
                }
            }'
            ```
          </Tab>

          <Tab title="图文混排（仅支持流式）">
            同步接口在启用图文混排输出（即 `parameters.enable_interleave = true`）时，<strong>仅支持流式输出</strong>，必须同时满足以下两项配置：

            - 设置`X-DashScope-Sse`为`enable`。
            - 设置`parameters.stream`为`true`。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'X-DashScope-Sse: enable' \
            --data '{
                "model": "wan2.6-image",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "text": "给我一个3张图辣椒炒肉教程"
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "max_images": 3,
                    "size": "1280*1280",
                    "stream": true,
                    "enable_interleave":true
                }
            }'
            ```
          </Tab>

          <Tab title="dashscope CLI">
            ```bash
            export DASHSCOPE_API_KEY="your-api-key"
            # 将 {WorkspaceId} 替换为业务空间ID，cn-beijing 替换为对应地域（新加坡: ap-southeast-1, 美东: us-east-1）
            export DASHSCOPE_HTTP_BASE_URL="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"
            dashscope image-generation create -m wan2.6-t2i -t "一只可爱的猫"
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
        #### 响应参数 <span id="dee809656c79t" />

        <strong>output</strong> `object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>choices</strong> `array of object`

          模型生成的输出内容。

          <Accordion title="属性" defaultOpen>
            <strong>finish\_reason</strong> `string`

            任务停止原因。

            <strong>非流式输出场景</strong>：自然停止时为`stop`。

            <strong>流式输出场景</strong>：当开启流式输出时，该参数判断数据流是否传输结束。

            - 传输过程中：前序数据包会持续返回 `"finish_reason": "null"`，表示内容仍在生成中，请继续接收。
            - 传输结束时：仅在<strong>最后一个</strong> JSON 结构体中返回 `"finish_reason":"stop"`，表示流式请求已全部结束，应停止接收。

            <strong>message</strong> `object`

            模型返回的消息。

            <Accordion title="属性" defaultOpen>
              <strong>role</strong>`string`

              消息的角色，固定为`assistant`。

              <strong>content</strong>`array`

              <Accordion title="属性" defaultOpen>
                <strong>type</strong> `string`

                输出的类型，枚举值为text、image。

                <strong>text</strong> `string`

                生成的文字。

                <strong>image</strong> `string`

                生成图像的 URL，图像格式为PNG。

                <strong>链接有效期为24小时</strong>，请及时下载并保存图像。
              </Accordion>
            </Accordion>
          </Accordion>

          <strong>finished</strong> `bool`

          请求结束标志符。

          - true：表示请求结束。
          - false：表示请求未结束。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          生成图像的张数。

          <strong>size</strong> `string`

          生成的图像分辨率。示例值：1376\*768。

          <strong>input\_tokens</strong> `integer`

          按图片张数计费。

          - 在图像编辑模式下，固定为0。
          - 在图文混排模式下，此字段统计输入文本的token数量（不计费）。

          <strong>output\_tokens</strong> `integer`

          按图片张数计费。

          - 在图像编辑模式下，固定为0。
          - 在图文混排模式下，此字段统计输出文本的token数量（不计费）。

          <strong>total\_tokens</strong>`integer`

          按图片张数计费。

          - 在图像编辑模式下，固定为0。
          - 在图文混排模式下，此字段统计总token数量（不计费）。
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
                                        "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx.png?Expires=xxx",
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
                    "size": "1376*768",
                    "total_tokens": 0
                },
                "request_id": "a3f4befe-cacd-49c9-8298-xxxxxx"
            }
            ```
          </Tab>

          <Tab title="任务执行成功（流式输出）">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            ```json
            {"output":{"choices":[{"message":{"content":[{"type":"text","text":"肉"}],"role":"assistant"},"finish_reason":"null"}],"finished":true},"usage":{"total_tokens":571,"image_count":3,"output_tokens":543,"size":"1280*1280","input_tokens":28},"request_id":"d2dcb952-bf91-4a6a-aad5-xxxxxx"}
            {"output":{"choices":[{"message":{"content":[{"type":"text","text":"香"}],"role":"assistant"},"finish_reason":"null"}],"finished":true},"usage":{"total_tokens":572,"image_count":3,"output_tokens":544,"size":"1280*1280","input_tokens":28},"request_id":"d2dcb952-bf91-4a6a-aad5-xxxxxx"}
            {"output":{"choices":[{"message":{"content":[{"type":"text","text":"交织"}],"role":"assistant"},"finish_reason":"null"}],"finished":true},"usage":{"total_tokens":573,"image_count":3,"output_tokens":545,"size":"1280*1280","input_tokens":28},"request_id":"d2dcb952-bf91-4a6a-aad5-xxxxxx"}
            ......
            {"output":{"choices":[{"message":{"content":[{"type":"image","image":"https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx.png?Expires=xxxx"}],"role":"assistant"},"finish_reason":"null"}],"finished":true},"usage":{"total_tokens":557,"image_count":3,"output_tokens":529,"size":"1280*1280","input_tokens":28},"request_id":"d2dcb952-bf91-4a6a-aad5-xxxxxx"}
            {"output":{"choices":[{"message":{"content":[{"type":"text","text":"趁"}],"role":"assistant"},"finish_reason":"null"}],"finished":true},"usage":{"total_tokens":558,"image_count":3,"output_tokens":530,"size":"1280*1280","input_tokens":28},"request_id":"d2dcb952-bf91-4a6a-aad5-xxxxxx"}
            {"output":{"choices":[{"message":{"content":[{"type":"text","text":"热"}],"role":"assistant"},"finish_reason":"null"}],"finished":true},"usage":{"total_tokens":559,"image_count":3,"output_tokens":531,"size":"1280*1280","input_tokens":28},"request_id":"d2dcb952-bf91-4a6a-aad5-xxxxxx"}
            {"output":{"choices":[{"message":{"content":[{"type":"text","text":"夹"}],"role":"assistant"},"finish_reason":"null"}],"finished":true},"usage":{"total_tokens":560,"image_count":3,"output_tokens":532,"size":"1280*1280","input_tokens":28},"request_id":"d2dcb952-bf91-4a6a-aad5-xxxxxx"}
            {"output":{"choices":[{"message":{"content":[{"type":"text","text":"起"}],"role":"assistant"},"finish_reason":"null"}],"finished":true},"usage":{"total_tokens":561,"image_count":3,"output_tokens":533,"size":"1280*1280","input_tokens":28},"request_id":"d2dcb952-bf91-4a6a-aad5-xxxxxx"}
            {"output":{"choices":[{"message":{"content":[{"type":"text","text":"一块"}],"role":"assistant"},"finish_reason":"null"}],"finished":true},"usage":{"total_tokens":562,"image_count":3,"output_tokens":534,"size":"1280*1280","input_tokens":28},"request_id":"d2dcb952-bf91-4a6a-aad5-xxxxxx"}
            {"output":{"choices":[{"message":{"content":[{"type":"text","text":"肉"}],"role":"assistant"},"finish_reason":"stop"}],"finished":true},"usage":{"total_tokens":563,"image_count":3,"output_tokens":535,"size":"1280*1280","input_tokens":28},"request_id":"d2dcb952-bf91-4a6a-aad5-xxxxxx"}
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

## HTTP异步调用 <span id="42703589880ts" />

流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="db8cf70432oe3" />

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
        #### 请求参数 <span id="f54fd191909ri" />

        ##### 请求头（Headers） <span id="0d489ca36e4mj" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="d6b6a20821cp8" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。

        示例值：wan2.6-image

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息。

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。当前<strong>仅支持单轮对话</strong>，即传入一组role、content参数，不支持多轮对话。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string` <strong>（必选）</strong>

            消息的角色。此参数固定设置为`user`。

            <strong>content</strong>`array` <strong>（必选）</strong>

            消息内容数组。

            <Accordion title="属性" defaultOpen>
              <strong>text</strong>`string`<strong>（必选）</strong>

              正向提示词用于描述您期望生成的图像内容、风格和构图。

              支持中英文，长度不超过2000个字符，每个汉字、字母、数字或符号计为一个字符，超过部分会自动截断。

              示例值：参考这个风格的图片，生成番茄炒蛋。

              <strong>注意</strong>：`content`数组中，必须且只能包含一个含 `text` 字段的对象。

              <strong>image</strong>`string`（可选）

              输入图像的URL或Base64编码字符串。

              图像限制：

              - 图像格式：JPEG、JPG、PNG（不支持透明通道）、BMP、WEBP。
              - 图像分辨率：图像的宽高范围均为\[240, 8000]像素。
              - 文件大小：不超过10MB。

              图像数量限制：

              - 输入图像数量与`parameters.enable_interleave`参数有关。

                - 当`enable_interleave=true`时（图文混排输出），可输入<strong>0\~1</strong>张图像。
                - 当`enable_interleave=false`时（图像编辑），<strong>必须</strong>输入<strong>1\~4</strong>张图像。
              - 当输入多张图像时，需在`content`数组中传入多个`image`对象，并按照数组顺序定义图像顺序。

              支持的输入格式：

              1. 使用公网可访问URL

                 - 支持 HTTP 或 HTTPS 协议。
                 - 示例值：`http://wanx.alicdn.com/material/xxx.jpeg`。
              2. 传入 Base64 编码图像后的字符串

                 - 格式：data:\{MIME\_type};base64,\{base64\_data}
                 - 示例：data:image/jpeg;base64,GDU7MtCZzEbTbmRZ...（仅示意，实际需传入完整字符串）
                 - Base64 编码规范请参见[图像传入方式](/zh/model-studio/wan-image-edit#8db0e2215frua)。
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

          输出图片分辨率参数，支持参考输入图比例和直接指定两种方式。

          当`enable_interleave=false`（即图像编辑模式）时：

          - 方式一：参考输入图比例（推荐）

            可选的输出分辨率档位：`1K`（默认）、`2K`。

            - `1K`：输出总像素接近 1280\*1280，宽高比与最后一张输入图像一致。
            - `2K`：输出总像素接近 2048\*2048，宽高比与最后一张输入图像一致。
          - 方式二：指定生成图像的宽高像素值

            总像素在 \[768*768, 2048*2048] 之间，且宽高比范围为 \[1:4, 4:1]。

            > 实际输出图像的像素值为接近指定值的16的倍数。

          当`enable_interleave=true`（即图文混排输出模式）时：

          - 方式一：参考输入图比例（默认方式）

            - 若输入图像总像素 ≤ 1280\*1280，输出总像素和宽高比与输入图像一致。
            - 若输入图像总像素 > 1280*1280，输出总像素接近 1280*1280，宽高比与输入图像一致。
            - 若无输入图像，则为1280\*1280。
          - 方式二：指定生成图像的宽高像素值

            总像素在 \[768*768, 1280*1280] 之间，且宽高比范围为 \[1:4, 4:1]。

            > 实际输出图像的像素值为接近指定值的16的倍数。

          <Accordion title="常见比例推荐的分辨率" defaultOpen>
            * 1:1：1280\*1280
            * 2:3：800\*1200
            * 3:2：1200\*800
            * 3:4：960\*1280
            * 4:3：1280\*960
            * 9:16：720\*1280
            * 16:9：1280\*720
            * 21:9：1344\*576
          </Accordion>

          <strong>enable\_interleave</strong> `bool` （可选）

          控制生图模式：

          - false：默认值，表示图像编辑模式（支持多图输入及主体一致性生成）。

            - 用途：基于1～4张输入图像进行编辑、风格迁移或主体一致性生成。
            - 输入：必须提供至少1张参考图像。
            - 输出：可生成1至4张结果图像。
          - true ：表示启用图文混排输出模式（仅支持传入一张图像或不传图像）。

            - 用途：根据文本描述生成图文并茂的内容，或进行纯文本生成图像（文生图）。

              纯文生图场景建议优先使用[wan2.6-t2i 模型（通义万相-文生图V2）](/zh/model-studio/text-to-image-v2-api-reference)。
            - 输入：可以不提供图像（文生图），或提供最多1张参考图像。
            - 输出：生成包含文本和图像的混合内容。

          <strong>n</strong> `integer` （可选）

          <Tip>
            n直接影响费用。费用 = 单价 × 成功生成的图片张数，请在调用前确认[模型价格](/zh/model-studio/model-pricing#e2540d71a2utl)。
          </Tip>

          指定生成图片的数量。该参数的取值范围与含义取决于 enable\_interleave（模式开关）的状态：

          - 当 `enable_interleave=false`（图像编辑模式）：

            - 作用：直接控制生成图像的数量。
            - 取值范围：1～4，默认值为 4。
            - 建议在测试阶段将此值设置为 1，以便低成本验证效果。
          - 当 `enable_interleave=true`（图文混排模式）：

            - 限制：此参数默认为1，且必须固定为1。若设置为其他值，接口将报错。
            - 说明：在此模式下，如需控制生成图像的数量上限，请使用 `max_images` 参数。

          <strong>max\_images</strong> `integer` （可选）

          <Tip>
            max\_images影响费用。费用 = 单价 × 成功生成的图片张数，请在调用前确认[模型价格](/zh/model-studio/model-pricing#e2540d71a2utl)。
          </Tip>

          仅在图文混排模式（即 `enable_interleave=true`）下生效。

          - 作用：指定模型在单次回复中生成图像的<strong>最大数量</strong>。
          - 取值范围：1～5，默认值为 5。
          - 注意：该参数仅代表“数量上限”。实际生成的图像数量由模型推理决定，可能会少于设定值（例如：设置为 5，模型可能根据内容仅生成 3 张）。

          <strong>prompt\_extend</strong> `bool` （可选）

          仅在图像编辑模式（即`enable_interleave = false`）下生效。

          是否开启 Prompt（提示词）智能改写功能。该功能仅对正向提示词进行优化与润色，不会改变反向提示词。

          - true：默认值，开启智能改写。
          - false：关闭智能改写，使用原始提示词。

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
        <CodeGroup>
          ```bash 图像编辑 expandable
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
          --header 'Content-Type: application/json' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'X-DashScope-Async: enable' \
          --data '{
              "model": "wan2.6-image",
              "input": {
                  "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "text": "参考图1的风格和图2的背景，生成番茄炒蛋"
                              },
                              {
                                  "image": "https://cdn.wanx.aliyuncs.com/tmp/pressure/umbrella1.png"
                              },
                              {
                                  "image": "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp"
                              }
                          ]
                      }
                  ]
              },
              "parameters": {
                  "prompt_extend": true,
                  "watermark": false,
                  "n": 1,
                  "enable_interleave": false,
                  "size": "1K"
              }
          }'
          ```

          ```bash 图文混排输出
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
          --header 'Content-Type: application/json' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'X-DashScope-Async: enable' \
          --data '{
              "model": "wan2.6-image",
              "input": {
                  "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "text": "给我一个3张图辣椒炒肉教程"
                              }
                          ]
                      }
                  ]
              },
              "parameters": {
                  "max_images": 3,
                  "size": "1280*1280",
                  "enable_interleave":true
              }
          }'
          ```
        </CodeGroup>
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
        #### 响应参数 <span id="b7e833ee1f962" />

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

          <strong>finished</strong> `bool`

          请求结束标志符。

          - true：表示请求结束。
          - false：表示请求未结束。

          <strong>choices</strong> `array of object`

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
                <strong>type</strong> `string`

                输出的类型，枚举值为text、image。

                <strong>text</strong> `string`

                生成的文字。

                <strong>image</strong> `string`

                生成图像的 URL，图像格式为PNG。

                <strong>链接有效期为24小时</strong>，请及时下载并保存图像。
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          生成图像的张数。

          <strong>size</strong> `string`

          生成的图像分辨率。示例值：1376\*768。

          <strong>input\_tokens</strong> `integer`

          输入token数量。按图片张数计费，当前固定为0。

          <strong>output\_tokens</strong> `integer`

          输出token数量。按图片张数计费，当前固定为0。

          <strong>total\_tokens</strong>`integer`

          总token数量。按图片张数计费，当前固定为0。
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
                "request_id": "43d9e959-25bc-4dc7-9888-xxxxxx",
                "output": {
                    "task_id": "858cad55-4bdc-4ba3-ae6c-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-12-16 04:21:02.275",
                    "scheduled_time": "2025-12-16 04:21:02.304",
                    "end_time": "2025-12-16 04:24:46.658",
                    "finished": true,
                    "choices": [
                        {
                            "finish_reason": "stop",
                            "message": {
                                "role": "assistant",
                                "content": [
                                    {
                                        "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/1xxx.png?Expires=xxx",
                                        "type": "image"
                                    }
                                ]
                            }
                        },
                        {
                            "finish_reason": "stop",
                            "message": {
                                "role": "assistant",
                                "content": [
                                    {
                                        "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/1xxx.png?Expires=xxx",
                                        "type": "image"
                                    }
                                ]
                            }
                        }
                    ]
                },
                "usage": {
                    "size": "1376*768",
                    "total_tokens": 0,
                    "image_count": 2,
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

## DashScope Python SDK调用 <span id="1751252d8bf7c" />

SDK 的参数命名与HTTP接口基本一致，参数结构根据语言特性进行封装。

由于任务耗时较长，SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

<Tip>
  请确保 DashScope Python SDK 版本<strong>不低于</strong>`1.25.8`，再运行以下代码。更新请参考[安装SDK](/zh/model-studio/install-sdk)。
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

### 图像编辑 <span id="626b76fc868or" />

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="1f90197d8cagy" />

    ```python expandable
    import os
    import dashscope
    from dashscope.aigc.image_generation import ImageGeneration
    from dashscope.api_entities.dashscope_response import Message

    # 以下为华北2（北京）地域的URL。请将{WorkspaceId}替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    message = Message(
        role="user",
        # 支持本地文件 如 "image": "file://umbrella1.png"
        content=[
            {
                "text": "参考图1的风格和图2的背景，生成番茄炒蛋"
            },
            {
                "image": "https://cdn.wanx.aliyuncs.com/tmp/pressure/umbrella1.png"
            },
            {
                "image": "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp"
            }
        ]
    )
    print("----sync call, please wait a moment----")
    rsp = ImageGeneration.call(
            model='wan2.6-image',
            api_key=api_key,
            messages=[message],
            negative_prompt="",
            prompt_extend=True,
            watermark=False,
            n=1,
            enable_interleave=False,
            size="1K"
        )

    print(rsp)
    ```

    ##### 响应示例 <span id="b0515f126ag6l" />

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "b6a4c68d-3a91-4018-ae96-3cf373xxxxxx",
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
            "size": "1376*768",
            "total_tokens": 0
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="7bd0a67abeuuz" />

    ```python expandable
    import os
    import dashscope
    from dashscope.aigc.image_generation import ImageGeneration
    from dashscope.api_entities.dashscope_response import Message
    from http import HTTPStatus

    # 以下为华北2（北京）地域的URL。请将{WorkspaceId}替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    # 创建异步任务
    def create_async_task():
        print("Creating async task...")
        message = Message(
            role="user",
            content=[
                {'text': '参考图1的风格和图2的背景，生成番茄炒蛋'},
                {'image': 'https://cdn.wanx.aliyuncs.com/tmp/pressure/umbrella1.png'},
                {'image': 'https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp'}
            ]
        )
        response = ImageGeneration.async_call(
            model="wan2.6-image",
            api_key=api_key,
            messages=[message],
            negative_prompt="",
            prompt_extend=True,
            watermark=False,
            n=1,
            enable_interleave=False,
            size="1K"
        )

        if response.status_code == 200:
            print("Task created successfully:", response)
            return response  # 返回任务ID
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
        "request_id": "4fb3050f-de57-4a24-84ff-e37ee5xxxxxx",
        "code": "",
        "message": "",
        "output": {
            "text": null,
            "finish_reason": null,
            "choices": null,
            "audio": null,
            "task_id": "127ec645-118f-4884-955d-0eba8dxxxxxx",
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
        "request_id": "b2a7fab4-5e00-4b0a-86fe-8b9964xxxxxx",
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
            "task_id": "127ec645-118f-4884-955d-0eba8xxxxxx",
            "task_status": "SUCCEEDED",
            "submit_time": "2026-01-09 17:52:04.136",
            "scheduled_time": "2026-01-09 17:52:04.164",
            "end_time": "2026-01-09 17:52:25.408",
            "finished": true
        },
        "usage": {
            "input_tokens": 0,
            "output_tokens": 0,
            "characters": 0,
            "size": "1376*768",
            "total_tokens": 0,
            "image_count": 1
        }
    }
    ```
  </Tab>
</Tabs>

### 图文混合输出 <span id="a988ae3cdau3e" />

<Tabs>
  <Tab title="同步调用（仅支持流式）">
    ##### 请求示例 <span id="e677ebd437p7j" />

    ```python expandable
    import os
    import dashscope
    from dashscope.aigc.image_generation import ImageGeneration
    from dashscope.api_entities.dashscope_response import Message

    # 以下为华北2（北京）地域的URL。请将{WorkspaceId}替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sync_call_with_stream():
        print("\n========== 同步调用 - 流式图文输出 ==========")

        image_message = Message(
            role="user",
            content=[
                {
                    "text": "给我一个3张图辣椒炒肉教程"
                }
            ]
        )

        image_stream_res = ImageGeneration.call(
            model="wan2.6-image",
            api_key=api_key,
            messages=[image_message],
            stream=True,  # 仅支持流式调用
            negative_prompt="",
            enable_interleave=True,
            max_images=3,
            size="1280*1280"
        )

        print("流式输出结果:")
        for stream_res in image_stream_res:
            print(stream_res)

    if __name__ == "__main__":
        sync_call_with_stream()
    ```

    ##### 响应示例 <span id="95e7baa6180vj" />

    > url 有效期24小时，请及时下载图像。

    ```json
    {"status_code": 200, "request_id": "5b98e8f3-aeff-4c20-a26c-499a7525axxx", "code": "", "message": "", "output": {"text": null, "finish_reason": null, "choices": [{"finish_reason": "null", "message": {"role": "assistant", "content": [{"type": "text", "text": "辣椒"}]}}], "audio": null, "finished": false}, "usage": {"input_tokens": 28, "output_tokens": 0, "characters": 0, "total_tokens": 28, "image_count": 0, "size": "0*0"}}
    {"status_code": 200, "request_id": "5b98e8f3-aeff-4c20-a26c-499a7525axxx", "code": "", "message": "", "output": {"text": null, "finish_reason": null, "choices": [{"finish_reason": "null", "message": {"role": "assistant", "content": [{"type": "text", "text": "炒"}]}}], "audio": null, "finished": false}, "usage": {"input_tokens": 28, "output_tokens": 1, "characters": 0, "total_tokens": 29, "image_count": 0, "size": "0*0"}}
    {"status_code": 200, "request_id": "5b98e8f3-aeff-4c20-a26c-499a7525axxx", "code": "", "message": "", "output": {"text": null, "finish_reason": null, "choices": [{"finish_reason": "null", "message": {"role": "assistant", "content": [{"type": "text", "text": "肉"}]}}], "audio": null, "finished": false}, "usage": {"input_tokens": 28, "output_tokens": 2, "characters": 0, "total_tokens": 30, "image_count": 0, "size": "0*0"}}

    ......

    {"status_code": 200, "request_id": "5b98e8f3-aeff-4c20-a26c-499a7525axxx", "code": "", "message": "", "output": {"text": null, "finish_reason": null, "choices": [{"finish_reason": "null", "message": {"role": "assistant", "content": [{"type": "text", "text": "。"}]}}], "audio": null, "finished": false}, "usage": {"input_tokens": 28, "output_tokens": 398, "characters": 0, "total_tokens": 426, "image_count": 2, "size": "1280*1280"}}
    {"status_code": 200, "request_id": "5b98e8f3-aeff-4c20-a26c-499a7525axxx", "code": "", "message": "", "output": {"text": null, "finish_reason": null, "choices": [{"finish_reason": "stop", "message": {"role": "assistant", "content": [{"type": "image", "image": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx"}]}}], "audio": null, "finished": true}, "usage": {"input_tokens": 28, "output_tokens": 523, "characters": 0, "total_tokens": 551, "image_count": 3, "size": "1280*1280"}}
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="ef2f7d167d3dx" />

    <strong>注意</strong>：异步调用不需要设置stream参数

    ```python expandable
    import os
    import dashscope
    from dashscope.aigc.image_generation import ImageGeneration
    from dashscope.api_entities.dashscope_response import Message
    from http import HTTPStatus

    # 以下为华北2（北京）地域的URL。请将{WorkspaceId}替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def main():
        """异步调用 - 图文输出"""
        print("========== wan2.6-image 异步调用 - 图文输出 ==========")

        image_message = Message(
            role="user",
            content=[
                {
                    "text": "给我一个3张图的辣椒炒肉教程"
                }
            ]
        )

        # 创建异步任务
        print("---async call, creating task----")
        response = ImageGeneration.async_call(
            model="wan2.6-image",
            api_key=api_key,
            messages=[image_message],
            # 异步调用不需要设置stream参数
            negative_prompt="",
            enable_interleave=True,
            max_images=3,
            size="1280*1280"
        )

        if response.status_code == HTTPStatus.OK:
            print(f"任务创建成功:")
            print(response)

            # 等待任务完成
            print("\n---waiting for task completion----")
            status = ImageGeneration.wait(task=response, api_key=api_key)

            if status.output.task_status == "SUCCEEDED":
                print("任务完成!")
                print(f"结果:")
                print(status)
            else:
                print(f"任务失败，状态: {status.output.task_status}")
        else:
            print(f"任务创建失败: {response.code} - {response.message}")

    if __name__ == "__main__":
        try:
            main()
        except Exception as e:
            print(f"执行出错: {str(e)}")
            import traceback
            traceback.print_exc()
    ```

    ##### 响应示例 <span id="3831331c686e8" />

    1、创建任务的响应示例

    ```json
    {
        "status_code": 200,
        "request_id": "4fb3050f-de57-4a24-84ff-e37ee5xxxxxx",
        "code": "",
        "message": "",
        "output": {
            "text": null,
            "finish_reason": null,
            "choices": null,
            "audio": null,
            "task_id": "5c67585e-a3be-4943-b04d-c3fbb2xxxxxx",
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
        "request_id": "997a759b-fbb9-4b35-9a4d-6dab1xxxxxx",
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
                                "text": "辣椒炒肉是湘菜中的经典，也是许多人心中的家常美味。它以鲜辣爽口、肉质鲜嫩而闻名，制作过程简单，却能带来极致的味蕾享受。今天，我们就来一起学习这道菜的制作方法。\n\n首先，准备好所有食材是成功的第一步。新鲜的猪肉、红绿辣椒、蒜瓣和姜片是必不可少的。将猪肉切成薄片，辣椒切段，蒜和姜切片备用。",
                                "type": "text"
                            },
                            {
                                "image": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx",
                                "type": "image"
                            },
                            {
                                "text": "接下来是炒制的关键环节。热锅凉油，放入蒜片和姜片爆香，接着倒入切好的肉片，快速翻炒至肉片变色。肉片炒香后，加入切好的辣椒段，继续翻炒，让辣椒的香气充分释放出来，与肉片的鲜美完美融合。",
                                "type": "text"
                            },
                            {
                                "image": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx",
                                "type": "image"
                            },
                            {
                                "text": "最后，加入适量的生抽、老抽、蚝油和少许糖调味，快速翻炒均匀，让每一片肉和辣椒都裹上酱汁。待汤汁收浓，即可关火出锅。这道色香味俱全的辣椒炒肉就大功告成了！",
                                "type": "text"
                            },
                            {
                                "image": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx",
                                "type": "image"
                            }
                        ]
                    }
                }
            ],
            "audio": null,
            "task_id": "5c67585e-a3be-4943-b04d-c3fbb2xxxxxx",
            "task_status": "SUCCEEDED",
            "submit_time": "2026-01-16 17:47:39.469",
            "scheduled_time": "2026-01-16 17:47:39.804",
            "end_time": "2026-01-16 17:49:46.736",
            "finished": true
        },
        "usage": {
            "input_tokens": 29,
            "output_tokens": 477,
            "characters": 0,
            "size": "1280*1280",
            "total_tokens": 506,
            "image_count": 3
        }
    }
    ```
  </Tab>
</Tabs>

## DashScope Java SDK调用 <span id="a5c3e8d7b9xyz" />

SDK 的参数命名与HTTP接口基本一致，参数结构根据语言特性进行封装。

由于任务耗时较长，SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

<Tip>
  请确保 DashScope Java SDK 版本<strong>不低于</strong>`2.22.6`，再运行以下代码。
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

### 图像编辑 <span id="a89623b02bahd" />

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="d7f8a6b5c9yza" />

    ```java expandable
    import com.alibaba.dashscope.aigc.imagegeneration.*;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.util.Arrays;
    import java.util.Collections;

    /**
     * wan2.6-image 图像编辑 - 同步调用示例
     */
    public class Main {

        static {
            // 以下为北京地域url，各地域的base_url不同
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void basicCall() throws ApiException, NoApiKeyException, UploadFileException {
            // 构建多图输入消息
            ImageGenerationMessage message = ImageGenerationMessage.builder()
                    .role("user")
                    .content(Arrays.asList(
                            // 支持多图输入，可以提供多张参考图片
                            Collections.singletonMap("text", "参考图1的风格和图2的背景，生成番茄炒蛋"),
                            Collections.singletonMap("image", "https://cdn.wanx.aliyuncs.com/tmp/pressure/umbrella1.png"),
                            Collections.singletonMap("image", "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp")
                    )).build();

            // 图像编辑使用普通同步调用，不需要设置stream和enable_interleave
            ImageGenerationParam param = ImageGenerationParam.builder()
                    .apiKey(apiKey)
                    .model("wan2.6-image")
                    .messages(Collections.singletonList(message))
                    .n(1)
                    .size("1K")
                    .negativePrompt("")
                    .promptExtend(true)
                    .build();

            ImageGeneration imageGeneration = new ImageGeneration();
            ImageGenerationResult result = null;
            try {
                System.out.println("---sync call for image editing, please wait a moment----");
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

    ##### 响应示例 <span id="b8e9f7a6d5efg" />

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "requestId": "b148327e-830f-414c-a8df-724dec28exxx",
        "usage": {
            "input_tokens": 0,
            "output_tokens": 0,
            "total_tokens": 0,
            "image_count": 1,
            "size": "1376*768"
        },
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
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="b9e8f7d6a5klm" />

    ```java expandable
    import com.alibaba.dashscope.aigc.imagegeneration.*;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.util.Arrays;
    import java.util.Collections;

    /**
     * wan2.6-image 图像编辑 - 异步调用示例
     */
    public class Main {

        static {
            // 以下为北京地域url，各地域的base_url不同
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void asyncCall() throws ApiException, NoApiKeyException, UploadFileException {
            // 构建多图输入消息
            ImageGenerationMessage message = ImageGenerationMessage.builder()
                    .role("user")
                    .content(Arrays.asList(
                            // 支持多图输入，可以提供多张参考图片
                            Collections.singletonMap("text", "参考图1的风格和图2的背景，生成番茄炒蛋"),
                            Collections.singletonMap("image", "https://cdn.wanx.aliyuncs.com/tmp/pressure/umbrella1.png"),
                            Collections.singletonMap("image", "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp")
                    )).build();

            ImageGenerationParam param = ImageGenerationParam.builder()
                    .apiKey(apiKey)
                    .model("wan2.6-image")
                    .n(1)
                    .size("1K")
                    .negativePrompt("")
                    .promptExtend(true)
                    .messages(Arrays.asList(message))
                    .build();

            ImageGeneration imageGeneration = new ImageGeneration();
            ImageGenerationResult result = null;
            try {
                System.out.println("---async call for image editing, creating task----");
                result = imageGeneration.asyncCall(param);
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                throw new RuntimeException(e.getMessage());
            }
            System.out.println("任务创建结果:");
            System.out.println(JsonUtils.toJson(result));

            String taskId = result.getOutput().getTaskId();
            // 等待任务完成
            waitTask(taskId);
        }

        public static void waitTask(String taskId) throws ApiException, NoApiKeyException {
            ImageGeneration imageGeneration = new ImageGeneration();
            System.out.println("\n---waiting for task completion----");
            ImageGenerationResult result = imageGeneration.wait(taskId, apiKey);
            System.out.println("任务完成结果:");
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

    ##### 响应示例 <span id="a8c9f7e6b5pqr" />

    1、创建任务的响应示例

    ```json
    {
        "status_code": 200,
        "request_id": "9cd85950-2e26-4b2c-b562-1694cf928xxx",
        "code": "",
        "message": "",
        "output": {
            "task_id": "4c861fbe-af89-4a2f-8fc5-4bb15c313xxx",
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
        "request_id": "cbdf1424-306e-4a52-82f3-8bf5d8a99xxx",
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
            "size": "1376*768",
            "total_tokens": 0,
            "image_count": 1
        }
    }
    ```
  </Tab>
</Tabs>

### 图文混合输出 <span id="eecec85d17ql5" />

<Tabs>
  <Tab title="同步调用（仅支持流式）">
    ##### 请求示例 <span id="be34d558a0k0l" />

    ```java expandable
    import com.alibaba.dashscope.aigc.imagegeneration.*;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;
    import io.reactivex.Flowable;

    import java.util.Collections;

    /**
     * wan2.6-image 图文输出 - 流式调用示例
     */
    public class Main {

        static {
            // 以下为北京地域url，各地域的base_url不同
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void streamCall() throws ApiException, NoApiKeyException, UploadFileException {
            ImageGenerationMessage message = ImageGenerationMessage.builder()
                    .role("user")
                    .content(Collections.singletonList(
                            Collections.singletonMap("text", "给我一个3张图辣椒炒肉教程")
                    )).build();

            // 图文输出必须使用流式调用
            ImageGenerationParam param = ImageGenerationParam.builder()
                    .apiKey(apiKey)
                    .model("wan2.6-image")
                    .messages(Collections.singletonList(message))
                    .stream(true) // 必须开启流式输出
                    .enableInterleave(true)
                    .size("1280*1280")
                    .negativePrompt("")
                    .maxImages(3)
                    .build();

            ImageGeneration imageGeneration = new ImageGeneration();
            try {
                System.out.println("---stream call for image interleave----");
                Flowable<ImageGenerationResult> resultFlowable = imageGeneration.streamCall(param);
                resultFlowable.blockingForEach(result -> {
                    System.out.println(JsonUtils.toJson(result));
                });
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                throw new RuntimeException(e.getMessage());
            }
        }

        public static void main(String[] args) {
            try {
                streamCall();
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```

    ##### 响应示例 <span id="55f0d419c4pkv" />

    > url 有效期24小时，请及时下载图像。

    ```json
    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":0,"total_tokens":28,"image_count":0,"size":"0*0"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"text","text":"辣椒"}]}}],"finished":false},"status_code":200,"code":"","message":""}
    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":1,"total_tokens":29,"image_count":0,"size":"0*0"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"text","text":"炒"}]}}],"finished":false},"status_code":200,"code":"","message":""}
    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":2,"total_tokens":30,"image_count":0,"size":"0*0"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"text","text":"肉"}]}}],"finished":false},"status_code":200,"code":"","message":""}

    ......

    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":73,"total_tokens":101,"image_count":0,"size":"0*0"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"text","text":"。"}]}}],"finished":false},"status_code":200,"code":"","message":""}
    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":198,"total_tokens":226,"image_count":1,"size":"1280*1280"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"image","image":"https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx"}]}}],"finished":false},"status_code":200,"code":"","message":""}
    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":199,"total_tokens":227,"image_count":1,"size":"1280*1280"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"text","text":"接下来"}]}}],"finished":false},"status_code":200,"code":"","message":""}

    ......

    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":245,"total_tokens":273,"image_count":1,"size":"1280*1280"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"text","text":"。"}]}}],"finished":false},"status_code":200,"code":"","message":""}
    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":368,"total_tokens":396,"image_count":2,"size":"1280*1280"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"image","image":"https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx"}]}}],"finished":false},"status_code":200,"code":"","message":""}
    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":369,"total_tokens":397,"image_count":2,"size":"1280*1280"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"text","text":"最后"}]}}],"finished":false},"status_code":200,"code":"","message":""}

    ......

    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":416,"total_tokens":444,"image_count":2,"size":"1280*1280"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"text","text":"锅"}]}}],"finished":false},"status_code":200,"code":"","message":""}
    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":417,"total_tokens":445,"image_count":2,"size":"1280*1280"},"output":{"choices":[{"finish_reason":"null","message":{"role":"assistant","content":[{"type":"text","text":"。"}]}}],"finished":false},"status_code":200,"code":"","message":""}
    {"requestId":"12c7432c-8028-4289-a97c-4e22df98bxxx","usage":{"input_tokens":28,"output_tokens":541,"total_tokens":569,"image_count":3,"size":"1280*1280"},"output":{"choices":[{"finish_reason":"stop","message":{"role":"assistant","content":[{"type":"image","image":"https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx"}]}}],"finished":true},"status_code":200,"code":"","message":""}
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="31bd2b2db4s5m" />

    <strong>注意</strong>：异步调用不需要设置stream参数

    ```java expandable
    import com.alibaba.dashscope.aigc.imagegeneration.*;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.util.Collections;

    /**
     * wan2.6-image 图文输出 - 异步调用示例
     */
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
                            Collections.singletonMap("text", "给我一个3张图的辣椒炒肉教程")
                    )).build();

            ImageGenerationParam param = ImageGenerationParam.builder()
                    .apiKey(apiKey)
                    .model("wan2.6-image")
                    .size("1280*1280")
                    .enableInterleave(true)
                    .maxImages(3)
                    .negativePrompt("")
                    .messages(Collections.singletonList(message))
                    .build();

            ImageGeneration imageGeneration = new ImageGeneration();
            ImageGenerationResult result = null;
            try {
                System.out.println("---async call for image interleave, creating task----");
                result = imageGeneration.asyncCall(param);
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                throw new RuntimeException(e.getMessage());
            }
            System.out.println("任务创建结果:");
            System.out.println(JsonUtils.toJson(result));

            String taskId = result.getOutput().getTaskId();
            // 等待任务完成
            waitTask(taskId);
        }

        public static void waitTask(String taskId) throws ApiException, NoApiKeyException {
            ImageGeneration imageGeneration = new ImageGeneration();
            System.out.println("\n---waiting for task completion----");
            ImageGenerationResult result = imageGeneration.wait(taskId, apiKey);
            System.out.println("任务完成结果:");
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

    ##### 响应示例 <span id="f8468ee4c3kwp" />

    1、创建任务的响应示例

    ```json
    {
        "requestId": "7d6c5760-334b-48c4-9b1e-08ee9c7fexxx",
        "output": {
            "task_id": "1bb9d9fa-bf1a-43dc-b5fe-366c1dc70xxx",
            "task_status": "PENDING"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```

    2、查询任务结果的响应示例

    > url 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "requestId": "6ed62b00-2225-4fc3-8ee3-2aed0b484xxx",
        "usage": {
            "input_tokens": 29,
            "output_tokens": 471,
            "total_tokens": 500,
            "image_count": 3,
            "size": "1280*1280"
        },
        "output": {
            "choices": [
                {
                    "finish_reason": "stop",
                    "message": {
                        "role": "assistant",
                        "content": [
                            {
                                "text": "辣椒炒肉是湘菜中的经典，也是许多人心中的家常美味。它以鲜辣爽口、肉质鲜嫩而闻名，制作过程简单，却能带来极致的味蕾享受。今天，我们就来一起学习这道菜的制作方法。\n\n首先，准备好所有食材是成功的第一步。新鲜的猪肉、红绿辣椒、蒜瓣和姜片是必不可少的。将猪肉切成薄片，辣椒切段，蒜和姜切片备用。",
                                "type": "text"
                            },
                            {
                                "image": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx",
                                "type": "image"
                            },
                            {
                                "text": "接下来是腌制猪肉。将切好的猪肉片放入碗中，加入少许生抽、料酒、淀粉和食用油，用手抓匀，腌制10-15分钟。这样处理过的肉片会更加滑嫩入味。",
                                "type": "text"
                            },
                            {
                                "image": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx",
                                "type": "image"
                            },
                            {
                                "text": "最后，热锅凉油，放入腌制好的肉片快速滑炒至变色，盛出备用。锅中留底油，放入蒜片和姜片爆香，再加入辣椒段翻炒出香味。随后倒入肉片，加入生抽、老抽、少许糖和蚝油，快速翻炒均匀，撒上葱花即可出锅。",
                                "type": "text"
                            },
                            {
                                "image": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx",
                                "type": "image"
                            }
                        ]
                    }
                }
            ],
            "task_id": "1bb9d9fa-bf1a-43dc-b5fe-366c1dc70836",
            "task_status": "SUCCEEDED",
            "finished": true,
            "submit_time": "2026-01-16 18:26:32.082",
            "scheduled_time": "2026-01-16 18:26:32.133",
            "end_time": "2026-01-16 18:28:41.748"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>
</Tabs>

## 使用限制 <span id="20263d27eeksq" />

- <strong>数据时效</strong>：任务`task_id`和 图像`url`均只保留 24 小时，过期后将无法查询或下载。
- <strong>内容审核</strong>：输入的 `prompt` 和输出的图像均会经过内容安全审核，包含违规内容的请求将报错“IPInfringementSuspect”或“DataInspectionFailed”，具体参见[错误码](/zh/model-studio/error-code)。

## 计费与限流 <span id="5dafefe82bgrh" />

- 模型免费额度和计费单价请参见[模型价格](/zh/model-studio/model-pricing#e2540d71a2utl)。
- 模型限流请参见[万相](/zh/model-studio/rate-limit#513e0a3df24v7)。
- 计费说明：按成功生成的 <strong>图像张数</strong> 计费。模型调用失败或处理错误不产生任何费用，也不消耗[新人免费额度](/zh/model-studio/new-free-quota)。

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="ed3bed65cdp1s" />

<strong>Q：为什么代码示例无法调用？</strong>

A：请升级您的SDK版本到最新版，更新请参考[安装SDK](/zh/model-studio/install-sdk)。

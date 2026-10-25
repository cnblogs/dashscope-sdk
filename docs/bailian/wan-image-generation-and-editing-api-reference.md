> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-图像生成与编辑2.7 API参考

> 万相2.7图像生成与编辑模型支持文生图、文生组图、图生组图、图像编辑和多图参考生成。

<meta property="og:title" content="万相-图像生成与编辑2.7 API参考" />

## 模型概览 <span id="753cf1c4316ie" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "18.07%" }} /><col style={{ width: "37.48%" }} /><col style={{ width: "44.45%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型简介</strong></p></th><th><p><strong>输出图像规格</strong></p></th></tr></thead><tbody><tr><td><p>wan2.7-image-pro</p></td><td><p>万相2.7 image专业版，文生图（非组图生成）支持4K高清输出</p></td><td rowSpan={2}><p>图片格式：PNG。</p> <p>图像分辨率和尺寸请参见<a href="/zh/model-studio/wan-image-generation-and-editing-api-reference">size参数</a>。</p></td></tr><tr><td><p>wan2.7-image</p></td><td><p>万相2.7 image，生成速度更快</p></td></tr></tbody></table>

<Note>
  调用前，请查阅各地域支持的[模型列表与价格](/zh/model-studio/model-pricing#e2540d71a2utl)。
</Note>

## 前提条件 <span id="0e41425c577u4" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

<Tip>
  华北2（北京）和新加坡地域拥有独立的 <strong>API Key</strong>与<strong>请求地址</strong>，不可混用，跨地域调用将导致鉴权失败或服务报错。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## HTTP同步调用 <span id="wan27-sync-title" />

一次请求即可获得结果，流程简单，推荐大多数场景使用。

<Tabs>
  <Tab title="北京">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "53.92%" }} />

    <col style={{ width: "46.08%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="wan27-h4-params" />
      </td>

      <td rowSpan={8}>
        <Tabs>
          <Tab title="文生图">
            > wan2.7-image-pro仅文生图场景支持4K分辨率，图像编辑和组图生成支持最高2K分辨率

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --data '{
                "model": "wan2.7-image-pro",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {"text": "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵"}
                            ]
                        }
                    ]
                },
                "parameters": {
                    "size": "2K",
                    "n": 1,
                    "watermark": false,
                    "thinking_mode": true
                }
            }'
            ```
          </Tab>

          <Tab title="图像编辑">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --data '{
                "model": "wan2.7-image-pro",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {"image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/pjeqdf/car.webp"},
                                {"image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/xsunlm/paint.webp"},
                                {"text": "把图2的涂鸦喷绘在图1的汽车上"}
                            ]
                        }
                    ]
                },
                "parameters": {
                    "size": "2K",
                    "n": 1,
                    "watermark": false
                }
            }'
            ```
          </Tab>

          <Tab title="交互式编辑">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --data '{
                "model": "wan2.7-image-pro",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {"image": "https://img.alicdn.com/imgextra/i3/O1CN0157XGE51l6iL9441yX_!!6000000004770-49-tps-1104-1472.webp"},
                                {"image": "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp"},
                                {"text": "把图1的闹钟放在图2的框选的位置，保持场景和光线融合自然"}
                            ]
                        }
                    ]
                },
                "parameters": {
                    "bbox_list": [[],[[989, 515, 1138, 681]]],
                    "size": "2K",
                    "n": 1,
                    "watermark": false
                }
            }'
            ```
          </Tab>

          <Tab title="组图生成">
            > wan2.7-image-pro组图生成仅支持最高2K分辨率。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --data '{
                "model": "wan2.7-image-pro",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {"text": "电影感组图，记录同一只流浪橘猫，特征必须前后一致。第一张：春天，橘猫穿梭在盛开的樱花树下；第二张：夏天，橘猫在老街的树荫下乘凉避暑；第三张：秋天，橘猫踩在满地的金色落叶上；第四张：冬天，橘猫在雪地上走留下足迹。"}
                            ]
                        }
                    ]
                },
                "parameters": {
                    "enable_sequential": true,
                    "n": 4,
                    "size": "2K"
                }
            }'
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        ##### 请求头（Headers） <span id="wan27-h5-headers" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。
      </td>
    </tr>

    <tr>
      <td>
        ##### 请求体（Request Body） <span id="wan27-h5-body" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。可选值：`wan2.7-image-pro`、`wan2.7-image`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息。

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。当前<strong>仅支持单轮对话</strong>，即传入一组role、content参数，不支持多轮对话。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong> `string` <strong>（必选）</strong>

            消息的角色。此参数固定设置为`user`。

            <strong>content</strong> `array` <strong>（必选）</strong>

            消息内容数组。

            <Accordion title="属性" defaultOpen>
              <strong>text</strong> `string`

              用户输入提示词。支持中英文，长度不超过5000个字符，每个汉字、字母、数字或符号计为一个字符，超过部分会自动截断。

              <strong>image</strong> `string`

              输入图像的URL或Base64编码字符串。

              图像限制：

              - 图像格式：JPEG、JPG、PNG（不支持透明通道）、BMP、WEBP。
              - 图像分辨率：图像的宽高范围均为\[240, 8000]像素，宽高比范围\[1:8, 8:1]。
              - 文件大小：不超过20MB。

              图像数量限制：

              - 可传入0-9张图片。
              - 当输入多张图像时，需在`content`数组中传入多个`image`对象，并按照数组顺序定义图像顺序。

              支持的输入格式：

              1. 使用公网可访问URL

                 - 支持HTTP或HTTPS协议。
                 - 示例值：`http://wanx.alicdn.com/material/xxx.jpeg`。
              2. 传入 Base64 编码图像后的字符串

                 - 格式：data:\{MIME\_type};base64,\{base64\_data}
                 - 示例：data:image/jpeg;base64,GDU7MtCZzEbTbmRZ...（仅示意，实际需传入完整字符串）
                 - Base64 编码规范请参见[图像传入方式](/zh/model-studio/wan-image-edit#8db0e2215frua)。
            </Accordion>
          </Accordion>
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>parameters</strong> `object` （可选）

        模型参数配置。

        <Accordion title="属性" defaultOpen>
          <strong>bbox\_list</strong> `array[array[array[integer]]]` （可选）

          交互式编辑框选区域。

          - 对应关系：列表长度必须与输入图片数量一致。若某张图片无需编辑，请在对应位置传入空列表 `[]`。
          - 坐标格式：`[x1, y1, x2, y2]`（左上角 x, 左上角 y, 右下角 x, 右下角 y），使用原图绝对像素坐标，左上角坐标为（0，0）。
          - 限制条件：单张图片最多支持 2 个边界框。

          示例：输入 3 张图片，其中第 2 张无框选，第 1 张有两个框选：

          ```json
          [
            [[0, 0, 12, 12], [25, 25, 100, 100]],  # 图 1 (2个框)
            [],                                    # 图 2 (无框)
            [[10, 10, 50, 50]]                    # 图 3 (1个框)
          ]
          ```

          <strong>enable\_sequential</strong> `boolean` （可选）

          控制生图模式：

          - false：默认值。
          - true ：启用组图输出模式。

          <strong>size</strong> `string` （可选）

          关于输出图片分辨率参数，支持以下两种方式，不可混用：

          <strong>模型：wan2.7-image-pro</strong>

          - <strong>方式一：指定输出图片的分辨率（推荐）</strong>

            - 支持 1K、2K（默认）、4K 三种规格
            - <strong>适用范围</strong>：

              - 文生图（无图片输入，非组图生成）：支持1K、2K、4K。
              - 其他场景：支持1K、2K。
            - <strong>各规格总像素</strong>：1K：1024*1024、2K：2048*2048、4K：4096\*4096
            - <strong>图像比例</strong>：

              - 当有图片输入时：输出宽高比与输入图像（多图输入时为最后一张）一致，并缩放到选定分辨率。
              - 当没有图片输入时：输出为正方形。
          - <strong>方式二：指定生成图像的宽高像素值</strong>

            - 文生图：总像素在 \[768*768, 4096*4096] 之间，宽高比范围为 \[1:8, 8:1]。
            - 其他场景：总像素在 \[768*768, 2048*2048] 之间，宽高比范围为 \[1:8, 8:1]。

          <strong>模型：wan2.7-image</strong>

          - <strong>方式一：指定输出图片的分辨率（推荐）</strong>

            - 支持1K、2K（默认）两种规格，不支持4K。
          - <strong>方式二：指定生成图像的宽高像素值</strong>

            - 所有场景下，总像素在 \[768*768, 2048*2048] 之间，宽高比范围为 \[1:8, 8:1]。

          > 输出图片的像素值可能和指定像素值存在微小差异。

          <strong>n</strong> `integer` （可选）

          <Tip>
            n直接影响费用。费用 = 单价 × 成功生成的图片张数，请在调用前确认[模型价格](/zh/model-studio/model-pricing#e2540d71a2utl)。
          </Tip>

          - 关闭组图模式时，该数值代表生成图像数量，取值范围 1-4，<strong>默认为 1</strong>；
          - 开启组图模式时，该数值代表最大生成图像数量，取值范围 1-12，<strong>默认为 12</strong>。实际数量由模型决定且不超过 n。

          <strong>thinking\_mode</strong> `boolean` （可选）

          是否开启思考模式，默认为`true`（开启）。仅在关闭组图模式且无图片输入时生效。开启时，模型将增强推理能力以提升出图质量，但会增加生成耗时。

          <strong>color\_palette</strong> `array` （可选）

          自定义颜色主题，一个包含颜色（hex）和占比（ratio）的对象数组，需要包含 3 至 10 种颜色，推荐设置为 8 种。

          仅当关闭组图模式（`enable_sequential=false`）时可用。

          <Accordion title="属性" defaultOpen>
            <strong>hex</strong> `string` <strong>（必选）</strong>

            十六进制（HEX）格式的色值。

            <strong>ratio</strong> `string` <strong>（必选）</strong>

            颜色所占的百分比，需精确到小数点后两位（如`"25.00%"`）。所有 ratio 值相加<strong>总和必须为 100.00%</strong>。
          </Accordion>

          <Accordion title="点击查看输入示例">
            ```json expandable
            "color_palette": [
                {
                    "hex": "#C2D1E6",
                    "ratio": "23.51%"
                },
                {
                    "hex": "#CDD8E9",
                    "ratio": "20.13%"
                },
                {
                    "hex": "#B5C8DB",
                    "ratio": "15.88%"
                },
                {
                    "hex": "#C0B5B4",
                    "ratio": "13.27%"
                },
                {
                    "hex": "#DAE0EC",
                    "ratio": "10.11%"
                },
                {
                    "hex": "#636574",
                    "ratio": "8.93%"
                },
                {
                    "hex": "#CACAD2",
                    "ratio": "5.55%"
                },
                {
                    "hex": "#CBD4E4",
                    "ratio": "2.62%"
                }
            ]
            ```
          </Accordion>

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
    </tr>
  </tbody>
</table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "53.61%" }} />

    <col style={{ width: "46.39%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 响应参数 <span id="0ef7966fee5nl" />
      </td>

      <td rowSpan={4}>
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
                                        "image": "https://dashscope-xxx.oss-xxx.aliyuncs.com/xxx.png?Expires=xxx",
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
                    "input_tokens": 10867,
                    "output_tokens": 2,
                    "size": "1488*704",
                    "total_tokens": 10869
                },
                "request_id": "71dfc3c6-f796-9972-97e4-bc4efc4faxxx"
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

    <tr>
      <td>
        <strong>output</strong> `object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>choices</strong> `array`

          模型生成的输出内容。

          <Accordion title="属性" defaultOpen>
            <strong>finish\_reason</strong> `string`

            任务停止原因。自然停止时为`stop`。

            <strong>message</strong> `object`

            模型返回的消息。

            <Accordion title="属性" defaultOpen>
              <strong>role</strong>`string`

              消息的角色，固定为`assistant`。

              <strong>content</strong>`array`

              <Accordion title="属性" defaultOpen>
                <strong>type</strong> `string`

                输出的类型，固定为image。

                <strong>image</strong> `string`

                生成图像的 URL，图像格式为PNG。

                <strong>链接有效期为24小时</strong>，请及时下载并保存图像。
              </Accordion>
            </Accordion>
          </Accordion>

          <strong>finished</strong> `boolean`

          任务是否结束。

          - true：已结束。
          - false：未结束。
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          生成图像的张数。

          <strong>size</strong> `string`

          生成的图像分辨率。示例值：1376\*768。

          <strong>input\_tokens</strong> `integer`

          输入token数量（不计费）。按图片张数计费。

          <strong>output\_tokens</strong> `integer`

          输出token数量（不计费）。按图片张数计费。

          <strong>total\_tokens</strong>`integer`

          总token数量（不计费）。按图片张数计费。
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>
    </tr>

    <tr>
      <td>
        <strong>code</strong>`string`

        请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>

      <td />
    </tr>

    <tr>
      <td>
        <strong>message</strong>`string`

        请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>

      <td />
    </tr>
  </tbody>
</table>

## HTTP异步调用 <span id="wan27-async-title" />

适用于耗时较长的任务，支持查询任务状态和结果。

### 步骤1：创建任务获取任务ID <span id="wan27-async-step1-title" />

<Tabs>
  <Tab title="北京">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "53.92%" }} />

    <col style={{ width: "46.08%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="wan27-async-h4-params" />
      </td>

      <td rowSpan={9}>
        <Tabs>
          <Tab title="文生图">
            > wan2.7-image-pro仅文生图场景支持4K分辨率，图像编辑和组图生成支持最高2K分辨率

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header "X-DashScope-Async: enable" \
            --data '{
                "model": "wan2.7-image-pro",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {"text": "一间有着精致窗户的花店，漂亮的木质门，摆放着花朵"}
                            ]
                        }
                    ]
                },
                "parameters": {
                    "size": "2K",
                    "n": 1,
                    "watermark": false,
                    "thinking_mode": true
                }
            }'
            ```
          </Tab>

          <Tab title="图像编辑">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header "X-DashScope-Async: enable" \
            --data '{
                "model": "wan2.7-image-pro",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {"image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/pjeqdf/car.webp"},
                                {"image": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/xsunlm/paint.webp"},
                                {"text": "把图2的涂鸦喷绘在图1的汽车上"}
                            ]
                        }
                    ]
                },
                "parameters": {
                    "size": "2K",
                    "n": 1,
                    "watermark": false
                }
            }'
            ```
          </Tab>

          <Tab title="交互式编辑">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header "X-DashScope-Async: enable" \
            --data '{
                "model": "wan2.7-image-pro",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {"image": "https://img.alicdn.com/imgextra/i3/O1CN0157XGE51l6iL9441yX_!!6000000004770-49-tps-1104-1472.webp"},
                                {"image": "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp"},
                                {"text": "把图1的闹钟放在图2的框选的位置，保持场景和光线融合自然"}
                            ]
                        }
                    ]
                },
                "parameters": {
                    "bbox_list": [[],[[989, 515, 1138, 681]]],
                    "size": "2K",
                    "n": 1,
                    "watermark": false
                }
            }'
            ```
          </Tab>

          <Tab title="组图生成">
            > wan2.7-image-pro组图生成仅支持最高2K分辨率。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header "X-DashScope-Async: enable" \
            --data '{
                "model": "wan2.7-image-pro",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {"text": "电影感组图，记录同一只流浪橘猫，特征必须前后一致。第一张：春天，橘猫穿梭在盛开的樱花树下；第二张：夏天，橘猫在老街的树荫下乘凉避暑；第三张：秋天，橘猫踩在满地的金色落叶上；第四张：冬天，橘猫在雪地上走留下足迹。"}
                            ]
                        }
                    ]
                },
                "parameters": {
                    "enable_sequential": true,
                    "n": 4,
                    "size": "2K"
                }
            }'
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        ##### 请求头（Headers） <span id="wan27-async-h5-headers" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。
      </td>
    </tr>

    <tr>
      <td>
        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>
      </td>
    </tr>

    <tr>
      <td>
        ##### 请求体（Request Body） <span id="wan27-async-h5-body" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。可选值：`wan2.7-image-pro`、`wan2.7-image`。
      </td>
    </tr>

    <tr>
      <td>
        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息。

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。当前<strong>仅支持单轮对话</strong>，即传入一组role、content参数，不支持多轮对话。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong> `string` <strong>（必选）</strong>

            消息的角色。此参数固定设置为`user`。

            <strong>content</strong> `array` <strong>（必选）</strong>

            消息内容数组。

            <Accordion title="属性" defaultOpen>
              <strong>text</strong> `string`

              用户输入提示词。支持中英文，长度不超过5000个字符，每个汉字、字母、数字或符号计为一个字符，超过部分会自动截断。

              <strong>image</strong> `string`

              输入图像的URL或Base64编码字符串。

              图像限制：

              - 图像格式：JPEG、JPG、PNG（不支持透明通道）、BMP、WEBP。
              - 图像分辨率：图像的宽高范围均为\[240, 8000]像素，宽高比范围\[1:8, 8:1]。
              - 文件大小：不超过20MB。

              图像数量限制：

              - 可传入0-9张图片。
              - 当输入多张图像时，需在`content`数组中传入多个`image`对象，并按照数组顺序定义图像顺序。

              支持的输入格式：

              1. 使用公网可访问URL

                 - 支持HTTP或HTTPS协议。
                 - 示例值：`http://wanx.alicdn.com/material/xxx.jpeg`。
              2. 传入 Base64 编码图像后的字符串

                 - 格式：data:\{MIME\_type};base64,\{base64\_data}
                 - 示例：data:image/jpeg;base64,GDU7MtCZzEbTbmRZ...（仅示意，实际需传入完整字符串）
                 - Base64 编码规范请参见[图像传入方式](/zh/model-studio/wan-image-edit#8db0e2215frua)。
            </Accordion>
          </Accordion>
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>parameters</strong> `object` （可选）

        模型参数配置。

        <Accordion title="属性" defaultOpen>
          <strong>bbox\_list</strong> `array[array[array[integer]]]` （可选）

          交互式编辑框选区域。

          - 对应关系：列表长度必须与输入图片数量一致。若某张图片无需编辑，请在对应位置传入空列表 `[]`。
          - 坐标格式：`[x1, y1, x2, y2]`（左上角 x, 左上角 y, 右下角 x, 右下角 y），使用原图绝对像素坐标，左上角坐标为（0，0）。
          - 限制条件：单张图片最多支持 2 个边界框。

          示例：输入 3 张图片，其中第 2 张无框选，第 1 张有两个框选：

          ```json
          [
            [[0, 0, 12, 12], [25, 25, 100, 100]],  # 图 1 (2个框)
            [],                                    # 图 2 (无框)
            [[10, 10, 50, 50]]                    # 图 3 (1个框)
          ]
          ```

          <strong>enable\_sequential</strong> `boolean` （可选）

          控制生图模式：

          - false：默认值。
          - true ：启用组图输出模式。

          <strong>size</strong> `string` （可选）

          关于输出图片分辨率参数，支持以下两种方式，不可混用：

          <strong>模型：wan2.7-image-pro</strong>

          - <strong>方式一：指定输出图片的分辨率（推荐）</strong>

            - 支持 1K、2K（默认）、4K 三种规格
            - <strong>适用范围</strong>：

              - 文生图（无图片输入，非组图生成）：支持1K、2K、4K。
              - 其他场景：支持1K、2K。
            - <strong>各规格总像素</strong>：1K：1024*1024、2K：2048*2048、4K：4096\*4096
            - <strong>图像比例</strong>：

              - 当有图片输入时：输出宽高比与输入图像（多图输入时为最后一张）一致，并缩放到选定分辨率。
              - 当没有图片输入时：输出为正方形。
          - <strong>方式二：指定生成图像的宽高像素值</strong>

            - 文生图：总像素在 \[768*768, 4096*4096] 之间，宽高比范围为 \[1:8, 8:1]。
            - 其他场景：总像素在 \[768*768, 2048*2048] 之间，宽高比范围为 \[1:8, 8:1]。

          <strong>模型：wan2.7-image</strong>

          - <strong>方式一：指定输出图片的分辨率（推荐）</strong>

            - 支持1K、2K（默认）两种规格，不支持4K。
          - <strong>方式二：指定生成图像的宽高像素值</strong>

            - 所有场景下，总像素在 \[768*768, 2048*2048] 之间，宽高比范围为 \[1:8, 8:1]。

          > 输出图片的像素值可能和指定像素值存在微小差异。

          <strong>n</strong> `integer` （可选）

          <Tip>
            n直接影响费用。费用 = 单价 × 成功生成的图片张数，请在调用前确认[模型价格](/zh/model-studio/model-pricing#e2540d71a2utl)。
          </Tip>

          - 关闭组图模式时，该数值代表生成图像数量，取值范围 1-4，<strong>默认为 1</strong>；
          - 开启组图模式时，该数值代表最大生成图像数量，取值范围 1-12，<strong>默认为 12</strong>。实际数量由模型决定且不超过 n。

          <strong>thinking\_mode</strong> `boolean` （可选）

          是否开启思考模式，默认为`true`（开启）。仅在关闭组图模式且无图片输入时生效。开启时，模型将增强推理能力以提升出图质量，但会增加生成耗时。

          <strong>color\_palette</strong> `array` （可选）

          自定义颜色主题，一个包含颜色（hex）和占比（ratio）的对象数组，需要包含 3 至 10 种颜色，推荐设置为 8 种。

          仅当关闭组图模式（`enable_sequential=false`）时可用。

          <Accordion title="属性" defaultOpen>
            <strong>hex</strong> `string` <strong>（必选）</strong>

            十六进制（HEX）格式的色值。

            <strong>ratio</strong> `string` <strong>（必选）</strong>

            颜色所占的百分比，需精确到小数点后两位（如`"25.00%"`）。所有 ratio 值相加<strong>总和必须为 100.00%</strong>。
          </Accordion>

          <Accordion title="点击查看输入示例">
            ```json expandable
            "color_palette": [
                {
                    "hex": "#C2D1E6",
                    "ratio": "23.51%"
                },
                {
                    "hex": "#CDD8E9",
                    "ratio": "20.13%"
                },
                {
                    "hex": "#B5C8DB",
                    "ratio": "15.88%"
                },
                {
                    "hex": "#C0B5B4",
                    "ratio": "13.27%"
                },
                {
                    "hex": "#DAE0EC",
                    "ratio": "10.11%"
                },
                {
                    "hex": "#636574",
                    "ratio": "8.93%"
                },
                {
                    "hex": "#CACAD2",
                    "ratio": "5.55%"
                },
                {
                    "hex": "#CBD4E4",
                    "ratio": "2.62%"
                }
            ]
            ```
          </Accordion>

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
    </tr>
  </tbody>
</table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "53.56%" }} />

    <col style={{ width: "46.44%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 响应参数 <span id="cd3063363egdc" />
      </td>

      <td rowSpan={3}>
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

    <tr>
      <td>
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
      </td>
    </tr>

    <tr>
      <td>
        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>
    </tr>

    <tr>
      <td>
        <strong>code</strong>`string`

        请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>

      <td />
    </tr>

    <tr>
      <td>
        <strong>message</strong>`string`

        请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>

      <td />
    </tr>
  </tbody>
</table>

### 步骤2：根据任务ID查询结果 <span id="wan27-async-step2-title" />

<Tabs>
  <Tab title="北京">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`
  </Tab>
</Tabs>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "53.75%" }} />

    <col style={{ width: "46.25%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求参数 <span id="153278ecd2pkr" />
      </td>

      <td rowSpan={5}>
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

    <tr>
      <td>
        ##### 请求头（Headers） <span id="9c498abd774f2" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。
      </td>
    </tr>

    <tr>
      <td>
        ##### URL路径参数（Path parameters） <span id="5af2d40d88y8w" />
      </td>
    </tr>

    <tr>
      <td>
        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>
    </tr>
  </tbody>
</table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "53.57%" }} />

    <col style={{ width: "46.43%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 响应参数 <span id="bb05ff3e1blpf" />
      </td>

      <td rowSpan={6}>
        <Tabs>
          <Tab title="任务执行成功">
            任务数据（如任务状态、图像URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的图像。

            ```json expandable
            {
                "request_id": "810fa5f5-334c-91f3-aaa4-ed89cf0caxxx",
                "output": {
                    "task_id": "a81ee7cb-014c-473d-b842-76e98311cxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-03-26 17:16:01.663",
                    "scheduled_time": "2026-03-26 17:16:01.716",
                    "end_time": "2026-03-26 17:16:22.961",
                    "finished": true,
                    "choices": [
                        {
                            "finish_reason": "stop",
                            "message": {
                                "role": "assistant",
                                "content": [
                                    {
                                        "image": "https://dashscope-xxx.oss-xxx.aliyuncs.com/xxx.png?Expires=xxx",
                                        "type": "image"
                                    }
                                ]
                            }
                        }
                    ]
                },
                "usage": {
                    "size": "2976*1408",
                    "total_tokens": 11017,
                    "image_count": 1,
                    "output_tokens": 2,
                    "input_tokens": 11015
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

    <tr>
      <td>
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
      </td>
    </tr>

    <tr>
      <td>
        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          生成图像的张数。

          <strong>size</strong> `string`

          生成的图像分辨率。示例值：1376\*768。

          <strong>input\_tokens</strong> `integer`

          输入token数量（不计费）。按图片张数计费。

          <strong>output\_tokens</strong> `integer`

          输出token数量（不计费）。按图片张数计费。

          <strong>total\_tokens</strong>`integer`

          总token数量（不计费）。按图片张数计费。
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>
    </tr>

    <tr>
      <td>
        <strong>code</strong>`string`

        请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>
    </tr>

    <tr>
      <td>
        <strong>message</strong>`string`

        请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>
    </tr>
  </tbody>
</table>

## Python SDK调用 <span id="1751252d8bf7c" />

SDK 参数命名与HTTP接口基本一致。

任务可能耗时较长，SDK 已封装HTTP异步调用流程，同时支持同步和异步调用。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

<Tip>
  请确保 DashScope Python SDK版本<strong>不低于 1.25.15</strong>，再运行以下代码。更新请参考[安装SDK](/zh/model-studio/install-sdk)。
</Tip>

各地域的`base_url`和API Key 不通用，以下示例以北京地域为例进行调用：

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

### 图像编辑 <span id="626b76fc868or" />

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="1f90197d8cagy" />

    ```python expandable
    import os
    import base64
    import mimetypes
    import urllib.request
    import dashscope
    from dashscope.aigc.image_generation import ImageGeneration
    from dashscope.api_entities.dashscope_response import Message

    # 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    # --- Base64编码函数 ---
    # base64编码格式为 data:{MIME_type};base64,{base64_data}
    def encode_file(file_path):
        mime_type, _ = mimetypes.guess_type(file_path)
        if not mime_type or not mime_type.startswith("image/"):
            raise ValueError("不支持或无法识别的图像格式")
        with open(file_path, "rb") as image_file:
            encoded_string = base64.b64encode(image_file.read()).decode("utf-8")
        return f"data:{mime_type};base64,{encoded_string}"

    """
    图像输入方式说明：
    以下提供了三种图片输入方式，三选一即可
    1. 使用公网URL - 适合已有公开可访问的图片
    2. 使用本地文件 - 适合本地开发测试
    3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
    """
    # 【方式一】使用公网图片 URL
    image_1 = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/pjeqdf/car.webp"
    image_2 = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/xsunlm/paint.webp"

    # 【方式二】使用本地文件（支持绝对路径和相对路径）
    # image_1 = "file:///path/to/your/car.png"
    # image_2 = "file:///path/to/your/paint.png"

    # 【方式三】使用Base64编码的图片
    # image_1 = encode_file("/path/to/your/car.png")
    # image_2 = encode_file("/path/to/your/paint.png")

    message = Message(
        role="user",
        content=[
            {"text": "把图2的涂鸦喷绘在图1的汽车上"},
            {"image": image_1},
            {"image": image_2},
        ],
    )
    print("----sync call, please wait a moment----")
    rsp = ImageGeneration.call(
        model="wan2.7-image-pro",
        api_key=api_key,
        messages=[message],
        watermark=False,
        n=1,
        size="2K",  # wan2.7-image-pro仅文生图场景支持4K分辨率，图像编辑和组图生成支持最高2K分辨率
    )

    # 提取结果图片URL并保存到本地
    if rsp.status_code == 200:
        for i, choice in enumerate(rsp.output.choices):
            for j, content in enumerate(choice["message"]["content"]):
                if content.get("type") == "image":
                    image_url = content["image"]
                    file_name = f"output_{i}_{j}.png"
                    # 结果URL有效期为24小时，请及时下载
                    urllib.request.urlretrieve(image_url, file_name)
                    print(f"Image saved to {file_name}")
    else:
        print(f"Failed: status_code={rsp.status_code}, message={rsp.message}")
    ```

    ##### 响应示例 <span id="b0515f126ag6l" />

    > URL 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "81d868c6-6ce1-92d8-a90d-d2ee71xxxxxx",
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
            "input_tokens": 18790,
            "output_tokens": 2,
            "characters": 0,
            "image_count": 1,
            "size": "2985*1405",
            "total_tokens": 18792
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="7bd0a67abeuuz" />

    ```python expandable
    import os
    import base64
    import mimetypes
    import urllib.request
    import dashscope
    from dashscope.aigc.image_generation import ImageGeneration
    from dashscope.api_entities.dashscope_response import Message
    from http import HTTPStatus

    # 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    # --- Base64编码函数 ---
    # base64编码格式为 data:{MIME_type};base64,{base64_data}
    def encode_file(file_path):
        mime_type, _ = mimetypes.guess_type(file_path)
        if not mime_type or not mime_type.startswith("image/"):
            raise ValueError("不支持或无法识别的图像格式")
        with open(file_path, "rb") as image_file:
            encoded_string = base64.b64encode(image_file.read()).decode("utf-8")
        return f"data:{mime_type};base64,{encoded_string}"

    """
    图像输入方式说明：
    以下提供了三种图片输入方式，三选一即可
    1. 使用公网URL - 适合已有公开可访问的图片
    2. 使用本地文件 - 适合本地开发测试
    3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
    """
    # 【方式一】使用公网图片 URL
    image_1 = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/pjeqdf/car.webp"
    image_2 = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/xsunlm/paint.webp"

    # 【方式二】使用本地文件（支持绝对路径和相对路径）
    # image_1 = "file:///path/to/your/car.png"
    # image_2 = "file:///path/to/your/paint.png"

    # 【方式三】使用Base64编码的图片
    # image_1 = encode_file("/path/to/your/car.png")
    # image_2 = encode_file("/path/to/your/paint.png")

    # 创建异步任务
    def create_async_task():
        print("Creating async task...")
        message = Message(
            role="user",
            content=[
                {"text": "把图2的涂鸦喷绘在图1的汽车上"},
                {"image": image_1},
                {"image": image_2},
            ],
        )
        response = ImageGeneration.async_call(
            model="wan2.7-image-pro",
            api_key=api_key,
            messages=[message],
            watermark=False,
            n=1,
            size="2K",  # wan2.7-image-pro仅文生图场景支持4K分辨率，图像编辑和组图生成支持最高2K分辨率
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
            # 提取结果图片URL并保存到本地
            for i, choice in enumerate(status.output.choices):
                for j, content in enumerate(choice["message"]["content"]):
                    if content.get("type") == "image":
                        image_url = content["image"]
                        file_name = f"output_{i}_{j}.png"
                        # 结果URL有效期为24小时，请及时下载
                        urllib.request.urlretrieve(image_url, file_name)
                        print(f"Image saved to {file_name}")
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

    > URL 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "3b99aae5-d26f-9059-8dd0-ee9ca4804xxx",
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
            "task_id": "127ec645-118f-4884-955d-0eba8dxxxxxx",
            "task_status": "SUCCEEDED",
            "submit_time": "2026-03-31 22:58:47.646",
            "scheduled_time": "2026-03-31 22:58:47.683",
            "end_time": "2026-03-31 22:58:59.642",
            "finished": true
        },
        "usage": {
            "input_tokens": 18711,
            "output_tokens": 2,
            "characters": 0,
            "size": "2985*1405",
            "total_tokens": 18713,
            "image_count": 1
        }
    }
    ```
  </Tab>
</Tabs>

### 组图生成 <span id="wan27-sequential-python-title" />

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="wan27-sequential-sync-request-title" />

    ```python expandable
    import os
    import base64
    import mimetypes
    import urllib.request
    import dashscope
    from dashscope.aigc.image_generation import ImageGeneration
    from dashscope.api_entities.dashscope_response import Message

    # 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    # --- Base64编码函数 ---
    # base64编码格式为 data:{MIME_type};base64,{base64_data}
    def encode_file(file_path):
        mime_type, _ = mimetypes.guess_type(file_path)
        if not mime_type or not mime_type.startswith("image/"):
            raise ValueError("不支持或无法识别的图像格式")
        with open(file_path, "rb") as image_file:
            encoded_string = base64.b64encode(image_file.read()).decode("utf-8")
        return f"data:{mime_type};base64,{encoded_string}"

    """
    图像输入方式说明（图生组图场景）：
    以下提供了三种图片输入方式，三选一即可
    1. 使用公网URL - 适合已有公开可访问的图片
    2. 使用本地文件 - 适合本地开发测试
    3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
    """
    # 【方式一】使用公网图片 URL
    # image_1 = "https://img.alicdn.com/imgextra/i4/O1CN01IM44WN23dq5uY1yla_!!6000000007279-49-tps-1024-1024.webp"

    # 【方式二】使用本地文件（支持绝对路径和相对路径）
    # image_1 = "file:///path/to/your/image.png"

    # 【方式三】使用Base64编码的图片
    # image_1 = encode_file("/path/to/your/image.png")

    message = Message(
        role="user",
        content=[
            {
                "text": "电影感组图，记录同一只流浪橘猫，特征必须前后一致。第一张：春天，橘猫穿梭在盛开的樱花树下；第二张：夏天，橘猫在老街的树荫下乘凉避暑；第三张：秋天，橘猫踩在满地的金色落叶上；第四张：冬天，橘猫在雪地上走留下足迹。"
            }
            # 图生组图场景：取消以下注释并注释掉上方纯文本
            # {"text": "参考图片风格生成四季组图"},
            # {"image": image_1}
        ],
    )

    print("----sync call, please wait a moment----")
    rsp = ImageGeneration.call(
        model="wan2.7-image-pro",
        api_key=api_key,
        messages=[message],
        enable_sequential=True,
        n=4,
        size="2K",  # wan2.7-image-pro仅文生图场景支持4K分辨率，图像编辑和组图生成支持最高2K分辨率
    )

    # 提取结果图片URL并保存到本地
    if rsp.status_code == 200:
        for i, choice in enumerate(rsp.output.choices):
            for j, content in enumerate(choice["message"]["content"]):
                if content.get("type") == "image":
                    image_url = content["image"]
                    file_name = f"output_{i}_{j}.png"
                    # 结果URL有效期为24小时，请及时下载
                    urllib.request.urlretrieve(image_url, file_name)
                    print(f"Image saved to {file_name}")
    else:
        print(f"Failed: status_code={rsp.status_code}, message={rsp.message}")
    ```

    ##### 响应示例 <span id="wan27-sequential-sync-response-title" />

    > URL 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "56e318fd-ed60-99e8-8ca1-cdef25ca4xxx",
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
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            },
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
            "input_tokens": 720,
            "output_tokens": 11,
            "characters": 0,
            "image_count": 4,
            "size": "2048*2048",
            "total_tokens": 731
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="wan27-sequential-async-request-title" />

    ```python expandable
    import os
    import base64
    import mimetypes
    import urllib.request
    import dashscope
    from dashscope.aigc.image_generation import ImageGeneration
    from dashscope.api_entities.dashscope_response import Message

    # 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    # --- Base64编码函数 ---
    # base64编码格式为 data:{MIME_type};base64,{base64_data}
    def encode_file(file_path):
        mime_type, _ = mimetypes.guess_type(file_path)
        if not mime_type or not mime_type.startswith("image/"):
            raise ValueError("不支持或无法识别的图像格式")
        with open(file_path, "rb") as image_file:
            encoded_string = base64.b64encode(image_file.read()).decode("utf-8")
        return f"data:{mime_type};base64,{encoded_string}"

    """
    图像输入方式说明（图生组图场景）：
    以下提供了三种图片输入方式，三选一即可
    1. 使用公网URL - 适合已有公开可访问的图片
    2. 使用本地文件 - 适合本地开发测试
    3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
    """
    # 【方式一】使用公网图片 URL
    # image_1 = "https://img.alicdn.com/imgextra/i4/O1CN01IM44WN23dq5uY1yla_!!6000000007279-49-tps-1024-1024.webp"

    # 【方式二】使用本地文件（支持绝对路径和相对路径）
    # image_1 = "file:///path/to/your/image.png"

    # 【方式三】使用Base64编码的图片
    # image_1 = encode_file("/path/to/your/image.png")

    def main():
        message = Message(
            role="user",
            content=[
                {
                    "text": "电影感组图，记录同一只流浪橘猫，特征必须前后一致。第一张：春天，橘猫穿梭在盛开的樱花树下；第二张：夏天，橘猫在老街的树荫下乘凉避暑；第三张：秋天，橘猫踩在满地的金色落叶上；第四张：冬天，橘猫在雪地上走留下足迹。"
                }
                # 图生组图场景：取消以下注释并注释掉上方纯文本
                # {"text": "参考图片风格生成四季组图"},
                # {"image": image_1}
            ],
        )

        # 提交异步任务
        print("提交异步任务...")
        response = ImageGeneration.async_call(
            model="wan2.7-image-pro",
            api_key=api_key,
            messages=[message],
            enable_sequential=True,
            n=4,
            size="2K",  # wan2.7-image-pro仅文生图场景支持4K分辨率，图像编辑和组图生成支持最高2K分辨率
        )

        if response.status_code == 200:
            print(f"任务提交成功，任务ID: {response.output.task_id}")

            # 等待任务完成
            status = ImageGeneration.wait(task=response, api_key=api_key)

            if status.output.task_status == "SUCCEEDED":
                print("任务完成!")
                # 提取结果图片URL并保存到本地
                for i, choice in enumerate(status.output.choices):
                    for j, content in enumerate(choice["message"]["content"]):
                        if content.get("type") == "image":
                            image_url = content["image"]
                            file_name = f"output_{i}_{j}.png"
                            # 结果URL有效期为24小时，请及时下载
                            urllib.request.urlretrieve(image_url, file_name)
                            print(f"Image saved to {file_name}")
            else:
                print(f"任务失败，状态: {status.output.task_status}")
        else:
            print(f"任务创建失败: {response.code} - {response.message}")

    if __name__ == "__main__":
        try:
            main()
        except Exception as e:
            print(f"错误: {e}")
    ```

    ##### 响应示例 <span id="wan27-sequential-async-response-title" />

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
            "task_id": "77093787-a217-4c29-9cd4-ca7b5ac86xxx",
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

    > URL 有效期24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "56e318fd-ed60-99e8-8ca1-cdef25ca4xxx",
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
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            }
                        ]
                    }
                }
            ],
            "audio": null,
            "task_id": "77093787-a217-4c29-9cd4-ca7b5ac86xxx",
            "task_status": "SUCCEEDED",
            "submit_time": "2026-03-31 23:04:46.166",
            "scheduled_time": "2026-03-31 23:04:46.208",
            "end_time": "2026-03-31 23:05:11.664",
            "finished": true
        },
        "usage": {
            "input_tokens": 720,
            "output_tokens": 11,
            "characters": 0,
            "size": "2048*2048",
            "total_tokens": 731,
            "image_count": 4
        }
    }
    ```
  </Tab>
</Tabs>

## Java SDK调用 <span id="d3a0dcecf1tyl" />

SDK 参数命名与HTTP接口基本一致。

任务可能耗时较长，SDK 已封装HTTP异步调用流程，同时支持同步和异步调用。

<Tip>
  请确保 DashScope Java SDK版本不低于 `2.22.13`，否则可能不支持本文所用的部分参数。
</Tip>

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

### 图像编辑 <span id="180cc839317zj" />

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="189e4d8658kmi" />

    ```java expandable
    import com.alibaba.dashscope.aigc.imagegeneration.*;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.io.IOException;
    import java.io.InputStream;
    import java.net.URL;
    import java.nio.file.Files;
    import java.nio.file.Paths;
    import java.nio.file.StandardCopyOption;
    import java.util.Arrays;
    import java.util.Base64;
    import java.util.Collections;
    import java.util.List;
    import java.util.Map;

    /**
     * wan2.7-image-pro 图像编辑 - 同步调用示例
     */
    public class Main {

        static {
            // 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        // --- Base64编码函数 ---
        // base64编码格式为 data:{MIME_type};base64,{base64_data}
        public static String encodeFile(String filePath) throws IOException {
            byte[] fileContent = Files.readAllBytes(Paths.get(filePath));
            String base64String = Base64.getEncoder().encodeToString(fileContent);
            String mimeType = Files.probeContentType(Paths.get(filePath));
            return "data:" + mimeType + ";base64," + base64String;
        }

        public static void basicCall() throws ApiException, NoApiKeyException, UploadFileException, IOException {
            /*
             * 图像输入方式说明：
             * 以下提供了三种图片输入方式，三选一即可
             * 1. 使用公网URL - 适合已有公开可访问的图片
             * 2. 使用本地文件 - 适合本地开发测试
             * 3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
             */
            // 【方式一】使用公网图片 URL
            String image1 = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/pjeqdf/car.webp";
            String image2 = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/xsunlm/paint.webp";

            // 【方式二】使用本地文件（支持绝对路径和相对路径）
            // 格式要求：file:// + 文件路径
            // String image1 = "file:///path/to/your/car.png";
            // String image2 = "file:///path/to/your/paint.png";

            // 【方式三】使用Base64编码的图片
            // String image1 = encodeFile("/path/to/your/car.png");
            // String image2 = encodeFile("/path/to/your/paint.png");

            // 构建多图输入消息
            ImageGenerationMessage message = ImageGenerationMessage.builder()
                    .role("user")
                    .content(Arrays.asList(
                            // 支持多图输入，可以提供多张参考图片
                            Collections.singletonMap("text", "把图2的涂鸦喷绘在图1的汽车上"),
                            Collections.singletonMap("image", image1),
                            Collections.singletonMap("image", image2)
                    )).build();

            ImageGenerationParam param = ImageGenerationParam.builder()
                    .apiKey(apiKey)
                    .model("wan2.7-image-pro")
                    .messages(Collections.singletonList(message))
                    .n(1)
                    .size("2K") // wan2.7-image-pro仅文生图场景支持4K分辨率，图像编辑和组图生成支持最高2K分辨率
                    .build();

            ImageGeneration imageGeneration = new ImageGeneration();
            ImageGenerationResult result = null;
            try {
                System.out.println("---sync call for image editing, please wait a moment----");
                result = imageGeneration.call(param);
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                throw new RuntimeException(e.getMessage());
            }
            // 提取结果图片URL并保存到本地
            for (int i = 0; i < result.getOutput().getChoices().size(); i++) {
                List<Map<String, Object>> contents = result.getOutput().getChoices().get(i)
                        .getMessage().getContent();
                for (int j = 0; j < contents.size(); j++) {
                    if ("image".equals(contents.get(j).get("type"))) {
                        String imageUrl = (String) contents.get(j).get("image");
                        String fileName = "output_" + i + "_" + j + ".png";
                        // 结果URL有效期为24小时，请及时下载
                        try (InputStream in = new URL(imageUrl).openStream()) {
                            Files.copy(in, Paths.get(fileName), StandardCopyOption.REPLACE_EXISTING);
                        }
                        System.out.println("Image saved to " + fileName);
                    }
                }
            }
        }

        public static void main(String[] args) throws ApiException, NoApiKeyException, UploadFileException, IOException {
            basicCall();
        }
    }
    ```

    ##### 响应示例 <span id="e4fbc7f75d4gg" />

    > URL 有效期24小时，请及时下载并保存图像。

    ```java expandable
    {
        "requestId": "1bf6173a-e8de-9f75-94d3-5e618f875xxx",
        "usage": {
            "input_tokens": 18790,
            "output_tokens": 2,
            "total_tokens": 18792,
            "image_count": 1,
            "size": "2985*1405"
        },
        "output": {
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
            "finished": true
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>

  <Tab title="异步调用">
    ##### 请求示例 <span id="5ce97169950io" />

    ```java expandable
    import com.alibaba.dashscope.aigc.imagegeneration.*;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.io.IOException;
    import java.io.InputStream;
    import java.net.URL;
    import java.nio.file.Files;
    import java.nio.file.Paths;
    import java.nio.file.StandardCopyOption;
    import java.util.Arrays;
    import java.util.Base64;
    import java.util.Collections;
    import java.util.List;
    import java.util.Map;

    /**
     * wan2.7-image-pro 图像编辑 - 异步调用示例
     */
    public class Main {

        static {
            // 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        // --- Base64编码函数 ---
        // base64编码格式为 data:{MIME_type};base64,{base64_data}
        public static String encodeFile(String filePath) throws IOException {
            byte[] fileContent = Files.readAllBytes(Paths.get(filePath));
            String base64String = Base64.getEncoder().encodeToString(fileContent);
            String mimeType = Files.probeContentType(Paths.get(filePath));
            return "data:" + mimeType + ";base64," + base64String;
        }

        public static void asyncCall() throws ApiException, NoApiKeyException, UploadFileException, IOException {
            /*
             * 图像输入方式说明：
             * 以下提供了三种图片输入方式，三选一即可
             * 1. 使用公网URL - 适合已有公开可访问的图片
             * 2. 使用本地文件 - 适合本地开发测试
             * 3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
             */
            // 【方式一】使用公网图片 URL
            String image1 = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/pjeqdf/car.webp";
            String image2 = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20251229/xsunlm/paint.webp";

            // 【方式二】使用本地文件（支持绝对路径和相对路径）
            // 格式要求：file:// + 文件路径
            // String image1 = "file:///path/to/your/car.png";
            // String image2 = "file:///path/to/your/paint.png";

            // 【方式三】使用Base64编码的图片
            // String image1 = encodeFile("/path/to/your/car.png");
            // String image2 = encodeFile("/path/to/your/paint.png");

            // 构建多图输入消息
            ImageGenerationMessage message = ImageGenerationMessage.builder()
                    .role("user")
                    .content(Arrays.asList(
                            // 支持多图输入，可以提供多张参考图片
                            Collections.singletonMap("text", "把图2的涂鸦喷绘在图1的汽车上"),
                            Collections.singletonMap("image", image1),
                            Collections.singletonMap("image", image2)
                    )).build();

            ImageGenerationParam param = ImageGenerationParam.builder()
                    .apiKey(apiKey)
                    .model("wan2.7-image-pro")
                    .n(1)
                    .size("2K") // wan2.7-image-pro仅文生图场景支持4K分辨率，图像编辑和组图生成支持最高2K分辨率
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

        public static void waitTask(String taskId) throws ApiException, NoApiKeyException, IOException {
            ImageGeneration imageGeneration = new ImageGeneration();
            System.out.println("\n---waiting for task completion----");
            ImageGenerationResult result = imageGeneration.wait(taskId, apiKey);
            // 提取结果图片URL并保存到本地
            for (int i = 0; i < result.getOutput().getChoices().size(); i++) {
                List<Map<String, Object>> contents = result.getOutput().getChoices().get(i)
                        .getMessage().getContent();
                for (int j = 0; j < contents.size(); j++) {
                    if ("image".equals(contents.get(j).get("type"))) {
                        String imageUrl = (String) contents.get(j).get("image");
                        String fileName = "output_" + i + "_" + j + ".png";
                        // 结果URL有效期为24小时，请及时下载
                        try (InputStream in = new URL(imageUrl).openStream()) {
                            Files.copy(in, Paths.get(fileName), StandardCopyOption.REPLACE_EXISTING);
                        }
                        System.out.println("Image saved to " + fileName);
                    }
                }
            }
        }

        public static void main(String[] args) throws ApiException, NoApiKeyException, UploadFileException, IOException {
            asyncCall();
        }
    }
    ```

    ##### 响应示例 <span id="02dea79668owz" />

    1、创建任务的响应示例

    ```json
    {
        "requestId": "ccf4b2f4-bf30-9e13-9461-3a28c6a7bxxx",
        "output": {
            "task_id": "8811b4a4-00ac-4aa2-a2fd-017d3b90cxxx",
            "task_status": "PENDING"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```

    2、查询任务结果的响应示例

    > URL 有效期24小时，请及时下载并保存图像。

    ```json expandable
    {
        "requestId": "60a08540-f1c1-9e76-8cd3-d5949db8cxxx",
        "usage": {
            "input_tokens": 18711,
            "output_tokens": 2,
            "total_tokens": 18713,
            "image_count": 1,
            "size": "2985*1405"
        },
        "output": {
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
            "task_id": "8811b4a4-00ac-4aa2-a2fd-017d3b90cxxx",
            "task_status": "SUCCEEDED",
            "finished": true,
            "submit_time": "2026-03-31 19:57:58.840",
            "scheduled_time": "2026-03-31 19:57:58.877",
            "end_time": "2026-03-31 19:58:11.563"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>
</Tabs>

### 组图生成 <span id="12158ab94dwat" />

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="fdcc0bc0b5xw2" />

    ```java expandable
    import com.alibaba.dashscope.aigc.imagegeneration.*;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.io.IOException;
    import java.io.InputStream;
    import java.net.URL;
    import java.nio.file.Files;
    import java.nio.file.Paths;
    import java.nio.file.StandardCopyOption;
    import java.util.Arrays;
    import java.util.Base64;
    import java.util.Collections;
    import java.util.List;
    import java.util.Map;

    /**
     * wan2.7-image-pro 组图生成 - 同步调用示例（北京地域）
     */
    public class Main {

        static {
            // 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        // --- Base64编码函数 ---
        // base64编码格式为 data:{MIME_type};base64,{base64_data}
        public static String encodeFile(String filePath) throws IOException {
            byte[] fileContent = Files.readAllBytes(Paths.get(filePath));
            String base64String = Base64.getEncoder().encodeToString(fileContent);
            String mimeType = Files.probeContentType(Paths.get(filePath));
            return "data:" + mimeType + ";base64," + base64String;
        }

        public static void basicCall() throws ApiException, NoApiKeyException, UploadFileException, IOException {
            /*
             * 图像输入方式说明（图生组图场景）：
             * 以下提供了三种图片输入方式，三选一即可
             * 1. 使用公网URL - 适合已有公开可访问的图片
             * 2. 使用本地文件 - 适合本地开发测试
             * 3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
             */
            // 【方式一】使用公网图片 URL
            // String image1 = "https://img.alicdn.com/imgextra/i4/O1CN01IM44WN23dq5uY1yla_!!6000000007279-49-tps-1024-1024.webp";

            // 【方式二】使用本地文件（支持绝对路径和相对路径）
            // 格式要求：file:// + 文件路径
            // String image1 = "file:///path/to/your/image.png";

            // 【方式三】使用Base64编码的图片
            // String image1 = encodeFile("/path/to/your/image.png");

            // 构建文本输入消息（支持文生组图以及图生组图，此处以文生组图为例）
            ImageGenerationMessage message = ImageGenerationMessage.builder()
                    .role("user")
                    .content(Collections.singletonList(
                            Collections.singletonMap("text", "电影感组图，记录同一只流浪橘猫，特征必须前后一致。第一张：春天，橘猫穿梭在盛开的樱花树下；第二张：夏天，橘猫在老街的树荫下乘凉避暑；第三张：秋天，橘猫踩在满地的金色落叶上；第四张：冬天，橘猫在雪地上走留下足迹。")
                    )).build();
            // 图生组图场景：取消以下注释并注释掉上方纯文本构建
            // ImageGenerationMessage message = ImageGenerationMessage.builder()
            //         .role("user")
            //         .content(Arrays.asList(
            //                 Collections.singletonMap("text", "参考图片风格生成四季组图"),
            //                 Collections.singletonMap("image", image1)
            //         )).build();

            ImageGenerationParam param = ImageGenerationParam.builder()
                    .apiKey(apiKey)
                    .model("wan2.7-image-pro")
                    .messages(Collections.singletonList(message))
                    .enableSequential(true)
                    .n(4)
                    .size("2K") // wan2.7-image-pro仅文生图场景支持4K分辨率，图像编辑和组图生成支持最高2K分辨率
                    .build();

            ImageGeneration imageGeneration = new ImageGeneration();
            ImageGenerationResult result = null;
            try {
                System.out.println("----sync call, please wait a moment----");
                result = imageGeneration.call(param);
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                throw new RuntimeException(e.getMessage());
            }
            // 提取结果图片URL并保存到本地
            for (int i = 0; i < result.getOutput().getChoices().size(); i++) {
                List<Map<String, Object>> contents = result.getOutput().getChoices().get(i)
                        .getMessage().getContent();
                for (int j = 0; j < contents.size(); j++) {
                    if ("image".equals(contents.get(j).get("type"))) {
                        String imageUrl = (String) contents.get(j).get("image");
                        String fileName = "output_" + i + "_" + j + ".png";
                        // 结果URL有效期为24小时，请及时下载
                        try (InputStream in = new URL(imageUrl).openStream()) {
                            Files.copy(in, Paths.get(fileName), StandardCopyOption.REPLACE_EXISTING);
                        }
                        System.out.println("Image saved to " + fileName);
                    }
                }
            }
        }

        public static void main(String[] args) throws ApiException, NoApiKeyException, UploadFileException, IOException {
            basicCall();
        }
    }
    ```

    ##### 响应示例 <span id="4dff52f5a4izo" />

    > URL 有效期24小时，请及时下载并保存图像。

    ```java expandable
    {
        "requestId": "4678c314-b37a-91c9-a2ae-2d3cd54bbxxx",
        "usage": {
            "input_tokens": 720,
            "output_tokens": 11,
            "total_tokens": 731,
            "image_count": 4,
            "size": "2048*2048"
        },
        "output": {
            "choices": [
                {
                    "finish_reason": "stop",
                    "message": {
                        "role": "assistant",
                        "content": [
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
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
    ##### 请求示例 <span id="fb681a63465zq" />

    ```java expandable
    import com.alibaba.dashscope.aigc.imagegeneration.*;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.io.IOException;
    import java.io.InputStream;
    import java.net.URL;
    import java.nio.file.Files;
    import java.nio.file.Paths;
    import java.nio.file.StandardCopyOption;
    import java.util.Arrays;
    import java.util.Base64;
    import java.util.Collections;
    import java.util.List;
    import java.util.Map;

    /**
     * wan2.7-image-pro 组图生成 - 异步调用示例（北京地域）
     */
    public class Main {

        static {
            // 以下为华北2（北京）地域的URL，调用时请将{WorkspaceId}替换为真实的业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 各地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        // --- Base64编码函数 ---
        // base64编码格式为 data:{MIME_type};base64,{base64_data}
        public static String encodeFile(String filePath) throws IOException {
            byte[] fileContent = Files.readAllBytes(Paths.get(filePath));
            String base64String = Base64.getEncoder().encodeToString(fileContent);
            String mimeType = Files.probeContentType(Paths.get(filePath));
            return "data:" + mimeType + ";base64," + base64String;
        }

        public static ImageGenerationResult waitTask(String taskId)
                throws ApiException, NoApiKeyException {
            ImageGeneration imageGeneration = new ImageGeneration();
            return imageGeneration.wait(taskId, apiKey);
        }

        public static void asyncCall() throws ApiException, NoApiKeyException, UploadFileException, IOException {
            /*
             * 图像输入方式说明（图生组图场景）：
             * 以下提供了三种图片输入方式，三选一即可
             * 1. 使用公网URL - 适合已有公开可访问的图片
             * 2. 使用本地文件 - 适合本地开发测试
             * 3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
             */
            // 【方式一】使用公网图片 URL
            // String image1 = "https://img.alicdn.com/imgextra/i4/O1CN01IM44WN23dq5uY1yla_!!6000000007279-49-tps-1024-1024.webp";

            // 【方式二】使用本地文件（支持绝对路径和相对路径）
            // 格式要求：file:// + 文件路径
            // String image1 = "file:///path/to/your/image.png";

            // 【方式三】使用Base64编码的图片
            // String image1 = encodeFile("/path/to/your/image.png");

            // 构建文本输入消息（支持文生组图以及图生组图，此处以文生组图为例）
            ImageGenerationMessage message = ImageGenerationMessage.builder()
                    .role("user")
                    .content(Collections.singletonList(
                            Collections.singletonMap("text", "电影感组图，记录同一只流浪橘猫，特征必须前后一致。第一张：春天，橘猫穿梭在盛开的樱花树下；第二张：夏天，橘猫在老街的树荫下乘凉避暑；第三张：秋天，橘猫踩在满地的金色落叶上；第四张：冬天，橘猫在雪地上走留下足迹。")
                    )).build();
            // 图生组图场景：取消以下注释并注释掉上方纯文本构建
            // ImageGenerationMessage message = ImageGenerationMessage.builder()
            //         .role("user")
            //         .content(Arrays.asList(
            //                 Collections.singletonMap("text", "参考图片风格生成四季组图"),
            //                 Collections.singletonMap("image", image1)
            //         )).build();

            ImageGenerationParam param = ImageGenerationParam.builder()
                    .apiKey(apiKey)
                    .model("wan2.7-image-pro")
                    .messages(Collections.singletonList(message))
                    .enableSequential(true)
                    .n(4)
                    .size("2K") // wan2.7-image-pro仅文生图场景支持4K分辨率，图像编辑和组图生成支持最高2K分辨率
                    .build();

            ImageGeneration imageGeneration = new ImageGeneration();
            ImageGenerationResult taskResult = null;
            try {
                System.out.println("----async call, creating task----");
                taskResult = imageGeneration.asyncCall(param);
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                throw new RuntimeException(e.getMessage());
            }
            System.out.println("Task created: " + JsonUtils.toJson(taskResult));

            // 等待任务完成
            String taskId = taskResult.getOutput().getTaskId();
            ImageGenerationResult result = waitTask(taskId);
            // 提取结果图片URL并保存到本地
            for (int i = 0; i < result.getOutput().getChoices().size(); i++) {
                List<Map<String, Object>> contents = result.getOutput().getChoices().get(i)
                        .getMessage().getContent();
                for (int j = 0; j < contents.size(); j++) {
                    if ("image".equals(contents.get(j).get("type"))) {
                        String imageUrl = (String) contents.get(j).get("image");
                        String fileName = "output_" + i + "_" + j + ".png";
                        // 结果URL有效期为24小时，请及时下载
                        try (InputStream in = new URL(imageUrl).openStream()) {
                            Files.copy(in, Paths.get(fileName), StandardCopyOption.REPLACE_EXISTING);
                        }
                        System.out.println("Image saved to " + fileName);
                    }
                }
            }
        }

        public static void main(String[] args) throws ApiException, NoApiKeyException, UploadFileException, IOException {
            asyncCall();
        }
    }
    ```

    ##### 响应示例 <span id="d207a5a32d9sj" />

    1、创建任务的响应示例

    ```json
    {
        "requestId": "7d026dc1-e8c9-9caa-84ac-e82e2da97xxx",
        "output": {
            "task_id": "2de18c56-c151-4b80-8105-1d164733exxx",
            "task_status": "PENDING"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```

    2、查询任务结果的响应示例

    > URL 有效期24小时，请及时下载并保存图像。

    ```json expandable
    {
        "requestId": "daea7295-4ce0-928a-9a11-4d2bea058xxx",
        "usage": {
            "input_tokens": 720,
            "output_tokens": 11,
            "total_tokens": 731,
            "image_count": 4,
            "size": "2048*2048"
        },
        "output": {
            "choices": [
                {
                    "finish_reason": "stop",
                    "message": {
                        "role": "assistant",
                        "content": [
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            },
                            {
                                "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxxxxx.png?Expires=xxxxxx",
                                "type": "image"
                            }
                        ]
                    }
                }
            ],
            "task_id": "2de18c56-c151-4b80-8105-1d164733exxx",
            "task_status": "SUCCEEDED",
            "finished": true,
            "submit_time": "2026-03-31 19:49:53.124",
            "scheduled_time": "2026-03-31 19:49:53.175",
            "end_time": "2026-03-31 19:50:53.160"
        },
        "status_code": 200,
        "code": "",
        "message": ""
    }
    ```
  </Tab>
</Tabs>

## 计费与限流 <span id="5dafefe82bgrh" />

- 模型免费额度和计费单价请参见[模型价格](/zh/model-studio/model-pricing#e2540d71a2utl)。
- 模型限流请参见[万相](/zh/model-studio/rate-limit#513e0a3df24v7)。
- 计费说明：按成功生成的 <strong>图像张数</strong> 计费。模型调用失败或处理错误不产生任何费用，也不消耗[新人免费额度](/zh/model-studio/new-free-quota)。

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

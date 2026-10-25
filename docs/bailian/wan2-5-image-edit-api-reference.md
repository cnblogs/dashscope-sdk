> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-通用图像编辑2.5

> 万相-通用图像编辑wan2.5模型仅需文本指令，即可基于单张或多张参考图像，实现主体一致的图像编辑、多图融合等能力。

<meta property="og:title" content="万相-通用图像编辑2.5" />

<strong>快速入口</strong>：[使用指南](/zh/model-studio/wan-image-edit)

## 模型概览 <span id="4a3e46b825d2k" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "13.82%" }} /><col style={{ width: "41.54%" }} /><col style={{ width: "44.64%" }} /></colgroup><thead><tr><th><p><strong>模型功能</strong></p></th><th><p><strong>输入示例</strong></p></th><th><p><strong>输出图像</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>单图编辑</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3943878571/p1011665.webp" alt="damotest2023_Portrait_photography_outdoors_fashionable_beauty_409ae3c1-19e8-4515-8e50-b3c9072e1282_2-转换自-png" width={360} /></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3943878571/p1011666.webp" alt="a26b226d-f044-4e95-a41c-d1c0d301c30b-转换自-png" width={360} /></p> <p>将花卉连衣裙换成一件复古风格的蕾丝长裙，领口和袖口有精致的刺绣细节。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>多图融合</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8142593671/p1020961.png" alt="image" width={517.706} /></p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7142593671/p1028892.webp" alt="p1028883" width={360} /></p> <p>将图1中的闹钟放置到图2的餐桌的花瓶旁边位置</p></td></tr></tbody></table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "18.08%" }} /><col style={{ width: "25.14%" }} /><col style={{ width: "56.78%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型简介</strong></p></th><th><p><strong>输出图像规格</strong></p></th></tr></thead><tbody><tr><td><p>wan2.5-i2i-preview</p></td><td><p>万相2.5 preview</p> <p>支持单图编辑、多图融合</p></td><td><p>图片格式：PNG。</p> <p>图像分辨率：</p> <ul><li><p>通过 <code>parameters.size</code> 参数指定输出图像的分辨率，格式为<code>宽\*高</code>（单位：像素）。</p></li> <li><p>若未指定分辨率，系统将默认生成总像素为 <code>1280\*1280</code> 的图像，并按以下规则保持宽高比（近似值）：</p> <ul><li><p>单图输入：宽高比与输入图像一致；</p></li> <li><p>多图输入：宽高比与最后一张输入图像一致。</p></li></ul></li></ul></td></tr></tbody></table>

<Note>
  调用前，请查阅各地域支持的[模型列表与价格](/zh/model-studio/models)。
</Note>

## 前提条件 <span id="0e41425c577u4" />

在调用前，先[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。如需通过SDK进行调用，请[安装DashScope SDK](/zh/model-studio/install-sdk)。

<Tip>
  华北2（北京）和新加坡地域拥有独立的 <strong>API Key</strong>与<strong>请求地址</strong>，不可混用，跨地域调用将导致鉴权失败或服务报错。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## HTTP调用 <span id="42703589880ts" />

由于图像编辑任务耗时较长（通常为1-2分钟），API采用异步调用。整个流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### 步骤1：创建任务获取任务ID <span id="db8cf70432oe3" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis`

<strong>新加坡地域</strong>：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

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

        模型名称。详情参见[模型列表与价格](/zh/model-studio/models)。

        示例值：wan2.5-i2i-preview。

        <strong>input</strong> `object` （必选）

        输入的基本信息，如提示词等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          正向提示词，用来描述生成图像中期望包含的元素和视觉特点。

          支持中英文，长度不超过2000个字符，每个汉字/字母占一个字符，超过部分会自动截断。

          提示词的使用技巧请参见[文生图Prompt指南](/zh/model-studio/text-to-image-prompt)。

          示例值：一只坐着的橘黄色的猫，表情愉悦，活泼可爱，逼真准确。

          <strong>images</strong> `array of string` <strong>（必选）</strong>

          图像URL数组。

          - 数组长度不超过3，即最多输入3张图片。
          - 多图输入时，按照数组顺序定义图像顺序。

          图像限制：

          - 图像格式：JPEG、JPG、PNG（不支持透明通道）、BMP、WEBP。
          - 图像分辨率：图像的宽高范围均为\[384, 5000]像素。
          - 文件大小：不超过10MB。

          支持的输入格式：

          1. 使用公网可访问URL

             - 支持 HTTP 或 HTTPS 协议。
             - 示例值：`http://wanx.alicdn.com/material/20250318/stylization_all_1.jpeg`。
          2. 传入 Base64 编码图像后的字符串

             - 格式：data:\{MIME\_type};base64,\{base64\_data}
             - 示例：data:image/jpeg;base64,GDU7MtCZzEbTbmRZ...（仅示意，实际需传入完整字符串）
             - Base64 编码规范请参见[图像传入方式](/zh/model-studio/wan-image-edit#8db0e2215frua)。

          <strong>negative\_prompt</strong> `string` （可选）

          反向提示词，用来描述不希望在画面中看到的内容，可以对画面进行限制。

          支持中英文，长度不超过500个字符，超过部分会自动截断。

          示例值：低分辨率、错误、最差质量、低质量、残缺、多余的手指、比例不良等。
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        图像处理参数。如设置图像分辨率、开启prompt智能改写、添加水印等。

        <Accordion title="属性" defaultOpen>
          <strong>size</strong> `string` （可选）

          设置输出图像的分辨率，格式为`宽*高`。默认值为 `1280*1280`。

          - 图像分辨率：总像素在 \[768*768, 1280*1280] 之间，且宽高比范围为 \[1:4, 4:1]。
          - 示例值：1280\*1280。

          <Accordion title="推荐分辨率及对应宽高比" defaultOpen>
            * 1280\*1280：1:1
            * 1024\*1024：1:1
            * 800\*1200：2:3
            * 1200\*800：3:2
            * 960\*1280：3:4
            * 1280\*960：4:3
            * 720\*1280：9:16
            * 1280\*720：16:9
            * 1344\*576：21:9
          </Accordion>

          若未指定`size`，系统将默认生成总像素为 `1280*1280` 的图像，并按以下规则保持宽高比（近似值）：

          - 单图输入：宽高比与输入图像一致；
          - 多图输入：宽高比与最后一张输入图像一致。

          <strong>n</strong> `integer` （可选）

          <Tip>
            n直接影响费用。n越大费用越高，请在调用前确认[模型价格](/zh/model-studio/models)。
          </Tip>

          生成图片的数量。取值范围为1\~4张，默认为`4`。为控制成本，测试建议显式设置为1。

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于图片右下角，文案固定为“AI生成”。

          - false：默认值，不添加水印。
          - true：添加水印。

          <strong>prompt\_extend</strong>`boolean` （可选）

          是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写，提升综合表现，但会增加耗时。

          - true：默认值，开启智能改写。
          - false：不开启智能改写。

          示例值：true。

          <strong>seed</strong> `integer` （可选）

          随机数种子，取值范围是`[0, 2147483647]`。

          如果不提供，则算法自动生成一个随机数作为种子。如果提供，则根据`n`的值分别为n张图片生成seed参数，例如n=4，算法将分别生成seed、seed+1、seed+2、seed+3作为参数的图片。

          若需提升生成结果的可复现性，建议固定seed值。

          请注意，由于模型生成具有概率性，即使使用相同 seed，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```bash 单图编辑
           curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
              -H 'X-DashScope-Async: enable' \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H 'Content-Type: application/json' \
              -d '{
              "model": "wan2.5-i2i-preview",
              "input": {
                  "prompt": "将花卉连衣裙换成一件复古风格的蕾丝长裙，领口和袖口有精致的刺绣细节。",
                  "images": [
                      "https://img.alicdn.com/imgextra/i2/O1CN01vHOj4h28jOxUJPwY8_!!6000000007968-49-tps-1344-896.webp"
                  ]
              },
              "parameters": {
                  "prompt_extend": true,
                  "n": 1
              }
          }'
          ```

          ```bash 多图融合
           curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2image/image-synthesis' \
              -H 'X-DashScope-Async: enable' \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H 'Content-Type: application/json' \
              -d '{
              "model": "wan2.5-i2i-preview",
              "input": {
                  "prompt": "将图1中的闹钟放置到图2的餐桌的花瓶旁边位置",
                  "images": [
                      "https://img.alicdn.com/imgextra/i3/O1CN0157XGE51l6iL9441yX_!!6000000004770-49-tps-1104-1472.webp",
                      "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp"
                  ]
              },
              "parameters": {
                  "n": 1
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

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>results</strong> `array of object`

          任务结果列表，包括图像URL、prompt、部分任务执行失败报错信息等。

          <Accordion title="属性" defaultOpen>
            <strong>orig\_prompt</strong> `string`

            原始输入的prompt，对应请求参数`prompt`。

            <strong>actual\_prompt</strong> `string`

            开启 prompt 智能改写后，返回实际使用的优化后 prompt。若未开启该功能，则不返回此字段。

            <strong>url</strong> `string`

            模型生成图片的URL地址。

            <strong>code</strong> `string`

            图像错误码。部分任务执行失败时会返回该字段。

            <strong>message</strong> `string`

            图像错误信息。部分任务执行失败时会返回该字段。
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
                "request_id": "d1f2a1be-9c58-48af-b43f-xxxxxx",
                "output": {
                    "task_id": "7f4836cd-1c47-41b3-b3a4-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-09-23 22:14:10.800",
                    "scheduled_time": "2025-09-23 22:14:10.825",
                    "end_time": "2025-09-23 22:15:23.456",
                    "results": [
                        {
                            "orig_prompt": "将花卉连衣裙换成一件复古风格的蕾丝长裙，领口和袖口有精致的刺绣细节。",
                            "actual_prompt": "将粉色褶皱连衣裙替换为一件复古风格的蕾丝长裙，领口和袖口带有精致的刺绣细节，保持人物发型、妆容和姿态不变，整体风格与原图的柔和色调和古典氛围一致。",
                            "url": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx"
                        }
                    ],
                    "task_metrics": {
                        "TOTAL": 1,
                        "FAILED": 0,
                        "SUCCEEDED": 1
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

## DashScope SDK调用 <span id="db17b3098c2iv" />

SDK 的参数命名与[HTTP调用](/zh/model-studio/wan2-5-image-edit-api-reference#42703589880ts)基本一致，参数结构根据语言特性进行封装。

由于图像编辑任务耗时约30秒～60秒，SDK 在底层封装了 HTTP 异步调用流程，支持同步、异步两种调用方式。

> 具体耗时受限于排队任务数和服务执行情况，请在获取结果时耐心等待。

### Python SDK调用 <span id="a3ad9a3b6d9if" />

<Tip>
  <strong>请确保 DashScope Python SDK 版本不低于</strong>`1.25.2`<strong>。</strong>

  若版本过低，可能会触发 “url error, please check url!” 等错误。请参考[安装或升级SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="de8be082068hw" />

    本示例支持三种图像输入方式：公网URL、Base64编码、本地文件路径。

    ```python expandable
    import base64
    import mimetypes
    from http import HTTPStatus
    from urllib.parse import urlparse, unquote
    from pathlib import PurePosixPath
    import dashscope
    import requests
    from dashscope import ImageSynthesis
    import os

    # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    # --- 输入图片：使用 Base64 编码 ---
    # base64编码格式为 data:{MIME_type};base64,{base64_data}
    def encode_file(file_path):
        mime_type, _ = mimetypes.guess_type(file_path)
        if not mime_type or not mime_type.startswith("image/"):
            raise ValueError("不支持或无法识别的图像格式")
        with open(file_path, "rb") as image_file:
            encoded_string = base64.b64encode(image_file.read()).decode('utf-8')
        return f"data:{mime_type};base64,{encoded_string}"

    """
    图像输入方式说明：
    以下提供了三种图片输入方式，三选一即可

    1. 使用公网URL - 适合已有公开可访问的图片
    2. 使用本地文件 - 适合本地开发测试
    3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
    """

    # 【方式一】使用公网图片 URL
    image_url_1 = "https://img.alicdn.com/imgextra/i3/O1CN0157XGE51l6iL9441yX_!!6000000004770-49-tps-1104-1472.webp"
    image_url_2 = "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp"

    # 【方式二】使用本地文件（支持绝对路径和相对路径）
    # 格式要求：file:// + 文件路径
    # 示例（绝对路径）：
    # image_url_1 = "file://" + "/path/to/your/image_1.png"     # Linux/macOS
    # image_url_2 = "file://" + "C:/path/to/your/image_2.png"  # Windows
    # 示例（相对路径）：
    # image_url_1 = "file://" + "./image_1.png"                 # 以实际路径为准
    # image_url_2 = "file://" + "./image_1.png"                # 以实际路径为准

    # 【方式三】使用Base64编码的图片
    # image_url_1 = encode_file("./image_1.png")               # 以实际路径为准
    # image_url_2 = encode_file("./image_2.png")              # 以实际路径为准

    print('----sync call, please wait a moment----')
    rsp = ImageSynthesis.call(api_key=api_key,
                              model="wan2.5-i2i-preview",
                              prompt="将图1中的闹钟放置到图2的餐桌的花瓶旁边位置",
                              images=[image_url_1, image_url_2],
                              negative_prompt="",
                              n=1,
                              # size="1280*1280",
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
        "request_id": "8ad45834-4321-44ed-adf5-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "3aff9ebd-35fc-4339-98a3-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [
                {
                    "url": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx",
                    "orig_prompt": "将图1中的闹钟放置到图2的餐桌的花瓶旁边位置",
                    "actual_prompt": "将图1中的蓝色闹钟放置在图2餐桌的花瓶右侧，靠近桌布边缘的位置，保持闹钟朝向镜头，与桌面平行，阴影自然投射于桌面。"
                }
            ],
            "submit_time": "2025-10-23 16:18:16.009",
            "scheduled_time": "2025-10-23 16:18:16.040",
            "end_time": "2025-10-23 16:19:09.591",
            "task_metrics": {
                "TOTAL": 1,
                "FAILED": 0,
                "SUCCEEDED": 1
            }
        },
        "usage": {
            "image_count": 1
        }
    }
    ```
  </Tab>

  <Tab title="异步调用">
    本示例使用公网URL方式传入图片。

    ##### 请求示例 <span id="b9b6d3bf42rc7" />

    ```python expandable
    import os
    from http import HTTPStatus
    from urllib.parse import urlparse, unquote
    from pathlib import PurePosixPath
    import dashscope
    import requests
    from dashscope import ImageSynthesis

    # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    # 使用公网图片 URL
    image_url_1 = "https://img.alicdn.com/imgextra/i3/O1CN0157XGE51l6iL9441yX_!!6000000004770-49-tps-1104-1472.webp"
    image_url_2 = "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp"

    def async_call():
        print('----create task----')
        task_info = create_async_task()
        print('----wait task----')
        wait_async_task(task_info)

    # 创建异步任务
    def create_async_task():
        rsp = ImageSynthesis.async_call(api_key=api_key,
                                        model="wan2.5-i2i-preview",
                                        prompt="将图1中的闹钟放置到图2的餐桌的花瓶旁边位置",
                                        images=[image_url_1, image_url_2],
                                        negative_prompt="",
                                        n=1,
                                        # size="1280*1280",
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

    ```
    {
        "status_code": 200,
        "request_id": "8ad45834-4321-44ed-adf5-xxxxxx",
        "code": null,
        "message": "",
        "output": {
            "task_id": "3aff9ebd-35fc-4339-98a3-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [
                {
                    "url": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx",
                    "orig_prompt": "将图1中的闹钟放置到图2的餐桌的花瓶旁边位置",
                    "actual_prompt": "将图1中的蓝色闹钟放置在图2餐桌的花瓶右侧，靠近桌布边缘的位置，保持闹钟朝向镜头，与桌面平行，阴影自然投射于桌面。"
                }
            ],
            "submit_time": "2025-10-23 16:18:16.009",
            "scheduled_time": "2025-10-23 16:18:16.040",
            "end_time": "2025-10-23 16:19:09.591",
            "task_metrics": {
                "TOTAL": 1,
                "FAILED": 0,
                "SUCCEEDED": 1
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

<Tip>
  <strong>请确保 DashScope Java SDK 版本不低于</strong>`2.22.2`<strong>。</strong>

  若版本过低，可能会触发 “url error, please check url!” 等错误。请参考[安装或升级SDK](/zh/model-studio/install-sdk)进行更新。
</Tip>

<Tabs>
  <Tab title="同步调用">
    ##### 请求示例 <span id="5ac3ea9f52tii" />

    本示例支持三种图像输入方式：公网URL、Base64编码、本地文件路径。

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;
    import java.io.IOException;
    import java.nio.file.Files;
    import java.nio.file.Path;
    import java.nio.file.Paths;
    import java.util.*;

    public class Image2Image {

        static {
            // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        /**
         * 图像输入方式说明：三选一即可
         *
         * 1. 使用公网URL - 适合已有公开可访问的图片
         * 2. 使用本地文件 - 适合本地开发测试
         * 3. 使用Base64编码 - 适合私有图片或需要加密传输的场景
         */

        //【方式一】公网URL
        static String imageUrl_1 = "https://img.alicdn.com/imgextra/i3/O1CN0157XGE51l6iL9441yX_!!6000000004770-49-tps-1104-1472.webp";
        static String imageUrl_2 = "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp";

        //【方式二】本地文件路径（file://+绝对路径 or file:///+绝对路径）
        // static String imageUrl_1 = "file://" + "/your/path/to/image_1.png";    // Linux/macOS
        // static String imageUrl_2 = "file:///" + "C:/your/path/to/image_2.png";  // Windows

        //【方式三】Base64编码
        // static String imageUrl_1 = encodeFile("/your/path/to/image_1.png");
        // static String imageUrl_2 = encodeFile("/your/path/to/image_2.png");

        // 设置待编辑的图片列表
        static List<String> imageUrls = new ArrayList<>();
        static {
            imageUrls.add(imageUrl_1);
            imageUrls.add(imageUrl_2);
        }

        public static void syncCall() {
            // 设置parameters参数
            Map<String, Object> parameters = new HashMap<>();
            parameters.put("prompt_extend", true);
            parameters.put("watermark", false);
            parameters.put("seed", 12345);

            ImageSynthesisParam param =
                    ImageSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.5-i2i-preview")
                            .prompt("将图1中的闹钟放置到图2的餐桌的花瓶旁边位置")
                            .images(imageUrls)
                            .n(1)
                             //.size("1280*1280")
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

        /**
         * 将文件编码为Base64字符串
         * @param filePath 文件路径
         * @return Base64字符串，格式为 data:{MIME_type};base64,{base64_data}
         */
        public static String encodeFile(String filePath) {
            Path path = Paths.get(filePath);
            if (!Files.exists(path)) {
                throw new IllegalArgumentException("文件不存在: " + filePath);
            }
            // 检测MIME类型
            String mimeType = null;
            try {
                mimeType = Files.probeContentType(path);
            } catch (IOException e) {
                throw new IllegalArgumentException("无法检测文件类型: " + filePath);
            }
            if (mimeType == null || !mimeType.startsWith("image/")) {
                throw new IllegalArgumentException("不支持或无法识别的图像格式");
            }
            // 读取文件内容并编码
            byte[] fileBytes = null;
            try{
                fileBytes = Files.readAllBytes(path);
            } catch (IOException e) {
                throw new IllegalArgumentException("无法读取文件内容: " + filePath);
            }

            String encodedString = Base64.getEncoder().encodeToString(fileBytes);
            return "data:" + mimeType + ";base64," + encodedString;
        }

        public static void main(String[] args) {
            syncCall();
        }
    }
    ```

    ##### 响应示例 <span id="ecf553d14e183" />

    > url 有效期24小时，请及时下载图像。

    ```json
    {
        "request_id": "d362685b-757f-4eac-bab5-xxxxxx",
        "output": {
            "task_id": "bfa7fc39-3d87-4fa7-b1e6-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [
                {
                    "orig_prompt": "将图1中的闹钟放置到图2的餐桌的花瓶旁边位置",
                    "actual_prompt": "将图1中的蓝色闹钟放置在图2餐桌的花瓶右侧，靠近桌布边缘的位置，保持闹钟正面朝向镜头，与花瓶平行摆放。",
                    "url": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx"
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

  <Tab title="异步调用">
    本示例使用公网URL方式传入图片。

    ##### 请求示例 <span id="f7b9a75fbbo26" />

    ```java expandable
    // Copyright (c) Alibaba, Inc. and its affiliates.

    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesis;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisListResult;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisParam;
    import com.alibaba.dashscope.aigc.imagesynthesis.ImageSynthesisResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.task.AsyncTaskListParam;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.util.ArrayList;
    import java.util.HashMap;
    import java.util.List;
    import java.util.Map;

    public class Image2Image {
        static {
            // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey="sk-xxx"
        // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        //公网URL
        static String imageUrl_1 = "https://img.alicdn.com/imgextra/i3/O1CN0157XGE51l6iL9441yX_!!6000000004770-49-tps-1104-1472.webp";
        static String imageUrl_2 = "https://img.alicdn.com/imgextra/i3/O1CN01SfG4J41UYn9WNt4X1_!!6000000002530-49-tps-1696-960.webp";

        // 设置待编辑的图片列表
        static List<String> imageUrls = new ArrayList<>();
        static {
            imageUrls.add(imageUrl_1);
            imageUrls.add(imageUrl_2);
        }

        public static void asyncCall() {
            // 设置parameters参数
            Map<String, Object> parameters = new HashMap<>();
            parameters.put("prompt_extend", true);
            parameters.put("watermark", false);
            parameters.put("seed", 12345);

            ImageSynthesisParam param =
                    ImageSynthesisParam.builder()
                            .apiKey(apiKey)
                            .model("wan2.5-i2i-preview")
                            .prompt("将图1中的闹钟放置到图2的餐桌的花瓶旁边位置")
                            .images(imageUrls)
                            .n(1)
                            //.size("1280*1280")
                            .negativePrompt("")
                            .parameters(parameters)
                            .build();
            ImageSynthesis imageSynthesis = new ImageSynthesis();
            ImageSynthesisResult result = null;
            try {
                System.out.println("---async call, please wait a moment----");
                result = imageSynthesis.asyncCall(param);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            }

            System.out.println(JsonUtils.toJson(result));

            String taskId = result.getOutput().getTaskId();

            System.out.println("taskId=" + taskId);

            try {
                result = imageSynthesis.wait(taskId, apiKey);
            } catch (ApiException | NoApiKeyException e){
                throw new RuntimeException(e.getMessage());
            }
            System.out.println(JsonUtils.toJson(result));
            System.out.println(JsonUtils.toJson(result.getOutput()));
        }

        public static void listTask() throws ApiException, NoApiKeyException {
            ImageSynthesis is = new ImageSynthesis();
            AsyncTaskListParam param = AsyncTaskListParam.builder().build();
            param.setApiKey(apiKey);
            ImageSynthesisListResult result = is.list(param);
            System.out.println(result);
        }

        public void fetchTask(String taskId) throws ApiException, NoApiKeyException {
            ImageSynthesis is = new ImageSynthesis();
            // 如果已设置 DASHSCOPE_API_KEY 为环境变量，apiKey 可为空。
            ImageSynthesisResult result = is.fetch(taskId, apiKey);
            System.out.println(result.getOutput());
            System.out.println(result.getUsage());
        }

        public static void main(String[] args) {
            asyncCall();
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

    > url 有效期24小时，请及时下载图片。

    ```
    {
        "request_id": "d362685b-757f-4eac-bab5-xxxxxx",
        "output": {
            "task_id": "bfa7fc39-3d87-4fa7-b1e6-xxxxxx",
            "task_status": "SUCCEEDED",
            "results": [
                {
                    "orig_prompt": "将图1中的闹钟放置到图2的餐桌的花瓶旁边位置",
                    "actual_prompt": "将图1中的蓝色闹钟放置在图2餐桌的花瓶右侧，靠近桌布边缘的位置，保持闹钟正面朝向镜头，与花瓶平行摆放。",
                    "url": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx"
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
</Tabs>

## 使用限制 <span id="63a009e43a1iy" />

- <strong>数据时效</strong>：任务task\_id和 图像url均只保留 24 小时，过期后将无法查询或下载。
- <strong>内容审核</strong>：输入prompt、图像和输出图像均会经过内容安全审核，包含违规内容的请求将报错“IPInfringementSuspect”或“DataInspectionFailed”，具体参见[错误码](/zh/model-studio/error-code)。

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="c928ee689aw6l" />

##### Q：之前使用通用图像编辑 2.1，现在切换到 wan2.5 模型，SDK调用方式需要调整吗？ <span id="4fbf0ee72aahq" />

A：需要调整。两个版本的参数设计不同：

- [通用图像编辑2.1](/zh/model-studio/wanx-image-edit-api-reference)：需同时传入`prompt`和`function`参数。
- [通用图像编辑2.5](/zh/model-studio/wan2-5-image-edit-api-reference)：<strong>仅需</strong>传入`prompt`，所有编辑操作通过文本指令描述，<strong>不再支持也不需要</strong>`function`参数。

##### Q: 如何查看模型调用量？ <span id="9d8d426f60za1" />

A: 模型调用完成后数分钟内，请在[模型监控](https://bailian.console.aliyun.com/cn-beijing/model/telemetry)页面，查看模型的调用次数、成功率等指标。操作指引：[如何查看模型调用记录？](/zh/model-studio/new-free-quota#ab6ba5c538rn3)

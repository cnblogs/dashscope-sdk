> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# OpenAI兼容-Chat

> 通过兼容 OpenAI 格式的 Chat API 调用模型，查看输入输出参数说明及调用示例。

<meta property="og:title" content="OpenAI兼容-Chat" />

<Tabs>
  <Tab title="华北2（北京）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>

  <Tab title="新加坡">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>

  <Tab title="美国（弗吉尼亚）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.us-east-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>

  <Tab title="德国（法兰克福）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.eu-central-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>

  <Tab title="日本（东京）">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.ap-northeast-1.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>

  <Tab title="中国香港">
    SDK 调用配置的`base_url`：`https://{WorkspaceId}.cn-hongkong.maas.aliyuncs.com/compatible-mode/v1`

    HTTP 请求地址：`POST https://{WorkspaceId}.cn-hongkong.maas.aliyuncs.com/compatible-mode/v1/chat/completions`
  </Tab>
</Tabs>

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

您需要先[获取与配置 API Key](/zh/model-studio/get-api-key)。若通过OpenAI SDK进行调用，需要[安装SDK](/zh/model-studio/install-sdk)。

<Tip>
  阿里云百炼为华北2（北京）、新加坡、中国香港地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## 请求体 <span id="afcd41d2b57zf" />

        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称。

        支持的模型：Qwen 大语言模型（商业版、开源版）、Qwen-VL、Qwen-Coder、Qwen-Omni、Qwen-Math、DeepSeek（阿里云直供、硅基流动直供、快手万擎直供）、Kimi（阿里云直供、月之暗面直供）、GLM（阿里云直供）、MiniMax（阿里云直供、稀宇科技直供）。

        > 三方直供模型仅在中国站的华北2（北京）地域可用，调用前需先在百炼控制台开通对应服务（以 SiliconFlow DeepSeek 为例：搜索 deepseek → 找到 SiliconFlow DeepSeek 模型卡片 → 单击立即开通 → 确认授权）。

        > Qwen-Audio不支持OpenAI兼容协议，仅支持DashScope协议。

        <strong>具体模型名称和计费，请参见</strong>[百炼控制台](https://bailian.console.aliyun.com/model/market)。

        <strong>messages</strong>`array`<strong>（必选）</strong>

        传递给大模型的上下文，按对话顺序排列。

        <Accordion title="消息类型" defaultOpen>
          System Message`object`（可选）

          系统消息，用于设定大模型的角色、语气、任务目标或约束条件等。一般放在`messages`数组的第一位。

          > QwQ 模型不建议设置 System Message，QVQ 模型设置 System Message不会生效。

          <Accordion title="属性">
            <strong>content</strong>`string`<strong>（必选）</strong>

            系统指令，用于明确模型的角色、行为规范、回答风格和任务约束等。

            <strong>role</strong>`string`<strong>（必选）</strong>

            系统消息的角色，固定为 `system` 。
          </Accordion>

          User Message`object`<strong>（必选）</strong>

          用户消息，用于向模型传递问题、指令或上下文等。

          <Accordion title="属性">
            <strong>content</strong>`string 或 array`<strong>（必选）</strong>

            消息内容。若输入只有文本，则为 string 类型；若输入包含图像等多模态数据，或启用显式缓存，则为 array 类型。

            <Accordion title="使用多模态模型或启用显式缓存时的属性">
              <strong>type</strong>`string`<strong>（必选）</strong>

              可选值：

              - `text`

                输入文本时需设为`text`。
              - `image_url`

                输入图片时需设为`image_url`。
              - `input_audio`

                输入音频时需设为`input_audio`。
              - `video`

                输入图片列表形式的视频时需设为`video`。
              - `video_url`

                输入视频文件时需设为`video_url`。

                > Qwen-VL仅部分模型可输入视频文件，详情参见[视频理解（Qwen-VL）](/zh/model-studio/vision#80dbf6ca8fh6s)；QVQ与Qwen-Omni 模型支持直接传入视频文件。

              <strong>text</strong>`string`

              输入的文本。当`type`为`text`时，是必选参数。

              <strong>image\_url</strong>`object`

              输入的图片信息。当`type`为`image_url`时是必选参数。

              <Accordion title="属性">
                <strong>url</strong> `string`<strong>（必选）</strong>

                图片的 URL或 Base64 Data URL。传入本地文件请参考[图像与视频理解](/zh/model-studio/vision)。
              </Accordion>

              <strong>input\_audio</strong>`object`

              输入的音频信息。当`type`为`input_audio`时是必选参数。

              <Accordion title="属性">
                <strong>data</strong> `string`<strong>（必选）</strong>

                音频的 URL 或Base64 Data URL。传入本地文件请参见：[输入 Base64 编码的本地文件](/zh/model-studio/qwen-omni#c516d1e824x03)。

                <strong>format</strong>`string`<strong>（必选）</strong>

                输入音频的格式，如`mp3`、`wav`等。
              </Accordion>

              <strong>video</strong>`array`

              输入的<strong>图片列表形式的视频信息</strong>。当`type`为`video`时是必选参数。使用方法请参见：[视频理解（Qwen-VL、QVQ）](/zh/model-studio/vision#80dbf6ca8fh6s)或[视频理解（Qwen-Omni）](/zh/model-studio/qwen-omni)。

              示例值：

              ```json
              [
                  "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/xzsgiz/football1.jpg",
                  "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/tdescd/football2.jpg",
                  "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/zefdja/football3.jpg",
                  "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20241108/aedbqh/football4.jpg"
              ]
              ```

              <strong>video\_url</strong>`object`

              输入的视频文件信息。当`type`为`video_url`时是必选参数。

              Qwen-VL 只可理解视频文件的视觉信息，Qwen-Omni 可理解视频文件中的视觉与音频信息。

              <Accordion title="属性">
                <strong>url</strong> `string`<strong>（必选）</strong>

                视频文件的公网 URL 或 Base64 Data URL。输入本地视频文件请参见[输入 Base64 编码的本地文件](/zh/model-studio/qwen-omni#c516d1e824x03)。
              </Accordion>

              <strong>fps</strong>`float`（可选）

              每秒抽帧数。取值范围为 \[0.1, 10]，默认值为2.0。

              > MiniMax/MiniMax-M3.1-Flash 和 MiniMax/MiniMax-M3 的 fps 取值范围为 \[0.2, 5]，默认值为 1。

              <Accordion title="功能说明">
                fps有两个功能：

                - 输入视频文件时，控制抽帧频率，每 `1/fps` 秒抽取一帧。

                  > 适用于 [Qwen-VL 与 QVQ](/zh/model-studio/vision)、[MiniMax/MiniMax-M3.1-Flash 和 MiniMax/MiniMax-M3](/zh/model-studio/minimax-api-by-minimax) 模型。
                - 告知模型相邻帧之间的时间间隔，帮助其更好地理解视频的时间动态。同时适用于输入视频文件与图像列表时。该功能同时支持视频文件和图像列表输入，适用于事件时间定位或分段内容摘要等场景。

                  > 支持Qwen3.7、Qwen3.6、Qwen3.5、`Qwen3-VL`、`Qwen2.5-VL`、Qwen3.8-Omni-Flash、Qwen3.5-Omni与QVQ模型。

                较大的`fps`适合高速运动的场景（如体育赛事、动作电影等），较小的`fps`适合长视频或内容偏静态的场景。
              </Accordion>

              <Accordion title="示例值">
                - 图像列表传入：`{"video":["https://xx1.jpg",...,"https://xxn.jpg"]，"fps":2}`
                - 视频文件传入：`{"video": "https://xx1.mp4"，"fps":2}`
              </Accordion>

              <strong>min\_pixels</strong>`integer`（可选）

              设定输入图像或视频帧的最小像素阈值。当输入图像或视频帧的像素小于`min_pixels`时，会将其进行放大，直到总像素高于`min_pixels`。适用型号及取值见下方说明。

              <Accordion title="取值范围">
                - <strong>输入图像：</strong>

                  - `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`、Qwen3.7、Qwen3.6、Qwen3.5、Qwen3-VL：默认值和最小值均为：`65536`
                  - `qwen3.8-omni-flash`、Qwen3.5-Omni ：默认值和最小值均为： `24576`
                  - `Qwen2.5-VL`开源系列及`QVQ`系列模型：默认值和最小值均为`3136`
                - <strong>输入视频文件或图像列表：</strong>

                  - `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`、`qwen3.8-omni-flash`、Qwen3.7、Qwen3.6、Qwen3.5、`Qwen3.5-Omni`、Qwen3-VL（包括商业版和开源版）：默认值为`65536`，最小值为`4096`
                  - `Qwen2.5-VL`开源系列及`QVQ`系列模型：默认值为`50176`，最小值为`3136`
              </Accordion>

              <Accordion title="示例值">
                - 输入图像：`{"type": "image_url","image_url": {"url":"https://xxxx.jpg"},"min_pixels": 65536}`
                - 输入视频文件时：`{"type": "video_url","video_url": {"url":"https://xxxx.mp4"},"min_pixels": 65536}`
                - 输入图像列表时：`{"type": "video","video": ["https://xx1.jpg",...,"https://xxn.jpg"],"min_pixels": 65536}`
              </Accordion>

              <strong>max\_pixels</strong>`integer`（可选）

              用于设定输入图像或视频帧的最大像素阈值。当输入图像或视频的像素在`[min_pixels, max_pixels]`区间内时，模型会按原图进行识别。当输入图像像素大于`max_pixels`时，会将图像进行缩小，直到总像素低于`max_pixels`。适用型号及取值见下方说明。

              <Accordion title="取值范围">
                - <strong>输入图像：</strong>

                  `max_pixels` 的取值与是否开启`vl_high_resolution_images`参数有关。

                  - 当`vl_high_resolution_images`为`False`时：

                    - `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`、Qwen3.7、 Qwen3.6 、 Qwen3.5 、 Qwen3-VL ：默认值为 `2621440` ，最大值为： `16777216`
                    - `qwen3.8-omni-flash`、Qwen3.5-Omni ：默认值为 `1310720` ，最大值为： `16777216`
                    - `Qwen2.5-VL` 开源系列及 `QVQ` 系列模型：默认值为 `1003520` ，最大值为 `12845056`

                  - 当`vl_high_resolution_images`为`True`时：

                    - `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`、`qwen3.8-omni-flash`、Qwen3.7、 Qwen3.6 、 Qwen3.5-Omni 、 Qwen3.5 、 Qwen3-VL ： `max_pixels` 无效，输入图像的最大像素固定为 `16777216`
                    - `Qwen2.5-VL` 开源系列及 `QVQ` 系列模型： `max_pixels` 无效，输入图像的最大像素固定为 `12845056`
                - <strong>输入视频文件或图像列表：</strong>

                  - `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`、`qwen3.8-omni-flash`、Qwen3.7、Qwen3.6、Qwen3.5、Qwen3.5-Omni、Qwen3-VL闭源系列：默认值为`655360`，最大值为`2048000`
                  - 其他`Qwen3-VL`开源模型：默认值`655360`，最大值为`786432`
                  - `Qwen2.5-VL`开源系列及`QVQ`系列模型：默认值为`501760`，最大值为`602112`
              </Accordion>

              <Accordion title="示例值">
                - 输入图像：`{"type": "image_url","image_url": {"url":"https://xxxx.jpg"},"max_pixels": 8388608}`
                - 输入视频文件时：`{"type": "video_url","video_url": {"url":"https://xxxx.mp4"},"max_pixels": 655360}`
                - 输入图像列表时：`{"type": "video","video": ["https://xx1.jpg",...,"https://xxn.jpg"],"max_pixels": 655360}`
              </Accordion>

              <strong>total\_pixels</strong>`integer`（可选）

              用于限制从视频中抽取的所有帧的总像素（单帧图像像素 × 总帧数）。如果视频总像素超过此限制，系统将对视频帧进行缩放，但仍会确保单帧图像的像素值在`[min_pixels, max_pixels]`范围内。适用型号及取值见下方说明。

              对于抽帧数量较多的长视频，可适当降低此值以减少Token消耗和处理时间，但这可能会导致图像细节丢失。

              <Accordion title="取值范围">
                - `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`、Qwen3.7、Qwen3.6、Qwen3.5系列 ：默认值和最大值均为 `819200000` ，该值对应 `800000` 个图像 Token（每 32×32 像素对应 1 个图像 Token）。
                - Qwen3-VL闭源系列 ：默认值和最大值均为 `134217728` ，该值对应 `131072` 个图像 Token（每 32×32 像素对应 1 个图像 Token）。
                - `qwen3.8-omni-flash`、`Qwen3.5-Omni` ：默认值和最小值均为 `184549376` ，该值对应 `180224` 个图像 Token（每 32×32 像素对应 1 个图像 Token）。
                - 其他`Qwen3-VL`开源模型：默认值和最小值均为`67108864`，该值对应 `65536` 个图像 Token（每 32×32 像素对应 1 个图像 Token）。
                - `Qwen2.5-VL`开源系列及`QVQ`系列模型：默认值和最小值均为`51380224`，该值对应 `65536` 个图像 Token（每 28×28 像素对应 1 个图像 Token）。
              </Accordion>

              <Accordion title="示例值">
                - 输入视频文件时：`{"type": "video_url","video_url": {"url":"https://xxxx.mp4"},"total_pixels": 134217728}`
                - 输入图像列表时：`{"type": "video","video": ["https://xx1.jpg",...,"https://xxn.jpg"],"total_pixels": 134217728}`
              </Accordion>

              <strong>cache\_control</strong>`object`（可选）

              用于开启显式缓存。相关文档：[显式缓存](/zh/model-studio/context-cache#825f201c5fy6o)。

              <Accordion title="属性">
                <strong>type</strong> `string`<strong>（必选）</strong>

                仅支持设定为`ephemeral`。
              </Accordion>
            </Accordion>

            <strong>role</strong>`string`<strong>（必选）</strong>

            用户消息的角色，固定为`user`。
          </Accordion>

          Assistant Message `object`（可选）

          模型的回复。通常用于在多轮对话中作为上下文回传给模型。

          <Accordion title="属性">
            <strong>content</strong>`string`（可选）

            模型回复的文本内容。包含`tool_calls`时，`content`可以为空；否则`content`为必选。

            <strong>role</strong>`string`<strong>（必选）</strong>

            助手消息的角色，固定为`assistant`。

            <strong>partial</strong>`boolean`（可选）默认值为`false`

            是否开启前缀续写。

            可选值：

            - true：开启；
            - false：不开启。

            支持的模型参见[前缀续写](/zh/model-studio/partial-mode)。

            <strong>tool\_calls</strong> `array`（可选）

            发起 Function Calling 后，返回的工具与入参信息，包含一个或多个对象。由上一轮模型响应的`tool_calls`字段获得。

            <Accordion title="属性">
              <strong>id</strong> `string`<strong>（必选）</strong>

              工具响应的ID。

              <strong>type</strong> `string`<strong>（必选）</strong>

              工具类型，当前只支持设为`function`。

              <strong>function</strong> `object`<strong>（必选）</strong>

              工具与入参信息。

              <Accordion title="属性">
                <strong>name</strong> `string`<strong>（必选）</strong>

                工具名称。

                <strong>arguments</strong> `string`<strong>（必选）</strong>

                入参信息，为JSON格式字符串。
              </Accordion>

              <strong>index</strong> `integer`<strong>（必选）</strong>

              当前工具信息在`tool_calls`数组中的索引。
            </Accordion>
          </Accordion>

          Tool Message `object`（可选）

          工具的输出信息。

          <Accordion title="属性">
            <strong>content</strong>`string`<strong>（必选）</strong>

            工具函数的输出内容，必须为字符串。若工具返回结构化数据（如JSON），需将其序列化为字符串。

            <strong>role</strong>`string`<strong>（必选）</strong>

            固定为`tool`。

            <strong>tool\_call\_id</strong>`string`<strong>（必选）</strong>

            发起 Function Calling 后返回的 id，通过completion.choices\[0].message.tool\_calls\[\$index].id获取，用于标记 Tool Message 对应的工具。
          </Accordion>
        </Accordion>

        <strong>stream</strong>`boolean`（可选） 默认值为 `false`

        是否以流式输出方式回复。相关文档：[流式输出](/zh/model-studio/stream)

        可选值：

        - `false`：模型生成全部内容后一次性返回；
        - `true`：边生成边输出，每生成一部分内容即返回一个数据块（chunk）。需实时逐个读取这些块以拼接完整回复。

        推荐设置为`true`，可提升阅读体验并降低超时风险。

        <Note>
          非流式调用的最大超时时间不少于300秒，实际时长因部署区域与选用模型存在差异。若超时未完成，服务将中断请求并返回已生成的内容（而非报错）。建议输出较长的场景务必使用流式调用。详情请参见[文本生成模型概述](/zh/model-studio/text-generation#11241147efwpm)中的超时说明。
        </Note>

        <strong>stream\_options</strong>`object`（可选）

        流式输出的配置项，仅在 `stream` 为 `true` 时生效。

        <Accordion title="属性" defaultOpen>
          <strong>include\_usage</strong>`boolean`（可选）默认值为`false`

          是否在响应的<strong>最后一个数据块</strong>包含Token消耗信息。

          可选值：

          - `true`：包含；
          - `false`：不包含。

          > 流式输出时，Token 消耗信息仅可出现在响应的最后一个数据块。
        </Accordion>

        <strong>modalities</strong>`array`（可选）默认值为`["text"]`

        输出数据的模态，仅适用于 Qwen-Omni 模型。相关文档：[非实时（Qwen-Omni）](/zh/model-studio/qwen-omni)

        可选值：

        - `["text","audio"]`：输出文本与音频，适用于支持音频输出的型号；
        - `["text"]`：仅输出文本。

        `qwen3.8-omni-flash` 仅输出文本。示例见 [Qwen3.8-Omni-Flash](/zh/model-studio/qwen-omni#qwen38-offline)。

        <strong>audio</strong>`object`（可选）

        输出音频的音色与格式，适用于支持音频输出的 Qwen-Omni 模型，且`modalities`参数需为`["text","audio"]`。相关文档：[非实时（Qwen-Omni）](/zh/model-studio/qwen-omni)

        <Accordion title="属性">
          <strong>voice</strong>`string` <strong>（必选）</strong>

          输出音频的音色。请参见[非实时（Qwen-Omni）](/zh/model-studio/qwen-omni)。

          <strong>format</strong>`string` <strong>（必选）</strong>

          输出音频的格式，仅支持设定为`wav`。
        </Accordion>

        <strong>temperature</strong>`float`（可选）

        采样温度，控制模型生成文本的多样性。

        temperature越高，生成的文本更多样，反之，生成的文本更确定。

        取值范围： \[0, 2)

        MiniMax/MiniMax-M3.1-Flash 和 MiniMax/MiniMax-M3 固定使用 `1.0`，不支持自定义；传入其他值时不会生效。

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。更多说明，请参见[概述](/zh/model-studio/text-generation)。

        <Accordion title="temperature默认值">
          - qwen3.8-max/qwen3.8-flash（思考模式）：视觉理解0.6，文本输入1.0，0.6以下的temperature值会默认改为0.6
          - Qwen3.8-Omni-Flash：思考模式为 0.6，非思考模式为 0.7；
          - Qwen3.8（非思考模式）、Qwen3.7（非思考模式）、Qwen3.6（非思考模式）、Qwen3.5-Omni、Qwen3.5（非思考模式）、Qwen3（非思考模式）、Qwen3-Instruct系列、Qwen3-Coder系列、qwen-max系列、qwen-plus系列（非思考模式）、qwen-flash系列（非思考模式）、qwen开源系列、qwen-coder系列、qwen-doc-turbo、Qwen3-VL（非思考）：0.7；
          - QVQ系列 : 0.5；
          - qwen-audio-turbo系列：0.00001；
          - qwen-vl系列：0.01；
          - qwen-math系列：0；
          - Qwen3.7（思考模式）、Qwen3.6（思考模式）、Qwen3.5（思考模式）、Qwen3（思考模式）、Qwen3-Thinking、Qwen3-Omni-Captioner、QwQ 系列：0.6；
          - qwen-long系列： 1.0；
          - qwen-plus-character：0.92
          - qwen3-omni-flash系列：0.9
          - Qwen3-VL（思考模式）：0.8
          - DeepSeek系列（阿里云直供）：deepseek-v4.1-flash、deepseek-v4-pro: 1.0；deepseek-r1-distill-qwen 蒸馏版: 0.6；
          - DeepSeek系列（硅基流动直供）：siliconflow/deepseek-v3.2、siliconflow/deepseek-v3.1-terminus、siliconflow/deepseek-r1-0528、siliconflow/deepseek-v3-0324: 1.0；
          - DeepSeek系列（快手万擎直供）：vanchin/deepseek-v3.2-think（思考模式）: 0.6；vanchin/deepseek-v3.1-terminus: 0.7；vanchin/deepseek-v3.2-speciale、vanchin/deepseek-r1、vanchin/deepseek-v3、vanchin/deepseek-ocr: 1.0；
          - Kimi系列（阿里云直供）：kimi-k2.7-code、kimi-k2.6（思考模式）、kimi-k2.5（思考模式）: 1.0；kimi-k2.6（非思考模式）、kimi-k2.5（非思考模式）: 0.6；
          - Kimi系列（月之暗面直供）：kimi/kimi-k3、kimi/kimi-k2.7-code-highspeed、kimi/kimi-k2.7-code、kimi/kimi-k2.6（思考模式）、kimi/kimi-k2.5（思考模式）: 1.0；kimi/kimi-k2.6（非思考模式）、kimi/kimi-k2.5（非思考模式）: 0.6；
          - GLM系列（阿里云直供）：glm-5.1、glm-5: 1.0；
          - GLM系列（智谱直供）：ZHIPU/GLM-5.1、ZHIPU/GLM-5: 0.6；
          - MiniMax系列（阿里云直供）：MiniMax-M2.5: 1.0；
          - MiniMax系列（稀宇科技直供）：MiniMax/MiniMax-M3.1-Flash、MiniMax/MiniMax-M3、MiniMax/MiniMax-M2.7、MiniMax/MiniMax-M2.5、MiniMax/MiniMax-M2.1: 1.0。
          - MiMo系列（小米直供）：mimo-v2.5-pro: 1.0，范围 \[0, 1.5]；xiaomi/mimo-v2.6-pro-ultraspeed: 1.0，不支持自定义，传入其他值时仍按 1.0 生效。
        </Accordion>

        > 不建议修改QVQ模型的默认temperature值 。

        <strong>top\_p</strong>`float`（可选）

        核采样的概率阈值，控制模型生成文本的多样性。

        top\_p越高，生成的文本更多样。反之，生成的文本更确定。

        取值范围：（0,1.0]

        MiniMax/MiniMax-M3.1-Flash 和 MiniMax/MiniMax-M3 固定使用 `0.95`，不支持自定义；传入其他值时不会生效。

        temperature与top\_p均可以控制生成文本的多样性，建议只设置其中一个值。更多说明，请参见[概述](/zh/model-studio/text-generation)。

        <Accordion title="top_p默认值">
          非思考模式下的以下型号（`qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`）、Qwen3.7（非思考模式）、Qwen3.6（非思考模式）、Qwen3.5-Omni、Qwen3.5（非思考模式）、Qwen3（非思考模式）、Qwen3-Instruct系列、Qwen3-Coder系列、qwen-max系列、qwen-plus系列（非思考模式）、qwen-flash系列（非思考模式）、Qwen 2.5开源系列、qwen-coder系列、qwen-long、qwen-doc-turbo、Qwen3-VL（非思考）：0.8；

          Qwen3.8-Omni-Flash：思考模式为 0.95，非思考模式为 0.8；

          QVQ系列 : 0.5；

          qwen-math系列、Qwen3-Omni-Flash系列：1.0；

          思考模式下的以下型号（`qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`）、Qwen3.7（思考模式）、Qwen3.6（思考模式）、Qwen3.5（思考模式）、Qwen3（思考模式）、Qwen3-VL（思考模式）、Qwen3-Thinking、QwQ 系列、Qwen3-Omni-Captioner、qwen-plus-character：0.95

          DeepSeek系列（阿里云直供）：deepseek-v4-pro、deepseek-r1-distill-qwen 蒸馏版: 0.95；

          DeepSeek系列（硅基流动直供）：siliconflow/deepseek-v3.2、siliconflow/deepseek-v3.1-terminus、siliconflow/deepseek-r1-0528、siliconflow/deepseek-v3-0324: 1.0；

          DeepSeek系列（快手万擎直供）：vanchin/deepseek-v3.2-think、vanchin/deepseek-v3.1-terminus: 0.95；vanchin/deepseek-v3.2-speciale: 0.9；vanchin/deepseek-r1: 0.8；vanchin/deepseek-v3、vanchin/deepseek-ocr: 1.0；

          Kimi系列（阿里云直供）：kimi-k2.7-code、kimi-k2.6、kimi-k2.5: 0.95；

          Kimi系列（月之暗面直供）：kimi/kimi-k3、kimi/kimi-k2.7-code-highspeed、kimi/kimi-k2.7-code、kimi/kimi-k2.6、kimi/kimi-k2.5: 0.95；

          GLM系列（阿里云直供）：0.95；

          GLM系列（智谱直供）：ZHIPU/GLM-5.1、ZHIPU/GLM-5: 0.95；

          MiniMax系列（阿里云直供）：MiniMax-M2.5: 0.95；

          MiniMax系列（稀宇科技直供）：MiniMax/MiniMax-M3.1-Flash、MiniMax/MiniMax-M3: 0.95；MiniMax/MiniMax-M2.7、MiniMax/MiniMax-M2.5、MiniMax/MiniMax-M2.1: 0.9。

          MiMo系列（小米直供）：xiaomi/mimo-v2.5-pro: 0.95，范围 \[0.01, 1.0]；xiaomi/mimo-v2.6-pro-ultraspeed: 0.95，不支持自定义，传入其他值时仍按 0.95 生效。
        </Accordion>

        > 不建议修改QVQ模型的默认 top\_p 值。

        <strong>top\_k</strong>`integer` （可选）

        指定生成过程中用于采样的候选 Token 数量。值越大，输出越随机；值越小，输出越确定。若设为 `null` 或大于 100，则禁用 `top_k` 策略，仅 `top_p` 策略生效。取值必须为大于或等于 0 的整数。

        <Accordion title="top_k默认值">
          QVQ系列：10；

          QwQ 系列：40；

          qwen-math 系列、qwen-audio-turbo系列：1；

          Qwen3-Omni-Flash系列：50；

          其余模型均为20。

          GLM系列（阿里云直供）：20；

          DeepSeek/Kimi/MiniMax系列均不支持top\_k参数。
        </Accordion>

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：extra\_body=\{"top\_k":xxx}。

        > 不建议修改QVQ模型的默认 top\_k 值。

        <strong>repetition\_penalty</strong>`float` （可选）

        模型生成时连续序列中的重复度。提高repetition\_penalty时可以降低模型生成的重复度，1.0表示不做惩罚。没有严格的取值范围，只要大于0即可。

        <Accordion title="repetition_penalty默认值">
          - `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`、qwen-max、qwen-math系列、qwen-audio-turbo系列、QVQ系列、QwQ系列、Qwen3-VL： 1.0；
          - qwen-coder系列、qwen2-1.5b-instruct、qwen2-0.5b-instruct：1.1；
          - 其余模型为1.05。
          - GLM系列（阿里云直供）：1.0；
          - Kimi系列（月之暗面直供）：0.0。
        </Accordion>

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：extra\_body=\{"repetition\_penalty":xxx}。

        > 不建议修改QVQ模型的默认 repetition\_penalty 值。

        <strong>presence\_penalty</strong> `float`（可选）

        控制模型生成文本时的内容重复度。

        取值范围：\[-2.0, 2.0]。正值降低重复度，负值增加重复度。

        在创意写作或头脑风暴等需要多样性、趣味性或创造力的场景中，建议调高该值；在技术文档或正式文本等强调一致性与术语准确性的场景中，建议调低该值。

        <Accordion title="presence_penalty默认值">
          非思考模式下的以下型号（`qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`）、Qwen3.7（非思考模式）、Qwen3.6（非思考模式）、Qwen3.5-Omni、Qwen3.5（非思考模式）、Qwen3（非思考模式）、Qwen3-Instruct系列/1.7b/4b（思考模式）、QVQ系列、qwen-max、qwen2.5-vl系列、Qwen3-VL（非思考）：1.5；

          Qwen3.8-Omni-Flash：思考模式为 0.0，非思考模式为 1.5；

          qwen-plus/qwen-plus-latest/2025-04-28（思考模式）、qwen-turbo-2025-04-28（思考模式）：0.5；

          其余均为0.0。

          DeepSeek系列（阿里云直供）：deepseek-r1-distill-qwen 蒸馏版: 1；

          Kimi系列（阿里云直供）：kimi-k2.7-code、kimi-k2.6、kimi-k2.5: 0.0；

          Kimi系列（月之暗面直供）：0.0；

          MiniMax系列（阿里云直供）：MiniMax-M2.5: 0.0；

          其余DeepSeek/Kimi/GLM/MiniMax模型无默认值。
        </Accordion>

        <Accordion title="原理介绍">
          如果参数值是正数，模型将对目前文本中已存在的Token施加一个惩罚值（惩罚值与文本出现的次数无关），减少这些Token重复出现的几率，从而减少内容重复度，增加用词多样性。
        </Accordion>

        <Accordion title="示例">
          提示词：把这句话翻译成中文“This movie is good. The plot is good, the acting is good, the music is good, and overall, the whole movie is just good. It is really good, in fact. The plot is so good, and the acting is so good, and the music is so good.”

          参数值为2.0：这部电影很好。剧情很棒，演技棒，音乐也非常好听，总的来说，整部电影都好得不得了。实际上它真的很优秀。剧情非常精彩，演技出色，音乐也是那么的动听。

          参数值为0.0：这部电影很好。剧情好，演技好，音乐也好，总的来说，整部电影都很好。事实上，它真的很棒。剧情非常好，演技也非常出色，音乐也同样优秀。

          参数值为-2.0：这部电影很好。情节很好，演技很好，音乐也很好，总的来说，整部电影都很好。实际上，它真的很棒。情节非常好，演技也非常好，音乐也非常好。
        </Accordion>

        > 不建议修改QVQ模型的默认presence\_penalty值。

        <strong>response\_format</strong>`object` （可选） 默认值为`{"type": "text"}`

        返回内容的格式。可选值：

        - `{"type": "text"}`：输出文字回复；
        - `{"type": "json_object"}`：输出标准格式的JSON字符串。
        - `{"type": "json_schema", "json_schema": {...}}`：输出严格符合指定 JSON Schema 的JSON字符串，可精确控制输出结构与字段类型。

        > 相关文档：[结构化输出](/zh/model-studio/qwen-structured-output)。`json_object` 与 `json_schema` 支持的模型不同，参见[支持的模型](/zh/model-studio/qwen-structured-output#7a8e438e89xeq)。

        > 若指定为`{"type": "json_object"}`，需在提示词中明确指示模型输出JSON，如：“请按照json格式输出”，否则会报错；若指定为`{"type": "json_schema", ...}`，提示词无需包含 JSON 关键词。

        <Accordion title="属性">
          <strong>type</strong>`string`<strong>（必选）</strong>

          返回内容的格式。可选值：

          - `text`：输出文字回复；
          - `json_object`：输出标准格式的JSON字符串；
          - `json_schema`：输出严格符合 `json_schema` 字段所定义结构的JSON字符串。

          <strong>json\_schema</strong>`object`（可选）

          `type` 为 `json_schema` 时必须设置，用于定义模型输出需要遵循的 JSON 结构。相关文档：[获取指定格式的输出](/zh/model-studio/qwen-structured-output#a4f5d7108anxd)。

          > 通过 OpenAI SDK 的 `parse` 方法调用时，可直接传入 Python Pydantic 类或 Node.js Zod 对象，SDK 会自动将其转换为 JSON Schema，无需手动构造。

          <Accordion title="属性">
            <strong>name</strong>`string`<strong>（必选）</strong>

            Schema 的名称。

            <strong>schema</strong>`object`<strong>（必选）</strong>

            描述输出结构的 JSON Schema 对象。通过 `properties` 定义字段结构，通过 `required` 声明必填字段列表，通过 `additionalProperties` 控制是否允许输出未在 Schema 中定义的字段（推荐设置为 `false`，即只输出已定义的字段）。支持的数据类型：string、number、integer、boolean、object、array、enum。相关文档：[配置指南](/zh/model-studio/qwen-structured-output#001cb6ae072tc)。

            <strong>strict</strong>`boolean`（可选）

            是否严格遵循 `schema` 定义的结构。推荐设置为 `true`。
          </Accordion>
        </Accordion>

        <strong>max\_tokens</strong>`integer` （可选， <strong>即将废弃</strong> ）

        > 该参数即将废弃，新接入请使用 `max_completion_tokens`。

        该参数的含义随模型不同，具体如下：

        - deepseek-v4.1-flash、deepseek-v4-pro、deepseek-v4-pro-0813、deepseek-v4-flash-0731：模型回答与思维链内容之和的最大 Token 数。模型输出超过此值时生成将提前停止，返回的 `finish_reason` 为 `length`。
        - glm-5.3：`max_tokens` 为模型回答与思维链内容之和的最大 Token 数，模型输出超过此值时生成将提前停止，返回的 `finish_reason` 为 `length`。glm-5.3 会忽略 `thinking_budget` 参数。
        - glm-5.2：不传入 `thinking_budget` 参数时，`max_tokens` 为模型回答与思维链内容之和的最大 Token 数，模型输出超过此值时生成将提前停止，返回的 `finish_reason` 为 `length`；传入 `thinking_budget` 参数时，`max_tokens` 仅为模型回答的最大 Token 数，思维链部分的 Token 数由 `thinking_budget` 单独控制。
        - 其他模型：模型回答的最大 Token 数（不包含思维链）。若生成内容超过此值，生成将提前停止，返回的 `finish_reason` 为 `length`。

        GLM 模型的默认值与最大值如下表所示，其他模型的默认值与最大值均为模型的最大输出长度。

| 供应方式  | 模型                                                                                             | 默认值   | 最大值    |
| ----- | ---------------------------------------------------------------------------------------------- | ----- | ------ |
| 阿里云直供 | glm-5.3、glm-5.2、glm-5.2-fast-preview、glm-5.1、glm-5                                             | 65536 | 131072 |
| 智谱直供  | ZHIPU/GLM-5.3、ZHIPU/GLM-5.3-Flash、ZHIPU/GLM-5.3-FlashX、ZHIPU/GLM-5.2、ZHIPU/GLM-5.1、ZHIPU/GLM-5 | 65536 | 131072 |

        <strong>max\_completion\_tokens</strong>`integer`（可选）

        模型输出的最大长度，包含思维链和模型回答。模型输出超过此值时生成将提前停止，返回的 `finish_reason` 为 `length`。

        GLM 模型的默认值与最大值见 `max_tokens` 参数中的表格，其他模型的默认值与最大值均为模型的最大输出长度。

        与 `max_tokens` 的区别：`max_completion_tokens` 始终限制模型完整输出（思维链 + 回答）；`max_tokens` 的具体限制范围随模型及 `thinking_budget` 设置而异，见上文说明。思考类模型推荐使用 `max_completion_tokens`。

        支持以下模型：

        - 千问 Max：Qwen3.7-Max 及之后的模型
        - 千问 Plus：Qwen3.5-Plus 及之后的模型
        - 千问 Flash：Qwen3.5-Flash 及之后的模型
        - Kimi：kimi-k2.5 及其之后推出的Kimi模型
        - GLM：上表所列的阿里云直供和智谱直供模型
        - MiniMax：MiniMax-M2.5 及之后推出的MiniMax模型
        - DeepSeek：deepseek-v4-pro 及之后推出的DeepSeek模型

        > 除上表所列的智谱直供 GLM 模型外，以上模型均不包含其他三方直供模型。

        > 实际输出 Token 数与设置的 `max_completion_tokens` 值之间最多可能存在 10 个 Token 的误差。

        <strong>vl\_high\_resolution\_images</strong>`boolean`（可选）默认值为`false`

        是否将输入图像的像素上限提升至 16384 Token 对应的像素值。相关文档：[处理高分辨率图像](/zh/model-studio/vision)。

        - `vl_high_resolution_images：true`，使用固定分辨率策略，忽略 `max_pixels` 设置，超过此分辨率时会将图像总像素缩小至此上限内。

          <Accordion title="点击查看各模型像素上限">
            `vl_high_resolution_images`为`True`时，不同模型像素上限不同：

            - `qwen3.8-flash`、`qwen3.8-omni-flash`、Qwen3.7系列、`Qwen3.6`系列、`Qwen3.5`系列、`Qwen3-VL系列`模型：`16777216`（每`Token`对应`32*32`像素，即`16384*32*32`）
            - `QVQ系列`、其他`Qwen2.5-VL系列`模型：`12845056`（每`Token`对应`28*28`像素，即 `16384*28*28`）
          </Accordion>
        - `vl_high_resolution_images`为`false`，像素上限由 `max_pixels` 决定，输入图像的像素超过`max_pixels`会将图像缩小至`max_pixels`内。各模型的默认像素上限即`max_pixels`的默认值。

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：extra\_body=\{"vl\_high\_resolution\_images":xxx}。

        <strong>n</strong>`integer`（可选） 默认值为1

        生成响应的数量，取值范围是`1-4`。适用于需生成多个候选响应的场景，例如创意写作或广告文案。

        > `n=0` 等价于 `n=1`。

        > 仅 [Qwen3（非思考模式）](/zh/model-studio/deep-thinking) 和 qwen-plus-character 支持将 `n` 设为大于 `1`。MiniMax/MiniMax-M3.1-Flash 和 MiniMax/MiniMax-M3 仅支持 `n=1`。

        > 若传入 `tools` 参数， 请将`n` 设为 1。

        > 增大 n 会增加输出 Token 的消耗，但不增加输入 Token 消耗。

        <strong>enable\_thinking</strong> `boolean` （可选）

        使用混合思考（回复前既可思考也可不思考）模型时，是否开启思考模式。适用于 Qwen3.8、Qwen3.7、Qwen3.6、Qwen3.5、Qwen3、Qwen3.8-Omni-Flash、Qwen3-Omni-Flash、Qwen3-VL模型，以及 DeepSeek-V4.1-Flash、DeepSeek-V4-Pro/V4-Flash 系列（阿里云直供）、DeepSeek-V3.2/V3.2-exp/V3.1 系列（阿里云直供、硅基流动直供、快手万擎直供）、Kimi-K2.7-code（仅思考模型）、Kimi-K2.6/K2.5 系列（阿里云直供、月之暗面直供）、GLM 系列。DeepSeek-V4 系列默认开启思考，可通过 `reasoning_effort` 参数调整推理力度。

        可选值：

        - `true`：开启

          > 开启后，思考内容将通过`reasoning_content`字段返回。
        - `false`：不开启

        不同模型的默认值：[支持的模型](/zh/model-studio/deep-thinking#78286fdc35hlw)

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"enable_thinking": xxx}`。

        > 若不使用 OpenAI SDK，而是通过 HTTP（如 curl）方式直接调用，则无需 `extra_body`，直接将 `enable_thinking` 与 `model`、`messages` 等参数一样放在请求体（`body`）的顶层即可，例如 `"enable_thinking": true`。

        > 稀宇科技直供的 MiniMax/MiniMax-M3.1-Flash 和 MiniMax/MiniMax-M3 不使用此参数，请使用 `thinking` 参数。

        <strong>thinking</strong> `object` （可选）默认值为 `{"type":"adaptive"}`

        控制稀宇科技直供 MiniMax 模型的思考模式。

        `thinking.type` 可选值：

        - `adaptive`：开启思考。MiniMax/MiniMax-M3.1-Flash 始终使用此模式；MiniMax/MiniMax-M3 默认使用此模式。
        - `disabled`：关闭思考，仅 MiniMax/MiniMax-M3 支持。MiniMax/MiniMax-M3.1-Flash 设置此值时返回 400 错误。

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"thinking": {"type": "adaptive"}}`。

        <strong>reasoning\_split</strong> `boolean` （可选）

        控制 MiniMax 思考内容的返回位置：

        - `true`：通过 `reasoning_content` 字段单独返回。MiniMax/MiniMax-M3.1-Flash 仅支持此值。
        - `false`：在 `content` 字段中以 `<think>` 标签返回。仅 MiniMax/MiniMax-M3 支持此值。

        该参数非 OpenAI 标准参数。通过 Python SDK 调用时，请放入 `extra_body` 对象中，例如 `extra_body={"reasoning_split": True}`。

        <strong>preserve\_thinking</strong> `boolean` （可选）默认值为 `false`

        是否将对话历史中 assistant 消息的 reasoning\_content 拼接至模型输入。适用于需要模型参考历史思考过程的场景。

        需要参考上一轮思考时，将其放在历史 assistant 消息的 `reasoning_content` 字段中随 `messages` 回传。用法见[传递思考过程](/zh/model-studio/deep-thinking#jln7docdq5et5)。

        目前支持qwen3.8-max、qwen3.8-max-0902、qwen3.8-flash（默认开启）、qwen3.8-omni-flash（默认开启）、qwen3.7-max、qwen3.7-max-2026-05-20以及后续快照、qwen3.7-plus、qwen3.7-plus-2026-05-26、qwen3.6-plus、qwen3.6-plus-2026-04-02、qwen3.7-flash、qwen3.7-flash-2026-07-15、qwen3.6-flash、qwen3.6-flash-2026-04-16、kimi-k2.6（阿里云百炼部署）、kimi-k2.7-code（阿里云百炼部署，默认开启）、kimi/kimi-k2.7-code-highspeed（月之暗面直供，默认开启）、kimi/kimi-k2.7-code（月之暗面直供，默认开启）。

        > <strong>重要：</strong>使用 qwen3.8-max/qwen3.8-flash 时，preserve\_thinking 默认为 true，必须将历史对话中所有的 reasoning\_content 完整回传。<strong>不支持将 reasoning\_content 拼接到 content 字段中回传。</strong>

        - 对于上述支持此参数的 `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、Qwen3.7、Qwen3.6 和 Kimi 型号，若历史消息中不包含 `reasoning_content`，开启此参数不会报错。
        - 开启后，历史对话中的 reasoning\_content 会计入输入 Token 数量并计费。

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"preserve_thinking": True}`。

        <strong>thinking\_budget</strong> `integer` （可选）

        思考过程的最大 Token 数。适用于 `qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`，以及Qwen3.7、Qwen3.6、Qwen3.5、Qwen3-VL、Qwen3、GLM（阿里云直供）、Kimi（阿里云直供）系列模型，其中 kimi-k3 不支持该参数。相关文档：[限制思考长度](/zh/model-studio/deep-thinking#e7c0002fe4meu)。

        上述型号的默认值为模型最大思维链长度，请参见：模型列表

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"thinking_budget": xxx}`。

        **use\_multichannel** `boolean` （可选）

        控制是否解析多通道音频中的空间信息。适用于 `qwen3.8-omni-flash` 的音频输入，默认为 `false`，所有音频按单通道解析。设为 `true` 且输入为双通道（左右）或四通道（FOA，WYZX 顺序）音频时，解析空间音频信息。HTTP 请求中放在请求体顶层；使用 OpenAI Python SDK 时通过 `extra_body={"use_multichannel": True}` 传入。

        <strong>reasoning\_effort</strong> `string` （可选）

        控制模型的推理力度，不同模型支持的可选值和默认值不同。

        <strong>MiniMax/MiniMax-M3.1-Flash</strong>（默认值为 `max`）

        可选值按推理力度从低到高依次为 `low`、`medium`、`high`、`xhigh` 和 `max`。模型始终开启思考；设置 `none` 会返回 400 错误。

        <strong>DeepSeek-V4、GLM 系列与 kimi/kimi-k3</strong>（默认值为 `high`）

        可选值：

        - `high`：高力度推理
        - `max`：最大力度推理

        low和medium映射为high，xhigh映射为max。

        适用于glm-5.2、glm-5.1、glm-5、deepseek-v4-pro（阿里云直供）、kimi/kimi-k3（月之暗面直供，仅支持 `max`）

        <strong>glm-5.3、ZHIPU/GLM-5.3、ZHIPU/GLM-5.3-Flash、ZHIPU/GLM-5.3-FlashX 与 kimi-k3（阿里云直供）模型：默认值为</strong>`max`

        可选值：

        - `max`（默认）：深度推理
        - `high`：增强推理
        - `low`：轻度推理

        glm-5.3 系列模型始终开启思考，`enable_thinking` 仅支持 `true`，传入 `false` 会导致 API 请求失败；kimi-k3 支持传入 `false` 关闭思考。

        <strong>deepseek-v4-flash-0731 与 deepseek-v4-pro-0813 模型：默认值为</strong>`high`

        可选值：

        - `max`：最大力度推理
        - `high`（默认）：高力度推理
        - `low`：低力度推理

        出于兼容性考虑，`medium` 映射为 high，`xhigh` 映射为 high。

        <strong>deepseek-v4.1-flash 模型：默认值为</strong>`high`

        可选值：

        - `max`：最大力度推理
        - `high`（默认）：高力度推理
        - `low`：低力度推理

        `minimal` 映射为 `low`，`medium` 和 `xhigh` 映射为 `high`，`ultra` 映射为 `max`。

        <strong>`qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`：默认值为</strong>`xhigh`

        可选值：

        - `xhigh`（默认）：高力度推理
        - `medium`：中力度推理
        - `low`：低力度推理

        `max` 映射为 xhigh，`high` 映射为 xhigh，`minimal` 映射为 low，`none` 映射为 enable\_thinking=False。

        > 设置上述可选值及映射值以外的值将会报错。

        <strong>重要：</strong>`qwen3.8-max`、`qwen3.8-max-0902`、`qwen3.8-flash`、`qwen3.8-2.4t-a95b`、`qwen3.8-27b`不支持 reasoning\_effort 与 thinking\_budget 同时设置，同时设置会报错。但两者支持互转：

        - 未设置 thinking\_budget 时，reasoning\_effort 档位自动映射 thinking\_budget：`low` 对应 4096，`medium` 对应 16384，`xhigh` 对应 262144。
        - 未设置 reasoning\_effort 时，thinking\_budget 自动映射回 reasoning\_effort：0\~4096 对应 `low`，4097\~16384 对应 `medium`，16385\~262144 对应 `xhigh`。
        - 两者均未设置时，使用默认 thinking\_budget（131072），默认 reasoning\_effort（xhigh）。

        <strong>Qwen3.8-Omni-Flash</strong>

        `qwen3.8-omni-flash` 默认开启思考，`reasoning_effort` 默认为 `xhigh`。Chat Completions 的 `reasoning_effort` 接受 `none`、`minimal`、`low`、`medium`、`high`、`xhigh`、`max`，不能与 `thinking_budget` 同时设置。参数位置及示例见[Qwen3.8 Omni](/zh/model-studio/qwen-omni#qwen38-offline)。

        可直接选择 `low`、`medium`、`xhigh` 三档思考力度。兼容取值中，`minimal` 映射为 `low`，`high` 和 `max` 映射为 `xhigh`，`none` 表示关闭思考。

        > `reasoning_effort` 是 OpenAI 标准参数。通过 Python SDK 调用时，直接传入 `reasoning_effort="high"`，无需放入 `extra_body`。

        <strong>tool\_stream</strong> `boolean` （可选）默认值为 `false`

        仅在`stream=true`时生效。当前仅Qwen和GLM系列支持。

        <strong>Qwen系列支持列表：</strong>

        - qwen-max系列：qwen3.8-max系列、qwen3.7-max系列的文本模态
        - qwen-plus系列：qwen3.7-plus系列、qwen3.6-plus系列的文本模态，以及qwen3.5-plus系列的全模态
        - qwen-flash系列：qwen3.8-flash系列、qwen3.7-flash系列、qwen3.6-flash系列、qwen3.5-flash系列的全模态

        <strong>Qwen系列使用参考：</strong>

        tool\_stream仅影响复杂工具参数的情况。普通工具参数只要开启`stream=true`就会流式输出。复杂工具是指工具定义中某些参数类型为array或object。

        - `tool_stream=false`：复杂工具参数会一次性输出，默认行为，复杂格式会更准确。
        - `tool_stream=true`：复杂工具参数会流式输出，复杂格式没有超时风险。

        <strong>GLM系列支持列表：</strong>glm-5、glm-5.1（阿里云直供）。

        <strong>GLM系列使用参考：</strong>

        - `tool_stream=false`：工具参数会一次性输出，默认行为，复杂格式会更准确。
        - `tool_stream=true`：工具参数会流式输出，复杂格式没有超时风险。

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"tool_stream": true}`。

        <strong>enable\_code\_interpreter</strong> `boolean` （可选）默认值为 `false`

        是否开启代码解释器功能。相关文档：[代码解释器](/zh/model-studio/qwen-code-interpreter)

        可选值：

        - `true`：开启
        - `false`：不开启

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"enable_code_interpreter": xxx}`。

        <strong>seed</strong>`integer`（可选）

        随机数种子。用于确保在相同输入和参数下生成结果可复现。若调用时传入相同的 `seed` 且其他参数不变，模型将尽可能返回相同结果。

        取值范围：`[0,2 31 −1]`。

        <Accordion title="seed默认值">
          其余模型均为1234。
        </Accordion>

        <strong>logprobs</strong> `boolean` （可选）默认值为 `false`

        是否返回输出 Token 的对数概率，可选值：

        - `true`

          返回
        - `false`

          不返回

        > 思考阶段生成的内容（`reasoning_content`）不会返回对数概率。

        <Accordion title="支持的模型">
          - qwen-plus系列的快照模型（不包含稳定版模型）
          - qwen3-vl-plus系列模型（包含稳定版模型）
          - Qwen3 开源模型
        </Accordion>

        <strong>top\_logprobs</strong> `integer` （可选）默认值为0

        指定在每一步生成时，返回模型最大概率的候选 Token 个数。

        取值范围：\[0,5]

        仅当 `logprobs` 为 `true` 时生效。

        <strong>stop</strong>`string 或 array`（可选）

        用于指定停止词。当模型生成的文本中出现 `stop` 指定的字符串或 `token_id` 时，生成将立即终止。

        > MiniMax/MiniMax-M3.1-Flash 不支持此参数。传入后不会按指定停止词截断输出；请求可能仍成功且返回 `finish_reason: "stop"`，但这不表示指定停止词已生效。

        可传入敏感词以控制模型的输出。

        > stop为数组时，不可将`token_id`和字符串同时作为元素输入，比如不可以指定为`["你好",104307]`。

        <strong>tools</strong>`array`（可选）

        包含一个或多个工具对象的数组，供模型在 Function Calling 中调用。相关文档：[Function Calling](/zh/model-studio/qwen-function-calling)

        设置 tools 且模型判断需要调用工具时，响应会通过 tool\_calls 返回工具信息。

        <Accordion title="属性">
          <strong>type</strong>`string`<strong>（必选）</strong>

          工具类型，当前仅支持设为`function`。

          <strong>function</strong>`object`<strong>（必选）</strong>

          <Accordion title="属性">
            <strong>name</strong>`string`<strong>（必选）</strong>

            工具名称。仅允许字母、数字、下划线（`_`）和短划线（`-`），最长 64 个 Token。

            <strong>description</strong>`string`<strong>（必选）</strong>

            工具描述信息，帮助模型判断何时以及如何调用该工具。

            <strong>parameters</strong>`object`（可选）默认值为 `{}`

            工具的参数描述，需要是一个合法的JSON Schema。JSON Schema的描述可以见[链接](https://json-schema.org/understanding-json-schema)。若`parameters`参数为空，表示该工具没有入参（如时间查询工具）。

            > 为提高工具调用的准确性，建议传入 `parameters`。
          </Accordion>
        </Accordion>

        <strong>tool\_choice</strong> `string 或 object`（可选）默认值为 `auto`

        工具选择策略。若需对某类问题强制指定工具调用方式（例如始终使用某工具或禁用所有工具），可设置此参数。

        > MiniMax/MiniMax-M3.1-Flash 仅支持 `auto`。`none`、`required` 或指定函数名称等其他取值可能报错或不生效；指定函数名称不能保证模型调用该函数。

        `deepseek-v4.1-flash-1017` 支持将 `tool_choice` 设为 `auto`、`none`、`required` 或指定函数名称。

        可选值：

        - `auto`

          大模型自主选择工具策略。
        - `none`

          若不希望进行工具调用，可设定`tool_choice`参数为`none`；
        - `required`

          若希望强制调用至少一个工具，可设定`tool_choice`参数为`required`，模型将始终返回工具调用信息。

          > Qwen 系列模型暂不支持`required`：非思考模式下无法保证一定调用工具，思考模式下当前不支持`required`。
        - `{"type": "function", "function": {"name": "the_function_to_call"}}`

          若希望强制调用某个工具，可设定`tool_choice`参数为`{"type": "function", "function": {"name": "the_function_to_call"}}`，其中`the_function_to_call`是指定的工具函数名称。

          > 思考模式的模型不支持强制调用某个工具。

        <strong>parallel\_tool\_calls</strong> `boolean` （可选）默认值为 `true`

        是否开启并行工具调用。相关文档：[并行工具调用](/zh/model-studio/qwen-function-calling#cb6b5c484bt4x)

        > MiniMax/MiniMax-M3.1-Flash、kimi-k3 不支持通过此参数控制工具调用的并行性。设置为 `false` 也不能保证串行调用，模型仍可能在一轮中返回多个 `tool_calls`。请按实际返回结果处理。

        可选值：

        - `true`：开启
        - `false`：不开启

        <strong>enable\_search</strong> `boolean`（可选）默认值为 `false`

        是否开启联网搜索。相关文档：[联网搜索](/zh/model-studio/web-search)

        可选值：

        - `true`：开启；

          > 若开启后未联网搜索，可优化提示词，或设置`search_options`中的`forced_search`参数开启强制搜索。
        - `false`：不开启。

        > 启用互联网搜索功能可能会增加 Token 的消耗。

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"enable_search": True}`。

        <strong>search\_options</strong>`object`（可选）

        联网搜索的策略。相关文档：[联网搜索](/zh/model-studio/web-search)

        <Accordion title="属性">
          <strong>forced\_search</strong> `boolean`（可选）默认值为`false`

          是否强制开启联网搜索，仅当`enable_search`为`true`时生效。

          可选值：

          - true：强制开启；
          - false：不强制开启，由模型判断是否联网搜索。

          <strong>search\_strategy</strong> `string`（可选）默认值为`turbo`

          搜索量级策略，仅当`enable_search`为`true`时生效。

          `qwen3.8-omni-flash` 开启联网搜索时，需将此参数设为 `agent`。

          可选值：

          - `turbo` （默认）: 兼顾响应速度与搜索效果，适用于大多数场景。
          - `max`: 采用更全面的搜索策略，可调用多源搜索引擎，以获取更详尽的搜索结果，但响应时间可能更长。
          - `agent`：可多次调用联网搜索工具与大模型，实现多轮信息检索与内容整合。

            > 该策略仅适用于 qwen3.5-plus、qwen3.5-plus-2026-02-15、qwen3.5-flash、qwen3.5-flash-2026-02-23、qwen3.8-omni-flash、qwen3.5-omni-plus、qwen3.5-omni-plus-2026-03-15、qwen3.5-omni-flash、qwen3.5-omni-flash-2026-03-15。
            > <strong>enable\_search\_extension</strong> `boolean`（可选）默认值为`false`

          是否开启垂域搜索，仅当`enable_search`为`true`时生效。

          可选值：

          - `true`：开启。
          - `false`：不开启。
        </Accordion>

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"search_options": xxx}`。

        <strong>include\_tool\_usage</strong> `boolean`（可选）默认值为 `false`

        是否在响应的`usage`中返回联网搜索的调用统计。

        可选值：

        - `true`：在`usage`中返回`x_tools`字段。
        - `false`：不返回。

        > `x_tools`仅在本次请求实际触发了联网搜索时返回。若未开启`enable_search`，或模型自行判断无需联网搜索，即使将本参数设为`true`也不会返回该字段。

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"include_tool_usage": True}`。

        <strong>X-DashScope-DataInspection</strong>`string` （可选）

        在千问 API 的内容安全能力基础上，是否进一步识别输入输出内容的违规信息。取值如下：

        - `'{"input":"cip","output":"cip"}'`：进一步识别；
        - 不设置该参数：不进一步识别。

        通过 HTTP 调用时请放入请求头：`-H "X-DashScope-DataInspection: {\"input\": \"cip\", \"output\": \"cip\"}"`；

        通过 Python SDK 调用时请通过`extra_headers`配置：`extra_headers={'X-DashScope-DataInspection': '{"input":"cip","output":"cip"}'}`。

        详细使用方法请参见[输⼊输出 AI 安全护栏](/zh/model-studio/content-security)。

        > 不支持通过 Node.js SDK设置。

        <strong>skill</strong>`array`（可选）

        技能参数，用于启用特定生成技能（如PPT生成）。仅`qwen-doc-turbo`模型支持。详细用法请参见[生成PPT](/zh/model-studio/data-mining-qwen-doc#f6a7b8c9d0pp1)。

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"skill": [...]}`。

        > 使用 <strong>skill 时，stream</strong> 必须设置为 <strong>true</strong>。

        <Accordion title="属性">
          <strong>type</strong>`string`<strong>（必选）</strong>

          技能类型。当前支持：

          - `ppt`：PPT生成。

          <strong>mode</strong>`string` （可选）

          PPT生成模式。可选值：

          - `general` （默认值）：模板模式，需配合`template_id` 使用，生成HTML格式的PPT。
          - `creative` ：创意模式，无需模板，生成图版PPT（每页为图片）。

          <strong>template\_id</strong>`string`（可选）

          PPT模板ID。与`mode`为`general`或未设置`mode`时配合使用。可选值：

          - `news_01`：新闻模板
          - `summary_01`：总结模板
          - `internet_01`：互联网模板
          - `thesis_01`：论文模板
        </Accordion>

        <strong>clear\_thinking</strong>`boolean`（可选）默认值为false

        用于控制多轮对话中是否将历史轮次的 `reasoning_content`（思考过程）作为上下文输入给模型。仅 GLM 系列glm-5.3、glm-5.2、glm-5.1、glm-5模型支持。其中 glm-5.3 的默认值为 `true`，其余模型的默认值为 `false`。

        > 该参数非OpenAI标准参数。通过 Python SDK调用时，请放入 <strong>extra\_body</strong> 对象中。配置方式为：`extra_body={"enable_thinking": True,"clear_thinking": True}`。

        - `true`：忽略历史轮次的 `reasoning_content`，仅使用可见文本、工具调用与结果等非推理内容作为上下文输入，可降低上下文长度与成本。
        - `false`（默认）：保留历史轮次的 `reasoning_content` 并随上下文一同提供给模型。若希望启用 Preserved Thinking，必须在 messages 中完整、未修改、按原顺序透传历史 `reasoning_content`，缺失、裁剪、改写或重排会导致效果下降或无法生效。
      </td>

      <td style={{ verticalAlign: "top" }}>
        <Tabs>
          <Tab title="文本输入">
            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )

              completion = client.chat.completions.create(
                  # 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  model="qwen3.8-max",
                  messages=[
                      {"role": "system", "content": "You are a helpful assistant."},
                      {"role": "user", "content": "你是谁？"},
                  ]
              )
              print(completion.model_dump_json())
              ```

              ```java Java expandable
              // 该代码 OpenAI SDK 版本为 2.6.0
              import com.openai.client.OpenAIClient;
              import com.openai.client.okhttp.OpenAIOkHttpClient;
              import com.openai.models.chat.completions.ChatCompletion;
              import com.openai.models.chat.completions.ChatCompletionCreateParams;

              public class Main {
                  public static void main(String[] args) {
                      OpenAIClient client = OpenAIOkHttpClient.builder()
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .baseUrl("https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1")
                              .build();

                      ChatCompletionCreateParams params = ChatCompletionCreateParams.builder()
                              .addUserMessage("你是谁")
                              .model("qwen3.8-max")
                              .build();

                      try {
                          ChatCompletion chatCompletion = client.chat().completions().create(params);
                          System.out.println(chatCompletion);
                      } catch (Exception e) {
                          System.err.println("Error occurred: " + e.getMessage());
                          e.printStackTrace();
                      }
                  }
              }
              ```

              ```javascript Node.js
              import OpenAI from "openai";

              const openai = new OpenAI(
                  {
                      // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx",
                      apiKey: process.env.DASHSCOPE_API_KEY,
                      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                  }
              );

              async function main() {
                  const completion = await openai.chat.completions.create({
                      model: "qwen3.8-max",  //此处以qwen3.8-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                      messages: [
                          { role: "system", content: "You are a helpful assistant." },
                          { role: "user", content: "你是谁？" }
                      ],
                  });
                  console.log(JSON.stringify(completion))
              }

              main();
              ```

              ```go Go expandable
              package main

              import (
              	"context"
              	"os"

              	"github.com/openai/openai-go"
              	"github.com/openai/openai-go/option"
              )

              func main() {
              	client := openai.NewClient(
              		option.WithAPIKey(os.Getenv("DASHSCOPE_API_KEY")),
              		option.WithBaseURL("https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"),
              	)
              	chatCompletion, err := client.Chat.Completions.New(
              		context.TODO(), openai.ChatCompletionNewParams{
              			Messages: []openai.ChatCompletionMessageParamUnion{
              				openai.UserMessage("你是谁"),
              			},
              			Model: "qwen3.8-max",
              		},
              	)

              	if err != nil {
              		panic(err.Error())
              	}

              	println(chatCompletion.Choices[0].Message.Content)
              }
              ```

              ```csharp C#（HTTP） expandable
              using System.Net.Http.Headers;
              using System.Text;

              class Program
              {
                  private static readonly HttpClient httpClient = new HttpClient();

                  static async Task Main(string[] args)
                  {
                      // 若没有配置环境变量，请用百炼API Key将下行替换为：string? apiKey = "sk-xxx";
                      string? apiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY");

                      if (string.IsNullOrEmpty(apiKey))
                      {
                          Console.WriteLine("API Key 未设置。请确保环境变量 'DASHSCOPE_API_KEY' 已设置。");
                          return;
                      }

                      // 设置请求 URL 和内容
                      string url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions";
                      // 此处以qwen3.8-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                      string jsonContent = @"{
                          ""model"": ""qwen3.8-max"",
                          ""messages"": [
                              {
                                  ""role"": ""system"",
                                  ""content"": ""You are a helpful assistant.""
                              },
                              {
                                  ""role"": ""user"",
                                  ""content"": ""你是谁？""
                              }
                          ]
                      }";

                      // 发送请求并获取响应
                      string result = await SendPostRequestAsync(url, jsonContent, apiKey);

                      // 输出结果
                      Console.WriteLine(result);
                  }

                  private static async Task<string> SendPostRequestAsync(string url, string jsonContent, string apiKey)
                  {
                      using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                      {
                          // 设置请求头
                          httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                          httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                          // 发送请求并获取响应
                          HttpResponseMessage response = await httpClient.PostAsync(url, content);

                          // 处理响应
                          if (response.IsSuccessStatusCode)
                          {
                              return await response.Content.ReadAsStringAsync();
                          }
                          else
                          {
                              return $"请求失败: {response.StatusCode}";
                          }
                      }
                  }
              }
              ```

              ```php PHP（HTTP） expandable
              <?php
              // 设置请求的URL
              $url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions';
              // 若没有配置环境变量，请用百炼API Key将下行替换为：$apiKey = "sk-xxx";
              $apiKey = getenv('DASHSCOPE_API_KEY');
              // 设置请求头
              $headers = [
                  'Authorization: Bearer '.$apiKey,
                  'Content-Type: application/json'
              ];
              // 设置请求体
              $data = [
                  // 此处以qwen3.8-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  "model" => "qwen3.8-max",
                  "messages" => [
                      [
                          "role" => "system",
                          "content" => "You are a helpful assistant."
                      ],
                      [
                          "role" => "user",
                          "content" => "你是谁？"
                      ]
                  ]
              ];
              // 初始化cURL会话
              $ch = curl_init();
              // 设置cURL选项
              curl_setopt($ch, CURLOPT_URL, $url);
              curl_setopt($ch, CURLOPT_POST, true);
              curl_setopt($ch, CURLOPT_POSTFIELDS, json_encode($data));
              curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
              curl_setopt($ch, CURLOPT_HTTPHEADER, $headers);
              // 执行cURL会话
              $response = curl_exec($ch);
              // 检查是否有错误发生
              if (curl_errno($ch)) {
                  echo 'Curl error: ' . curl_error($ch);
              }
              // 关闭cURL资源
              curl_close($ch);
              // 输出响应结果
              echo $response;
              ?>
              ```

              ```bash curl
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                  "model": "qwen3.8-max",
                  "messages": [
                      {
                          "role": "system",
                          "content": "You are a helpful assistant."
                      },
                      {
                          "role": "user",
                          "content": "你是谁？"
                      }
                  ]
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="流式输出">
            > 相关文档：[流式输出](/zh/model-studio/stream)。

            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )
              completion = client.chat.completions.create(
                  model="qwen3.8-max",  # 此处以qwen3.8-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  messages=[{'role': 'system', 'content': 'You are a helpful assistant.'},
                              {'role': 'user', 'content': '你是谁？'}],
                  stream=True,
                  stream_options={"include_usage": True}
                  )
              for chunk in completion:
                  print(chunk.model_dump_json())
              ```

              ```javascript Node.js expandable
              import OpenAI from "openai";

              const openai = new OpenAI(
                  {
                      // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx",
                      apiKey: process.env.DASHSCOPE_API_KEY,
                      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                  }
              );

              async function main() {
                  const completion = await openai.chat.completions.create({
                      model: "qwen3.8-max", // 此处以qwen3.8-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                      messages: [
                          {"role": "system", "content": "You are a helpful assistant."},
                          {"role": "user", "content": "你是谁？"}
                      ],
                      stream: true,
                      stream_options: {include_usage: true}
                  });
                  for await (const chunk of completion) {
                      console.log(JSON.stringify(chunk));
                  }
              }

              main();
              ```

              ```bash curl
              curl --location "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions" \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header "Content-Type: application/json" \
              --data '{
                  "model": "qwen3.8-max",
                  "messages": [
                      {
                          "role": "system",
                          "content": "You are a helpful assistant."
                      },
                      {
                          "role": "user",
                          "content": "你是谁？"
                      }
                  ],
                  "stream":true,
                  "stream_options": {
                      "include_usage": true
                  }
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="图像输入">
            > 相关文档：[图像与视频理解](/zh/model-studio/vision)。

            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )
              completion = client.chat.completions.create(
                  model="qwen3-vl-plus",  # 此处以qwen3-vl-plus为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  messages=[{"role": "user","content": [
                          {"type": "image_url",
                           "image_url": {"url": "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"}},
                          {"type": "text", "text": "这是什么"},
                          ]}]
                  )
              print(completion.model_dump_json())
              ```

              ```javascript Node.js
              import OpenAI from "openai";

              const openai = new OpenAI(
                  {
                      // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx",
                      apiKey: process.env.DASHSCOPE_API_KEY,
                      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                  }
              );

              async function main() {
                  const response = await openai.chat.completions.create({
                      model: "qwen3-vl-plus", // 此处以qwen3-vl-plus为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                      messages: [{role: "user",content: [
                          { type: "image_url",image_url: {"url": "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"}},
                          { type: "text", text: "这是什么？" },
                      ]}]
                  });
                  console.log(JSON.stringify(response));
              }

              main();
              ```

              ```bash curl
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H 'Content-Type: application/json' \
              -d '{
                "model": "qwen3-vl-plus",
                "messages": [{
                    "role": "user",
                    "content": [
                     {"type": "image_url","image_url": {"url": "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"}},
                     {"type": "text","text": "这是什么"}
                     ]}]
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="视频输入">
            > 以下示例展示了如何将图片列表作为视频输入。如需使用视频文件等其他方式，请参阅“[视觉理解](/zh/model-studio/vision#80dbf6ca8fh6s)。

            <CodeGroup dropdown>
              ```python Python expandable
              import os
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )
              completion = client.chat.completions.create(
                  # 此处以qwen3-vl-plus为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  model="qwen3-vl-plus",
                  messages=[{
                      "role": "user",
                      "content": [
                          {
                              "type": "video",
                              "video": [
                                  "https://img.alicdn.com/imgextra/i3/O1CN01K3SgGo1eqmlUgeE9b_!!6000000003923-0-tps-3840-2160.jpg",
                                  "https://img.alicdn.com/imgextra/i4/O1CN01BjZvwg1Y23CF5qIRB_!!6000000003000-0-tps-3840-2160.jpg",
                                  "https://img.alicdn.com/imgextra/i4/O1CN01Ib0clU27vTgBdbVLQ_!!6000000007859-0-tps-3840-2160.jpg",
                                  "https://img.alicdn.com/imgextra/i1/O1CN01aygPLW1s3EXCdSN4X_!!6000000005710-0-tps-3840-2160.jpg"]
                          },
                          {
                              "type": "text",
                              "text": "描述这个视频的具体过程"
                          }]}]
              )
              print(completion.model_dump_json())
              ```

              ```javascript Node.js expandable
              // 确保之前在 package.json 中指定了 "type": "module"
              import OpenAI from "openai";

              const openai = new OpenAI({
                  // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx",
                  apiKey: process.env.DASHSCOPE_API_KEY,
                  baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
              });

              async function main() {
                  const response = await openai.chat.completions.create({
                      // 此处以qwen3-vl-plus为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                      model: "qwen3-vl-plus",
                      messages: [{
                          role: "user",
                          content: [
                              {
                                  type: "video",
                                  video: [
                                      "https://img.alicdn.com/imgextra/i3/O1CN01K3SgGo1eqmlUgeE9b_!!6000000003923-0-tps-3840-2160.jpg",
                                      "https://img.alicdn.com/imgextra/i4/O1CN01BjZvwg1Y23CF5qIRB_!!6000000003000-0-tps-3840-2160.jpg",
                                      "https://img.alicdn.com/imgextra/i4/O1CN01Ib0clU27vTgBdbVLQ_!!6000000007859-0-tps-3840-2160.jpg",
                                      "https://img.alicdn.com/imgextra/i1/O1CN01aygPLW1s3EXCdSN4X_!!6000000005710-0-tps-3840-2160.jpg"
                                  ]
                              },
                              {
                                  type: "text",
                                  text: "描述这个视频的具体过程"
                              }
                      ]}]
                  });
                  console.log(JSON.stringify(response));
              }

              main();
              ```

              ```bash curl expandable
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H 'Content-Type: application/json' \
              -d '{
                  "model": "qwen3-vl-plus",
                  "messages": [
                      {
                          "role": "user",
                          "content": [
                              {
                                  "type": "video",
                                  "video": [
                                      "https://img.alicdn.com/imgextra/i3/O1CN01K3SgGo1eqmlUgeE9b_!!6000000003923-0-tps-3840-2160.jpg",
                                      "https://img.alicdn.com/imgextra/i4/O1CN01BjZvwg1Y23CF5qIRB_!!6000000003000-0-tps-3840-2160.jpg",
                                      "https://img.alicdn.com/imgextra/i4/O1CN01Ib0clU27vTgBdbVLQ_!!6000000007859-0-tps-3840-2160.jpg",
                                      "https://img.alicdn.com/imgextra/i1/O1CN01aygPLW1s3EXCdSN4X_!!6000000005710-0-tps-3840-2160.jpg"
                                  ]
                              },
                              {
                                  "type": "text",
                                  "text": "描述这个视频的具体过程"
                              }
                          ]
                      }
                  ]
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="工具调用">
            > 相关文档：[Function Calling](/zh/model-studio/qwen-function-calling)

            <Note>
              <strong>模型知识时效性</strong>：大模型基于训练数据生成回复，其知识存在截止时间，无法自动感知当前真实日期。询问当前日期时，模型返回的是训练数据截止时间之前的旧日期，这不是参数配置错误。

              获取当前准确时间的方式：

              1. <strong>Function Calling</strong>：定义 `get_current_time` 工具，由模型通过函数调用获取实时时间，见下方工具调用示例。
              2. <strong>系统提示词注入</strong>：在系统消息（system role）中注入当前日期，每次调用需动态传入。
              3. <strong>联网搜索</strong>：`enable_search` 可获取实时资讯，但无法直接获取当前时间。
            </Note>

            <CodeGroup dropdown>
              ```python Python expandable
              import os
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",  # 填写DashScope SDK的base_url
              )

              tools = [
                  # 工具1 获取当前时刻的时间
                  {
                      "type": "function",
                      "function": {
                          "name": "get_current_time",
                          "description": "当你想知道现在的时间时非常有用。",
                          "parameters": {}  # 因为获取当前时间无需输入参数，因此parameters为空字典
                      }
                  },
                  # 工具2 获取指定城市的天气
                  {
                      "type": "function",
                      "function": {
                          "name": "get_current_weather",
                          "description": "当你想查询指定城市的天气时非常有用。",
                          "parameters": {
                              "type": "object",
                              "properties": {
                                  # 查询天气时需要提供位置，因此参数设置为location
                                  "location": {
                                      "type": "string",
                                      "description": "城市或县区，比如北京市、杭州市、余杭区等。"
                                  }
                              },
                              "required": ["location"]
                          }
                      }
                  }
              ]
              messages = [{"role": "user", "content": "杭州天气怎么样"}]
              completion = client.chat.completions.create(
                  model="qwen3.8-max",  # 此处以qwen3.8-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  messages=messages,
                  tools=tools
              )

              print(completion.model_dump_json())
              ```

              ```javascript Node.js expandable
              import OpenAI from "openai";

              const openai = new OpenAI(
                  {
                      // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx",
                      apiKey: process.env.DASHSCOPE_API_KEY,
                      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                  }
              );

              const messages = [{"role": "user", "content": "杭州天气怎么样"}];
              const tools = [
              // 工具1 获取当前时刻的时间
              {
                  "type": "function",
                  "function": {
                      "name": "get_current_time",
                      "description": "当你想知道现在的时间时非常有用。",
                      // 因为获取当前时间无需输入参数，因此parameters为空
                      "parameters": {}
                  }
              },
              // 工具2 获取指定城市的天气
              {
                  "type": "function",
                  "function": {
                      "name": "get_current_weather",
                      "description": "当你想查询指定城市的天气时非常有用。",
                      "parameters": {
                          "type": "object",
                          "properties": {
                              // 查询天气时需要提供位置，因此参数设置为location
                              "location": {
                                  "type": "string",
                                  "description": "城市或县区，比如北京市、杭州市、余杭区等。"
                              }
                          },
                          "required": ["location"]
                      }
                  }
              }
              ];

              async function main() {
                  const response = await openai.chat.completions.create({
                      model: "qwen3.8-max", // 此处以qwen3.8-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                      messages: messages,
                      tools: tools,
                  });
                  console.log(JSON.stringify(response));
              }

              main();
              ```

              ```bash curl expandable
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                  "model": "qwen3.8-max",
                  "messages": [
                      {
                          "role": "system",
                          "content": "You are a helpful assistant."
                      },
                      {
                          "role": "user",
                          "content": "杭州天气怎么样"
                      }
                  ],
                  "tools": [
                  {
                      "type": "function",
                      "function": {
                          "name": "get_current_time",
                          "description": "当你想知道现在的时间时非常有用。",
                          "parameters": {}
                      }
                  },
                  {
                      "type": "function",
                      "function": {
                          "name": "get_current_weather",
                          "description": "当你想查询指定城市的天气时非常有用。",
                          "parameters": {
                              "type": "object",
                              "properties": {
                                  "location":{
                                      "type": "string",
                                      "description": "城市或县区，比如北京市、杭州市、余杭区等。"
                                  }
                              },
                              "required": ["location"]
                          }
                      }
                  }
                ]
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="联网搜索">
            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )
              completion = client.chat.completions.create(
                  model="qwen3.8-max",  # 此处以qwen3.8-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  messages=[
                      {'role': 'system', 'content': 'You are a helpful assistant.'},
                      {'role': 'user', 'content': '中国队在巴黎奥运会获得了多少枚金牌'}],
                  extra_body={
                      "enable_search": True
                  }
                  )
              print(completion.model_dump_json())
              ```

              ```javascript Node.js
              import OpenAI from "openai";

              const openai = new OpenAI(
                  {
                      // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx",
                      apiKey: process.env.DASHSCOPE_API_KEY,
                      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                  }
              );
              async function main() {
                  const completion = await openai.chat.completions.create({
                      model: "qwen3.8-max", //此处以qwen3.8-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                      messages: [
                          { role: "system", content: "You are a helpful assistant." },
                          { role: "user", content: "中国队在巴黎奥运会获得了多少枚金牌" }
                      ],
                      enable_search:true
                  });
                  console.log(JSON.stringify(completion))
              }

              main();
              ```

              ```bash curl
              curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions \
              -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
              -H "Content-Type: application/json" \
              -d '{
                  "model": "qwen3.8-max",
                  "messages": [
                      {
                          "role": "system",
                          "content": "You are a helpful assistant."
                      },
                      {
                          "role": "user",
                          "content": "中国队在巴黎奥运会获得了多少枚金牌"
                      }
                  ],
                  "enable_search": true
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="异步调用">
            ```python
            import os
            import asyncio
            from openai import AsyncOpenAI
            import platform

            client = AsyncOpenAI(
                # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                api_key=os.getenv("DASHSCOPE_API_KEY"),
                base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
            )

            async def main():
                response = await client.chat.completions.create(
                    messages=[{"role": "user", "content": "你是谁"}],
                    model="qwen3.8-max",  # 此处以qwen3.8-max为例，可按需更换模型名称。模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                )
                print(response.model_dump_json())

            if platform.system() == "Windows":
                asyncio.set_event_loop_policy(asyncio.WindowsSelectorEventLoopPolicy())
            asyncio.run(main())
            ```
          </Tab>

          <Tab title="文档理解">
            > 当前仅qwen-long模型支持对文档进行分析，详细用法请参见[长上下文（Qwen-Long）](/zh/model-studio/long-context-qwen-long)。

            <CodeGroup dropdown>
              ```python Python
              import os
              from pathlib import Path
              from openai import OpenAI

              client = OpenAI(
                  # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )
              file_object = client.files.create(file=Path("百炼系列手机产品介绍.docx"), purpose="file-extract")
              completion = client.chat.completions.create(
                  model="qwen-long",  # 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                  messages=[
                      {'role': 'system', 'content': f'fileid://{file_object.id}'},
                      {'role': 'user', 'content': '这篇文章讲了什么？'}
                  ]
              )
              print(completion.model_dump_json())
              ```

              ```java Java expandable
              // 建议OpenAI SDK的版本 >= 0.32.0
              import com.openai.client.OpenAIClient;
              import com.openai.client.okhttp.OpenAIOkHttpClient;
              import com.openai.models.chat.completions.ChatCompletion;
              import com.openai.models.chat.completions.ChatCompletionCreateParams;
              import com.openai.models.files.FileCreateParams;
              import com.openai.models.files.FileObject;
              import com.openai.models.files.FilePurpose;

              import java.nio.file.Path;
              import java.nio.file.Paths;

              public class Main {
                  public static void main(String[] args) {
                      // 创建客户端，使用环境变量中的API密钥
                      OpenAIClient client = OpenAIOkHttpClient.builder()
                              .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                              .baseUrl("https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1")
                              .build();

                      // 设置文件路径
                      Path filePath = Paths.get("百炼系列手机产品介绍.docx");
                      // 创建文件上传参数
                      FileCreateParams fileParams = FileCreateParams.builder()
                              .file(filePath)
                              .purpose(FilePurpose.of("file-extract"))
                              .build();

                      // 上传文件
                      FileObject fileObject = client.files().create(fileParams);
                      String fileId = fileObject.id();

                      // 创建聊天请求
                      ChatCompletionCreateParams chatParams = ChatCompletionCreateParams.builder()
                              .addSystemMessage("fileid://" + fileId)
                              .addUserMessage("这篇文章讲了什么？")
                              .model("qwen-long")
                              .build();

                      // 发送请求并获取响应
                      ChatCompletion chatCompletion = client.chat().completions().create(chatParams);

                      // 打印响应结果
                      System.out.println(chatCompletion);
                  }
              }
              ```

              ```javascript Node.js expandable
              import fs from "fs";
              import OpenAI from "openai";

              const openai = new OpenAI(
                  {
                      // 若没有配置环境变量，请用百炼API Key将下行替换为：apiKey: "sk-xxx",
                      apiKey: process.env.DASHSCOPE_API_KEY,
                      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                  }
              );

              async function getFileID() {
                  const fileObject = await openai.files.create({
                      file: fs.createReadStream("百炼系列手机产品介绍.docx"),
                      purpose: "file-extract"
                  });
                  return fileObject.id;
              }

              async function main() {
                  const fileID = await getFileID();
                  const completion = await openai.chat.completions.create({
                      model: "qwen-long",  //模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
                      messages: [
                          { role: "system", content: `fileid://${fileID}`},
                          { role: "user", content: "这篇文章讲了什么？" }
                      ],
                  });
                  console.log(JSON.stringify(completion))
              }

              main();
              ```

              ```bash curl
              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header "Content-Type: application/json" \
              --data '{
                  "model": "qwen-long",
                  "messages": [
                      {"role": "system","content": "You are a helpful assistant."},
                      {"role": "system","content": "fileid://file-fe-xxx"},
                      {"role": "user","content": "这篇文章讲了什么？"}
                  ],
                  "stream": true,
                  "stream_options": {
                      "include_usage": true
                  }
              }'
              ```
            </CodeGroup>
          </Tab>

          <Tab title="PPT生成">
            > 当前仅`qwen-doc-turbo`模型支持PPT生成。详细用法请参见[生成PPT](/zh/model-studio/data-mining-qwen-doc#f6a7b8c9d0pp1)。

            <CodeGroup dropdown>
              ```python Python
              import os
              from openai import OpenAI

              client = OpenAI(
                  api_key=os.getenv("DASHSCOPE_API_KEY"),
                  base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1",
              )

              completion = client.chat.completions.create(
                  model="qwen-doc-turbo",
                  messages=[
                      {"role": "system", "content": "you are a helpful assistant."},
                      {"role": "system", "content": "您的文档内容"},
                      {"role": "user", "content": "生成一个10到20页的ppt"}
                  ],
                  extra_body={"skill": [{"type": "ppt", "mode": "general", "template_id": "news_01"}]},
                  stream=True,
                  stream_options={"include_usage": True}
              )

              for chunk in completion:
                  if chunk.choices and chunk.choices[0].delta.content:
                      print(chunk.choices[0].delta.content, end='', flush=True)
              ```

              ```javascript Node.js expandable
              import OpenAI from "openai";

              const openai = new OpenAI(
                  {
                      apiKey: process.env.DASHSCOPE_API_KEY,
                      baseURL: "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1"
                  }
              );

              async function main() {
                  const completion = await openai.chat.completions.create({
                      model: "qwen-doc-turbo",
                      messages: [
                          {"role": "system", "content": "you are a helpful assistant."},
                          {"role": "system", "content": "您的文档内容"},
                          {"role": "user", "content": "生成一个10到20页的ppt"}
                      ],
                      skill: [{"type": "ppt", "mode": "general", "template_id": "news_01"}],
                      stream: true,
                      stream_options: {"include_usage": true}
                  });
                  for await (const chunk of completion) {
                      if (chunk.choices?.length > 0 && chunk.choices[0].delta.content) {
                          process.stdout.write(chunk.choices[0].delta.content);
                      }
                  }
              }

              main();
              ```

              ```bash curl expandable
              curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1/chat/completions' \
              --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
              --header "Content-Type: application/json" \
              --data '{
                  "model": "qwen-doc-turbo",
                  "messages": [
                      {
                          "role": "system",
                          "content": "you are a helpful assistant."
                      },
                      {
                          "role": "system",
                          "content": "您的文档内容"
                      },
                      {
                          "role": "user",
                          "content": "生成一个10到20页的ppt"
                      }
                  ],
                  "skill": [
                      {
                          "type": "ppt",
                          "mode": "general",
                          "template_id": "news_01"
                      }
                  ],
                  "stream": true,
                  "stream_options": {
                      "include_usage": true
                  }
              }'
              ```
            </CodeGroup>
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## chat响应对象（非流式输出） <span id="cd3063363egdc" />

        <strong>id</strong>`string`

        本次调用的唯一标识符。

        <strong>choices</strong>`array`

        模型生成内容的数组。

        <Accordion title="属性">
          <strong>finish\_reason</strong>`string`

          模型停止生成的原因。

          有三种情况：

          - 触发输入参数中的`stop`参数，或自然停止输出时为`stop`；
          - 生成长度过长而结束为`length`；
          - 需要调用工具而结束为`tool_calls`。

          <strong>index</strong>`integer`

          当前对象在`choices`数组中的索引。

          <strong>logprobs</strong>`object`

          模型输出的 Token 概率信息。

          <Accordion title="属性">
            <strong>content</strong> `array`

            包含每个 Token 及其对数概率的数组。

            <Accordion title="属性">
              <strong>token</strong> `string`

              当前 Token 的文本。

              <strong>bytes</strong> `array`

              当前 Token 的 UTF‑8 原始字节列表，用于精确还原输出内容（例如表情符号或中文字符）。

              <strong>logprob</strong> `float`

              当前 Token 的对数概率。返回值为 `null` 表示概率值极低。

              <strong>top\_logprobs</strong> `array`

              当前 Token 位置最可能的若干候选 Token，数量与请求参数`top_logprobs`保持一致。每个元素包含：

              <Accordion title="属性">
                <strong>token</strong> `string`

                候选 Token 文本。

                <strong>bytes</strong> `array`

                当前 Token 的 UTF‑8 原始字节列表，用于精确还原输出内容（例如表情符号或中文字符）。

                <strong>logprob</strong> `float`

                该候选 Token 的对数概率。返回值为 null 表示概率值极低。
              </Accordion>
            </Accordion>
          </Accordion>

          <strong>message</strong>`object`

          模型输出的消息。

          <Accordion title="属性">
            <strong>content</strong> `string`

            模型的回复内容。

            <strong>reasoning\_content</strong> `string`

            模型的思维链内容。

            <strong>refusal</strong> `string`

            该参数当前固定为`null`。

            <strong>role</strong> `string`

            消息的角色，固定为`assistant`。

            <strong>audio</strong> `object`

            该参数当前固定为`null`。

            <strong>function\_call</strong>（即将废弃）`object`

            该值固定为`null`，请参考`tool_calls`参数。

            <strong>tool\_calls</strong> `array`

            在发起 Function Calling后，模型生成的工具与入参信息。

            <Accordion title="属性">
              <strong>id</strong> `string`

              本次工具响应的唯一标识符。

              <strong>type</strong> `string`

              工具类型，当前只支持`function`。

              <strong>function</strong> `object`

              工具信息。

              <Accordion title="属性">
                <strong>name</strong> `string`

                工具名称。

                <strong>arguments</strong> `string`

                入参信息，为JSON格式字符串。

                > 由于大模型响应有一定随机性，输出的入参信息可能不符合函数签名。请在调用前校验参数有效性
              </Accordion>

              <strong>index</strong> `integer`

              当前工具在`tool_calls`数组中的索引。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>created</strong>`integer`

        请求创建时的 Unix 时间戳（秒）。

        <strong>model</strong>`string`

        本次请求使用的模型。

        <strong>object</strong> `string`

        始终为`chat.completion`。

        <strong>service\_tier</strong> `string`

        该参数当前固定为`null`。

        <strong>system\_fingerprint</strong>`string`

        该参数当前固定为`null`。

        <strong>usage</strong> `object`

        本次请求的 Token 消耗信息。

        <Accordion title="属性">
          <strong>completion\_tokens</strong> `integer`

          模型输出的 Token 数。

          <strong>prompt\_tokens</strong> `integer`

          输入的 Token 数。[补充说明](/zh/model-studio/text-generation#e710782c79xqy)

          <strong>total\_tokens</strong> `integer`

          消耗的总 Token 数，为`prompt_tokens`与`completion_tokens`的总和。

          <strong>completion\_tokens\_details</strong> `object`（可选）

          输出 Token 的细粒度分类。部分模型返回该字段。其中 `text_tokens` 包含 `reasoning_tokens`——`reasoning_tokens` 是 `text_tokens` 中对应思考过程的子集，`text_tokens` 减去 `reasoning_tokens` 才是最终可见回复的 Token 数。该字段通常仅在模型输出思考过程时返回，未开启思考时可能为 `null` 或不返回。

          <Accordion title="属性">
            <strong>audio\_tokens</strong> `integer`（可选）

            输出的音频 Token 数。

            <strong>reasoning\_tokens</strong> `integer`（可选）

            思考过程 Token 数，是 `text_tokens` 的子集。

            <strong>text\_tokens</strong> `integer`（可选）

            输出文本的 Token 数，已包含 `reasoning_tokens`。
          </Accordion>

          <strong>prompt\_tokens\_details</strong> `object`

          输入 Token 的细粒度分类。

          <Accordion title="属性">
            <strong>audio\_tokens</strong> `integer`

            该参数当前固定为`null`。

            <strong>cached\_tokens</strong> `integer`

            命中 Cache 的 Token 数。Context Cache 详情请参见[上下文缓存](/zh/model-studio/context-cache)。

            <strong>text\_tokens</strong> `integer`

            输入的文本 Token 数。

            <strong>image\_tokens</strong> `integer`

            输入的图像 Token 数。

            <strong>video\_tokens</strong> `integer`

            输入的视频文件或者图像列表 Token 数。

            <strong>cache\_creation</strong> `object`

            [显式缓存](/zh/model-studio/context-cache#825f201c5fy6o)创建信息。

            <Accordion title="属性">
              <strong>ephemeral\_5m\_input\_tokens</strong> `integer`

              创建显式缓存的 Token 数。
            </Accordion>

            <strong>cache\_creation\_input\_tokens</strong> `integer`

            创建显式缓存的 Token 数。

            <strong>cache\_type</strong> `string`

            使用[显式缓存](/zh/model-studio/context-cache#825f201c5fy6o)时，参数值为`ephemeral`，否则该参数不存在。
          </Accordion>

          <strong>x\_tools</strong> `object`（可选）

          联网搜索的调用统计。仅当请求中`include_tool_usage`为`true`，且本次请求实际触发了联网搜索时返回。

          <Accordion title="属性">
            <strong>search</strong> `object`（可选）

            联网搜索的调用统计。开启[联网搜索](/zh/model-studio/web-search)并实际触发检索时返回。

            <Accordion title="属性">
              <strong>count</strong> `integer`

              本次请求中联网搜索的触发次数。使用`agent`搜索策略时，模型可进行多轮检索，该值可能大于 1。

              该值在意图判定阶段即被置位，即使触发限流或检索未返回结果也会计数，不代表检索已实际完成，不作为计费或对账依据。详情请参见[账单次数与 count 字段不一致](/zh/model-studio/web-search#faq-search-billing-count-mismatch-title)。

              <strong>strategy</strong> `string`

              本次联网搜索实际使用的搜索量级策略，与请求中`search_options.search_strategy`的取值一致，如`turbo`、`max`、`agent`。
            </Accordion>
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <Tabs>
          <Tab title="基础响应">
            ```json expandable
            {
                "choices": [
                    {
                        "message": {
                            "role": "assistant",
                            "content": "我是阿里云开发的一款超大规模语言模型，我叫千问。"
                        },
                        "finish_reason": "stop",
                        "index": 0,
                        "logprobs": null
                    }
                ],
                "object": "chat.completion",
                "usage": {
                    "prompt_tokens": 3019,
                    "completion_tokens": 104,
                    "total_tokens": 3123,
                    "prompt_tokens_details": {
                        "cached_tokens": 2048
                    }
                },
                "created": 1735120033,
                "system_fingerprint": null,
                "model": "qwen3.8-max",
                "id": "chatcmpl-6ada9ed2-7f33-9de2-8bb0-78bd4035025a"
            }
            ```
          </Tab>

          <Tab title="联网搜索响应">
            设置`include_tool_usage`为`true`并实际触发联网搜索时，`usage`中会增加`x_tools`字段：

            ```json expandable
            {
                "id": "chatcmpl-4233d020-62cd-949f-94e2-444b97503856",
                "object": "chat.completion",
                "created": 1792037298,
                "model": "qwen3.8-max",
                "choices": [
                    {
                        "index": 0,
                        "message": {
                            "role": "assistant",
                            "content": "杭州亚运会（第19届亚洲运动会）是在2023年举办的，具体举办时间为2023年9月23日至10月8日。"
                        },
                        "finish_reason": "stop",
                        "logprobs": null
                    }
                ],
                "usage": {
                    "prompt_tokens": 1905,
                    "total_tokens": 1987,
                    "completion_tokens": 82,
                    "prompt_tokens_details": {
                        "cached_tokens": 0,
                        "text_tokens": 1905
                    },
                    "x_tools": {
                        "search": {
                            "count": 1,
                            "strategy": "turbo"
                        }
                    }
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ## chat响应chunk对象（流式输出） <span id="e98e6b7aa5nb2" />

        <strong>id</strong>`string`

        本次调用的唯一标识符。每个chunk对象有相同的 id。

        <strong>choices</strong>`array`

        模型生成内容的数组，可包含一个或多个对象。若设置`include_usage`参数为`true`，则`choices`在最后一个chunk中为空数组。

        <Accordion title="属性">
          <strong>delta</strong> `object`

          请求的增量对象。

          <Accordion title="属性">
            <strong>content</strong> `string`

            增量消息内容。

            <strong>reasoning\_content</strong> `string`

            增量思维链内容。

            <strong>function\_call</strong> `object`

            该值默认为`null`，请参考`tool_calls`参数。

            <strong>audio</strong>`object`

            使用 [Qwen-Omni](/zh/model-studio/qwen-omni) 模型时生成的回复。

            <Accordion title="属性">
              <strong>data</strong> `string`

              增量的 Base64 音频编码数据。

              <strong>expires\_at</strong> `integer`

              创建请求时的时间戳。
            </Accordion>

            <strong>refusal</strong> `object`

            该参数当前固定为`null`。

            <strong>role</strong> `string`

            增量消息对象的角色，只在第一个chunk中有值。

            <strong>tool\_calls</strong> `array`

            在发起 Function Calling后，模型生成的工具与入参信息。

            <Accordion title="属性">
              <strong>index</strong> `integer`

              当前工具在`tool_calls`数组中的索引。

              <strong>id</strong> `string`

              本次工具响应的唯一标识符。

              <strong>function</strong> `object`

              被调用的工具信息。

              <Accordion title="属性">
                <strong>arguments</strong> `string`

                增量的入参信息，所有chunk的`arguments`拼接后为完整的入参。

                > 由于大模型响应有一定随机性，输出的入参信息可能不符合函数签名。请在调用前校验参数有效性。

                <strong>name</strong> `string`

                工具名称，只在第一个chunk中有值。
              </Accordion>

              <strong>type</strong> `string`

              工具类型，当前只支持`function`。
            </Accordion>
          </Accordion>

          <strong>finish\_reason</strong> `string`

          模型停止生成的原因。有四种情况：

          - 因触发输入参数中的`stop`参数，或自然停止输出时为`stop`；
          - 生成未结束时为`null`；
          - 生成长度过长而结束为`length`；
          - 需要调用工具而结束为`tool_calls`。

          <strong>index</strong> `integer`

          当前响应在`choices`数组中的索引。当输入参数 n 大于1时，需根据本参数进行不同响应对应的完整内容的拼接。

          <strong>logprobs</strong>`object`

          当前对象的概率信息。

          <Accordion title="属性">
            <strong>content</strong> `array`

            带有对数概率信息的 Token 数组。

            <Accordion title="属性">
              <strong>token</strong> `string`

              当前 Token。

              <strong>bytes</strong> `array`

              当前 Token 的 UTF‑8 原始字节列表，用于精确还原输出内容，在处理表情符号、中文字符时有帮助。

              <strong>logprob</strong> `float`

              当前 Token 的对数概率。返回值为 null 表示概率值极低。

              <strong>top\_logprobs</strong> `array`

              当前 Token 位置最可能的若干个 Token 及其对数概率，元素个数与入参的`top_logprobs`保持一致。

              <Accordion title="属性">
                <strong>token</strong> `string`

                当前 Token。

                <strong>bytes</strong> `array`

                当前 Token 的 UTF‑8 原始字节列表，用于精确还原输出内容，在处理表情符号、中文字符时有帮助。

                <strong>logprob</strong> `float`

                当前 Token 的对数概率。返回值为 null 表示概率值极低。
              </Accordion>
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>created</strong>`integer`

        本次请求被创建时的时间戳。每个chunk有相同的时间戳。

        <strong>model</strong>`string`

        本次请求使用的模型。

        <strong>object</strong> `string`

        始终为`chat.completion.chunk`。

        <strong>service\_tier</strong> `string`

        该参数当前固定为`null`。

        <strong>system\_fingerprint</strong>`string`

        该参数当前固定为`null`。

        <strong>usage</strong> `object`

        本次请求消耗的Token。只在`include_usage`为`true`时，在最后一个chunk显示。

        <Accordion title="属性">
          <strong>completion\_tokens</strong> `integer`

          模型输出的 Token 数。

          <strong>prompt\_tokens</strong> `integer`

          输入 Token 数。

          <strong>total\_tokens</strong> `integer`

          总 Token 数，为`prompt_tokens`与`completion_tokens`的总和。

          <strong>completion\_tokens\_details</strong> `object`（可选）

          输出 Token 的详细信息。部分模型返回该字段。其中 `text_tokens` 包含 `reasoning_tokens`——`reasoning_tokens` 是 `text_tokens` 中对应思考过程的子集，`text_tokens` 减去 `reasoning_tokens` 才是最终可见回复的 Token 数。该字段通常仅在模型输出思考过程时返回，未开启思考时可能为 `null` 或不返回。

          <Accordion title="属性">
            <strong>audio\_tokens</strong>`integer`（可选）

            输出的音频 Token 数。

            <strong>reasoning\_tokens</strong> `integer`（可选）

            思考过程 Token 数，是 `text_tokens` 的子集。

            <strong>text\_tokens</strong>`integer`（可选）

            输出文本 Token 数，已包含 `reasoning_tokens`。
          </Accordion>

          <strong>prompt\_tokens\_details</strong> `object`

          输入 Token的细粒度分类。

          <Accordion title="属性">
            <strong>audio\_tokens</strong> `integer`

            输入音频的 Token 数。

            > 视频文件中的音频 Token 数通过本参数返回。

            <strong>text\_tokens</strong> `integer`

            输入文本的 Token 数。

            <strong>video\_tokens</strong> `integer`

            输入视频（图片列表形式或视频文件）的 Token 数。

            <strong>image\_tokens</strong> `integer`

            输入图片的 Token 数。

            <strong>cached\_tokens</strong> `integer`

            命中缓存的 Token 数。Context Cache 详情请参见[上下文缓存](/zh/model-studio/context-cache)。

            <strong>cache\_creation</strong> `object`

            [显式缓存](/zh/model-studio/context-cache#825f201c5fy6o)创建信息。

            <Accordion title="属性">
              <strong>ephemeral\_5m\_input\_tokens</strong> `integer`

              创建显式缓存的 Token 数。
            </Accordion>

            <strong>cache\_creation\_input\_tokens</strong> `integer`

            创建显式缓存的 Token 数。

            <strong>cache\_type</strong> `string`

            缓存类型，固定为`ephemeral`。
          </Accordion>

          <strong>x\_tools</strong> `object`（可选）

          联网搜索的调用统计。仅当请求中`include_tool_usage`为`true`，且本次请求实际触发了联网搜索时返回，随`usage`在最后一个 chunk 中显示。

          <Accordion title="属性">
            <strong>search</strong> `object`（可选）

            联网搜索的调用统计。开启[联网搜索](/zh/model-studio/web-search)并实际触发检索时返回。

            <Accordion title="属性">
              <strong>count</strong> `integer`

              本次请求中联网搜索的触发次数。使用`agent`搜索策略时，模型可进行多轮检索，该值可能大于 1。

              该值在意图判定阶段即被置位，即使触发限流或检索未返回结果也会计数，不代表检索已实际完成，不作为计费或对账依据。详情请参见[账单次数与 count 字段不一致](/zh/model-studio/web-search#faq-search-billing-count-mismatch-title)。

              <strong>strategy</strong> `string`

              本次联网搜索实际使用的搜索量级策略，与请求中`search_options.search_strategy`的取值一致，如`turbo`、`max`、`agent`。
            </Accordion>
          </Accordion>
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        ```json
        {"id":"chatcmpl-e30f5ae7-3063-93c4-90fe-beb5f900bd57","choices":[{"delta":{"content":"","function_call":null,"refusal":null,"role":"assistant","tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1735113344,"model":"qwen3.8-max","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-e30f5ae7-3063-93c4-90fe-beb5f900bd57","choices":[{"delta":{"content":"我是","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1735113344,"model":"qwen3.8-max","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-e30f5ae7-3063-93c4-90fe-beb5f900bd57","choices":[{"delta":{"content":"来自","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1735113344,"model":"qwen3.8-max","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-e30f5ae7-3063-93c4-90fe-beb5f900bd57","choices":[{"delta":{"content":"阿里","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1735113344,"model":"qwen3.8-max","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-e30f5ae7-3063-93c4-90fe-beb5f900bd57","choices":[{"delta":{"content":"云的超大规模","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1735113344,"model":"qwen3.8-max","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-e30f5ae7-3063-93c4-90fe-beb5f900bd57","choices":[{"delta":{"content":"语言模型，我","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1735113344,"model":"qwen3.8-max","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-e30f5ae7-3063-93c4-90fe-beb5f900bd57","choices":[{"delta":{"content":"叫千问千","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1735113344,"model":"qwen3.8-max","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-e30f5ae7-3063-93c4-90fe-beb5f900bd57","choices":[{"delta":{"content":"问。","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":null,"index":0,"logprobs":null}],"created":1735113344,"model":"qwen3.8-max","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-e30f5ae7-3063-93c4-90fe-beb5f900bd57","choices":[{"delta":{"content":"","function_call":null,"refusal":null,"role":null,"tool_calls":null},"finish_reason":"stop","index":0,"logprobs":null}],"created":1735113344,"model":"qwen3.8-max","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":null}
        {"id":"chatcmpl-e30f5ae7-3063-93c4-90fe-beb5f900bd57","choices":[],"created":1735113344,"model":"qwen3.8-max","object":"chat.completion.chunk","service_tier":null,"system_fingerprint":null,"usage":{"completion_tokens":17,"prompt_tokens":22,"total_tokens":39,"completion_tokens_details":null,"prompt_tokens_details":{"audio_tokens":null,"cached_tokens":0}}}
        ```

        设置`include_tool_usage`为`true`并实际触发联网搜索时，最后一个 chunk 的`usage`中会增加`x_tools`字段：

        ```json
        {"id":"chatcmpl-6076672d-ea69-9497-86bc-a5a9c9354662","object":"chat.completion.chunk","created":1792037303,"model":"qwen3.8-max","choices":[],"usage":{"prompt_tokens":1905,"total_tokens":1984,"completion_tokens":79,"prompt_tokens_details":{"cached_tokens":0,"text_tokens":1905},"x_tools":{"search":{"count":1,"strategy":"turbo"}}}}
        ```
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="occ-errcode-h2" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

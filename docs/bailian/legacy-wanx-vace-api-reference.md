> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-视频编辑API参考（2.1）

> 万相2.1-视频编辑统一模型支持文本、图像、视频等多种模态输入，可执行多种视频生成与编辑任务。

<meta property="og:title" content="万相-视频编辑API参考（2.1）" />

<strong>相关文档</strong>：[使用指南](/zh/model-studio/wan-vace-guide)

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证模型、Endpoint URL 和 API Key 均属于<strong>同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](/zh/model-studio/wan-vace-guide#scope-title)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

<Note>
  本文的示例代码适用于<strong>北京地域</strong>。
</Note>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## HTTP调用 <span id="42703589880ts" />

视频编辑统一模型的处理耗时较长（约5-10分钟），API采用异步调用的方式。整个流程包含 <strong>“创建任务 -> 轮询获取”</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="db8cf70432oe3" />

<Tabs>
  <Tab title="北京">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
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

        <Tabs>
          <Tab title="多图参考">
            <strong>model</strong> `string` <strong>（必选）</strong>

            模型名称。示例值：wanx2.1-vace-plus。

            <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
              <colgroup>
                <col style={{ width: "100%" }} />
              </colgroup>

              <tbody>
                <tr>
                  <td>
                    <strong>input</strong> `object` <strong>（必选）</strong>

                    输入的基本信息，如提示词等。

                    <Accordion title="属性" defaultOpen>
                      <strong>prompt</strong>`string`<strong>（必选）</strong>

                      提示词，用来描述生成视频中期望包含的元素和视觉特点。

                      支持中英文，长度不超过800个字符，每个汉字/字母占一个字符，超过部分会自动截断。

                      提示词技巧请参见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

                      <strong>function</strong>`string`<strong>（必选）</strong>

                      功能名称。多图参考设置为`image_reference`。

                      多图参考支持最多3张参考图。图像内容可以包括主体与背景，例如人物、动物、服饰、场景等。使用 `prompt` 描述期望生成的视频画面内容，模型可将多张图片融合生成连贯的视频内容。

                      <strong>ref\_images\_url</strong>`array[string]` <strong>（必选）</strong>

                      输入参考图像的URL 数组。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      参考图像支持<strong>1-3 张</strong>图像，若超过 3 张，则仅保留前 3 张作为输入。

                      图像限制：

                      - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP。
                      - 图像分辨率：图像的宽和高范围在\[360, 2000]，单位像素。
                      - 图像大小：不超过10MB。
                      - URL地址中不能包含中文字符。

                      使用建议：

                      - 若使用参考图像中的主体时，建议每张图像中仅包含一个主体。背景建议为纯色（如白色或单一颜色），以便更好地突出主体。
                      - 若使用参考图像的背景时，背景图像最多只有一张，且背景图像中不包含任何主体对象。
                    </Accordion>
                  </td>
                </tr>

                <tr>
                  <td>
                    <strong>parameters</strong> `object` （可选）

                    视频处理参数，如设置水印等。

                    <Accordion title="属性" defaultOpen>
                      <strong>obj\_or\_bg</strong> `array[string]` （可选）

                      该参数用于标识每张参考图像的用途，与 `ref_images_url` 参数一一对应。数组中每个元素表示对应位置的图像为“主体”还是“背景”：

                      - `obj`：表示该图像作为主体参考。
                      - `bg`：表示该图像作为背景参考 （最多仅允许一个）。

                      使用说明：

                      - 建议传入该参数，且长度必须与 `ref_images_url`保持一致，否则将报错。
                      - 仅当 `ref_images_url`为单元素数组时，可不传，此时默认值为 `["obj"]`。

                      示例值： \["obj", "obj", "bg"]。

                      <strong>size</strong> `string` （可选）

                      生成视频的分辨率（宽\*高）。目前支持生成720P 视频，分辨率的取值为：

                      - `1280*720`（默认值）：视频宽高比为16:9。其中，1280代表宽度，720代表高度。
                      - `720*1280`：视频宽高比为9:16。
                      - `960*960`：视频宽高比为1:1。
                      - `832*1088`：视频宽高比为3:4。
                      - `1088*832`：视频宽高比为4:3。

                      <strong>duration</strong> `integer` （可选）

                      视频生成时长，单位为秒。当前参数值固定为5，且不支持修改。模型将始终生成5秒时长的视频。

                      <strong>prompt\_extend</strong>`bool` （可选）

                      是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写。对于较短的prompt生成效果提升明显，但会增加耗时。

                      - `true`（默认值）：开启智能改写。
                      - `false`：关闭智能改写。

                      <strong>seed</strong>`integer`（可选）

                      随机数种子，用于控制模型生成内容的随机性。seed参数取值范围是`[0, 2147483647]`。

                      如果不提供，则算法自动生成一个随机数作为种子。如果您希望生成内容保持相对稳定，请使用相同的seed参数值。

                      <strong>watermark</strong> `bool`（可选）

                      是否添加水印标识，水印位于图片右下角，文案为“AI生成”。

                      - `false`（默认值）：不添加水印。
                      - `true`：添加水印。
                    </Accordion>
                  </td>
                </tr>
              </tbody>
            </table>
          </Tab>

          <Tab title="视频重绘">
            <strong>model</strong> `string` <strong>（必选）</strong>

            模型名称。示例值：wanx2.1-vace-plus。

            <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
              <colgroup>
                <col style={{ width: "100%" }} />
              </colgroup>

              <tbody>
                <tr>
                  <td>
                    <strong>input</strong> `object` <strong>（必选）</strong>

                    输入的基本信息，如提示词等。

                    <Accordion title="属性" defaultOpen>
                      <strong>prompt</strong>`string`<strong>（必选）</strong>

                      提示词，用来描述生成视频中期望包含的元素和视觉特点。

                      支持中英文，长度不超过800个字符，每个汉字/字母占一个字符，超过部分会自动截断。

                      提示词技巧请参见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

                      <strong>function</strong>`string`<strong>（必选）</strong>

                      功能名称。视频重绘设置为 `video_repainting`。

                      视频重绘支持从输入视频中提取主体姿态与动作、构图与运动轮廓以及线稿结构，结合文本提示词（prompt），生成具有相同动态特征的新视频。同时，还支持通过参考图像替换原视频中的主体，例如更换角色形象但仍保留原有动作。

                      <strong>video\_url</strong>`string` <strong>（必选）</strong>

                      输入视频的URL地址。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      视频限制：

                      - 视频格式：MP4。
                      - 视频帧率：大于等于16FPS。
                      - 视频大小：不超过50MB。
                      - 视频长度：不超过5秒，否则取视频的前5秒。
                      - URL地址中不能包含中文字符。

                      关于输出视频的分辨率：

                      - 若输入视频分辨率 ≤ 720P，输出将保留原始分辨率；
                      - 若输入视频分辨率 > 720P，则在保持原视频宽高比的前提下，按比例缩放至不超过 720P。

                      关于输出视频的时长：

                      - 输出视频时长与输入视频一致，最长不超过 5 秒。
                      - 示例：若输入视频为 3 秒，则输出也为 3 秒；若输入为 6 秒，则输出为前 5 秒。

                      <strong>ref\_images\_url</strong>`array[string]` （可选）

                      输入参考图像的 URL数组。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      仅支持传入<strong>1张</strong>参考图像，且该图像建议为主体图像，用于替换输入视频中的主体内容。

                      图像限制：

                      - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP。
                      - 图像分辨率：图像的宽度和高度范围为\[360, 2000]，单位为像素。
                      - 图像大小：不超过10MB。
                      - URL地址中不能包含中文字符。

                      使用建议：

                      - 若使用参考图像中的主体时，建议每张图像仅包含一个主体。背景建议为纯色（如白色或单一颜色），以便更好地突出主体。
                    </Accordion>
                  </td>
                </tr>

                <tr>
                  <td>
                    <strong>parameters</strong> `object` <strong>（必选）</strong>

                    视频处理参数，如设置水印等。

                    <Accordion title="属性" defaultOpen>
                      <strong>control\_condition</strong> `string` <strong>（必选）</strong>

                      设置视频特征提取的方式。

                      - `posebodyface`：提取输入视频中主体的脸部表情和肢体动作，适用于需保留主体表情细节的场景。
                      - `posebody`：提取输入视频中主体的肢体动作（不含脸部表情），适用于只需要控制主体身体动作的场景。
                      - `depth`：提取输入视频的构图和运动轮廓。
                      - `scribble`：提取输入视频的线稿结构。

                      <strong>strength</strong> `float` （可选）

                      调节 `control_condition` 所指定的视频特征提取方式对生成视频的控制强度。

                      默认值为1.0，取值范围\[0.0, 1.0]。

                      数值越大，生成视频越贴近原视频动作和构图；数值越小，生成内容越自由。

                      <strong>prompt\_extend</strong>`bool` （可选）

                      是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写。对于较短的prompt生成效果提升明显，但会增加耗时。

                      - `true`（默认值）：开启智能改写。
                      - `false`：关闭智能改写。<strong>（推荐）</strong>

                      > 当文本描述与输入的视频内容不一致时，模型可能产生误解。建议手动关闭智能扩写，并在 `prompt` 中提供清晰、具体的画面描述，以提升生成一致性与准确性。

                      <strong>seed</strong>`integer`（可选）

                      随机数种子，用于控制模型生成内容的随机性。seed参数取值范围是`[0, 2147483647]`。

                      如果不提供，则算法自动生成一个随机数作为种子。如果您希望生成内容保持相对稳定，请使用相同的seed参数值。

                      <strong>watermark</strong> `bool`（可选）

                      是否添加水印标识，水印位于图片右下角，文案为“AI生成”。

                      - `false`（默认值）：不添加水印。
                      - `true`：添加水印。
                    </Accordion>
                  </td>
                </tr>
              </tbody>
            </table>
          </Tab>

          <Tab title="局部编辑">
            <strong>model</strong> `string` <strong>（必选）</strong>

            模型名称。示例值：wanx2.1-vace-plus。

            <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
              <colgroup>
                <col style={{ width: "100%" }} />
              </colgroup>

              <tbody>
                <tr>
                  <td>
                    <strong>input</strong> `object` <strong>（必选）</strong>

                    输入的基本信息，如提示词等。

                    <Accordion title="属性" defaultOpen>
                      <strong>prompt</strong>`string`<strong>（必选）</strong>

                      提示词，用来描述生成视频中期望包含的元素和视觉特点。

                      支持中英文，长度不超过800个字符，每个汉字/字母占一个字符，超过部分会自动截断。

                      提示词技巧请参见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

                      <strong>function</strong>`string`<strong>（必选）</strong>

                      功能名称。局部编辑设置为 `video_edit`。

                      局部编辑支持对输入视频的指定区域进行增加、修改或删除元素，还可以对编辑区域的主体或背景进行替换，实现精细化的视频编辑。

                      <strong>video\_url</strong>`string` <strong>（必选）</strong>

                      输入视频的URL地址。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      视频限制：

                      - 视频格式：MP4。
                      - 视频帧率：大于等于16FPS。
                      - 视频大小：不超过50MB。
                      - 视频长度：不超过5秒，否则取视频的前5秒。
                      - URL地址中不能包含中文字符。

                      关于输出视频的分辨率：

                      - 若输入视频分辨率 ≤ 720P，输出将保留原始分辨率；
                      - 若输入视频分辨率 > 720P，则在保持原视频宽高比的前提下，按比例缩放至不超过 720P。

                      关于输出视频的时长：

                      - 输出视频时长与输入视频一致，最长不超过 5 秒。
                      - 示例：若输入视频为 3 秒，则输出也为 3 秒；若输入为 6 秒，则输出为前 5 秒。

                      <strong>ref\_images\_url</strong>`array[string]` （可选）

                      输入参考图像的URL数组。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      当前仅支持传入 <strong>1 张</strong>参考图像 ，该图像可作为主体或背景使用，用于替换输入视频中的对应内容。

                      图像限制：

                      - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP。
                      - 图像分辨率：图像的宽度和高度范围为\[360, 2000]，单位为像素。
                      - 图像大小：不超过10MB。
                      - URL地址中不能包含中文字符。

                      使用建议：

                      - 若使用参考图像中的主体时，建议每张图像中仅包含一个主体。背景建议为纯色（如白色或单一颜色），以便更好地突出主体。
                      - 若使用参考图像的背景时，背景图像中不包含任何主体对象。

                      <strong>mask\_image\_url</strong>`string` （可选）

                      掩码图像的URL地址。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      该参数用于指定视频的编辑区域。与 `mask_video_url` 参数二选一填写，<strong>推荐优先使用此参数</strong> 。

                      掩码图像的白色区域（像素值严格为 \[255, 255, 255]）表示需要编辑的部分；黑色区域（像素值严格为 \[0, 0, 0]）表示保留不变的区域。

                      图像限制：

                      - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP。
                      - 图像分辨率：和输入视频（`video_url`）分辨率严格相同。
                      - 图像大小：不超过10MB。
                      - URL地址中不能包含中文字符。

                      <strong>mask\_frame\_id</strong>`integer` （可选）

                      当 `mask_image_url` 不为空时，该参数生效，用于标识掩码目标出现在视频中的哪一帧，以“帧 ID”表示。

                      默认值为 1，单位为帧，表示视频的第一帧（首帧）。

                      取值范围为`[1, max_frame_id]`，其中`max_frame_id=输入视频帧率*输入视频时长+1`。

                      > 例如，输入视频（`video_url`）帧率为16FPS，表示每秒 16 帧，视频时长为5秒，因此输入视频的总帧数为16\*5+1=81，即max\_frame\_id=81。

                      <strong>mask\_video\_url</strong>`string` （可选）

                      掩码视频的URL地址。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      该参数用于指定视频的编辑区域。与`mask_image_url`参数，二选一填写。

                      掩码视频的视频格式、帧率、分辨率、长度需与输入视频（`video_url`）完全一致。

                      掩码视频的白色区域（像素值严格为 \[255, 255, 255]）表示需要编辑的部分；黑色区域（像素值严格为 \[0, 0, 0]）表示保留不变的区域。
                    </Accordion>
                  </td>
                </tr>

                <tr>
                  <td>
                    <strong>parameters</strong> `object` （可选）

                    视频处理参数，如设置水印等。

                    <Accordion title="属性" defaultOpen>
                      <strong>control\_condition</strong> `string` （可选）

                      设置视频特征提取的方式。默认为""，表示不做任何提取。

                      - `posebodyface`：提取输入视频的主体的脸部表情和肢体动作，适用于主体脸部在画面中占比较大、特征清晰可见的场景。
                      - `depth`：提取输入视频的构图和运动轮廓。

                      <strong>mask\_type</strong> `string` （可选）

                      当 `mask_image_url` 不为空时，该参数生效，用于指定编辑区域的行为方式。

                      - `tracking`（默认值）：编辑区域将根据目标物体的运动轨迹动态跟随，适用于主体运动场景。
                      - `fixed` ：编辑区域保持固定不变，不会随画面内容变化。

                      <strong>expand\_ratio</strong> `float` （可选）

                      当 `mask_type` 为 `tracking` 时，该参数生效，表示对掩码区域进行向外扩展的比例。

                      取值范围为 \[0.0, 1.0]，默认值为 0.05。推荐使用默认值。

                      取值越小，掩码区域越贴合目标物体；取值越大，掩码区域的扩展范围越广。

                      <strong>expand\_mode</strong> `string` （可选）

                      当 `mask_type` 为 `tracking` 时，该参数生效，表示掩码区域的形状。

                      算法会根据选择的`expand_mode`，基于输入的掩码图像生成对应形状的掩码视频。支持的取值如下：

                      - `hull`（默认值）：多边形模式，表示使用一个多边形包裹掩码目标。
                      - `bbox`：边界框模式，表示使用一个矩形包裹掩码目标。
                      - `original`：原始模式，表示尽量保持与原始掩码目标的形状一致。

                      <strong>size</strong> `string` （可选）

                      生成视频的分辨率（宽\*高）。目前支持生成720P 视频，分辨率的取值为：

                      - `1280*720`（默认值）：视频宽高比为16:9。其中，1280代表宽度，720代表高度。
                      - `720*1280`：视频宽高比为9:16。
                      - `960*960`：视频宽高比为1:1。
                      - `832*1088`：视频宽高比为3:4。
                      - `1088*832`：视频宽高比为4:3。

                      <strong>duration</strong> `integer` （可选）

                      视频生成时长，单位为秒。当前参数值固定为5，且不支持修改。模型将始终生成5秒时长的视频。

                      <strong>prompt\_extend</strong>`bool` （可选）

                      是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写。对于较短的prompt生成效果提升明显，但会增加耗时。

                      - `true`（默认值）：开启智能改写。
                      - `false`：关闭智能改写。<strong>（推荐）</strong>

                      > 当文本描述与输入的视频内容不一致时，模型可能产生误解。建议手动关闭智能扩写，并在 `prompt` 中提供清晰、具体的画面描述，以提升生成一致性与准确性。

                      <strong>seed</strong>`integer`（可选）

                      随机数种子，用于控制模型生成内容的随机性。seed参数取值范围是`[0, 2147483647]`。

                      如果不提供，则算法自动生成一个随机数作为种子。如果您希望生成内容保持相对稳定，请使用相同的seed参数值。

                      <strong>watermark</strong> `bool`（可选）

                      是否添加水印标识，水印位于图片右下角，文案为“AI生成”。

                      - `false`（默认值）：不添加水印。
                      - `true`：添加水印。
                    </Accordion>
                  </td>
                </tr>
              </tbody>
            </table>
          </Tab>

          <Tab title="视频延展">
            <strong>model</strong> `string` <strong>（必选）</strong>

            模型名称。示例值：wanx2.1-vace-plus。

            <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
              <colgroup>
                <col style={{ width: "100%" }} />
              </colgroup>

              <tbody>
                <tr>
                  <td>
                    <strong>input</strong> `object` <strong>（必选）</strong>

                    输入的基本信息，如提示词等。

                    <Accordion title="属性" defaultOpen>
                      <strong>prompt</strong>`string`<strong>（必选）</strong>

                      提示词，用来描述生成视频中期望包含的元素和视觉特点。

                      支持中英文，长度不超过800个字符，每个汉字/字母占一个字符，超过部分会自动截断。

                      提示词技巧请参见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

                      <strong>function</strong>`string`<strong>（必选）</strong>

                      功能名称。视频延展设置为 `video_extension`。

                      视频延展支持基于图像或视频生成延续性内容，还支持通过参考视频提取动态特征（如动作、构图等），用于指导生成具有相似运动表现的视频。

                      > 延长后的视频总时长为 5 秒 ，请注意：这是指最终输出视频的完整时长为 5 秒，而非在原视频基础上延长 5 秒。

                      <strong>first\_frame\_url</strong>`string` （可选）

                      首帧图像的URL地址。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      图像限制：

                      - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP。
                      - 图像分辨率：图像的宽和高范围在\[360, 2000]，单位像素。
                      - 图像大小：不超过10MB。
                      - URL地址中不能包含中文字符。

                      <strong>last\_frame\_url</strong>`string`（可选）

                      尾帧图像的URL地址。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      图像限制：

                      - 图像格式：JPG、JPEG、PNG、BMP、TIFF、WEBP。
                      - 图像分辨率：图像的宽和高范围在\[360, 2000]，单位像素。
                      - 图像大小：不超过10MB。
                      - URL地址中不能包含中文字符。

                      <strong>first\_clip\_url</strong>`string` （可选）

                      首段视频的URL地址。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      视频限制：

                      - 视频格式：MP4。
                      - 视频帧率：大于等于16FPS。当`first_clip_url`和`last_clip_url`同时使用时，建议两个片段的帧率保持一致。
                      - 视频大小：不超过50MB。
                      - 视频长度：不超过3秒，否则取视频的前3秒。若同时填写`first_clip_url`和`last_clip_url`时，两段视频的总时长不超过3秒。
                      - URL地址中不能包含中文字符。

                      关于输出视频的分辨率：

                      - 若输入视频分辨率 ≤ 720P，输出将保留原始分辨率；
                      - 若输入视频分辨率 > 720P，则在保持原视频宽高比的前提下，按比例缩放至不超过 720P。

                      <strong>last\_clip\_url</strong>`string`（可选）

                      尾段视频的URL地址。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://help-static-aliyun-doc.aliyuncs.com/xxx.mp3">[https://help-static-aliyun-doc.aliyuncs.com/xxx.mp3](https://help-static-aliyun-doc.aliyuncs.com/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      视频限制：

                      - 视频格式：MP4。
                      - 视频帧率：大于等于16FPS。当`first_clip_url`和`last_clip_url`同时使用时，建议两个片段的帧率保持一致。
                      - 视频大小：不超过50MB。
                      - 视频长度：不超过3秒，否则取视频的前3秒。若同时填写`first_clip_url`和`last_clip_url`时，两段视频的总时长不超过3秒。
                      - URL地址中不能包含中文字符。

                      关于输出视频的分辨率：

                      - 若输入视频分辨率 ≤ 720P，输出将保留原始分辨率；
                      - 若输入视频分辨率 > 720P，则在保持原视频宽高比的前提下，按比例缩放至不超过 720P。

                      <strong>video\_url</strong>`string` （可选）

                      输入视频的URL地址。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://help-static-aliyun-doc.aliyuncs.com/xxx.mp3">[https://help-static-aliyun-doc.aliyuncs.com/xxx.mp3](https://help-static-aliyun-doc.aliyuncs.com/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      该视频主要用于提取其中的运动特征，与 `first_frame_url` 、 `last_frame_url` 、`first_clip_url` 、 `last_clip_url`参数配合使用，以指导生成具有相似运动表现的延展视频。

                      视频限制：

                      - 视频格式：MP4。
                      - 视频帧率：大于等于16FPS，与前后片段保持一致。
                      - 视频分辨率：与前后帧、前后片段保持一致。
                      - 视频大小：不超过50MB。
                      - 视频长度：不超过5秒，否则取视频的前5秒。
                      - URL地址中不能包含中文字符。
                    </Accordion>
                  </td>
                </tr>

                <tr>
                  <td>
                    <strong>parameters</strong> `object` （可选）

                    视频处理参数，如设置输出视频的分辨率等。

                    <Accordion title="属性" defaultOpen>
                      <strong>control\_condition</strong> `string` （可选）

                      设置视频特征提取的方式，输入video\_url时必选。默认为""，表示不做任何提取。

                      - `posebodyface`：提取输入视频的主体的脸部表情和肢体动作。
                      - `depth`：提取输入视频的构图和运动轮廓。

                      <strong>duration</strong> `integer` （可选）

                      视频生成时长，单位为秒。当前参数值固定为5，且不支持修改。模型将始终生成5秒时长的视频。

                      <strong>prompt\_extend</strong>`bool` （可选）

                      是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写。对于较短的prompt生成效果提升明显，但会增加耗时。

                      - `true`（默认值）：开启智能改写。
                      - `false`：关闭智能改写。<strong>（推荐）</strong>

                      > 当文本描述与输入的视频内容不一致时，模型可能产生误解。建议手动关闭智能扩写，并在 `prompt` 中提供清晰、具体的画面描述，以提升生成一致性与准确性。

                      <strong>seed</strong>`integer`（可选）

                      随机数种子，用于控制模型生成内容的随机性。seed参数取值范围是`[0, 2147483647]`。

                      如果不提供，则算法自动生成一个随机数作为种子。如果您希望生成内容保持相对稳定，请使用相同的seed参数值。

                      <strong>watermark</strong> `bool`（可选）

                      是否添加水印标识，水印位于图片右下角，文案为“AI生成”。

                      - `false`（默认值）：不添加水印。
                      - `true`：添加水印。
                    </Accordion>
                  </td>
                </tr>
              </tbody>
            </table>
          </Tab>

          <Tab title="视频画面扩展">
            <strong>model</strong> `string` <strong>（必选）</strong>

            模型名称。示例值：wanx2.1-vace-plus。

            <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
              <colgroup>
                <col style={{ width: "100%" }} />
              </colgroup>

              <tbody>
                <tr>
                  <td>
                    <strong>input</strong> `object` <strong>（必选）</strong>

                    输入的基本信息，如提示词等。

                    <Accordion title="属性" defaultOpen>
                      <strong>prompt</strong>`string`<strong>（必选）</strong>

                      提示词，用来描述生成视频中期望包含的元素和视觉特点。

                      支持中英文，长度不超过800个字符，每个汉字/字母占一个字符，超过部分会自动截断。

                      提示词技巧请参见[文生视频/图生视频Prompt指南](/zh/model-studio/text-to-video-prompt)。

                      <strong>function</strong>`string`<strong>（必选）</strong>

                      功能名称。视频画面扩展设置为 `video_outpainting`。

                      视频画面扩展支持对视频在上、下、左、右四个方向按比例扩展。

                      <strong>video\_url</strong>`string` <strong>（必选）</strong>

                      输入视频的URL地址。

                      1. 公网URL：

                         - 支持 HTTP 和 HTTPS 协议。
                         - 示例值：<a href="https://xxx/xxx.mp3">[https://xxx/xxx.mp3](https://xxx/xxx.mp3)</a>。
                      2. 临时URL：

                         - 支持OSS协议，必须通过[上传文件获取临时 URL](/zh/model-studio/get-temporary-file-url)。
                         - 示例值：oss\://dashscope-instant/xxx/xxx.mp3。

                      视频限制：

                      - 视频格式：MP4。
                      - 视频帧率：大于等于16FPS。
                      - 视频大小：不超过50MB。
                      - 视频长度：不超过5秒，否则取视频的前5秒。
                      - URL地址中不能包含中文字符。

                      关于输出视频的分辨率：

                      - 若输入视频分辨率 ≤ 720P，输出将保留原始分辨率；
                      - 若输入视频分辨率 > 720P，则在保持原视频宽高比的前提下，按比例缩放至不超过 720P。

                      关于输出视频的时长：

                      - 输出视频时长与输入视频一致，最长不超过 5 秒。
                      - 示例：若输入视频为 3 秒，则输出也为 3 秒；若输入为 6 秒，则输出为前 5 秒。
                    </Accordion>
                  </td>
                </tr>

                <tr>
                  <td>
                    <strong>parameters</strong> `object` （可选）

                    视频处理参数，如设置扩展比例等。

                    <Accordion title="属性" defaultOpen>
                      <strong>top\_scale</strong> `float` （可选）

                      视频画面居中，向上按比例扩展视频。

                      取值范围为\[1.0, 2.0]，默认值为1.0，表示不扩展。

                      <strong>bottom\_scale</strong> `float` （可选）

                      视频画面居中，向下按比例扩展视频。

                      取值范围为\[1.0, 2.0]，默认值为1.0，表示不扩展。

                      <strong>left\_scale</strong> `float` （可选）

                      视频画面居中，向左按比例扩展视频。

                      取值范围为\[1.0, 2.0]，默认值为1.0，表示不扩展。

                      <strong>right\_scale</strong> `float` （可选）

                      视频画面居中，向右按比例扩展视频。

                      取值范围为\[1.0, 2.0]，默认值为1.0，表示不扩展。

                      <strong>duration</strong> `integer` （可选）

                      视频生成时长，单位为秒。当前参数值固定为5，且不支持修改。模型将始终生成5秒时长的视频。

                      <strong>prompt\_extend</strong>`bool` （可选）

                      是否开启prompt智能改写。开启后使用大模型对输入prompt进行智能改写。对于较短的prompt生成效果提升明显，但会增加耗时。

                      - `true`（默认值）：开启智能改写。
                      - `false`：关闭智能改写。<strong>（推荐）</strong>

                      > 当文本描述与输入的视频内容不一致时，模型可能产生误解。建议手动关闭智能扩写，并在 `prompt` 中提供清晰、具体的画面描述，以提升生成一致性与准确性。

                      <strong>seed</strong>`integer`（可选）

                      随机数种子，用于控制模型生成内容的随机性。seed参数取值范围是`[0, 2147483647]`。

                      如果不提供，则算法自动生成一个随机数作为种子。如果您希望生成内容保持相对稳定，请使用相同的seed参数值。

                      <strong>watermark</strong> `bool`（可选）

                      是否添加水印标识，水印位于图片右下角，文案为“AI生成”。

                      - `false`（默认值）：不添加水印。
                      - `true`：添加水印。
                    </Accordion>
                  </td>
                </tr>
              </tbody>
            </table>
          </Tab>
        </Tabs>
      </td>

      <td>
        <CodeGroup>
          ```bash 多图参考
          # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
          --header 'X-DashScope-Async: enable' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "wanx2.1-vace-plus",
              "input": {
                  "function": "image_reference",
                  "prompt": "视频中，一位女孩自晨雾缭绕的古老森林深处款款走出，她步伐轻盈，镜头捕捉她每一个灵动瞬间。当女孩站定，环顾四周葱郁林木时，她脸上绽放出惊喜与喜悦交织的笑容。这一幕，定格在了光影交错的瞬间，记录下女孩与大自然的美妙邂逅。",
                  "ref_images_url": [
                      "http://wanx.alicdn.com/material/20250318/image_reference_2_5_16.png",
                      "http://wanx.alicdn.com/material/20250318/image_reference_1_5_16.png"
                  ]
              },
              "parameters": {
                  "prompt_extend": true,
                  "obj_or_bg": ["obj","bg"],
                  "size": "1280*720"
              }
          }'
          ```

          ```bash 视频重绘
          # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
          --header 'X-DashScope-Async: enable' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "wanx2.1-vace-plus",
              "input": {
                  "function": "video_repainting",
                  "prompt": "视频展示了一辆黑色的蒸汽朋克风格汽车，绅士驾驶着，车辆装饰着齿轮和铜管。背景是蒸汽驱动的糖果工厂和复古元素，画面复古与趣味。",
                  "video_url": "http://wanx.alicdn.com/material/20250318/video_repainting_1.mp4"
              },
              "parameters": {
                  "prompt_extend": false,
                  "control_condition": "depth"
              }
          }'
          ```

          ```bash 局部编辑
          # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
          --header 'X-DashScope-Async: enable' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "wanx2.1-vace-plus",
              "input": {
                  "function": "video_edit",
                  "prompt": "视频展示了一家巴黎风情的法式咖啡馆，一只穿着西装的狮子优雅地品着咖啡。它一手端着咖啡杯，轻轻啜饮，神情惬意。咖啡馆装饰雅致，柔和的色调与温暖灯光映照着狮子所在的区域。",
                  "mask_image_url": "http://wanx.alicdn.com/material/20250318/video_edit_1_mask.png",
                  "video_url": "http://wanx.alicdn.com/material/20250318/video_edit_2.mp4",
                  "mask_frame_id": 1
              },
              "parameters": {
                  "prompt_extend": false,
                  "mask_type": "tracking",
                  "expand_ratio": 0.05
              }
          }'
          ```

          ```bash 视频延展
          # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
          --header 'X-DashScope-Async: enable' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "wanx2.1-vace-plus",
              "input": {
                  "function": "video_extension",
                  "prompt": "一只戴着墨镜的狗在街道上滑滑板，3D卡通。",
                  "first_clip_url": "http://wanx.alicdn.com/material/20250318/video_extension_1.mp4"
              },
              "parameters": {
                  "prompt_extend": false
              }
          }'
          ```

          ```bash 视频画面扩展
          # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
          curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
          --header 'X-DashScope-Async: enable' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "wanx2.1-vace-plus",
              "input": {
                  "function": "video_outpainting",
                  "prompt": "一位优雅的女士正在激情演奏小提琴，她身后是一支完整的交响乐团。",
                  "video_url": "http://wanx.alicdn.com/material/20250318/video_outpainting_1.mp4"
              },
              "parameters": {
                  "prompt_extend": false,
                  "top_scale": 1.5,
                  "bottom_scale": 1.5,
                  "left_scale": 1.5,
                  "right_scale": 1.5
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
  <Tab title="北京">
    `GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `GET https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/{task_id}`

    调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。
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
        #### 响应参数 <span id="810682d979i8q" />

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

          <strong>video\_url</strong>`string`

          视频URL。链接有效期24小时，可通过此URL下载视频。输出视频格式为mp4（H.264 编码）。

          <strong>orig\_prompt</strong> `string`

          原始的输入prompt。

          <strong>actual\_prompt</strong> `string`

          开启prompt智能改写后实际使用的prompt。若不开启prompt智能改写，不会返回该字段。

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>video\_duration</strong> `integer`

          生成视频的时长，单位为秒。

          <strong>video\_ratio</strong> `string`

          生成视频的比例。固定为`standard`。

          <strong>video\_count</strong> `integer`

          生成视频的数量。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            任务数据（如任务状态、视频URL等）仅保留24小时，超时后会被自动清除。请您务必及时保存生成的视频。

            ```json
            {
                "request_id": "851985d0-fbba-9d8d-a17a-xxxxxx",
                "output": {
                    "task_id": "208e2fd1-fcb4-4adf-9fcc-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2025-05-15 16:14:44.723",
                    "scheduled_time": "2025-05-15 16:14:44.750",
                    "end_time": "2025-05-15 16:20:09.389",
                    "video_url": "https://dashscope-result-wlcb.oss-cn-wulanchabu.aliyuncs.com/xxx.mp4?xxxxxx",
                    "orig_prompt": "视频中，一位女孩自晨雾缭绕的古老森林深处款款走出，她步伐轻盈，镜头捕捉她每一个灵动瞬间。当女孩站定，环顾四周葱郁林木时，她脸上绽放出惊喜与喜悦交织的笑容。这一幕，定格在了光影交错的瞬间，记录下女孩与大自然的美妙邂逅。",
                    "actual_prompt": "一位身着浅色长裙的女孩从晨雾缭绕的古老森林深处缓缓走出，步伐轻盈如舞。她长发微卷，面容清秀，眼神明亮。镜头跟随她的动作，捕捉每一个灵动瞬间。当她站定，转身环顾四周葱郁林木时，脸上绽放出惊喜与喜悦交织的笑容。阳光透过树叶洒下斑驳光影，定格这一人与自然和谐共处的美好时刻。画面风格为清新自然系写真，中景全景结合，平视视角带有轻微移动运镜。"
                },
                "usage": {
                    "video_duration": 5,
                    "video_ratio": "standard",
                    "video_count": 1
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
                    "message": "The size is not match xxxxxx"
                }
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 使用限制 <span id="48194b2ea8c7l" />

- <strong>数据时效</strong>：任务`task_id`和 视频`video_url`均只保留 24 小时，过期后将无法查询或下载。
- <strong>音频支持</strong>：当前仅支持生成无声视频，不支持音频输出。如有需要，可通过[语音合成](/zh/model-studio/speech-recognition-api-reference)生成音频。

## 错误码 <span id="b61f9fd1c3caw" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="042dd764b01yt" />

#### Q：如何获取视频存储的访问域名白名单？ <span id="c7421e6e32dj8" />

A： 模型生成的视频存储于阿里云OSS，API将返回一个临时的公网URL。<strong>若需要对该下载地址进行防火墙白名单配置</strong>，请注意：由于底层存储会根据业务情况进行动态变更，为避免过期信息影响访问，文档不提供固定的OSS域名白名单。如有安全管控需求，请联系客户经理获取最新OSS域名列表。

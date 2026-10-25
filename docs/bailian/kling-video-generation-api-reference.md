> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 可灵-视频生成API文档

> 可灵-视频生成模型支持 文生视频、图生视频-基于首帧、图生视频-基于首尾帧、参考生视频以及视频编辑 。

<meta property="og:title" content="可灵-视频生成API文档" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 服务开通 <span id="kl004a0h2svco" /> <span id="kl003a0sesvco" />

请前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索“kling”，找到<strong>可灵AI</strong>模型卡片，单击<strong>立即开通</strong>，在弹窗内确认开通及授权。

## 适用范围 <span id="kl013a0h2scop" /> <span id="kl012a0scope" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](/zh/model-studio/video-generate-edit-model)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/get-api-key)。

## 模型概览

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "30%" }} /><col style={{ width: "20%" }} /><col style={{ width: "30%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>能力支持</strong></p></th><th><p><strong>输入模态</strong></p></th><th><p><strong>输出视频规格</strong></p></th></tr></thead><tbody><tr><td><p>kling/kling-v3-turbo-video-generation</p></td><td><ul><li><p>文生视频</p></li><li><p>图生视频-基于首帧</p></li></ul></td><td><p>文本、图像</p></td><td><p>分辨率模式：std（720P）、pro（1080P）</p><p>宽高比：16:9、9:16、1:1</p><p>视频时长：3-15秒</p><p>视频格式：MP4</p></td></tr><tr><td><p>kling/kling-v3-video-generation</p></td><td><ul><li><p>文生视频</p></li><li><p>图生视频-基于首帧</p></li><li><p>图生视频-基于首尾帧</p></li></ul></td><td><p>文本、图像</p></td><td><p>分辨率模式：std（720P）、pro（1080P）、4k</p><p>宽高比：16:9、9:16、1:1</p><p>视频时长：3-15秒</p><p>视频格式：MP4</p></td></tr><tr><td><p>kling/kling-v3-omni-video-generation</p></td><td><ul><li><p>文生视频</p></li><li><p>图生视频-基于首帧</p></li><li><p>图生视频-基于首尾帧</p></li><li><p>参考生视频</p></li><li><p>视频编辑</p></li></ul></td><td><p>文本、图像、视频</p></td><td><p>分辨率模式：std（720P）、pro（1080P）、4k</p><p>宽高比：16:9、9:16、1:1</p><p>视频时长：3-15秒</p><p>视频格式：MP4</p></td></tr></tbody></table>

## HTTP调用 <span id="kl101a0h2http" /> <span id="kl100a0httpsec" />

由于视频生成任务耗时较长（通常为1-5分钟），API采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤1：创建任务获取任务ID <span id="kl111a0h3stp1" /> <span id="kl110a0step1" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`

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
        #### 请求参数 <span id="kl123a0h4reqp" />

        ##### 请求头（Headers） <span id="kl142a0h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="kl154a0h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。

        可选值：

        - `kling/kling-v3-turbo-video-generation`
        - `kling/kling-v3-video-generation`
        - `kling/kling-v3-omni-video-generation`

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，如提示词、媒体素材等。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` （条件必填）

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，每个汉字/字母占一个字符，不超过2500个字符，超过将返回错误。

          填写逻辑：

          - 当 `shot_type=intelligence` 时，此参数<strong>必填</strong>。
          - 当 `shot_type=customize` 时，此参数<strong>不生效</strong>，请以 `multi_prompt` 为准。

          Omni模型可通过prompt与主体、图片、视频等内容实现多种能力：

          - 支持模型：`kling/kling-v3-omni-video-generation`。
          - 适用任务：参考生视频。
          - 引用格式：通过`<<<>>>`的格式来指定某个主体、图片、视频，如：`<<<element_1>>>`、`<<<image_1>>>`、`<<<video_1>>>`。
          - 引用顺序：按照`media`数组顺序定义prompt中引用的顺序。
          - 用法示例：`一只<<<element_1>>>在月光下奔跑`。

          <strong>negative\_prompt</strong> `string` （可选）

          负向提示词，用于描述不希望在结果中出现的内容。不超过2500个字符。

          - `kling/kling-v3-turbo-video-generation` 无独立负向提示词能力，该字段不生效（负向描述可写入`prompt`）。
          - 支持模型：`kling/kling-v3-video-generation`、`kling/kling-v3-omni-video-generation`。

          <strong>media</strong> `array` （可选）

          文生视频任务无需填写此参数。

          媒体素材列表，用于指定图像或视频素材。

          不同模型支持的媒体素材搭配不一样：

          <Accordion title="素材组合规则" defaultOpen>
            <Accordion title="kling/kling-v3-turbo-video-generation" defaultOpen>
              仅支持以下媒体素材组合，非法组合将报错。

              - <strong>图生视频-基于首帧</strong>：`first_frame`。

                - 数量限制：首帧恰好1张。不支持`last_frame`。
            </Accordion>

            <Accordion title="kling/kling-v3-video-generation" defaultOpen>
              仅支持以下媒体素材组合，非法组合将报错。

              - <strong>图生视频-基于首帧</strong>：`first_frame`。

                - 数量限制：首帧1张。
              - <strong>图生视频-基于首尾帧</strong>：`first_frame+last_frame`。

                - 数量限制：首帧1张，尾帧1张。
            </Accordion>

            <Accordion title="kling/kling-v3-omni-video-generation" defaultOpen>
              仅支持以下媒体素材组合，非法组合将报错。

              - <strong>图生视频-基于首帧</strong>：`first_frame`。

                - 图像数量：首帧1张。
              - <strong>图生视频-基于首尾帧</strong>：`first_frame+last_frame`。

                - 图像数量：首帧1张，尾帧1张。
              - <strong>参考生视频</strong>：支持以下 `media` 组合。

                - 仅传入 `feature` 时：必须传入1个视频。
                - 仅传入 `refer` 时：参考图片与多图主体数量（`element_list` 的数组长度）之和不得超过7。
                - 传入 `feature+refer` 时：必须传入1个视频，且参考图片与多图主体数量（`element_list` 的数组长度）之和不得超过4。
                - 传入 `feature+first_frame` 时：必须传入1个视频和1张首帧。
              - <strong>视频编辑</strong>：支持以下 `media` 组合。

                - 仅传入 `base` 时：必须传入1个视频。
                - 传入 `base+refer` 时：必须传入1个视频，且参考图片与多图主体数量（`element_list` 的数组长度）之和不得超过4。
            </Accordion>
          </Accordion>

          <Accordion title="属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体素材类型。可选值与所选模型有关：

            <Accordion title="kling/kling-v3-turbo-video-generation" defaultOpen>
              可选值：

              - `first_frame`：首帧图片。

              不支持`last_frame`。不同媒体素材之间的搭配限制，请参见 `media` 参数下的”素材组合规则”。
            </Accordion>

            <Accordion title="kling/kling-v3-video-generation" defaultOpen>
              可选值：

              - `first_frame`：首帧图片。
              - `last_frame`：尾帧图片。

              不同媒体素材之间的搭配限制，请参见 `media` 参数下的”素材组合规则”。
            </Accordion>

            <Accordion title="kling/kling-v3-omni-video-generation" defaultOpen>
              可选值：

              - `first_frame`：首帧图片。
              - `last_frame`：尾帧图片。
              - `refer`：参考图片。
              - `base`：待编辑视频。
              - `feature`：特征参考视频。

              不同媒体素材之间的搭配限制，请参见 `media` 参数下的“素材组合规则”。
            </Accordion>

            <strong>url</strong> `string` <strong>（必选）</strong>

            媒体素材URL。素材包括图像、视频。

            <Accordion title="传入图像（type=first_frame或last_frame或refer）" defaultOpen>
              图像URL。

              - 支持 HTTP 或 HTTPS 协议。
              - 示例值：<a href="https://xxx/xxx.png">[https://xxx/xxx.png](https://xxx/xxx.png)</a>。

              图像限制：

              - 格式：JPEG、JPG、PNG（不支持透明通道）。
              - 分辨率：宽和高的范围为\[300, 8000]像素。
              - 宽高比：1:2.5 \~ 2.5:1。
              - 文件大小：不超过10MB。
            </Accordion>

            <Accordion title="传入视频（type=base或feature）" defaultOpen>
              视频文件的 URL。

              - 支持 HTTP 和 HTTPS 协议。
              - 示例值：<a href="https://xxx/xxx.mp4">[https://xxx/xxx.mp4](https://xxx/xxx.mp4)</a>。

              视频限制：

              - 格式：mp4、mov。
              - 文件大小：不超过200MB。
              - 时长：3～15.5s。
              - 分辨率：宽和高的范围为\[700, 4553]像素，像素总面积不超过8294400。
              - 宽高比：0.4～2。
              - 帧率：24～60fps。
            </Accordion>

            <strong>keep\_original\_sound</strong> `string` （可选）

            支持模型：kling/kling-v3-omni-video-generation 。

            生效条件：当且仅当传入视频（type=base或feature）时生效。

            是否保留原视频声音。

            - `no`：默认值，不保留原声。
            - `yes`：保留原声。
          </Accordion>

          <strong>multi\_shot</strong> `boolean` （可选）

          是否开启多镜头生成。

          - `false`：默认值，不开启多镜头。
          - `true`：开启多镜头生成。

          注意：

          - 视频特征参考（feature）场景支持开启多镜头（`shot_type`仅支持`intelligence`）。
          - 视频编辑（base）场景不支持多镜头。

          <strong>shot\_type</strong> `string` （条件必填）

          当`multi_shot=true` 时必填。多镜头模式类型。

          - `intelligence`：智能分镜，由模型自动规划镜头。
          - `customize`：自定义模式，支持自定义每个片段的提示词和时长。

          <strong>multi\_prompt</strong> `array` （条件必填）

          当`shot_type=customize` 时必填。多镜头自定义模式下的片段列表。

          <Accordion title="属性" defaultOpen>
            <strong>index</strong> `integer` <strong>（必选）</strong>

            分镜片段索引，分镜数量为1～6个，索引从1开始。

            <strong>prompt</strong> `string` <strong>（必选）</strong>

            对应片段的提示词，支持中英文，不超过512个字符，超过自动截断。

            <strong>duration</strong> `integer` <strong>（必选）</strong>

            对应片段的时长，单位为秒。

            取值为\[1, `parameters.duration`]之间的整数。
          </Accordion>

          <strong>element\_list</strong> `array` （可选）

          主体列表，用于指定视频中需要引入的主体元素。

          注意：`kling/kling-v3-turbo-video-generation` 不支持主体，该字段不可传。

          <Accordion title="属性" defaultOpen>
            <strong>element\_id</strong> `integer` （条件必填）

            传`element_list`时必填，表示主体ID。请在[可灵-主体ID列表](/zh/model-studio/kling-object-ids)获取主体ID。

            主体个数限制（element\_list数组长度限制）：

            - 图生视频-基于首帧：最多支持3个主体。
            - 图生视频-基于首尾帧：最多支持3个主体。
            - 参考生视频（`type=refer`）：参考图片与多图主体数量之和不得超过7。
            - 参考生视频（`type=feature+refer`）：参考图片与多图主体数量之和不得超过4。
            - 视频输入场景（omni模型）：支持视频类型主体，最多1个。引用视频类型主体后，不可再传参考图/图片类型主体。
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        视频生成参数。如设置生成模式、画面比例、视频时长、是否生成音频等。

        <Accordion title="属性" defaultOpen>
          <strong>mode</strong> `string` （可选）

          视频生成模式。

          - `pro`：默认值，专业模式，输出视频分辨率为1080P。
          - `std`：标准模式，输出视频分辨率为720P。
          - `4k`：4K模式，输出视频分辨率为4K。

          注意：`kling/kling-v3-turbo-video-generation` 仅支持`std`（720P）和`pro`（1080P），不支持4K。

          <strong>aspect\_ratio</strong> `string` （条件必填）

          生成视频的宽高比例。

          可选值：

          - `16:9`：默认值。
          - `9:16`
          - `1:1`

          以下场景必须填写：

          - 文生视频：必须设置。
          - 参考生视频（`type=feature`、`type=feature+refer`、`type=refer`）：必须设置。

          <Accordion title="其他场景无需设置">
            * 图生视频-基于首帧：以首帧的宽高比为基准，无需填写。
            * 图生视频-基于首尾帧：以首帧的宽高比为基准，无需填写。
            * 参考生视频（`type=feature+first_frame`）：以首帧的宽高比为基准，无需填写。
            * 视频编辑：以输入视频的宽高比为基准，无需填写。
          </Accordion>

          <strong>duration</strong> `integer` （可选）

          <Tip>
            duration直接影响费用，按秒计费，时间越长费用越高，请前往[百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)查看价格。
          </Tip>

          生成视频的时长，单位为秒。

          - kling/kling-v3-turbo-video-generation：取值为\[3, 15]之间的整数，默认值为5。
          - kling/kling-v3-video-generation：取值为\[3, 15]之间的整数，默认值为5。
          - kling/kling-v3-omni-video-generation：取值为\[3, 15]之间的整数，默认值为5。

            - 注意：当传入视频时，该参数的行为因`type`不同而异：

              - `type=feature`：取值为\[3, 10]之间的整数，默认值为5。
              - `type=base`：输出视频时长与传入视频时长相同，此时当前参数无效。按输入视频时长四舍五入取整计量计费。

          示例值：5。

          <strong>audio</strong> `boolean` （可选）

          <Tip>
            audio直接影响费用，请前往[百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)查看价格。
          </Tip>

          是否生成有声视频。开启后模型将根据视频内容自动生成匹配的背景音乐或音效。

          - `false`：默认值，输出无声视频。
          - `true`：输出有声视频。

          注意：

          - 当传入视频（`type=base或feature`）时，`audio`只能设置为`false`。
          - `kling/kling-v3-turbo-video-generation` 固定生成音画同出视频，该字段不生效（恒为生成）。

          <strong>watermark</strong> `boolean` （可选）

          是否同时生成含水印的视频。水印位于视频右下角，文案固定为”可灵AI”。设置为true时，响应中将额外返回`watermark_video_url`字段。

          - `false`：默认值，不生成含水印的视频。
          - `true`：同时生成含水印的视频。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="文生视频">
            支持模型：`kling/kling-v3-turbo-video-generation`、`kling/kling-v3-video-generation`、`kling/kling-v3-omni-video-generation`。

            ```bash
            # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "kling/kling-v3-turbo-video-generation",
                "input": {
                    "prompt": "一只小猫在月光下奔跑"
                },
                "parameters": {
                    "mode": "std",
                    "aspect_ratio": "16:9",
                    "duration": 5,
                    "audio": false,
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="Python SDK">
            ```python expandable
            from http import HTTPStatus
            from dashscope import VideoSynthesis
            import dashscope
            import os

            # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
            # 获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
            api_key = os.getenv("DASHSCOPE_API_KEY")

            def sample_async_call():
                # 异步提交视频生成任务
                rsp = VideoSynthesis.async_call(api_key=api_key,
                                            model='kling/kling-v3-turbo-video-generation',
                                            prompt='一只小猫在月光下奔跑',
                                            mode='std',
                                            aspect_ratio='16:9',
                                            duration=5,
                                            audio=False,
                                            watermark=True)
                print(rsp)
                if rsp.status_code == HTTPStatus.OK:
                    print("task_id: %s" % rsp.output.task_id)
                else:
                    print('Failed, status_code: %s, code: %s, message: %s' %
                          (rsp.status_code, rsp.code, rsp.message))

                # 查询任务状态
                status = VideoSynthesis.fetch(task=rsp, api_key=api_key)
                if status.status_code == HTTPStatus.OK:
                    print(status.output.task_status)
                else:
                    print('Failed, status_code: %s, code: %s, message: %s' %
                          (status.status_code, status.code, status.message))

                # 等待任务完成
                rsp = VideoSynthesis.wait(task=rsp, api_key=api_key)
                print(rsp)
                if rsp.status_code == HTTPStatus.OK:
                    print(rsp.output.video_url)
                else:
                    print('Failed, status_code: %s, code: %s, message: %s' %
                          (rsp.status_code, rsp.code, rsp.message))

            if __name__ == '__main__':
                sample_async_call()
            ```
          </Tab>

          <Tab title="文生视频（智能分镜）">
            支持模型：`kling/kling-v3-turbo-video-generation`、`kling/kling-v3-video-generation`、`kling/kling-v3-omni-video-generation`。

            ```bash expandable
            # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "kling/kling-v3-video-generation",
                "input": {
                    "prompt": "",
                    "multi_shot": true,
                    "shot_type": "customize",
                    "multi_prompt": [
                        {
                            "index": 1,
                            "prompt": "雾岭镇比地图上更小，山雾像棉絮一样堵在街口。邮局背后果然有三棵槐树，第三棵树根旁的泥土被人动过。",
                            "duration": 5
                        },
                        {
                            "index": 2,
                            "prompt": "林澈蹲下挖出一个铁盒，里面除了一把生锈的钥匙，还有一盘老旧的录音带。录音机是邮局借的，按下播放键时，父亲的声音从沙沙杂音里爬出来：“如果你听到这段话，说明你已经走到我走过的路上了。",
                            "duration": 5
                        }
                    ],
                    "media": [],
                    "element_list": []
                },
                "parameters": {
                    "mode": "pro",
                    "duration": 10,
                    "audio": true,
                    "aspect_ratio": "9:16",
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="图生视频（首帧生视频）">
            支持模型：`kling/kling-v3-turbo-video-generation`、`kling/kling-v3-video-generation`、`kling/kling-v3-omni-video-generation`。

            ```bash
            # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "kling/kling-v3-omni-video-generation",
                "input": {
                    "prompt": "让图片中的人物动起来，头发被微风吹动",
                    "media": [
                        {
                            "type": "first_frame",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260121/zlpocv/wan-i2v-haigui.webp"
                        }
                    ]
                },
                "parameters": {
                    "mode": "std",
                    "duration": 5,
                    "audio": false,
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="图生视频（首尾帧生视频）">
            支持模型：`kling/kling-v3-video-generation`、`kling/kling-v3-omni-video-generation`。

            ```bash expandable
            # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "kling/kling-v3-omni-video-generation",
                "input": {
                    "prompt": "写实风格，一只黑色小猫好奇地看向天空，镜头从平视逐渐上升，最后俯拍它的好奇的眼神。",
                    "media": [
                        {
                            "type": "first_frame",
                            "url": "https://wanx.alicdn.com/material/20250318/first_frame.png"
                        },
                      {
                            "type": "last_frame",
                            "url": "https://wanx.alicdn.com/material/20250318/last_frame.png"
                        }
                    ]
                },
                "parameters": {
                    "mode": "std",
                    "duration": 5,
                    "audio": false,
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="参考生视频（视频+图像）">
            支持模型：`kling/kling-v3-omni-video-generation`。

            ```bash expandable
            # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID。
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "kling/kling-v3-omni-video-generation",
                "input": {
                    "prompt": "<<<element_1>>>背景，<<<image_2>>>和<<<image_1>>>握手交谈，<<<video_1>>>人物穿着黑色风衣的侦探站在公寓楼顶，手持望远镜观察街道",
                    "multi_shot": false,
                    "shot_type": "intelligence",
                    "multi_prompt": [],
                    "media": [
                        {
                            "url": "https://p2-kling.klingai.com/kcdn/cdn-kcdn112452/kling-qa-test/lip_sync_5s.mp4",
                            "type": "base",
                            "keep_original_sound": "yes"
                        },
                        {
                            "type": "refer",
                            "url": "https://p2-kling.klingai.com/kcdn/cdn-kcdn112452/kling-qa-test/zem_test/yangmi01.jpg"
                        },
                        {
                            "type": "refer",
                            "url": "https://p2-kling.klingai.com/kcdn/cdn-kcdn112452/kling-qa-test/human_2.JPG"
                        }
                    ],
                    "element_list": [
                        {
                            "element_id": 171
                        }
                    ]
                },
                "parameters": {
                    "mode": "pro",
                    "duration": 10,
                    "audio": false,
                    "aspect_ratio": "1:1",
                    "watermark": true
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
        #### 响应参数 <span id="kl365a0h4rsp1" /> <span id="kl361a0sersp1" />

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

### 步骤2：根据任务ID查询结果 <span id="kl401a0h3stp2" /> <span id="kl400a0step2" />

<strong>北京地域</strong>：`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

<Note>
  - <strong>轮询建议</strong>：视频生成过程约需数分钟，建议采用<strong>轮询</strong>机制，并设置合理的查询间隔（如 15 秒）来获取结果。
  - <strong>任务状态流转</strong>：PENDING（排队中）→ RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
  - <strong>task\_id 有效期</strong>：<strong>24小时</strong>，超时后将无法查询结果，接口将返回任务状态为`UNKNOWN`。
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
        #### 请求参数 <span id="kl424a0h4qr01" /> <span id="kl420a0secqry" />

        ##### 请求头（Headers） <span id="kl434a0h5qr01" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL路径参数（Path parameters） <span id="kl440a0h5qr02" />

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
        #### 响应参数 <span id="kl448a0h4qrs1" /> <span id="kl444a0seqrsp" />

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
          - 当状态变为 SUCCEEDED 时，响应中将包含生成的视频URL。
          - 若状态为 FAILED，请检查错误信息并重试。
          - 若状态为 CANCELED，表示任务已取消，如需继续请重新提交任务。
          - 若状态为 UNKNOWN，表示任务不存在或状态未知，可能在 task\_id 不存在或超过 24 小时有效期后出现。

          <strong>submit\_time</strong> `string`

          任务提交时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>scheduled\_time</strong> `string`

          任务执行时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>end\_time</strong> `string`

          任务完成时间。格式为 YYYY-MM-DD HH:mm:ss.SSS。

          <strong>video\_url</strong>`string`

          视频URL。仅在 task\_status 为 SUCCEEDED 时返回。

          视频格式为MP4（H.264 编码）。视频链接有效期30天，但不建议将其作为长期存储依赖，请及时下载。

          <strong>watermark\_video\_url</strong>`string`

          带水印的视频URL。仅在 task\_status 为 SUCCEEDED 时返回。

          视频格式为MP4（H.264 编码）。视频链接有效期30天，但不建议将其作为长期存储依赖，请及时下载。

          <strong>orig\_prompt</strong> `string`

          原始输入的prompt，对应请求参数`prompt`。

          <strong>code</strong>`string`

          请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。

          <strong>message</strong>`string`

          请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>duration</strong> `integer`

          生成视频的总视频时长，用于计费。

          <strong>size</strong> `string`

          生成视频的分辨率。示例值：1280\*720。

          <strong>fps</strong> `integer`

          生成视频的帧率。示例值：24。

          <strong>SR</strong> `string`

          生成视频的分辨率档位。示例值：720。`mode=4k`时返回`4k`；`kling/kling-v3-turbo-video-generation`仅返回`720`或`1080`。

          <strong>audio</strong> `boolean`

          生成视频是否为有声视频。示例值：false。`kling/kling-v3-turbo-video-generation`恒为`true`（固定音画同出）。

          <strong>video\_count</strong> `integer`

          生成视频的数量。固定为1。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            ```json
            {
                "request_id": "340f0d7d-bf7e-4c8f-9c03-xxxxxx",
                "output": {
                    "task_id": "24f5c51e-d67b-44dc-9acb-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-03-27 21:30:32.575",
                    "scheduled_time": "2026-03-27 21:30:32.603",
                    "end_time": "2026-03-27 21:31:09.177",
                    "video_url": "https://v4-fdl.kechuangai.com/ksc2/xxx.mp4?xxxx",
                    "watermark_video_url": "https://v2-fdl.kechuangai.com/ksc2/xxx.mp4?xxxx"
                },
                "usage": {
                    "duration": 5,
                    "size": "1280*720",
                    "fps": 24,
                    "video_count": 1,
                    "audio": false,
                    "SR": "720"
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
                    "message": "The parameter is invalid xxxxxx"
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

## 错误码 <span id="OajFO" /> <span id="b3533d7d91okm" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Vidu-参考生视频 API 参考

> Vidu-参考生视频模型支持传入 参考图片 和 文本提示词 ，将图片中的主体角色融合到提示词描述的场景中，生成流畅的视频内容。

<meta property="og:title" content="Vidu-参考生视频 API 参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 服务开通 <span id="8bb48973d87d6" />

请前往[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)，搜索 <strong>Vidu</strong>，找到 <strong>Vidu</strong> 模型卡片，单击<strong>立即开通</strong>；在弹窗内确认开通及授权。

## 适用范围 <span id="1fd78afa5brlj" />

为确保调用成功，请务必保证<strong>模型、Endpoint URL 和 API Key 均属于同一地域</strong>。跨地域调用将会失败。

- [<strong>选择模型</strong>](https://bailian.console.aliyun.com/cn-beijing/model/market)：确认模型所属的地域。
- <strong>选择 URL</strong>：选择对应的地域 Endpoint URL，支持HTTP URL。
- <strong>配置 API Key</strong>：选择地域并[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## HTTP 调用 <span id="pvr101a0h2http" />

由于参考生视频任务耗时较长（通常为 1-5 分钟），API 采用异步调用。整个流程包含 <strong>"创建任务 -> 轮询获取"</strong> 两个核心步骤，具体如下：

### 步骤 1：创建任务获取任务 ID <span id="pvr111a0h4stp1" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis`

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
        #### 请求参数 <span id="pvr123a0h4reqp" />

        ##### 请求头（Headers） <span id="pvr144a0h5hdr1" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        <strong>X-DashScope-Async</strong>`string`<strong>（必选）</strong>

        异步处理配置参数。HTTP请求只支持异步，<strong>必须设置为</strong>`enable`。

        <Tip>
          缺少此请求头将报错：“current user api does not support synchronous calls”。
        </Tip>

        ##### 请求体（Request Body） <span id="pvr156a0h5body" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。可选值为：

        - `vidu/viduq3-ad_reference2video`
        - `vidu/viduq3-drama_reference2video`
        - `vidu/viduq3-mix_reference2video`
        - `vidu/viduq3_reference2video`
        - `vidu/viduq3-turbo_reference2video`
        - `vidu/viduq2-pro_reference2video`
        - `vidu/viduq2_reference2video`

        <Accordion title="模型选型" defaultOpen>
          * <strong>广告视频</strong>：推荐 <strong>viduq3-ad</strong>，面向广告行业，支持营销级智能切镜、运镜和音效直出，上传商品图即可生成广告视频。
          * <strong>精品剧 / AI 漫剧</strong>：推荐 <strong>viduq3-drama</strong>，角色一致性强、动效细腻、情绪表达真实，适合剧情向内容生产。
          * <strong>通用场景</strong>：推荐 <strong>viduq3-mix</strong>，画面质感与动态效果均衡，适合大多数参考生视频需求。
          * <strong>追求速度与性价比</strong>：推荐 <strong>viduq3-turbo</strong>，生成速度最快。
        </Accordion>

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息，包括参考图片和提示词。

        <Accordion title="属性" defaultOpen>
          <strong>prompt</strong> `string` <strong>（必选）</strong>

          文本提示词。用来描述生成视频中期望包含的元素和视觉特点。

          支持中英文，不超过 5000 个字符，超过部分会自动截断。

          示例值：男人坐在靠窗的椅子上，手持吉他，在咖啡厅旁演奏一首舒缓的美国乡村民谣。

          提示词编写请参见[Vidu视频生成Prompt指南](/zh/model-studio/vidu-video-generation-prompt-guide)。

          <strong>media</strong> `array` <strong>（必选）</strong>

          媒体素材列表，用于指定视频生成所需的参考图像。

          数组的每个元素为一个媒体对象，包含 `type`和 `url` 字段。

          <Accordion title="元素属性" defaultOpen>
            <strong>type</strong> `string` <strong>（必选）</strong>

            媒体素材类型。可选值与模型有关：

            <Accordion title="仅传入参考图像" defaultOpen>
              支持模型：vidu/viduq3-ad\_reference2video、vidu/viduq3-drama\_reference2video、vidu/viduq3-mix\_reference2video、vidu/viduq3\_reference2video、vidu/viduq3-turbo\_reference2video、vidu/viduq2\_reference2video

              固定为：

              - `image`：参考图像。

              素材限制：图像数量为1～7张。
            </Accordion>

            <Accordion title="传入参考图像和视频" defaultOpen>
              支持模型：vidu/viduq2-pro\_reference2video

              可选值为：

              - `image`：参考图像。必选。
              - `video`：参考视频。可选。

              素材限制：

              - 仅参考图像：图像数量为1～7张。
              - 参考图像和视频：

                - 图像数量为1～4张。
                - 视频数量为1～2个。
            </Accordion>

            <strong>url</strong> `string` <strong>（必选）</strong>

            媒体素材URL。素材包括图像、视频。

            <Accordion title="传入图像（type=image）" defaultOpen>
              参考图像URL，必须为公网可访问的 URL。

              - 支持 HTTP 或 HTTPS 协议。
              - 示例值：<a href="https://xxx/xxx.png">[https://xxx/xxx.png](https://xxx/xxx.png)</a>。

              图像限制：

              - 格式：JPEG、JPG、PNG、WEBP。
              - 宽高比：1:4～4:1。
              - 文件大小：不超过 50MB。
            </Accordion>

            <Accordion title="传入视频（type=video）" defaultOpen>
              参考视频URL，必须为公网可访问的 URL。

              - 支持 HTTP 或 HTTPS 协议。
              - 示例值：<a href="https://xxx/xxx.mp4">[https://xxx/xxx.mp4](https://xxx/xxx.mp4)</a>。

              视频限制：

              - 格式：mp4、avi、mov。
              - 分辨率：总像素值不小于128\*128。
              - 宽高比：1:4～4:1。
              - 时长：传入1个参考视频时为1～8秒；传入2个参考视频时各为1～5秒。
              - 文件大小：不超过 50MB。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` <strong>（必选）</strong>

        视频生成参数。如设置视频分辨率、时长等。

        <Accordion title="属性" defaultOpen>
          <strong>resolution</strong> `string` （可选）

          <Tip>
            resolution直接影响费用，请在调用前确认[模型价格](/zh/model-studio/model-pricing#69993500dc6mi)。
          </Tip>

          生成视频的分辨率。可选值与模型有关：

          - vidu/viduq3-ad\_reference2video：可选720P、1080P。默认值为720P。
          - vidu/viduq3-drama\_reference2video：可选720P、1080P。默认值为1080P。
          - vidu/viduq3-mix\_reference2video：可选720P、1080P。默认值为720P。
          - vidu/viduq3\_reference2video：可选540P、720P、1080P。默认值为720P。
          - vidu/viduq3-turbo\_reference2video：可选540P、720P、1080P。默认值为720P。
          - vidu/viduq2-pro\_reference2video：可选540P、720P、1080P。默认值为720P。
          - vidu/viduq2\_reference2video：可选540P、720P、1080P。默认值为720P。

          <strong>size</strong> `string` （可选）

          生成视频的分辨率，格式为`宽*高`的像素值。

          默认值根据`resolution`而定：

          - 当 resolution=540P 时，size 默认为 `1024*576`。
          - 当 resolution=720P 时，size 默认为 `1280*720`。
          - 当 resolution=1080P 时，size 默认为 `1920*1080`。

          各模型支持的宽高比有所不同：

          - vidu/viduq3-drama\_reference2video：仅支持 16:9 和 9:16。
          - 其他模型：支持 16:9、4:3、1:1、3:4、9:16。

          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28.57%" }} /><col style={{ width: "28.57%" }} /><col style={{ width: "42.86%" }} /></colgroup><tbody><tr><td><p>分辨率档位</p></td><td><p>宽高比</p></td><td><p><strong>size 取值（宽\*高）</strong></p></td></tr><tr><td rowSpan={5}><p>540P</p></td><td><p>16:9</p></td><td><p>1024\*576</p></td></tr><tr><td><p>4:3</p></td><td><p>1024\*768</p></td></tr><tr><td><p>1:1</p></td><td><p>1024\*1024</p></td></tr><tr><td><p>3:4</p></td><td><p>768\*1024</p></td></tr><tr><td><p>9:16</p></td><td><p>576\*1024</p></td></tr><tr><td rowSpan={5}><p>720P</p></td><td><p>16:9</p></td><td><p>1280\*720</p></td></tr><tr><td><p>4:3</p></td><td><p>1280\*960</p></td></tr><tr><td><p>1:1</p></td><td><p>1280\*1280</p></td></tr><tr><td><p>3:4</p></td><td><p>960\*1280</p></td></tr><tr><td><p>9:16</p></td><td><p>720\*1280</p></td></tr><tr><td rowSpan={5}><p>1080P</p></td><td><p>16:9</p></td><td><p>1920\*1080</p></td></tr><tr><td><p>4:3</p></td><td><p>1920\*1440</p></td></tr><tr><td><p>1:1</p></td><td><p>1808\*1808</p></td></tr><tr><td><p>3:4</p></td><td><p>1440\*1920</p></td></tr><tr><td><p>9:16</p></td><td><p>1080\*1920</p></td></tr></tbody></table>

          <strong>duration</strong> `integer` <strong>（必选）</strong>

          <Tip>
            duration直接影响费用，按秒计费，请在调用前确认[模型价格](/zh/model-studio/model-pricing#69993500dc6mi)。
          </Tip>

          生成视频的时长，单位为秒。

          - vidu/viduq3-ad\_reference2video：取值为\[3, 15]之间的整数。默认值为`5`。
          - vidu/viduq3-drama\_reference2video：取值为\[2, 15]之间的整数。默认值为`5`。

            - 当分镜数量过多或明显少于duration时，模型可能自动调整时长以保障故事完整性，实际视频时长可能多于或少于duration设定值。
            - 计费按实际输出时长计算。
          - vidu/viduq3-mix\_reference2video：取值为\[1, 16]之间的整数。默认值为`5`。
          - vidu/viduq3\_reference2video：取值为\[1, 16]之间的整数。默认值为`5`。
          - vidu/viduq3-turbo\_reference2video：取值为\[1, 16]之间的整数。默认值为`5`。
          - vidu/viduq2-pro\_reference2video：取值为\[1, 10]之间的整数。默认值为`5`。

            - 特殊设置：支持设置为0，表示自动规划时长，上限不超过 10 秒。

              - 若上传 1 个参考视频：生成视频时长通常等于该参考视频时长。
              - 若上传 2 个参考视频：模型结合提示词，生成视频时长通常等于”主要参考视频”的时长。
          - vidu/viduq2\_reference2video：取值为\[1, 10]之间的整数。默认值为`5`。

          <strong>audio</strong> `boolean` （可选）

          <strong>支持模型</strong>：vidu/viduq3-ad\_reference2video、vidu/viduq3-mix\_reference2video、vidu/viduq3\_reference2video、vidu/viduq3-turbo\_reference2video。

          是否生成有声视频。开启后模型将根据视频内容自动生成匹配的背景音乐或音效。

          - `false`：默认值，输出无声视频。
          - `true`：输出有声视频。

          <Note>
            vidu/viduq3-drama\_reference2video 不支持该参数，默认输出有声视频。
          </Note>

          <strong>watermark</strong> `boolean` （可选）

          是否添加水印标识，水印位于视频右下角，文案固定为“内容由AI生成”。

          - `false`：默认值，不添加水印。
          - `true`：添加水印。

          <strong>seed</strong>`integer`（可选）

          随机数种子，取值范围为`[0, 2147483647]`。

          未指定时，系统自动生成随机种子。若需提升生成结果的可复现性，建议固定seed值。

          请注意，由于模型生成具有概率性，即使使用相同 seed，也不能保证每次生成结果完全一致。

          示例值：12345。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="参考生视频-广告（仅参考图像）">
            支持模型：vidu/viduq3-ad\_reference2video。

            ```bash expandable
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "vidu/viduq3-ad_reference2video",
                "input": {
                    "media": [
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260713/dtnctw/ad-1.png"
                        },
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260713/bspdpd/ad-2.png"
                        },
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260713/mhsjyy/ad-3.png"
                        }
                    ],
                    "prompt": "一个瑜伽裤的广告。商品是一条左右各有一个口袋的黑色高腰瑜伽裤。亚洲模特开心地说"我现在再也不穿没有口袋的瑜伽裤了"。画面保持清晰、稳定、容易理解。"
                },
                "parameters": {
                    "duration": 8,
                    "size": "1080*1920",
                    "resolution": "1080P",
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

            media = [{'type': 'image', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260713/dtnctw/ad-1.png'}, {'type': 'image', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260713/bspdpd/ad-2.png'}, {'type': 'image', 'url': 'https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260713/mhsjyy/ad-3.png'}]

            def sample_async_call():
                # 异步提交视频生成任务
                rsp = VideoSynthesis.async_call(api_key=api_key,
                                            model='vidu/viduq3-ad_reference2video',
                                            prompt='一个瑜伽裤的广告。商品是一条左右各有一个口袋的黑色高腰瑜伽裤。亚洲模特开心地说"我现在再也不穿没有口袋的瑜伽裤了"。画面保持清晰、稳定、容易理解。',
                                            media=media,
                                            size='1080*1920',
                                            resolution='1080P',
                                            duration=8,
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

          <Tab title="参考生视频-短剧（仅参考图像）">
            支持模型：vidu/viduq3-drama\_reference2video。

            ```bash expandable
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "vidu/viduq3-drama_reference2video",
                   "input": {
                    "media": [
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260713/eelcsu/daram-1.png"
                        },
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260713/adglrn/drama-2.png"
                        },
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260713/fmofkp/drama-3.png"
                        },
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260713/mktdht/drama-4.png"
                        }
                    ],
                    "prompt": "酒店宴会厅，王桂兰（图3）（在画面右侧，坐在席位上）苏文博（图1）、许向暖（图2）（在画面左侧远处，敬酒中）。婚礼现场座无虚席，坐满宾客。苏文博的母亲王桂兰坐在席位上，眉头紧紧皱起，面露不满。镜头固定，捕捉她皱眉的表情。镜头推进王桂兰面部，她眼神阴沉，嘴角下撇，带着嫌弃和不满。强调她的不悦。"
                },
                "parameters": {
                    "duration": 9,
                    "size": "1080*1920",
                    "resolution": "1080P",
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="参考生视频（仅参考图像）">
            ```bash expandable
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
               -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "vidu/viduq3-mix_reference2video",
                   "input": {
                    "media": [
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260320/knsple/wan-r2v-role-frame.jpg"
                        },
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png"
                        },
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png"
                        }
                    ],
                    "prompt": "男人坐在靠窗的椅子上，手持吉他，在咖啡厅旁演奏一首舒缓的美国乡村民谣"
                },
                "parameters": {
                    "duration": 5,
                    "size": "1280*720",
                    "resolution": "720P",
                    "watermark": true
                }
            }'
            ```
          </Tab>

          <Tab title="参考生视频（参考图像+视频）">
            支持模型：vidu/viduq2-pro\_reference2video。

            ```bash expandable
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
                -H 'X-DashScope-Async: enable' \
               -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                "model": "vidu/viduq2-pro_reference2video",
                   "input": {
                    "media": [
                        {
                            "type": "video",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qigswt/wan-r2v-role2.mp4"
                        },
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/qpzxps/wan-r2v-object4.png"
                        },
                        {
                            "type": "image",
                            "url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20260129/wfjikw/wan-r2v-backgroud5.png"
                        }
                    ],
                    "prompt": "男人坐在靠窗的椅子上，手持吉他，在咖啡厅旁演奏一首舒缓的美国乡村民谣"
                },
                "parameters": {
                    "duration": 5,
                    "size": "1280*720",
                    "resolution": "720P",
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
        #### 响应参数 <span id="pvr379a0h4rsp1" />

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

### 步骤 2：根据任务 ID 查询结果 <span id="pvr411a0h4stp2" />

<strong>北京地域</strong>：`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

<Note>
  - <strong>轮询建议</strong>：视频生成过程约需数分钟，建议采用<strong>轮询</strong>机制，并设置合理的查询间隔（如 15 秒）来获取结果。
  - <strong>任务状态流转</strong>：PENDING（排队中）→ RUNNING（处理中）→ SUCCEEDED（成功）/ FAILED（失败）。
  - <strong>task\_id 有效期</strong>：<strong>24 小时</strong>，超时后将无法查询结果，接口将返回任务状态为`UNKNOWN`。
  - <strong>RPS 限制</strong>：查询接口默认 RPS 为 20。如需更高频查询或事件通知，建议[配置异步任务回调](/zh/model-studio/async-task-api)。
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
        #### 请求参数 <span id="pvr424a0h4qr01" />

        ##### 请求头（Headers） <span id="pvr434a0h5qr01" />

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### URL 路径参数（Path parameters） <span id="pvr440a0h5qr02" />

        <strong>task\_id</strong> `string`<strong>（必选）</strong>

        任务ID。
      </td>

      <td>
        <Tabs>
          <Tab title="查询任务结果">
            将`{task_id}`完整替换为上一步接口返回的`task_id`的值。`task_id`查询有效期为24小时

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
        #### 响应参数 <span id="pvr448a0h4qrs1" />

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

          视频 URL。仅在 task\_status 为 SUCCEEDED 时返回。

          视频格式为 MP4（H.264 编码）。视频链接有效期24 小时，请及时下载。

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

          生成视频的分辨率。

          <strong>fps</strong> `integer`

          生成视频的帧率。固定为 24。

          <strong>SR</strong> `string`

          生成视频的分辨率档位。

          <strong>audio</strong> `boolean`

          生成视频是否为有声视频。

          <strong>video\_count</strong> `integer`

          生成视频的数量。固定为 1。

          <strong>reference\_type</strong> `string`

          参考素材类型。
        </Accordion>

        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <Tabs>
          <Tab title="任务执行成功">
            视频URL仅保留24小时，超时后会被自动清除，请及时保存生成的视频。

            ```json
            {
                "request_id": "8f240644-efe8-43bf-86ff-xxxxxx",
                "output": {
                    "task_id": "b7e05baa-c318-440d-b293-xxxxxx",
                    "task_status": "SUCCEEDED",
                    "submit_time": "2026-03-27 15:10:42.723",
                    "scheduled_time": "2026-03-27 15:10:42.754",
                    "end_time": "2026-03-27 15:11:17.388",
                    "orig_prompt": "男人坐在靠窗的椅子上，手持吉他，在咖啡厅旁演奏一首舒缓的美国乡村民谣",
                    "video_url": "https://prod-ss-vidu.s3.cn-northwest-1.amazonaws.com.cn/xxx.mp4?xxx"
                },
                "usage": {
                    "duration": 5,
                    "size": "960*528",
                    "output_video_duration": 5,
                    "fps": 24,
                    "video_count": 1,
                    "audio": false,
                    "reference_type": "image,video",
                    "SR": "540"
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

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="a0379eaad3lho" />

#### Q：size 与 resolution 必须同时传入吗？ <span id="d64acbbcf61fn" />

<strong>A：</strong> 不必。二者均为可选参数，但<strong>推荐同时传入</strong>。同时传入可精准控制生成视频的宽高比，具体参见[size取值](/zh/model-studio/vidu-reference-to-video-api-reference)。

若不同时传入，系统将按以下两种场景处理：

- <strong>仅传 size</strong>：size参数会被忽略，系统强制按默认的 `resolution=720P` 及其对应的`size`默认值（`1280*720`）处理。

  > 例如：接口返回为size="1280\*720", SR=720。
- <strong>仅传 resolution</strong>：按指定分辨率档位及该档位对应的 16:9 比例输出。

  > 例如：当resolution=540P时，接口返回 size="960*528", SR=540；当resolution=1080P时，接口返回 size="1920*1080", SR=1080。

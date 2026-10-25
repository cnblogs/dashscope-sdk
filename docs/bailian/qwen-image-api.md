> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 千问-文生图API参考

> 千问-文生图模型（Qwen-Image）是一款通用图像生成模型，支持多种艺术风格，尤其擅长 复杂文本渲染 。模型支持多行布局、段落级文本生成以及细粒度细节刻画，可实现复杂的图文混合布局设计。

<meta property="og:title" content="千问-文生图API参考" />

<table><tbody><tr><td><p><strong>快速入口：</strong><Link href="/zh/model-studio/text-to-image">使用指南</Link> | 在线体验（<Link href="https://bailian.console.aliyun.com/cn-beijing/model/experience/vision/imageGenerate?modelId=qwen-image-2.0-pro">北京</Link> | <Link href="https://bailian.console.aliyun.com/ap-southeast-1/model/experience/vision/imageGenerate?modelId=qwen-image-2.0-pro">新加坡</Link>） | <Link href="https://qwen.ai/blog?id=9467b4bff9c638e847f08443802c6b96ab116a87&from=research.research-list">技术博客</Link></p></td></tr></tbody></table>

## 效果展示 <span id="5849834e92mb7" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>输入提示词</strong></p></th><th><p><strong>输出图像</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>冬日北京的都市街景，青灰瓦顶、朱红色外墙的两间相邻中式商铺比肩而立，檐下悬挂印有剪纸马的暖光灯笼，在阴天漫射光中投下柔和光晕，映照湿润鹅卵石路面泛起细腻反光。左侧为书法店：靛蓝色老旧的牌匾上以遒劲行书刻着“文字渲染”。店门口的玻璃上挂着一幅字，自上而下，用田英章硬笔写着“专业幻灯片 中英文海报 高级信息图”，落款印章为“1k token”朱砂印。店内的墙上，可以模糊的辨认有三幅竖排的书法作品，第一幅写着“阿里巴巴”，第二幅写着“通义千问”，第三幅写着“图像生成”。一位白发苍苍的老人背对着镜头观赏。右侧为花店，牌匾上以鲜花做成文字“真实质感”；店内多层花架陈列红玫瑰、粉洋牡丹和绿植，门上贴了一个圆形花边标识，标识上写着“2k resolution”，门口摆放了一个彩色霓虹灯，上面写着“细腻刻画 人物 自然 建筑”。两家店中间堆放了一个雪人，举了一老式小黑板，上面用粉笔字写着“Qwen-Image-2.0 正式发布”。街道左侧，年轻情侣依偎在一起，女孩是瘦脸，身穿米白色羊绒大衣，肉色光腿神器。女孩举着心形透明气球，气球印有白色的字：“生图编辑二合一”。里面有一个毛茸茸的卡皮巴拉玩偶。男孩身着剪裁合体的深灰色呢子外套，内搭浅色高领毛衣。街道右侧，一个后背上写着“更小模型，更快速度”的骑手疾驰而过。整条街光影交织、动静相宜。</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9373463771/p1058343.png" alt="image (10)" width={524.995} height={300} /></p></td></tr></tbody></table>

## 模型概览 <span id="4a3e46b825d2k" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "24%" }} />

    <col style={{ width: "38%" }} />

    <col style={{ width: "38%" }} />
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
        <strong>输出图像规格</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        qwen-image-2.0-pro `推荐`

        > 当前与qwen-image-2.0-pro-2026-04-22能力相同
      </td>

      <td rowSpan={4}>
        千问图像生成与编辑模型Pro系列。文字渲染、真实质感、语义遵循能力更强。

        > 图像编辑请参考[千问-图像编辑](/zh/model-studio/qwen-image-edit-api)。
      </td>

      <td rowSpan={6}>
        图像分辨率：支持自由设置宽高，输出图像总像素需在512*512至2048*2048之间。默认分辨率为2048\*2048。

        图像格式：png

        图像张数：1-6张
      </td>
    </tr>

    <tr>
      <td>
        qwen-image-2.0-pro-2026-06-22 `推荐`
      </td>
    </tr>

    <tr>
      <td>
        qwen-image-2.0-pro-2026-04-22
      </td>
    </tr>

    <tr>
      <td>
        qwen-image-2.0-pro-2026-03-03
      </td>
    </tr>

    <tr>
      <td>
        qwen-image-2.0 `推荐`

        > 当前与qwen-image-2.0-2026-03-03能力相同
      </td>

      <td rowSpan={2}>
        千问图像生成与编辑模型加速版，兼顾效果与响应速度。

        > 图像编辑请参考[千问-图像编辑](/zh/model-studio/qwen-image-edit-api)。
      </td>
    </tr>

    <tr>
      <td>
        qwen-image-2.0-2026-03-03 `推荐`
      </td>
    </tr>
  </tbody>
</table>

各地域支持的模型请参见[百炼控制台](https://bailian.console.aliyun.com/cn-beijing/model/market)。

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

## 同步接口 <span id="39337f0fd0wzt" />

### HTTP调用 <span id="90575c8228nmq" />

千问图像模型支持同步接口，一次请求即可获得结果，调用流程简单，推荐用于多数场景。

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

<strong>新加坡地域</strong>：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求参数 <span id="576ce84612n7u" />

        ##### 请求头（Headers） <span id="bb0a157056fbk" />

        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。

        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。

        ##### 请求体（Request Body） <span id="45d1fa2260zn6" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        模型名称。示例值：`qwen-image-2.0-pro`。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息。

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。<strong>当前仅支持单轮对话</strong>，数组内<strong>有且只有一个元素</strong>。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string` <strong>（必选）</strong>

            消息的角色。此参数必须设置为`user`。

            <strong>content</strong>`array` <strong>（必选）</strong>

            消息内容数组。

            <Accordion title="属性" defaultOpen>
              <strong>text</strong>`string`<strong>（必选）</strong>

              正向提示词用于描述您期望生成的图像内容、风格和构图。

              支持中英文，qwen-image-2.0系列模型长度上限为 1300 Token，其他模型为 800 Token，超出部分将自动截断。

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

          <strong>qwen-image-2.0系列模型</strong>：输出图像总像素需在`512*512`至`2048*2048`之间，默认分辨率为`2048*2048`。推荐分辨率：

          - `2688*1536` ：16:9
          - `1536*2688` ：9:16
          - `2048*2048`（<strong>默认值）</strong>：1:1
          - `2368*1728` ：4:3
          - `1728*2368` ：3:4

          <strong>n</strong> `integer` （可选）

          输出图像的数量，默认值为1。

          对于qwen-image-2.0系列模型，可选择输出1-6张图片。

          <strong>prompt\_extend</strong> `bool` （可选）

          是否开启 Prompt（提示词）智能改写功能。开启后模型将对正向提示词进行优化与润色。此功能不会修改反向提示词。

          - `true`：<strong>默认值</strong>，开启智能改写。如果希望图像内容更多样化，由模型补充细节，建议开启此选项。
          - `false`：关闭智能改写。如果图像细节更可控，建议关闭此选项，并参考[文生图Prompt指南](/zh/model-studio/text-to-image-prompt)进行优化，

          <Accordion title="点击查看改写示例">
            <strong>原始提示词（orig\_prompt）</strong>：一只坐着的橘黄色的猫，表情愉悦，活泼可爱，逼真准确。

            <strong>实际提示词（actual\_prompt）</strong>：一只坐着的橘黄色猫咪，毛发蓬松柔软，阳光透过窗户洒在它身上，呈现出温暖的光泽。猫咪体型匀称，四肢自然弯曲，稳稳地坐在木质地板上，尾巴轻轻卷曲在身侧，显得格外放松而优雅。它的大眼睛圆润明亮，瞳孔微微收缩，流露出愉悦而灵动的神情，嘴角微扬，仿佛正享受着美好的时光。耳朵微微向前倾斜，透露出活泼与好奇。背景是一间温馨的现代家居客厅，浅色木地板、一扇半开的窗户透进柔和的自然光，窗外可见绿意盎然的庭院，窗台上摆放着几盆绿植。画面采用真实摄影风格，细节逼真，光影层次丰富，突出猫咪的毛发质感、眼神神态与整体姿态的生动自然，整体氛围轻松愉快，充满生活气息。
          </Accordion>

          <strong>watermark</strong> `bool` （可选）

          是否在图像右下角添加 "Qwen-Image" 水印。默认值为 `false`。水印样式：<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8972029571/p1012089.jpg" alt="1" className="inline" noZoom width={80.293} height={24} />

          <strong>seed</strong> `integer` （可选）

          随机数种子，取值范围`[0,2147483647]`。

          使用相同的`seed`参数值可使生成内容保持相对稳定。若不提供，算法将自动使用随机数种子。

          <strong>注意</strong>：模型生成过程具有概率性，即使使用相同的`seed`，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <Tabs>
          <Tab title="文生图">
            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --data '{
                "model": "qwen-image-2.0-pro",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "text": "冬日北京的都市街景，青灰瓦顶、朱红色外墙的两间相邻中式商铺比肩而立，檐下悬挂印有剪纸马的暖光灯笼，在阴天漫射光中投下柔和光晕，映照湿润鹅卵石路面泛起细腻反光。左侧为书法店：靛蓝色老旧的牌匾上以遒劲行书刻着“文字渲染”。店门口的玻璃上挂着一幅字，自上而下，用田英章硬笔写着“专业幻灯片 中英文海报 高级信息图”，落款印章为“1k token”朱砂印。店内的墙上，可以模糊的辨认有三幅竖排的书法作品，第一幅写着“阿里巴巴”，第二幅写着“通义千问”，第三幅写着“图像生成”。一位白发苍苍的老人背对着镜头观赏。右侧为花店，牌匾上以鲜花做成文字“真实质感”；店内多层花架陈列红玫瑰、粉洋牡丹和绿植，门上贴了一个圆形花边标识，标识上写着“2k resolution”，门口摆放了一个彩色霓虹灯，上面写着“细腻刻画 人物 自然 建筑”。两家店中间堆放了一个雪人，举了一老式小黑板，上面用粉笔字写着“Qwen-Image-2.0 正式发布”。街道左侧，年轻情侣依偎在一起，女孩是瘦脸，身穿米白色羊绒大衣，肉色光腿神器。女孩举着心形透明气球，气球印有白色的字：“生图编辑二合一”。里面有一个毛茸茸的卡皮巴拉玩偶。男孩身着剪裁合体的深灰色呢子外套，内搭浅色高领毛衣。街道右侧，一个后背上写着“更小模型，更快速度”的骑手疾驰而过。整条街光影交织、动静相宜。"
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "negative_prompt": "低分辨率，低画质，肢体畸形，手指畸形，画面过饱和，蜡像感，人脸无细节，过度光滑，画面具有AI感。构图混乱。文字模糊，扭曲。",
                    "prompt_extend": true,
                    "watermark": false,
                    "size": "2048*2048"
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
        #### 响应参数 <span id="b7e833ee1f962" />

        <strong>output</strong> `object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>choices</strong> `array`

          模型生成的输出内容。此数组仅包含一个元素。

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
              </Accordion>
            </Accordion>
          </Accordion>

          <strong>task\_metric</strong> `object`

          任务结果统计。使用qwen-image-2.0系列时无此返回值。

          <Accordion title="属性" defaultOpen>
            <strong>TOTAL</strong> `integer`

            总的任务数。

            <strong>SUCCEEDED</strong> `integer`

            任务状态为成功的任务数。

            <strong>FAILED</strong> `integer`

            任务状态为失败的任务数。
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> `integer`

          模型生成图像的数量，当前固定为1。

          <strong>width</strong> `integer`

          模型生成图像的宽度（像素）。

          <strong>height</strong> `integer`

          模型生成图像的高度（像素）。
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
            图像URL仅保留24小时，超时后会被自动清除，请及时保存生成的图像。

            ```json
            {
                "output": {
                    "choices": [
                        {
                            "finish_reason": "stop",
                            "message": {
                                "content": [
                                    {
                                        "image": "https://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.png?Expires=xxx"
                                    }
                                ],
                                "role": "assistant"
                            }
                        }
                    ]
                },
                "usage": {
                    "height": 2048,
                    "image_count": 1,
                    "width": 2048
                },
                "request_id": "d0250a3d-b07f-49e1-bdc8-6793f4929xxx"
            }
            ```
          </Tab>

          <Tab title="任务执行异常">
            如果因为某种原因导致任务执行失败，将返回相关信息，可以通过code和message字段明确指示错误原因。请参见[错误码](/zh/model-studio/error-code)进行解决。

            ```json
            {
                "request_id": "a4d78a5f-655f-9639-8437-xxxxxx",
                "code": "InvalidParameter",
                "message": "n must be 1"
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

---

### DashScope SDK调用 <span id="bc5bea44cab1b" />

DashScope SDK目前已支持Python和Java。

SDK与HTTP接口的参数名基本一致，参数结构根据语言特性进行封装。同步调用参数说明可参考[HTTP调用](/zh/model-studio/qwen-image-api#90575c8228nmq)。

<Tabs>
  <Tab title="Python">
    <Note>
      请先确认已安装最新版DashScope Python SDK，否则可能运行报错：[安装SDK](/zh/model-studio/install-sdk)。
    </Note>

    ##### 请求示例 <span id="3dcaa20713o8e" />

    ```python expandable
    import json
    import os
    import dashscope
    from dashscope import MultiModalConversation

    # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
    dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

    messages = [
        {
            "role": "user",
            "content": [
                {"text": "冬日北京的都市街景，青灰瓦顶、朱红色外墙的两间相邻中式商铺比肩而立，檐下悬挂印有剪纸马的暖光灯笼，在阴天漫射光中投下柔和光晕，映照湿润鹅卵石路面泛起细腻反光。左侧为书法店：靛蓝色老旧的牌匾上以遒劲行书刻着“文字渲染”。店门口的玻璃上挂着一幅字，自上而下，用田英章硬笔写着“专业幻灯片 中英文海报 高级信息图”，落款印章为“1k token”朱砂印。店内的墙上，可以模糊的辨认有三幅竖排的书法作品，第一幅写着“阿里巴巴”，第二幅写着“通义千问”，第三幅写着“图像生成”。一位白发苍苍的老人背对着镜头观赏。右侧为花店，牌匾上以鲜花做成文字“真实质感”；店内多层花架陈列红玫瑰、粉洋牡丹和绿植，门上贴了一个圆形花边标识，标识上写着“2k resolution”，门口摆放了一个彩色霓虹灯，上面写着“细腻刻画 人物 自然 建筑”。两家店中间堆放了一个雪人，举了一老式小黑板，上面用粉笔字写着“Qwen-Image-2.0 正式发布”。街道左侧，年轻情侣依偎在一起，女孩是瘦脸，身穿米白色羊绒大衣，肉色光腿神器。女孩举着心形透明气球，气球印有白色的字：“生图编辑二合一”。里面有一个毛茸茸的卡皮巴拉玩偶。男孩身着剪裁合体的深灰色呢子外套，内搭浅色高领毛衣。街道右侧，一个后背上写着“更小模型，更快速度”的骑手疾驰而过。整条街光影交织、动静相宜。"}
            ]
        }
    ]

    # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    api_key = os.getenv("DASHSCOPE_API_KEY")

    response = MultiModalConversation.call(
        api_key=api_key,
        model="qwen-image-2.0-pro",
        messages=messages,
        result_format='message',
        stream=False,
        watermark=False,
        prompt_extend=True,
        negative_prompt="低分辨率，低画质，肢体畸形，手指畸形，画面过饱和，蜡像感，人脸无细节，过度光滑，画面具有AI感。构图混乱。文字模糊，扭曲。",
        size='2048*2048'
    )

    if response.status_code == 200:
        print(json.dumps(response, ensure_ascii=False))
    else:
        print(f"HTTP返回码：{response.status_code}")
        print(f"错误码：{response.code}")
        print(f"错误信息：{response.message}")
        print("请参考文档：https://help.aliyun.com/zh/model-studio/developer-reference/error-code")
    ```

    ##### 响应示例 <span id="f60171c71fg3t" />

    > 图像链接的有效期为24小时，请及时下载图像。

    ```json expandable
    {
        "status_code": 200,
        "request_id": "d2d1a8c0-325f-9b9d-8b90-xxxxxx",
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
                                "image": "https://dashscope-result-wlcb.oss-cn-wulanchabu.aliyuncs.com/xxx.png?Expires=xxx"
                            }
                        ]
                    }
                }
            ]
        },
        "usage": {
            "input_tokens": 0,
            "output_tokens": 0,
            "width": 2048,
            "image_count": 1,
            "height": 2048
        }
    }
    ```
  </Tab>

  <Tab title="Java">
    <Note>
      请先确认已安装最新版DashScope Java SDK，否则可能运行报错：[安装SDK](/zh/model-studio/install-sdk)。
    </Note>

    ##### 请求示例 <span id="d137cf544fnw6" />

    ```java expandable
    import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
    import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
    import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
    import com.alibaba.dashscope.common.MultiModalMessage;
    import com.alibaba.dashscope.common.Role;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import com.alibaba.dashscope.utils.JsonUtils;

    import java.io.IOException;
    import java.util.Arrays;
    import java.util.Collections;
    import java.util.HashMap;
    import java.util.Map;

    public class QwenImage {

        static {
            // 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
            Constants.baseHttpApiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1";
        }

        // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        // 若没有配置环境变量，请用百炼API Key将下行替换为：static String apiKey ="sk-xxx"
        static String apiKey = System.getenv("DASHSCOPE_API_KEY");

        public static void call() throws ApiException, NoApiKeyException, UploadFileException, IOException {

            MultiModalConversation conv = new MultiModalConversation();

            MultiModalMessage userMessage = MultiModalMessage.builder().role(Role.USER.getValue())
                    .content(Arrays.asList(
                            Collections.singletonMap("text", "冬日北京的都市街景，青灰瓦顶、朱红色外墙的两间相邻中式商铺比肩而立，檐下悬挂印有剪纸马的暖光灯笼，在阴天漫射光中投下柔和光晕，映照湿润鹅卵石路面泛起细腻反光。左侧为书法店：靛蓝色老旧的牌匾上以遒劲行书刻着“文字渲染”。店门口的玻璃上挂着一幅字，自上而下，用田英章硬笔写着“专业幻灯片 中英文海报 高级信息图”，落款印章为“1k token”朱砂印。店内的墙上，可以模糊的辨认有三幅竖排的书法作品，第一幅写着“阿里巴巴”，第二幅写着“通义千问”，第三幅写着“图像生成”。一位白发苍苍的老人背对着镜头观赏。右侧为花店，牌匾上以鲜花做成文字“真实质感”；店内多层花架陈列红玫瑰、粉洋牡丹和绿植，门上贴了一个圆形花边标识，标识上写着“2k resolution”，门口摆放了一个彩色霓虹灯，上面写着“细腻刻画 人物 自然 建筑”。两家店中间堆放了一个雪人，举了一老式小黑板，上面用粉笔字写着“Qwen-Image-2.0 正式发布”。街道左侧，年轻情侣依偎在一起，女孩是瘦脸，身穿米白色羊绒大衣，肉色光腿神器。女孩举着心形透明气球，气球印有白色的字：“生图编辑二合一”。里面有一个毛茸茸的卡皮巴拉玩偶。男孩身着剪裁合体的深灰色呢子外套，内搭浅色高领毛衣。街道右侧，一个后背上写着“更小模型，更快速度”的骑手疾驰而过。整条街光影交织、动静相宜。")
                    )).build();

            Map<String, Object> parameters = new HashMap<>();
            parameters.put("watermark", false);
            parameters.put("prompt_extend", true);
            parameters.put("negative_prompt", "低分辨率，低画质，肢体畸形，手指畸形，画面过饱和，蜡像感，人脸无细节，过度光滑，画面具有AI感。构图混乱。文字模糊，扭曲。");
            parameters.put("size", "2048*2048");

            MultiModalConversationParam param = MultiModalConversationParam.builder()
                    .apiKey(apiKey)
                    .model("qwen-image-2.0-pro")
                    .messages(Collections.singletonList(userMessage))
                    .parameters(parameters)
                    .build();

            MultiModalConversationResult result = conv.call(param);
            System.out.println(JsonUtils.toJson(result));
        }

        public static void main(String[] args) {
            try {
                call();
            } catch (ApiException | NoApiKeyException | UploadFileException | IOException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```

    ##### 响应示例 <span id="9631851c87t5h" />

    > 图像链接的有效期为24小时，请及时下载图像。

    ```json
    {
        "requestId": "5b6f2d04-b019-40db-a5cc-xxxxxx",
        "usage": {
            "image_count": 1,
            "width": 2048,
            "height": 2048
        },
        "output": {
            "choices": [
                {
                    "finish_reason": "stop",
                    "message": {
                        "role": "assistant",
                        "content": [
                            {
                                "image": "https://dashscope-result-wlcb.oss-cn-wulanchabu.aliyuncs.com/xxx.png?Expires=xxx"
                            }
                        ]
                    }
                }
            ]
        }
    }
    ```
  </Tab>
</Tabs>

## 结合 OpenAI Agents SDK 使用 <span id="oaisdk00h2t" />

千问-文生图（Qwen-Image）等图像生成模型通过 DashScope 原生接口调用，不支持 OpenAI 兼容（compatible-mode）模式，因此无法直接作为 OpenAI Agents SDK 中 Agent 的推理模型。

在 OpenAI Agents SDK 等智能体框架中，可将图像生成能力封装为工具（function tool），由支持 OpenAI 兼容模式的文本对话模型（如 qwen-plus）作为 Agent 的推理核心进行调度。示例如下：

运行前请先安装依赖：`pip install openai-agents dashscope`。

```python expandable
import os
import asyncio
import dashscope
from openai import AsyncOpenAI
from dashscope import MultiModalConversation
from agents import Agent, Runner, function_tool, OpenAIChatCompletionsModel

# 从环境变量读取百炼 API Key（若未配置，可直接替换为 api_key="sk-xxx"）
API_KEY = os.getenv("DASHSCOPE_API_KEY")

# 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
dashscope.base_http_api_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"
# 文本对话模型（Agent 推理核心）通过 OpenAI 兼容模式接入
client = AsyncOpenAI(api_key=API_KEY, base_url="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/compatible-mode/v1")

# 将图像生成封装为工具（function tool），供 Agent 调用
@function_tool
def generate_image(prompt: str) -> str:
    """根据文本描述生成一张图片并返回图片 URL。prompt：图片内容的文字描述。"""
    rsp = MultiModalConversation.call(
        api_key=API_KEY,
        model="qwen-image-2.0-pro",
        messages=[{"role": "user", "content": [{"text": prompt}]}],
        result_format="message",
    )
    if rsp.status_code != 200:
        return f"生成失败：{rsp.code} {rsp.message}"
    return rsp.output.choices[0].message.content[0]["image"]

agent = Agent(
    name="图像生成助手",
    instructions="你是图像生成助手。当用户想要图片时，调用 generate_image 工具，并把返回的图片 URL 告诉用户。",
    model=OpenAIChatCompletionsModel(model="qwen-plus", openai_client=client),
    tools=[generate_image],
)

async def main():
    result = await Runner.run(agent, "帮我画一只在草地上奔跑的柯基犬")
    print(result.final_output)

if __name__ == "__main__":
    asyncio.run(main())
```

## 计费与限流 <span id="aa1d5dd501v1b" />

- 模型免费额度和计费单价请参见[模型价格](/zh/model-studio/model-pricing#2e0ddf5c9edy0)。
- 模型限流请参见[千问（Qwen-Image）](/zh/model-studio/rate-limit#f812e7c63axvx)。
- 计费说明：按成功生成的 <strong>图像张数</strong> 计费。模型调用失败或处理错误不产生任何费用，也不消耗[新人免费额度](/zh/model-studio/new-free-quota)。

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="2000000003510" />

#### Q：prompt\_extend参数应该开启还是关闭？ <span id="2000000000135" />

A：如果希望图像内容更多样化，由模型补充细节，建议开启此选项（默认）。如果图像细节更可控，建议关闭此选项，并参考[文生图Prompt指南](/zh/model-studio/text-to-image-prompt)进行优化，

#### Q：qwen-image-2.0系列模型有什么区别？ <span id="8c22963d65qr5" />

A：

- <strong>图像生成与编辑融合模型：</strong>同时支持文生图和图像编辑。

  - `qwen-image-2.0-pro`、`qwen-image-2.0-pro-2026-04-22`：当前两者能力相同，Pro系列具备更专业的文字渲染能力、更细腻的真实质感，细腻刻画写实场景，以及更强的语义遵循能力。仅支持同步接口。
  - `qwen-image-2.0`、`qwen-image-2.0-2026-03-03`：当前两者能力相同，加速版有效实现了模型效果和性能的最佳平衡。仅支持同步接口。

### Q：如何获取图像存储的访问域名白名单？ <span id="5fb153423ct8z" />

A： 模型生成的图像存储于阿里云OSS，API将返回一个临时的公网URL（链接有效期为 24 小时，请及时下载保存）。图片URL的域名格式为`dashscope-{标识}.oss-accelerate.aliyuncs.com`或`dashscope-{标识}.oss-cn-{地域}.aliyuncs.com`。<strong>若需要对该下载地址进行防火墙白名单配置</strong>，请注意：由于底层存储会动态变更，以下bucket名称仅供参考，可能随时更新：dashscope-a717、dashscope-66f3、dashscope-7c2c、dashscope-2522、dashscope-c72b、dashscope-0484、dashscope-7e0f、dashscope-5859、dashscope-5496、dashscope-35f9、dashscope-31d9、dashscope-7f1f、dashscope-cc75、dashscope-64e9。为避免过期信息影响访问，文档不提供固定的OSS域名白名单。如有安全管控需求，请联系客户经理获取最新OSS域名列表。API当前仅支持返回URL格式，不支持base64格式输出。

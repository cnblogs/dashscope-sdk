> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Z-Image API参考

> Z-Image 是一款轻量级文生图模型，可快速生成图像，支持中英文字渲染，并灵活适配多种分辨率与宽高比例。

<meta property="og:title" content="Z-Image API参考" />

<strong>快速入口</strong>：在线体验（[北京](https://bailian.console.aliyun.com/cn-beijing/model/experience/vision/imageGenerate?modelId=z-image-turbo) | [新加坡](https://bailian.console.aliyun.com/ap-southeast-1/model/experience/vision/imageGenerate?modelId=z-image-turbo)） | [技术博客](https://tongyi-mai.github.io/Z-Image-blog/)

## 效果展示 <span id="5849834e92mb7" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50.04%" }} /><col style={{ width: "49.96%" }} /></colgroup><thead><tr><th><p><strong>输入提示词</strong></p></th><th><p><strong>输出图像</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>film grain, analog film texture, soft film lighting, Kodak Portra 400 style, cinematic grainy texture, photorealistic details, subtle noise, (film grain:1.2)。采用近景特写镜头拍摄的东亚年轻女性，呈现户外雪地场景。她体型纤瘦，呈站立姿势，身体微微向右侧倾斜，头部抬起看向画面上方，姿态自然放松。她的面部是典型东亚长相，肤色白皙，脸颊带有自然的红润感，五官清秀：眼睛是深棕色，眼型偏圆，眼神略带惊讶地望向上方，眼白部分可见；眉毛是深黑色，形状自然弯长；鼻子小巧挺直，嘴唇涂有红色口红，唇瓣微张，表情带着轻微的惊讶或好奇。她的头发是深黑色长直发，发丝被风吹得略显凌乱，部分垂在脸颊两侧，头顶佩戴一顶深灰色的头盔，头盔边缘露出少量发丝。服装是蓝白拼接的厚重外套，外套材质看起来是毛绒与布料结合，显得温暖厚实，适合雪地环境。背景是被白雪覆盖的户外场景，远处可见模糊的树木轮廓，天空是明亮的浅蓝色，带有少量白云，光线是强烈的自然日光，照亮人物面部与头发，形成清晰的光影，色调以蓝、白、黑为主，整体风格清新自然。画面顶部有黑色提示框，内有“<strong>Press esc to exit full screen</strong>”的白色文字。镜头的近景视角放大了人物的表情与细节，营造出户外雪地的真实氛围。</p></td><td style={{ verticalAlign: "top" }}><p><Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/6915736671/p1036890.png" alt="image" width={350} /></p></td></tr></tbody></table>

## 模型概览 <span id="4a3e46b825d2k" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24.06%" }} /><col style={{ width: "28.21%" }} /><col style={{ width: "47.73%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>模型简介</strong></p></th><th><p><strong>输出图像规格</strong></p></th></tr></thead><tbody><tr><td><p>z-image-turbo</p></td><td><p>轻量模型，快速生图</p></td><td><p>图像分辨率：总像素在\[512\*512, 2048\*2048]之间，推荐分辨率请参见<a href="/zh/model-studio/z-image-api-reference">size参数设置</a></p><p>图像格式：png</p><p>图像张数：固定1张</p></td></tr></tbody></table>

<Note>
  调用前，请查阅各地域支持的模型列表。
</Note>

## 前提条件 <span id="b0dc1cbdbfd9g" />

您需要[获取与配置 API Key](/zh/model-studio/get-api-key)，并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## HTTP同步调用 <span id="39337f0fd0wzt" />

<strong>北京地域</strong>：`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

<strong>新加坡地域</strong>：`POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation`

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

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

        模型名称。必须为：z-image-turbo。

        <strong>input</strong> `object` <strong>（必选）</strong>

        输入的基本信息。

        <Accordion title="属性" defaultOpen>
          <strong>messages</strong> `array` <strong>（必选）</strong>

          请求内容数组。当前<strong>仅支持单轮对话</strong>，即传入一组role、content参数，不支持多轮对话。

          <Accordion title="属性" defaultOpen>
            <strong>role</strong>`string` <strong>（必选）</strong>

            消息的角色。此参数必须设置为`user`。

            <strong>content</strong>`array` <strong>（必选）</strong>

            消息内容数组。必须包含且仅包含<strong>1 个 text 对象</strong>。

            <Accordion title="属性" defaultOpen>
              <strong>text</strong>`string`<strong>（必选）</strong>

              正向提示词用于描述期望生成的图像内容、风格和构图。

              支持中英文，长度不超过800个字符，每个汉字、字母、数字或符号计为一个字符，超过部分会自动截断。

              示例值：一只坐着的橘黄色的猫，表情愉悦，活泼可爱，逼真准确。

              <strong>注意</strong>：仅支持传入一个text，不传或传入多个将报错。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        图像处理参数。

        <Accordion title="属性" defaultOpen>
          <strong>size</strong> `string` （可选）

          输出图像的分辨率，格式为`宽*高`。

          - 默认值：`1024*1536`。
          - 总像素范围限制：总像素在 \[512*512, 2048*2048]之间。
          - 推荐分辨率范围：总像素在 \[1024*1024, 1536*1536]之间，出图效果更佳。

          示例值：1024\*1536。

          <Accordion title="总像素为1024*1024的推荐分辨率：" defaultOpen>
            - 1:1: 1024\*1024
            - 2:3: 832\*1248
            - 3:2: 1248\*832
            - 3:4: 864\*1152
            - 4:3: 1152\*864
            - 7:9: 896\*1152
            - 9:7: 1152\*896
            - 9:16: 720\*1280
            - 9:21: 576\*1344
            - 16:9: 1280\*720
            - 21:9：1344\*576
          </Accordion>

          <Accordion title="总像素为1280*1280的推荐分辨率：" defaultOpen>
            - 1:1: 1280\*1280
            - 2:3: 1024\*1536
            - 3:2: 1536\*1024
            - 3:4: 1104\*1472
            - 4:3: 1472\*1104
            - 7:9: 1120\*1440
            - 9:7: 1440\*1120
            - 9:16: 864\*1536
            - 9:21: 720\*1680
            - 16:9: 1536\*864
            - 21:9: 1680\*720
          </Accordion>

          <Accordion title="总像素为1536*1536的推荐分辨率：" defaultOpen>
            - 1:1：1536\*1536
            - 2:3: 1248\*1872
            - 3:2: 1872\*1248
            - 3:4: 1296\*1728
            - 4:3: 1728\*1296
            - 7:9: 1344\*1728
            - 9:7: 1728\*1344
            - 9:16: 1152\*2048
            - 9:21: 864\*2016
            - 16:9: 2048\*1152
            - 21:9: 2016\*864
          </Accordion>

          <strong>prompt\_extend</strong> `bool` （可选）

          <Tip>
            prompt\_extend直接影响费用。设为 `true` 时价格高于 `false`，具体见参见[模型价格](/zh/model-studio/model-pricing#6713612f55h4l)。
          </Tip>

          是否启用智能提示词（text）改写。开启后，将使用大模型优化提示词，并输出思考过程。

          - false：默认值，关闭智能改写。输出图像和原始文本提示词。
          - true：开启智能改写。输出图像、优化后的文本提示词、思考过程。

          <strong>seed</strong> `integer` （可选）

          随机数种子，取值范围`[0,2147483647]`。

          使用相同的`seed`参数值可使生成内容保持相对稳定。若不提供，算法将自动使用随机数种子。

          <strong>注意</strong>：模型生成过程具有概率性，即使使用相同的`seed`，也不能保证每次生成结果完全一致。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="文生图">
            以下示例直接返回图片，响应速度较快。

            若想开启“智能思考”能力，请设置`prompt_extend=true` 。开启后，系统将在返回图片的同时，返回优化后的提示词及其推理过程，但会增加响应时间。

            ```bash
            curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --data '{
                "model": "z-image-turbo",
                "input": {
                    "messages": [
                        {
                            "role": "user",
                            "content": [
                                {
                                    "text": "film grain, analog film texture, soft film lighting, Kodak Portra 400 style, cinematic grainy texture, photorealistic details, subtle noise, (film grain:1.2)。采用近景特写镜头拍摄的东亚年轻女性，呈现户外雪地场景。她体型纤瘦，呈站立姿势，身体微微向右侧倾斜，头部抬起看向画面上方，姿态自然放松。她的面部是典型东亚长相，肤色白皙，脸颊带有自然的红润感，五官清秀：眼睛是深棕色，眼型偏圆，眼神略带惊讶地望向上方，眼白部分可见；眉毛是深黑色，形状自然弯长；鼻子小巧挺直，嘴唇涂有红色口红，唇瓣微张，表情带着轻微的惊讶或好奇。她的头发是深黑色长直发，发丝被风吹得略显凌乱，部分垂在脸颊两侧，头顶佩戴一顶深灰色的头盔，头盔边缘露出少量发丝。服装是蓝白拼接的厚重外套，外套材质看起来是毛绒与布料结合，显得温暖厚实，适合雪地环境。背景是被白雪覆盖的户外场景，远处可见模糊的树木轮廓，天空是明亮的浅蓝色，带有少量白云，光线是强烈的自然日光，照亮人物面部与头发，形成清晰的光影，色调以蓝、白、黑为主，整体风格清新自然。画面顶部有黑色提示框，内有“Press esc to exit full screen”的白色文字。镜头的近景视角放大了人物的表情与细节，营造出户外雪地的真实氛围。"
                                }
                            ]
                        }
                    ]
                },
                "parameters": {
                    "prompt_extend": false,
                    "size": "1120*1440"
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

          模型生成的输出内容。此数组仅包含1个元素。

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

                生成图像的 URL，图像格式为PNG。<strong>链接有效期为24小时</strong>，请及时下载并保存图像。

                <strong>text</strong> `string`

                - 当prompt\_extend=false时，为输入的提示词。
                - 当prompt\_extend=true时，为改写后的提示词。
              </Accordion>

              <strong>reasoning\_content</strong> `string`

              模型的思考过程，仅在prompt\_extend=true时返回思考文本。
            </Accordion>
          </Accordion>
        </Accordion>

        <strong>usage</strong> `object`

        输出信息统计。只对成功的结果计数。

        <Accordion title="属性" defaultOpen>
          <strong>width</strong> `integer`

          生成图像的宽度（像素）。

          <strong>height</strong> `integer`

          生成图像的高度（像素）。

          <strong>image\_count</strong> `integer`

          生成图像的数量，固定为1。

          <strong>input\_tokens</strong> `integer`

          输入token数量，prompt\_extend=false时固定为0。

          <strong>output\_tokens</strong> `integer`

          输出token数量，prompt\_extend=false时固定为0。

          <strong>output\_tokens\_details</strong>`object`

          输出 token 详情，仅当prompt\_extend=true时返回。

          <Accordion title="属性" defaultOpen>
            <strong>reasoning\_tokens</strong>`integer`

            推理思考使用的 token 数量。
          </Accordion>

          <strong>total\_tokens</strong>`integer`

          总token数量，prompt\_extend=false时固定为0。
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
                                        "image": "https://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/xxx.png?Expires=xxx"
                                    },
                                    {
                                        "text": "film grain, analog film texture, soft film lighting, Kodak Portra 400 style, cinematic grainy texture, photorealistic details, subtle noise, (film grain:1.2)。采用近景特写镜头拍摄的东亚年轻女性，呈现户外雪地场景。她体型纤瘦，呈站立姿势，身体微微向右侧倾斜，头部抬起看向画面上方，姿态自然放松。她的面部是典型东亚长相，肤色白皙，脸颊带有自然的红润感，五官清秀：眼睛是深棕色，眼型偏圆，眼神略带惊讶地望向上方，眼白部分可见；眉毛是深黑色，形状自然弯长；鼻子小巧挺直，嘴唇涂有红色口红，唇瓣微张，表情带着轻微的惊讶或好奇。她的头发是深黑色长直发，发丝被风吹得略显凌乱，部分垂在脸颊两侧，头顶佩戴一顶深灰色的头盔，头盔边缘露出少量发丝。服装是蓝白拼接的厚重外套，外套材质看起来是毛绒与布料结合，显得温暖厚实，适合雪地环境。背景是被白雪覆盖的户外场景，远处可见模糊的树木轮廓，天空是明亮的浅蓝色，带有少量白云，光线是强烈的自然日光，照亮人物面部与头发，形成清晰的光影，色调以蓝、白、黑为主，整体风格清新自然。画面顶部有黑色提示框，内有“Press esc to exit full screen”的白色文字。镜头的近景视角放大了人物的表情与细节，营造出户外雪地的真实氛围。"
                                    }
                                ],
                                "reasoning_content": "",
                                "role": "assistant"
                            }
                        }
                    ]
                },
                "usage": {
                    "height": 1440,
                    "image_count": 1,
                    "input_tokens": 0,
                    "output_tokens": 0,
                    "total_tokens": 0,
                    "width": 1120
                },
                "request_id": "8a0809b4-a796-47f4-a095-394b02b62xxx"
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

## 使用限制 <span id="63a009e43a1iy" />

- 图像`url`均只保留 24 小时，请及时下载。
- <strong>内容审核</strong>：输入的 `prompt` 和输出的图像均会经过内容安全审核，包含违规内容的请求将报错“IPInfringementSuspect”或“DataInspectionFailed”，具体参见[错误码](/zh/model-studio/error-code)。

## 计费与限流 <span id="5dafefe82bgrh" />

- 模型免费额度和计费单价请参见[模型价格](/zh/model-studio/model-pricing#6713612f55h4l)。
- 模型限流请参见[Z-Image](/zh/model-studio/rate-limit#e17ad85fd8wwl)。
- 根据是否开启智能按成功生成的 <strong>图像张数</strong> 计费。模型调用失败或处理错误不产生任何费用，也不消耗[免费额度](/zh/model-studio/new-free-quota)。

## 错误码 <span id="e2b1945f71tld" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="c928ee689aw6l" />

### Q: 如何查看模型调用量？ <span id="ae11776b04p4u" />

A: 模型调用完一小时后，请在[<strong>模型监控</strong>（北京）](https://bailian.console.aliyun.com/cn-beijing/model/telemetry)或[<strong>模型监控</strong>（新加坡）](https://bailian.console.aliyun.com/ap-southeast-1/model/telemetry) 页面，查看模型的调用次数、成功率等指标。详情请参见[账单查询与成本管理](/zh/model-studio/bill-query-and-cost-management)。

### Q：如何获取图像存储的访问域名白名单？ <span id="5fb153423ct8z" />

A： 模型生成的图像存储于阿里云OSS，API将返回一个临时的公网URL（链接有效期为 24 小时，请及时下载保存）。图片URL的域名格式为`dashscope-{标识}.oss-accelerate.aliyuncs.com`或`dashscope-{标识}.oss-cn-{地域}.aliyuncs.com`。<strong>若需要对该下载地址进行防火墙白名单配置</strong>，请注意：由于底层存储会动态变更，以下bucket名称仅供参考，可能随时更新：dashscope-a717、dashscope-66f3、dashscope-7c2c、dashscope-2522、dashscope-c72b、dashscope-0484、dashscope-7e0f、dashscope-5859、dashscope-5496、dashscope-35f9、dashscope-31d9、dashscope-7f1f、dashscope-cc75、dashscope-64e9。为避免过期信息影响访问，文档不提供固定的OSS域名白名单。如有安全管控需求，请联系客户经理获取最新OSS域名列表。API当前仅支持返回URL格式，不支持base64格式输出。

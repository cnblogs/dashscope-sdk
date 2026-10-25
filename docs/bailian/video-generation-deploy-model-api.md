> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 视频生成-部署模型

> 将训练好的模型发布为在线 API 服务。

<meta property="og:title" content="视频生成-部署模型" />

## 适用范围 <span id="dep-prereq-title" /> <span id="dep-prereq-section" />

- <strong>适用地域</strong>：当前模型部署 API <strong>仅在华北2（北京）地域</strong>开放。如您使用其他地域，请通过该地域的百炼控制台完成模型部署操作。
- <strong>开通账号权限</strong>：若使用[阿里云子账号](/zh/model-studio/permission-management-overview)（[RAM用户](https://help.aliyun.com/zh/ram/user-guide/overview-of-ram-users)），需要为子账号授予模型调用、训练和部署[权限](/zh/model-studio/permission-management-overview)。
- <strong>配置环境变量</strong>：已成功[获取 API Key](/zh/model-studio/get-api-key)，并[配置到环境变量](/zh/model-studio/get-api-key)。
- <strong>前提条件</strong>：已完成模型微调训练。请先调用[查询调优任务](/zh/model-studio/create-fine-tuning-job-api)接口，确认任务状态 `status` 为 <strong>SUCCEEDED</strong> 后再进行部署。

## 部署模型 <span id="dep-main-h2" /> <span id="dep-main-section" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://dashscope.aliyuncs.com/api/v1/deployments`
  </Tab>
</Tabs>

> Windows CMD 请将`$DASHSCOPE_API_KEY`替换为`%DASHSCOPE_API_KEY%`，PowerShell请替换为 `$env:DASHSCOPE_API_KEY`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ### 请求参数 <span id="dep-req-header-h4" /> <span id="dep-req-section" />

        ##### 请求头（Headers） <span id="dep-req-headers-h5" />

        <strong>Content-Type</strong> `string` <strong>（必选）</strong>

        固定值：`application/json`

        <strong>Authorization</strong> `string` <strong>（必选）</strong>

        API Key鉴权，格式为`Bearer sk-xxxx`。

        ##### 请求体（Request Body） <span id="dep-req-body-h5" />

        <strong>model\_name</strong> `string` <strong>（必选）</strong>

        待部署的模型ID（非基础模型名称，而是微调或导出后生成的模型标识）。获取方式：

        - 微调产出的模型：使用[创建调优任务](/zh/model-studio/video-generation-create-fine-tuning-job-api)响应中 `output.finetuned_output` 的值，格式如 `wan2.5-i2v-preview-ft-202511111122-xxxx`。
        - 导出的模型：使用[查询导出的模型详情](/zh/model-studio/create-fine-tuning-job-api)响应中 `output[].model_name` 的值。

        <strong>capacity</strong> `integer` <strong>（必选）</strong>

        部署的模型实例数量。推荐设置为1。

        <strong>plan</strong> `string` <strong>（必选）</strong>

        部署方式。LoRA高效微调推荐为`lora`。

        <strong>aigc\_config</strong> `object` <strong>（必选）</strong>

        提示词配置。

        <Accordion title="aigc_config 属性" defaultOpen>
          <strong>use\_input\_prompt</strong> `boolean` <strong>（必选）</strong>

          控制调用已部署模型生成视频时的提示词来源。

          - `false`（默认值）：自动生成模式。调用视频生成接口时，系统忽略传入的 prompt，强制使用 `aigc_config.prompt` 和 `aigc_config.lora_prompt_default` 配置的模板自动生成提示词。
          - `true`：手动传入模式。调用视频生成接口时，系统直接使用传入的 prompt，上述两个参数将全部失效。

          <strong>prompt</strong> `string` <strong>（必选）</strong>

          预设的Prompt模板。仅在`use_input_prompt=false`时生效。

          调用已部署模型生成视频时，系统会分析输入图像，将画面内容填入该模板，自动生成带特效指令的完整描述。

          注意：此参数会<strong>覆盖</strong>视频生成接口中传入的 prompt 参数。

          <strong>lora\_prompt\_default</strong> `string` <strong>（必选）</strong>

          备用Prompt模板。仅在`use_input_prompt=false`时生效。

          当使用`aigc_config.prompt`生成失败时，系统将使用此参数作为兜底提示词来生成视频。
        </Accordion>
      </td>

      <td>
        <Tabs>
          <Tab title="图生视频-基于首帧">
            以下示例以金钱雨特效为例，上传一张人物图片，系统自动生成带有特效的视频。

            ```bash
            curl --location 'https://dashscope.aliyuncs.com/api/v1/deployments' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model_name": "<替换为模型名称model_name>",
                "aigc_config": {
                    "use_input_prompt": false,
                    "prompt": "根据图像内容提供视频描述，描述必须包含"然后开始展示s86b5p金钱雨特效，无数巨大尺寸的美元钞票（米黄底/深绿图案）如暴雨般倾泻而下，密集地砸向并环绕她（他）。钞票持续落下，同时镜头缓缓拉进，她（他）双臂舒展上扬，脖颈微仰，表情惊喜，完全沉浸在这场狂野的金钱雨中"。\n输出模板:\n视频开头展示了[主体描述]。[环境描述]。然后开始展示s86b5p金钱雨特效，无数巨大尺寸的美元钞票（米黄底/深绿图案）如暴雨般倾泻而下，密集地砸向并环绕她（他）。钞票持续落下，同时镜头缓缓拉进，她（他）双臂舒展上扬，脖颈微仰，表情惊喜，完全沉浸在这场狂野的金钱雨中。\n示例:\n视频开头展示了一位年轻女性在海滩上的场景。她的头发湿漉漉的，呈现出深棕色，卷曲且略显凌乱。她的脸上带着灿烂的笑容。背景是波涛汹涌的海浪和远处的山脉。然后开始展示s86b5p金钱雨特效，无数巨大尺寸的美元钞票（米黄底/深绿图案）如暴雨般倾泻而下，密集地砸向并环绕她。钞票持续落下，同时镜头缓缓拉进，她双臂舒展上扬，脖颈微仰，表情惊喜，完全沉浸在这场狂野的金钱雨中。",
                    "lora_prompt_default": "然后开始展示s86b5p金钱雨特效，无数巨大尺寸的美元钞票（米黄底/深绿图案）如暴雨般倾泻而下，密集地砸向并环绕主角。钞票持续落下，同时镜头缓缓拉进，主角双臂舒展上扬，脖颈微仰，表情惊喜，完全沉浸在这场狂野的金钱雨中。"
                },
                "capacity": 1,
                "plan": "lora"
            }'
            ```
          </Tab>

          <Tab title="图生视频-基于首尾帧">
            以下示例以时尚杂志换装特效为例，上传首帧和尾帧图片，系统自动生成人物换装变身的过渡视频。

            ```bash
            curl --location 'https://dashscope.aliyuncs.com/api/v1/deployments' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model_name": "<替换为模型名称model_name>",
                "aigc_config": {
                    "use_input_prompt": false,
                    "prompt": "根据图像内容提供视频描述，描述必须包含"然后她开始了s86b5p变身。"\n输出模板:\n视频开头展示了[主体描述]。[环境描述]。然后她开始了s86b5p变身。\n示例:\n视频开头展示了一位年轻女性在户外的场景。她有着短而卷曲的深棕色头发，面带微笑，显得非常亲切。她穿着一件黑色的Polo衫，上面有彩色的花卉刺绣图案，背景是绿色的植被和远处的山脉。然后她开始了s86b5p变身。",
                    "lora_prompt_default": "然后她开始了s86b5p变身。"
                },
                "capacity": 1,
                "plan": "lora"
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
        ### 响应参数 <span id="dep-resp-header-h4" /> <span id="dep-resp-section" />

        <strong>request\_id</strong> `string`

        请求的唯一标识符。

        <strong>output</strong> `object`

        任务详情。

        <Accordion title="属性" defaultOpen>
          <strong>deployed\_model</strong> `string`

          部署模型的唯一标识。用于查询模型部署状态和调用模型。

          <strong>model\_name</strong> `string`

          模型标识名。

          <strong>status</strong> `string`

          部署状态：

          - `PENDING`：部署中。
          - `RUNNING`：运行中。
          - `FAILED`：部署失败。

          <strong>base\_model</strong> `string`

          使用的基准模型。

          <strong>gmt\_create</strong> `string`

          部署任务创建时间。

          <strong>gmt\_modified</strong> `string`

          部署任务更新时间。

          <strong>workspace\_id</strong> `string`

          阿里云百炼API Key所属的业务空间ID。请参见[获取Workspace ID](/zh/model-studio/obtain-the-app-id-and-workspace-id)。

          <strong>charge\_type</strong> `string`

          付费模式。post\_paid表示后付费。

          <strong>creator</strong> `string`

          创建人的阿里云账号ID。

          <strong>modifier</strong> `string`

          修改人的阿里云账号ID。

          <strong>plan</strong> `string`

          部署方式。
        </Accordion>

        <strong>code</strong> `string`

        错误码。调用失败时返回。请参见下方错误码表。

        <strong>message</strong> `string`

        错误详情描述。调用失败时返回。
      </td>

      <td>
        <Tabs>
          <Tab title="成功响应示例">
            重点关注：`output.deployed_model`（部署模型的唯一标识）、`output.status`（部署状态）。

            ```json
            {
                "request_id": "96020b2e-9072-4c8a-9981-xxxxxxxxx",
                "output": {
                    "deployed_model": "wan2.5-i2v-preview-ft-202511111122-xxxx",
                    "gmt_create": "2025-11-11T17:46:53.294",
                    "gmt_modified": "2025-11-11T17:46:53.294",
                    "status": "PENDING",
                    "model_name": "wan2.5-i2v-preview-ft-202511111122-xxxx",
                    "base_model": "wan2.5-i2v-preview",
                    "workspace_id": "llm-xxxxxxxxx",
                    "charge_type": "post_paid",
                    "creator": "12xxxxxxx",
                    "modifier": "12xxxxxxx",
                    "plan": "lora"
                }
            }
            ```
          </Tab>

          <Tab title="错误响应示例">
            ```json
            {
                "code": "InvalidParameter",
                "request_id": "BE213CDD-8A5C-59EE-9A67-055EAB0CB59B",
                "message": "The model xxx does not exist or is not deployable"
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## Prompt 模板配置 <span id="dep-aigc-note-h2" /> <span id="dep-aigc-note-section" />

以下说明如何配置 `aigc_config.prompt` 参数。

- <strong>为什么要设置这个参数</strong>？

  如果不设置此参数，每次调用已部署模型都需要手动编写完整 Prompt；设置后，调用微调模型生成视频时，只需上传图片，系统会根据此参数自动补全特效指令，无需手动输入 Prompt。
- <strong>如何设置？</strong>

  <strong>Prompt = 任务指令（告诉模型要做什么）+ 输出模板（规范输出格式）+ 示例（让模型学习示例结构）。</strong>

```plaintext
根据图像内容提供视频描述，描述必须包含"然后开始展示s86b5p金钱雨特效，无数巨大尺寸的美元钞票（米黄底/深绿图案）如暴雨般倾泻而下，密集地砸向并环绕她（他）。钞票持续落下，同时镜头缓缓拉进，她（他）双臂舒展上扬，脖颈微仰，表情惊喜，完全沉浸在这场狂野的金钱雨中"。
输出模版:
视频开头展示了[主体描述]。[环境描述]。然后开始展示s86b5p金钱雨特效，无数巨大尺寸的美元钞票（米黄底/深绿图案）如暴雨般倾泻而下，密集地砸向并环绕她（他）。钞票持续落下，同时镜头缓缓拉进，她（他）双臂舒展上扬，脖颈微仰，表情惊喜，完全沉浸在这场狂野的金钱雨中。
示例:
视频开头展示了一位年轻女性在海滩上的场景。她的头发湿漉漉的，呈现出深棕色，卷曲且略显凌乱。她的脸上带着灿烂的笑容。背景是波涛汹涌的海浪和远处的山脉。然后开始展示s86b5p金钱雨特效，无数巨大尺寸的美元钞票（米黄底/深绿图案）如暴雨般倾泻而下，密集地砸向并环绕她。钞票持续落下，同时镜头缓缓拉进，她双臂舒展上扬，脖颈微仰，表情惊喜，完全沉浸在这场狂野的金钱雨中。
```

> 关于"输出模板"描述，请参见[视频标注：为视频编写Prompt](/zh/model-studio/wan-video-generation-finetune-guide#102ebee9f3dtq)。

## 下一步 <span id="dep-next-h2" /> <span id="dep-next" />

部署为异步操作，调用本接口后可通过[查询和管理部署](/zh/model-studio/get-deployment-api)接口查询部署状态。

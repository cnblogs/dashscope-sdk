> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 图像生成-部署模型

> 将训练好的图像生成模型发布为在线 API 服务。

<meta property="og:title" content="图像生成-部署模型" />

## 适用范围 <span id="img-dep-prereq-title" /> <span id="img-dep-prereq-section" />

- <strong>适用地域</strong>：当前模型部署 API <strong>仅在华北2（北京）地域</strong>开放。如您使用其他地域，请通过该地域的百炼控制台完成模型部署操作。
- <strong>开通账号权限</strong>：若使用[阿里云子账号](/zh/model-studio/permission-management-overview)（[RAM用户](https://help.aliyun.com/zh/ram/user-guide/overview-of-ram-users)），需要为子账号授予模型调用、训练和部署[权限](/zh/model-studio/permission-management-overview)。
- <strong>配置环境变量</strong>：已成功[获取 API Key](/zh/model-studio/get-api-key)，并[配置到环境变量](/zh/model-studio/get-api-key)。
- <strong>前提条件</strong>：已完成模型微调训练。请先调用[查询调优任务](/zh/model-studio/create-fine-tuning-job-api)接口，确认任务状态 `status` 为 <strong>SUCCEEDED</strong> 后再进行部署。

## 部署模型 <span id="img-dep-main-h2" /> <span id="img-dep-main-section" />

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
        ### 请求参数 <span id="img-dep-req-header-h4" /> <span id="img-dep-req-section" />

        ##### 请求头（Headers） <span id="img-dep-req-headers-h5" />

        <strong>Content-Type</strong> `string` <strong>（必选）</strong>

        固定值：`application/json`

        <strong>Authorization</strong> `string` <strong>（必选）</strong>

        API Key鉴权，格式为`Bearer sk-xxxx`。

        ##### 请求体（Request Body） <span id="img-dep-req-body-h5" />

        <strong>model\_name</strong> `string` <strong>（必选）</strong>

        待部署的模型ID（非基础模型名称，而是微调或导出后生成的模型标识）。获取方式：

        - 微调产出的模型：使用[创建调优任务](/zh/model-studio/create-fine-tuning-job-api)响应中 `output.finetuned_output` 的值。
        - 导出的模型：使用[查询导出的模型详情](/zh/model-studio/create-fine-tuning-job-api)响应中 `output[].model_name` 的值。

        <strong>capacity</strong> `integer` <strong>（必选）</strong>

        部署的模型实例数量。推荐设置为1。

        <strong>plan</strong> `string` <strong>（必选）</strong>

        部署方式。LoRA高效微调推荐为`lora`。
      </td>

      <td>
        <Tabs>
          <Tab title="部署模型">
            ```bash
            curl --location 'https://dashscope.aliyuncs.com/api/v1/deployments' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --header 'Content-Type: application/json' \
            --data '{
                "model_name": "<替换为模型名称model_name>",
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
        ### 响应参数 <span id="img-dep-resp-header-h4" /> <span id="img-dep-resp-section" />

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

        错误码。调用失败时返回。

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
                    "deployed_model": "wan2.7-image-pro-ft-202507011122-xxxx",
                    "gmt_create": "2025-07-01T10:30:00.000",
                    "gmt_modified": "2025-07-01T10:30:00.000",
                    "status": "PENDING",
                    "model_name": "wan2.7-image-pro-ft-202507011122-xxxx",
                    "base_model": "wan2.7-image-pro",
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

## 下一步 <span id="img-dep-next-h2" /> <span id="img-dep-next" />

部署为异步操作，调用本接口后可通过[查询和管理部署](/zh/model-studio/get-deployment-api)接口查询部署状态。

> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 语音合成（CosyVoice）-创建调优任务

> 创建一个语音合成的模型调优训练任务。数据集支持通过API上传数据集或OSS挂载。

<meta property="og:title" content="语音合成（CosyVoice）-创建调优任务" />

## 适用范围 <span id="voc-prereq-title" /> <span id="voc-prereq-section" />

- <strong>适用地域</strong>：本文描述的功能<strong>仅在华北2（北京）地域</strong>可用，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
- <strong>开通账号权限</strong>：若使用[阿里云子账号](/zh/model-studio/permission-management-overview)（[RAM用户](https://help.aliyun.com/zh/ram/user-guide/overview-of-ram-users)），需要为子账号授予模型调用、训练和部署[权限](/zh/model-studio/permission-management-overview)。
- <strong>配置环境变量</strong>：已成功[获取 API Key](/zh/model-studio/get-api-key)，并[配置到环境变量](/zh/model-studio/get-api-key)。
- <strong>准备工作</strong>：已阅读[CosyVoice模型调优](/zh/model-studio/fine-tune-speech-synthesis-model-by-api)，了解支持调优的模型、调优步骤、数据格式以及计费说明。

## 创建调优任务 <span id="voc-api-title" /> <span id="voc-api-section" />

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://dashscope.aliyuncs.com/api/v1/fine-tunes`
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
        ### 请求参数 <span id="voc-req-header-h4" /> <span id="voc-req-section" />

        ##### 请求头（Headers） <span id="voc-req-headers-h5" />

        <strong>Content-Type</strong> `string` <strong>（必选）</strong>

        固定值：`application/json`

        <strong>Authorization</strong> `string` <strong>（必选）</strong>

        API Key鉴权，格式为`Bearer sk-xxxx`。

        ##### 请求体（Request Body） <span id="voc-req-body-h5" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        指定调优所用的基准模型。当前仅支持：

        - `cosyvoice-v3-flash`

        <strong>training\_type</strong> `string` <strong>（必选）</strong>

        微调类型，当前仅支持`efficient_sft`（高效微调）。

        <strong>hyper\_parameters</strong> `object` <strong>（必选）</strong>

        <strong>超参数配置</strong>。CosyVoice 调优涉及两个子网络，两者已解耦、可独立调整：

        - LM（Language Model）：将文本转为离散语音 token 的自回归语言模型，对韵律影响较大。超参以 `lm_*` 前缀区分。
        - FM（Flow Matching）：将语音 token 还原为 Mel 谱的流匹配模型，对音色还原度影响较大。超参以 `fm_*` 前缀区分。

        <Accordion title="超参数属性" defaultOpen>
          <strong>lm\_max\_epoch</strong> `int` <strong>（必选）</strong>

          LM 调优轮次（epoch 数）。推荐值：60。取值范围：\[1, 2147483647]。

          <strong>lm\_step</strong> `int` <strong>（必选）</strong>

          LM 保存 checkpoint 的步长（每多少个 epoch 保存一次）。推荐值：5。取值范围：\[1, 2147483647]。

          <strong>lm\_num</strong> `int` <strong>（必选）</strong>

          LM 保留的 checkpoint 数量上限。推荐值：3。取值范围：\[1, 2147483647]。

          <strong>lm\_batch\_size</strong> `int` <strong>（必选）</strong>

          LM 批次大小（batch size）。推荐值：1000。取值范围：\[1, 2147483647]。

          <strong>fm\_max\_epoch</strong> `int` <strong>（必选）</strong>

          FM 调优轮次（epoch 数）。推荐值：100。取值范围：\[1, 2147483647]。

          <strong>fm\_step</strong> `int` <strong>（必选）</strong>

          FM 保存 checkpoint 的步长（每多少个 epoch 保存一次）。推荐值：10。取值范围：\[1, 2147483647]。

          <strong>fm\_num</strong> `int` <strong>（必选）</strong>

          FM 保留的 checkpoint 数量上限。推荐值：3。取值范围：\[1, 2147483647]。

          <strong>fm\_batch\_size</strong> `int` <strong>（必选）</strong>

          FM 批次大小（batch size）。推荐值：2000。取值范围：\[1, 2147483647]。
        </Accordion>

        <strong>training\_datasets</strong> `Array of Dataset` <strong>（条件必选）</strong>

        训练集文件列表。与 `training_file_ids` 二选一，若使用 `training_file_ids` 则无需传此参数。仅支持挂载一个训练文件。

        <Accordion title="Dataset 结构" defaultOpen>
          <strong>data\_source\_type</strong> `string` <strong>（必选）</strong>

          数据源类型，可选值：

          - `oss_mount`（挂载 OSS 文件）
          - `file_id`（由文件管理服务 API 上传的文件）

          <strong>mount\_storage</strong> `object` （条件必选）

          数据源类型为 `oss_mount` 时必填。OSS 挂载信息。

          <Accordion title="属性" defaultOpen>
            <strong>region</strong> `string` <strong>（必选）</strong>

            要挂载的 OSS Bucket 所属地域。支持北京（cn-beijing）和新加坡（ap-southeast-1）。

            <strong>bucket</strong> `string` <strong>（必选）</strong>

            要挂载的 OSS Bucket 名称。

            <strong>file\_path</strong> `string` <strong>（必选）</strong>

            要挂载的 OSS 文件路径（object key）。对包含多个文件的数据集，使用其 data.jsonl 的文件路径。与使用 file\_id 的方式不同，需要将未经压缩的数据集文件夹整体上传到 OSS，不支持 zip 文件。
          </Accordion>

          <strong>file\_id</strong> `string` （条件必选）

          数据源类型为 `file_id` 时必填。文件 ID，由[上传文件 API](/zh/model-studio/upload-file-api) 产生。
        </Accordion>

        <strong>validation\_datasets</strong> `Array of Dataset` （可选）

        验证集文件列表。结构同`training_datasets`。

        与 `validation_file_ids` 二选一。若两者均不提供，系统会从训练集中自动划分。

        <strong>training\_file\_ids</strong> `array[string]` <strong>（条件必选）</strong>

        训练集文件ID数组，可传入多个ID。与 `training_datasets` 二选一，若使用 `training_datasets` 则无需传此参数。文件ID通过[上传文件 API](/zh/model-studio/upload-file-api) 获取。

        <strong>validation\_file\_ids</strong> `array[string]` （可选）

        验证集文件ID数组，可传入多个ID。与 `validation_datasets` 二选一，若使用 `validation_datasets` 则无需传此参数。若两者均不提供，系统会从训练集中自动划分。文件ID通过[上传文件 API](/zh/model-studio/upload-file-api) 获取。

        <strong>job\_name</strong> `string` （可选）

        调优任务名称。

        <strong>model\_name</strong> `string` （可选）

        调优完成后的模型名称。
      </td>

      <td>
        <CodeGroup>
          ```bash 使用file_id的数据集
          curl --location 'https://dashscope.aliyuncs.com/api/v1/fine-tunes' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "cosyvoice-v3-flash",
              "training_datasets": [
                  {
                      "data_source_type": "file_id",
                      "file_id": "<替换为训练数据集的file_id>"
                  }
              ],
              "hyper_parameters": {
                  "lm_max_epoch": 60,
                  "lm_step": 5,
                  "lm_num": 3,
                  "lm_batch_size": 1000,
                  "fm_max_epoch": 100,
                  "fm_step": 10,
                  "fm_num": 3,
                  "fm_batch_size": 2000
              },
              "training_type": "efficient_sft"
          }'
          ```

          ```bash 使用OSS挂载的数据集 expandable
          curl --location 'https://dashscope.aliyuncs.com/api/v1/fine-tunes' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "cosyvoice-v3-flash",
              "training_datasets": [
                  {
                      "data_source_type": "oss_mount",
                      "mount_storage": {
                          "region": "cn-beijing",
                          "bucket": "example_bucket",
                          "file_path": "dataset/data.jsonl"
                      }
                  }
              ],
              "hyper_parameters": {
                  "lm_max_epoch": 60,
                  "lm_step": 5,
                  "lm_num": 3,
                  "lm_batch_size": 1000,
                  "fm_max_epoch": 100,
                  "fm_step": 10,
                  "fm_num": 3,
                  "fm_batch_size": 2000
              },
              "training_type": "efficient_sft"
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
        ### 响应参数 <span id="voc-resp-header-h4" /> <span id="voc-resp-section" />

        <strong>request\_id</strong> `string`

        请求的唯一标识符。

        <strong>output</strong> `object`

        任务详情。

        <Accordion title="属性" defaultOpen>
          <strong>job\_id</strong> `string`

          模型调优任务唯一标识，用于查询任务详情、日志、取消或删除任务。生成规则：`ft-{yyyyMMddHHmm}-{4位uuid}`。

          <strong>job\_name</strong> `string`

          模型调优任务名称。

          <strong>model\_name</strong> `string`

          调优完成后的模型名称。

          <strong>status</strong> `string`

          调优训练任务的状态：

          - `PENDING`：训练待开始。
          - `QUEUING`：训练正在排队（同一时间只能运行一个调优任务）。
          - `RUNNING`：训练正在进行中。
          - `SUCCEEDED`：训练成功。
          - `FAILED`：训练失败。
          - `CANCELED`：训练已取消。
          - `CANCELING`：训练正在取消中。

          <strong>finetuned\_output</strong> `string`

          调优后产出的新模型ID，部署和调用时需要用到。任务状态为 SUCCEEDED 时返回。

          <strong>model</strong> `string`

          使用的基准模型。

          <strong>base\_model</strong> `string`

          使用的基准模型。

          <strong>training\_file\_ids</strong> `array`

          兼容旧版字段，新任务始终返回空数组，请使用 `training_datasets`。

          <strong>training\_datasets</strong> `Array of Dataset`

          训练数据集列表。

          <strong>validation\_file\_ids</strong> `array`

          兼容旧版字段，新任务始终返回空数组，请使用 `validation_datasets`。

          <strong>validation\_datasets</strong> `Array of Dataset`

          验证数据集列表。若未指定验证集，为空数组。

          <strong>hyper\_parameters</strong> `object`

          实际使用的超参数。

          <strong>training\_type</strong> `string`

          模型调优的训练方式。

          <strong>output\_cnt</strong> `integer`

          当前任务已产出的 Checkpoint 数量。Checkpoint 详细列表可通过[Checkpoint 管理](/zh/model-studio/list-checkpoints-api)接口获取。

          <strong>max\_output\_cnt</strong> `integer`

          单次任务可产出的 Checkpoint 数量上限。`output_cnt` 超出上限的部分会按调优充分度截断。

          <strong>charge\_type</strong> `string`

          计费类型。CosyVoice 模型为 token。

          <strong>create\_time</strong> `string`

          任务创建时间。

          <strong>end\_time</strong> `string`

          任务结束时间。任务状态为 SUCCEEDED、FAILED 或 CANCELED 时返回。

          <strong>usage</strong> `integer`

          调优任务消耗的 Token 数。任务状态为 SUCCEEDED 或 CANCELED 时返回。

          <strong>workspace\_id</strong> `string`

          阿里云百炼API Key所属的业务空间ID。请参见[获取Workspace ID](/zh/model-studio/obtain-the-app-id-and-workspace-id)。

          <strong>user\_identity</strong> `string`

          用户标识，阿里云账号ID。

          <strong>creator</strong> `string`

          创建人的阿里云账号ID。

          <strong>modifier</strong> `string`

          修改人的阿里云账号ID。

          <strong>group</strong> `string`

          模型调优任务分组。
        </Accordion>

        <strong>code</strong> `string`

        错误码。调用失败时返回。请参见下方错误码表。

        <strong>message</strong> `string`

        错误详情描述。调用失败时返回。
      </td>

      <td>
        <Tabs>
          <Tab title="成功响应示例">
            重点关注：`output.job_id`（任务ID）、`output.finetuned_output`（调优后产出的新模型名称）。

            ```json expandable
            {
                "request_id": "0eb05b0c-02ba-414a-9d0c-xxxxxxxxx",
                "output": {
                    "job_id": "ft-202511111122-xxxx",
                    "job_name": "ft-202511111122-xxxx",
                    "status": "PENDING",
                    "finetuned_output": "cosyvoice-v3-flash-ft-202511111122-xxxx",
                    "model": "cosyvoice-v3-flash",
                    "base_model": "cosyvoice-v3-flash",
                    "training_file_ids": [],
                    "training_datasets": [
                        {
                            "data_source_type": "file_id",
                            "file_id": "xxxxxxxxxxxx"
                        }
                    ],
                    "validation_file_ids": [],
                    "validation_datasets": [],
                    "hyper_parameters": {
                        "lm_max_epoch": 60,
                        "lm_step": 5,
                        "lm_num": 3,
                        "lm_batch_size": 1000,
                        "fm_max_epoch": 100,
                        "fm_step": 10,
                        "fm_num": 3,
                        "fm_batch_size": 2000
                    },
                    "training_type": "efficient_sft",
                    "model_name": "ft-202511111122-xxxx",
                    "output_cnt": 0,
                    "max_output_cnt": 10,
                    "charge_type": "token",
                    "create_time": "2025-11-11 11:22:22"
                }
            }
            ```
          </Tab>

          <Tab title="错误响应示例">
            ```json
            {
                "code": "InvalidParameter",
                "request_id": "BE213CDD-8A5C-59EE-9A67-055EAB0CB59B",
                "message": "The model cosyvoice-v3-flash does not support training_type: full_sft"
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="voc-err-h2" /> <span id="voc-err-section" />

如果调用失败并返回报错信息，请参见下表进行排查。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "30%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>HTTP 状态码</strong></p></th><th><p><strong>错误码</strong></p></th><th><p><strong>解决方案</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>参数错误，缺少参数或者参数格式问题等。根据错误信息修正您的参数。</p></td></tr><tr><td><p>400</p></td><td><p>UnsupportedOperation</p></td><td><p>当资源处于特定状态时，无法对其进行操作。待要操作的资源到达可操作状态时再进行操作。</p></td></tr><tr><td><p>404</p></td><td><p>NotFound</p></td><td><p>要查询/操作的资源不存在。检查资源ID是否错误。</p></td></tr><tr><td><p>409</p></td><td><p>Conflict</p></td><td><p>已存在同名部署实例，需要指定后缀进行区分。</p></td></tr><tr><td><p>429</p></td><td><p>Throttling</p></td><td><p>资源的创建触发平台限制。删除不再使用的模型。<span>如您确实需要提高调优任务的并发量或保留更多调优成功的模型，请联系商务经理。</span></p></td></tr><tr><td><p>500</p></td><td><p>InternalError</p></td><td><p>内部错误。记录 request\_id，通过工单联系阿里云工程师进行排查。</p></td></tr></tbody></table>

## 下一步 <span id="voc-ft-next-h2" /> <span id="voc-ft-next" />

调优为异步操作，调用本接口后可通过[查询和管理调优任务](/zh/model-studio/get-fine-tuning-job-api)接口查询调优任务状态。

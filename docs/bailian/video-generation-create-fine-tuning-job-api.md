> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 视频生成-创建调优任务

> 创建一个视频生成的模型微调训练任务。数据集支持通过API上传数据集或OSS挂载。

<meta property="og:title" content="视频生成-创建调优任务" />

## 适用范围 <span id="vid-prereq-title" /> <span id="vid-prereq-section" />

- <strong>适用地域</strong>：本文描述的功能<strong>仅在华北2（北京）地域</strong>可用，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
- <strong>开通账号权限</strong>：若使用[阿里云子账号](/zh/model-studio/permission-management-overview)（[RAM用户](https://help.aliyun.com/zh/ram/user-guide/overview-of-ram-users)），需要为子账号授予模型调用、训练和部署[权限](/zh/model-studio/permission-management-overview)。
- <strong>配置环境变量</strong>：已成功[获取 API Key](/zh/model-studio/get-api-key)，并[配置到环境变量](/zh/model-studio/get-api-key)。
- <strong>准备工作</strong>：已阅读[视频生成模型调优](/zh/model-studio/wan-video-generation-finetune-guide)，了解支持微调的模型、微调步骤、数据格式以及计费说明。

## 创建微调任务 <span id="2a2f41392afai" /> <span id="c4ce795eaa5ci" />

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
        ### 请求参数 <span id="vid-req-header-h4" /> <span id="vid-req-section" />

        ##### 请求头（Headers） <span id="vid-req-headers-h5" />

        <strong>Content-Type</strong> `string` <strong>（必选）</strong>

        固定值：`application/json`

        <strong>Authorization</strong> `string` <strong>（必选）</strong>

        API Key鉴权，格式为`Bearer sk-xxxx`。

        ##### 请求体（Request Body） <span id="vid-req-body-h5" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        指定微调所用的基准模型。

        图生视频-基于首帧：

        - `wan2.7-i2v`：支持首帧和首尾帧微调，通过超参数`task_type`切换，默认为首帧（`i2v`）。
        - `wan2.5-i2v-preview`
        - `wan2.2-i2v-flash`

        图生视频-基于首尾帧：

        - `wan2.7-i2v`：需设置超参数`task_type`为`kf2v`。
        - `wan2.2-kf2v-flash`

        <strong>training\_file\_ids</strong> `array[string]` <strong>（条件必选）</strong>

        训练集文件ID数组，可传入多个ID。与 `training_datasets` 二选一，若使用 `training_datasets` 则无需传此参数。文件ID通过[上传文件 API](/zh/model-studio/upload-file-api) 获取。

        <strong>validation\_file\_ids</strong> `array[string]` （可选）

        验证集文件ID数组，可传入多个ID。与 `validation_datasets` 二选一，若使用 `validation_datasets` 则无需传此参数。若两者均不提供，系统会从训练集中自动划分。文件ID通过[上传文件 API](/zh/model-studio/upload-file-api) 获取。

        <strong>training\_type</strong> `string` <strong>（必选）</strong>

        微调类型，当前仅支持`efficient_sft`（LoRA高效微调）。

        <strong>hyper\_parameters</strong> `object` （可选）

        <strong>超参数配置</strong>。初次训练时，推荐使用默认的超参数。若模型效果不佳或训练不收敛，可以尝试调整 n\_epochs 或 learning\_rate 等参数。

        <Accordion title="超参数属性" defaultOpen>
          <strong>batch\_size</strong> `int` <strong>（必选）</strong>

          批次大小。一次性送入模型进行训练的数据条数。

          - wan2.7-i2v：推荐为1。
          - wan2.5-i2v-preview：推荐为2。
          - wan2.2-i2v-flash：推荐为4。
          - wan2.2-kf2v-flash：推荐为4。

          <strong>n\_epochs</strong> `int` <strong>（必选）</strong>

          训练循环次数。推荐值：50。

          `steps = n_epochs × ⌈数据集大小 / batch_size⌉`。建议总步数 ≥ 800。

          例如：数据集 5 条，batch\_size=2，每轮步数=⌈5/2⌉=3，最小 n\_epochs = 800/3 ≈ 267。

          - 推荐训练轮数会根据数据量自动调整。数据越少，需要更多轮数来充分学习；数据越多，每轮包含的样本越多，因此所需轮数会减少。
          - 50 epochs 主要适用于 2 条左右的小数据集；当数据量达到 50-60 条视频时，通常建议训练约 3000-5000 steps 即可。

          <Tip>
            该参数影响[训练计费](/zh/model-studio/model-training-and-deployment-billing)，请合理设置。
          </Tip>

          <strong>learning\_rate</strong> `float` <strong>（必选）</strong>

          学习率。推荐值：2e-5。控制模型权重更新的幅度。过高可能导致模型变差，过低则变化不明显。

          <strong>eval\_epochs</strong> `int` <strong>（必选）</strong>

          验证间隔。推荐值：20。取值需≥`n_epochs/10`。训练期间每隔多少个epoch进行一次验证评估。

          <strong>max\_pixels</strong> `int` <strong>（必选）</strong>

          训练视频的最大分辨率。设置训练视频分辨率的像素总数（宽×高）限制。系统仅对超过该值的视频进行缩放处理。

          - wan2.7-i2v：推荐 102400。取值范围：36864 (192×192) ～ 123904 (352×352)。
          - wan2.5-i2v-preview：推荐 36864。取值范围：16384 (128×128) ～ 36864 (192×192)。
          - wan2.2-i2v-flash：推荐 262144。取值范围：65536 (256×256) ～ 262144 (512×512)。
          - wan2.2-kf2v-flash：推荐 262144。取值范围：65536 (256×256) ～ 262144 (512×512)。

          <strong>split</strong> `float` （可选）

          训练集划分比例。推荐值：0.9。取值范围为 (0, 1)。

          仅在未指定`validation_file_ids`时生效。此参数用于从训练集中自动按比例拆分出验证集。例如，0.9表示90%训练集，10%验证集。

          <strong>max\_split\_val\_dataset\_sample</strong> `int` （可选）

          从训练集中自动划分验证集的最大样本数。推荐值：5。取值需≥1。

          仅在未指定`validation_file_ids`时生效。该参数为验证集数量设置上限：`验证集数量 = min(数据集总数 × (1 − split), max_split_val_dataset_sample)`

          <strong>save\_total\_limit</strong> `int` （可选）

          Checkpoint 保存数量上限。推荐值：10。限制最多保存的模型数量，系统将只保存最后 N 个 Checkpoint。

          <strong>lora\_rank</strong> `int` （可选）

          LoRA 低秩矩阵的维数。推荐值：32。该值决定了微调参数量的大小，数值越大拟合能力越强，但训练速度会变慢。取值必须为2<sup>n</sup>（如 16、32、64）。

          <strong>lora\_alpha</strong> `int` （可选）

          LoRA 权重的缩放系数。推荐值：32。用于调节微调后的参数对原模型权重的影响程度。取值必须为2<sup>n</sup>（如 16、32、64）。

          <strong>task\_type</strong> `string` （可选）

          微调任务类型。仅对`wan2.7-i2v`模型有效，其他模型无需设置此参数。

          - `i2v`（默认值）：首帧生视频微调。
          - `kf2v`：首尾帧生视频微调。
        </Accordion>

        <strong>training\_datasets</strong> `Array of Dataset` <strong>（条件必选）</strong>

        训练集文件列表。与 `training_file_ids` 二选一，若使用 `training_file_ids` 则无需传此参数。

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

        <strong>job\_name</strong> `string` （可选）

        调优任务名称。

        <strong>model\_name</strong> `string` （可选）

        调优完成后的模型名称。
      </td>

      <td>
        <CodeGroup>
          ```bash 图生视频-基于首帧（Wan2.7）
          curl --location 'https://dashscope.aliyuncs.com/api/v1/fine-tunes' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "wan2.7-i2v",
              "training_file_ids": [
                  "<替换为训练数据集的文件id>"
              ],
              "training_type": "efficient_sft",
              "hyper_parameters": {
                  "task_type": "i2v",
                  "n_epochs": 50,
                  "batch_size": 1,
                  "learning_rate": 2e-5,
                  "split": 0.9,
                  "max_split_val_dataset_sample": 5,
                  "eval_epochs": 20,
                  "max_pixels": 102400,
                  "save_total_limit": 10,
                  "lora_rank": 32,
                  "lora_alpha": 32
              }
          }'
          ```

          ```bash 图生视频-基于首尾帧（Wan2.7）
          curl --location 'https://dashscope.aliyuncs.com/api/v1/fine-tunes' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "wan2.7-i2v",
              "training_file_ids": [
                  "<替换为训练数据集的文件id>"
              ],
              "training_type": "efficient_sft",
              "hyper_parameters": {
                  "task_type": "kf2v",
                  "n_epochs": 50,
                  "batch_size": 1,
                  "learning_rate": 2e-5,
                  "split": 0.9,
                  "max_split_val_dataset_sample": 5,
                  "eval_epochs": 20,
                  "max_pixels": 102400,
                  "save_total_limit": 10,
                  "lora_rank": 32,
                  "lora_alpha": 32
              }
          }'
          ```

          ```bash 图生视频-基于首尾帧（Wan2.2）
          curl --location 'https://dashscope.aliyuncs.com/api/v1/fine-tunes' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "wan2.2-kf2v-flash",
              "training_file_ids": [
                  "<替换为训练数据集的文件id>"
              ],
              "training_type": "efficient_sft",
              "hyper_parameters": {
                  "n_epochs": 50,
                  "batch_size": 4,
                  "learning_rate": 2e-5,
                  "split": 0.9,
                  "max_split_val_dataset_sample": 5,
                  "eval_epochs": 20,
                  "max_pixels": 262144,
                  "save_total_limit": 10,
                  "lora_rank": 32,
                  "lora_alpha": 32
              }
          }'
          ```

          ```bash 使用OSS挂载的数据集 expandable
          curl --location 'https://dashscope.aliyuncs.com/api/v1/fine-tunes' \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model": "wan2.7-i2v",
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
              "training_type": "efficient_sft",
              "hyper_parameters": {
                  "n_epochs": 50,
                  "batch_size": 1,
                  "learning_rate": 2e-5,
                  "split": 0.9,
                  "max_split_val_dataset_sample": 5,
                  "eval_epochs": 20,
                  "max_pixels": 102400,
                  "save_total_limit": 10,
                  "lora_rank": 32,
                  "lora_alpha": 32
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
        ### 响应参数 <span id="vid-resp-header-h4" /> <span id="d61ef6313a8fm" />

        <strong>request\_id</strong> `string`

        请求的唯一标识符。

        <strong>output</strong> `object`

        任务详情。

        <Accordion title="属性" defaultOpen>
          <strong>job\_id</strong> `string`

          模型微调任务唯一标识，用于查询任务详情、日志、取消或删除任务。生成规则：`ft-{yyyyMMddHHmm}-{4位uuid}`。

          <strong>job\_name</strong> `string`

          模型微调任务名称。

          <strong>status</strong> `string`

          微调训练任务的状态：

          - `PENDING`：训练待开始。
          - `QUEUING`：训练正在排队（同一时间只能运行一个微调任务）。
          - `RUNNING`：训练正在进行中。
          - `SUCCEEDED`：训练成功。
          - `FAILED`：训练失败。
          - `CANCELED`：训练已取消。
          - `CANCELING`：训练正在取消中。

          <strong>finetuned\_output</strong> `string`

          微调后产出的新模型ID，部署和调用时需要用到。任务状态为 SUCCEEDED 时返回。

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

          模型微调的训练方式。

          <strong>create\_time</strong> `string`

          任务创建时间。

          <strong>end\_time</strong> `string`

          任务结束时间。任务状态为 SUCCEEDED、FAILED 或 CANCELED 时返回。

          <strong>usage</strong> `integer`

          微调任务消耗的 Token 数。任务状态为 SUCCEEDED 或 CANCELED 时返回。

          <strong>workspace\_id</strong> `string`

          阿里云百炼API Key所属的业务空间ID。请参见[获取Workspace ID](/zh/model-studio/obtain-the-app-id-and-workspace-id)。

          <strong>user\_identity</strong> `string`

          用户标识，阿里云账号ID。

          <strong>creator</strong> `string`

          创建人的阿里云账号ID。

          <strong>modifier</strong> `string`

          修改人的阿里云账号ID。

          <strong>group</strong> `string`

          模型微调任务分组。

          <strong>max\_output\_cnt</strong> `integer`

          训练期间最多保存的 checkpoint 数量。等同于超参数 save\_total\_limit 的值。
        </Accordion>

        <strong>code</strong> `string`

        错误码。调用失败时返回。请参见下方错误码表。

        <strong>message</strong> `string`

        错误详情描述。调用失败时返回。
      </td>

      <td>
        <Tabs>
          <Tab title="成功响应示例">
            重点关注：`output.job_id`（任务ID）、`output.finetuned_output`（微调后产出的新模型名称）。

            ```json expandable
            {
                "request_id": "0eb05b0c-02ba-414a-9d0c-xxxxxxxxx",
                "output": {
                    "job_id": "ft-202511111122-xxxx",
                    "job_name": "ft-202511111122-xxxx",
                    "status": "PENDING",
                    "finetuned_output": "wan2.5-i2v-preview-ft-202511111122-xxxx",
                    "model": "wan2.5-i2v-preview",
                    "base_model": "wan2.5-i2v-preview",
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
                        "n_epochs": 50,
                        "batch_size": 2,
                        "learning_rate": 2.0E-5,
                        "split": 0.9,
                        "eval_epochs": 20
                    },
                    "training_type": "efficient_sft",
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
                "message": "The model wan2.7-i2v does not support training_type: full_sft"
            }
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="vid-err-h2" /> <span id="vid-err-section" />

如果调用失败并返回报错信息，请参见下表进行排查。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "30%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>HTTP 状态码</strong></p></th><th><p><strong>错误码</strong></p></th><th><p><strong>解决方案</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>参数错误，缺少参数或者参数格式问题等。根据错误信息修正您的参数。</p></td></tr><tr><td><p>400</p></td><td><p>UnsupportedOperation</p></td><td><p>当资源处于特定状态时，无法对其进行操作。待要操作的资源到达可操作状态时再进行操作。</p></td></tr><tr><td><p>404</p></td><td><p>NotFound</p></td><td><p>要查询/操作的资源不存在。检查资源ID是否错误。</p></td></tr><tr><td><p>409</p></td><td><p>Conflict</p></td><td><p>已存在同名部署实例，需要指定后缀进行区分。</p></td></tr><tr><td><p>429</p></td><td><p>Throttling</p></td><td><p>资源的创建触发平台限制。删除不再使用的模型。<span>如您确实需要提高调优任务的并发量或保留更多调优成功的模型，请联系商务经理。</span></p></td></tr><tr><td><p>500</p></td><td><p>InternalError</p></td><td><p>内部错误。记录 request\_id，通过工单联系阿里云工程师进行排查。</p></td></tr></tbody></table>

## 下一步 <span id="vid-ft-next-h2" /> <span id="vid-ft-next" />

调优为异步操作，调用本接口后可通过[查询和管理调优任务](/zh/model-studio/get-fine-tuning-job-api)接口查询调优任务状态。

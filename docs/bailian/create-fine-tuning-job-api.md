> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 文本生成-创建调优任务

> 创建一个文本生成的模型微调训练任务。数据集支持通过API上传数据集或OSS挂载。

<meta property="og:title" content="文本生成-创建调优任务" />

## 适用范围 <span id="txt-prereq-title" />

- <strong>适用地域</strong>：本文描述的功能<strong>仅在华北2（北京）地域</strong>可用，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
- <strong>开通账号权限</strong>：若使用[阿里云子账号](/zh/model-studio/permission-management-overview)（[RAM用户](https://help.aliyun.com/zh/ram/user-guide/overview-of-ram-users)），需要为子账号授予模型调用、训练和部署[权限](/zh/model-studio/use-workspace)。
- <strong>配置环境变量</strong>：已成功[获取 API Key](/zh/model-studio/get-api-key)，并[配置到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
- <strong>准备工作</strong>：已阅读[千问模型调优](/zh/model-studio/fine-tune-text-generation-model)，了解支持微调的模型、微调步骤、数据格式以及计费说明。
- <strong>RL/OPD 训练</strong>：本接口适用于 SFT/CPT/DPO 微调。强化学习（RL）与在线策略蒸馏（OPD）训练通过 DashScope SDK/CLI 提交（`dashscope rl run` 或 `AgenticRL().run()`），非 REST API，详见 [强化学习训练配置](/zh/model-studio/rl-training-config-monitoring) 与 [在线策略蒸馏训练配置](/zh/model-studio/opd-training-config)。

## 创建微调任务 <span id="2a2f41392afai" />

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
        ### 请求参数 <span id="txt-req-header-h4" />

        ##### 请求头（Headers） <span id="txt-req-headers-h5" />

        <strong>Content-Type</strong> `string` <strong>（必选）</strong>

        固定值：`application/json`

        <strong>Authorization</strong> `string` <strong>（必选）</strong>

        API Key鉴权，格式为`Bearer sk-xxxx`。

        ##### 请求体（Request Body） <span id="txt-req-body-h5" />

        <strong>model</strong> `string` <strong>（必选）</strong>

        用于调优的[基础模型ID](/zh/model-studio/fine-tuning-api-guide)或其他调优任务产出的模型ID（对已经调优了的模型进行再次调优）。

        <strong>training\_type</strong> `string` （可选）

        调优方法，可选值：

        - `cpt`
        - `sft`
        - `efficient_sft`
        - `dpo_full`
        - `dpo_lora`

        <strong>hyper\_parameters</strong> `object` （可选）

        <strong>超参数设置</strong>。不同模型支持的参数集合及其默认值不同，请前往[控制台](https://bailian.console.aliyun.com/model/tuning/create)选择相同的模型和调优方式查看实际默认值。其中 `n_epochs`、`batch_size`、`max_length` 影响调优费用，<strong>必须填写</strong>。

        <Accordion title="超参数属性" defaultOpen>
          <strong>n\_epochs</strong> `int` <strong>（必选）</strong>

          训练循环次数。模型遍历训练的次数，请根据模型调优实际使用经验进行调整。

          - 数据量 \< 10,000：推荐 3\~5 次。
          - 数据量 > 10,000：推荐 1\~2 次。

          <Tip>
            该参数影响[训练计费](/zh/model-studio/model-training-and-deployment-billing)，循环次数越多，训练时间越长，费用越高。
          </Tip>

          <strong>batch\_size</strong> `int` <strong>（必选）</strong>

          批次大小。一次性送入模型进行训练的数据条数，参数过小会显著延长训练时间。不同模型的默认值不同，请前往控制台查看。

          <strong>max\_length</strong> `int` <strong>（必选）</strong>

          序列长度。推荐值：8192。单条训练数据 token 支持的最大长度。如果单条数据 token 长度超过设定值，调优会直接丢弃该条数据，不进行训练。

          字符与 token 之间的关系请参考[Token和字符串之间怎么换算](/zh/model-studio/billing-for-model-studio)。

          <strong>learning\_rate</strong> `float` （可选）

          学习率。推荐使用百炼默认值。控制模型修正权重的强度。

          - 学习率过高，模型参数会剧烈变化，可能导致模型表现变差。
          - 学习率过低，模型表现不会有太大变化。

          <strong>lr\_scheduler\_type</strong> `string` （可选）

          学习率调整策略。推荐 `linear` 或 `inverse_sqrt`。在模型训练中动态调整学习率的策略。各策略详情请参考[学习率调整策略说明](/zh/model-studio/model-training-on-console)。

          <strong>split</strong> `float` （可选）

          训练集在训练文件中的占比。推荐使用百炼默认值。

          未设置 `validation_datasets` 时，百炼会自动把训练文件中的 80% 作为训练集、20% 作为验证集。设置了 `validation_datasets` 时该参数无效。

          <strong>max\_split\_val\_dataset\_sample</strong> `int` （可选）

          验证集数据最大数量。推荐使用百炼默认值。

          未设置 `validation_datasets` 时，自动分割的验证集最多 1000 条。设置了 `validation_datasets` 时该参数无效。

          <strong>eval\_steps</strong> `int` （可选）

          验证步数。训练阶段针对模型的验证间隔步长，用于阶段性评估模型训练准确率、训练损失。

          该参数影响模型调优进行时的 Validation Loss 和 Validation Token Accuracy 的显示频率。

          <strong>logging\_steps</strong> `int` （可选）

          日志显示步数。调优日志打印的间隔步数。

          <strong>warmup\_ratio</strong> `float` （可选）

          学习率预热比例。推荐使用百炼默认值。学习率预热占用总的训练过程的比例。学习率预热是指学习率在训练开始后由一个较小值线性递增至学习率设定值，帮助模型更稳定地训练。

          - 比例过大：效果与过低的学习率相同，会导致调优后的模型表现不会有太大变化。
          - 比例过小：效果与过高的学习率相同，可能导致调优后的模型表现不一定更好，甚至变差。

          > 该参数仅对学习率调整策略 `Constant` 无效。

          <strong>weight\_decay</strong> `float` （可选）

          权重衰减（L2 正则化强度）。推荐使用百炼默认值。能在一定程度上保持模型的通用能力，数值过大会导致调优效果不明显。

          <strong>freeze\_vit</strong> `boolean` （可选）

          是否冻结视觉主干网络。用于冻结视觉主干网络的参数，使其在训练过程中不更新权重。仅适用于千问-VL（视觉理解）模型。

          > 只有 `freeze_vit` 设置为 `true` 时，模型才能进行按 Token 用量计费。

          <strong>lora\_rank</strong> `int` （可选）

          LoRA 秩值。推荐值：64。LoRA 训练中的低秩矩阵的秩大小。秩越大调优效果越好，但训练会略慢。

          仅在 `training_type` 为 `efficient_sft` 或 `dpo_lora` 时生效。

          > 当对一个已经高效微调后的模型进行二次高效微调时，`lora_rank`、`lora_alpha`、`lora_dropout` 三个参数必须保持一致。

          <strong>lora\_alpha</strong> `int` （可选）

          LoRA 缩放系数。推荐使用百炼默认值。用于控制原模型权重与 LoRA 的低秩修正项之间的结合缩放系数。

          - 较大的 Alpha 值会给予 LoRA 修正项更多权重，使得模型更加依赖于微调任务的特定信息。
          - 较小的 Alpha 值则会让模型更倾向于保留原始预训练模型的知识。

          仅在 `training_type` 为 `efficient_sft` 或 `dpo_lora` 时生效。

          <strong>lora\_dropout</strong> `float` （可选）

          LoRA 丢弃率。推荐使用百炼默认值。LoRA 训练中的低秩矩阵值的丢弃率。使用推荐数值能增强模型通用化能力，数值过大会导致模型微调效果不明显。

          仅在 `training_type` 为 `efficient_sft` 或 `dpo_lora` 时生效。

          <strong>data\_augmentation</strong> `boolean` （可选）

          是否开启混合训练。开启后训练数据将与百炼提供的通用数据集混合，提升训练效果，避免模型能力退化。混合数据计入总训练 Token，按标准计费。

          仅在 `training_type` 为 `efficient_sft` 或 `sft` 时生效。

          <strong>augmentation\_types</strong> `string` （可选）

          预置数据类型。开启混合训练时，选择预置数据类型，多个类型以逗号分隔。需与 `augmentation_ratio` 配合使用。示例：`"dialogue_cn,general_purpose_cn,nlp"`。

          可选值：

          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /><col style={{ width: "33.333333%" }} /></colgroup><tbody><tr><td><p>取值</p></td><td><p>数据集名称</p></td><td><p>适用模型</p></td></tr><tr><td><p><code>dialogue\_cn</code></p></td><td><p>中文-对话</p></td><td><p>千问 2 系列</p></td></tr><tr><td><p><code>math\_cn</code></p></td><td><p>中文-数学</p></td><td><p>千问 2 系列</p></td></tr><tr><td><p><code>general\_coding\_cn</code></p></td><td><p>中文-代码</p></td><td><p>千问 2 系列</p></td></tr><tr><td><p><code>general\_purpose\_cn</code></p></td><td><p>中文-通用</p></td><td><p>千问 2 系列</p></td></tr><tr><td><p><code>nlp</code></p></td><td><p>NLP 理解</p></td><td><p>千问 2 系列</p></td></tr><tr><td><p><code>dialogue\_en</code></p></td><td><p>英文-对话</p></td><td><p>千问 2 系列</p></td></tr><tr><td><p><code>math\_en</code></p></td><td><p>英文-数学</p></td><td><p>千问 2 系列</p></td></tr><tr><td><p><code>general\_coding\_en</code></p></td><td><p>英文-代码</p></td><td><p>千问 2 系列</p></td></tr><tr><td><p><code>general\_purpose\_en</code></p></td><td><p>英文-通用</p></td><td><p>千问 2 系列</p></td></tr><tr><td><p><code>mix\_v2</code></p></td><td><p>通用-V2</p></td><td><p>千问 3 系列</p></td></tr><tr><td><p><code>vl\_mix</code></p></td><td><p>通用</p></td><td><p>千问 3 VL 系列</p></td></tr></tbody></table>

          仅在 `training_type` 为 `efficient_sft` 或 `sft` 时生效。

          <strong>augmentation\_ratio</strong> `string` （可选）

          混合倍率。需与 `augmentation_types` 完全对应，按训练数据量的比例随机抽取混合。取值范围：0.0\~2.0。示例：`"0.1,0.05,0.15"`。

          仅在 `training_type` 为 `efficient_sft` 或 `sft` 时生效。

          <strong>save\_strategy</strong> `string` （可选）

          快照存储策略。可设置为 `epoch` 或 `steps`。设置为 `steps` 时，可通过 `save_steps` 参数调整保存间隔。

          仅在 `training_type` 为 `efficient_sft` 或 `sft` 时生效。

          <strong>save\_steps</strong> `int` （可选）

          存储步数。设置每训练多少步保存一次模型参数快照（Checkpoint）。建议设置为 `eval_steps` 的整数倍。

          仅在 `training_type` 为 `efficient_sft` 或 `sft` 时生效。

          <strong>save\_total\_limit</strong> `int` （可选）

          快照存储数量上限。推荐值：10。限制最多保存多少个模型参数快照（Checkpoint）用于发布。

          仅在 `training_type` 为 `efficient_sft` 或 `sft` 时生效。
        </Accordion>

        <strong>training\_datasets</strong> `Array of Dataset` <strong>（必选）</strong>

        训练集文件列表。

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

        测试集文件列表。结构同`training_datasets`。

        <strong>job\_name</strong> `string` （可选）

        调优任务名称。

        <strong>model\_name</strong> `string` （可选）

        调优完成后的模型名称。
      </td>

      <td>
        <CodeGroup>
          ```bash 使用 file_id 数据集 expandable
          curl --location --request POST "https://dashscope.aliyuncs.com/api/v1/fine-tunes" \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model":"qwen3-14b",
              "training_datasets":[
                  {
                      "data_source_type":"file_id",
                      "file_id":"<替换为训练数据集的文件id>"
                  }
              ],
              "validation_datasets":[
                  {
                      "data_source_type":"file_id",
                      "file_id":"<替换为验证数据集的文件id>"
                  }
              ],
              "hyper_parameters":{
                  "n_epochs":1,
                  "learning_rate":"1.6e-5",
                  "batch_size":32,
                  "max_length":8192,
                  "split":0.8
              },
              "training_type":"sft",
              "finetuned_output_suffix":"suffix"
          }'
          ```

          ```bash 使用 OSS 挂载数据集 expandable
          curl --location --request POST "https://dashscope.aliyuncs.com/api/v1/fine-tunes" \
          --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
          --header 'Content-Type: application/json' \
          --data '{
              "model":"qwen3-14b",
              "training_datasets":[
                  {
                      "data_source_type":"oss_mount",
                      "mount_storage":{
                          "region":"cn-beijing",
                          "bucket":"example_bucket",
                          "file_path":"dataset/data.jsonl"
                      }
                  }
              ],
              "validation_datasets":[
                  {
                      "data_source_type":"oss_mount",
                      "mount_storage":{
                          "region":"cn-beijing",
                          "bucket":"example_bucket",
                          "file_path":"dataset/val.jsonl"
                      }
                  }
              ],
              "hyper_parameters":{
                  "n_epochs":1,
                  "learning_rate":"1.6e-5",
                  "batch_size":32,
                  "max_length":8192,
                  "split":0.8
              },
              "training_type":"sft",
              "finetuned_output_suffix":"suffix"
          }'
          ```

          ```bash dashscope CLI expandable
          export DASHSCOPE_API_KEY="your-api-key"
          # 将 {WorkspaceId} 替换为业务空间ID，cn-beijing 替换为对应地域（新加坡: ap-southeast-1, 美东: us-east-1）
          export DASHSCOPE_HTTP_BASE_URL="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"
          # 列出微调任务
          dashscope ft list

          # 创建微调任务
          dashscope ft create -t <file-id> -m qwen3-4b-instruct-2507 -e 1 -b 8
          ```
        </CodeGroup>

        <Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>
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
        ### 返回参数 <span id="txt-resp-header-h4" />

        <strong>request\_id</strong> `string`

        本次请求的ID。

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

          微调后产出的新模型ID。任务状态为 SUCCEEDED 时返回。

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

          微调任务消耗的 Token 数。扣费计算公式请参考：[计费说明](/zh/model-studio/billing-for-model-studio)。任务状态为 SUCCEEDED 或 CANCELED 时返回。

          <strong>workspace\_id</strong> `string`

          调优任务所属的业务空间ID。

          <strong>user\_identity</strong> `string`

          用户标识，阿里云账号ID。

          <strong>creator</strong> `string`

          创建人的阿里云账号ID。

          <strong>modifier</strong> `string`

          修改人的阿里云账号ID。

          <strong>group</strong> `string`

          模型微调任务分组。
        </Accordion>

        <strong>code</strong> `string`

        错误码。调用失败时返回。请参见下方错误码表。

        <strong>message</strong> `string`

        错误详情描述。调用失败时返回。
      </td>

      <td>
        <CodeGroup>
          ```json 成功响应示例 expandable
          {
              "request_id": "9654e55a-d74b-4113-aee1-fa19c9384fcc",
              "output": {
                  "job_id": "ft-202410291653-1c7f",
                  "job_name": "ft-202410291653-1c7f",
                  "status": "PENDING",
                  "model": "qwen3-14b",
                  "base_model": "qwen3-14b",
                  "training_file_ids": [],
                  "training_datasets": [
                      {
                          "data_source_type": "file_id",
                          "file_id": "976bd01a-f30b-4414-86fd-50c54486e3ef"
                      }
                  ],
                  "validation_file_ids": [],
                  "validation_datasets": [],
                  "hyper_parameters": {
                      "n_epochs": 3,
                      "batch_size": 32,
                      "max_length": 8192,
                      "learning_rate": "1.6e-5",
                      "lr_scheduler_type": "linear",
                      "split": 0.9
                  },
                  "training_type": "sft",
                  "create_time": "2024-10-29 16:53:53",
                  "workspace_id":"llm-v71tlv***",
                  "user_identity": "1396993924585947",
                  "modifier": "1396993924585947",
                  "creator": "1396993924585947",
                  "group": "llm"
              }
          }
          ```

          ```json 错误响应示例
          {
              "code": "InvalidParameter",
              "request_id": "BE213CDD-8A5C-59EE-9A67-055EAB0CB59B",
              "message": "Missing training files"
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="txt-err-h2" />

如果调用失败并返回报错信息，请参见下表进行排查。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "30%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>HTTP 状态码</strong></p></th><th><p><strong>错误码</strong></p></th><th><p><strong>解决方案</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>参数错误，缺少参数或者参数格式问题等。根据错误信息修正您的参数。</p></td></tr><tr><td><p>400</p></td><td><p>UnsupportedOperation</p></td><td><p>当资源处于特定状态时，无法对其进行操作。待要操作的资源到达可操作状态时再进行操作。</p></td></tr><tr><td><p>404</p></td><td><p>NotFound</p></td><td><p>要查询/操作的资源不存在。检查资源ID是否错误。</p></td></tr><tr><td><p>409</p></td><td><p>Conflict</p></td><td><p>已存在同名部署实例，需要指定后缀进行区分。</p></td></tr><tr><td><p>429</p></td><td><p>Throttling</p></td><td><p>资源的创建触发平台限制。删除不再使用的模型。<span>如您确实需要提高调优任务的并发量或保留更多调优成功的模型，请联系商务经理。</span></p></td></tr><tr><td><p>500</p></td><td><p>InternalError</p></td><td><p>内部错误。记录 request\_id，通过工单联系阿里云工程师进行排查。</p></td></tr></tbody></table>

## 下一步 <span id="txt-ft-next-h2" />

调优为异步操作，调用本接口后可通过[查询和管理调优任务](/zh/model-studio/get-fine-tuning-job-api)接口查询调优任务状态。

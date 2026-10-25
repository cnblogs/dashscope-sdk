> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 调优任务管理

> 查询、取消、删除调优任务，以及查询调优任务日志。

<meta property="og:title" content="调优任务管理" />

## 适用范围 <span id="scope-6612755-h2" />

- <strong>适用地域</strong>：本文描述的功能仅在华北2（北京）地域可用，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
- 已成功[获取 API Key](/zh/model-studio/get-api-key)并[配置到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
- 已通过[创建调优任务](/zh/model-studio/create-fine-tuning-job-api)接口创建了调优任务。

## 查询调优任务状态 <span id="faa99e295ckyg" />

查询指定调优任务的详细信息和运行状态。

### 地址 <span id="addr-qftst-h3" />

```http
GET https://dashscope.aliyuncs.com/api/v1/fine-tunes/{job_id}
```

### 请求示例 <span id="reqex-qftst-h3" />

```bash
curl --location --request GET "https://dashscope.aliyuncs.com/api/v1/fine-tunes/<替换为您的调优任务id>" \
      --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --header 'Content-Type: application/json'
```

### 请求参数 <span id="9fbddca4907ka" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "19.93%" }} /><col style={{ width: "20.3%" }} /><col style={{ width: "13.82%" }} /><col style={{ width: "10.59%" }} /><col style={{ width: "35.36%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>String</p></td><td><p>Url Path</p></td><td><p>是</p></td><td><p>要查询的调优任务的ID。即<a href="/zh/model-studio/create-fine-tuning-job-api">创建调优任务返回参数</a>中的job\_id。</p></td></tr></tbody></table>

### 响应示例 <span id="f5e8e2d6b9zsd" />

<Tabs>
  <Tab title="文本生成模型">
    ```json expandable
    {
        "request_id": "c59b2145-a93c-4e00-b610-4d7cc5c521a2",
        "output": {
            "job_id": "ft-202410291653-1c7f",
            "job_name": "ft-202410291653-1c7f",
            "status": "SUCCEEDED",
            "finetuned_output": "qwen3-14b-suffix-ft-202410291653-1c7f",
            "model": "qwen3-14b",
            "base_model": "qwen3-14b",
            "training_file_ids": [],
            "validation_file_ids": [],
            "training_datasets": [
                {
                    "data_source_type": "file_id",
                    "file_id": "976bd01a-f30b-4414-86fd-50c54486e3ef"
                }
            ],
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
            "workspace_id": "llm-v71tlv***",
            "user_identity": "1396993924585947",
            "modifier": "1396993924585947",
            "creator": "1396993924585947",
            "end_time": "2024-10-29 17:11:26",
            "group": "llm",
            "usage": 279808
        }
    }
    ```
  </Tab>

  <Tab title="视频生成模型">
    关注 `output.status`（SUCCEEDED 表示训练完成）和 `output.usage`（训练消耗的总 Token 数量）。

    ```json expandable
    {
        "request_id": "9bbb953c-bef2-4b59-9fc5-xxxxxxxxx",
        "output": {
            "job_id": "ft-202511111122-xxxx",
            "status": "SUCCEEDED",
            "finetuned_output": "wan2.5-i2v-preview-ft-202511111122-xxxx",
            "model": "wan2.5-i2v-preview",
            "base_model": "wan2.5-i2v-preview",
            "training_file_ids": [],
            "validation_file_ids": [],
            "training_datasets": [
                {
                    "data_source_type": "file_id",
                    "file_id": "xxxxxxxxxxxx"
                }
            ],
            "validation_datasets": [],
            "hyper_parameters": {
                "n_epochs": 400,
                "learning_rate": 2e-05,
                "split": 0.9,
                "eval_epochs": 50
            },
            "training_type": "efficient_sft",
            "create_time": "2025-11-11 11:22:22",
            "end_time": "2025-11-11 16:49:01",
            "usage": 432000,
            "output_cnt": 8
        }
    }
    ```
  </Tab>

  <Tab title="图像生成模型">
    ```json expandable
    {
        "request_id": "03d738f5-3720-90b0-9c7b-xxxxxxxxx",
        "output": {
            "job_id": "ft-202606030110-xxxx",
            "status": "SUCCEEDED",
            "finetuned_output": "wan2.7-image-pro-ft-202606030110-xxxx",
            "model": "wan2.7-image-pro",
            "base_model": "wan2.7-image-pro",
            "training_file_ids": [],
            "validation_file_ids": [],
            "training_datasets": [
                {
                    "data_source_type": "file_id",
                    "file_id": "xxxxxxxxxxxx"
                }
            ],
            "validation_datasets": [],
            "hyper_parameters": {
                "max_steps": 800,
                "learning_rate": 3e-05,
                "eval_steps": 200,
                "max_token_length": "2k",
                "max_pixels": "2k",
                "val_img_size": "2k",
                "generation_type": "t2i",
                "lora_rank": 32
            },
            "training_type": "efficient_sft",
            "create_time": "2026-06-03 01:10:47",
            "end_time": "2026-06-03 01:38:53",
            "usage": 10273216,
            "output_cnt": 1
        }
    }
    ```
  </Tab>
</Tabs>

### 响应参数 <span id="e917ac3670zpr" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.67%" }} /><col style={{ width: "27.65%" }} /><col style={{ width: "46.68%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>参数说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的ID。</p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>调优任务的详细信息。</p></td></tr><tr><td><p>output.job\_id</p></td><td><p>String</p></td><td><p>本次调优的任务ID，用于后续查询状态。</p> <p>生成规则：<code>ft-\{yyyyMMddHHmm}-\{4位uuid}</code>。</p></td></tr><tr><td><p>output.jobs\_name</p></td><td><p>String</p></td><td><p>同<code>output.job\_id</code>。</p></td></tr><tr><td><p>output.status</p></td><td><p>String</p></td><td><p>本次调优<a href="/zh/model-studio/create-fine-tuning-job-api">任务的状态</a>。</p></td></tr><tr><td><p>output.finetuned\_output</p></td><td><p>String</p></td><td><p>仅调优任务状态为“SUCCEED”时出现，返回的是调优完成的模型ID。</p></td></tr><tr><td><p>output.model</p></td><td><p>String</p></td><td><p>调优任务使用的模型ID。</p></td></tr><tr><td><p>output.base\_model</p></td><td><p>String</p></td><td><p>调优任务使用的模型对应的基础模型ID。</p> <p>比如：调优任务<code>ft-202410291653-1c7f</code>使用的基础模型是<code>qwen3-14b</code>。</p></td></tr><tr><td><p>output.training\_file\_ids</p></td><td><p>Array</p></td><td><p>兼容字段，新任务始终返回空数组，请使用 training\_datasets。</p></td></tr><tr><td><p>output.validation\_file\_ids</p></td><td><p>Array</p></td><td><p>兼容字段，新任务始终返回空数组，请使用 validation\_datasets。</p></td></tr><tr><td><p>output.training\_datasets</p></td><td><p>Array of Dataset</p></td><td><p>训练数据集列表。</p></td></tr><tr><td><p>output.validation\_datasets</p></td><td><p>Array of Dataset</p></td><td><p>测试数据集列表。</p></td></tr><tr><td><p>output.hyper\_parameters</p></td><td><p>Object</p></td><td><p>显性声明过的超参表。</p></td></tr><tr><td><p>output.training\_type</p></td><td><p>String</p></td><td><p>调优方法。</p></td></tr><tr><td><p>output.create\_time</p></td><td><p>String</p></td><td><p>调优任务创建时间。</p></td></tr><tr><td><p>output.workspace\_id</p></td><td><p>String</p></td><td><p>调优任务所属的业务空间ID。</p></td></tr><tr><td><p>output.user\_identity</p></td><td><p>String</p></td><td><p>该调优任务隶属的主账号UID。</p></td></tr><tr><td><p>output.modifier</p></td><td><p>String</p></td><td><p>对该调优任务进行最后一次操作的账号UID。（比如：某个子账号取消了该任务，该子账号UID会显示在这里）</p></td></tr><tr><td><p>output.creator</p></td><td><p>String</p></td><td><p>该调优任务创建人UID。</p></td></tr><tr><td><p>output.end\_time</p></td><td><p>String</p></td><td><p>调优任务结束时间，当任务状态为“SUCCEED”、“FAILED”、“CANCELED”时出现。</p></td></tr><tr><td><p>output.group</p></td><td><p>String</p></td><td><p>模型调优的任务类型。</p></td></tr><tr><td><p>output.usage</p></td><td><p>Integer</p></td><td><p>调优任务消耗的Token（count）数，扣费计算公式请参考：计费说明。当任务状态为“SUCCEED”、“CANCELED”时出现。</p></td></tr><tr><td><p>output.output\_cnt</p></td><td><p>Integer</p></td><td><p>当前任务已产出的 Checkpoint 数量。仅对支持多 Checkpoint 输出的模型（如 <code>cosyvoice-v3-flash</code>）返回。Checkpoint 详细列表可通过查询 Checkpoint 接口获取。</p></td></tr><tr><td><p>output.max\_output\_cnt</p></td><td><p>Integer</p></td><td><p>单次任务可产出的 Checkpoint 数量上限。<code>output\_cnt</code> 超出上限的部分会按调优充分度截断。仅对支持多 Checkpoint 输出的模型返回。</p></td></tr></tbody></table>

## 取消调优任务 <span id="3gqm7xoh2njae" />

取消正在运行的调优任务。仅处于运行中状态的任务可被取消。

### 地址 <span id="addr-cancelft-h3" />

```http
POST https://dashscope.aliyuncs.com/api/v1/fine-tunes/{job_id}/cancel
```

### 请求示例 <span id="reqex-cancelft-h3" />

```bash
curl --location --request POST "https://dashscope.aliyuncs.com/api/v1/fine-tunes/<替换为您的调优任务id>/cancel" \
      --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --header 'Content-Type: application/json'
```

### 请求参数 <span id="5i7yrdng1qtgh" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.39%" }} /><col style={{ width: "15%" }} /><col style={{ width: "17.78%" }} /><col style={{ width: "19.17%" }} /><col style={{ width: "31.66%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>String</p></td><td><p>Url Path</p></td><td><p>是</p></td><td><p>要取消的调优任务ID，可通过<a href="/zh/model-studio/create-fine-tuning-job-api">创建训练任务</a>或列举训练任务接口获取。</p></td></tr></tbody></table>

### 响应示例 <span id="83u3lwnldunmk" />

```json
{
    "request_id": "670fca9d-48dd-4c5a-83e7-33bcc20420f8",
    "output": {
        "status": "success"
    }
}
```

### 响应参数 <span id="tfww4gjfkt6hz" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.69%" }} /><col style={{ width: "34.75%" }} /><col style={{ width: "39.56%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>参数说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的ID。</p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>取消操作返回的详细信息。</p></td></tr></tbody></table>

## 删除调优任务 <span id="c3mq93izbce81" />

删除指定的调优任务记录。

### 地址 <span id="addr-delft-h3" />

```http
DELETE https://dashscope.aliyuncs.com/api/v1/fine-tunes/{job_id}
```

### 请求示例 <span id="reqex-delft-h3" />

```bash
curl --location --request DELETE "https://dashscope.aliyuncs.com/api/v1/fine-tunes/<替换为您的调优任务id>" \
      --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --header 'Content-Type: application/json'
```

### 请求参数 <span id="knobd0sern5lt" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.02%" }} /><col style={{ width: "15.6%" }} /><col style={{ width: "12.77%" }} /><col style={{ width: "16.17%" }} /><col style={{ width: "38.44%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>String</p></td><td><p>Url Path</p></td><td><p>是</p></td><td><p>要删除的调优任务ID，可通过<a href="/zh/model-studio/create-fine-tuning-job-api">创建训练任务</a>或列举训练任务接口获取。</p></td></tr></tbody></table>

### 响应示例 <span id="95d3clte04c1w" />

```json
{
    "request_id": "7d7e1469-df77-4163-83ce-54425df744ed",
    "output": {
        "status": "success"
    }
}
```

### 响应参数 <span id="tuf932oscuuv3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.98%" }} /><col style={{ width: "19.04%" }} /><col style={{ width: "54.98%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>参数说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的ID。</p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>删除操作返回的详细信息。</p></td></tr></tbody></table>

## 查询调优任务列表 <span id="c23cdff6f399h" />

分页查询当前账号下的调优任务列表。

### 地址 <span id="addr-qftlist-h3" />

```http
GET https://dashscope.aliyuncs.com/api/v1/fine-tunes
```

### 请求示例 <span id="reqex-qftlist-h3" />

```bash
curl --location --request GET "https://dashscope.aliyuncs.com/api/v1/fine-tunes?model=qwen3-14b&page_no=2" \
      --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --header 'Content-Type: application/json'
```

### 请求参数 <span id="ab4ffd1a30d0p" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "12.8%" }} /><col style={{ width: "11.71%" }} /><col style={{ width: "17.35%" }} /><col style={{ width: "10.2%" }} /><col style={{ width: "47.94%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>是否必填</strong></p></th><th><p><strong>参数说明</strong></p></th></tr></thead><tbody><tr><td><p>page\_no</p></td><td><p>Integer</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>默认1</p></td></tr><tr><td><p>page\_size</p></td><td><p>Integer</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>默认10，最大值1000，最小值1</p></td></tr><tr><td><p>model</p></td><td><p>String</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>模型ID，指定该参数表示只列举基于该模型的调优任务</p></td></tr></tbody></table>

### 响应示例 <span id="7e4df7a8ealk9" />

```json expandable
{
    "request_id": "2182ef64-6398-457b-b3f6-5fde5fd6b388",
    "output": {
        "page_no": 2,
        "page_size": 10,
        "total": 12,
        "jobs": [
            {
                "job_id": "ft-202410291653-1c7f",
                "job_name": "ft-202410291653-1c7f",
                "status": "SUCCEEDED",
                "finetuned_output": "qwen3-14b-suffix-ft-202410291653-1c7f",
                "model": "qwen3-14b",
                "base_model": "qwen3-14b",
                "training_file_ids": [],
                "validation_file_ids": [],
                "training_datasets": [
                    {
                        "data_source_type": "file_id",
                        "file_id": "976bd01a-f30b-4414-86fd-50c54486e3ef"
                    }
                ],
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
                "workspace_id": "llm-v71tlv***",
                "user_identity": "1396993924585947",
                "modifier": "1396993924585947",
                "creator": "1396993924585947",
                "end_time": "2024-10-29 17:11:26",
                "group": "llm",
                "usage": 279808
            },
            {
                "job_id": "ft-202410291512-1851",
                "job_name": "ft-202410291512-1851",
                "status": "CANCELED",
                "model": "qwen3-14b",
                "base_model": "qwen3-14b",
                "training_file_ids": [],
                "validation_file_ids": [],
                "training_datasets": [
                    {
                        "data_source_type": "file_id",
                        "file_id": "86a9fe7f-dd77-43b0-9834-2170e12339ec"
                    },
                    {
                        "data_source_type": "file_id",
                        "file_id": "03ead352-6190-4328-8016-61821c23d4fc"
                    }
                ],
                "validation_datasets": [],
                "hyper_parameters": {
                    "n_epochs": 3,
                    "batch_size": 32,
                    "max_length": 8192,
                    "learning_rate": "1.6e-5",
                    "lr_scheduler_type": "linear",
                    "split": 0.9
                },
                "code": "1",
                "training_type": "sft",
                "create_time": "2024-10-29 15:12:00",
                "workspace_id": "llm-v71tlv***",
                "user_identity": "1396993924585947",
                "modifier": "1396993924585947",
                "creator": "1396993924585947",
                "end_time": "2024-10-29 15:16:00",
                "group": "llm",
                "usage": 0
            }
        ]
    }
}
```

### 响应参数 <span id="e34d1d71f4aov" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "20.37%" }} />

    <col style={{ width: "21.48%" }} />

    <col style={{ width: "58.15%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>参数名称</strong>
      </th>

      <th>
        <strong>类型</strong>
      </th>

      <th>
        <strong>参数说明</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        request\_id
      </td>

      <td>
        String
      </td>

      <td>
        本次请求的ID。
      </td>
    </tr>

    <tr>
      <td>
        output
      </td>

      <td>
        Object
      </td>

      <td>
        查询返回的详细信息。
      </td>
    </tr>

    <tr>
      <td>
        output.page\_no
      </td>

      <td>
        Integer
      </td>

      <td>
        页码。
      </td>
    </tr>

    <tr>
      <td>
        output.page\_size
      </td>

      <td>
        Integer
      </td>

      <td>
        每页显示的数据量。
      </td>
    </tr>

    <tr>
      <td>
        output.total
      </td>

      <td>
        Integer
      </td>

      <td>
        调优任务总计数量。
      </td>
    </tr>

    <tr>
      <td>
        output.jobs
      </td>

      <td>
        Array
      </td>

      <td>
        多个调优任务详情。

        <Accordion title="点击此处查看调优任务详情对象">
          <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "33.3%" }} /><col style={{ width: "33.3%" }} /><col style={{ width: "33.4%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>参数说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的ID。</p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>调优任务的详细信息。</p></td></tr><tr><td><p>output.job\_id</p></td><td><p>String</p></td><td><p>本次调优的任务ID，用于后续查询状态。</p> <p>生成规则：<code>ft-\{yyyyMMddHHmm}-\{4位uuid}</code>。</p></td></tr><tr><td><p>output.jobs\_name</p></td><td><p>String</p></td><td><p>同<code>output.job\_id</code>。</p></td></tr><tr><td><p>output.status</p></td><td><p>String</p></td><td><p>本次调优<a href="/zh/model-studio/create-fine-tuning-job-api">任务的状态</a>。</p></td></tr><tr><td><p>output.finetuned\_output</p></td><td><p>String</p></td><td><p>仅调优任务状态为“SUCCEED”时出现，返回的是调优完成的模型ID。</p></td></tr><tr><td><p>output.model</p></td><td><p>String</p></td><td><p>调优任务使用的模型ID。</p></td></tr><tr><td><p>output.base\_model</p></td><td><p>String</p></td><td><p>调优任务使用的模型对应的基础模型ID。</p> <p>比如：调优任务<code>ft-202410291653-1c7f</code>使用的基础模型是<code>qwen3-14b</code>。</p></td></tr><tr><td><p>output.training\_file\_ids</p></td><td><p>Array</p></td><td><p>兼容字段，新任务始终返回空数组，请使用 training\_datasets。</p></td></tr><tr><td><p>output.validation\_file\_ids</p></td><td><p>Array</p></td><td><p>兼容字段，新任务始终返回空数组，请使用 validation\_datasets。</p></td></tr><tr><td><p>output.training\_datasets</p></td><td><p>Array of Dataset</p></td><td><p>训练数据集列表。</p></td></tr><tr><td><p>output.validation\_datasets</p></td><td><p>Array of Dataset</p></td><td><p>测试数据集列表。</p></td></tr><tr><td><p>output.hyper\_parameters</p></td><td><p>Object</p></td><td><p>显性声明过的超参表。</p></td></tr><tr><td><p>output.training\_type</p></td><td><p>String</p></td><td><p>调优方法。</p></td></tr><tr><td><p>output.create\_time</p></td><td><p>String</p></td><td><p>调优任务创建时间。</p></td></tr><tr><td><p>output.workspace\_id</p></td><td><p>String</p></td><td><p>调优任务所属的业务空间ID。</p></td></tr><tr><td><p>output.user\_identity</p></td><td><p>String</p></td><td><p>该调优任务隶属的主账号UID。</p></td></tr><tr><td><p>output.modifier</p></td><td><p>String</p></td><td><p>对该调优任务进行最后一次操作的账号UID。（比如：某个子账号取消了该任务，该子账号UID会显示在这里）</p></td></tr><tr><td><p>output.creator</p></td><td><p>String</p></td><td><p>该调优任务创建人UID。</p></td></tr><tr><td><p>output.end\_time</p></td><td><p>String</p></td><td><p>调优任务结束时间，当任务状态为“SUCCEED”、“FAILED”、“CANCELED”时出现。</p></td></tr><tr><td><p>output.group</p></td><td><p>String</p></td><td><p>模型调优的任务类型。</p></td></tr><tr><td><p>output.usage</p></td><td><p>Integer</p></td><td><p>调优任务消耗的Token（count）数，扣费计算公式请参考：计费说明。当任务状态为“SUCCEED”、“CANCELED”时出现。</p></td></tr><tr><td><p>output.output\_cnt</p></td><td><p>Integer</p></td><td><p>当前任务已产出的 Checkpoint 数量。仅对支持多 Checkpoint 输出的模型（如 <code>cosyvoice-v3-flash</code>）返回。Checkpoint 详细列表可通过查询 Checkpoint 接口获取。</p></td></tr><tr><td><p>output.max\_output\_cnt</p></td><td><p>Integer</p></td><td><p>单次任务可产出的 Checkpoint 数量上限。<code>output\_cnt</code> 超出上限的部分会按调优充分度截断。仅对支持多 Checkpoint 输出的模型返回</p></td></tr></tbody></table>
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## 查询调优任务日志 <span id="e76df8eaa2vbs" />

查询指定调优任务的训练日志。

### 地址 <span id="addr-qftlog-h3" />

```http
GET https://dashscope.aliyuncs.com/api/v1/fine-tunes/{job_id}/logs
```

### 请求示例 <span id="reqex-qftlog-h3" />

```bash
curl --location --request GET "https://dashscope.aliyuncs.com/api/v1/fine-tunes/<替换为您的调优任务id>/logs?offset=10&line=10" \
      --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --header 'Content-Type: application/json'
```

### 请求参数 <span id="b57741b445w6r" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "13.05%" }} /><col style={{ width: "11.95%" }} /><col style={{ width: "9.73%" }} /><col style={{ width: "11.39%" }} /><col style={{ width: "53.88%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>String</p></td><td><p>Url Path</p></td><td><p>是</p></td><td><p>要打印日志的调优任务ID，可通过创建训练任务或列举训练任务接口获取。</p></td></tr><tr><td><p>offset</p></td><td><p>Number</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>忽略前<code>offset</code>行输出，从第<code>offset+1</code>行开始读取。不能超过当前已有输出的总行数，超过则不会有输出信息返回。默认值为0。</p></td></tr><tr><td><p>line</p></td><td><p>Number</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>从<code>offset+1</code>行（包含）起，读取<code>line</code>行输出信息，如果本次请求的输出不足<code>line</code>行，则以实际输出为准。默认值为100，上限为1000。</p></td></tr></tbody></table>

### 响应示例 <span id="6464563a5bnw1" />

```json
{
    "request_id": "ce49b45d-fe46-474e-9e1b-3e7427ffdf5a",
    "output": {
        "total": 20,
        "logs": [
            "{'train_runtime': 216.3999, 'train_samples_per_second': 2.066, 'train_steps_per_second': 0.014, 'train_loss': 0.9122632344563802, 'epoch': 0.8571428571428571}",
            " Actual number of consumed tokens is 279808!",
            " Uploaded checkpoint!",
            " Fine-tune succeeded!",
            " use checkpoint-3 as final checkpoint",
            "2024-10-29 17:03:47,719 - INFO - transfer for inference succeeded, start to deliver it for inference",
            "2024-10-29 17:09:43,322 - INFO - start to save checkpoint",
            "2024-10-29 17:11:24,689 - INFO - finetune-job succeeded",
            "2024-10-29 17:11:25,130 - INFO - training usage 279808",
            "2024-10-29 17:11:25,175 - INFO - ##FT_COMPLETE##"
        ]
    }
}
```

### 响应参数 <span id="4a15fcece6z9u" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "23.13%" }} /><col style={{ width: "37.47%" }} /><col style={{ width: "39.4%" }} /></colgroup><thead><tr><th><p><strong>参数名称</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>参数说明</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的ID。</p></td></tr><tr><td><p>output</p></td><td><p>Object</p></td><td><p>查询返回的详细信息。</p></td></tr><tr><td><p>output.total</p></td><td><p>Integer</p></td><td><p>日志总计行数。</p></td></tr><tr><td><p>output.logs</p></td><td><p>Array</p></td><td><p>输出的日志。</p></td></tr></tbody></table>

## 异常响应 <span id="cc08546c7csjy" />

### 响应示例 <span id="errresp-example-h3" />

```json
{
    "code": "NotFound",
    "request_id": "BE213CDD-8A5C-59EE-9A67-055EAB0CB59B",
    "message": "Not Found!"
}
```

### 响应参数 <span id="errresp-params-h3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.9%" }} /><col style={{ width: "15.2%" }} /><col style={{ width: "32.55%" }} /><col style={{ width: "29.35%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>code</p></td><td><p>String</p></td><td><p>错误码。</p></td><td><p>NotFound</p></td></tr><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统唯一码。</p></td><td><p>6332fb02-3111-43f0-bf79-f9e8c5ffa7f9</p></td></tr><tr><td><p>message</p></td><td><p>String</p></td><td><p>错误信息。</p></td><td><p>Not Found!</p></td></tr></tbody></table>

当请求出错时，可能返回以下错误：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "10.87%" }} /><col style={{ width: "16.58%" }} /><col style={{ width: "26.45%" }} /><col style={{ width: "20.02%" }} /><col style={{ width: "26.08%" }} /></colgroup><thead><tr><th><p><strong>HTTP状态码</strong></p></th><th><p><strong>错误码</strong></p></th><th><p><strong>错误信息举例</strong></p></th><th><p><strong>含义</strong></p></th><th><p><strong>处理方式</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>Missing training files</p></td><td><p>参数错误，缺少参数或者参数格式问题等。</p></td><td><p>根据错误信息，修正您的参数。</p></td></tr><tr><td><p>400</p></td><td><p>UnsupportedOperation</p></td><td><p>The fine-tune job can not be canceled because it is succeeded or failed</p></td><td><p>当资源处于特定状态时，无法对其进行操作。</p></td><td><p>待要操作的资源到达可操作状态时再进行操作。</p></td></tr><tr><td><p>400</p></td><td><p>UnsupportedOperation</p></td><td><p>The fine-tune job can not be deleted because it is succeeded,failed or canceled</p></td><td><p>当资源处于特定状态时，无法对其进行操作。</p></td><td><p>待要操作的资源到达可操作状态时再进行操作。</p></td></tr><tr><td><p>404</p></td><td><p>NotFound</p></td><td><p>Not found!</p></td><td><p>要查询/操作的资源不存在。</p></td><td><p>检查要查询/操作的资源ID是否错误。</p></td></tr><tr><td><p>409</p></td><td><p>Conflict</p></td><td><p>Model instance xxxxx already exists, please specify a suffix</p></td><td><p>已存在deployed\_model名为xxxxx的部署实例，需要指定后缀进行区分。</p></td><td><p>为部署指定唯一的后缀。</p></td></tr><tr><td><p>429</p></td><td><p>Throttling</p></td><td><ul><li><p>Too many fine-tune job in running, please retry later.</p></li> <li><p>Only 20 fine-tune job in running or succeeded allowed per user.</p></li></ul></td><td><p>资源的创建触发平台限制。</p></td><td><ul><li><p>删除不再使用的模型。</p></li> <li><p>如您确实需要提高调优任务的并发量或保留更多调优成功的模型，请联系商务经理。</p></li></ul></td></tr><tr><td><p>500</p></td><td><p>InternalError</p></td><td><p>Internal server error!</p></td><td><p>内部错误。</p></td><td><p>记录 request\_id，通过工单联系阿里云工程师进行排查。</p></td></tr></tbody></table>

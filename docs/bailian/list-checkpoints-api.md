> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Checkpoint 管理

> 管理调优任务产出的 Checkpoint：列举、导出、查询验证结果，及 Checkpoint 对象说明。

<meta property="og:title" content="Checkpoint 管理" />

## 适用范围 <span id="scope-h2" />

- <strong>适用地域</strong>：本文描述的功能仅在华北2（北京）地域可用，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
- <strong>开通账号权限</strong>：若使用[阿里云子账号](/zh/model-studio/permission-management-overview)（[RAM用户](https://help.aliyun.com/zh/ram/user-guide/overview-of-ram-users)），需要为子账号授予模型调用、训练和部署[权限](/zh/model-studio/use-workspace)。
- <strong>配置环境变量</strong>：已成功[获取 API Key](/zh/model-studio/get-api-key)，并[配置到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。
- <strong>接口支持的微调模型类型：</strong>

  - [列举 Checkpoint](/zh/model-studio/list-checkpoints-api#51ryvkmpq074r)：文本生成、视频生成、图像生成、语音合成
  - [导出 Checkpoint](/zh/model-studio/list-checkpoints-api#a7k3m9x2q8r4t)：文本生成、视频生成、图像生成、语音合成
  - [查询Checkpoint验证列表](/zh/model-studio/list-checkpoints-api#d5cd230b48sab)：视频生成、图像生成
  - [查询Checkpoint验证结果](/zh/model-studio/list-checkpoints-api#09c9c6eb85lxz)：视频生成、图像生成

## 列举 Checkpoint <span id="51ryvkmpq074r" />

列举指定调优任务产出的所有 Checkpoint 及其状态、模型信息等。

### 请求接口 <span id="7htlg4h3cm29q" />

```http
GET https://dashscope.aliyuncs.com/api/v1/fine-tunes/{job_id}/checkpoints
```

### 入参描述 <span id="lcp-h3-input" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "14.74%" }} /><col style={{ width: "12.88%" }} /><col style={{ width: "10.94%" }} /><col style={{ width: "8.07%" }} /><col style={{ width: "27.77%" }} /><col style={{ width: "25.6%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>Path parameter</p></td><td><p>string</p></td><td><p>是</p></td><td><p>调优任务ID，可通过<a href="/zh/model-studio/create-fine-tuning-job-api">创建训练任务</a>或列举训练任务接口获取。</p></td><td><p>ft-202511111122-xxxx</p></td></tr></tbody></table>

### 出参描述 <span id="o1ja9kgfy5hl7" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "24.214876%" }} />

    <col style={{ width: "12.396694%" }} />

    <col style={{ width: "32.066116%" }} />

    <col style={{ width: "31.322314%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>字段</strong>
      </th>

      <th>
        <strong>类型</strong>
      </th>

      <th>
        <strong>描述</strong>
      </th>

      <th>
        <strong>示例值</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        request\_id
      </td>

      <td>
        string
      </td>

      <td>
        请求的唯一标识符。
      </td>

      <td>
        aa4b0229-b1db-9afa-bb6e-xxxxxxxxx
      </td>
    </tr>

    <tr>
      <td>
        output
      </td>

      <td>
        array\[object]
      </td>

      <td>
        Checkpoint 数组。按 step 从大到小排序。

        > 语音合成模型（如 CosyVoice）的 step 由 LM epoch 与 FM epoch 组合而成，排序按 LM epoch × FM epoch 的乘积从大到小（乘积越大代表双端调优越充分）。
      </td>

      <td>
        -
      </td>
    </tr>

    <tr>
      <td>
        output\[].checkpoint\_id
      </td>

      <td>
        string
      </td>

      <td>
        Checkpoint 唯一标识，格式为 `{job_id}:checkpoint-{step编号}`。不同模型类型的 step 编号格式不同，详见 `step` 字段说明。
      </td>

      <td>
        ft-202605271743-dd2a:checkpoint-00040004
      </td>
    </tr>

    <tr>
      <td>
        output\[].full\_name
      </td>

      <td>
        string
      </td>

      <td>
        同 `checkpoint_id`。
      </td>

      <td>
        ft-202605271743-dd2a:checkpoint-00040004
      </td>
    </tr>

    <tr>
      <td>
        output\[].checkpoint
      </td>

      <td>
        string
      </td>

      <td>
        Checkpoint 名称，格式为 `checkpoint-{step编号}`。

        > 语音合成模型（如 CosyVoice）的 step 编号由 LM epoch 和 FM epoch 组合而成，例如 `checkpoint-00040004` 表示 LM 第 4 轮 + FM 第 4 轮。
      </td>

      <td>
        checkpoint-00040004
      </td>
    </tr>

    <tr>
      <td>
        output\[].job\_id
      </td>

      <td>
        string
      </td>

      <td>
        该 Checkpoint 所属的调优任务 ID。
      </td>

      <td>
        ft-202605271743-dd2a
      </td>
    </tr>

    <tr>
      <td>
        output\[].step
      </td>

      <td>
        integer
      </td>

      <td>
        Checkpoint 对应的训练步数编码。

        > 语音合成模型（如 CosyVoice）的 step 计算方式为 `LM_epoch × 10000 + FM_epoch`，例如 40004 表示 LM 第 4 轮 + FM 第 4 轮。
      </td>

      <td>
        40004
      </td>
    </tr>

    <tr>
      <td>
        output\[].status
      </td>

      <td>
        string
      </td>

      <td>
        该 Checkpoint 的状态。常见值：`SUCCEEDED`（已就绪，可用于部署）、`PENDING`（尚未就绪）。
      </td>

      <td>
        SUCCEEDED
      </td>
    </tr>

    <tr>
      <td>
        output\[].model\_name
      </td>

      <td>
        string
      </td>

      <td>
        该 Checkpoint 对应的模型 ID，可作为[部署模型](/zh/model-studio/create-deployment-api)接口的 `model_name` 入参。<strong>仅在</strong>`status=SUCCEEDED`<strong>时返回</strong>。
      </td>

      <td>
        cosyvoice-v3-flash-ft-202605271743-dd2a
      </td>
    </tr>

    <tr>
      <td>
        output\[].model\_display\_name
      </td>

      <td>
        string
      </td>

      <td>
        模型显示名（控制台展示用）。仅在 `status=SUCCEEDED` 时返回。
      </td>

      <td>
        ft-202605271743-dd2a
      </td>
    </tr>

    <tr>
      <td>
        output\[].create\_time
      </td>

      <td>
        string
      </td>

      <td>
        该 Checkpoint 的创建时间，格式为 ISO 8601。
      </td>

      <td>
        2026-05-27T18:07:16
      </td>
    </tr>

    <tr>
      <td>
        output\[].expire\_time
      </td>

      <td>
        string
      </td>

      <td>
        该 Checkpoint 的过期时间，格式为 ISO 8601。
      </td>

      <td>
        2026-06-11T18:07:16
      </td>
    </tr>
  </tbody>
</table>

### 请求示例 <span id="lcp-h3-reqex" />

请将 URL 中的 `<替换为调优任务job_id>` 完整替换为[创建训练任务](/zh/model-studio/create-fine-tuning-job-api)输出参数`job_id`的值。

> Windows CMD 请将`${DASHSCOPE_API_KEY}`替换为 `%DASHSCOPE_API_KEY%`，PowerShell 请替换为 `$env:DASHSCOPE_API_KEY`

```bash
curl --location 'https://dashscope.aliyuncs.com/api/v1/fine-tunes/<替换为调优任务job_id>/checkpoints' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY" \
--header 'Content-Type: application/json'
```

### 响应示例 <span id="yoj2qdrstg8m7" />

```json expandable
{
    "request_id": "aa4b0229-b1db-9afa-bb6e-3b3e9ee1489b",
    "output": [
        {
            "create_time": "2026-05-27T18:07:16",
            "full_name": "ft-202605271743-dd2a:checkpoint-00040004",
            "job_id": "ft-202605271743-dd2a",
            "checkpoint_id": "ft-202605271743-dd2a:checkpoint-00040004",
            "checkpoint": "checkpoint-00040004",
            "model_name": "cosyvoice-v3-flash-ft-202605271743-dd2a",
            "model_display_name": "ft-202605271743-dd2a",
            "status": "SUCCEEDED",
            "expire_time": "2026-06-11T18:07:16",
            "step": 40004
        },
        {
            "create_time": "2026-05-27T18:07:16",
            "full_name": "ft-202605271743-dd2a:checkpoint-00030004",
            "job_id": "ft-202605271743-dd2a",
            "checkpoint_id": "ft-202605271743-dd2a:checkpoint-00030004",
            "checkpoint": "checkpoint-00030004",
            "status": "PENDING",
            "expire_time": "2026-06-11T18:07:16",
            "step": 30004
        }
    ]
}
```

## 导出 Checkpoint <span id="a7k3m9x2q8r4t" />

将指定 Checkpoint 导出为可部署模型。导出成功后，需根据模型类型调用对应的[部署接口](/zh/model-studio/deployments-api)（不同模型类型的部署接口不同）。

### 请求接口 <span id="660dd7600bnl4" />

```http
GET https://dashscope.aliyuncs.com/api/v1/fine-tunes/{job_id}/export/{checkpoint}?model_name={model_name}
```

### 入参描述 <span id="61f9eb431f29b" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "14.74%" }} /><col style={{ width: "12.88%" }} /><col style={{ width: "10.94%" }} /><col style={{ width: "8.07%" }} /><col style={{ width: "27.77%" }} /><col style={{ width: "25.6%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>Path parameter</p></td><td><p>string</p></td><td><p>是</p></td><td><p>调优任务ID，可通过<a href="/zh/model-studio/create-fine-tuning-job-api">创建训练任务</a>或列举训练任务接口获取。</p></td><td><p>ft-202511111122-xxxx</p></td></tr><tr><td><p>checkpoint</p></td><td><p>Path parameter</p></td><td><p>string</p></td><td><p>是</p></td><td><p>Checkpoint名称，可通过列举Checkpoint接口获取。</p></td><td><p>checkpoint-160</p></td></tr><tr><td><p>model\_name</p></td><td><p>Query parameter</p></td><td><p>string</p></td><td><p>是</p></td><td><p>用于在控制台中展示的导出模型名称。</p> <p>该名称需全局唯一，建议使用中英文、数字、下划线（\_）和短横线（-）字符。</p> <p>请注意：此参数仅用于控制台显示，实际导出的模型名称以列举 Checkpoint输出参数<code>output\[].model\_name</code>为准。</p></td><td><p>wan2.5-i2v-preview-ft-202511111122-xxxx</p></td></tr></tbody></table>

### 出参描述 <span id="8d2a1f7722aek" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24.214876%" }} /><col style={{ width: "12.396694%" }} /><col style={{ width: "32.066116%" }} /><col style={{ width: "31.322314%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>string</p></td><td><p>请求的唯一标识符。</p></td><td><p>0eb05b0c-02ba-414a-9d0c-xxxxxxxxx</p></td></tr><tr><td><p>output</p></td><td><p>boolean</p></td><td><p>导出请求是否提交成功。</p> <ul><li><p>true：表示导出请求提交成功。</p></li> <li><p>false：表示导出请求提交失败，建议重试。</p></li></ul></td><td><p>true</p></td></tr></tbody></table>

### 请求示例 <span id="95084f5ca8rbv" />

- `<替换为调优任务job_id>`：完整替换为[创建训练任务](/zh/model-studio/create-fine-tuning-job-api)输出参数`job_id`的值。
- `<替换为待导出的checkpoint>`：完整替换为checkpoint的值，例如“checkpoint-160”。
- `<替换为控制台展示的导出模型名称>`：完整替换为自定义的模型名称，仅用于控制台展示。

```bash
curl --location 'https://dashscope.aliyuncs.com/api/v1/fine-tunes/<替换为调优任务job_id>/export/<替换为待导出的checkpoint>?model_name=<替换为控制台展示的导出模型名称>' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY"
```

### 响应示例 <span id="7f48f5ef53zqy" />

```json
{
    "request_id": "0817d1ed-b6b6-4383-9650-xxxxx",
    "output": true
}
```

## 查询Checkpoint验证列表 <span id="d5cd230b48sab" />

<Note>
  文本生成模型不支持此接口。
</Note>

查询通过验证集成功生成预览视频或图像的 Checkpoint 列表，验证失败的不会列出。

<strong>使用限制</strong>：该接口需在模型调优完成后调用，否则将返回空列表。

### 请求接口 <span id="d2e05f2b54nge" />

```http
GET https://dashscope.aliyuncs.com/api/v1/fine-tunes/{job_id}/validation-results
```

### 入参描述 <span id="d1f7c4ceb44y5" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "14.74%" }} /><col style={{ width: "12.9%" }} /><col style={{ width: "10.95%" }} /><col style={{ width: "8.09%" }} /><col style={{ width: "27.74%" }} /><col style={{ width: "25.58%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>Path parameter</p></td><td><p>string</p></td><td><p>是</p></td><td><p>调优任务ID，可通过<a href="/zh/model-studio/create-fine-tuning-job-api">创建训练任务</a>或列举训练任务接口获取。</p></td><td><p>ft-202511111122-xxxx</p></td></tr></tbody></table>

### 出参描述 <span id="d4895c3b00e23" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24.214876%" }} /><col style={{ width: "12.396694%" }} /><col style={{ width: "32.066116%" }} /><col style={{ width: "31.322314%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>string</p></td><td><p>请求的唯一标识符。</p></td><td><p>0eb05b0c-02ba-414a-9d0c-xxxxxxxxx</p></td></tr><tr><td><p>output</p></td><td><p>array\[string]</p></td><td><p>Checkpoint列表。</p></td><td><p>-</p></td></tr><tr><td><p>output\[].checkpoint</p></td><td><p>string</p></td><td><p>Checkpoint名称。</p></td><td><p>checkpoint-160</p></td></tr></tbody></table>

### 请求示例 <span id="b7fcd7371av2m" />

请将 URL 中的 `<替换为调优任务job_id>` 完整替换为[创建训练任务](/zh/model-studio/create-fine-tuning-job-api)输出参数`job_id`的值。

```bash
curl --location 'https://dashscope.aliyuncs.com/api/v1/fine-tunes/<替换为调优任务job_id>/validation-results' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY" \
--header 'Content-Type: application/json'
```

### 响应示例 <span id="55f33732fcibs" />

```json
{
    "request_id": "da1310f5-5a21-4e29-99d4-xxxxxx",
    "output": [
        {
            "checkpoint": "checkpoint-160"
        },
        {
            "checkpoint": "checkpoint-20"
        },
        {
            "checkpoint": "checkpoint-40"
        },
        {
            "checkpoint": "checkpoint-60"
        }
    ]
}
```

## 查询Checkpoint验证结果 <span id="09c9c6eb85lxz" />

<Note>
  文本生成模型不支持此接口。
</Note>

根据 `checkpoint`（例如 "checkpoint-160"），查询指定 Checkpoint 的验证结果详情，包括生成的预览视频或图像。

### 请求接口 <span id="f8c67434d6fo4" />

```http
GET https://dashscope.aliyuncs.com/api/v1/fine-tunes/{job_id}/validation-details/{checkpoint}?page_no=1&page_size=10
```

### 入参描述 <span id="eb3da6ffc3xrq" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "14.74%" }} /><col style={{ width: "12.88%" }} /><col style={{ width: "10.94%" }} /><col style={{ width: "8.07%" }} /><col style={{ width: "27.77%" }} /><col style={{ width: "25.6%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>job\_id</p></td><td><p>Path parameter</p></td><td><p>string</p></td><td><p>是</p></td><td><p>调优任务ID，可通过创建训练任务或列举训练任务接口获取。</p></td><td><p>ft-202511111122-xxxx</p></td></tr><tr><td><p>checkpoint</p></td><td><p>Path parameter</p></td><td><p>string</p></td><td><p>是</p></td><td><p>Checkpoint名称，可通过列举 Checkpoint 或查询Checkpoint验证列表接口获取。</p></td><td><p>checkpoint-160</p></td></tr><tr><td><p>page\_no</p></td><td><p>Query parameter</p></td><td><p>integer</p></td><td><p>否</p></td><td><p>页码。默认为1。</p></td><td><p>1</p></td></tr><tr><td><p>page\_size</p></td><td><p>Query parameter</p></td><td><p>integer</p></td><td><p>否</p></td><td><p>每页数量。默认为10。</p></td><td><p>10</p></td></tr></tbody></table>

### 出参描述 <span id="ca68f349d78mu" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24.214876%" }} /><col style={{ width: "12.396694%" }} /><col style={{ width: "32.066116%" }} /><col style={{ width: "31.322314%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>string</p></td><td><p>请求的唯一标识符。</p></td><td><p>375b3ad0-d3fa-451f-b629-xxxxxxx</p></td></tr><tr><td><p>output</p></td><td><p>object</p></td><td><p>输出结果。</p></td><td><p>-</p></td></tr><tr><td><p>output.page\_no</p></td><td><p>integer</p></td><td><p>页码。</p></td><td><p>1</p></td></tr><tr><td><p>output.page\_size</p></td><td><p>integer</p></td><td><p>每页数量。</p></td><td><p>10</p></td></tr><tr><td><p>output.total</p></td><td><p>integer</p></td><td><p>验证集列表总数量。</p></td><td><p>1</p></td></tr><tr><td><p>output.list</p></td><td><p>array\[object]</p></td><td><p>验证集列表。</p></td><td><p>-</p></td></tr><tr><td><p>output.list\[].video\_path</p></td><td><p>string</p></td><td><p>通过Checkpoint生成的视频。</p> <p>video\_path有效期为24小时，请及时下载视频。</p></td><td><p>[https://finetune-swap-wulanchabu.oss-cn-wulanchabu.aliyuncs.com/xxx.mp4?Expires=xxxx](https://finetune-swap-wulanchabu.oss-cn-wulanchabu.aliyuncs.com/xxx.mp4?Expires=xxxx)</p></td></tr><tr><td><p>output.list\[].prompt</p></td><td><p>string</p></td><td><p>验证数据的prompt。从数据集的标注文件data.jsonl获得。</p></td><td><p>视频开头展示了一位年轻男性坐在咖啡馆的场景...</p></td></tr><tr><td><p>output.list\[].first\_frame\_path</p></td><td><p>string</p></td><td><p>验证的图像地址。系统会读取数据集中的图像，并生成一个公网URL地址。</p></td><td><p>[https://finetune-swap-wulanchabu.oss-cn-wulanchabu.aliyuncs.com/xxx.jpeg](https://finetune-swap-wulanchabu.oss-cn-wulanchabu.aliyuncs.com/xxx.jpeg)</p></td></tr></tbody></table>

### 请求示例 <span id="0a3269f27c7bx" />

- `<替换为调优任务job_id>`： 完整替换为创建训练任务输出参数`job_id`的值。
- `<替换为选择的checkpoint>`：完整替换为选定的Checkpoint名称，例如“checkpoint-160”。

```bash
curl --location 'https://dashscope.aliyuncs.com/api/v1/fine-tunes/<替换为调优任务job_id>/validation-details/<替换为选择的checkpoint>?page_no=1&page_size=10' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY"
```

### 响应示例 <span id="ad1335d3f4v9d" />

> video\_path有效期为24小时，请及时下载视频。

```json
{
    "request_id": "375b3ad0-d3fa-451f-b629-xxxxxxx",
    "output": {
        "page_no": 1,
        "page_size": 10,
        "total": 1,
        "list": [
            {
                "video_path": "https://finetune-swap-wulanchabu.oss-cn-wulanchabu.aliyuncs.com/xxx.mp4?Expires=xxxx",
                "prompt": "视频开头展示了一位年轻男性坐在咖啡馆的场景。他穿着一件米色的Polo衫，神情专注且略显沉思，手指轻轻托着下巴。他的面前摆放着一杯热气腾腾的咖啡，背景是木质条纹的墙壁和一个装饰牌。然后开始展示s86b5p金钱雨特效，无数巨大尺寸的美元钞票（米黄底/深绿图案）如暴雨般倾泻而下，密集地砸向并环绕他。钞票持续落下，他双臂舒展上扬，脖颈微仰，表情惊喜，完全沉浸在这场狂野的金钱雨中。",
                "first_frame_path": "https://finetune-swap-wulanchabu.oss-cn-wulanchabu.aliyuncs.com/xxx.jpeg"
            }
        ]
    }
}
```

## Checkpoint 对象 <span id="c4d5m7n9p1q3t" />

> 当前 Checkpoint API 仅在北京 Region 开放。如您使用其他 Region，请通过该 Region 的百炼控制台管理 Checkpoint。

检查点对象（Checkpoint）表示调优过程中保存的中间模型状态。通过列举检查点接口返回。

### 字段说明 <span id="204f3d1mpqi7f" />

以下字段来自列举检查点接口的返回结果。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "25.67%" }} />

    <col style={{ width: "27.65%" }} />

    <col style={{ width: "46.68%" }} />
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
        Array
      </td>

      <td>
        Checkpoint 数组。按 step 从大到小排序。

        > 语音合成模型（如 CosyVoice）的 step 由 LM epoch 与 FM epoch 组合而成，排序按 LM epoch × FM epoch 的乘积从大到小（乘积越大代表双端调优越充分）。
      </td>
    </tr>

    <tr>
      <td>
        output\[\*].checkpoint\_id
      </td>

      <td>
        String
      </td>

      <td>
        Checkpoint 唯一标识，格式为 `{job_id}:checkpoint-{step编号}`。不同模型类型的 step 编号格式不同，详见 `step` 字段说明。
      </td>
    </tr>

    <tr>
      <td>
        output\[\*].full\_name
      </td>

      <td>
        String
      </td>

      <td>
        同 `checkpoint_id`。
      </td>
    </tr>

    <tr>
      <td>
        output\[\*].checkpoint
      </td>

      <td>
        String
      </td>

      <td>
        Checkpoint 名称，格式为 `checkpoint-{step编号}`。

        > 语音合成模型（如 CosyVoice）的 step 编号由 LM epoch 和 FM epoch 组合而成，例如 `checkpoint-00040004` 表示 LM 第 4 轮 + FM 第 4 轮。
      </td>
    </tr>

    <tr>
      <td>
        output\[\*].job\_id
      </td>

      <td>
        String
      </td>

      <td>
        该 Checkpoint 所属的调优任务 ID。
      </td>
    </tr>

    <tr>
      <td>
        output\[\*].step
      </td>

      <td>
        Integer
      </td>

      <td>
        Checkpoint 对应的训练步数编码。

        > 语音合成模型（如 CosyVoice）的 step 计算方式为 `LM_epoch × 10000 + FM_epoch`，例如 40004 表示 LM 第 4 轮 + FM 第 4 轮。
      </td>
    </tr>

    <tr>
      <td>
        output\[\*].status
      </td>

      <td>
        String
      </td>

      <td>
        该 Checkpoint 的状态。常见值：`SUCCEEDED`（已就绪，可用于部署）、`PENDING`（尚未就绪）。
      </td>
    </tr>

    <tr>
      <td>
        output\[\*].model\_name
      </td>

      <td>
        String
      </td>

      <td>
        该 Checkpoint 对应的模型 ID，可作为[部署模型](/zh/model-studio/create-deployment-api)接口的 `model_name` 入参。<strong>仅在</strong>`status=SUCCEEDED`<strong>时返回</strong>。
      </td>
    </tr>

    <tr>
      <td>
        output\[\*].model\_display\_name
      </td>

      <td>
        String
      </td>

      <td>
        模型显示名（控制台展示用）。仅在 `status=SUCCEEDED` 时返回。
      </td>
    </tr>

    <tr>
      <td>
        output\[\*].create\_time
      </td>

      <td>
        String
      </td>

      <td>
        该 Checkpoint 的创建时间，格式为 ISO 8601。
      </td>
    </tr>

    <tr>
      <td>
        output\[\*].expire\_time
      </td>

      <td>
        String
      </td>

      <td>
        该 Checkpoint 的过期时间，格式为 ISO 8601。
      </td>
    </tr>
  </tbody>
</table>

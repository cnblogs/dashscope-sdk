> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 人物形象训练API详情

<meta property="og:title" content="人物形象训练API详情" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 人物形象训练 <span id="40e48c80477de" />

<Note>
  支持的领域 / 任务：aigc /facechain人物写真生成
</Note>

对上传的图像压缩包进行模型训练，从而获得该图像中对应人物的resource，基于该resource可以实现人物的写真生成。

人物形象训练是人物写真生成的前置环节，只有在针对某人物进行形象训练并成功获得形象resource后，才可基于此进行生成。

## 模型概览 <span id="ad4a5f601bhtp" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "26.197183%" }} /><col style={{ width: "73.802817%" }} /></colgroup><tbody><tr><td><p>模型名</p></td><td><p>模型简介</p></td></tr><tr><td><p>facechain-finetune</p></td><td><p>对上传的图像进行模型训练，从而获得该图像中对应人物的resource，基于该resource可以实现人物的写真生成。</p></td></tr></tbody></table>

## HTTP调用接口 <span id="08675ef11bhvi" />

### 功能描述 <span id="8f79b5d0f8ker" />

本接口为模型定制类服务，需要相对较长的算法调用时间，所以在接口层面采用了异步调用的方式进行任务提交，在通过任务接口提交作业之后，系统会返回对应的作业ID，随后可以通过对模型定制类任务的查询/管理接口进行相应操作。

### 前提条件 <span id="48063821f9diz" />

- 已开通服务并获得API-KEY：[获取与配置 API Key](/zh/model-studio/get-api-key)。

<Note>
  接口限制：对单账户（含主账号与RAM子账号）任务下发接口限制QPS为2，并发任务数量限制为1。
</Note>

### 准备训练数据 <span id="82e57f5073x31" />

准备1\~10张包含人脸的图片，您可以通过以下方式将这些图片提供给训练服务：

1、将这些图片存储在文件服务（例如阿里云oss）中，并生成授权的访问链接【推荐】

2、如果您没有合适的文件服务，可以将这些图片分别或打包成zip文件上传至大模型服务平台提供的模型定制文件管理服务中，并获取file\_id。

### 文件管理 <span id="5a4bf92073ieg" />

您可以使用DashScope提供的文件服务来管理您的训练文件，详细的api请参考[百炼文件管理 API](/zh/model-studio/model-customization-file-management-service)。

### 步骤1：创建模型定制任务 <span id="8958687073a4w" />

```http
POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/fine-tunes
```

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

##### 功能描述 <span id="8958687173s44" />

完成训练数据的上传之后，就可以使用在上传之后得到的 file\_id 发起facechain模型定制任务。

##### 入参描述 <span id="8958687373a0e" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.045455%" }} /><col style={{ width: "8.585859%" }} /><col style={{ width: "9.722222%" }} /><col style={{ width: "6.313131%" }} /><col style={{ width: "19.949495%" }} /><col style={{ width: "38.383838%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>Content-Type</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>请求类型：application/json</p></td><td><p>application/json</p></td></tr><tr><td><p>Authorization</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>API-Key，例如：Bearer sk-xxxx</p></td><td><p>Bearer sk-xxxx</p></td></tr><tr><td><p>model</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>是</p></td><td><p>用于定制的基础模型名</p></td><td><p>facechain-finetune</p></td></tr><tr><td><p>training\_file\_ids</p></td><td><p>Array</p></td><td><p>Body</p></td><td><p>是</p></td><td><p>训练集文件列表，此处使用前述准备好的训练文件，支持url、file\_id及两者混合</p></td><td><p>\[ "<a href="https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg">[https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg](https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg)</a>","<a href="https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample2.jpg">[https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample2.jpg](https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample2.jpg)</a>","<a href="https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample3.jpg">[https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample3.jpg](https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample3.jpg)</a>"]</p></td></tr></tbody></table>

##### 出参描述 <span id="895952d773gro" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "23.760684%" }} /><col style={{ width: "12.820513%" }} /><col style={{ width: "35.384615%" }} /><col style={{ width: "28.034188%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的唯一标识符，用于问题排查和日志追踪</p></td><td><p>7574ee8f-38a3-4b1e-9280-11c33ab46e51</p></td></tr><tr><td><p>output.status</p></td><td><p>String</p></td><td><p>任务状态。可选值包括：</p> <ul><li><p>PENDING：任务排队中</p></li> <li><p>RUNNING：任务处理中</p></li> <li><p>SUCCEEDED：任务执行成功</p></li> <li><p>FAILED：任务执行失败</p></li> <li><p>CANCELED：任务取消成功</p></li> <li><p>UNKNOWN：任务不存在或状态未知</p></li></ul></td><td><p>PENDING</p></td></tr><tr><td><p>output.job\_id</p></td><td><p>String</p></td><td><p>系统生成的唯一任务 ID，用于查询该定制训练任务</p></td><td><p>ft-202509020951-f8bf</p></td></tr><tr><td><p>output.job\_name</p></td><td><p>String</p></td><td><p>任务名称，通常与任务 ID 一致</p></td><td><p>ft-202509020951-f8bf</p></td></tr><tr><td><p>output.finetuned\_output</p></td><td><p>String</p></td><td><p>定制完成后生成的模型名称，后续推理调用需使用此模型</p></td><td><p>facechain-finetune-ft-202509020951-f8bf</p></td></tr><tr><td><p>output.model</p></td><td><p>String</p></td><td><p>定制任务使用的模型标识</p></td><td><p>facechain-finetune</p></td></tr><tr><td><p>output.base\_model</p></td><td><p>String</p></td><td><p>定制任务使用的基准模型，即预训练模型</p></td><td><p>facechain-finetune</p></td></tr><tr><td><p>output.training\_file\_ids</p></td><td><p>Array</p></td><td><p>用于模型训练的输入文件 URL 列表（支持多个）</p></td><td><p>\[ "<a href="https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg">[https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg](https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg)</a>"]</p></td></tr><tr><td><p>output.validation\_file\_ids</p></td><td><p>Array</p></td><td><p>用于模型验证的文件 URL 列表，可为空表示无验证集</p></td><td><p>\[]</p></td></tr><tr><td><p>output.hyper\_parameters</p></td><td><p>Object</p></td><td><p>训练过程中使用的超参数配置，默认返回为空</p></td><td><p>\{}</p></td></tr><tr><td><p>output.training\_type</p></td><td><p>String</p></td><td><p>定制模型的训练方式，当前固定为：sft</p></td><td><p>sft</p></td></tr><tr><td><p>output.create\_time</p></td><td><p>String</p></td><td><p>任务创建时间，格式为</p> <p>YYYY-MM-DD HH:mm:ss</p></td><td><p>2025-09-02 09:51:02</p></td></tr><tr><td><p>output.workspace\_id</p></td><td><p>String</p></td><td><p>调用 API 所使用的 API-Key 所属的业务空间ID</p></td><td><p>llm-dmt509ikxxxxxx</p></td></tr><tr><td><p>output.user\_identity</p></td><td><p>String</p></td><td><p>调用 API 的账号唯一用户标识（UID）</p></td><td><p>12402258xxxxxx</p></td></tr><tr><td><p>output.modifier</p></td><td><p>String</p></td><td><p>最近一次修改该任务的用户 UID</p></td><td><p>12402258xxxxxx</p></td></tr><tr><td><p>output.creator</p></td><td><p>String</p></td><td><p>该训练任务的创建者用户 UID</p></td><td><p>12402258xxxxxx</p></td></tr><tr><td><p>output.group</p></td><td><p>String</p></td><td><p>任务所属的逻辑分组标签</p></td><td><p>facechain</p></td></tr><tr><td><p>output.model\_name</p></td><td><p>String</p></td><td><p>任务的模型名称标识，通常与任务 ID 一致</p></td><td><p>ft-202509020951-f8bf</p></td></tr><tr><td><p>output.max\_output\_cnt</p></td><td><p>Int</p></td><td><p>允许同时产出的最大定制模型数量</p></td><td><p>1</p></td></tr></tbody></table>

#### 请求示例 <span id="f4dfdfb173ke3" />

<Note>
  需要使用您的API-KEY替换示例中的\<YOUR-DASHSCOPE-API-KEY>，代码才能正常运行。
</Note>

```bash
curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/fine-tunes' \
--header 'Authorization: Bearer <YOUR-DASHSCOPE-API-KEY>' \
--header 'Content-Type: application/json' \
--data '{
    "model": "facechain-finetune",
    "training_file_ids": [
        "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg"
    ]
}'
```

#### 响应示例 <span id="f4dfdfb773btd" />

```json
{
    "request_id": "b6c9e77d-02a1-4a70-b127-xxxxxx",
    "output": {
        "job_id": "ft-202509020951-f8bf",
        "job_name": "ft-202509020951-f8bf",
        "status": "PENDING",
        "finetuned_output": "facechain-finetune-ft-202509020951-f8bf",
        "model": "facechain-finetune",
        "base_model": "facechain-finetune",
        "training_file_ids": [
            "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg"
        ],
        "validation_file_ids": [],
        "hyper_parameters": {},
        "training_type": "sft",
        "create_time": "2025-09-02 09:51:02",
        "workspace_id": "llm-dmt509ikxxxxxx",
        "user_identity": "12402258xxxxxx",
        "modifier": "124022586xxxxxx",
        "creator": "1240225868xxxxxx",
        "group": "facechain",
        "model_name": "ft-202509020951-f8bf",
        "max_output_cnt": 1
    }
}
```

### 步骤2：查询定制任务的状态 <span id="313917b073t5t" />

<Note>
  需要使用真实的job\_id替换示例中的\<job\_id>，代码才能正常运行。
</Note>

```
GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/fine-tunes/<job_id>
```

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

##### 功能描述 <span id="2a30b54373v6j" />

查询模型定制任务的状态，并在任务完成之后获取对应的任务结果。当训练任务成功之后，就可以使用对应的 finetuned\_output 内容做推理调用。

##### 入参描述 <span id="2a30b54573mvz" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.454545%" }} /><col style={{ width: "10.30303%" }} /><col style={{ width: "11.666667%" }} /><col style={{ width: "7.575758%" }} /><col style={{ width: "31.666667%" }} /><col style={{ width: "18.333333%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>Content-Type</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>请求类型：application/json</p></td><td><p>application/json</p></td></tr><tr><td><p>Authorization</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>API-Key，例如：Bearer sk-xxxx</p></td><td><p>Bearer sk-xxxx</p></td></tr><tr><td><p>job\_id</p></td><td><p>String</p></td><td><p>URL</p></td><td><p>是</p></td><td><p>需要查询的作业 id</p></td><td><p>ft-202509020951-f8bf</p></td></tr></tbody></table>

##### 出参描述 <span id="2a30dc7d735a9" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "23.760684%" }} /><col style={{ width: "12.820513%" }} /><col style={{ width: "35.384615%" }} /><col style={{ width: "28.034188%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的唯一标识符，用于问题排查和日志追踪</p></td><td><p>7574ee8f-38a3-4b1e-9280-11c33ab46e51</p></td></tr><tr><td><p>output.status</p></td><td><p>String</p></td><td><p>任务状态。可选值包括：</p> <ul><li><p>PENDING：任务排队中</p></li> <li><p>RUNNING：任务处理中</p></li> <li><p>SUCCEEDED：任务执行成功</p></li> <li><p>FAILED：任务执行失败</p></li> <li><p>CANCELED：任务取消成功</p></li> <li><p>UNKNOWN：任务不存在或状态未知</p></li></ul></td><td><p>PENDING</p></td></tr><tr><td><p>output.job\_id</p></td><td><p>String</p></td><td><p>系统生成的唯一任务 ID，用于查询该定制训练任务</p></td><td><p>ft-202509020951-f8bf</p></td></tr><tr><td><p>output.job\_name</p></td><td><p>String</p></td><td><p>任务名称，通常与任务 ID 一致</p></td><td><p>ft-202509020951-f8bf</p></td></tr><tr><td><p>output.finetuned\_output</p></td><td><p>String</p></td><td><p>定制完成后生成的模型名称，后续推理调用需使用此模型</p></td><td><p>facechain-finetune-ft-202509020951-f8bf</p></td></tr><tr><td><p>output.model</p></td><td><p>String</p></td><td><p>定制任务使用的模型标识</p></td><td><p>facechain-finetune</p></td></tr><tr><td><p>output.base\_model</p></td><td><p>String</p></td><td><p>定制任务使用的基准模型，即预训练模型</p></td><td><p>facechain-finetune</p></td></tr><tr><td><p>output.training\_file\_ids</p></td><td><p>Array</p></td><td><p>用于模型训练的输入文件 URL 列表（支持多个）</p></td><td><p>\[ "<a href="https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg">[https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg](https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg)</a>"]</p></td></tr><tr><td><p>output.validation\_file\_ids</p></td><td><p>Array</p></td><td><p>用于模型验证的文件 URL 列表，可为空表示无验证集</p></td><td><p>\[]</p></td></tr><tr><td><p>output.hyper\_parameters</p></td><td><p>Object</p></td><td><p>训练过程中使用的超参数配置，默认返回为空</p></td><td><p>\{}</p></td></tr><tr><td><p>output.training\_type</p></td><td><p>String</p></td><td><p>定制模型的训练方式，当前固定为：sft</p></td><td><p>sft</p></td></tr><tr><td><p>output.create\_time</p></td><td><p>String</p></td><td><p>任务创建时间，格式为</p> <p>YYYY-MM-DD HH:mm:ss</p></td><td><p>2025-09-02 09:51:02</p></td></tr><tr><td><p>output.workspace\_id</p></td><td><p>String</p></td><td><p>调用 API 所使用的 API-Key 所属的业务空间ID</p></td><td><p>llm-dmt509ikxxxxxx</p></td></tr><tr><td><p>output.user\_identity</p></td><td><p>String</p></td><td><p>调用 API 的账号唯一用户标识（UID）</p></td><td><p>12402258xxxxxx</p></td></tr><tr><td><p>output.modifier</p></td><td><p>String</p></td><td><p>最近一次修改该任务的用户 UID</p></td><td><p>12402258xxxxxx</p></td></tr><tr><td><p>output.creator</p></td><td><p>String</p></td><td><p>该训练任务的创建者用户 UID</p></td><td><p>12402258xxxxxx</p></td></tr><tr><td><p>output.group</p></td><td><p>String</p></td><td><p>任务所属的逻辑分组标签</p></td><td><p>facechain</p></td></tr><tr><td><p>output.usage</p></td><td><p>Int</p></td><td><p>训练次数，用于计量计费</p></td><td><p>1</p></td></tr><tr><td><p>output.end\_time</p></td><td><p>String</p></td><td><p>任务结束时间，格式为</p> <p>YYYY-MM-DD HH:mm:ss</p> <p>仅在任务完成（SUCCEEDED/FAILED）后返回</p></td><td><p>2025-09-02 09:56:04</p></td></tr><tr><td><p>output.model\_name</p></td><td><p>String</p></td><td><p>任务的模型名称标识，通常与任务 ID 一致</p></td><td><p>ft-202509020951-f8bf</p></td></tr><tr><td><p>output.max\_output\_cnt</p></td><td><p>Int</p></td><td><p>允许同时产出的最大定制模型数量</p></td><td><p>1</p></td></tr><tr><td><p>output.output\_cnt</p></td><td><p>Int</p></td><td><p>当前已产出的定制模型数量</p> <p>仅在任务完成后返回</p></td><td><p>1</p></td></tr></tbody></table>

#### 请求示例 <span id="2a30dcac73ps1" />

<Note>
  需要使用您的API-KEY替换示例中的\<YOUR-DASHSCOPE-API-KEY>，代码才能正常运行。
</Note>

```bash
curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/fine-tunes/ft-202509020951-xxxx' \
--header 'Authorization: Bearer <YOUR-DASHSCOPE-API-KEY>' \
--header 'Content-Type: application/json'
```

#### 响应示例 <span id="2a30dcb27315c" />

```json expandable
{
    "request_id": "fcfb99e6-7ae5-48b2-acca-xxxxxx",
    "output": {
        "job_id": "ft-202509020951-f8bf",
        "job_name": "ft-202509020951-f8bf",
        "status": "SUCCEEDED",
        "finetuned_output": "facechain-finetune-ft-202509020951-f8bf",
        "model": "facechain-finetune",
        "base_model": "facechain-finetune",
        "training_file_ids": [
            "https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg"
        ],
        "validation_file_ids": [],
        "hyper_parameters": {},
        "training_type": "sft",
        "create_time": "2025-09-02 09:51:02",
        "workspace_id": "llm-dmt509ikfxxxxxxx",
        "user_identity": "1240225868xxxxxx",
        "modifier": "1240225868xxxxxx",
        "creator": "1240225868xxxxxx",
        "end_time": "2025-09-02 09:56:04",
        "group": "facechain",
        "usage": 1,
        "model_name": "ft-202509020951-f8bf",
        "max_output_cnt": 1,
        "output_cnt": 1
    }
}
```

其他更多定制任务的列举、删除等能力，请参考[模型定制API详情](/zh/model-studio/model-training-api-reference)

## 状态码说明 <span id="9c31ff011bxae" />

大模型服务平台通用状态码请查阅：[错误码](/zh/model-studio/error-code)

同时本模型还有如下特定错误码：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "8.474576%" }} /><col style={{ width: "20.903955%" }} /><col style={{ width: "31.073446%" }} /><col style={{ width: "19.774011%" }} /><col style={{ width: "19.774011%" }} /></colgroup><thead><tr><th><p><strong>http状态码</strong></p></th><th><p><strong>错误码（code）</strong></p></th><th><p><strong>错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th><th><p><strong>处理方式</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>Missing training files.</p></td><td><p>参数错误，缺少参数或者参数格式问题等</p></td><td><p>根据错误信息，修正您的参数</p></td></tr><tr><td><p>400</p></td><td><p>UnsupportedOperation</p></td><td><p>The fine-tune job can not be deleted because it is succeeded,failed or canceled</p></td><td><p>当资源处于特定状态时，无法对其进行操作</p></td><td><p>待要操作的资源到达可操作状态时再进行操作</p></td></tr><tr><td><p>404</p></td><td><p>NotFound</p></td><td><p>job \{job\_id} not found.</p></td><td><p>要查询/操作的资源不存在</p></td><td><p>检查要查询/操作的资源id是否错误</p></td></tr><tr><td><p>409</p></td><td><p>Conflict</p></td><td><p>Model instance xxxxx already exists, please specify a suffix</p></td><td><p>已存在deployed\_model名为xxxxx的部署实例，需要指定后缀进行区分</p></td><td><p>为部署指定唯一的后缀</p></td></tr><tr><td><p>429</p></td><td><p>Throttling</p></td><td><p>Too many fine-tune job in running, please retry later</p></td><td><p>资源的创建触发平台限制</p></td><td><p>稍后再试</p></td></tr><tr><td><p>500</p></td><td><p>InternalError</p></td><td><p>Internal server error!</p></td><td><p>内部错误</p></td><td><p>记录reqeust\_id，通过工单联系阿里云</p></td></tr></tbody></table>

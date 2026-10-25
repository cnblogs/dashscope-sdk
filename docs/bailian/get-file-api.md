> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询和管理文件

> 查询、列举和删除文件，并了解文件对象的属性信息。

<meta property="og:title" content="查询和管理文件" />

<Tip>
  当前接口主要用于兼容历史场景，推荐优先使用 [OpenAI 兼容的 File 接口](/zh/model-studio/openai-file-interface)。
</Tip>

## 获取指定文件信息 <span id="066b66310ek68" />

```http
GET https://dashscope.aliyuncs.com/api/v1/files/{file_id}
Accept: application/json
```

### 请求示例 <span id="7aee84600eddz" />

```bash
curl --location --request GET "https://dashscope.aliyuncs.com/api/v1/files/123" \
--header "Authorization: Bearer ${DASHSCOPE_API_KEY}"
```

### 请求参数 <span id="5c0612700ewey" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.377622%" }} /><col style={{ width: "13.566434%" }} /><col style={{ width: "12.447552%" }} /><col style={{ width: "7.412587%" }} /><col style={{ width: "44.195804%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>file\_id</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>文件ID，可通过<a href="/zh/model-studio/upload-file-api">上传文件</a>或列举文件接口获取</p></td></tr></tbody></table>

### 返回示例 <span id="e06c1d700eqza" />

```json
{
    "request_id": "bddee90e-d320-4bf9-b32b-ac3359192d1e",
    "data": {
        "file_id": "mbSwIGR9yA6i1EtwIpndFErIoDKFyQ6W5601487ECAB111EDA20422ECF4959B5E",
        "name": "dogs.jpg",
        "description": "1",
        "size": 129862,
        "md5": "1d5ee55c2453009b14db98e74c453abb",
        "gmt_create": "2023-03-25 10:04:11",
        "url": "http://xxxxx.oss-cn-hangzhou.aliyuncs.com/oss%3A//dashscope-pre/api-fs/1253/236/dogs.jpg?Expires=1679797621&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE"
    }
}
```

### 返回参数 <span id="d498c5700e5t8" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "34.763314%" }} /><col style={{ width: "15.828402%" }} /><col style={{ width: "49.408284%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>data.file\_id</p></td><td><p>String</p></td><td><p>文件ID</p></td></tr><tr><td><p>data.url</p></td><td><p>String</p></td><td><p>文件下载链接</p></td></tr><tr><td><p>data.name</p></td><td><p>String</p></td><td><p>文件名</p></td></tr><tr><td><p>data.size</p></td><td><p>Number</p></td><td><p>文件大小</p></td></tr><tr><td><p>data.md5</p></td><td><p>String</p></td><td><p>文件的md5</p></td></tr><tr><td><p>data.description</p></td><td><p>String</p></td><td><p>文件的描述</p></td></tr><tr><td><p>data.gmt\_create</p></td><td><p>Date</p></td><td><p>文件上传时间</p></td></tr><tr><td><p>data.id</p></td><td><p>String</p></td><td><p>文件的内部ID</p></td></tr><tr><td><p>data.region</p></td><td><p>String</p></td><td><p>文件所在地域</p></td></tr><tr><td><p>data.user\_id</p></td><td><p>String</p></td><td><p>用户ID</p></td></tr><tr><td><p>data.api\_key\_id</p></td><td><p>String</p></td><td><p>API Key ID</p></td></tr><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统唯一码</p></td></tr></tbody></table>

## 列举当前所有的文件 <span id="a3b144d10ei2c" />

```http
GET https://dashscope.aliyuncs.com/api/v1/files
Accept: application/json
```

### 请求示例 <span id="719650700eyng" />

```bash
curl --location --request GET "https://dashscope.aliyuncs.com/api/v1/files?page_no=1&page_size=20" \
--header "Authorization: Bearer ${DASHSCOPE_API_KEY}"
```

### 请求参数 <span id="6677a2700etc3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.377622%" }} /><col style={{ width: "13.566434%" }} /><col style={{ width: "12.447552%" }} /><col style={{ width: "7.412587%" }} /><col style={{ width: "44.195804%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>page\_no</p></td><td><p>Number</p></td><td><p>Query</p></td><td><p>是</p></td><td><p>当前页，最小值为1，默认值为1</p></td></tr><tr><td><p>page\_size</p></td><td><p>Number</p></td><td><p>Query</p></td><td><p>是</p></td><td><p>分页大小。最小值为1，最大值为100，默认值为10</p></td></tr></tbody></table>

### 返回示例 <span id="128113d00e4fg" />

```json expandable
{
    "request_id": "123456",
    "data": {
        "total": 2,
        "page_size": 20,
        "page_no": 1,
        "files": [
            {
                "id": 1001,
                "file_id": "9G2EaQtq7p1fw7oRhYXdHTtDFYAMVQSh95432B38CAB211EDB8F952C2E8001733",
                "name": "test.txt",
                "description": "1",
                "size": 0,
                "md5": "d41d8cd98f00b204e9800998ecf8427e",
                "gmt_create": "2023-03-25 10:13:07",
                "url": "http://xxx.oss-cn-hangzhou.aliyuncs.com/oss%3A//dashscope-dev/api-fs/123456/123456/test.txt",
                "region": "cn-hangzhou",
                "user_id": "123456",
                "api_key_id": "sk-xxxx",
                "purpose": "fine-tune"
            },
            {
                "id": 1002,
                "file_id": "wmIDj6zemqjIb8L2o8NHIlMRk3QinjGP00E987C7CA3711ED83E0000EC63B0D1C",
                "name": "sdsdsd.mp4",
                "description": "1",
                "size": 780635,
                "md5": "8382b3d3137bce6eaf1beef9c8920ef6",
                "gmt_create": "2023-03-24 19:28:30",
                "url": "http://xxxx.oss-cn-hangzhou.aliyuncs.com/oss%3A//dashscope-dev/api-fs/123456/123456/sdsdsd.mp4",
                "region": "cn-hangzhou",
                "user_id": "123456",
                "api_key_id": "sk-xxxx",
                "purpose": "file-extract"
            }
        ]
    }
}
```

### 返回参数 <span id="f85518800em7y" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "34.763314%" }} /><col style={{ width: "15.828402%" }} /><col style={{ width: "49.408284%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>data.total</p></td><td><p>Number</p></td><td><p>总记录数</p></td></tr><tr><td><p>data.page\_size</p></td><td><p>Number</p></td><td><p>分页大小</p></td></tr><tr><td><p>data.page\_no</p></td><td><p>Number</p></td><td><p>当前页</p></td></tr><tr><td><p>data.files</p></td><td><p>Array</p></td><td><p>文件列表，元素为文件对象，属性详见文件对象小节</p></td></tr><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统唯一码</p></td></tr></tbody></table>

## 删除文件 <span id="436c33610en47" />

```http
DELETE https://dashscope.aliyuncs.com/api/v1/files/{file_id}
Accept: application/json
```

### 请求示例 <span id="9bc256200ez7n" />

```bash
curl --location --request DELETE "https://dashscope.aliyuncs.com/api/v1/files/123" \
--header "Authorization: Bearer ${DASHSCOPE_API_KEY}"
```

### 请求参数 <span id="86ba1ec00eiha" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.377622%" }} /><col style={{ width: "13.566434%" }} /><col style={{ width: "12.447552%" }} /><col style={{ width: "7.412587%" }} /><col style={{ width: "44.195804%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>file\_id</p></td><td><p>String</p></td><td><p>path</p></td><td><p>是</p></td><td><p>文件ID，可通过<a href="/zh/model-studio/upload-file-api">上传文件</a>或列举文件接口获取</p></td></tr></tbody></table>

### 返回示例 <span id="2fbe8d800e5qa" />

```json
{
    "request_id": "038e9953-6f0e-4691-afcc-1f0fe07b32c2"
}
```

### 返回参数 <span id="d5f441500eih1" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "34.763314%" }} /><col style={{ width: "15.828402%" }} /><col style={{ width: "49.408284%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>code</p></td><td><p>String</p></td><td><p>错误码，仅当本次请求出错时返回</p></td></tr><tr><td><p>message</p></td><td><p>String</p></td><td><p>错误信息，仅当本次请求出错时返回</p></td></tr><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统唯一码</p></td></tr></tbody></table>

## 文件对象 <span id="v0tcaa5eft1qc" />

文件对象表示已上传到百炼平台的文件及其属性信息。通过[上传文件](/zh/model-studio/upload-file-api)接口创建，可通过列举文件和查询文件详情接口获取。

除 DashScope 原生接口外，您也可以通过 [OpenAI 兼容-File 接口](/zh/model-studio/openai-file-interface)（`/compatible-mode/v1/files`）管理文件。

> 当前文件管理 API 仅在北京 Region 开放。如您使用其他 Region，请通过该 Region 的百炼控制台完成文件管理操作。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "34.763314%" }} /><col style={{ width: "15.828402%" }} /><col style={{ width: "49.408284%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>file\_id</p></td><td><p>String</p></td><td><p>文件ID</p></td></tr><tr><td><p>name</p></td><td><p>String</p></td><td><p>文件名</p></td></tr><tr><td><p>description</p></td><td><p>String</p></td><td><p>文件的描述</p></td></tr><tr><td><p>size</p></td><td><p>Number</p></td><td><p>文件大小</p></td></tr><tr><td><p>md5</p></td><td><p>String</p></td><td><p>文件的md5</p></td></tr><tr><td><p>url</p></td><td><p>String</p></td><td><p>文件下载链接</p></td></tr><tr><td><p>gmt\_create</p></td><td><p>Date</p></td><td><p>文件上传时间</p></td></tr><tr><td><p>id</p></td><td><p>String</p></td><td><p>文件的内部ID</p></td></tr><tr><td><p>region</p></td><td><p>String</p></td><td><p>文件所在地域</p></td></tr><tr><td><p>user\_id</p></td><td><p>String</p></td><td><p>用户ID</p></td></tr><tr><td><p>api\_key\_id</p></td><td><p>String</p></td><td><p>API Key ID</p></td></tr><tr><td><p>purpose</p></td><td><p>String</p></td><td><p>文件用途</p></td></tr></tbody></table>

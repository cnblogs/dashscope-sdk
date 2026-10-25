> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 数字人wan2.2-s2v图像检测API参考

> 数字人wan2.2-s2v-detect 模型，用于检测输入图片是否符合 wan2.2-s2v 模型的输入规范。

<meta property="og:title" content="数字人wan2.2-s2v图像检测API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 模型与价格 <span id="f1c3d9948b4tg" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.752101%" }} /><col style={{ width: "15.546218%" }} /><col style={{ width: "20.063025%" }} /><col style={{ width: "18.592437%" }} /><col style={{ width: "28.046218%" }} /></colgroup><thead><tr><th rowSpan={2}><p><strong>模型名称</strong></p></th><th rowSpan={2}><p><strong>计费单价</strong></p></th><th colSpan={2}><p><strong>限流（主账号与RAM子账号共用）</strong></p></th><th rowSpan={2}><p><strong>免费额度</strong><a href="/zh/model-studio/new-free-quota">（查看）</a></p></th></tr><tr><th><p><strong>任务下发接口RPS限制</strong></p></th><th><p><strong>同时处理中任务数量</strong></p></th></tr></thead><tbody><tr><td><p>wan2.2-s2v-detect</p></td><td><p>0.004元/张</p></td><td><p>5</p></td><td><p>同步接口无限制</p></td><td><p>200张</p></td></tr></tbody></table>

- <strong>计费项</strong>：无论检测是否通过，只要请求成功就计费；请求失败不计费。

  > 当图像因不合规而导致检测不通过时，本次API调用仍会正常计费，因为模型已经执行了完整的检测流程。
- <strong>抵扣顺序</strong>：优先消耗免费额度。额度用尽后，默认转为按量付费。您可开启“免费额度用完即停”功能，以避免免费额度耗尽后产生额外费用。详情请参见[免费额度](/zh/model-studio/new-free-quota)。

## HTTP调用接口 <span id="2f67df3cf92ii" />

该模型用于检测输入的图片是否满足“wan2.2-s2v”所需的人物肖像图片规范。

### 前提条件 <span id="4325f8ce6amyc" />

- 已开通服务并获得API-KEY：[获取与配置 API Key](/zh/model-studio/get-api-key)。
- 配置环境变量：[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

### 图像检测接口 <span id="31d1727a4ejzu" />

```http
POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/face-detect
```

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

#### 入参描述 <span id="9ee77439ecy5y" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.604575%" }} /><col style={{ width: "10.294118%" }} /><col style={{ width: "9.803922%" }} /><col style={{ width: "6.944444%" }} /><col style={{ width: "31.699346%" }} /><col style={{ width: "25.653595%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>Content-Type</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>请求类型：application/json。</p></td><td><p>application/json</p></td></tr><tr><td><p>Authorization</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>API-Key，格式为 Bearer sk-xxx。</p></td><td><p>Bearer sk-1a\*\*2b</p></td></tr><tr><td><p>model</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>是</p></td><td><p>指定调用的模型名称。</p></td><td><p>wan2.2-s2v-detect</p></td></tr><tr><td><p>input.image\_url</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>是</p></td><td><p>待检测的图像 URL。</p><ul><li><p>图像格式：支持jpg，jpeg，png，bmp，webp。</p></li><li><p>图像分辨率：图像的宽度和高度范围为\[400, 7000]像素。</p></li><li><p>上传图片仅支持公网可访问的 HTTP/HTTPS 链接。<span>本地文件可通过<a href="/zh/model-studio/get-temporary-file-url">上传文件获取临时URL</a>。</span></p></li></ul></td><td><p>http\://aaa/bbb.jpg</p></td></tr></tbody></table>

#### 出参描述 <span id="5cb0f3560c2xb" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "23.98%" }} />

    <col style={{ width: "10.87%" }} />

    <col style={{ width: "38.63%" }} />

    <col style={{ width: "26.52%" }} />
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
        output.check\_pass
      </td>

      <td>
        Bool
      </td>

      <td>
        检测结果。true 表示通过，false 表示不通过。
      </td>

      <td>
        true
      </td>
    </tr>

    <tr>
      <td>
        output.humanoid
      </td>

      <td>
        Bool
      </td>

      <td>
        是否检测到人像：true 表示是，false 表示否。
      </td>

      <td>
        true
      </td>
    </tr>

    <tr>
      <td>
        usage.image\_count
      </td>

      <td>
        Integer
      </td>

      <td>
        本次请求检测图像数量，固定为 1 张，用于计费。无论检测是否通过，只要请求成功就计费；请求失败不计费。

        > 当图像因不合规而导致检测不通过时，本次API调用仍会正常计费，因为模型已经执行了完整的检测流程。
      </td>

      <td>
        1
      </td>
    </tr>

    <tr>
      <td>
        request\_id
      </td>

      <td>
        String
      </td>

      <td>
        本次请求的唯一ID。
      </td>

      <td>
        7574ee8f-38a3-4b1e-9280-11c33ab46e51
      </td>
    </tr>
  </tbody>
</table>

#### 请求示例 <span id="a4441af7ade5m" />

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

```bash
curl 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/face-detect' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY" \
--header 'Content-Type: application/json' \
--data-raw '{
    "model": "wan2.2-s2v-detect",
    "input": {
        "image_url": "https://img.alicdn.com/imgextra/i3/O1CN011FObkp1T7Ttowoq4F_!!6000000002335-0-tps-1440-1797.jpg"
        }
    }'
```

#### 响应示例（通过） <span id="c1a5d31e34jqm" />

```json
{
    "output":{
        "check_pass": true,
        "humanoid": true
    },
    "usage":{
        "image_count":1
    },
    "request_id":"c56f62df-724e-9c19-96bd-xxxxxx"
}
```

#### 响应示例（不通过） <span id="800bb54a9folx" />

```json
{
    "output":{
        "check_pass": false,
        "code": "",
        "message": ""
    },
    "usage":{
        "image_count":1
    },
    "request_id":"c56f62df-724e-9c19-96bd-xxxxxx"
}
```

## 错误码 <span id="0d0a562d05nkh" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

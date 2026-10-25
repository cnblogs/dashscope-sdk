> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 数字人wan2.2-s2v视频生成API参考

> 数字人wan2.2-s2v模型能基于 单张图片和音频 ，生成动作自然的说话、唱歌或表演视频。

<meta property="og:title" content="数字人wan2.2-s2v视频生成API参考" />

- <strong>音频驱动</strong>: 通过输入的人声音频，驱动静态图片中的人物实现口型、表情和动作与音频同步。
- <strong>场景丰富</strong>: 支持 "说话"、"唱歌" 、“表演”三种对口型场景
- <strong>人物形象多样化</strong>：支持真人（肖像、半身、全身）及卡通人物。
- <strong>输出视频分辨率</strong>：提供480P、720P两档分辨率选项。

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 模型与价格 <span id="f1c3d9948b4tg" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "17.770768%" }} /><col style={{ width: "15.562566%" }} /><col style={{ width: "20.820189%" }} /><col style={{ width: "19.663512%" }} /><col style={{ width: "26.182965%" }} /></colgroup><thead><tr><th rowSpan={2}><p><strong>模型名称</strong></p></th><th rowSpan={2}><p><strong>计费单价</strong></p></th><th colSpan={2}><p><strong>限流（主账号与RAM子账号共用）</strong></p></th><th rowSpan={2}><p><strong>免费额度</strong><a href="/zh/model-studio/new-free-quota">（查看）</a></p></th></tr><tr><th><p><strong>任务下发接口RPS限制</strong></p></th><th><p><strong>同时处理中任务数量</strong></p></th></tr></thead><tbody><tr><td><p>wan2.2-s2v</p></td><td><p>480P：0.5元/秒</p> <p>720P：0.9元/秒</p></td><td><p>5</p></td><td><p>1</p></td><td><p>100秒</p></td></tr></tbody></table>

## HTTP调用接口 <span id="2f67df3cf92ii" />

### 前提条件 <span id="4325f8ce6amyc" />

- 已开通服务并获得API-KEY：[获取与配置 API Key](/zh/model-studio/get-api-key)。
- 配置环境变量：[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

### 步骤1：创建任务获取任务ID <span id="31d1727a4ejzu" />

```http
POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis
```

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Note>
  - 因该模型调用耗时较长，故采用异步调用的方式创建任务。
  - 任务创建后，系统会立即返回一个 `task_id`。在下一步中，您需要使用此 task\_id 在<strong>24小时内</strong>查询任务结果。
</Note>

#### 入参描述 <span id="9ee77439ecy5y" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "15.378422%" }} />

    <col style={{ width: "10.144928%" }} />

    <col style={{ width: "9.661836%" }} />

    <col style={{ width: "6.8438%" }} />

    <col style={{ width: "32.689211%" }} />

    <col style={{ width: "25.281804%" }} />
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
        <strong>传参方式</strong>
      </th>

      <th>
        <strong>必选</strong>
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
        Content-Type
      </td>

      <td>
        String
      </td>

      <td>
        Header
      </td>

      <td>
        是
      </td>

      <td>
        请求类型：application/json。
      </td>

      <td>
        application/json
      </td>
    </tr>

    <tr>
      <td>
        Authorization
      </td>

      <td>
        String
      </td>

      <td>
        Header
      </td>

      <td>
        是
      </td>

      <td>
        API-Key，格式为 Bearer sk-xxx。
      </td>

      <td>
        Bearer sk-1a\*\*2b
      </td>
    </tr>

    <tr>
      <td>
        X-DashScope-Async
      </td>

      <td>
        String
      </td>

      <td>
        Header
      </td>

      <td>
        是
      </td>

      <td>
        固定值为 enable，表示使用异步调用方式。
      </td>

      <td>
        enable
      </td>
    </tr>

    <tr>
      <td>
        model
      </td>

      <td>
        String
      </td>

      <td>
        Body
      </td>

      <td>
        是
      </td>

      <td>
        指明需要调用的模型。
      </td>

      <td>
        wan2.2-s2v
      </td>
    </tr>

    <tr>
      <td>
        input.image\_url
      </td>

      <td>
        String
      </td>

      <td>
        Body
      </td>

      <td>
        是
      </td>

      <td>
        上传的图片 URL。

        - 图像格式：支持jpg，jpeg，png，bmp，webp。
        - 图像分辨率：图像的宽度和高度范围为\[400, 7000]像素。
        - 上传图片仅支持公网可访问的 HTTP/HTTPS 链接。本地文件可通过[上传文件获取临时URL](/zh/model-studio/get-temporary-file-url)。
      </td>

      <td>
        [http://aaa/bbb.jpg](http://aaa/bbb.jpg)
      </td>
    </tr>

    <tr>
      <td>
        input.audio\_url
      </td>

      <td>
        String
      </td>

      <td>
        Body
      </td>

      <td>
        是
      </td>

      <td>
        上传的音频文件 URL。

        - 音频格式：格式为wav、mp3。
        - 音频限制：文件\<15M，时长＜20s。
        - 音频内容：音频中需包含清晰、响亮的人声语音，并去除了环境噪音、背景音乐等声音干扰信息。
        - 上传音频仅支持公网可访问的 HTTP/HTTPS 链接。本地文件可通过[上传文件获取临时URL](/zh/model-studio/get-temporary-file-url)。
      </td>

      <td>
        [http://aaa/bbb.mp3](http://aaa/bbb.mp3)
      </td>
    </tr>

    <tr>
      <td>
        parameters.resolution
      </td>

      <td>
        String
      </td>

      <td>
        Body
      </td>

      <td>
        否
      </td>

      <td>
        视频分辨率档位。

        可选值为480P、720P。默认值为480P。

        模型会尽量保持输出视频与输入图像的宽高比一致，在宽高比不变的基础上，将视频总像素调整到所选档位附近。

        <Accordion title="示例">
          480P：视频分辨率通常指 640×480（约 31万像素），视频宽高比为4:3。

          720P：视频分辨率通常指 1280×720（约 92万像素），视频宽高比为16:9。

          <strong>示例</strong>：若输入图像的宽高比例为 4:5，且选择 480P 档位，则输出视频的宽高比会保持4:5，分辨率调整为接近 31万像素。例如，输出视频的分辨率为 480×600，总像素 28.8万（此数据仅做参考，以实际输出为准）。
        </Accordion>
      </td>

      <td>
        480P
      </td>
    </tr>
  </tbody>
</table>

<Note>
  生成视频的时长与输入音频的时长一致，因此单次生成的视频时长同样受音频时长限制（须小于 20 秒）；若输入音频超过 20 秒，会导致任务报错。如需生成更长的视频，可将长音频按小于 20 秒切分为多段，分别调用本接口生成对应的视频片段，再使用剪辑工具将多段视频拼接合并为一个完整的长视频。
</Note>

#### 出参描述 <span id="5cb0f3560c2xb" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24%" }} /><col style={{ width: "10.88%" }} /><col style={{ width: "38.63%" }} /><col style={{ width: "26.49%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>output.task\_id</p></td><td><p>String</p></td><td><p>异步任务的唯一ID。</p></td><td><p>a8532587-fa8c-4ef8-82be-0c46b17950d1</p></td></tr><tr><td><p>output.task\_status</p></td><td><p>String</p></td><td><p>提交异步任务后的 作业状态。</p></td><td><p>PENDING</p></td></tr><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的唯一ID。</p></td><td><p>7574ee8f-38a3-4b1e-9280-11c33ab46e51</p></td></tr></tbody></table>

#### 请求示例 <span id="a4441af7ade5m" />

调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

<Tabs>
  <Tab title="curl">
    ```bash
    curl 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis' \
     --header 'X-DashScope-Async: enable' \
     --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
     --header 'Content-Type: application/json' \
     --data '{
         "model": "wan2.2-s2v",
         "input": {
                "image_url": "https://img.alicdn.com/imgextra/i3/O1CN011FObkp1T7Ttowoq4F_!!6000000002335-0-tps-1440-1797.jpg",
                "audio_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250825/iaqpio/input_audio.MP3"
            },
            "parameters": {
                "resolution": "480P"
            }
        }'
    ```
  </Tab>

  <Tab title="Python SDK">
    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_async_call():
        # 异步提交视频生成任务
        rsp = VideoSynthesis.async_call(api_key=api_key,
                                    model='wan2.2-s2v',
                                    img_url='https://img.alicdn.com/imgextra/i3/O1CN011FObkp1T7Ttowoq4F_!!6000000002335-0-tps-1440-1797.jpg',
                                    audio_url='https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250825/iaqpio/input_audio.MP3',
                                    resolution='480P')
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print("task_id: %s" % rsp.output.task_id)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

        # 查询任务状态
        status = VideoSynthesis.fetch(task=rsp, api_key=api_key)
        if status.status_code == HTTPStatus.OK:
            print(status.output.task_status)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (status.status_code, status.code, status.message))

        # 等待任务完成
        rsp = VideoSynthesis.wait(task=rsp, api_key=api_key)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_async_call()
    ```
  </Tab>
</Tabs>

#### 响应示例 <span id="c1a5d31e34jqm" />

```json
{
    "output": {
        "task_id": "a8532587-fa8c-4ef8-82be-xxxxxx",
        "task_status": "PENDING"
    }
    "request_id": "7574ee8f-38a3-4b1e-9280-xxxxxx"
}
```

### 步骤2：根据任务ID查询结果 <span id="c89331030e0y3" />

使用上一步获取的 `task_id`，发送 GET 请求查询任务状态和结果。请将 URL 中的`{task_id}` 替换为您的实际任务ID，并将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

```http
GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}
```

<Note>
  - 视频生成任务耗时较长（约5-10分钟），建议采用轮询机制，并设置合理的查询间隔（如 15秒）来获取结果。
  - 任务成功后返回的 `video_url`有效期为<strong>24小时</strong>，请及时下载并保存视频。
  - 此查询接口的默认QPS为20。如需更高频次的查询或事件通知，请[配置异步任务回调](/zh/model-studio/async-task-api)。
  - 如需批量查询或取消任务，请参见[管理异步任务](/zh/model-studio/manage-asynchronous-tasks)。
</Note>

#### 入参描述 <span id="3fe6afeb79bju" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "13.88%" }} /><col style={{ width: "9.82%" }} /><col style={{ width: "10.24%" }} /><col style={{ width: "7.01%" }} /><col style={{ width: "29.45%" }} /><col style={{ width: "29.6%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>Authorization</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>API-Key，例如：Bearer sk-xxx。</p></td><td><p>Bearer sk-xxx</p></td></tr><tr><td><p>task\_id</p></td><td><p>String</p></td><td><p>Url Path</p></td><td><p>是</p></td><td><p>需要查询任务的ID。</p></td><td><p>a8532587-fa8c-4ef8-82be-0c46b17950d1</p></td></tr></tbody></table>

#### 出参描述 <span id="1fe854c50ckzz" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "22.02%" }} /><col style={{ width: "11.08%" }} /><col style={{ width: "35.34%" }} /><col style={{ width: "31.56%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>output.task\_id</p></td><td><p>String</p></td><td><p>查询的任务ID。</p></td><td><p>a8532587-fa8c-4ef8-82be-0c46b17950d1</p></td></tr><tr><td><p>output.task\_status</p></td><td><p>String</p></td><td><p>任务状态。可能的值包括：</p> <ul><li><p>PENDING 排队中</p></li> <li><p>RUNNING 处理中</p></li> <li><p>SUCCEEDED 成功</p></li> <li><p>FAILED 失败</p></li> <li><p>UNKNOWN 作业不存在或状态未知</p></li> <li><p>CANCELED：任务取消成功</p></li></ul></td><td><p>SUCCEEDED</p></td></tr><tr><td><p>output.submit\_time</p></td><td><p>String</p></td><td><p>任务提交时间。</p></td><td><p>2025-09-01 09:37:27.468</p></td></tr><tr><td><p>output.scheduled\_time</p></td><td><p>String</p></td><td><p>任务执行时间。</p></td><td><p>2025-09-01 09:37:34.885</p></td></tr><tr><td><p>output.end\_time</p></td><td><p>String</p></td><td><p>任务完成时间。</p></td><td><p>2025-09-01 09:40:20.734</p></td></tr><tr><td><p>output.results.video\_url</p></td><td><p>String</p></td><td><p>生成的视频文件。</p> <p><strong>video\_url有效期为24小时</strong>，请及时下载。</p></td><td><p>https\://xxx/1.mp4?Expires=xxx</p></td></tr><tr><td><p>usage.duration</p></td><td><p>Float</p></td><td><p>视频时长（秒），用于计费，按秒计费。</p></td><td><p>10.23</p></td></tr><tr><td><p>usage.video\_count</p></td><td><p>Integer</p></td><td><p>生成视频的数量。</p></td><td><p>1</p></td></tr><tr><td><p>usage.SR</p></td><td><p>Integer</p></td><td><p>生成视频分辨率档位。</p></td><td><p>480</p></td></tr><tr><td><p>usage.size</p></td><td><p>String</p></td><td><p>本次请求生成视频的分辨率</p></td><td><p>"size": "640\*480"</p></td></tr><tr><td><p>usage.fps</p></td><td><p>Integer</p></td><td><p>本次请求生成视频的帧率</p></td><td><p>"fps": 16</p></td></tr><tr><td><p>output.code</p></td><td><p>String</p></td><td><p>错误码。任务失败时返回此参数。</p></td><td><p>InvalidParameter</p></td></tr><tr><td><p>output.message</p></td><td><p>String</p></td><td><p>错误详情。任务失败时返回此参数。</p></td><td><p>The request is missing required parameters or in a wrong format</p></td></tr><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的唯一ID。</p></td><td><p>7574ee8f-38a3-4b1e-9280-11c33ab46e51</p></td></tr></tbody></table>

#### 请求示例 <span id="e0200c87dblj3" />

将`86ecf553-d340-4e21-xxxxxxxxx`替换为真实的task\_id，将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

```bash
curl -X GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/86ecf553-d340-4e21-xxxxxxxxx \
--header "Authorization: Bearer $DASHSCOPE_API_KEY"
```

<Note>
  task\_id 仅支持在<strong>24小时内</strong>查询任务结果，超时会被系统自动清除。
</Note>

#### 响应示例 <span id="53400368e4wlg" />

<Tabs>
  <Tab title="成功响应">
    任务数据（如任务状态、视频URL等）仅保留24小时，超时后自动清除，请及时保存结果。

    ```json
    {
        "output": {
            "task_id": "bcae8761-f242-4775-a11e-xxxxxx",
            "task_status": "SUCCEEDED",
            "submit_time": "2025-09-01 09:37:27.468",
            "scheduled_time": "2025-09-01 09:37:34.885",
            "end_time": "2025-09-01 09:40:20.734",
            "results": {
                "video_url": "http://dashscope-result-hz.oss-cn-hangzhou.aliyuncs.com/1d/xxx.mp4?Expires=xxxxxx"
            }
        },
        "usage": {
            "duration": 18.13,
            "size": "512*640",
            "fps": 16,
            "video_count": 1,
            "SR": 480
        },
        "request_id": "28cfedb1-cd60-9e0c-b920-xxxxxx"
    }
    ```
  </Tab>

  <Tab title="失败响应">
    ```json height="231"
    {
        "request_id": "8d49f522-f6a4-9eed-b322-xxxxxx",
        "output": {
            "task_id": "101ad32f-7653-4ae9-8f22-xxxxxx",
            "task_status": "FAILED",
            "submit_time": "2025-09-01 11:43:41.174",
            "scheduled_time": "2025-09-01 11:43:48.937",
            "end_time": "2025-09-01 11:43:49.802",
            "code": "InvalidURL",
            "message": "Required URL is missing or invalid, please check the request URL."
        }
    }
    ```
  </Tab>
</Tabs>

## 计费与限流 <span id="ce68f54f7bbqe" />

- 模型免费额度和计费单价请参见[万相-数字人](/zh/model-studio/model-pricing#6fa00e4db0qlz)。
- 模型限流请参见[万相系列](/zh/model-studio/rate-limit)。
- 计费说明：

  - 输入不计费，输出计费。按成功生成的 视频秒数 计费。
  - 模型调用失败或处理错误不产生任何费用，也不消耗[新人免费额度](/zh/model-studio/new-free-quota)。

## 错误码 <span id="0d0a562d05nkh" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-数字人

> 数字人wan2.2-s2v模型支持基于 单张图片和音频 ，生成动作自然的说话、唱歌或表演视频，不限制形象画幅，支持 肖像、全身或半身 的人物图像。

<meta property="og:title" content="万相-数字人" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 模型概览 <span id="8ed6166014a83" /> <span id="a2429cf3ebvbq" />

##### 效果示例 <span id="72ea9ffa162uw" /> <span id="5f20476c46vtj" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50.07%" }} />

    <col style={{ width: "49.93%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>输入示例</strong>
      </th>

      <th>
        <strong>输出视频</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8144216571/p1001125.jpeg" alt="input_image" />

        <strong>输入音频</strong>

        <audio controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250825/iaqpio/input_audio.MP3" type="audio/mpeg" />
        </audio>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250825/qvtpxw/output_video.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>
  </tbody>
</table>

生成数字人视频的流程为：

- <strong>步骤1</strong>：调用 wan2.2-s2v-detect 接口，传入图片URL，确认图片合规。
- <strong>步骤2</strong>：若检测通过，调用 wan2.2-s2v 异步接口，传入图片URL和音频URL，提交视频生成任务，并轮询获取结果。

## 快速开始 <span id="e5bdbcf4deej1" /> <span id="81171c677cvwd" />

#### 前提条件 <span id="d3e92944e3t55" /> <span id="8b74b54246ib7" />

在调用前，您需要[开通模型服务并获取API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/get-api-key)。

#### 示例代码 <span id="fa053a9e29u3k" /> <span id="9a919c58acvrt" />

本文的示例图片已通过图像检测，以下展示视频生成的示例代码。

<Note>
  HTTP 请求分两步：先创建任务，再获取结果。初学者建议使用 [Postman](/zh/model-studio/first-call-to-image-and-video-api)调用API。
</Note>

##### 步骤1：创建任务获取任务ID <span id="a4ac549258mtq" /> <span id="752f63a499kjw" />

该请求会返回一个`task_id`用于查询结果。

```bash
curl 'https://dashscope.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis' \
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
            "style": "speech"
        }
    }'
```

##### 步骤2：根据任务ID查询结果 <span id="26c6e917d9j74" /> <span id="adaad8ba0flc1" />

请将`86ecf553-d340-4e21-xxxxxxxxx`替换为真实的task\_id。

> 若使用新加坡地域的模型，需将base\_url替换为https\://\{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/tasks/86ecf553-d340-4e21-xxxxxxxxx，其中\{WorkspaceId}需替换为真实的业务空间ID。

```bash
curl -X GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/86ecf553-d340-4e21-xxxxxxxxx \
--header "Authorization: Bearer $DASHSCOPE_API_KEY"
```

`task_id`查询有效期为24小时，过期将无法查询，接口将返回任务状态为`UNKNOWN`。

## 下一步 <span id="8129d25e7067o" /> <span id="2849fa3c9aebl" />

根据您的具体需求，查阅API文档开始您的开发工作：

[图像检测API](/zh/model-studio/wan-s2v-detect-api)

[视频生成API](/zh/model-studio/wan-s2v-api)

> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# VideoRetalk 视频生成 API参考

> 声动人像VideoRetalk是一个人物视频生成模型，可基于人物视频和人声音频，生成人物讲话口型与输入音频相匹配的新视频。本文档介绍了该模型提供的视频生成能力的API调用方法。

<meta property="og:title" content="VideoRetalk 视频生成 API参考" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## HTTP调用接口 <span id="a36070fcff2bv" />

## VideoRetalk视频生成API仅支持通过HTTP进行调用。为了减少等待时间并且避免请求超时，采用了异步处理方法。因此，您需要发起两个请求来生成视频。 <span id="b188c19ebb602" />

- [作业提交接口](/zh/model-studio/videoretalk-api#99d4e18b3e9hc)：您需要先发送一个请求来创建视频生成任务。任务提交后，将返回该任务ID。
- [作业任务状态查询和结果获取接口](/zh/model-studio/videoretalk-api#c89331030e0y3)：使用上一步获得的任务ID，再发送一个请求，获取任务状态及生成的视频。

### 前提条件 <span id="31548f9be6np9" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

### 输入限制 <span id="453159b867r4g" />

- 视频要求：

  - 文件限制：支持mp4、avi、mov，文件≤300MB，2秒＜时长＜120秒。
  - 视频限制：15fps≤帧率≤60fps；编码采用H.264或H.265；边长不低于640，不大于2048。
  - 视频内容：视频应为人物正面出镜的近景画面。避免大角度侧脸或人脸过小。
- 音频要求：

  - 文件限制：支持wav、mp3、aac，文件≤30MB，2秒＜时长＜120秒。
  - 音频内容：音频中需包含清晰、响亮的人声语音，并去除了环境噪音、背景音乐等声音干扰信息。
- 人物参考图要求：

  - 文件限制：支持jpeg、jpg、png、bmp、webp，文件≤10MB。
  - 图片内容：需包含一张清晰的人物正脸，且该人物出现在视频画面中。也可从视频画面中截图用作人物参考图。
- 上传文件链接要求：

  - 上传的视频、音频、图像文件支持HTTP链接，不支持本地路径。也可使用平台提供的[临时存储空间](/zh/model-studio/get-temporary-file-url)，上传本地文件并创建链接。
  - 如果使用临时存储空间上传文件后获取到的`oss://`前缀URL，在HTTP请求头中必须添加`X-DashScope-OssResourceResolve: enable`参数。未添加该参数会导致报错`No connection adapters were found for 'oss://...'`。详细说明请参见[上传文件获取临时URL](/zh/model-studio/get-temporary-file-url)。

### 作业提交接口 <span id="99d4e18b3e9hc" />

```http
POST https://dashscope.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis
```

#### 入参描述 <span id="45e3c74b3byfh" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "20.93%" }} /><col style={{ width: "9.66%" }} /><col style={{ width: "8.37%" }} /><col style={{ width: "8.37%" }} /><col style={{ width: "32.21%" }} /><col style={{ width: "20.46%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>Content-Type</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>请求类型：application/json</p></td><td><p>application/json</p></td></tr><tr><td><p>Authorization</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>API-Key，例如：Bearer d1\*\*2a</p></td><td><p>Bearer d1\*\*2a</p></td></tr><tr><td><p>X-DashScope-Async</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>设置为<code>enable</code>，表明使用异步方式创建任务</p></td><td><p>enable</p></td></tr><tr><td><p>model</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>是</p></td><td><p>指明需要调用的模型</p></td><td><p>videoretalk</p></td></tr><tr><td><p>input.video\_url</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>是</p></td><td><p>用户上传的视频文件 URL。</p> <p>URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。<span>您也可在此<a href="/zh/model-studio/get-temporary-file-url">获取临时公网URL</a>。</span></p> <p>视频文件要求：</p> <ul><li><p>大小：文件≤300MB</p></li> <li><p>格式：mp4、avi、mov</p></li> <li><p>时长：2秒＜时长＜120秒</p></li> <li><p>帧率：15fps≤帧率≤60fps</p></li> <li><p>编码：推荐采用H.264或H.265编码</p></li> <li><p>边长：640≤边长≤2048</p></li> <li><p>内容：人物正面出镜的近景画面，避免大角度侧脸或人脸过小。如果视频的画面中人脸拍不全、没有人，请参考<a href="/zh/model-studio/videoretalk-api#a6986dc8b724w">常见问题</a>进行处理。</p></li></ul></td><td><p>http\://aaa/bbb.mp4</p></td></tr><tr><td><p>input.audio\_url</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>是</p></td><td><p>用户上传的音频文件 URL。</p> <p>URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。<span>您也可在此<a href="/zh/model-studio/get-temporary-file-url">获取临时公网URL</a>。</span></p> <p>音频文件要求：</p> <ul><li><p>大小：文件≤30MB</p></li> <li><p>格式：wav、mp3、aac</p></li> <li><p>时长：2秒＜时长＜120秒，如果视频和音频的时长不一致，请参考<a href="/zh/model-studio/videoretalk-api#a6986dc8b724w">常见问题</a>进行处理</p></li> <li><p>内容：音频中需包含清晰、响亮的人声语音，并去除了环境噪音、背景音乐等声音干扰信息。</p></li></ul></td><td><p>http\://aaa/bbb.wav</p></td></tr><tr><td><p>input.ref\_image\_url</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>否</p></td><td><p>用户上传的人脸参考图 URL。</p> <p>URL 需为公网可访问的地址，并支持 HTTP 或 HTTPS 协议。<span>您也可在此<a href="/zh/model-studio/get-temporary-file-url">获取临时公网URL</a>。</span></p> <p>当输入视频中存在多张人脸时，您可以通过该参数指定用于口型匹配的人脸。如果视频中仅有一张人脸，则无需进行指定。</p> <p>若不输入人脸参考图，默认将选择视频中第一个有人脸的画面中，人脸占比最大的人物为目标。</p> <p>图像文件要求：</p> <ul><li><p>内容：需包含一张清晰的人物正脸，且为视频中出现的人物</p></li> <li><p>文件大小：文件≤10MB</p></li> <li><p>图像大小：长宽比小于等于2，最大边长小于等4096</p></li> <li><p>格式：jpeg、jpg、png、bmp、webp</p></li></ul></td><td><p>http\://aaa/bbb.jpg</p></td></tr><tr><td><p>parameters.video\_extension</p></td><td><p>Boolean</p></td><td><p>Body</p></td><td><p>否</p></td><td><p>当输入的音频时长大于视频时长时，是否扩展视频长度。默认值为<code>false</code>，可设置为<code>true</code>或<code>false</code>。</p> <ul><li><p>值为<code>true</code>时，使用原视频画面“倒放-正放”交替模式扩展视频时长，直至与音频相同。</p></li> <li><p>值为<code>false</code>时，不扩展画面长度，生成视频时长将与原视频相同，音频将被截断。</p></li></ul></td><td><p>false</p></td></tr><tr><td><p>parameters.query\_face\_threshold</p></td><td><p>Integer</p></td><td><p>Body</p></td><td><p>否</p></td><td><p>当输入人脸参考图时，可通过该值调整人脸匹配的置信度。</p> <p>取值范围为120-200。值越小，人脸参考图的匹配越宽松；值越大，人脸参考图的匹配越严格。默认值为170。</p> <p>备注：input.ref\_image\_url 为空时该参数不生效。</p></td><td><p>170</p></td></tr></tbody></table>

#### 出参描述 <span id="1553c7f6daoc3" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "23.99%" }} /><col style={{ width: "10.95%" }} /><col style={{ width: "34.3%" }} /><col style={{ width: "30.76%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>output.task\_id</p></td><td><p>String</p></td><td><p>提交异步任务的作业 ID，实际作业结果需要通过异步任务查询接口获取。</p></td><td><p>a8532587-fa8c-4ef8-82be-0c46b17950d1</p></td></tr><tr><td><p>output.task\_status</p></td><td><p>String</p></td><td><p>提交异步任务后的作业状态。</p></td><td><p>“PENDING”</p></td></tr><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统唯一码。</p></td><td><p>7574ee8f-38a3-4b1e-9280-11c33ab46e51</p></td></tr></tbody></table>

#### 请求示例 <span id="469d8d64cc95l" />

```bash
curl --location 'https://dashscope.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis' \
--header 'X-DashScope-Async: enable' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY" \
--header 'Content-Type: application/json' \
--data '{
    "model": "videoretalk",
    "input": {
        "video_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/pvegot/input_video_01.mp4",
        "audio_url": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/aumwir/stella2-%E6%9C%89%E5%A3%B0%E4%B9%A67.wav",
        "ref_image_url": ""
     },
    "parameters": {
        "video_extension": false
    }
  }'
```

#### 响应示例 <span id="0d7715817cl3c" />

```json
{
    "output": {
	"task_id": "a8532587-fa8c-4ef8-82be-0c46b17950d1",
        "task_status": "PENDING"
    },
    "request_id": "7574ee8f-38a3-4b1e-9280-11c33ab46e51"
}
```

### 作业任务状态查询和结果获取接口 <span id="c89331030e0y3" />

```http
GET https://dashscope.aliyuncs.com/api/v1/tasks/{task_id}
```

#### 入参描述 <span id="3fe6afeb79bju" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "16.26%" }} /><col style={{ width: "9.98%" }} /><col style={{ width: "9.18%" }} /><col style={{ width: "5.48%" }} /><col style={{ width: "32.21%" }} /><col style={{ width: "26.89%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>Authorization</p></td><td><p>String</p></td><td><p>Header</p></td><td><p>是</p></td><td><p>API-Key，例如：Bearer d1\*\*2a。</p></td><td><p>Bearer d1\*\*2a</p></td></tr><tr><td><p>task\_id</p></td><td><p>String</p></td><td><p>Url Path</p></td><td><p>是</p></td><td><p>需要查询作业的task\_id，为作业提交接口返回的值。</p></td><td><p>a8532587-fa8c-4ef8-82be-0c46b17950d1</p></td></tr></tbody></table>

#### 出参描述 <span id="1fe854c50ckzz" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24.15%" }} /><col style={{ width: "10.95%" }} /><col style={{ width: "28.99%" }} /><col style={{ width: "35.91%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>output.task\_id</p></td><td><p>String</p></td><td><p>查询作业的 task\_id。</p></td><td><p>a8532587-fa8c-4ef8-82be-0c46b17950d1</p></td></tr><tr><td><p>output.task\_status</p></td><td><p>String</p></td><td><p>被查询作业的作业状态。</p></td><td><p>任务状态：</p> <ul><li><p>PENDING：排队中</p></li> <li><p>PRE-PROCESSING：前置处理中</p></li> <li><p>RUNNING：处理中</p></li> <li><p>POST-PROCESSING：后置处理中</p></li> <li><p>SUCCEEDED：成功</p></li> <li><p>FAILED：失败</p></li> <li><p>UNKNOWN：作业不存在或状态未知</p></li></ul></td></tr><tr><td><p>output.video\_url</p></td><td><p>String</p></td><td><p>生成的视频，<strong>video\_url有效期为作业完成后24小时。</strong></p></td><td><p><a href="https://xxx/1.mp4">https\://xxx/1.mp4</a>"</p></td></tr><tr><td><p>usage.video\_duration</p></td><td><p>Float</p></td><td><p>本次请求生成视频时长计量，单位：秒。</p></td><td><p>"video\_duration": 10.23</p></td></tr><tr><td><p>usage.video\_ratio</p></td><td><p>String</p></td><td><p>本次请求生成视频的画幅类型，该值为standard，默认按原视频比例输出。</p></td><td><p>"video\_ratio": "standard"</p></td></tr><tr><td><p>usage.size</p></td><td><p>String</p></td><td><p>本次请求生成视频的分辨率，和输入视频分辨率一致</p></td><td><p>"size": "1080\*1920"</p></td></tr><tr><td><p>usage.fps</p></td><td><p>Integer</p></td><td><p>本次请求生成视频的帧率，和输入视频帧率一致</p></td><td><p>"fps": 25</p></td></tr><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>本次请求的系统唯一码。</p></td><td><p>7574ee8f-38a3-4b1e-9280-11c33ab46e51</p></td></tr></tbody></table>

#### 请求示例 <span id="e0200c87dblj3" />

```bash
curl -X GET 'https://dashscope.aliyuncs.com/api/v1/tasks/<YOUR_TASK_ID>' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY"
```

#### 响应示例 <span id="7bfc2ff742jqm" />

```json
{
    "request_id": "87b9dce5-7f36-4305-a347-xxxxxx",
    "output": {
        "task_id": "3afd65eb-9604-48ea-8a91-xxxxxx",
        "task_status": "SUCCEEDED",
        "submit_time": "2025-09-11 20:15:29.887",
        "scheduled_time": "2025-09-11 20:15:36.741",
        "end_time": "2025-09-11 20:16:40.577",
        "video_url": "http://dashscope-result-sh.oss-cn-shanghai.aliyuncs.com/xxx.mp4?Expires=xxx"
    },
    "usage": {
        "video_duration": 7.2,
        "size": "1080*1920",
        "video_ratio": "standard",
        "fps": 25
    }
}
```

#### 异常响应示例 <span id="4d01f109585oo" />

```json
{
    "request_id": "7574ee8f-38a3-4b1e-9280-11c33ab46e51",
    "output": {
        "task_id": "a8532587-fa8c-4ef8-82be-0c46b17950d1",
        "task_status": "FAILED",
        "code": "xxx",
        "message": "xxxxxx"
    }
}
```

## 状态码说明 <span id="0a1ca76200o0m" />

大模型服务通用状态码请查阅：[错误码](/zh/model-studio/error-code)

同时本模型还有如下特定错误码：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.552326%" }} /><col style={{ width: "21.947674%" }} /><col style={{ width: "32.122093%" }} /><col style={{ width: "30.377907%" }} /></colgroup><thead><tr><th><p><strong>HTTP 返回码</strong></p></th><th><p><strong>错误码（code）</strong></p></th><th><p><strong>错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>Field required: xxx</p></td><td><p>缺少入参，或格式错误</p></td></tr><tr><td><p>400</p></td><td><p>InvalidURL.ConnectionRefused</p></td><td><p>Connection to $\{url} refused, please provide avaiable URL</p></td><td><p>下载被拒绝，请提供可用的url</p></td></tr><tr><td><p>400</p></td><td><p>InvalidURL.Timeout</p></td><td><p>Download $\{url} timeout, please check network connection.</p></td><td><p>下载超时，60s超时</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.Size</p></td><td><p>Invalid file size. The video/audio/image file size must be less than \*\*MB.</p></td><td><p>视频/音频/图像文件必须小于\*\*MB</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.Format</p></td><td><p>Invalid file format，the request file format is one of the following types: MP4, AVI, MOV, MP3, WAV, AAC, JPEG, JPG, PNG, BMP, and WEBP.</p></td><td><p>文件格式不符合要求，视频支持mp4、avi、mov；音频支持mp3, wav, aac；图片支持jpg, jpeg, png, bmp, webp</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.Resolution</p></td><td><p>Invalid video resolution. The height or width of video must be 640 \~ 2048.</p></td><td><p>视频边长需介于640-2048之间</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.FPS</p></td><td><p>Invalid video FPS. The video FPS must be 15 \~ 60.</p></td><td><p>视频帧率需介于15-60fps之间。</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.Duration</p></td><td><p>Invalid file duration. The video/audio file duration must be 2s \~ 120s.</p></td><td><p>视频/音频时长需介于2-120s之间</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.ImageSize</p></td><td><p>The size of image is beyond limit.</p></td><td><p>图片大小超出限制。</p> <p>要求图片长宽比例不大于2，且最长边不大于4096</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.Openerror</p></td><td><p>Invalid file, cannot open file as video/audio/image.</p></td><td><p>视频/音频/图像文件无法打开</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.Content</p></td><td><p>The input image has no human body or multi human bodies. Please upload other image with single person.</p></td><td><p>输入图片中没有人或有多人</p></td></tr><tr><td><p>400</p></td><td><p>InvalidFile.FaceNotMatch</p></td><td><p>There are no matched face in the video with the provided reference image.</p></td><td><p>参考图与视频人脸匹配失败</p></td></tr></tbody></table>

## 常见问题 <span id="a6986dc8b724w" />

1. 输入语音和视频长度不一致，会如何处理？

   默认将按音频、视频两者中时长较短的来截断。

   当输入的音频时长大于视频时长时，并希望按音频长度来生成时，可将入参的视频扩展（parameters.video\_extension）值设为true，算法将使用原视频画面“倒放-正放”交替模式扩展视频时长，直至与音频相同。
2. 输入音频中有静音的情况，会如何处理？

   音频静音的时段，预期视频中人物也会闭嘴。
3. 输入视频的画面中有无人/脸拍不全的情况，会如何处理？

   若音频中有人声，但画面无人或未出现人物嘴型，则保留原视频画面，音频正常播放。
4. 输入视频的画面中有多人的情况，会如何处理？

   仅支持替换一个人物。算法会按照输入人脸参考图（input.ref\_image\_url），识别指定人脸。若未输入人脸参考图，则默认选择第一个有人脸画面中，占比最大的人脸。

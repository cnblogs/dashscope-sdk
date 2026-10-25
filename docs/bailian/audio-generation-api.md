> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 音频生成 API参考

> 使用 HTTPS 接口提交文本提示词和参考音频，获取生成的音频文件。本文说明请求参数、响应结构和错误处理。

<meta property="og:title" content="音频生成 API参考" />

## 接入前准备

获取对应地域的 [API Key](/zh/model-studio/get-api-key) 和[业务空间 ID](/zh/model-studio/obtain-the-app-id-and-workspace-id)，分别配置为环境变量 `DASHSCOPE_API_KEY` 和 `SFM_WORKSPACE_ID`。下文示例以北京地域为例。

请求方法为 HTTPS POST，端点如下，其中 `{WorkspaceId}` 为业务空间 ID：

<Tabs>
  <Tab title="华北2（北京）">
    `POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/SpeechSynthesizer`
  </Tab>

  <Tab title="新加坡">
    `POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/audio/tts/SpeechSynthesizer`
  </Tab>
</Tabs>

### 请求头

<table autofit="true" tablewidth="100%" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "26%" }} />

    <col style={{ width: "12%" }} />

    <col style={{ width: "62%" }} />
  </colgroup>

  <thead><tr><th style={{ verticalAlign: "top" }}>字段</th><th style={{ verticalAlign: "top" }}>必填</th><th style={{ verticalAlign: "top" }}>说明</th></tr></thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}>Authorization</td><td style={{ verticalAlign: "top" }}>是</td><td style={{ verticalAlign: "top" }}><code>Bearer \<API Key></code></td></tr>
    <tr><td style={{ verticalAlign: "top" }}>Content-Type</td><td style={{ verticalAlign: "top" }}>是</td><td style={{ verticalAlign: "top" }}><code>application/json</code></td></tr>
  </tbody>
</table>

## 调用示例

```bash
curl --request POST \
  "https://$SFM_WORKSPACE_ID.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/SpeechSynthesizer" \
  --max-time 300 \
  --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
  --header 'Content-Type: application/json' \
  --data '{
    "model": "qwen-audio-3.1-tts-next",
    "input": {
      "text_prompt": "一位女性清晰地说：“你好，欢迎使用。”",
      "format": "wav",
      "sample_rate": 48000,
      "channels": 2
    }
  }'
```

响应示例（ID 和下载地址已替换为示意值）：

```json
{
  "request_id": "example-request-id",
  "output": {
    "finish_reason": "stop",
    "audio": {
      "data": "",
      "url": "https://example.com/generated.wav",
      "id": "audio_example-request-id",
      "expires_at": 1789616932,
      "duration": 1.12
    }
  },
  "usage": {
    "duration": 1
  }
}
```

使用实际响应中的 `output.audio.url` 下载文件，URL 有效期为 24 小时。不要使用上述示意地址下载。

## 请求参数

`model` 位于请求体顶层，其余生成参数位于 `input` 对象内。

<table autofit="true" tablewidth="100%" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "26%" }} />

    <col style={{ width: "10%" }} />

    <col style={{ width: "10%" }} />

    <col style={{ width: "9%" }} />

    <col style={{ width: "45%" }} />
  </colgroup>

  <thead><tr><th style={{ verticalAlign: "top" }}>字段</th><th style={{ verticalAlign: "top" }}>类型</th><th style={{ verticalAlign: "top" }}>必填</th><th style={{ verticalAlign: "top" }}>默认值</th><th style={{ verticalAlign: "top" }}>说明</th></tr></thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}>model</td><td style={{ verticalAlign: "top" }}>string</td><td style={{ verticalAlign: "top" }}>是</td><td style={{ verticalAlign: "top" }}>—</td><td style={{ verticalAlign: "top" }}>模型 ID，见支持的模型。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input</td><td style={{ verticalAlign: "top" }}>object</td><td style={{ verticalAlign: "top" }}>是</td><td style={{ verticalAlign: "top" }}>—</td><td style={{ verticalAlign: "top" }}>音频生成输入。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.text\_prompt</td><td style={{ verticalAlign: "top" }}>string</td><td style={{ verticalAlign: "top" }}>是</td><td style={{ verticalAlign: "top" }}>—</td><td style={{ verticalAlign: "top" }}>音频描述或待合成文本。参考音频按顺序使用 <code>@voice1</code>、<code>@voice2</code>、<code>@voice3</code> 引用。长度上限见模型表。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.references</td><td style={{ verticalAlign: "top" }}>array</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>—</td><td style={{ verticalAlign: "top" }}>参考音频列表。不传时仅使用文本生成。当前模型最多支持 3 条，每条最长 30 秒、不超过 10 MB。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.references\[].audio\_url</td><td style={{ verticalAlign: "top" }}>string</td><td style={{ verticalAlign: "top" }}>条件必填</td><td style={{ verticalAlign: "top" }}>—</td><td style={{ verticalAlign: "top" }}>服务端可访问的公网音频 URL，与 <code>audio\_data</code> 二选一。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.references\[].audio\_data</td><td style={{ verticalAlign: "top" }}>string</td><td style={{ verticalAlign: "top" }}>条件必填</td><td style={{ verticalAlign: "top" }}>—</td><td style={{ verticalAlign: "top" }}>音频 data URI：<code>data:\{mime\_type};base64,\{base64\_encoded\_data}</code>。与 <code>audio\_url</code> 二选一。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.format</td><td style={{ verticalAlign: "top" }}>string</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>wav</td><td style={{ verticalAlign: "top" }}>输出格式：<code>wav</code>、<code>mp3</code>、<code>pcm</code>。不支持 Opus 输出。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.sample\_rate</td><td style={{ verticalAlign: "top" }}>integer</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>48000</td><td style={{ verticalAlign: "top" }}>输出采样率，单位 Hz：8000、16000、24000、44100、48000。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.channels</td><td style={{ verticalAlign: "top" }}>integer</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>2</td><td style={{ verticalAlign: "top" }}>声道数：1（单声道）、2（双声道）。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.volume</td><td style={{ verticalAlign: "top" }}>integer</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>50</td><td style={{ verticalAlign: "top" }}>音量，取值范围 \[0, 100]。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.enable\_cbr</td><td style={{ verticalAlign: "top" }}>boolean</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>false</td><td style={{ verticalAlign: "top" }}>仅 MP3 生效。true 为恒定码率（CBR），false 为可变码率（VBR）。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.bit\_rate</td><td style={{ verticalAlign: "top" }}>integer</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>128</td><td style={{ verticalAlign: "top" }}>仅 MP3 CBR 生效，单位 kbps。实际输出受采样率及 MP3 编码档位约束，见下表。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.quality</td><td style={{ verticalAlign: "top" }}>integer</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>5</td><td style={{ verticalAlign: "top" }}>仅 MP3 VBR 生效。取值范围 \[0, 9]，0 为最高质量。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.rate</td><td style={{ verticalAlign: "top" }}>float</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>1.0</td><td style={{ verticalAlign: "top" }}>语速，取值范围 \[0.5, 2.0]。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.seed</td><td style={{ verticalAlign: "top" }}>integer</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>42</td><td style={{ verticalAlign: "top" }}>请求级别随机种子。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>input.enable\_aigc\_tag</td><td style={{ verticalAlign: "top" }}>boolean</td><td style={{ verticalAlign: "top" }}>否</td><td style={{ verticalAlign: "top" }}>false</td><td style={{ verticalAlign: "top" }}>是否在生成音频中添加 AIGC 标识水印。</td></tr>
  </tbody>
</table>

参考音频支持 WAV、MP3 和 OGG Opus，不支持裸 PCM。参考音频格式与输出格式是两套限制：可以输入 OGG Opus，但不能输出 Opus。

参考音频须通过 URL 或 Base64 提交，不支持使用系统音色或声音复刻音色 ID。

### MP3 CBR 码率

<table autofit="true" tablewidth="100%" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "30%" }} />

    <col style={{ width: "35%" }} />

    <col style={{ width: "35%" }} />
  </colgroup>

  <thead><tr><th style={{ verticalAlign: "top" }}>采样率（Hz）</th><th style={{ verticalAlign: "top" }}>输出码率下限（kbps）</th><th style={{ verticalAlign: "top" }}>输出码率上限（kbps）</th></tr></thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}>8000</td><td style={{ verticalAlign: "top" }}>8</td><td style={{ verticalAlign: "top" }}>64</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>16000、24000</td><td style={{ verticalAlign: "top" }}>8</td><td style={{ verticalAlign: "top" }}>160</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>44100、48000</td><td style={{ verticalAlign: "top" }}>32</td><td style={{ verticalAlign: "top" }}>320</td></tr>
  </tbody>
</table>

码率由采样率和 MP3 编码档位共同决定。表中为输出码率范围，不表示支持区间内的任意整数；超出范围的指定值会被限制在对应范围内。

## 提交参考音频

以下 Python 示例使用两条参考音频生成双人对话。先安装 `requests`，准备分别包含两位说话人声音、符合模型限制的 `reference1.wav` 和 `reference2.wav`，并配置前述环境变量。

`references` 按列表顺序分配槽位：第一项 `reference1.wav` 对应 `@voice1`，第二项 `reference2.wav` 对应 `@voice2`。示例将两条音频分别编码为 Base64，并在提示词中引用对应说话人。

```python height=500
import base64
import os
from pathlib import Path

import requests

reference1 = base64.b64encode(Path("reference1.wav").read_bytes()).decode("ascii")
reference2 = base64.b64encode(Path("reference2.wav").read_bytes()).decode("ascii")
workspace_id = os.environ["SFM_WORKSPACE_ID"]
endpoint = (
    f"https://{workspace_id}.cn-beijing.maas.aliyuncs.com"
    "/api/v1/services/audio/tts/SpeechSynthesizer"
)
response = requests.post(
    endpoint,
    headers={
        "Authorization": f"Bearer {os.environ['DASHSCOPE_API_KEY']}",
        "Content-Type": "application/json",
    },
    json={
        "model": "qwen-audio-3.1-tts-next",
        "input": {
            "text_prompt": "@voice1 说：“今天阳光很好，一起去散步吧。”@voice2 回答：“好啊，我们去公园。”",
            "references": [
                {"audio_data": f"data:audio/wav;base64,{reference1}"},
                {"audio_data": f"data:audio/wav;base64,{reference2}"}
            ],
            "format": "wav",
        },
    },
    timeout=300,
)
response.raise_for_status()
result = response.json()
audio_response = requests.get(result["output"]["audio"]["url"], timeout=60)
audio_response.raise_for_status()
Path("output.wav").write_bytes(audio_response.content)
```

使用 URL 时，将列表元素替换为 `{"audio_url": "可公开访问的音频URL"}`，不要同时填写 `audio_data`。多人参考按列表顺序编号，不能引用不存在的编号。

## 响应参数

<table autofit="true" tablewidth="100%" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "30%" }} />

    <col style={{ width: "12%" }} />

    <col style={{ width: "58%" }} />
  </colgroup>

  <thead><tr><th style={{ verticalAlign: "top" }}>字段</th><th style={{ verticalAlign: "top" }}>类型</th><th style={{ verticalAlign: "top" }}>说明</th></tr></thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}>request\_id</td><td style={{ verticalAlign: "top" }}>string</td><td style={{ verticalAlign: "top" }}>请求 ID，用于问题排查。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>output.finish\_reason</td><td style={{ verticalAlign: "top" }}>string</td><td style={{ verticalAlign: "top" }}>正常结束时为 "stop"。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>output.audio.data</td><td style={{ verticalAlign: "top" }}>string</td><td style={{ verticalAlign: "top" }}>此调用方式下为空字符串；通过 output.audio.url 下载完整音频。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>output.audio.url</td><td style={{ verticalAlign: "top" }}>string</td><td style={{ verticalAlign: "top" }}>完整音频文件的下载 URL，有效期 24 小时。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>output.audio.id</td><td style={{ verticalAlign: "top" }}>string</td><td style={{ verticalAlign: "top" }}>生成音频的 ID。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>output.audio.expires\_at</td><td style={{ verticalAlign: "top" }}>integer</td><td style={{ verticalAlign: "top" }}>下载 URL 的过期时间戳。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>output.audio.duration</td><td style={{ verticalAlign: "top" }}>float</td><td style={{ verticalAlign: "top" }}>生成音频的时长，单位秒。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>usage.duration</td><td style={{ verticalAlign: "top" }}>integer</td><td style={{ verticalAlign: "top" }}>本次生成音频的时长，按秒四舍五入。此字段不用于计算 Token 费用。</td></tr>
  </tbody>
</table>

播客场景单次最多生成 240 秒（4 分钟）音频，其他场景单次最多生成 120 秒。单价请参见[模型调用计费](/zh/model-studio/model-pricing#audio-generation-pricing)。

## 支持的模型

<table autofit="true" tablewidth="100%" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "34%" }} />

    <col style={{ width: "22%" }} />

    <col style={{ width: "44%" }} />
  </colgroup>

  <thead><tr><th style={{ verticalAlign: "top" }}>模型 ID</th><th style={{ verticalAlign: "top" }}>Prompt 上限</th><th style={{ verticalAlign: "top" }}>单次生成时长上限</th></tr></thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}><a href="/zh/model-studio/qwen-audio-3-1-tts-next">qwen-audio-3.1-tts-next</a></td><td style={{ verticalAlign: "top" }}>3000 字符</td><td style={{ verticalAlign: "top" }}>播客：240 秒（4 分钟）；其他场景：120 秒</td></tr>
  </tbody>
</table>

场景、试听与提示词写法请参见[音频生成](/zh/model-studio/audio-generation)。本文示例使用表中的模型。

## 错误处理

错误响应示例：

```json
{
  "request_id": "example-request-id",
  "code": "CLIENT_ERROR",
  "message": "text_prompt exceeds the maximum length of 3000 characters."
}
```

<table autofit="true" tablewidth="100%" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "15%" }} />

    <col style={{ width: "25%" }} />

    <col style={{ width: "60%" }} />
  </colgroup>

  <thead><tr><th style={{ verticalAlign: "top" }}>HTTP 状态码</th><th style={{ verticalAlign: "top" }}>code</th><th style={{ verticalAlign: "top" }}>处理方式</th></tr></thead>

  <tbody>
    <tr><td style={{ verticalAlign: "top" }}>400</td><td style={{ verticalAlign: "top" }}>CLIENT\_ERROR</td><td style={{ verticalAlign: "top" }}>检查 Prompt 长度、参考音频数量与时长、URL/Base64 互斥关系、引用编号，以及是否使用了不支持的 voice 字段。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>404</td><td style={{ verticalAlign: "top" }}>InvalidParameter</td><td style={{ verticalAlign: "top" }}>若 message 为 "Model not exist."，核对模型 ID、地域及当前账号可用范围。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>401</td><td style={{ verticalAlign: "top" }}>InvalidApiKey</td><td style={{ verticalAlign: "top" }}>核对 API Key 是否有效。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>403</td><td style={{ verticalAlign: "top" }}>AccessDenied</td><td style={{ verticalAlign: "top" }}>检查模型调用权限。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>429</td><td style={{ verticalAlign: "top" }}>Throttling.RateQuota</td><td style={{ verticalAlign: "top" }}>降低请求频率。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>400</td><td style={{ verticalAlign: "top" }}>DataInspectionFailed</td><td style={{ verticalAlign: "top" }}>检查文本或参考音频是否符合内容安全要求。</td></tr>
    <tr><td style={{ verticalAlign: "top" }}>500</td><td style={{ verticalAlign: "top" }}>InternalError</td><td style={{ verticalAlign: "top" }}>保留 request\_id，稍后重试或联系技术支持。</td></tr>
  </tbody>
</table>

> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 视频口型替换-声动人像VideoRetalk

> 声动人像VideoRetalk是一个人物视频生成模型，可基于人物视频和人声音频，生成人物讲话口型与输入音频相匹配的新视频。

<meta property="og:title" content="视频口型替换-声动人像VideoRetalk" />

<Tip>
  本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 模型概览 <span id="2c5fd145d6aab" />

### 模型效果示例 <span id="0b4fcc80c1dfp" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>输入示例</strong></p></th><th><p><strong>输出示例</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>人物视频：</p><video controls><source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/pvegot/input_video_01.mp4" type="video/mp4" /></video><p>人声音频：</p><audio controls><source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/aumwir/stella2-%E6%9C%89%E5%A3%B0%E4%B9%A67.wav" type="audio/x-wav" /></audio></td><td style={{ verticalAlign: "top" }}><video controls><source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250717/cvyhia/60540fe4-5d30-4b6c-846a-68e09a112064_vidretalk.mp4" type="video/mp4" /></video></td></tr></tbody></table>

## 资费与限流 <span id="babef7c7bcvfy" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "15.97%" }} /><col style={{ width: "18.01%" }} /><col style={{ width: "21.61%" }} /><col style={{ width: "18.01%" }} /><col style={{ width: "26.4%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>单价</strong></p></th><th><p><strong>免费额度</strong><a href="/zh/model-studio/new-free-quota">（查看）</a></p></th><th><p><strong>任务下发接口RPS限制</strong></p></th><th><p><strong>同时处理中任务数量</strong></p></th></tr></thead><tbody><tr><td><p>videoretalk</p></td><td><p>后付费，按生成视频的时长计费：0.08元/秒</p></td><td><p>1800秒</p></td><td><p>1</p></td><td><p>1</p><p>（在同一时刻，只有1个作业实际处于运行状态，其他队列中的作业处于排队状态）</p></td></tr></tbody></table>

### 模型调用 <span id="a71eae78f68zw" />

- 声动人像VideoRetalk模型支持后付费调用。目前仅支持通过API调用，不支持在阿里云百炼的控制台在线体验。
- 调用“声动人像VideoRetalk”模型，输入画面清晰且正面镜头的人物视频，以及人声清晰的音频文件，即可生成人物口型替换视频。具体操作请参见[VideoRetalk视频生成](/zh/model-studio/videoretalk-api)。

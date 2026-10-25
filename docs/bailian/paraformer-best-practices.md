> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 最佳实践

<meta property="og:title" content="最佳实践" />

## 预处理视频文件以提高文件转写效率（针对录音文件识别场景） <span id="c5dda0a0cf2x9" />

虽然Paraformer语音识别API可以兼容视频文件，但由于视频文件尺寸通常较大、传输较为耗时，建议对其进行预处理，仅提取需要进行语音识别的音轨，并进行合理压缩，从而显著降低文件尺寸。这样做将大大加快视频文件转写的吞吐效率。在以下最佳实践中，展示了如何使用ffmpeg进行有关的预处理。

### 前提条件 <span id="MpVu6" />

安装ffmpeg：[请前往ffmpeg官方网站](https://ffmpeg.org/)。

### 预处理视频文件 <span id="LkSEj" />

使用ffmpeg提取视频文件中的第一条音轨、降采样到16kHz、并压缩编码为opus文件。

```bash
ffmpeg -i input-video-file -ac 1 -ar 16000 -acodec libopus output-audio-file.opus
```

一般情况下，输出的音频文件将显著小于输入的视频文件的尺寸，接下去可向文件转写API提交该音频文件（以URL指定），获得语音识别结果。

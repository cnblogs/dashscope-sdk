> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 声音复刻API参考

> 声音复刻依托大模型进行特征提取，无需训练即可复刻声音。仅需提供 10~20 秒的音频，即可生成高度相似且听感自然的定制音色。声音复刻与模型调用是前后关联的两个步骤。本文档聚焦于介绍声音复刻的参数和接口细节，模型调用请参见 实时（Qwen-Omni-Realtime） 或 非实时（Qwen-Omni） 。

<meta property="og:title" content="声音复刻API参考" />

<strong>用户指南</strong>：关于模型介绍和选型建议请参见[实时（Qwen-Omni-Realtime）](/zh/model-studio/realtime)或[非实时（Qwen-Omni）](/zh/model-studio/qwen-omni)。

<Tip>
  本文档专用于千问Omni和千问Omni-Realtime声音复刻接口；若您使用的是语音合成模型，请参见[语音合成](/zh/model-studio/speech-synthesis-api-reference)。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，<strong>能够为推理请求提供卓越的性能和更高的稳定性</strong>，建议迁移至新域名：

  - 华北2（北京）地域：从 `https://dashscope.aliyuncs.com` 迁移至 `https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `https://dashscope-intl.aliyuncs.com` 迁移至 `https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`

  其中 `{WorkspaceId}` 为您的业务空间 ID，可在阿里云百炼控制台的<strong>业务空间详情</strong>页面查看。现有域名仍可正常使用。
</Tip>

## 音频要求 <span id="fe3daa6c3f7jw" />

高质量的输入音频是获得优质复刻效果的基础。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "23.3%" }} /><col style={{ width: "76.7%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>项目</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>要求</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p><strong>支持格式</strong></p></td><td style={{ verticalAlign: "top" }}><p>WAV (16bit)、MP3、M4A</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>音频时长</strong></p></td><td style={{ verticalAlign: "top" }}><p>推荐10\~20秒，最长不得超过60秒</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>文件大小</strong></p></td><td style={{ verticalAlign: "top" }}><p>＜ 10 MB</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>采样率</strong></p></td><td style={{ verticalAlign: "top" }}><p>≥ 24 kHz</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>声道</strong></p></td><td style={{ verticalAlign: "top" }}><p>单声道</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>内容</strong></p></td><td style={{ verticalAlign: "top" }}><p>音频必须包含至少3秒连续清晰朗读（无背景音），其余部分仅允许短暂停顿（≤2秒）；整段音频应避免背景音乐、噪音或其他人声，确保核心朗读内容质量；请使用正常说话音频作为输入，不要上传歌曲或唱歌音频，以确保复刻效果准确和可用</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>语言</strong></p></td><td style={{ verticalAlign: "top" }}><p>中文（zh）、英文（en）、德语（de）、意大利语（it）、葡萄牙语（pt）、西班牙语（es）、日语（ja）、韩语（ko）、法语（fr）、俄语（ru）、泰语（th）、印尼语（id）、阿拉伯语（ar）、捷克语（cs）、丹麦语（da）、荷兰语（nl）、芬兰语（fi）、希伯来语（he）、印地语（hi）、冰岛语（is）、马来语（ms）、挪威语（no）、波斯语（fa）、波兰语（pl）、瑞典语（sv）、他加禄语（tl）、土耳其语（tr）、乌尔都语（ur）、越南语（vi）</p> <p>中文方言：东北话（Dongbei）、陕西话（Shannxi）、四川话（Sichuan）、河南话（Henan）、长沙话（Changsha）、天津话（Tianjin）、杭州话（Hangzhou）、辽宁话（Liaoning）、沈阳话（Shenyang）、鞍山话（Anshan）</p></td></tr></tbody></table>

## 快速开始：复刻与使用音色 <span id="bb60d81324wsu" />

<img src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/2365984871/CAEQbxiBgICd6_Do8BkiIDM3NjYwZDQxMGIyMTQzMDdhOGMyY2YwNWFhMmM2NjVi5899512_20251120114927.389.svg" alt="image" />

### 1. 工作流程 <span id="a2df2ea2e75bc" />

声音复刻与模型调用是紧密关联的两个独立步骤，遵循“先创建，后使用”的流程：

1. 创建音色

   调用[创建音色](/zh/model-studio/qwen-omni-voice-cloning#1eaa57d82did9)接口，上传一段音频。系统会分析该音频，创建一个专属的复刻音色。<strong>此步骤必须指定</strong>`target_model`<strong>，声明创建的音色将由哪个全模态模型驱动。</strong>

   若已有创建好的音色（调用[查询音色列表](/zh/model-studio/qwen-omni-voice-cloning#401d33226330i)接口查看），可跳过这一步直接进行下一步。
2. 使用音色进行对话

   调用 Omni 接口（实时或非实时），传入上一步获得的音色。<strong>此步骤指定的全模态模型必须和上一步的</strong>`target_model`<strong>一致。</strong>

### 2. 模型配置与准备工作 <span id="fc04e61befslt" />

选择合适的模型并完成准备工作。

#### 模型配置 <span id="ef6ea6b7e902t" />

声音复刻时需要指定以下两个模型：

- 声音复刻模型：qwen-voice-enrollment
- 驱动音色的全模态模型：

  - qwen3.8-omni-flash-realtime
  - qwen3.5-omni-plus-realtime
  - qwen3.5-omni-flash-realtime
  - qwen3.5-omni-plus
  - qwen3.5-omni-flash

#### 准备工作 <span id="c59930c21b85q" />

1. <strong>获取API Key</strong>：[获取与配置 API Key](/zh/model-studio/get-api-key)，为安全起见，推荐将API Key配置到环境变量。
2. <strong>安装SDK</strong>：确保已[安装最新版DashScope SDK](/zh/model-studio/install-sdk)。
3. <strong>准备待复刻音频</strong>：音频需符合[音频要求](/zh/model-studio/qwen-omni-voice-cloning#fe3daa6c3f7jw)。

### 3. 端到端示例 <span id="2be563ecb13j7" />

以下示例演示了如何在对话中使用声音复刻生成的专属音色，实现与原音高度相似的输出效果。

- <strong>关键原则</strong>：声音复刻时，`target_model`（驱动音色的全模态模型）必须与后续调用 Omni 接口时指定的模型一致，否则会合成失败。
- 示例使用本地音频文件 `voice.mp3` 进行声音复刻，运行代码时，请注意替换。

<Tabs>
  <Tab title="实时">
    适用于千问Omni-Realtime系列模型，更多说明请参见[实时（Qwen-Omni-Realtime）](/zh/model-studio/realtime)。

    <CodeGroup dropdown>
      ```python Python expandable
      # 依赖：dashscope >= 1.26.5，pyaudio
      import os
      import requests
      import base64
      import pathlib
      import time
      import pyaudio
      from dashscope.audio.qwen_omni import MultiModality, OmniRealtimeCallback, OmniRealtimeConversation
      import dashscope

      # ======= 常量配置 =======
      DEFAULT_TARGET_MODEL = "qwen3.8-omni-flash-realtime"  # 声音复刻、实时对话要使用相同的模型
      DEFAULT_PREFERRED_NAME = "guanyu"
      DEFAULT_AUDIO_MIME_TYPE = "audio/mpeg"
      VOICE_FILE_PATH = "voice.mp3"  # 用于声音复刻的本地音频文件的相对路径

      def create_voice(file_path: str,
                       url: str,
                       target_model: str = DEFAULT_TARGET_MODEL,
                       preferred_name: str = DEFAULT_PREFERRED_NAME,
                       audio_mime_type: str = DEFAULT_AUDIO_MIME_TYPE) -> str:
          """
          创建音色，并返回 voice 参数
          """
          # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
          # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key = "sk-xxx"
          api_key = os.getenv("DASHSCOPE_API_KEY")

          file_path_obj = pathlib.Path(file_path)
          if not file_path_obj.exists():
              raise FileNotFoundError(f"音频文件不存在: {file_path}")

          base64_str = base64.b64encode(file_path_obj.read_bytes()).decode()
          data_uri = f"data:{audio_mime_type};base64,{base64_str}"
          payload = {
              "model": "qwen-voice-enrollment",
              "parameters": {"voice_clone_mode": "normal"},
              "input": {
                  "action": "create",
                  "target_model": target_model,
                  "preferred_name": preferred_name,
                  "audio": {"data": data_uri}
              }
          }
          headers = {
              "Authorization": f"Bearer {api_key}",
              "Content-Type": "application/json"
          }

          resp = requests.post(url, json=payload, headers=headers)
          if resp.status_code != 200:
              raise RuntimeError(f"创建 voice 失败: {resp.status_code}, {resp.text}")

          try:
              return resp.json()["output"]["voice"]
          except (KeyError, ValueError) as e:
              raise RuntimeError(f"解析 voice 响应失败: {e}")

      class SimpleCallback(OmniRealtimeCallback):
          def __init__(self, pya):
              self.pya = pya
              self.out = None
          def on_open(self):
              self.out = self.pya.open(
                  format=pyaudio.paInt16,
                  channels=1,
                  rate=24000,
                  output=True
              )
          def on_event(self, response):
              if response['type'] == 'response.audio.delta':
                  self.out.write(base64.b64decode(response['delta']))
              elif response['type'] == 'conversation.item.input_audio_transcription.completed':
                  print(f"[User] {response['transcript']}")
              elif response['type'] == 'response.audio_transcript.done':
                  print(f"[LLM] {response['transcript']}")

      if __name__ == '__main__':
          # 若没有配置环境变量，请用百炼API Key将下行替换为：dashscope.api_key = "sk-xxx"
          dashscope.api_key = os.getenv("DASHSCOPE_API_KEY")
          # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          base_url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com"
          ws_url = base_url.replace("https://", "wss://") + "/api-ws/v1/realtime"

          # 1. 声音复刻：创建专属音色
          voice = create_voice(VOICE_FILE_PATH, url=f"{base_url}/api/v1/services/audio/tts/customization")
          print(f"声音复刻完成，音色: {voice}")

          # 2. 使用复刻音色进行实时对话
          pya = pyaudio.PyAudio()
          callback = SimpleCallback(pya)
          conv = OmniRealtimeConversation(model=DEFAULT_TARGET_MODEL, callback=callback, url=ws_url)
          conv.connect()
          conv.update_session(
              output_modalities=[MultiModality.AUDIO, MultiModality.TEXT],
              voice=voice  # 使用复刻生成的专属音色
          )
          mic = pya.open(format=pyaudio.paInt16, channels=1, rate=16000, input=True)
          print("对话已开始，对着麦克风说话 (Ctrl+C 退出)...")
          try:
              while True:
                  audio_data = mic.read(3200, exception_on_overflow=False)
                  conv.append_audio(base64.b64encode(audio_data).decode())
                  time.sleep(0.01)
          except KeyboardInterrupt:
              conv.close()
              mic.close()
              callback.out.close()
              pya.terminate()
              print("\n对话结束")
      ```

      ```java Java expandable
      import com.alibaba.dashscope.audio.omni.*;
      import com.alibaba.dashscope.exception.NoApiKeyException;
      import com.alibaba.dashscope.utils.Constants;
      import com.google.gson.Gson;
      import com.google.gson.JsonObject;

      import javax.sound.sampled.*;
      import java.io.*;
      import java.net.HttpURLConnection;
      import java.net.URL;
      import java.nio.ByteBuffer;
      import java.nio.file.*;
      import java.nio.charset.StandardCharsets;
      import java.util.Arrays;
      import java.util.Base64;
      import java.util.Queue;
      import java.util.concurrent.ConcurrentLinkedQueue;
      import java.util.concurrent.atomic.AtomicBoolean;

      public class Main {
          // ===== 常量定义 =====
          // 声音复刻、实时对话要使用相同的模型
          private static final String TARGET_MODEL = "qwen3.8-omni-flash-realtime";
          private static final String PREFERRED_NAME = "guanyu";
          // 用于声音复刻的本地音频文件的相对路径
          private static final String AUDIO_FILE = "voice.mp3";
          private static final String AUDIO_MIME_TYPE = "audio/mpeg";
          // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          private static final String BASE_URL = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com";

          // 生成 data URI
          public static String toDataUrl(String filePath) throws IOException {
              byte[] bytes = Files.readAllBytes(Paths.get(filePath));
              String encoded = Base64.getEncoder().encodeToString(bytes);
              return "data:" + AUDIO_MIME_TYPE + ";base64," + encoded;
          }

          // 调用 API 创建 voice
          public static String createVoice() throws Exception {
              // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              // 若没有配置环境变量，请用百炼API Key将下行替换为：String apiKey = "sk-xxx"
              String apiKey = System.getenv("DASHSCOPE_API_KEY");

              String jsonPayload =
                      "{"
                              + "\"model\": \"qwen-voice-enrollment\","
                              + "\"parameters\": {\"voice_clone_mode\": \"normal\"},"
                              + "\"input\": {"
                              +     "\"action\": \"create\","
                              +     "\"target_model\": \"" + TARGET_MODEL + "\","
                              +     "\"preferred_name\": \"" + PREFERRED_NAME + "\","
                              +     "\"audio\": {"
                              +         "\"data\": \"" + toDataUrl(AUDIO_FILE) + "\""
                              +     "}"
                              + "}"
                              + "}";

              String url = BASE_URL + "/api/v1/services/audio/tts/customization";
              HttpURLConnection con = (HttpURLConnection) new URL(url).openConnection();
              con.setRequestMethod("POST");
              con.setRequestProperty("Authorization", "Bearer " + apiKey);
              con.setRequestProperty("Content-Type", "application/json");
              con.setDoOutput(true);

              try (OutputStream os = con.getOutputStream()) {
                  os.write(jsonPayload.getBytes(StandardCharsets.UTF_8));
              }

              int status = con.getResponseCode();
              try (BufferedReader br = new BufferedReader(
                      new InputStreamReader(status >= 200 && status < 300 ? con.getInputStream() : con.getErrorStream(),
                              StandardCharsets.UTF_8))) {
                  StringBuilder response = new StringBuilder();
                  String line;
                  while ((line = br.readLine()) != null) {
                      response.append(line);
                  }
                  if (status == 200) {
                      JsonObject jsonObj = new Gson().fromJson(response.toString(), JsonObject.class);
                      return jsonObj.getAsJsonObject("output").get("voice").getAsString();
                  }
                  throw new IOException("创建语音失败: " + status + " - " + response);
              }
          }

          // 简单的音频播放器
          static class SimpleAudioPlayer {
              private final SourceDataLine line;
              private final Queue<byte[]> audioQueue = new ConcurrentLinkedQueue<>();
              private final Thread playerThread;
              private final AtomicBoolean shouldStop = new AtomicBoolean(false);

              public SimpleAudioPlayer() throws LineUnavailableException {
                  AudioFormat format = new AudioFormat(24000, 16, 1, true, false);
                  line = AudioSystem.getSourceDataLine(format);
                  line.open(format);
                  line.start();
                  playerThread = new Thread(() -> {
                      while (!shouldStop.get()) {
                          byte[] audio = audioQueue.poll();
                          if (audio != null) {
                              line.write(audio, 0, audio.length);
                          } else {
                              try { Thread.sleep(10); } catch (InterruptedException ignored) {}
                          }
                      }
                  }, "AudioPlayer");
                  playerThread.start();
              }

              public void play(String base64Audio) {
                  audioQueue.add(Base64.getDecoder().decode(base64Audio));
              }

              public void close() {
                  shouldStop.set(true);
                  try { playerThread.join(1000); } catch (InterruptedException ignored) {}
                  line.drain();
                  line.close();
              }
          }

          public static void main(String[] args) {
              try {
                  // 1. 声音复刻：创建专属音色
                  String voice = createVoice();
                  System.out.println("声音复刻完成，音色: " + voice);

                  // 2. 使用复刻音色进行实时对话
                  SimpleAudioPlayer player = new SimpleAudioPlayer();
                  AtomicBoolean shouldStop = new AtomicBoolean(false);

                  OmniRealtimeParam param = OmniRealtimeParam.builder()
                          .model(TARGET_MODEL)
                          // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                          // 若没有配置环境变量，请用百炼API Key将下行替换为：.apikey("sk-xxx")
                          .apikey(System.getenv("DASHSCOPE_API_KEY"))
                          .url(BASE_URL.replace("https://", "wss://") + "/api-ws/v1/realtime")
                          .build();

                  OmniRealtimeConversation conversation = new OmniRealtimeConversation(param, new OmniRealtimeCallback() {
                      @Override public void onOpen() { System.out.println("连接已建立"); }
                      @Override public void onClose(int code, String reason) {
                          System.out.println("连接已关闭 (" + code + "): " + reason);
                          shouldStop.set(true);
                      }
                      @Override public void onEvent(JsonObject event) {
                          String type = event.get("type").getAsString();
                          if ("response.audio.delta".equals(type)) {
                              player.play(event.get("delta").getAsString());
                          } else if ("conversation.item.input_audio_transcription.completed".equals(type)) {
                              System.out.println("[User] " + event.get("transcript").getAsString());
                          } else if ("response.audio_transcript.done".equals(type)) {
                              System.out.println("[LLM] " + event.get("transcript").getAsString());
                          }
                      }
                  });

                  conversation.connect();
                  conversation.updateSession(OmniRealtimeConfig.builder()
                          .modalities(Arrays.asList(OmniRealtimeModality.AUDIO, OmniRealtimeModality.TEXT))
                          .voice(voice)  // 使用复刻生成的专属音色
                          .enableTurnDetection(true)
                          .enableInputAudioTranscription(true)
                          .build()
                  );

                  System.out.println("对话已开始，对着麦克风说话 (Ctrl+C 退出)...");
                  AudioFormat format = new AudioFormat(16000, 16, 1, true, false);
                  TargetDataLine mic = AudioSystem.getTargetDataLine(format);
                  mic.open(format);
                  mic.start();

                  ByteBuffer buffer = ByteBuffer.allocate(3200);
                  while (!shouldStop.get()) {
                      int bytesRead = mic.read(buffer.array(), 0, buffer.capacity());
                      if (bytesRead > 0) {
                          conversation.appendAudio(Base64.getEncoder().encodeToString(buffer.array()));
                      }
                      Thread.sleep(20);
                  }

                  conversation.close(1000, "正常结束");
                  player.close();
                  mic.close();
                  System.out.println("\n对话结束");
              } catch (NoApiKeyException e) {
                  System.err.println("未找到API KEY: 请设置环境变量 DASHSCOPE_API_KEY");
              } catch (Exception e) {
                  e.printStackTrace();
              }
              System.exit(0);
          }
      }
      ```
    </CodeGroup>
  </Tab>

  <Tab title="非实时">
    适用于千问Omni系列模型，更多说明请参见[非实时（Qwen-Omni）](/zh/model-studio/qwen-omni)。

    <CodeGroup dropdown>
      ```python Python expandable
      # 依赖：dashscope >= 1.23.9，soundfile，numpy
      import os
      import requests
      import base64
      import pathlib
      import numpy as np
      import soundfile as sf
      import dashscope

      # ======= 常量配置 =======
      DEFAULT_TARGET_MODEL = "qwen3.5-omni-plus"  # 声音复刻、非实时对话要使用相同的模型
      DEFAULT_PREFERRED_NAME = "guanyu"
      DEFAULT_AUDIO_MIME_TYPE = "audio/mpeg"
      VOICE_FILE_PATH = "voice.mp3"  # 用于声音复刻的本地音频文件的相对路径

      def create_voice(file_path: str,
                       target_model: str = DEFAULT_TARGET_MODEL,
                       preferred_name: str = DEFAULT_PREFERRED_NAME,
                       audio_mime_type: str = DEFAULT_AUDIO_MIME_TYPE) -> str:
          """
          创建音色，并返回 voice 参数
          """
          # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
          # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key = "sk-xxx"
          api_key = os.getenv("DASHSCOPE_API_KEY")

          file_path_obj = pathlib.Path(file_path)
          if not file_path_obj.exists():
              raise FileNotFoundError(f"音频文件不存在: {file_path}")

          base64_str = base64.b64encode(file_path_obj.read_bytes()).decode()
          data_uri = f"data:{audio_mime_type};base64,{base64_str}"

          url = dashscope.base_http_api_url + "/services/audio/tts/customization"
          payload = {
              "model": "qwen-voice-enrollment",
              "input": {
                  "action": "create",
                  "target_model": target_model,
                  "preferred_name": preferred_name,
                  "audio": {"data": data_uri}
              }
          }
          headers = {
              "Authorization": f"Bearer {api_key}",
              "Content-Type": "application/json"
          }

          resp = requests.post(url, json=payload, headers=headers)
          if resp.status_code != 200:
              raise RuntimeError(f"创建 voice 失败: {resp.status_code}, {resp.text}")

          try:
              return resp.json()["output"]["voice"]
          except (KeyError, ValueError) as e:
              raise RuntimeError(f"解析 voice 响应失败: {e}")

      if __name__ == '__main__':
          # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          dashscope.base_http_api_url = 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1'

          # 1. 声音复刻：创建专属音色
          voice = create_voice(VOICE_FILE_PATH)
          print(f"声音复刻完成，音色: {voice}")

          # 2. 使用复刻音色进行非实时对话
          messages = [{"role": "user", "content": [{"text": "你好，请做一段自我介绍"}]}]

          response = dashscope.MultiModalConversation.call(
              # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
              api_key=os.getenv("DASHSCOPE_API_KEY"),
              model=DEFAULT_TARGET_MODEL,
              messages=messages,
              modalities=["text", "audio"],
              audio={"voice": voice, "format": "wav"},  # 使用复刻生成的专属音色
              stream=True
          )

          print("模型回复：")
          audio_base64_string = ""
          for r in response:
              try:
                  content = r.output.choices[0].message.content
                  for item in content:
                      if isinstance(item, dict):
                          if "audio" in item:
                              audio_base64_string += item["audio"].get("data", "")
                          elif "text" in item:
                              print(item["text"], end="")
              except Exception:
                  pass

          if audio_base64_string:
              wav_bytes = base64.b64decode(audio_base64_string)
              audio_np = np.frombuffer(wav_bytes, dtype=np.int16)
              sf.write("audio_cloned.wav", audio_np, samplerate=24000)
              print("\n音频文件已保存至：audio_cloned.wav")
      ```

      ```java Java expandable
      import com.google.gson.Gson;
      import com.google.gson.JsonObject;

      import java.io.*;
      import java.net.HttpURLConnection;
      import java.net.URL;
      import java.nio.ByteBuffer;
      import java.nio.ByteOrder;
      import java.nio.file.*;
      import java.nio.charset.StandardCharsets;
      import java.util.Base64;

      public class Main {
          // ===== 常量定义 =====
          // 声音复刻、非实时对话要使用相同的模型
          private static final String TARGET_MODEL = "qwen3.5-omni-plus";
          private static final String PREFERRED_NAME = "guanyu";
          // 用于声音复刻的本地音频文件的相对路径
          private static final String AUDIO_FILE = "voice.mp3";
          private static final String AUDIO_MIME_TYPE = "audio/mpeg";
          // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
          private static final String BASE_URL = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com";

          // 将 PCM 数据写入标准 WAV 文件
          public static void writeWav(String path, byte[] pcmData, int sampleRate) throws IOException {
              int channels = 1, bitsPerSample = 16;
              int byteRate = sampleRate * channels * bitsPerSample / 8;
              int blockAlign = channels * bitsPerSample / 8;
              ByteBuffer header = ByteBuffer.allocate(44).order(ByteOrder.LITTLE_ENDIAN);
              header.put("RIFF".getBytes()); header.putInt(36 + pcmData.length);
              header.put("WAVE".getBytes()); header.put("fmt ".getBytes());
              header.putInt(16); header.putShort((short) 1); header.putShort((short) channels);
              header.putInt(sampleRate); header.putInt(byteRate);
              header.putShort((short) blockAlign); header.putShort((short) bitsPerSample);
              header.put("data".getBytes()); header.putInt(pcmData.length);
              try (FileOutputStream fos = new FileOutputStream(path)) {
                  fos.write(header.array());
                  fos.write(pcmData);
              }
          }

          // 生成 data URI
          public static String toDataUrl(String filePath) throws IOException {
              byte[] bytes = Files.readAllBytes(Paths.get(filePath));
              String encoded = Base64.getEncoder().encodeToString(bytes);
              return "data:" + AUDIO_MIME_TYPE + ";base64," + encoded;
          }

          // 调用 API 创建 voice
          public static String createVoice() throws Exception {
              // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              // 若没有配置环境变量，请用百炼API Key将下行替换为：String apiKey = "sk-xxx"
              String apiKey = System.getenv("DASHSCOPE_API_KEY");

              String jsonPayload =
                      "{"
                              + "\"model\": \"qwen-voice-enrollment\","
                              + "\"input\": {"
                              +     "\"action\": \"create\","
                              +     "\"target_model\": \"" + TARGET_MODEL + "\","
                              +     "\"preferred_name\": \"" + PREFERRED_NAME + "\","
                              +     "\"audio\": {"
                              +         "\"data\": \"" + toDataUrl(AUDIO_FILE) + "\""
                              +     "}"
                              + "}"
                              + "}";

              String url = BASE_URL + "/api/v1/services/audio/tts/customization";
              HttpURLConnection con = (HttpURLConnection) new URL(url).openConnection();
              con.setRequestMethod("POST");
              con.setRequestProperty("Authorization", "Bearer " + apiKey);
              con.setRequestProperty("Content-Type", "application/json");
              con.setDoOutput(true);

              try (OutputStream os = con.getOutputStream()) {
                  os.write(jsonPayload.getBytes(StandardCharsets.UTF_8));
              }

              int status = con.getResponseCode();
              try (BufferedReader br = new BufferedReader(
                      new InputStreamReader(status >= 200 && status < 300 ? con.getInputStream() : con.getErrorStream(),
                              StandardCharsets.UTF_8))) {
                  StringBuilder response = new StringBuilder();
                  String line;
                  while ((line = br.readLine()) != null) {
                      response.append(line);
                  }
                  if (status == 200) {
                      JsonObject jsonObj = new Gson().fromJson(response.toString(), JsonObject.class);
                      return jsonObj.getAsJsonObject("output").get("voice").getAsString();
                  }
                  throw new IOException("创建语音失败: " + status + " - " + response);
              }
          }

          public static void main(String[] args) {
              try {
                  // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                  // 若没有配置环境变量，请用百炼API Key将下行替换为：String apiKey = "sk-xxx"
                  String apiKey = System.getenv("DASHSCOPE_API_KEY");

                  // 1. 声音复刻：创建专属音色
                  String voice = createVoice();
                  System.out.println("声音复刻完成，音色: " + voice);

                  // 2. 使用复刻音色进行非实时对话（OpenAI 兼容接口）
                  String requestBody = "{"
                          + "\"model\": \"" + TARGET_MODEL + "\","
                          + "\"messages\": [{\"role\": \"user\", \"content\": \"你好，请做一段自我介绍\"}],"
                          + "\"modalities\": [\"text\", \"audio\"],"
                          + "\"audio\": {\"voice\": \"" + voice + "\", \"format\": \"wav\"},"
                          + "\"stream\": true,"
                          + "\"stream_options\": {\"include_usage\": true}"
                          + "}";

                  String url = BASE_URL + "/compatible-mode/v1/chat/completions";
                  HttpURLConnection con = (HttpURLConnection) new URL(url).openConnection();
                  con.setRequestMethod("POST");
                  con.setRequestProperty("Authorization", "Bearer " + apiKey);
                  con.setRequestProperty("Content-Type", "application/json");
                  con.setDoOutput(true);

                  try (OutputStream os = con.getOutputStream()) {
                      os.write(requestBody.getBytes(StandardCharsets.UTF_8));
                  }

                  // 3. 解析流式 SSE 响应
                  StringBuilder audioBase64 = new StringBuilder();
                  System.out.println("模型回复：");

                  try (BufferedReader br = new BufferedReader(
                          new InputStreamReader(con.getInputStream(), StandardCharsets.UTF_8))) {
                      String line;
                      while ((line = br.readLine()) != null) {
                          if (!line.startsWith("data: ") || line.equals("data: [DONE]")) continue;
                          String json = line.substring(6);
                          JsonObject chunk = new Gson().fromJson(json, JsonObject.class);
                          if (!chunk.has("choices") || chunk.getAsJsonArray("choices").size() == 0) continue;

                          JsonObject delta = chunk.getAsJsonArray("choices").get(0)
                                  .getAsJsonObject().getAsJsonObject("delta");
                          if (delta.has("content") && !delta.get("content").isJsonNull()) {
                              System.out.print(delta.get("content").getAsString());
                          }
                          if (delta.has("audio") && !delta.get("audio").isJsonNull()) {
                              JsonObject audio = delta.getAsJsonObject("audio");
                              if (audio.has("data")) {
                                  audioBase64.append(audio.get("data").getAsString());
                              }
                          }
                      }
                  }

                  // 4. 保存音频文件（API 返回原始 PCM 数据，需添加 WAV 头）
                  if (audioBase64.length() > 0) {
                      byte[] pcmBytes = Base64.getDecoder().decode(audioBase64.toString());
                      writeWav("audio_cloned.wav", pcmBytes, 24000);
                      System.out.println("\n音频文件已保存至：audio_cloned.wav");
                  }
              } catch (Exception e) {
                  e.printStackTrace();
              }
          }
      }
      ```
    </CodeGroup>
  </Tab>
</Tabs>

## API参考 <span id="a52b6c14e5zw9" />

使用不同 API 时，请确保使用同一账号进行操作。

### 创建音色 <span id="1eaa57d82did9" />

上传用于复刻的音频，创建自定义音色。

- <strong>URL</strong>

  华北2（北京）：

```http
POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization
```

新加坡：

```http
POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/audio/tts/customization
```

- <strong>请求头</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25.02%" }} /><col style={{ width: "8.53%" }} /><col style={{ width: "10.3%" }} /><col style={{ width: "56.15%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>是否必须</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Authorization</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>鉴权令牌，格式为<code>Bearer \<your\_api\_key></code>，使用时，将“<code>\<your\_api\_key></code>”替换为实际的API Key。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Content-Type</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>请求体中传输的数据的媒体类型。固定为<code>application/json</code>。</p></td></tr></tbody></table>
- <strong>消息体</strong>

  包含所有请求参数的消息体如下，对于可选字段，在实际业务中可根据需求省略。

  <Tip>
    注意区分如下参数：

    - `model`：声音复刻模型，固定为`qwen-voice-enrollment`
    - `target_model`：驱动音色的全模态模型，须和后续调用实时多模态接口时使用的全模态模型一致，否则合成会失败
  </Tip>

```json
{
    "model": "qwen-voice-enrollment",
    "input": {
        "action": "create",
        "target_model": "qwen3.8-omni-flash-realtime",
        "preferred_name": "guanyu",
        "audio": {
            "data": "https://xxx.wav"
        },
        "text": "可选项，填入audio.data对应的文本",
        "language": "可选项，填入audio.data对应的语种，如zh"
    },
    "parameters": {
        "voice_clone_mode": "normal",
        "skip_audio_process": false
    }
}
```

<Tip>
  `parameters`中的`voice_clone_mode`参数在使用qwen3.8-omni系列模型时必传。不传该字段时，默认回退到qwen3.5-omni talker system instruct模式（兼容旧服务）。
</Tip>

- <strong>请求参数</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
    <colgroup>
      <col style={{ width: "14.98%" }} />

      <col style={{ width: "9.45%" }} />

      <col style={{ width: "9.51%" }} />

      <col style={{ width: "10.83%" }} />

      <col style={{ width: "55.23%" }} />
    </colgroup>

    <thead>
      <tr>
        <th style={{ verticalAlign: "top" }}>
          <strong>参数</strong>
        </th>

        <th style={{ verticalAlign: "top" }}>
          <strong>类型</strong>
        </th>

        <th style={{ verticalAlign: "top" }}>
          <strong>默认值</strong>
        </th>

        <th style={{ verticalAlign: "top" }}>
          <strong>是否必须</strong>
        </th>

        <th style={{ verticalAlign: "top" }}>
          <strong>说明</strong>
        </th>
      </tr>
    </thead>

    <tbody>
      <tr>
        <td style={{ verticalAlign: "top" }}>
          model
        </td>

        <td style={{ verticalAlign: "top" }}>
          string
        </td>

        <td style={{ verticalAlign: "top" }}>
          -
        </td>

        <td style={{ verticalAlign: "top" }}>
          支持
        </td>

        <td style={{ verticalAlign: "top" }}>
          声音复刻模型，固定为`qwen-voice-enrollment`。
        </td>
      </tr>

      <tr>
        <td style={{ verticalAlign: "top" }}>
          action
        </td>

        <td style={{ verticalAlign: "top" }}>
          string
        </td>

        <td style={{ verticalAlign: "top" }}>
          -
        </td>

        <td style={{ verticalAlign: "top" }}>
          支持
        </td>

        <td style={{ verticalAlign: "top" }}>
          操作类型，固定为`create`。
        </td>
      </tr>

      <tr>
        <td style={{ verticalAlign: "top" }}>
          target\_model
        </td>

        <td style={{ verticalAlign: "top" }}>
          string
        </td>

        <td style={{ verticalAlign: "top" }}>
          -
        </td>

        <td style={{ verticalAlign: "top" }}>
          支持
        </td>

        <td style={{ verticalAlign: "top" }}>
          驱动音色的全模态模型：

          - qwen3.8-omni-flash-realtime
          - qwen3.5-omni-plus-realtime
          - qwen3.5-omni-flash-realtime
          - qwen3.5-omni-plus
          - qwen3.5-omni-flash

          必须与后续调用全模态接口时使用的模型一致，否则合成会失败。
        </td>
      </tr>

      <tr>
        <td style={{ verticalAlign: "top" }}>
          preferred\_name
        </td>

        <td style={{ verticalAlign: "top" }}>
          string
        </td>

        <td style={{ verticalAlign: "top" }}>
          -
        </td>

        <td style={{ verticalAlign: "top" }}>
          支持
        </td>

        <td style={{ verticalAlign: "top" }}>
          为音色指定一个便于识别的名称（仅允许数字、大小写字母和下划线，不超过16个字符）。建议选用与角色、场景相关的标识。

          > 该关键字会在复刻的音色名中出现，例如关键字为“guanyu”，最终音色名为“qwen-omni-vc-guanyu-voice-20250812105009984-838b”
        </td>
      </tr>

      <tr>
        <td style={{ verticalAlign: "top" }}>
          audio.data
        </td>

        <td style={{ verticalAlign: "top" }}>
          string
        </td>

        <td style={{ verticalAlign: "top" }}>
          -
        </td>

        <td style={{ verticalAlign: "top" }}>
          支持
        </td>

        <td style={{ verticalAlign: "top" }}>
          用于复刻的音频（录制时需遵循[录音操作指南](/zh/model-studio/qwen-omni-voice-cloning#8d342f3949vge)，音频需满足[音频要求](/zh/model-studio/qwen-omni-voice-cloning)）。

          可通过以下两种方式提交音频数据：

          1. [Data URL](https://www.rfc-editor.org/rfc/rfc2397)

             格式：`data:<mediatype>;base64,<data>`

             - `<mediatype>`：MIME类型

               - WAV：`audio/wav`
               - MP3：`audio/mpeg`
               - M4A：`audio/mp4`
             - `<data>`：音频转成的Base64编码的字符串

               Base64编码会增大体积，请控制原文件大小，确保编码后仍小于10MB
             - 示例：`data:audio/wav;base64,SUQzBAAAAAAAI1RTU0UAAAAPAAADTGF2ZjU4LjI5LjEwMAAAAAAAAAAAAAAA//PAxABQ/BXRbMPe4IQAhl9`

               <Accordion title="点击查看示例代码">
                 <CodeGroup dropdown>
                   ```python
                   import base64, pathlib

                   # input.mp3为用于声音复刻的本地音频文件，请替换为自己的音频文件路径，确保其符合音频要求
                   file_path = pathlib.Path("input.mp3")
                   base64_str = base64.b64encode(file_path.read_bytes()).decode()
                   data_uri = f"data:audio/mpeg;base64,{base64_str}"
                   ```

                   ```java
                   import java.nio.file.*;
                   import java.util.Base64;

                   public class Main {
                       /**
                        * filePath为用于声音复刻的本地音频文件，请替换为自己的音频文件路径，确保其符合音频要求
                        */
                       public static String toDataUrl(String filePath) throws Exception {
                           byte[] bytes = Files.readAllBytes(Paths.get(filePath));
                           String encoded = Base64.getEncoder().encodeToString(bytes);
                           return "data:audio/mpeg;base64," + encoded;
                       }

                       // 使用示例
                       public static void main(String[] args) throws Exception {
                           System.out.println(toDataUrl("input.mp3"));
                       }
                   }
                   ```
                 </CodeGroup>
               </Accordion>
          2. 音频URL（推荐将音频[上传至OSS](https://help.aliyun.com/zh/oss/user-guide/simple-upload#a632b50f190j8)）

             - 文件大小不超过10MB
             - URL必须公网可访问且无需鉴权
        </td>
      </tr>

      <tr>
        <td style={{ verticalAlign: "top" }}>
          text
        </td>

        <td style={{ verticalAlign: "top" }}>
          string
        </td>

        <td style={{ verticalAlign: "top" }}>
          -
        </td>

        <td style={{ verticalAlign: "top" }}>
          不支持
        </td>

        <td style={{ verticalAlign: "top" }}>
          与`audio.data`音频内容相匹配的文本。

          传入该参数后，服务端会对比音频与该文本的差异，若差异过大，将返回Audio.PreprocessError。
        </td>
      </tr>

      <tr>
        <td style={{ verticalAlign: "top" }}>
          language
        </td>

        <td style={{ verticalAlign: "top" }}>
          string
        </td>

        <td style={{ verticalAlign: "top" }}>
          -
        </td>

        <td style={{ verticalAlign: "top" }}>
          不支持
        </td>

        <td style={{ verticalAlign: "top" }}>
          `audio.data`音频对应的语种。

          支持`zh`（中文）、`en`（英文）、`de`（德语）、`it`（意大利语）、`pt`（葡萄牙语）、`es`（西班牙语）、`ja`（日语）、`ko`（韩语）、`fr`（法语）、`ru`（俄语）、`th`（泰语）、`id`（印尼语）、`ar`（阿拉伯语）、`cs`（捷克语）、`da`（丹麦语）、`nl`（荷兰语）、`fi`（芬兰语）、`he`（希伯来语）、`hi`（印地语）、`is`（冰岛语）、`ms`（马来语）、`no`（挪威语）、`fa`（波斯语）、`pl`（波兰语）、`sv`（瑞典语）、`tl`（他加禄语）、`tr`（土耳其语）、`ur`（乌尔都语）、`vi`（越南语）。

          中文方言：`Dongbei`（东北话）、`Shannxi`（陕西话）、`Sichuan`（四川话）、`Henan`（河南话）、`Changsha`（长沙话）、`Tianjin`（天津话）、`Hangzhou`（杭州话）、`Liaoning`（辽宁话）、`Shenyang`（沈阳话）、`Anshan`（鞍山话）。

          若使用该参数，设置的语种要和实际用于复刻的音频的语种一致。
        </td>
      </tr>

      <tr>
        <td style={{ verticalAlign: "top" }}>
          voice\_clone\_mode
        </td>

        <td style={{ verticalAlign: "top" }}>
          string
        </td>

        <td style={{ verticalAlign: "top" }}>
          -
        </td>

        <td style={{ verticalAlign: "top" }}>
          条件必填
        </td>

        <td style={{ verticalAlign: "top" }}>
          声音复刻模式。使用qwen3.8-omni系列模型时必传，不传时默认回退到qwen3.5-omni talker system instruct模式（兼容旧服务）。

          可选值：

          - `normal`：普通模式
          - `dialect`：方言模式
          - `crosslingual`：跨语言模式

          该参数需放在`parameters`中。
        </td>
      </tr>

      <tr>
        <td style={{ verticalAlign: "top" }}>
          skip\_audio\_process
        </td>

        <td style={{ verticalAlign: "top" }}>
          boolean
        </td>

        <td style={{ verticalAlign: "top" }}>
          false
        </td>

        <td style={{ verticalAlign: "top" }}>
          不支持
        </td>

        <td style={{ verticalAlign: "top" }}>
          是否跳过音频预处理。默认为`false`。该参数需放在`parameters`中。
        </td>
      </tr>
    </tbody>
  </table>
- <strong>响应参数</strong>

  <Accordion title="点击查看响应示例">
    ```json
    {
        "output": {
            "voice": "yourVoice",
            "target_model": "qwen3.8-omni-flash-realtime"
        },
        "usage": {
            "count": 1
        },
        "request_id": "yourRequestId"
    }
    ```
  </Accordion>

  需关注的参数如下：

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "14.48%" }} /><col style={{ width: "9.89%" }} /><col style={{ width: "75.63%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>voice</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>音色名称，可直接用于实时多模态接口的<code>voice</code>参数。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>target\_model</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><div><p>驱动音色的全模态模型：</p> <ul><li><p>qwen3.8-omni-flash-realtime</p></li> <li><p>qwen3.5-omni-plus-realtime</p></li> <li><p>qwen3.5-omni-flash-realtime</p></li> <li><p>qwen3.5-omni-plus</p></li> <li><p>qwen3.5-omni-flash</p></li></ul> <p>必须与后续调用全模态接口时使用的模型一致，否则合成会失败。</p></div></td></tr><tr><td style={{ verticalAlign: "top" }}><p>request\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>Request ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>count</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><div><p>本次请求实际计入费用的“创建音色”次数，本次请求的费用为 count × 0.01<span>元</span>。</p></div> <p>创建音色时，count恒为1。</p></td></tr></tbody></table>
- <strong>示例代码</strong>

  <Tip>
    注意区分如下参数：

    - `model`：声音复刻模型，固定为`qwen-voice-enrollment`
    - `target_model`：驱动音色的语音合成模型，须和后续调用语音合成接口时使用的语音合成模型一致，否则合成会失败
  </Tip>

  <Tabs>
    <Tab title="cURL">
      若未将API Key配置到环境变量，需将示例中的`$DASHSCOPE_API_KEY`替换为实际的API Key。

      ```bash
      # ======= 重要提示 =======
      # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
      # 新加坡地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
      # === 执行时请删除该注释 ===

      curl -X POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      -H "Content-Type: application/json" \
      -d '{
          "model": "qwen-voice-enrollment",
          "input": {
              "action": "create",
              "target_model": "qwen3.8-omni-flash-realtime",
              "preferred_name": "chinese",
              "audio": {
                  "data": "https://xxx.wav"
              }
          },
          "parameters": {
              "skip_audio_process": false,
              "voice_clone_mode": "normal"
          }
      }'
      ```

      响应示例：

      ```json
      {"output":{"target_model":"qwen3.8-omni-flash-realtime","voice":"qwen-omni-vc-chinese-voice-20260911161744020-3f9d"},"usage":{"count":1},"request_id":"7a36a3cb-be34-9be8-a127-df2449d51ab2"}
      ```
    </Tab>

    <Tab title="Python">
      ```python expandable
      import os
      import requests
      import base64, pathlib

      target_model = "qwen3.8-omni-flash-realtime"
      preferred_name = "guanyu"
      audio_mime_type = "audio/mpeg"

      file_path = pathlib.Path("input.mp3")
      base64_str = base64.b64encode(file_path.read_bytes()).decode()
      data_uri = f"data:{audio_mime_type};base64,{base64_str}"

      # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
      # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key = "sk-xxx"
      api_key = os.getenv("DASHSCOPE_API_KEY")
      # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
      url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization"

      payload = {
          "model": "qwen-voice-enrollment", # 不要修改这个值
          "parameters": {"voice_clone_mode": "normal"},
          "input": {
              "action": "create",
              "target_model": target_model,
              "preferred_name": preferred_name,
              "audio": {
                  "data": data_uri
              }
          }
      }

      headers = {
          "Authorization": f"Bearer {api_key}",
          "Content-Type": "application/json"
      }

      # 发送 POST 请求
      resp = requests.post(url, json=payload, headers=headers)

      if resp.status_code == 200:
          data = resp.json()
          voice = data["output"]["voice"]
          print(f"生成的 voice 参数为: {voice}")
      else:
          print("请求失败:", resp.status_code, resp.text)
      ```
    </Tab>

    <Tab title="Java">
      ```java expandable
      import com.google.gson.Gson;
      import com.google.gson.JsonObject;

      import java.io.*;
      import java.net.HttpURLConnection;
      import java.net.URL;
      import java.nio.file.*;
      import java.util.Base64;

      public class Main {
          private static final String TARGET_MODEL = "qwen3.8-omni-flash-realtime";
          private static final String PREFERRED_NAME = "guanyu";
          private static final String AUDIO_FILE = "input.mp3";
          private static final String AUDIO_MIME_TYPE = "audio/mpeg";

          public static String toDataUrl(String filePath) throws Exception {
              byte[] bytes = Files.readAllBytes(Paths.get(filePath));
              String encoded = Base64.getEncoder().encodeToString(bytes);
              return "data:" + AUDIO_MIME_TYPE + ";base64," + encoded;
          }

          public static void main(String[] args) {
              // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              // 若没有配置环境变量，请用百炼API Key将下行替换为：String apiKey = "sk-xxx"
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
              String apiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization";

              try {
                  // 构造 JSON 请求体（注意内部的引号需转义）
                  String jsonPayload =
                          "{"
                                  + "\"model\": \"qwen-voice-enrollment\"," // 不要修改该值
                                  + "\"parameters\": {\"voice_clone_mode\": \"normal\"},"
                                  + "\"input\": {"
                                  +     "\"action\": \"create\","
                                  +     "\"target_model\": \"" + TARGET_MODEL + "\","
                                  +     "\"preferred_name\": \"" + PREFERRED_NAME + "\","
                                  +     "\"audio\": {"
                                  +         "\"data\": \"" + toDataUrl(AUDIO_FILE) + "\""
                                  +     "}"
                                  + "}"
                                  + "}";

                  HttpURLConnection con = (HttpURLConnection) new URL(apiUrl).openConnection();
                  con.setRequestMethod("POST");
                  con.setRequestProperty("Authorization", "Bearer " + apiKey);
                  con.setRequestProperty("Content-Type", "application/json");
                  con.setDoOutput(true);

                  // 发送请求体
                  try (OutputStream os = con.getOutputStream()) {
                      os.write(jsonPayload.getBytes("UTF-8"));
                  }

                  int status = con.getResponseCode();
                  InputStream is = (status >= 200 && status < 300)
                          ? con.getInputStream()
                          : con.getErrorStream();

                  StringBuilder response = new StringBuilder();
                  try (BufferedReader br = new BufferedReader(new InputStreamReader(is, "UTF-8"))) {
                      String line;
                      while ((line = br.readLine()) != null) {
                          response.append(line);
                      }
                  }

                  System.out.println("HTTP 状态码: " + status);
                  System.out.println("返回内容: " + response.toString());

                  if (status == 200) {
                      // 解析 JSON
                      Gson gson = new Gson();
                      JsonObject jsonObj = gson.fromJson(response.toString(), JsonObject.class);
                      String voice = jsonObj.getAsJsonObject("output").get("voice").getAsString();
                      System.out.println("生成的 voice 参数为: " + voice);
                  }

              } catch (Exception e) {
                  e.printStackTrace();
              }
          }
      }
      ```
    </Tab>
  </Tabs>

### 查询音色列表 <span id="401d33226330i" />

分页查询已创建的音色列表。

- <strong>URL</strong>

  华北2（北京）：

```http
POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization
```

新加坡：

```http
POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/audio/tts/customization
```

- <strong>请求头</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25.02%" }} /><col style={{ width: "8.53%" }} /><col style={{ width: "10.3%" }} /><col style={{ width: "56.15%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>是否必须</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Authorization</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>鉴权令牌，格式为<code>Bearer \<your\_api\_key></code>，使用时，将“<code>\<your\_api\_key></code>”替换为实际的API Key。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Content-Type</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>请求体中传输的数据的媒体类型。固定为<code>application/json</code>。</p></td></tr></tbody></table>
- <strong>消息体</strong>

  包含所有请求参数的消息体如下，对于可选字段，在实际业务中可根据需求省略。

  <Tip>
    `model`：声音复刻模型，固定为`qwen-voice-enrollment`，请勿修改。
  </Tip>

```json
{
    "model": "qwen-voice-enrollment",
    "input": {
        "action": "list",
        "page_size": 2,
        "page_index": 0
    }
}
```

- <strong>请求参数</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "13.54%" }} /><col style={{ width: "10.86%" }} /><col style={{ width: "8.83%" }} /><col style={{ width: "10%" }} /><col style={{ width: "56.77%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>默认值</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>是否必须</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>model</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>-</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>声音复刻模型，固定为<code>qwen-voice-enrollment</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>action</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>-</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>操作类型，固定为<code>list</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>page\_index</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>0</p></td><td style={{ verticalAlign: "top" }}><p>不支持</p></td><td style={{ verticalAlign: "top" }}><p>页码索引。取值范围：\[0, 1000000]。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>page\_size</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><p>10</p></td><td style={{ verticalAlign: "top" }}><p>不支持</p></td><td style={{ verticalAlign: "top" }}><p>每页包含数据条数。取值范围：\[0, 1000000]。</p></td></tr></tbody></table>
- <strong>响应参数</strong>

  <Accordion title="点击查看响应示例">
    ```json
    {
        "output": {
            "voice_list": [
                {
                    "voice": "yourVoice1",
                    "gmt_create": "2025-08-11 17:59:32",
                    "target_model": "qwen3.5-omni-plus-realtime"
                },
                {
                    "voice": "yourVoice2",
                    "gmt_create": "2025-08-11 17:38:10",
                    "target_model": "qwen3.5-omni-plus-realtime"
                }
            ]
        },
        "usage": {
            "count": 0
        },
        "request_id": "yourRequestId"
    }
    ```
  </Accordion>

  需关注的参数如下：

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "14.71%" }} /><col style={{ width: "9.69%" }} /><col style={{ width: "75.6%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>voice</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>音色名称，可直接用于实时多模态接口的<code>voice</code>参数。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>gmt\_create</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>创建音色的时间。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>target\_model</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><div><p>驱动音色的全模态模型：</p> <ul><li><p>qwen3.8-omni-flash-realtime</p></li> <li><p>qwen3.5-omni-plus-realtime</p></li> <li><p>qwen3.5-omni-flash-realtime</p></li> <li><p>qwen3.5-omni-plus</p></li> <li><p>qwen3.5-omni-flash</p></li></ul> <p>必须与后续调用全模态接口时使用的模型一致，否则合成会失败。</p></div></td></tr><tr><td style={{ verticalAlign: "top" }}><p>request\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>Request ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>count</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><div><p>本次请求实际计入费用的“创建音色”次数，本次请求的费用为count × 0.01<span>元</span>。</p></div> <p>查询音色不计费，因此<code>count</code>恒为0。</p></td></tr></tbody></table>
- <strong>示例代码</strong>

  <Tip>
    `model`：声音复刻模型，固定为`qwen-voice-enrollment`，请勿修改。
  </Tip>

  <Tabs>
    <Tab title="cURL">
      若未将API Key配置到环境变量，需将示例中的`$DASHSCOPE_API_KEY`替换为实际的API Key。

      ```bash
      # ======= 重要提示 =======
      # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
      # 新加坡地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
      # === 执行时请删除该注释 ===

      curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization' \
      --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --header 'Content-Type: application/json' \
      --data '{
          "model": "qwen-voice-enrollment",
          "input": {
              "action": "list",
              "page_size": 10,
              "page_index": 0
          }
      }'
      ```
    </Tab>

    <Tab title="Python">
      ```python expandable
      import os
      import requests

      # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
      # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key = "sk-xxx"
      api_key = os.getenv("DASHSCOPE_API_KEY")
      # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
      url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization"

      payload = {
          "model": "qwen-voice-enrollment", # 不要修改该值
          "input": {
              "action": "list",
              "page_size": 10,
              "page_index": 0
          }
      }

      headers = {
          "Authorization": f"Bearer {api_key}",
          "Content-Type": "application/json"
      }

      response = requests.post(url, json=payload, headers=headers)

      print("HTTP 状态码:", response.status_code)

      if response.status_code == 200:
          data = response.json()
          voice_list = data["output"]["voice_list"]

          print("查询到的音色列表：")
          for item in voice_list:
              print(f"- 音色: {item['voice']}  创建时间: {item['gmt_create']}  模型: {item['target_model']}")
      else:
          print("请求失败:", response.text)
      ```
    </Tab>

    <Tab title="Java">
      ```java expandable
      import com.google.gson.Gson;
      import com.google.gson.JsonArray;
      import com.google.gson.JsonObject;

      import java.io.BufferedReader;
      import java.io.InputStreamReader;
      import java.io.OutputStream;
      import java.net.HttpURLConnection;
      import java.net.URL;

      public class Main {
          public static void main(String[] args) {
              // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              // 若没有配置环境变量，请用百炼API Key将下行替换为：String apiKey = "sk-xxx"
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
              String apiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization";

              // JSON 请求体（旧版本 Java 无 """ 多行字符串）
              String jsonPayload =
                      "{"
                              + "\"model\": \"qwen-voice-enrollment\"," // 不要修改该值
                              + "\"input\": {"
                              +     "\"action\": \"list\","
                              +     "\"page_size\": 10,"
                              +     "\"page_index\": 0"
                              + "}"
                              + "}";

              try {
                  HttpURLConnection con = (HttpURLConnection) new URL(apiUrl).openConnection();
                  con.setRequestMethod("POST");
                  con.setRequestProperty("Authorization", "Bearer " + apiKey);
                  con.setRequestProperty("Content-Type", "application/json");
                  con.setDoOutput(true);

                  try (OutputStream os = con.getOutputStream()) {
                      os.write(jsonPayload.getBytes("UTF-8"));
                  }

                  int status = con.getResponseCode();
                  BufferedReader br = new BufferedReader(new InputStreamReader(
                          status >= 200 && status < 300 ? con.getInputStream() : con.getErrorStream(), "UTF-8"));

                  StringBuilder response = new StringBuilder();
                  String line;
                  while ((line = br.readLine()) != null) {
                      response.append(line);
                  }
                  br.close();

                  System.out.println("HTTP 状态码: " + status);
                  System.out.println("返回 JSON: " + response.toString());

                  if (status == 200) {
                      Gson gson = new Gson();
                      JsonObject jsonObj = gson.fromJson(response.toString(), JsonObject.class);
                      JsonArray voiceList = jsonObj.getAsJsonObject("output").getAsJsonArray("voice_list");

                      System.out.println("\n 查询到的音色列表：");
                      for (int i = 0; i < voiceList.size(); i++) {
                          JsonObject voiceItem = voiceList.get(i).getAsJsonObject();
                          String voice = voiceItem.get("voice").getAsString();
                          String gmtCreate = voiceItem.get("gmt_create").getAsString();
                          String targetModel = voiceItem.get("target_model").getAsString();

                          System.out.printf("- 音色: %s  创建时间: %s  模型: %s\n",
                                  voice, gmtCreate, targetModel);
                      }
                  }

              } catch (Exception e) {
                  e.printStackTrace();
              }
          }
      }
      ```
    </Tab>
  </Tabs>

### 删除音色 <span id="c2e73bd5335sz" />

删除指定音色，释放对应额度。

- <strong>URL</strong>

  华北2（北京）：

```http
POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization
```

新加坡：

```http
POST https://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api/v1/services/audio/tts/customization
```

- <strong>请求头</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25.02%" }} /><col style={{ width: "8.53%" }} /><col style={{ width: "10.3%" }} /><col style={{ width: "56.15%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>是否必须</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Authorization</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>鉴权令牌，格式为<code>Bearer \<your\_api\_key></code>，使用时，将“<code>\<your\_api\_key></code>”替换为实际的API Key。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>Content-Type</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>请求体中传输的数据的媒体类型。固定为<code>application/json</code>。</p></td></tr></tbody></table>
- <strong>消息体</strong>

  包含所有请求参数的消息体如下，对于可选字段，在实际业务中可根据需求省略：

  <Tip>
    `model`：声音复刻模型，固定为`qwen-voice-enrollment`，请勿修改。
  </Tip>

```json
{
    "model": "qwen-voice-enrollment",
    "input": {
        "action": "delete",
        "voice": "yourVoice"
    }
}
```

- <strong>请求参数</strong>

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "13.54%" }} /><col style={{ width: "10.86%" }} /><col style={{ width: "8.83%" }} /><col style={{ width: "10.28%" }} /><col style={{ width: "56.49%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>默认值</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>是否必须</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>model</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>-</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>声音复刻模型，固定为<code>qwen-voice-enrollment</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>action</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>-</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>操作类型，固定为<code>delete</code>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>voice</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>-</p></td><td style={{ verticalAlign: "top" }}><p>支持</p></td><td style={{ verticalAlign: "top" }}><p>待删除的音色。</p></td></tr></tbody></table>
- <strong>响应参数</strong>

  <Accordion title="点击查看响应示例">
    ```json
    {
        "usage": {
            "count": 0
        },
        "request_id": "yourRequestId"
    }
    ```
  </Accordion>

  需关注的参数如下：

  <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "14.71%" }} /><col style={{ width: "9.69%" }} /><col style={{ width: "75.6%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>request\_id</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>Request ID。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>count</p></td><td style={{ verticalAlign: "top" }}><p>integer</p></td><td style={{ verticalAlign: "top" }}><div><p>本次请求实际计入费用的“创建音色”次数，本次请求的费用为count × 0.01<span>元</span>。</p></div> <p>删除音色不计费，因此<code>count</code>恒为0。</p></td></tr></tbody></table>
- <strong>示例代码</strong>

  <Tip>
    `model`：声音复刻模型，固定为`qwen-voice-enrollment`，请勿修改。
  </Tip>

  <Tabs>
    <Tab title="cURL">
      若未将API Key配置到环境变量，需将示例中的`$DASHSCOPE_API_KEY`替换为实际的API Key。

      ```bash
      # ======= 重要提示 =======
      # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
      # 新加坡地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
      # === 执行时请删除该注释 ===

      curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization' \
      --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --header 'Content-Type: application/json' \
      --data '{
          "model": "qwen-voice-enrollment",
          "input": {
              "action": "delete",
              "voice": "yourVoice"
          }
      }'
      ```
    </Tab>

    <Tab title="Python">
      ```python expandable
      import os
      import requests

      # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
      # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key = "sk-xxx"
      api_key = os.getenv("DASHSCOPE_API_KEY")
      # 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
      url = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization"

      voice_to_delete = "yourVoice"  # 要删除的音色（替换为真实值）

      payload = {
          "model": "qwen-voice-enrollment", # 不要修改该值
          "input": {
              "action": "delete",
              "voice": voice_to_delete
          }
      }

      headers = {
          "Authorization": f"Bearer {api_key}",
          "Content-Type": "application/json"
      }

      response = requests.post(url, json=payload, headers=headers)

      print("HTTP 状态码:", response.status_code)

      if response.status_code == 200:
          data = response.json()
          request_id = data["request_id"]

          print(f"删除成功")
          print(f"Request ID: {request_id}")
      else:
          print("请求失败:", response.text)
      ```
    </Tab>

    <Tab title="Java">
      ```java expandable
      import com.google.gson.Gson;
      import com.google.gson.JsonObject;

      import java.io.BufferedReader;
      import java.io.InputStreamReader;
      import java.io.OutputStream;
      import java.net.HttpURLConnection;
      import java.net.URL;

      public class Main {
          public static void main(String[] args) {
              // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
              // 若没有配置环境变量，请用百炼API Key将下行替换为：String apiKey = "sk-xxx"
              String apiKey = System.getenv("DASHSCOPE_API_KEY");
              // 以下为华北2（北京）地域的URL。请将 {WorkspaceId} 替换为您的百炼业务空间ID，各地域的URL不同。
              String apiUrl = "https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/audio/tts/customization";
              String voiceToDelete = "yourVoice"; // 要删除的音色（替换为真实值）

              // 构造 JSON 请求体（字符串拼接，兼容 Java 8）
              String jsonPayload =
                      "{"
                              + "\"model\": \"qwen-voice-enrollment\"," // 不要修改该值
                              + "\"input\": {"
                              +     "\"action\": \"delete\","
                              +     "\"voice\": \"" + voiceToDelete + "\""
                              + "}"
                              + "}";

              try {
                  // 建立 POST 连接
                  HttpURLConnection con = (HttpURLConnection) new URL(apiUrl).openConnection();
                  con.setRequestMethod("POST");
                  con.setRequestProperty("Authorization", "Bearer " + apiKey);
                  con.setRequestProperty("Content-Type", "application/json");
                  con.setDoOutput(true);

                  // 发送请求体
                  try (OutputStream os = con.getOutputStream()) {
                      os.write(jsonPayload.getBytes("UTF-8"));
                  }

                  int status = con.getResponseCode();
                  BufferedReader br = new BufferedReader(new InputStreamReader(
                          status >= 200 && status < 300 ? con.getInputStream() : con.getErrorStream(), "UTF-8"));

                  StringBuilder response = new StringBuilder();
                  String line;
                  while ((line = br.readLine()) != null) {
                      response.append(line);
                  }
                  br.close();

                  System.out.println("HTTP 状态码: " + status);
                  System.out.println("返回 JSON: " + response.toString());

                  if (status == 200) {
                      Gson gson = new Gson();
                      JsonObject jsonObj = gson.fromJson(response.toString(), JsonObject.class);
                      String requestId = jsonObj.get("request_id").getAsString();

                      System.out.println("删除成功");
                      System.out.println("Request ID: " + requestId);
                  }

              } catch (Exception e) {
                  e.printStackTrace();
              }
          }
      }
      ```
    </Tab>
  </Tabs>

### 对话使用 <span id="da30eeebc4uwk" />

如何使用声音复刻生成的专属音色进行对话，请参见[快速开始：复刻与使用音色](/zh/model-studio/qwen-omni-voice-cloning#bb60d81324wsu)。

## 音色配额与自动清理规则 <span id="4bd111d07d16t" />

- <strong>总数限制</strong>：1000个音色/账号

  > 当前接口不提供音色数量查询功能，可通过调用[查询音色列表](/zh/model-studio/qwen-omni-voice-cloning#401d33226330i)接口自行统计音色数目
- <strong>自动清理</strong>：若单个音色在过去一年内未被用于任何模型调用请求，系统将自动将其删除

## 计费说明 <span id="b819f697047nb" />

声音复刻和模型调用分开计费：

- 声音复刻：[创建音色](/zh/model-studio/qwen-omni-voice-cloning#1eaa57d82did9)按0.01 元/个计费，创建失败不计费

  <Note>
    免费额度说明（仅中国站北京地域和国际站新加坡地域有免费额度）：

    - 阿里云百炼开通后90天内，可享1000次免费音色创建机会。
    - 创建失败不占用免费次数。
    - 删除音色不会恢复免费次数。
    - 免费额度用完或超出 90 天有效期后，创建音色将按0.01 元/个的价格计费。
  </Note>
- 使用复刻生成的专属音色进行对话：按模型调用的 token 用量计费，详情请参见[模型调用计费](/zh/model-studio/model-pricing)

## 版权与合法性 <span id="8d2900c951llr" />

您需对所提供声音的所有权及合法使用权负责，请注意阅读[服务协议](https://terms.alicdn.com/legal-agreement/terms/b_platform_service_agreement/20240229113512917/20240229113512917.html)。

## 录音操作指南 <span id="8d342f3949vge" />

### 录音设备 <span id="85abbb0177bib" />

推荐使用具备降噪功能的麦克风，或在安静环境下使用手机近距离录音，以保证音源纯净。

### 录音环境 <span id="3ec09d008dnbi" />

#### 场地 <span id="d79d5d756b9uv" />

- 建议在 10 平方米以内的小型封闭空间录音。
- 优先选择配有吸音材料（如吸音棉、地毯、窗帘）的房间。
- 避免空旷大厅、会议室、教室等高混响场所。

#### 噪音控制 <span id="55ab739cf81zm" />

- 室外噪音：关闭门窗，避免交通、施工等干扰。
- 室内噪音：关闭空调、风扇、日光灯镇流器等设备；可通过手机录制环境音并放大播放，识别潜在噪音源。

#### 混响控制 <span id="ff05de9b001zq" />

- 混响会导致声音模糊、清晰度下降。
- 减少光滑表面反射：拉上窗帘、打开衣柜门、铺放衣物或床单覆盖桌面/柜面。
- 利用不规则物体（如书架、软包家具）实现声波漫反射。

### 录音文案 <span id="b289f25224ira" />

- 文案内容灵活，建议与目标应用场景一致（例如，若用于客服场景，文案应为客服对话风格），但必须确保不包含任何敏感或非法词汇（如政治、色情、暴力相关内容），否则会导致复刻失败。
- 避免短句（如“你好”、“是的”），应使用完整句子。
- 保持语义连贯，朗读时避免频繁停顿（建议至少连续 3 秒无中断）。
- 可带入目标情绪（如亲切、严肃），但需避免过度夸张的戏剧化朗读，保持语调自然。

### 操作建议 <span id="51867fa4024fr" />

以普通卧室为例：

1. 关闭门窗，隔绝外部噪音。
2. 关闭空调、电扇等电器。
3. 拉上窗帘，减少玻璃反射。
4. 在桌面铺放衣物或毛毯，降低桌面反射。
5. 提前熟悉文案，设定角色语气，自然演绎。
6. 与录音设备保持约 10 厘米距离，避免喷麦或信号过弱。

## 错误信息 <span id="72adf7921b3zg" />

如遇报错问题，请参见[错误码](/zh/model-studio/error-code)进行排查。

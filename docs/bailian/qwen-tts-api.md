> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 非实时语音合成（Qwen-TTS）API参考

> 非实时语音合成（Qwen-TTS）API 的请求参数与返回字段说明。

<meta property="og:title" content="非实时语音合成（Qwen-TTS）API参考" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "47.89%" }} />

    <col style={{ width: "52.11%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ## 请求体 <span id="58f39efa90jby" />
      </td>

      <td rowSpan={3}>
        <Tabs>
          <Tab title="非流式输出">
            <Tabs>
              <Tab title="Python">
                > DashScope Python SDK中的`SpeechSynthesizer`接口已统一为`MultiModalConversation`，使用方法和参数保持完全一致。

                ```python
                # 请安装 DashScope SDK 的最新版本
                import os
                import dashscope

                # 以下为华北2（北京）地域的配置。
                dashscope.base_http_api_url = 'https://dashscope.aliyuncs.com/api/v1'

                text = "那我来给大家推荐一款T恤，这款呢真的是超级好看，这个颜色呢很显气质，而且呢也是搭配的绝佳单品，大家可以闭眼入，真的是非常好看，对身材的包容性也很好，不管啥身材的宝宝呢，穿上去都是很好看的。推荐宝宝们下单哦。"
                # SpeechSynthesizer接口使用方法：dashscope.audio.qwen_tts.SpeechSynthesizer.call(...)
                response = dashscope.MultiModalConversation.call(
                    model="qwen3-tts-flash",
                    # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx"
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    text=text,
                    voice="Cherry"
                )
                print(response)
                ```
              </Tab>

              <Tab title="Java">
                ```java expandable
                // 请安装 DashScope SDK 的最新版本
                import com.alibaba.dashscope.aigc.multimodalconversation.AudioParameters;
                import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
                import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
                import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import com.alibaba.dashscope.exception.UploadFileException;
                import com.alibaba.dashscope.utils.JsonUtils;
                import com.alibaba.dashscope.utils.Constants;

                public class Main {
                    private static final String MODEL = "qwen3-tts-flash";
                    public static void call() throws ApiException, NoApiKeyException, UploadFileException {
                        MultiModalConversation conv = new MultiModalConversation();
                        MultiModalConversationParam param = MultiModalConversationParam.builder()
                                .model(MODEL)
                                // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey("sk-xxx")
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .text("Today is a wonderful day to build something people love!")
                                .voice(AudioParameters.Voice.CHERRY)
                                .parameter("language_type", "English")
                                .build();
                        MultiModalConversationResult result = conv.call(param);
                        System.out.println(JsonUtils.toJson(result));
                    }
                    public static void main(String[] args) {
                        // 以下为华北2（北京）地域的配置。
                        Constants.baseHttpApiUrl = "https://dashscope.aliyuncs.com/api/v1";
                        try {
                            call();
                        } catch (ApiException | NoApiKeyException | UploadFileException e) {
                            System.out.println(e.getMessage());
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="curl">
                ```bash
                # ======= 重要提示 =======
                # 以下为华北2（北京）地域的配置。
                # 新加坡地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                # 若没有配置环境变量，请用阿里云百炼API Key将$DASHSCOPE_API_KEY替换为：sk-xxx。
                # === 执行时请删除该注释 ===

                curl -X POST 'https://dashscope.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -d '{
                    "model": "qwen3-tts-flash",
                    "input": {
                        "text": "那我来给大家推荐一款T恤，这款呢真的是超级好看，这个颜色呢很显气质，而且呢也是搭配的绝佳单品，大家可以闭眼入，真的是非常好看，对身材的包容性也很好，不管啥身材的宝宝呢，穿上去都是很好看的。推荐宝宝们下单哦。",
                        "voice": "Cherry",
                        "language_type": "Chinese"
                    }
                }'
                ```
              </Tab>
            </Tabs>
          </Tab>

          <Tab title="流式输出">
            <Tabs>
              <Tab title="Python">
                > DashScope Python SDK中的`SpeechSynthesizer`接口已统一为`MultiModalConversation`，使用新接口只需替换名称即可，其他参数完全兼容。

                ```python
                # DashScope SDK 版本不低于 1.24.5
                import os
                import dashscope

                # 以下为华北2（北京）地域的配置。
                dashscope.base_http_api_url = 'https://dashscope.aliyuncs.com/api/v1'

                text = "那我来给大家推荐一款T恤，这款呢真的是超级好看，这个颜色呢很显气质，而且呢也是搭配的绝佳单品，大家可以闭眼入，真的是非常好看，对身材的包容性也很好，不管啥身材的宝宝呢，穿上去都是很好看的。推荐宝宝们下单哦。"
                # SpeechSynthesizer接口使用方法：dashscope.audio.qwen_tts.SpeechSynthesizer.call(...)
                response = dashscope.MultiModalConversation.call(
                    model="qwen3-tts-flash",
                    # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                    # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx"
                    api_key=os.getenv("DASHSCOPE_API_KEY"),
                    text=text,
                    voice="Cherry",
                    stream=True
                )
                for chunk in response:
                    print(chunk)
                ```
              </Tab>

              <Tab title="Java">
                ```java expandable
                // DashScope SDK 版本需要不低于 2.19.0
                import com.alibaba.dashscope.aigc.multimodalconversation.AudioParameters;
                import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
                import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
                import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
                import com.alibaba.dashscope.exception.ApiException;
                import com.alibaba.dashscope.exception.NoApiKeyException;
                import com.alibaba.dashscope.exception.UploadFileException;
                import com.alibaba.dashscope.utils.JsonUtils;
                import com.alibaba.dashscope.utils.Constants;
                import io.reactivex.Flowable;

                public class Main {
                    private static final String MODEL = "qwen3-tts-flash";
                    public static void streamCall() throws ApiException, NoApiKeyException, UploadFileException {
                        MultiModalConversation conv = new MultiModalConversation();
                        MultiModalConversationParam param = MultiModalConversationParam.builder()
                                .model(MODEL)
                                // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey("sk-xxx")
                                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                                .text("Today is a wonderful day to build something people love!")
                                .voice(AudioParameters.Voice.CHERRY)
                                .parameter("language_type", "English")
                                .build();
                        Flowable<MultiModalConversationResult> result = conv.streamCall(param);
                        result.blockingForEach(r -> {System.out.println(JsonUtils.toJson(r));
                        });
                    }
                    public static void main(String[] args) {
                        // 以下为华北2（北京）地域的配置。
                        Constants.baseHttpApiUrl = "https://dashscope.aliyuncs.com/api/v1";
                        try {
                            streamCall();
                        } catch (ApiException | NoApiKeyException | UploadFileException e) {
                            System.out.println(e.getMessage());
                        }
                        System.exit(0);
                    }
                }
                ```
              </Tab>

              <Tab title="curl">
                ```bash
                # ======= 重要提示 =======
                # 以下为华北2（北京）地域的配置。
                # 新加坡地域和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                # 若没有配置环境变量，请用阿里云百炼API Key将$DASHSCOPE_API_KEY替换为：sk-xxx。
                # === 执行时请删除该注释 ===

                curl -X POST 'https://dashscope.aliyuncs.com/api/v1/services/aigc/multimodal-generation/generation' \
                -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
                -H 'Content-Type: application/json' \
                -H 'X-DashScope-SSE: enable' \
                -d '{
                    "model": "qwen3-tts-flash",
                    "input": {
                        "text": "那我来给大家推荐一款T恤，这款呢真的是超级好看，这个颜色呢很显气质，而且呢也是搭配的绝佳单品，大家可以闭眼入，真的是非常好看，对身材的包容性也很好，不管啥身材的宝宝呢，穿上去都是很好看的。推荐宝宝们下单哦。",
                        "voice": "Cherry",
                        "language_type": "Chinese"
                    }
                }'
                ```
              </Tab>
            </Tabs>

            实时播放 Base64 音频的方法请参见[流式播放](/zh/model-studio/qwen-tts-api#qwen-tts-streaming-playback)。
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td>
        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称，详情请参见[支持的模型](/zh/model-studio/non-realtime-tts-user-guide#4a43cc1bb7kxg)。
      </td>
    </tr>

    <tr>
      <td>
        <strong>input</strong>`object`<strong>（必选）</strong>

        输入参数<strong>。</strong>

        <Accordion title="属性" defaultOpen>
          <strong>text</strong> `string` <strong>（必选）</strong>

          要合成的文本，支持多语种混合输入。最大输入长度为 600 字符。

          <strong>voice</strong> `string` <strong>（必选）</strong>

          使用的音色，参见[支持的系统音色](/zh/model-studio/non-realtime-tts-user-guide#bac280ddf5a1u)。

          <strong>language\_type</strong> `string` （可选）

          合成音频的语种。默认为 `Auto`。

          - `Auto`：适用于文本包含多种语言或语种不确定的场景。模型自动为不同语言片段匹配发音，但无法保证完全精准。
          - 指定语种：适用于单一语种文本。指定具体语种能显著提升合成质量，效果通常优于 `Auto`。可选值：

            - `Chinese`
            - `English`
            - `German`
            - `Italian`
            - `Portuguese`
            - `Spanish`
            - `Japanese`
            - `Korean`
            - `French`
            - `Russian`
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "47.82%" }} />

    <col style={{ width: "52.18%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        ## 返回对象（流式与非流式输出格式一致） <span id="cd3063363egdc" />
      </td>

      <td rowSpan={7}>
        <CodeGroup>
          ```json 千问3-TTS-Flash
          {
              "status_code": 200,
              "request_id": "5c63c65c-cad8-4bf4-959d-xxxxxxxxxxxx",
              "code": "",
              "message": "",
              "output": {
                  "text": null,
                  "finish_reason": "stop",
                  "choices": null,
                  "audio": {
                      "data": "",
                      "url": "http://dashscope-result-bj.oss-cn-beijing.aliyuncs.com/1d/ab/20251218/d2033070/39b6d8f2-c0db-4daa-9073-5d27bfb66b78.wav?Expires=1766113409&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE",
                      "id": "audio_5c63c65c-cad8-4bf4-959d-xxxxxxxxxxxx",
                      "expires_at": 1766113409
                  }
              },
              "usage": {
                  "input_tokens": 0,
                  "output_tokens": 0,
                  "characters": 195
              }
          }
          ```
        </CodeGroup>
      </td>
    </tr>

    <tr>
      <td>
        <strong>status\_code</strong> `integer`

        HTTP状态码。遵循 [RFC 9110](https://www.rfc-editor.org/rfc/rfc9110.html#name-status-codes)标准定义。例如：<br />• `200`：请求成功，正常返回结果<br />• `400`：客户端请求参数错误<br />• `401`：未授权访问<br />• `404`：资源未找到<br />• `500`：服务器内部错误。<br />
      </td>
    </tr>

    <tr>
      <td>
        <strong>request\_id</strong> `string`

        本次请求的唯一标识，可用于问题排查。
      </td>
    </tr>

    <tr>
      <td>
        <strong>code</strong> `string`

        请求失败时展示错误码（参见[错误码](/zh/model-studio/error-code)）。
      </td>
    </tr>

    <tr>
      <td>
        <strong>message</strong> `string`

        请求失败时展示错误信息（参见[错误码](/zh/model-studio/error-code)）。
      </td>
    </tr>

    <tr>
      <td>
        <strong>output</strong>`object`

        模型的输出。

        <Accordion title="属性" defaultOpen>
          <strong>text</strong>`string`

          始终为null，无需关注该参数。

          <strong>choices</strong>`string`

          始终为null，无需关注该参数。

          <strong>finish\_reason</strong>`string`

          生成状态标识：

          - 正在生成时为"null"；
          - 模型输出自然结束或触发了停止条件时为 "stop"。

          <strong>audio</strong> `object`

          模型输出的音频信息。

          <Accordion title="属性" defaultOpen>
            <strong>url</strong> `string`

            完整音频文件的 URL，有效期 24 小时。

            <Note>
              流式输出说明：流式模式下，API 返回多个 chunk。中间 chunk 的 audio.data 包含 Base64 编码的音频片段，audio.url 为空；最后一个 chunk 的 audio.data 为空字符串，audio.url 包含完整音频文件的 <strong>OSS</strong> 地址。开发者可在最后一个 chunk 中通过 url 字段下载完整音频。非流式模式下，该字段直接返回完整音频文件 URL。
            </Note>

            <strong>data</strong> `string`

            Base64 编码的音频数据。非流式输出和流式输出的中间 chunk 中返回 Base64 编码的音频片段；流式输出的最后一个 chunk 中该字段为空字符串，音频通过同级的 url 字段获取。

            <strong>id</strong> `string`

            音频的唯一标识。

            <strong>expires\_at</strong> `integer`

            URL 过期时间的 UNIX 时间戳。
          </Accordion>
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>usage</strong> `object`

        本次请求的字符消耗信息。

        <Accordion title="属性" defaultOpen>
          <strong>output\_tokens</strong> `integer`

          输出音频的 Token 消耗量。对于千问3-TTS-Flash模型，该字段固定为0。

          <strong>input\_tokens</strong> `integer`

          输入文本的 Token 消耗量。对于千问3-TTS-Flash模型，该字段固定为0。

          <strong>characters</strong> `integer`

          输入文本的字符数。仅千问3-TTS-Flash模型返回该字段。
        </Accordion>
      </td>
    </tr>

    <tr>
      <td>
        <strong>request\_id</strong> `string`

        本次请求的 ID。
      </td>

      <td />
    </tr>
  </tbody>
</table>

## 音频下载与播放 <span id="qwen-tts-audio-examples" />

运行示例前，请先[获取并配置 API Key](/zh/model-studio/get-api-key)，并[安装 DashScope SDK](/zh/model-studio/install-sdk)。Python 播放示例还依赖 PyAudio 和 NumPy；PyAudio 的安装方式见代码注释，NumPy 可通过 `pip install numpy` 安装。

### 下载音频（Java） <span id="qwen-tts-audio-download" />

需要导入 Gson 依赖，Maven 或 Gradle 添加方式如下：

<Tabs>
  <Tab title="Maven">
    在`pom.xml`中添加：

    ```xml
    <!-- https://mvnrepository.com/artifact/com.google.code.gson/gson -->
    <dependency>
        <groupId>com.google.code.gson</groupId>
        <artifactId>gson</artifactId>
        <version>2.13.1</version>
    </dependency>
    ```
  </Tab>

  <Tab title="Gradle">
    在`build.gradle`中添加：

    ```gradle
    // https://mvnrepository.com/artifact/com.google.code.gson/gson
    implementation("com.google.code.gson:gson:2.13.1")
    ```
  </Tab>
</Tabs>

```java expandable
import com.alibaba.dashscope.aigc.multimodalconversation.AudioParameters;
import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
import com.alibaba.dashscope.exception.ApiException;
import com.alibaba.dashscope.exception.NoApiKeyException;
import com.alibaba.dashscope.exception.UploadFileException;
import com.alibaba.dashscope.utils.Constants;
import java.io.FileOutputStream;
import java.io.InputStream;
import java.net.URL;

public class Main {
    private static final String MODEL = "qwen3-tts-flash";
    public static void call() throws ApiException, NoApiKeyException, UploadFileException {
        MultiModalConversation conv = new MultiModalConversation();
        MultiModalConversationParam param = MultiModalConversationParam.builder()
                // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                .model(MODEL)
                .text("Today is a wonderful day to build something people love!")
                .voice(AudioParameters.Voice.CHERRY)
                .parameter("language_type", "English") // 建议与文本语种一致，以获得正确的发音和自然的语调。
                .build();
        MultiModalConversationResult result = conv.call(param);
        String audioUrl = result.getOutput().getAudio().getUrl();
        System.out.print(audioUrl);

        // 下载音频文件到本地
        try (InputStream in = new URL(audioUrl).openStream();
             FileOutputStream out = new FileOutputStream("downloaded_audio.wav")) {
            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = in.read(buffer)) != -1) {
                out.write(buffer, 0, bytesRead);
            }
            System.out.println("\n音频文件已下载到本地：downloaded_audio.wav");
        } catch (Exception e) {
            System.out.println("\n下载音频文件时出错：" + e.getMessage());
        }
    }
    public static void main(String[] args) {
        try {
            // 以下为华北2（北京）地域的配置。
            Constants.baseHttpApiUrl = "https://dashscope.aliyuncs.com/api/v1";
            call();
        } catch (ApiException | NoApiKeyException | UploadFileException e) {
            System.out.println(e.getMessage());
        }
        System.exit(0);
    }
}
```

### 流式播放 <span id="qwen-tts-streaming-playback" />

<Tabs>
  <Tab title="Python">
    ```python expandable
    # coding=utf-8
    #
    # Installation instructions for pyaudio:
    # APPLE Mac OS X
    #   brew install portaudio
    #   pip install pyaudio
    # Debian/Ubuntu
    #   sudo apt-get install python-pyaudio python3-pyaudio
    #   or
    #   pip install pyaudio
    # CentOS
    #   sudo yum install -y portaudio portaudio-devel && pip install pyaudio
    # Microsoft Windows
    #   python -m pip install pyaudio

    import os
    import dashscope
    import pyaudio
    import time
    import base64
    import numpy as np

    # 以下为华北2（北京）地域的配置。
    dashscope.base_http_api_url = 'https://dashscope.aliyuncs.com/api/v1'

    p = pyaudio.PyAudio()
    # 创建音频流
    stream = p.open(format=pyaudio.paInt16,
                    channels=1,
                    rate=24000,
                    output=True)

    text = "你好啊，我是千问"
    response = dashscope.MultiModalConversation.call(
        # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key = "sk-xxx"
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        model="qwen3-tts-flash",
        text=text,
        voice="Cherry",
        language_type="Chinese",  # 建议与文本语种一致，以获得正确的发音和自然的语调。
        stream=True
    )

    for chunk in response:
        if chunk.output is not None:
          audio = chunk.output.audio
          if audio.data is not None:
              wav_bytes = base64.b64decode(audio.data)
              audio_np = np.frombuffer(wav_bytes, dtype=np.int16)
              # 直接播放音频数据
              stream.write(audio_np.tobytes())
          if chunk.output.finish_reason == "stop":
              print(f"finish at: {chunk.output.audio.expires_at}")
    time.sleep(0.8)
    # 清理资源
    stream.stop_stream()
    stream.close()
    p.terminate()
    ```
  </Tab>

  <Tab title="Java">
    需要导入 Gson 依赖，Maven 或 Gradle 添加方式如下：

    <Tabs>
      <Tab title="Maven">
        在`pom.xml`中添加：

        ```xml
        <!-- https://mvnrepository.com/artifact/com.google.code.gson/gson -->
        <dependency>
            <groupId>com.google.code.gson</groupId>
            <artifactId>gson</artifactId>
            <version>2.13.1</version>
        </dependency>
        ```
      </Tab>

      <Tab title="Gradle">
        在`build.gradle`中添加：

        ```gradle
        // https://mvnrepository.com/artifact/com.google.code.gson/gson
        implementation("com.google.code.gson:gson:2.13.1")
        ```
      </Tab>
    </Tabs>

    ```java expandable
    import com.alibaba.dashscope.aigc.multimodalconversation.AudioParameters;
    import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversation;
    import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationParam;
    import com.alibaba.dashscope.aigc.multimodalconversation.MultiModalConversationResult;
    import com.alibaba.dashscope.exception.ApiException;
    import com.alibaba.dashscope.exception.NoApiKeyException;
    import com.alibaba.dashscope.exception.UploadFileException;
    import com.alibaba.dashscope.utils.Constants;
    import io.reactivex.Flowable;
    import javax.sound.sampled.*;
    import java.util.Base64;

    public class Main {
        private static final String MODEL = "qwen3-tts-flash";
        public static void streamCall() throws ApiException, NoApiKeyException, UploadFileException {
            MultiModalConversation conv = new MultiModalConversation();
            MultiModalConversationParam param = MultiModalConversationParam.builder()
                    // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                    // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：.apiKey("sk-xxx")
                    .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                    .model(MODEL)
                    .text("Today is a wonderful day to build something people love!")
                    .voice(AudioParameters.Voice.CHERRY)
                    .parameter("language_type", "English") // 建议与文本语种一致，以获得正确的发音和自然的语调。
                    .build();
            Flowable<MultiModalConversationResult> result = conv.streamCall(param);
            result.blockingForEach(r -> {
                try {
                    // 1. 获取Base64编码的音频数据
                    String base64Data = r.getOutput().getAudio().getData();
                    byte[] audioBytes = Base64.getDecoder().decode(base64Data);

                    // 2. 配置音频格式（根据API返回的音频格式调整）
                    AudioFormat format = new AudioFormat(
                            AudioFormat.Encoding.PCM_SIGNED,
                            24000, // 采样率（需与API返回格式一致）
                            16,    // 采样位数
                            1,     // 声道数
                            2,     // 帧大小（字节数）
                            24000, // 帧率（需与采样率一致）
                            false  // 大端序
                    );

                    // 3. 实时播放音频数据
                    DataLine.Info info = new DataLine.Info(SourceDataLine.class, format);
                    try (SourceDataLine line = (SourceDataLine) AudioSystem.getLine(info)) {
                        if (line != null) {
                            line.open(format);
                            line.start();
                            line.write(audioBytes, 0, audioBytes.length);
                            line.drain();
                        }
                    }
                } catch (LineUnavailableException e) {
                    e.printStackTrace();
                }
            });
        }
        public static void main(String[] args) {
            // 以下为华北2（北京）地域的配置。
            Constants.baseHttpApiUrl = "https://dashscope.aliyuncs.com/api/v1";
            try {
                streamCall();
            } catch (ApiException | NoApiKeyException | UploadFileException e) {
                System.out.println(e.getMessage());
            }
            System.exit(0);
        }
    }
    ```
  </Tab>
</Tabs>

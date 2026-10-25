> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# WebSocket 接入指南

> 本文介绍通过WebSocket连接访问CosyVoice实时语音合成服务的交互流程、接口地址和请求头。

<meta property="og:title" content="WebSocket 接入指南" />

## 接口地址 <span id="07bfd546805pn" />

WebSocket 接口使用 `wss://` 协议，地址如下：

<Tabs>
  <Tab title="华北2（北京）">
    `wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>

  <Tab title="新加坡">
    `wss://{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com/api-ws/v1/inference`

    调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。
  </Tab>
</Tabs>

<Tip>
  阿里云百炼为华北2（北京）、新加坡地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议迁移至新域名：

  - 华北2（北京）地域：从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`
  - 新加坡地域：从 `dashscope-intl.aliyuncs.com` 迁移至 `{WorkspaceId}.ap-southeast-1.maas.aliyuncs.com`
</Tip>

## 请求头 <span id="b02603aacf7e9" />

请求头中需添加如下信息：

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "22%" }} />

    <col style={{ width: "14%" }} />

    <col style={{ width: "10%" }} />

    <col style={{ width: "54%" }} />
  </colgroup>

  <thead>
    <tr>
      <th style={{ verticalAlign: "top" }}>
        <span><strong>参数</strong></span>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <span><strong>类型</strong></span>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <span><strong>是否必选</strong></span>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <span><strong>说明</strong></span>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>Authorization</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <code>string</code>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>是</span>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>鉴权令牌，格式为 <code>Bearer \<your\_api\_key></code>，将 <code>\<your\_api\_key></code> 替换为实际的 API Key。</span>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>user-agent</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <code>string</code>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>否</span>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>客户端标识，便于服务端追踪来源。</span>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>X-DashScope-WorkSpace</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <code>string</code>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>否</span>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>阿里云百炼<Link href="/zh/model-studio/use-workspace">业务空间ID</Link>。</span>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>X-DashScope-DataInspection</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <code>string</code>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>否</span>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <span>是否启用数据合规检测功能。默认不传或设为<code>enable</code>。如非必要，请勿启用该参数。</span>
      </td>
    </tr>
  </tbody>
</table>

<Tip>
  Authorization 鉴权在 WebSocket 握手阶段验证。如果 API Key 无效或缺失，握手将失败并返回 HTTP 401/403 错误。
</Tip>

## 交互流程 <span id="62149ca39bx2i" />

<img src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/7962835871/CAEQaxiBgID50pCW3hkiIDVlOWNkODdhOGYyYjQ2ZDFiMzgyYjNmMmUzOGZkNGVh4709861_20241015153444.149.svg" alt="image" width="560" />

客户端事件和服务端事件的详细说明，请参见[客户端事件](/zh/model-studio/cosyvoice-client-events)和[服务端事件](/zh/model-studio/cosyvoice-server-events)。

按时间顺序，客户端与服务端的交互流程如下：

1. 建立连接：客户端与服务端建立WebSocket连接。
2. 开启任务：客户端发送run-task事件以开启任务。
3. 等待确认：客户端收到服务端返回的task-started事件，标志着任务已成功开启，可以进行后续步骤。
4. 发送待合成文本：

   客户端按顺序向服务端发送一个或多个包含待合成文本的continue-task事件，服务端接收到完整语句后返回result-generated事件和音频流（文本长度有约束， 详情参见continue-task事件中`text`字段描述）。

   <Note>
     支持多次发送continue-task事件，按顺序提交文本片段。服务端接收文本片段后自动进行分句：

     - 完整语句立即合成，此时客户端能够接收到服务端返回的音频
     - 不完整语句缓存至完整后合成，语句不完整时服务端不返回音频

     当发送finish-task事件时，服务端会强制合成所有缓存内容。
   </Note>
5. 接收音频：通过 `binary` 通道接收音频流
6. 通知服务端结束任务：

   待文本发送完毕后，客户端发送finish-task事件通知服务端结束任务，并继续接收服务端返回的音频流。此步骤不可省略，否则可能导致语音数据不完整。
7. 任务结束：

   客户端收到服务端返回的task-finished事件，标志着任务结束。
8. 关闭连接：客户端关闭WebSocket连接。

为提高资源利用率，建议复用 WebSocket 连接处理多个任务，而非为每个任务建立新连接。

<Tip>
  同一次合成任务中，run-task、所有 continue-task、finish-task 必须使用相同的 `task_id`。每次发起新任务时生成新的 task\_id（如使用 UUID）。使用不同 task\_id 会导致音频错乱或任务失败。
</Tip>

## 示例代码 <span id="ws-examples" />

以下示例使用通用 WebSocket 库直接发送协议事件，使用 `cosyvoice-v3-flash` 和音色 `longanyang`。先[获取 API Key](/zh/model-studio/get-api-key)并配置 `DASHSCOPE_API_KEY` 环境变量，将 `{WorkspaceId}` 替换为对应地域的业务空间 ID；使用其他地域时同时替换接口地址和 API Key。

<Tabs>
  <Tab title="Java">
    建议使用 Java DashScope SDK 进行开发，请参见[CosyVoice Java SDK](/zh/model-studio/cosyvoice-java-sdk)。

    以下是 Java WebSocket 直连示例，运行前请导入以下依赖：

    - `Java-WebSocket`
    - `jackson-databind`

    推荐使用Maven或Gradle管理依赖包，其配置如下：

    <CodeGroup dropdown>
      ```xml pom.xml
      <dependencies>
          <!-- WebSocket Client -->
          <dependency>
              <groupId>org.java-websocket</groupId>
              <artifactId>Java-WebSocket</artifactId>
              <version>1.5.3</version>
          </dependency>

          <!-- JSON Processing -->
          <dependency>
              <groupId>com.fasterxml.jackson.core</groupId>
              <artifactId>jackson-databind</artifactId>
              <version>2.13.0</version>
          </dependency>
      </dependencies>
      ```

      ```gradle build.gradle
      // 省略其它代码
      dependencies {
        // WebSocket Client
        implementation 'org.java-websocket:Java-WebSocket:1.5.3'
        // JSON Processing
        implementation 'com.fasterxml.jackson.core:jackson-databind:2.13.0'
      }
      // 省略其它代码
      ```
    </CodeGroup>

    Java代码如下：

    ```java expandable
    import com.fasterxml.jackson.databind.ObjectMapper;

    import org.java_websocket.client.WebSocketClient;
    import org.java_websocket.handshake.ServerHandshake;

    import java.io.FileOutputStream;
    import java.io.IOException;
    import java.net.URI;
    import java.nio.ByteBuffer;
    import java.util.*;

    public class TTSWebSocketClient extends WebSocketClient {
        private final String taskId = UUID.randomUUID().toString();
        private final String outputFile = "output_" + System.currentTimeMillis() + ".mp3";
        private boolean taskFinished = false;

        public TTSWebSocketClient(URI serverUri, Map<String, String> headers) {
            super(serverUri, headers);
        }

        @Override
        public void onOpen(ServerHandshake serverHandshake) {
            System.out.println("连接成功");

            // 发送run-task事件
            // 如果enable_ssml设为true，只允许发送一次continue-task事件，否则会报错“Text request limit violated, expected 1.”
            String runTaskCommand = "{ \"header\": { \"action\": \"run-task\", \"task_id\": \"" + taskId + "\", \"streaming\": \"duplex\" }, \"payload\": { \"task_group\": \"audio\", \"task\": \"tts\", \"function\": \"SpeechSynthesizer\", \"model\": \"cosyvoice-v3-flash\", \"parameters\": { \"text_type\": \"PlainText\", \"voice\": \"longanyang\", \"format\": \"mp3\", \"sample_rate\": 22050, \"volume\": 50, \"rate\": 1, \"pitch\": 1, \"enable_ssml\": false }, \"input\": {} }}";
            send(runTaskCommand);
        }

        @Override
        public void onMessage(String message) {
            System.out.println("收到服务端返回的消息：" + message);
            try {
                // Parse JSON message
                Map<String, Object> messageMap = new ObjectMapper().readValue(message, Map.class);

                if (messageMap.containsKey("header")) {
                    Map<String, Object> header = (Map<String, Object>) messageMap.get("header");

                    if (header.containsKey("event")) {
                        String event = (String) header.get("event");

                        if ("task-started".equals(event)) {
                            System.out.println("收到服务端返回的task-started事件");

                            List<String> texts = Arrays.asList(
                                    "床前明月光，疑是地上霜",
                                    "举头望明月，低头思故乡"
                            );

                            for (String text : texts) {
                                // 发送continue-task事件
                                sendContinueTask(text);
                            }

                            // 发送finish-task事件
                            sendFinishTask();
                        } else if ("task-finished".equals(event)) {
                            System.out.println("收到服务端返回的task-finished事件");
                            taskFinished = true;
                            closeConnection();
                        } else if ("task-failed".equals(event)) {
                            System.out.println("任务失败：" + message);
                            closeConnection();
                        }
                    }
                }
            } catch (Exception e) {
                System.err.println("出现异常：" + e.getMessage());
            }
        }

        @Override
        public void onMessage(ByteBuffer message) {
            System.out.println("收到的二进制音频数据大小为：" + message.remaining());

            try (FileOutputStream fos = new FileOutputStream(outputFile, true)) {
                byte[] buffer = new byte[message.remaining()];
                message.get(buffer);
                fos.write(buffer);
                System.out.println("音频数据已写入本地文件" + outputFile + "中");
            } catch (IOException e) {
                System.err.println("音频数据写入本地文件失败：" + e.getMessage());
            }
        }

        @Override
        public void onClose(int code, String reason, boolean remote) {
            System.out.println("连接关闭：" + reason + " (" + code + ")");
        }

        @Override
        public void onError(Exception ex) {
            System.err.println("报错：" + ex.getMessage());
            ex.printStackTrace();
        }

        private void sendContinueTask(String text) {
            String command = "{ \"header\": { \"action\": \"continue-task\", \"task_id\": \"" + taskId + "\", \"streaming\": \"duplex\" }, \"payload\": { \"input\": { \"text\": \"" + text + "\" } }}";
            send(command);
        }

        private void sendFinishTask() {
            String command = "{ \"header\": { \"action\": \"finish-task\", \"task_id\": \"" + taskId + "\", \"streaming\": \"duplex\" }, \"payload\": { \"input\": {} }}";
            send(command);
        }

        private void closeConnection() {
            if (!isClosed()) {
                close();
            }
        }

        public static void main(String[] args) {
            try {
                // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
                // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：String apiKey = "sk-xxx"
                String apiKey = System.getenv("DASHSCOPE_API_KEY");
                if (apiKey == null || apiKey.isEmpty()) {
                    System.err.println("请设置 DASHSCOPE_API_KEY 环境变量");
                    return;
                }

                Map<String, String> headers = new HashMap<>();
                headers.put("Authorization", "bearer " + apiKey);
                // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
                TTSWebSocketClient client = new TTSWebSocketClient(new URI("wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference"), headers);

                client.connect();

                while (!client.isClosed() && !client.taskFinished) {
                    Thread.sleep(1000);
                }
            } catch (Exception e) {
                System.err.println("连接WebSocket服务失败：" + e.getMessage());
                e.printStackTrace();
            }
        }
    }
    ```
  </Tab>

  <Tab title="Python">
    建议使用 Python DashScope SDK 进行开发，请参见[CosyVoice Python SDK](/zh/model-studio/cosyvoice-python-sdk)。

    以下是 Python WebSocket 直连示例，运行前请导入以下依赖：

    ```bash
    pip uninstall websocket-client
    pip uninstall websocket
    pip install websocket-client
    ```

    <Tip>
      请不要将运行示例代码的Python文件命名为“websocket.py”，否则会报错（AttributeError: module 'websocket' has no attribute 'WebSocketApp'. Did you mean: 'WebSocket'?）。
    </Tip>

    ```python expandable
    import websocket
    import json
    import uuid
    import os
    import time

    class TTSClient:
        def __init__(self, api_key, uri):
            """
        初始化 TTSClient 实例

        参数:
            api_key (str): 鉴权用的 API Key
            uri (str): WebSocket 服务地址
        """
            self.api_key = api_key  # 替换为你的 API Key
            self.uri = uri  # 替换为你的 WebSocket 地址
            self.task_id = str(uuid.uuid4())  # 生成唯一任务 ID
            self.output_file = f"output_{int(time.time())}.mp3"  # 输出音频文件路径
            self.ws = None  # WebSocketApp 实例
            self.task_started = False  # 是否收到 task-started
            self.task_finished = False  # 是否收到 task-finished / task-failed

        def on_open(self, ws):
            """
        WebSocket 连接建立时回调函数
        发送 run-task 事件开启语音合成任务
        """
            print("WebSocket 已连接")

            # 构造 run-task 事件
            run_task_cmd = {
                "header": {
                    "action": "run-task",
                    "task_id": self.task_id,
                    "streaming": "duplex"
                },
                "payload": {
                    "task_group": "audio",
                    "task": "tts",
                    "function": "SpeechSynthesizer",
                    "model": "cosyvoice-v3-flash",
                    "parameters": {
                        "text_type": "PlainText",
                        "voice": "longanyang",
                        "format": "mp3",
                        "sample_rate": 22050,
                        "volume": 50,
                        "rate": 1,
                        "pitch": 1,
                        # 如果enable_ssml设为True，只允许发送一次continue-task事件，否则会报错“Text request limit violated, expected 1.”
                        "enable_ssml": False
                    },
                    "input": {}
                }
            }

            # 发送 run-task 事件
            ws.send(json.dumps(run_task_cmd))
            print("已发送 run-task 事件")

        def on_message(self, ws, message):
            """
        接收到消息时的回调函数
        区分文本和二进制消息处理
        """
            if isinstance(message, str):
                # 处理 JSON 文本消息
                try:
                    msg_json = json.loads(message)
                    print(f"收到 JSON 消息: {msg_json}")

                    if "header" in msg_json:
                        header = msg_json["header"]

                        if "event" in header:
                            event = header["event"]

                            if event == "task-started":
                                print("任务已启动")
                                self.task_started = True

                                # 发送 continue-task 事件
                                texts = [
                                    "床前明月光，疑是地上霜",
                                    "举头望明月，低头思故乡"
                                ]

                                for text in texts:
                                    self.send_continue_task(text)

                                # 所有 continue-task 发送完成后发送 finish-task
                                self.send_finish_task()

                            elif event == "task-finished":
                                print("任务已完成")
                                self.task_finished = True
                                self.close(ws)

                            elif event == "task-failed":
                                error_msg = msg_json.get("header", {}).get("error_message", "未知错误")
                                print(f"任务失败: {error_msg}")
                                self.task_finished = True
                                self.close(ws)

                except json.JSONDecodeError as e:
                    print(f"JSON 解析失败: {e}")
            else:
                # 处理二进制消息（音频数据）
                print(f"收到二进制消息，大小: {len(message)} 字节")
                with open(self.output_file, "ab") as f:
                    f.write(message)
                print(f"已将音频数据写入本地文件{self.output_file}中")

        def on_error(self, ws, error):
            """发生错误时的回调"""
            print(f"WebSocket 出错: {error}")

        def on_close(self, ws, close_status_code, close_msg):
            """连接关闭时的回调"""
            print(f"WebSocket 已关闭: {close_msg} ({close_status_code})")

        def send_continue_task(self, text):
            """发送 continue-task 事件，附带要合成的文本内容"""
            cmd = {
                "header": {
                    "action": "continue-task",
                    "task_id": self.task_id,
                    "streaming": "duplex"
                },
                "payload": {
                    "input": {
                        "text": text
                    }
                }
            }

            self.ws.send(json.dumps(cmd))
            print(f"已发送 continue-task 事件，文本内容: {text}")

        def send_finish_task(self):
            """发送 finish-task 事件，结束语音合成任务"""
            cmd = {
                "header": {
                    "action": "finish-task",
                    "task_id": self.task_id,
                    "streaming": "duplex"
                },
                "payload": {
                    "input": {}
                }
            }

            self.ws.send(json.dumps(cmd))
            print("已发送 finish-task 事件")

        def close(self, ws):
            """主动关闭连接"""
            if ws and ws.sock and ws.sock.connected:
                ws.close()
                print("已主动关闭连接")

        def run(self):
            """启动 WebSocket 客户端"""
            # 设置请求头部（鉴权）
            header = {
                "Authorization": f"bearer {self.api_key}",
                "X-DashScope-DataInspection": "enable"
            }

            # 创建 WebSocketApp 实例
            self.ws = websocket.WebSocketApp(
                self.uri,
                header=header,
                on_open=self.on_open,
                on_message=self.on_message,
                on_error=self.on_error,
                on_close=self.on_close
            )

            print("正在监听 WebSocket 消息...")
            self.ws.run_forever()  # 启动长连接监听

    # 示例使用方式
    if __name__ == "__main__":
        # 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：API_KEY = "sk-xxx"
        API_KEY = os.environ.get("DASHSCOPE_API_KEY")
        # 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
        SERVER_URI = "wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference"  # 替换为你的 WebSocket 地址

        client = TTSClient(API_KEY, SERVER_URI)
        client.run()
    ```
  </Tab>

  <Tab title="Go">
    ```bash
    go mod init tts-example
    go get github.com/gorilla/websocket@v1.5.3 github.com/google/uuid@v1.6.0
    ```

    ```go expandable
    package main

    import (
    	"encoding/json"
    	"fmt"
    	"net/http"
    	"os"
    	"strings"
    	"time"

    	"github.com/google/uuid"
    	"github.com/gorilla/websocket"
    )

    const (
    	// 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
    	wsURL      = "wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference"
    	outputFile = "output.mp3"
    )

    func main() {
    	// 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    	// 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：apiKey := "sk-xxx"
    	apiKey := os.Getenv("DASHSCOPE_API_KEY")

    	// 清空输出文件
    	os.Remove(outputFile)
    	os.Create(outputFile)

    	// 连接WebSocket
    	header := make(http.Header)
    	header.Add("X-DashScope-DataInspection", "enable")
    	header.Add("Authorization", fmt.Sprintf("bearer %s", apiKey))

    	conn, resp, err := websocket.DefaultDialer.Dial(wsURL, header)
    	if err != nil {
    		if resp != nil {
    			fmt.Printf("连接失败 HTTP状态码: %d\n", resp.StatusCode)
    		}
    		fmt.Println("连接失败:", err)
    		return
    	}
    	defer conn.Close()

    	// 生成任务ID
    	taskID := uuid.New().String()
    	fmt.Printf("生成任务ID: %s\n", taskID)

    	// 发送run-task事件
    	runTaskCmd := map[string]interface{}{
    		"header": map[string]interface{}{
    			"action":    "run-task",
    			"task_id":   taskID,
    			"streaming": "duplex",
    		},
    		"payload": map[string]interface{}{
    			"task_group": "audio",
    			"task":       "tts",
    			"function":   "SpeechSynthesizer",
    			"model":      "cosyvoice-v3-flash",
    			"parameters": map[string]interface{}{
    				"text_type":   "PlainText",
    				"voice":       "longanyang",
    				"format":      "mp3",
    				"sample_rate": 22050,
    				"volume":      50,
    				"rate":        1,
    				"pitch":       1,
    				// 如果enable_ssml设为true，只允许发送一次continue-task事件，否则会报错“Text request limit violated, expected 1.”
    				"enable_ssml": false,
    			},
    			"input": map[string]interface{}{},
    		},
    	}

    	runTaskJSON, _ := json.Marshal(runTaskCmd)
    	fmt.Printf("发送run-task事件: %s\n", string(runTaskJSON))

    	err = conn.WriteMessage(websocket.TextMessage, runTaskJSON)
    	if err != nil {
    		fmt.Println("发送run-task失败:", err)
    		return
    	}

    	textSent := false

    	// 处理消息
    	for {
    		messageType, message, err := conn.ReadMessage()
    		if err != nil {
    			fmt.Println("读取消息失败:", err)
    			break
    		}

    		// 处理二进制消息
    		if messageType == websocket.BinaryMessage {
    			fmt.Printf("收到二进制消息，长度: %d\n", len(message))
    			file, _ := os.OpenFile(outputFile, os.O_APPEND|os.O_WRONLY|os.O_CREATE, 0644)
    			file.Write(message)
    			file.Close()
    			continue
    		}

    		// 处理文本消息
    		messageStr := string(message)
    		fmt.Printf("收到文本消息: %s\n", strings.ReplaceAll(messageStr, "\n", ""))

    		// 简单解析JSON获取event类型
    		var msgMap map[string]interface{}
    		if json.Unmarshal(message, &msgMap) == nil {
    			if header, ok := msgMap["header"].(map[string]interface{}); ok {
    				if event, ok := header["event"].(string); ok {
    					fmt.Printf("事件类型: %s\n", event)

    					switch event {
    					case "task-started":
    						fmt.Println("=== 收到task-started事件 ===")

    						if !textSent {
    							// 发送continue-task事件

    							texts := []string{"床前明月光，疑是地上霜。", "举头望明月，低头思故乡。"}

    							for _, text := range texts {
    								continueTaskCmd := map[string]interface{}{
    									"header": map[string]interface{}{
    										"action":    "continue-task",
    										"task_id":   taskID,
    										"streaming": "duplex",
    									},
    									"payload": map[string]interface{}{
    										"input": map[string]interface{}{
    											"text": text,
    										},
    									},
    								}

    								continueTaskJSON, _ := json.Marshal(continueTaskCmd)
    								fmt.Printf("发送continue-task事件: %s\n", string(continueTaskJSON))

    								err = conn.WriteMessage(websocket.TextMessage, continueTaskJSON)
    								if err != nil {
    									fmt.Println("发送continue-task失败:", err)
    									return
    								}
    							}

    							textSent = true

    							// 延迟发送finish-task
    							time.Sleep(500 * time.Millisecond)

    							// 发送finish-task事件
    							finishTaskCmd := map[string]interface{}{
    								"header": map[string]interface{}{
    									"action":    "finish-task",
    									"task_id":   taskID,
    									"streaming": "duplex",
    								},
    								"payload": map[string]interface{}{
    									"input": map[string]interface{}{},
    								},
    							}

    							finishTaskJSON, _ := json.Marshal(finishTaskCmd)
    							fmt.Printf("发送finish-task事件: %s\n", string(finishTaskJSON))

    							err = conn.WriteMessage(websocket.TextMessage, finishTaskJSON)
    							if err != nil {
    								fmt.Println("发送finish-task失败:", err)
    								return
    							}
    						}

    					case "task-finished":
    						fmt.Println("=== 任务完成 ===")
    						return

    					case "task-failed":
    						fmt.Println("=== 任务失败 ===")
    						if header["error_message"] != nil {
    							fmt.Printf("错误信息: %s\n", header["error_message"])
    						}
    						return

    					case "result-generated":
    						fmt.Println("收到result-generated事件")
    					}
    				}
    			}
    		}
    	}
    }
    ```
  </Tab>

  <Tab title="C#">
    ```csharp expandable
    using System.Net.WebSockets;
    using System.Text;
    using System.Text.Json;

    class Program {
        // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
        // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：private static readonly string ApiKey = "sk-xxx"
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY") ?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");

        // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
        private const string WebSocketUrl = "wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference";
        // 输出文件路径
        private const string OutputFilePath = "output.mp3";

        // WebSocket客户端
        private static ClientWebSocket _webSocket = new ClientWebSocket();
        // 取消令牌源
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        // 任务ID
        private static string? _taskId;
        // 任务是否已启动
        private static TaskCompletionSource<bool> _taskStartedTcs = new TaskCompletionSource<bool>();

        static async Task Main(string[] args) {
            try {
                // 清空输出文件
                ClearOutputFile(OutputFilePath);

                // 连接WebSocket服务
                await ConnectToWebSocketAsync(WebSocketUrl);

                // 启动接收消息的任务
                Task receiveTask = ReceiveMessagesAsync();

                // 发送run-task事件
                _taskId = GenerateTaskId();
                await SendRunTaskCommandAsync(_taskId);

                // 等待task-started事件
                await _taskStartedTcs.Task;

                // 持续发送continue-task事件
                string[] texts = {
                    "床前明月光",
                    "疑是地上霜",
                    "举头望明月",
                    "低头思故乡"
                };
                foreach (string text in texts) {
                    await SendContinueTaskCommandAsync(text);
                }

                // 发送finish-task事件
                await SendFinishTaskCommandAsync(_taskId);

                // 等待接收任务完成
                await receiveTask;

                Console.WriteLine("任务完成，连接已关闭。");
            } catch (OperationCanceledException) {
                Console.WriteLine("任务被取消。");
            } catch (Exception ex) {
                Console.WriteLine($"发生错误：{ex.Message}");
            } finally {
                _cancellationTokenSource.Cancel();
                _webSocket.Dispose();
            }
        }

        private static void ClearOutputFile(string filePath) {
            if (File.Exists(filePath)) {
                File.WriteAllText(filePath, string.Empty);
                Console.WriteLine("输出文件已清空。");
            } else {
                Console.WriteLine("输出文件不存在，无需清空。");
            }
        }

        private static async Task ConnectToWebSocketAsync(string url) {
            var uri = new Uri(url);
            if (_webSocket.State == WebSocketState.Connecting || _webSocket.State == WebSocketState.Open) {
                return;
            }

            // 设置WebSocket连接的头部信息
            _webSocket.Options.SetRequestHeader("Authorization", $"bearer {ApiKey}");
            _webSocket.Options.SetRequestHeader("X-DashScope-DataInspection", "enable");

            try {
                await _webSocket.ConnectAsync(uri, _cancellationTokenSource.Token);
                Console.WriteLine("已成功连接到WebSocket服务。");
            } catch (OperationCanceledException) {
                Console.WriteLine("WebSocket连接被取消。");
            } catch (Exception ex) {
                Console.WriteLine($"WebSocket连接失败: {ex.Message}");
                throw;
            }
        }

        private static async Task SendRunTaskCommandAsync(string taskId) {
            var command = CreateCommand("run-task", taskId, "duplex", new {
                task_group = "audio",
                task = "tts",
                function = "SpeechSynthesizer",
                model = "cosyvoice-v3-flash",
                parameters = new
                {
                    text_type = "PlainText",
                    voice = "longanyang",
                    format = "mp3",
                    sample_rate = 22050,
                    volume = 50,
                    rate = 1,
                    pitch = 1,
                    // 如果enable_ssml设为true，只允许发送一次continue-task事件，否则会报错“Text request limit violated, expected 1.”
                    enable_ssml = false
                },
                input = new { }
            });

            await SendJsonMessageAsync(command);
            Console.WriteLine("已发送run-task事件。");
        }

        private static async Task SendContinueTaskCommandAsync(string text) {
            if (_taskId == null) {
                throw new InvalidOperationException("任务ID未初始化。");
            }

            var command = CreateCommand("continue-task", _taskId, "duplex", new {
                input = new {
                    text
                }
            });

            await SendJsonMessageAsync(command);
            Console.WriteLine("已发送continue-task事件。");
        }

        private static async Task SendFinishTaskCommandAsync(string taskId) {
            var command = CreateCommand("finish-task", taskId, "duplex", new {
                input = new { }
            });

            await SendJsonMessageAsync(command);
            Console.WriteLine("已发送finish-task事件。");
        }

        private static async Task SendJsonMessageAsync(string message) {
            var buffer = Encoding.UTF8.GetBytes(message);
            try {
                await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, _cancellationTokenSource.Token);
            } catch (OperationCanceledException) {
                Console.WriteLine("消息发送被取消。");
            }
        }

        private static async Task ReceiveMessagesAsync() {
            while (_webSocket.State == WebSocketState.Open) {
                var response = await ReceiveMessageAsync();
                if (response != null) {
                    var eventStr = response.RootElement.GetProperty("header").GetProperty("event").GetString();
                    switch (eventStr) {
                        case "task-started":
                            Console.WriteLine("任务已启动。");
                            _taskStartedTcs.TrySetResult(true);
                            break;
                        case "task-finished":
                            Console.WriteLine("任务已完成。");
                            _cancellationTokenSource.Cancel();
                            break;
                        case "task-failed":
                            Console.WriteLine("任务失败：" + response.RootElement.GetProperty("header").GetProperty("error_message").GetString());
                            _cancellationTokenSource.Cancel();
                            break;
                        default:
                            // result-generated可在此处理
                            break;
                    }
                }
            }
        }

        private static async Task<JsonDocument?> ReceiveMessageAsync() {
            var buffer = new byte[1024 * 4];
            var segment = new ArraySegment<byte>(buffer);

            try {
                WebSocketReceiveResult result = await _webSocket.ReceiveAsync(segment, _cancellationTokenSource.Token);

                if (result.MessageType == WebSocketMessageType.Close) {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", _cancellationTokenSource.Token);
                    return null;
                }

                if (result.MessageType == WebSocketMessageType.Binary) {
                    // 处理二进制数据
                    Console.WriteLine("接收到二进制数据...");

                    // 将二进制数据保存到文件
                    using (var fileStream = new FileStream(OutputFilePath, FileMode.Append)) {
                        fileStream.Write(buffer, 0, result.Count);
                    }

                    return null;
                }

                string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                return JsonDocument.Parse(message);
            } catch (OperationCanceledException) {
                Console.WriteLine("消息接收被取消。");
                return null;
            }
        }

        private static string GenerateTaskId() {
            return Guid.NewGuid().ToString("N").Substring(0, 32);
        }

        private static string CreateCommand(string action, string taskId, string streaming, object payload) {
            var command = new {
                header = new {
                    action,
                    task_id = taskId,
                    streaming
                },
                payload
            };

            return JsonSerializer.Serialize(command);
        }
    }
    ```
  </Tab>

  <Tab title="PHP">
    示例代码目录结构为：

    my-php-project/

    ├── composer.json

    ├── vendor/

    └── index.php

    composer.json内容如下，相关依赖的版本号请根据实际情况自行决定：

    ```json
    {
        "require": {
            "react/event-loop": "^1.3",
            "react/socket": "^1.11",
            "react/stream": "^1.2",
            "react/http": "^1.1",
            "ratchet/pawl": "^0.4"
        },
        "autoload": {
            "psr-4": {
                "App\\": "src/"
            }
        }
    }
    ```

    index.php内容如下：

    ```php expandable
    <?php

    require __DIR__ . '/vendor/autoload.php';

    use Ratchet\Client\Connector;
    use React\EventLoop\Loop;
    use React\Socket\Connector as SocketConnector;

    // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：$api_key = "sk-xxx"
    $api_key = getenv("DASHSCOPE_API_KEY");
    // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
    $websocket_url = 'wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference'; // WebSocket服务器地址
    $output_file = 'output.mp3'; // 输出文件路径

    $loop = Loop::get();

    if (file_exists($output_file)) {
        // 清空文件内容
        file_put_contents($output_file, '');
    }

    // 创建自定义的连接器
    $socketConnector = new SocketConnector($loop, [
        'tcp' => [
            'bindto' => '0.0.0.0:0',
        ],
        'tls' => [
            'verify_peer' => true,
            'verify_peer_name' => true,
        ],
    ]);

    $connector = new Connector($loop, $socketConnector);

    $headers = [
        'Authorization' => 'bearer ' . $api_key,
        'X-DashScope-DataInspection' => 'enable'
    ];

    $connector($websocket_url, [], $headers)->then(function ($conn) use ($loop, $output_file) {
        echo "连接到WebSocket服务器\n";

        // 生成任务ID
        $taskId = generateTaskId();

        // 发送 run-task 事件
        sendRunTaskMessage($conn, $taskId);

        // 定义发送 continue-task 事件的函数
        $sendContinueTask = function() use ($conn, $loop, $taskId) {
            // 待发送的文本
            $texts = ["床前明月光", "疑是地上霜", "举头望明月", "低头思故乡"];
            $continueTaskCount = 0;
            foreach ($texts as $text) {
                $continueTaskMessage = json_encode([
                    "header" => [
                        "action" => "continue-task",
                        "task_id" => $taskId,
                        "streaming" => "duplex"
                    ],
                    "payload" => [
                        "input" => [
                            "text" => $text
                        ]
                    ]
                ]);
                echo "准备发送continue-task事件: " . $continueTaskMessage . "\n";
                $conn->send($continueTaskMessage);
                $continueTaskCount++;
            }
            echo "发送的continue-task事件个数为：" . $continueTaskCount . "\n";

            // 发送 finish-task 事件
            sendFinishTaskMessage($conn, $taskId);
        };

        // 标记是否收到 task-started 事件
        $taskStarted = false;

        // 监听消息
        $conn->on('message', function($msg) use ($conn, $sendContinueTask, $loop, &$taskStarted, $taskId, $output_file) {
            if ($msg->isBinary()) {
                // 写入二进制数据到本地文件
                file_put_contents($output_file, $msg->getPayload(), FILE_APPEND);
            } else {
                // 处理非二进制消息
                $response = json_decode($msg, true);

                if (isset($response['header']['event'])) {
                    handleEvent($conn, $response, $sendContinueTask, $loop, $taskId, $taskStarted);
                } else {
                    echo "未知的消息格式\n";
                }
            }
        });

        // 监听连接关闭
        $conn->on('close', function($code = null, $reason = null) {
            echo "连接已关闭\n";
            if ($code !== null) {
                echo "关闭代码: " . $code . "\n";
            }
            if ($reason !== null) {
                echo "关闭原因：" . $reason . "\n";
            }
        });
    }, function ($e) {
        echo "无法连接：{$e->getMessage()}\n";
    });

    $loop->run();

    /**
     * 生成任务ID
     * @return string
     */
    function generateTaskId(): string {
        return bin2hex(random_bytes(16));
    }

    /**
     * 发送 run-task 事件
     * @param $conn
     * @param $taskId
     */
    function sendRunTaskMessage($conn, $taskId) {
        $runTaskMessage = json_encode([
            "header" => [
                "action" => "run-task",
                "task_id" => $taskId,
                "streaming" => "duplex"
            ],
            "payload" => [
                "task_group" => "audio",
                "task" => "tts",
                "function" => "SpeechSynthesizer",
                "model" => "cosyvoice-v3-flash",
                "parameters" => [
                    "text_type" => "PlainText",
                    "voice" => "longanyang",
                    "format" => "mp3",
                    "sample_rate" => 22050,
                    "volume" => 50,
                    "rate" => 1,
                    "pitch" => 1,
                    // 如果enable_ssml设为true，只允许发送一次continue-task事件，否则会报错“Text request limit violated, expected 1.”
                    "enable_ssml" => false
                ],
                "input" => (object) []
            ]
        ]);
        echo "准备发送run-task事件: " . $runTaskMessage . "\n";
        $conn->send($runTaskMessage);
        echo "run-task事件已发送\n";
    }

    /**
     * 读取音频文件
     * @param string $filePath
     * @return bool|string
     */
    function readAudioFile(string $filePath) {
        $voiceData = file_get_contents($filePath);
        if ($voiceData === false) {
            echo "无法读取音频文件\n";
        }
        return $voiceData;
    }

    /**
     * 分割音频数据
     * @param string $data
     * @param int $chunkSize
     * @return array
     */
    function splitAudioData(string $data, int $chunkSize): array {
        return str_split($data, $chunkSize);
    }

    /**
     * 发送 finish-task 事件
     * @param $conn
     * @param $taskId
     */
    function sendFinishTaskMessage($conn, $taskId) {
        $finishTaskMessage = json_encode([
            "header" => [
                "action" => "finish-task",
                "task_id" => $taskId,
                "streaming" => "duplex"
            ],
            "payload" => [
                "input" => (object) []
            ]
        ]);
        echo "准备发送finish-task事件: " . $finishTaskMessage . "\n";
        $conn->send($finishTaskMessage);
        echo "finish-task事件已发送\n";
    }

    /**
     * 处理事件
     * @param $conn
     * @param $response
     * @param $sendContinueTask
     * @param $loop
     * @param $taskId
     * @param $taskStarted
     */
    function handleEvent($conn, $response, $sendContinueTask, $loop, $taskId, &$taskStarted) {
        switch ($response['header']['event']) {
            case 'task-started':
                echo "任务开始，发送continue-task事件...\n";
                $taskStarted = true;
                // 发送 continue-task 事件
                $sendContinueTask();
                break;
            case 'result-generated':
                // 收到result-generated事件
                break;
            case 'task-finished':
                echo "任务完成\n";
                $conn->close();
                break;
            case 'task-failed':
                echo "任务失败\n";
                echo "错误代码：" . $response['header']['error_code'] . "\n";
                echo "错误信息：" . $response['header']['error_message'] . "\n";
                $conn->close();
                break;
            case 'error':
                echo "错误：" . $response['payload']['message'] . "\n";
                break;
            default:
                echo "未知事件：" . $response['header']['event'] . "\n";
                break;
        }

        // 如果任务已完成，关闭连接
        if ($response['header']['event'] == 'task-finished') {
            // 等待1秒以确保所有数据都已传输完毕
            $loop->addTimer(1, function() use ($conn) {
                $conn->close();
                echo "客户端关闭连接\n";
            });
        }

        // 如果没有收到 task-started 事件，关闭连接
        if (!$taskStarted && in_array($response['header']['event'], ['task-failed', 'error'])) {
            $conn->close();
        }
    }
    ```
  </Tab>

  <Tab title="Node.js">
    需安装相关依赖：

    ```bash
    npm install ws
    npm install uuid
    ```

    示例代码如下：

    ```javascript expandable
    const WebSocket = require('ws');
    const fs = require('fs');
    const uuid = require('uuid').v4;

    // 新加坡和北京地域的API Key不同。获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    // 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：const apiKey = "sk-xxx"
    const apiKey = process.env.DASHSCOPE_API_KEY;
    // 以下为华北2（北京）地域的配置，调用时请将"{WorkspaceId}"替换为真实的业务空间ID，各地域的配置不同。
    const url = 'wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference';
    // 输出文件路径
    const outputFilePath = 'output.mp3';

    // 清空输出文件
    fs.writeFileSync(outputFilePath, '');

    // 创建WebSocket客户端
    const ws = new WebSocket(url, {
      headers: {
        Authorization: `bearer ${apiKey}`,
        'X-DashScope-DataInspection': 'enable'
      }
    });

    let taskStarted = false;
    let taskId = uuid();

    ws.on('open', () => {
      console.log('已连接到WebSocket服务器');

      // 发送run-task事件
      const runTaskMessage = JSON.stringify({
        header: {
          action: 'run-task',
          task_id: taskId,
          streaming: 'duplex'
        },
        payload: {
          task_group: 'audio',
          task: 'tts',
          function: 'SpeechSynthesizer',
          model: 'cosyvoice-v3-flash',
          parameters: {
            text_type: 'PlainText',
            voice: 'longanyang', // 音色
            format: 'mp3', // 音频格式
            sample_rate: 22050, // 采样率
            volume: 50, // 音量
            rate: 1, // 语速
            pitch: 1, // 音调
            enable_ssml: false // 是否开启SSML功能。如果enable_ssml设为true，只允许发送一次continue-task事件，否则会报错“Text request limit violated, expected 1.”
          },
          input: {}
        }
      });
      ws.send(runTaskMessage);
      console.log('已发送run-task消息');
    });

    const fileStream = fs.createWriteStream(outputFilePath, { flags: 'a' });
    ws.on('message', (data, isBinary) => {
      if (isBinary) {
        // 写入二进制数据到文件
        fileStream.write(data);
      } else {
        const message = JSON.parse(data);

        switch (message.header.event) {
          case 'task-started':
            taskStarted = true;
            console.log('任务已开始');
            // 发送continue-task事件
            sendContinueTasks(ws);
            break;
          case 'task-finished':
            console.log('任务已完成');
            ws.close();
            fileStream.end(() => {
              console.log('文件流已关闭');
            });
            break;
          case 'task-failed':
            console.error('任务失败：', message.header.error_message);
            ws.close();
            fileStream.end(() => {
              console.log('文件流已关闭');
            });
            break;
          default:
            // 可以在这里处理result-generated
            break;
        }
      }
    });

    function sendContinueTasks(ws) {
      const texts = [
        '床前明月光，',
        '疑是地上霜。',
        '举头望明月，',
        '低头思故乡。'
      ];

      texts.forEach((text, index) => {
        setTimeout(() => {
          if (taskStarted) {
            const continueTaskMessage = JSON.stringify({
              header: {
                action: 'continue-task',
                task_id: taskId,
                streaming: 'duplex'
              },
              payload: {
                input: {
                  text: text
                }
              }
            });
            ws.send(continueTaskMessage);
            console.log(`已发送continue-task，文本：${text}`);
          }
        }, index * 1000); // 每隔1秒发送一次
      });

      // 发送finish-task事件
      setTimeout(() => {
        if (taskStarted) {
          const finishTaskMessage = JSON.stringify({
            header: {
              action: 'finish-task',
              task_id: taskId,
              streaming: 'duplex'
            },
            payload: {
              input: {}
            }
          });
          ws.send(finishTaskMessage);
          console.log('已发送finish-task');
        }
      }, texts.length * 1000 + 1000); // 在所有continue-task事件发送完毕后1秒发送
    }

    ws.on('close', () => {
      console.log('已断开与WebSocket服务器的连接');
    });
    ```
  </Tab>
</Tabs>

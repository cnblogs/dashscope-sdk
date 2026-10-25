> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# WebSocket 接入指南

> 本文介绍通过WebSocket连接访问Sambert实时语音合成服务的交互流程、接口地址和请求头。

<meta property="og:title" content="WebSocket 接入指南" />

DashScope SDK目前仅支持Java和Python。使用其他编程语言时，可通过WebSocket连接与服务进行通信。

<strong>用户指南：</strong>关于模型介绍和选型建议请参见[语音合成](/zh/model-studio/tts-model)。

## 接口地址 <span id="sb_ws_h2_endpoint" />

Sambert仅支持在北京地域使用。

WebSocket 接口地址固定为：`wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference`

调用时请将`{WorkspaceId}`替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。

<Tip>
  URL 必须使用 `wss://` 协议，且固定不变。Authorization 在请求头中设置（参见[请求头](/zh/model-studio/sambert-websocket-api#sb_ws_h2_headers)）。
</Tip>

<Tip>
  阿里云百炼为华北2（北京）地域推出了业务空间专属域名，能够为推理请求提供卓越的性能和更高的稳定性，建议从 `dashscope.aliyuncs.com` 迁移至 `{WorkspaceId}.cn-beijing.maas.aliyuncs.com`。

  `{WorkspaceId}`需要替换为真实的[Workspace ID](/zh/model-studio/regions#h2_migrate_domain)。现有域名仍可正常使用。
</Tip>

## 请求头 <span id="sb_ws_h2_headers" />

请求头中需添加如下信息：

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "25%" }} /><col style={{ width: "8.54%" }} /><col style={{ width: "10.31%" }} /><col style={{ width: "56.15%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>参数</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>类型</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>是否必选</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>Authorization</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>是</p></td><td style={{ verticalAlign: "top" }}><p>鉴权令牌，格式为 <code>Bearer \<your\_api\_key></code>，将 <code>\<your\_api\_key></code> 替换为实际的 API Key。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>user-agent</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>客户端标识，便于服务端追踪来源。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>X-DashScope-WorkSpace</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>阿里云百炼<a href="/zh/model-studio/use-workspace">业务空间ID</a>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>X-DashScope-DataInspection</p></td><td style={{ verticalAlign: "top" }}><p>string</p></td><td style={{ verticalAlign: "top" }}><p>否</p></td><td style={{ verticalAlign: "top" }}><p>是否启用数据合规检测功能。默认不传或设为<code>enable</code>。如非必要，请勿启用该参数。</p></td></tr></tbody></table>

<Tip>
  Authorization 鉴权在 WebSocket 握手阶段验证。如果 API Key 无效或缺失，握手将失败并返回 HTTP 401/403 错误。
</Tip>

## 交互流程 <span id="62149ca39bx2i" />

<img src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3672835871/CAEQURiBgICX8Y6AmBkiIGJmOTc2YjY2ZTYyNDQ0MmI4M2U4NTYxYmE5ZjYwMDY44709861_20241015153444.149.svg" alt="image" />

客户端事件和服务端事件的详细说明，请参见[客户端事件](/zh/model-studio/sambert-client-events)和[服务端事件](/zh/model-studio/sambert-server-events)。

按时间顺序，客户端与服务端的交互流程如下：

1. 建立连接：客户端与服务端建立WebSocket连接。
2. 开启任务：客户端发送run-task事件以开启任务。Sambert在run-task中一次性发送全部待合成文本。
3. 等待确认：客户端收到服务端返回的task-started事件，标志着任务已成功开启。
4. 接收音频：客户端通过 `binary` 通道接收服务端持续返回的音频流，同时收到result-generated事件（携带时间戳等附加信息）。
5. 任务结束：客户端收到服务端返回的task-finished事件，标志着任务结束。
6. 关闭连接：客户端关闭WebSocket连接。

收到 `task-finished` 后，可在同一连接上用新的 `task_id` 发送下一轮 `run-task`。每轮在 `run-task` 中一次提交完整文本。

<Tip>
  Sambert 不支持流式输入（streaming 为 `out` 而非 duplex），所有待合成文本必须在 run-task 事件中一次性发送。不支持 continue-task 和 finish-task 指令。
</Tip>

通过 Java SDK 使用对象池并发调用 Sambert 的方法和完整示例，请参见[语音合成高并发最佳实践](/zh/model-studio/tts-high-concurrency-best-practices#sambert-java)。直接使用 WebSocket 协议时，可按上述流程复用连接。

## 示例代码 <span id="ws-examples" />

以下示例使用通用 WebSocket 库直接发送协议事件，使用 `sambert-zhichu-v1`。先[获取 API Key](/zh/model-studio/get-api-key)并配置 `DASHSCOPE_API_KEY` 环境变量，将 `{WorkspaceId}` 替换为对应地域的业务空间 ID；使用其他地域时同时替换接口地址和 API Key。

<Tabs>
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
    	"time"

    	"github.com/google/uuid"
    	"github.com/gorilla/websocket"
    )

    const (
    	wsURL      = "wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference" // WebSocket服务器地址
    	outputFile = "output.mp3"                                        // 输出文件路径
    )

    func main() {
    	// 若没有将API Key配置到环境变量，可将下行替换为：apiKey := "your_api_key"。不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
    	apiKey := os.Getenv("DASHSCOPE_API_KEY")

    	// 检查并清空输出文件
    	if err := clearOutputFile(outputFile); err != nil {
    		fmt.Println("清空输出文件失败：", err)
    		return
    	}

    	// 连接WebSocket服务
    	conn, err := connectWebSocket(apiKey)
    	if err != nil {
    		fmt.Println("连接WebSocket失败：", err)
    		return
    	}
    	defer closeConnection(conn)

    	// 创建一个通道用于接收任务完成的通知
    	done := make(chan struct{})

    	// 启动异步接收消息的goroutine
    	go receiveMessage(conn, done)

    	// 发送run-task指令
    	if err := sendRunTaskMsg(conn); err != nil {
    		fmt.Println("发送run-task指令失败：", err)
    		return
    	}

    	// 等待任务完成或超时
    	select {
    	case <-done:
    		fmt.Println("任务结束")
    	case <-time.After(5 * time.Minute):
    		fmt.Println("任务超时")
    	}
    }

    // 定义消息结构体
    type Message struct {
    	Header  Header  `json:"header"`
    	Payload Payload `json:"payload"`
    }

    // 定义头部信息
    type Header struct {
    	Action       string                 `json:"action,omitempty"`
    	TaskID       string                 `json:"task_id"`
    	Streaming    string                 `json:"streaming,omitempty"`
    	Event        string                 `json:"event,omitempty"`
    	ErrorCode    string                 `json:"error_code,omitempty"`
    	ErrorMessage string                 `json:"error_message,omitempty"`
    	Attributes   map[string]interface{} `json:"attributes"`
    }

    // 定义负载信息
    type Payload struct {
    	Model      string     `json:"model,omitempty"`
    	TaskGroup  string     `json:"task_group,omitempty"`
    	Task       string     `json:"task,omitempty"`
    	Function   string     `json:"function,omitempty"`
    	Input      Input      `json:"input,omitempty"`
    	Parameters Parameters `json:"parameters,omitempty"`
    	Output     Output     `json:"output,omitempty"`
    	Usage      Usage      `json:"usage,omitempty"`
    }

    // 定义输入信息
    type Input struct {
    	Text string `json:"text"`
    }

    // 定义参数信息
    type Parameters struct {
    	TextType                string  `json:"text_type"`
    	Format                  string  `json:"format"`
    	SampleRate              int     `json:"sample_rate"`
    	Volume                  int     `json:"volume"`
    	Rate                    float64 `json:"rate"`
    	Pitch                   float64 `json:"pitch"`
    	WordTimestampEnabled    bool    `json:"word_timestamp_enabled"`
    	PhonemeTimestampEnabled bool    `json:"phoneme_timestamp_enabled"`
    }

    // 定义输出信息
    type Output struct {
    	Sentence Sentence `json:"sentence"`
    }

    // 定义句子信息
    type Sentence struct {
    	BeginTime int    `json:"begin_time"`
    	EndTime   int    `json:"end_time"`
    	Words     []Word `json:"words"`
    }

    // 定义单词信息
    type Word struct {
    	Text      string    `json:"text"`
    	BeginTime int       `json:"begin_time"`
    	EndTime   int       `json:"end_time"`
    	Phonemes  []Phoneme `json:"phonemes"`
    }

    // 定义音素信息
    type Phoneme struct {
    	BeginTime int    `json:"begin_time"`
    	EndTime   int    `json:"end_time"`
    	Text      string `json:"text"`
    	Tone      int    `json:"tone"`
    }

    // 定义使用信息
    type Usage struct {
    	Characters int `json:"characters"`
    }

    func receiveMessage(conn *websocket.Conn, done chan struct{}) {
    	for {
    		msgType, message, err := conn.ReadMessage()
    		if err != nil {
    			fmt.Println("解析服务器消息失败：", err)
    			close(done)
    			break
    		}

    		if msgType == websocket.BinaryMessage {
    			// 处理二进制音频流
    			if err := writeBinaryDataToFile(message, outputFile); err != nil {
    				fmt.Println("写入二进制数据失败：", err)
    				close(done)
    				break
    			}
    			fmt.Println("音频片段已写入本地文件")
    		} else {
    			// 处理文本消息
    			var msg Message
    			if err := json.Unmarshal(message, &msg); err != nil {
    				fmt.Println("解析事件失败：", err)
    				continue
    			}
    			if handleMessage(conn, msg, done) {
    				break
    			}
    		}
    	}
    }

    func handleMessage(conn *websocket.Conn, msg Message, done chan struct{}) bool {
    	switch msg.Header.Event {
    	case "task-started":
    		fmt.Println("任务已启动")

    	case "result-generated":
    	// 如需获取附加消息，可在此处添加相应代码

    	case "task-finished":
    		fmt.Println("任务已完成")
    		close(done)
    		return true

    	case "task-failed":
    		if msg.Header.ErrorMessage != "" {
    			fmt.Printf("任务失败：%s\n", msg.Header.ErrorMessage)
    		} else {
    			fmt.Println("未知原因导致任务失败")
    		}
    		close(done)
    		return true

    	default:
    		fmt.Printf("预料之外的事件：%v\n", msg)
    		close(done)
    	}

    	return false
    }

    func sendRunTaskMsg(conn *websocket.Conn) error {
    	runTaskMsg, err := generateRunTaskMsg()
    	if err != nil {
    		return err
    	}
    	if err := conn.WriteMessage(websocket.TextMessage, []byte(runTaskMsg)); err != nil {
    		return err
    	}
    	return nil
    }

    func generateRunTaskMsg() (string, error) {
    	runTaskMessage := Message{
    		Header: Header{
    			Action:    "run-task",
    			TaskID:    uuid.New().String(),
    			Streaming: "out",
    		},
    		Payload: Payload{
    			Model:     "sambert-zhichu-v1",
    			TaskGroup: "audio",
    			Task:      "tts",
    			Function:  "SpeechSynthesizer",
    			Input: Input{
    				Text: "白日依山尽，黄河入海流。欲穷千里目，更上一层楼。",
    			},
    			Parameters: Parameters{
    				TextType:                "PlainText",
    				Format:                  "mp3",
    				SampleRate:              16000,
    				Volume:                  50,
    				Rate:                    1.0,
    				Pitch:                   1.0,
    				WordTimestampEnabled:    true,
    				PhonemeTimestampEnabled: true,
    			},
    		},
    	}

    	runTaskMsgJSON, err := json.Marshal(runTaskMessage)
    	return string(runTaskMsgJSON), err
    }

    func connectWebSocket(apiKey string) (*websocket.Conn, error) {
    	header := make(http.Header)
    	header.Add("X-DashScope-DataInspection", "enable")
    	header.Add("Authorization", fmt.Sprintf("bearer %s", apiKey))
    	conn, _, err := websocket.DefaultDialer.Dial(wsURL, header)
    	if err != nil {
    		fmt.Println("连接WebSocket失败：", err)
    		return nil, err
    	}
    	return conn, nil
    }

    func writeBinaryDataToFile(data []byte, filePath string) error {
    	file, err := os.OpenFile(filePath, os.O_APPEND|os.O_CREATE|os.O_WRONLY, 0644)
    	if err != nil {
    		return err
    	}
    	defer file.Close()
    	_, err = file.Write(data)
    	return err
    }

    func closeConnection(conn *websocket.Conn) {
    	if conn != nil {
    		conn.Close()
    	}
    }

    func clearOutputFile(filePath string) error {
    	file, err := os.OpenFile(filePath, os.O_TRUNC|os.O_CREATE|os.O_WRONLY, 0644)
    	if err != nil {
    		return err
    	}
    	file.Close()
    	return nil
    }
    ```
  </Tab>

  <Tab title="C#">
    示例代码如下：

    ```csharp expandable
    using System.Net.WebSockets;
    using System.Text;
    using System.Text.Json;

    class Program {
        // 若没有将API Key配置到环境变量，可将下行替换为：private const string ApiKey="your_api_key"。不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
        private static readonly string ApiKey = Environment.GetEnvironmentVariable("DASHSCOPE_API_KEY") ?? throw new InvalidOperationException("DASHSCOPE_API_KEY environment variable is not set.");

        private const string WebSocketUrl = "wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference"; // WebSocket服务器地址
        private const string OutputFilePath = "output.mp3"; // 输出文件路径

        static async Task Main(string[] args) {
            var ws = new ClientWebSocket();
            try {
                // 1. 连接WebSocket服务，鉴权
                await ConnectWithAuth(ws, WebSocketUrl);

                // 2. 启动接收消息的线程
                var receiveTask = ReceiveMessages(ws);

                // 3. 发送run-task指令
                string textToSynthesize = "白日依山尽，黄河入海流。欲穷千里目，更上一层楼。";
                string taskId = GenerateTaskId();
                await SendRunTaskCommand(ws, textToSynthesize, taskId);

                // 4. 等待接收任务完成
                await receiveTask;
            } catch (Exception ex) {
                Console.WriteLine($"错误：{ex.Message}");
            } finally {
                if (ws.State == WebSocketState.Open) {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "关闭连接", CancellationToken.None);
                }
            }
        }

        private static async Task ConnectWithAuth(ClientWebSocket ws, string url) {
            var uri = new Uri(url);
            ws.Options.SetRequestHeader("Authorization", $"bearer {ApiKey}");
            ws.Options.SetRequestHeader("X-DashScope-DataInspection", "enable");
            await ws.ConnectAsync(uri, CancellationToken.None);
            Console.WriteLine("已连接到WebSocket服务器。");
        }

        private static string GenerateTaskId() {
            return Guid.NewGuid().ToString("N");
        }

        private static async Task SendRunTaskCommand(ClientWebSocket ws, string text, string taskId) {
            var command = CreateRunTaskCommand(text, taskId);
            var buffer = Encoding.UTF8.GetBytes(command);
            await ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            Console.WriteLine("已发送run-task指令。");
        }

        private static string CreateRunTaskCommand(string text, string taskId) {
            var command = new {
                header = new {
                    action = "run-task",
                    task_id = taskId,
                    streaming = "out"
                },
                payload = new {
                    model = "sambert-zhichu-v1",
                    task_group = "audio",
                    task = "tts",
                    function = "SpeechSynthesizer",
                    input = new {
                        text = text
                    },
                    parameters = new {
                        text_type = "PlainText",
                        format = "mp3",
                        sample_rate = 16000,
                        volume = 50,
                        rate = 1,
                        pitch = 1,
                        word_timestamp_enabled = true,
                        phoneme_timestamp_enabled = true
                    }
                }
            };
            return JsonSerializer.Serialize(command);
        }

        private static async Task ReceiveMessages(ClientWebSocket ws) {
            var buffer = new byte[1024 * 4];
            var fs = new FileStream(OutputFilePath, FileMode.Create, FileAccess.Write);
            bool taskStarted = false;
            bool taskFinished = false;

            while (ws.State == WebSocketState.Open && !taskFinished) {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                switch (result.MessageType) {
                    case WebSocketMessageType.Text:
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        var jsonMessage = JsonSerializer.Deserialize<JsonElement>(message);

                        ProcessTextMessage(jsonMessage, ref taskStarted, ref taskFinished);
                        break;
                    case WebSocketMessageType.Binary:
                        if (taskStarted) {
                            await fs.WriteAsync(buffer, 0, result.Count);
                            Console.WriteLine("收到音频数据。");
                        }
                        break;
                    case WebSocketMessageType.Close:
                        Console.WriteLine("服务器关闭了连接。");
                        taskFinished = true;
                        break;
                }
            }
            fs.Close();
        }

        private static void ProcessTextMessage(JsonElement jsonMessage, ref bool taskStarted, ref bool taskFinished) {
            if (jsonMessage.TryGetProperty("header", out JsonElement header) && header.TryGetProperty("event", out JsonElement eventToken)) {
                var eventType = eventToken.GetString();
                switch (eventType) {
                    case "task-started":
                        taskStarted = true;
                        Console.WriteLine("任务开始。");
                        break;
                    case "result-generated":
                        // 如需获取附加消息，可在此处添加相应代码
                        break;
                    case "task-finished":
                        taskFinished = true;
                        Console.WriteLine("任务完成。");
                        break;
                    case "task-failed":
                        taskFinished = true;
                        Console.WriteLine("任务失败。");
                        break;
                }
            }
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

    require 'vendor/autoload.php';

    use Ratchet\Client\Connector;
    use React\EventLoop\Loop;
    use React\Socket\Connector as SocketConnector;

    # 若没有将API Key配置到环境变量，可将下行替换为：$api_key="your_api_key"。不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
    $api_key = getenv("DASHSCOPE_API_KEY");
    $websocket_url = 'wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference'; // WebSocket服务器地址
    $output_file = 'output.mp3'; // 输出文件路径

    $loop = Loop::get();

    if (file_exists($output_file)) {
        // 清空文件内容
        file_put_contents($output_file, '');
        echo "文件已清空\n";
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

    // 连接WebSocket服务
    $connector($websocket_url, [], $headers)
        ->then(function ($conn) use ($output_file) {
            echo "连接成功\n";

            // 异步接收WebSocket消息
            $conn->on('message', function ($msg) use ($conn, $output_file) {
                if ($msg->isBinary()) {
                    // 写入二进制数据到本地文件
                    file_put_contents($output_file, $msg->getPayload(), FILE_APPEND);
                    echo "二进制数据写入文件\n";
                } else {
                    $data = json_decode($msg, true);
                    switch ($data['header']['event']) {
                        case 'task-started':
                            echo "任务开始\n";
                            break;
                        case 'result-generated':
                            // 如需获取附加消息，可在此处添加相应代码
                            break;
                        case 'task-finished':
                            echo "任务完成\n";
                            $conn->close();
                            break;
                        case 'task-failed':
                            echo "任务失败：" . $data['header']['error_message'] . "\n";
                            $conn->close();
                            break;
                        default:
                            echo "未知事件：" . $msg . "\n";
                    }
                }
            });

            // 监听连接关闭
            $conn->on('close', function($code = null, $reason = null) {
                echo "连接已关闭\n";
                if ($code !== null) {
                    echo "关闭代码：" . $code . "\n";
                }
                if ($reason !== null) {
                    echo "关闭原因：" . $reason . "\n";
                }
            });

            // 发送run-task指令
            $conn->send(json_encode([
                'header' => [
                    'action' => 'run-task',
                    'task_id' => bin2hex(random_bytes(16)),
                    'streaming' => 'out'
                ],
                'payload' => [
                    'model' => 'sambert-zhichu-v1',
                    'task_group' => 'audio',
                    'task' => 'tts',
                    'function' => 'SpeechSynthesizer',
                    'input' => [
                        'text' => '床前明月光，疑是地上霜。举头望明月，低头思故乡。'
                    ],
                    'parameters' => [
                        'text_type' => 'PlainText',
                        'format' => 'mp3',
                        'sample_rate' => 16000,
                        'volume' => 50,
                        'rate' => 1,
                        'pitch' => 1,
                        'word_timestamp_enabled' => true,
                        'phoneme_timestamp_enabled' => true
                    ]
                ]
            ]));
            echo "run-task指令已发送\n";
        }, function (Exception $e) {
            echo "连接失败：{$e->getMessage()}\n";
            file_put_contents('error.log', $e->getMessage() . "\n", FILE_APPEND);
        });

    $loop->run();
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
    const { v4: uuidv4 } = require('uuid');

    // 若没有将API Key配置到环境变量，可将下行替换为：apiKey = 'your_api_key'。不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
    const apiKey = process.env.DASHSCOPE_API_KEY;
    const wsUrl = 'wss://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api-ws/v1/inference'; // WebSocket服务器地址
    const outputFilePath = 'output.mp3'; // 替换为您的音频文件路径

    async function main() {
      await checkAndClearOutputFile(outputFilePath);
      createWebSocketConnection();
    }

    const fileStream = fs.createWriteStream(outputFilePath, { flags: 'a' });
    function createWebSocketConnection() {
      const ws = new WebSocket(wsUrl, {
        headers: {
          Authorization: `bearer ${apiKey}`,
          'X-DashScope-DataInspection': 'enable'
        }
      });

      ws.on('open', () => {
        console.log('已连接到WebSocket服务器');
        sendRunTaskMessage(ws);
      });

      ws.on('message', (data, isBinary) => handleWebSocketMessage(data, isBinary, ws));
      ws.on('error', (error) => console.error('WebSocket错误:', error));
      ws.on('close', () => console.log('WebSocket连接已关闭'));

      return ws;
    }

    function sendRunTaskMessage(ws) {
      const taskId = uuidv4();
      const runTaskMessage = {
        header: {
          action: 'run-task',
          task_id: taskId,
          streaming: 'out'
        },
        payload: {
          model: 'sambert-zhichu-v1',
          task_group: 'audio',
          task: 'tts',
          function: 'SpeechSynthesizer',
          input: {
            text: '白日依山尽，黄河入海流。欲穷千里目，更上一层楼。'
          },
          parameters: {
            text_type: 'PlainText',
            format: 'mp3',
            sample_rate: 16000,
            volume: 50,
            rate: 1,
            pitch: 1,
            word_timestamp_enabled: true,
            phoneme_timestamp_enabled: true
          }
        }
      };
      ws.send(JSON.stringify(runTaskMessage));
      console.log('run-task指令已发送');
    }

    function handleWebSocketMessage(data, isBinary, ws) {
      if (isBinary) {
        fileStream.write(data);
      } else {
        const message = JSON.parse(data);
        handleWebSocketEvent(message, ws);
      }
    }

    function handleWebSocketEvent(message, ws) {
      switch (message.header.event) {
        case 'task-started':
          console.log('任务已启动');
          break;
        case 'result-generated':
          console.log('结果已生成');
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
          console.log('未知事件：', message.header.event);
      }
    }

    function checkAndClearOutputFile(filePath) {
      return new Promise((resolve, reject) => {
        fs.access(filePath, fs.F_OK, (err) => {
          if (!err) {
            fs.truncate(filePath, 0, (truncateErr) => {
              if (truncateErr) return reject(truncateErr);
              console.log('文件已清空');
              resolve();
            });
          } else {
            fs.open(filePath, 'w', (openErr) => {
              if (openErr) return reject(openErr);
              console.log('文件已创建');
              resolve();
            });
          }
        });
      });
    }

    main().catch(console.error);
    ```
  </Tab>
</Tabs>

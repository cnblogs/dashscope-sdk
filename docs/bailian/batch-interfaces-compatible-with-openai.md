> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# OpenAI兼容-Batch（文件输入）

> 阿里云百炼提供与 OpenAI 兼容的 Batch File API，支持通过文件批量提交请求。系统异步处理所有请求，在全部完成或达到最长等待时间后返回结果，费用仅为实时调用的 50% 。适用于数据分析、模型评测等时效性要求不高但需大批量处理的场景。

<meta property="og:title" content="OpenAI兼容-Batch（文件输入）" />

如需在控制台操作，请参见[批量推理](/zh/model-studio/batch-inference)。

## 工作流程 <span id="fe3cf9d7b2w12" /> <span id="ac1601293a3ux" />

<img src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9782127871/CAEQaxiBgIDB5qWk4BkiIDViYzQ0MWUwNTYyNDQ3NDM5NzM0ZTc4N2Y3NTU2NjA56318723_20260129171731.699.svg" alt="image" />

## 前提条件 <span id="235500910ci04" /> <span id="0a394b8cf1okd" />

支持通过 OpenAI SDK（Python、Node.js）或HTTP API调用 Batch File 接口。

- <strong>获取API Key</strong>：[获取并配置API Key 到环境变量](/zh/model-studio/get-api-key)
- <strong>安装 SDK（可选）：</strong>如需使用SDK调用，请安装 [OpenAI SDK](/zh/model-studio/install-sdk)
- <strong>服务端点</strong>

  - <strong>华北2（北京）：</strong>`https://dashscope.aliyuncs.com/compatible-mode/v1`
  - <strong>新加坡：</strong>`https://dashscope-intl.aliyuncs.com/compatible-mode/v1`

## 适用范围 <span id="9ff45144fafya" /> <span id="51eff8d97fl6c" />

<Tabs>
  <Tab title="华北2（北京）">
    <strong>支持的模型：</strong>

    - <strong>文本生成模型</strong>

      - 千问 Max：qwen3.8-max、qwen3.7-max
      - 千问 Plus：qwen3.7-plus、qwen3.6-plus、qwen3.5-plus、qwen-plus、qwen-plus-latest
      - 千问 Flash：qwen3.8-flash、qwen3.7-flash、qwen3.6-flash、qwen3.5-flash、qwen-flash
      - 千问 Long：qwen-long
    - <strong>多模态模型</strong>

      - [图像与视频理解](/zh/model-studio/vision)：qwen3.8-max、qwen3.8-flash、qwen3.7-plus、qwen3.6-plus、qwen3.7-flash、qwen3.6-flash、qwen3.5-plus、qwen3.5-flash、qwen3-vl-plus
      - [文字提取](/zh/model-studio/qwen-vl-ocr)：qwen-vl-ocr
      - [全模态](/zh/model-studio/qwen-omni)：qwen3.5-omni-plus
    - [<strong>向量模型</strong>](https://help.aliyun.com/zh/model-studio/user-guide/embedding)<strong>：</strong>text-embedding-v1、text-embedding-v2、text-embedding-v3、text-embedding-v4、qwen3.7-text-embedding、qwen3.7-text-embedding-flash
  </Tab>

  <Tab title="新加坡">
    <strong>支持的模型</strong>：qwen3.7-plus、qwen-max、qwen-plus。
  </Tab>
</Tabs>

<Tip>
  - 在Batch 场景下，`qwen3.8-max`、`qwen3.8-flash`、`qwen3.7-max`、`qwen3.7-plus`、`qwen3.6-plus`、`qwen3.7-flash`、`qwen3.6-flash`、`qwen3.5-plus`、`qwen3.5-flash`和`qwen3.5-omni-plus`单次请求的上下文 Token 数最大支持 256K，`qwen3.5-omni-plus`不支持语音输出。
  - 部分模型支持思考模式，开启后会产生思考`tokens`导致成本增加。
  - `qwen3.8`、`qwen3.7`、`qwen3.6`和`qwen3.5` 系列模型默认开启思考模式。建议使用混合思考模型时，显式设置`enable_thinking`参数（`true`开启/`false`关闭）。
  - 在 JSONL 请求体中，`enable_thinking` 为 `body` 的顶层参数，须与 `model` 同级传入，不能放在 `extra_body` 中。
</Tip>

## 快速开始 <span id="abe0ee0bfd5dc" /> <span id="b056ab78e5blg" />

在处理正式任务前，使用测试模型`batch-test-model`进行全链路闭环测试。该模型跳过推理过程，直接返回固定的成功响应，用于验证API调用链路和数据格式是否正确。

<Note>
  <strong>测试模型（batch-test-model）的限制：</strong>

  - 测试文件需满足[输入文件要求](/zh/model-studio/batch-interfaces-compatible-with-openai#0214fa4f9dxb3)，且文件大小不超过 <strong>1 MB</strong>，行数不超过<strong>100行</strong>。
  - 并发限制：最大并行任务数 <strong>2 个</strong>。
  - 费用：测试模型不产生模型推理费用。
</Note>

### 第 1 步：准备输入文件 <span id="a687b7c50ej0t" /> <span id="eb771197c0u9g" />

准备一个名为[test\_model.jsonl](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250403/ilveat/test_model.jsonl)的文件，内容如下：

```json
{"custom_id":"1","method":"POST","url":"/v1/chat/ds-test","body":{"model":"batch-test-model","messages":[{"role":"system","content":"You are a helpful assistant."},{"role":"user","content":"你好！有什么可以帮助你的吗？"}]}}
{"custom_id":"2","method":"POST","url":"/v1/chat/ds-test","body":{"model":"batch-test-model","messages":[{"role":"system","content":"You are a helpful assistant."},{"role":"user","content":"What is 2+2?"}]}}
```

多模态模型（如 qwen3-vl-plus）支持文件 URL、Base64 编码传入方式：

```json
{"custom_id":"image-url","method":"POST","url":"/v1/chat/completions","body":{"model":"qwen3-vl-plus","messages":[{"role":"user","content":[{"type":"image_url","image_url":{"url":"https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"}},{"type":"text","text":"请描述这张图片"}]}]}}
{"custom_id":"image-base64","method":"POST","url":"/v1/chat/completions","body":{"model":"qwen3-vl-plus","messages":[{"role":"user","content":[{"type":"image_url","image_url":{"url":"data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEA8ADwAAD..."}},{"type":"text","text":"请描述这张图片"}]}]}}
```

### 第 2 步：运行代码 <span id="826aaa760a1qd" /> <span id="5cd01fa5a89l8" />

根据使用的编程语言，选择以下示例代码并保存到输入文件的同一目录下，然后运行。代码将完成文件上传、创建任务、轮询状态和下载结果的完整流程。

> 如需调整文件路径或其他参数，请根据实际情况修改代码。

<Note>
  <strong>复用已有文件 ID：</strong>文件上传后返回的 ID（如 `file-batch-xxx`）可重复使用。如果输入内容不变，无需每次重新上传，直接用已有 ID 创建任务即可：

  ```python
  batch = client.batches.create(
      input_file_id="file-batch-xxx",  # 直接使用已有文件 ID，无需重新上传
      endpoint="/v1/chat/completions",
      completion_window="24h"
  )
  ```

  可通过 `client.files.list(purpose="batch")` 接口查询已上传的 Batch 文件 ID。
</Note>

<Accordion title="示例代码">
  <CodeGroup dropdown>
    ```python OpenAI Python SDK expandable
    import os
    from pathlib import Path
    from openai import OpenAI
    import time

    # 初始化客户端
    client = OpenAI(
        # 若没有配置环境变量，可用阿里云百炼API Key将下行替换为：api_key="sk-xxx"，但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
        # 新加坡和北京地域的API Key不同。
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
        # 注意：切换地域时，API Key也需要对应更换
        base_url="https://dashscope.aliyuncs.com/compatible-mode/v1"  # 阿里云百炼服务的base_url
    )

    def upload_file(file_path):
        print(f"正在上传包含请求信息的JSONL文件...")
        file_object = client.files.create(file=Path(file_path), purpose="batch")
        print(f"文件上传成功。得到文件ID: {file_object.id}\n")
        return file_object.id

    def create_batch_job(input_file_id):
        print(f"正在基于文件ID，创建Batch任务...")
        # 请注意:此处endpoint参数值需和输入文件中的url字段保持一致.测试模型(batch-test-model)填写/v1/chat/ds-test,向量模型填写/v1/embeddings,其他模型填写/v1/chat/completions
        batch = client.batches.create(input_file_id=input_file_id, endpoint="/v1/chat/ds-test", completion_window="24h")
        print(f"Batch任务创建完成。 得到Batch任务ID: {batch.id}\n")
        return batch.id

    def check_job_status(batch_id):
        print(f"正在检查Batch任务状态...")
        batch = client.batches.retrieve(batch_id=batch_id)
        print(f"Batch任务状态: {batch.status}\n")
        return batch.status

    def get_output_id(batch_id):
        print(f"正在获取Batch任务中执行成功请求的输出文件ID...")
        batch = client.batches.retrieve(batch_id=batch_id)
        print(f"输出文件ID: {batch.output_file_id}\n")
        return batch.output_file_id

    def get_error_id(batch_id):
        print(f"正在获取Batch任务中执行错误请求的输出文件ID...")
        batch = client.batches.retrieve(batch_id=batch_id)
        print(f"错误文件ID: {batch.error_file_id}\n")
        return batch.error_file_id

    def download_results(output_file_id, output_file_path):
        print(f"正在打印并下载Batch任务的请求成功结果...")
        content = client.files.content(output_file_id)
        # 打印部分内容以供测试
        print(f"打印请求成功结果的前1000个字符内容: {content.text[:1000]}...\n")
        # 保存结果文件至本地
        content.write_to_file(output_file_path)
        print(f"完整的输出结果已保存至本地输出文件result.jsonl\n")

    def download_errors(error_file_id, error_file_path):
        print(f"正在打印并下载Batch任务的请求失败信息...")
        content = client.files.content(error_file_id)
        # 打印部分内容以供测试
        print(f"打印请求失败信息的前1000个字符内容: {content.text[:1000]}...\n")
        # 保存错误信息文件至本地
        content.write_to_file(error_file_path)
        print(f"完整的请求失败信息已保存至本地错误文件error.jsonl\n")

    def main():
        # 文件路径
        input_file_path = "test_model.jsonl"  # 可替换为您的输入文件路径
        output_file_path = "result.jsonl"  # 可替换为您的输出文件路径
        error_file_path = "error.jsonl"  # 可替换为您的错误文件路径
        try:
            # Step 1: 上传包含请求信息的JSONL文件,得到输入文件ID,如果您需要输入OSS文件,可将下行替换为：input_file_id = "实际的OSS文件URL或资源标识符"
            input_file_id = upload_file(input_file_path)
            # Step 2: 基于输入文件ID,创建Batch任务
            batch_id = create_batch_job(input_file_id)
            # Step 3: 检查Batch任务状态直到结束
            status = ""
            while status not in ["completed", "failed", "expired", "cancelled"]:
                status = check_job_status(batch_id)
                print(f"等待任务完成...")
                time.sleep(10)  # 等待10秒后再次查询状态
            # 如果任务失败,则打印错误信息并退出
            if status == "failed":
                batch = client.batches.retrieve(batch_id)
                print(f"Batch任务失败。错误信息为:{batch.errors}\n")
                print(f"参见错误码文档: https://help.aliyun.com/zh/model-studio/developer-reference/error-code")
                return
            # Step 4: 下载结果：如果输出文件ID不为空,则打印请求成功结果的前1000个字符内容，并下载完整的请求成功结果到本地输出文件;
            # 如果错误文件ID不为空,则打印请求失败信息的前1000个字符内容,并下载完整的请求失败信息到本地错误文件.
            output_file_id = get_output_id(batch_id)
            if output_file_id:
                download_results(output_file_id, output_file_path)
            error_file_id = get_error_id(batch_id)
            if error_file_id:
                download_errors(error_file_id, error_file_path)
                print(f"参见错误码文档: https://help.aliyun.com/zh/model-studio/developer-reference/error-code")
        except Exception as e:
            print(f"An error occurred: {e}")
            print(f"参见错误码文档: https://help.aliyun.com/zh/model-studio/developer-reference/error-code")

    if __name__ == "__main__":
        main()
    ```

    ```javascript OpenAI Node.js SDK expandable
    /**
     * 阿里云百炼 Batch API 测试 - 使用 OpenAI Node.js SDK
     *
     * 安装依赖：npm install openai
     * 运行：node test-nodejs.js
     */

    const OpenAI = require('openai');
    const fs = require('fs');

    // 北京地域的 Base URL
    const BASE_URL = 'https://dashscope.aliyuncs.com/compatible-mode/v1';
    // 如果使用新加坡地域，使用以下 URL：
    // const BASE_URL = 'https://dashscope-intl.aliyuncs.com/compatible-mode/v1';

    const apiKey = process.env.DASHSCOPE_API_KEY;
    if (!apiKey) {
        console.error('错误: 请设置环境变量 DASHSCOPE_API_KEY');
        process.exit(1);
    }

    // 初始化客户端
    const client = new OpenAI({
        apiKey: apiKey,
        baseURL: BASE_URL
    });

    async function sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

    async function main() {
        try {
            console.log('=== 开始 Batch API 测试 ===\n');

            // Step 1: 上传文件
            console.log('步骤 1: 上传包含请求信息的 JSONL 文件...');
            const fileStream = fs.createReadStream('test_model.jsonl');
            const fileObject = await client.files.create({
                file: fileStream,
                purpose: 'batch'
            });
            const fileId = fileObject.id;
            console.log(`✓ 文件上传成功，文件ID: ${fileId}\n`);

            // Step 2: 创建 Batch 任务
            console.log('步骤 2: 创建 Batch 任务...');
            const batch = await client.batches.create({
                input_file_id: fileId,
                endpoint: '/v1/chat/ds-test',  // 测试模型使用 /v1/chat/ds-test
                completion_window: '24h'
            });
            const batchId = batch.id;
            console.log(`✓ Batch 任务创建成功，任务ID: ${batchId}\n`);

            // Step 3: 轮询任务状态
            console.log('步骤 3: 等待任务完成...');
            let status = batch.status;
            let pollCount = 0;
            let latestBatch = batch;

            while (!['completed', 'failed', 'expired', 'cancelled'].includes(status)) {
                await sleep(10000); // 等待 10 秒
                latestBatch = await client.batches.retrieve(batchId);
                status = latestBatch.status;
                pollCount++;
                console.log(`  [${pollCount}] 任务状态: ${status}`);
            }

            console.log(`\n✓ 任务已完成，最终状态: ${status}\n`);

            // Step 4: 处理结果
            if (status === 'completed') {
                console.log('步骤 4: 下载结果文件...');

                // 下载成功结果
                const outputFileId = latestBatch.output_file_id;
                if (outputFileId) {
                    console.log(`  输出文件ID: ${outputFileId}`);
                    const content = await client.files.content(outputFileId);
                    const text = await content.text();
                    console.log('\n--- 成功请求结果（前 500 字符）---');
                    console.log(text.substring(0, Math.min(500, text.length)));
                    console.log('...\n');
                }

                // 下载错误文件（如有）
                const errorFileId = latestBatch.error_file_id;
                if (errorFileId) {
                    console.log(`  错误文件ID: ${errorFileId}`);
                    const errorContent = await client.files.content(errorFileId);
                    const errorText = await errorContent.text();
                    console.log('\n--- 错误信息 ---');
                    console.log(errorText);
                }

                console.log('\n=== 测试成功完成 ===');
            } else if (status === 'failed') {
                console.error('\n✗ Batch 任务失败');
                if (latestBatch.errors) {
                    console.error('错误信息:', latestBatch.errors);
                }
                console.error('\n请参考错误码文档: https://help.aliyun.com/zh/model-studio/developer-reference/error-code');
            } else {
                console.log(`\n任务状态: ${status}`);
            }

        } catch (error) {
            console.error('发生错误:', error.message);
            console.error(error);
        }
    }

    main();
    ```

    ```java Java（HTTP） expandable
    import java.io.*;
    import java.net.HttpURLConnection;
    import java.net.URL;
    import java.nio.file.Files;
    import java.nio.file.Paths;
    import java.util.Scanner;

    /**
     * 阿里云百炼 Batch API 测试 - 使用 HTTP API 调用
     *
     * 前置条件：
     * 1. 确保已经设置环境变量 DASHSCOPE_API_KEY
     * 2. 准备好测试文件 test_model.jsonl（在项目根目录）
     *
     * 地域配置说明：
     * - 北京地域：https://dashscope.aliyuncs.com/compatible-mode/v1
     * - 新加坡地域：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
     */
    public class BatchAPITest {

        // 北京地域的 Base URL（默认）
        private static final String BASE_URL = "https://dashscope.aliyuncs.com/compatible-mode/v1";
        // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
        // private static final String BASE_URL = "https://dashscope-intl.aliyuncs.com/compatible-mode/v1";

        private static String API_KEY;

        public static void main(String[] args) throws Exception {
            // 从环境变量获取 API Key
            API_KEY = System.getenv("DASHSCOPE_API_KEY");
            if (API_KEY == null || API_KEY.isEmpty()) {
                System.err.println("错误: 请设置环境变量 DASHSCOPE_API_KEY");
                System.exit(1);
            }

            System.out.println("=== 开始 Batch API 测试 ===\n");

            try {
                // Step 1: 上传文件
                System.out.println("步骤 1: 上传包含请求信息的 JSONL 文件...");
                String fileId = uploadFile("test_model.jsonl");
                System.out.println("✓ 文件上传成功，文件ID: " + fileId + "\n");

                // Step 2: 创建 Batch 任务
                System.out.println("步骤 2: 创建 Batch 任务...");
                String batchId = createBatch(fileId);
                System.out.println("✓ Batch 任务创建成功，任务ID: " + batchId + "\n");

                // Step 3: 轮询任务状态
                System.out.println("步骤 3: 等待任务完成...");
                String status = "";
                int pollCount = 0;

                while (!isTerminalStatus(status)) {
                    Thread.sleep(10000); // 等待 10 秒
                    String batchInfo = getBatch(batchId);
                    status = parseStatus(batchInfo);
                    pollCount++;
                    System.out.println("  [" + pollCount + "] 任务状态: " + status);

                    // Step 4: 如果完成，下载结果
                    if ("completed".equals(status)) {
                        System.out.println("\n✓ 任务已完成！\n");
                        System.out.println("步骤 4: 下载结果文件...");

                        String outputFileId = parseOutputFileId(batchInfo);
                        if (outputFileId != null && !outputFileId.isEmpty()) {
                            System.out.println("  输出文件ID: " + outputFileId);
                            String content = getFileContent(outputFileId);
                            System.out.println("\n--- 成功结果（前 500 字符）---");
                            System.out.println(content.substring(0, Math.min(500, content.length())));
                            System.out.println("...\n");
                        }

                        String errorFileId = parseErrorFileId(batchInfo);
                        if (errorFileId != null && !errorFileId.isEmpty() && !"null".equals(errorFileId)) {
                            System.out.println("  错误文件ID: " + errorFileId);
                            String errorContent = getFileContent(errorFileId);
                            System.out.println("\n--- 错误信息 ---");
                            System.out.println(errorContent);
                        }

                        System.out.println("\n=== 测试成功完成 ===");
                        break;
                    } else if ("failed".equals(status)) {
                        System.err.println("\n✗ Batch 任务失败");
                        System.err.println("任务信息: " + batchInfo);
                        System.err.println("\n请参考错误码文档: https://help.aliyun.com/zh/model-studio/developer-reference/error-code");
                        break;
                    } else if ("expired".equals(status) || "cancelled".equals(status)) {
                        System.out.println("\n任务状态: " + status);
                        break;
                    }
                }

            } catch (Exception e) {
                System.err.println("发生错误: " + e.getMessage());
                e.printStackTrace();
            }
        }

        /**
         * 上传文件
         */
        private static String uploadFile(String filePath) throws Exception {
            String boundary = "----WebKitFormBoundary" + System.currentTimeMillis();
            URL url = new URL(BASE_URL + "/files");
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setDoOutput(true);
            conn.setRequestMethod("POST");
            conn.setRequestProperty("Authorization", "Bearer " + API_KEY);
            conn.setRequestProperty("Content-Type", "multipart/form-data; boundary=" + boundary);

            try (DataOutputStream out = new DataOutputStream(conn.getOutputStream())) {
                // 添加 purpose 字段
                out.writeBytes("--" + boundary + "\r\n");
                out.writeBytes("Content-Disposition: form-data; name=\"purpose\"\r\n\r\n");
                out.writeBytes("batch\r\n");

                // 添加文件
                out.writeBytes("--" + boundary + "\r\n");
                out.writeBytes("Content-Disposition: form-data; name=\"file\"; filename=\"" + filePath + "\"\r\n");
                out.writeBytes("Content-Type: application/octet-stream\r\n\r\n");

                byte[] fileBytes = Files.readAllBytes(Paths.get(filePath));
                out.write(fileBytes);
                out.writeBytes("\r\n");
                out.writeBytes("--" + boundary + "--\r\n");
            }

            String response = readResponse(conn);
            return parseField(response, "\"id\":\s*\"([^\"]+)\"");
        }

        /**
         * 创建 Batch 任务
         */
        private static String createBatch(String fileId) throws Exception {
            String jsonBody = String.format(
                "{\"input_file_id\":\"%s\",\"endpoint\":\"/v1/chat/ds-test\",\"completion_window\":\"24h\"}",
                fileId
            );

            String response = sendRequest("POST", "/batches", jsonBody);
            return parseField(response, "\"id\":\s*\"([^\"]+)\"");
        }

        /**
         * 获取 Batch 任务信息
         */
        private static String getBatch(String batchId) throws Exception {
            return sendRequest("GET", "/batches/" + batchId, null);
        }

        /**
         * 获取文件内容
         */
        private static String getFileContent(String fileId) throws Exception {
            return sendRequest("GET", "/files/" + fileId + "/content", null);
        }

        /**
         * 发送 HTTP 请求
         */
        private static String sendRequest(String method, String path, String jsonBody) throws Exception {
            URL url = new URL(BASE_URL + path);
            HttpURLConnection conn = (HttpURLConnection) url.openConnection();
            conn.setRequestMethod(method);
            conn.setRequestProperty("Authorization", "Bearer " + API_KEY);

            if (jsonBody != null) {
                conn.setDoOutput(true);
                conn.setRequestProperty("Content-Type", "application/json");
                try (OutputStream os = conn.getOutputStream()) {
                    os.write(jsonBody.getBytes("UTF-8"));
                }
            }

            return readResponse(conn);
        }

        /**
         * 读取响应
         */
        private static String readResponse(HttpURLConnection conn) throws Exception {
            int responseCode = conn.getResponseCode();
            InputStream is = (responseCode < 400) ? conn.getInputStream() : conn.getErrorStream();

            try (Scanner scanner = new Scanner(is, "UTF-8").useDelimiter("\\A")) {
                return scanner.hasNext() ? scanner.next() : "";
            }
        }

        /**
         * 解析 JSON 字段（简单实现）
         */
        private static String parseField(String json, String regex) {
            java.util.regex.Pattern pattern = java.util.regex.Pattern.compile(regex);
            java.util.regex.Matcher matcher = pattern.matcher(json);
            return matcher.find() ? matcher.group(1) : null;
        }

        private static String parseStatus(String json) {
            return parseField(json, "\"status\":\s*\"([^\"]+)\"");
        }

        private static String parseOutputFileId(String json) {
            return parseField(json, "\"output_file_id\":\s*\"([^\"]+)\"");
        }

        private static String parseErrorFileId(String json) {
            return parseField(json, "\"error_file_id\":\s*\"([^\"]+)\"");
        }

        /**
         * 判断是否为终止状态
         */
        private static boolean isTerminalStatus(String status) {
            return "completed".equals(status)
                || "failed".equals(status)
                || "expired".equals(status)
                || "cancelled".equals(status);
        }
    }
    ```

    ```bash curl (HTTP) expandable
    #!/bin/bash
    # 阿里云百炼 Batch API 测试 - 使用 curl
    #
    # 前置条件：
    # 1. 确保已经设置环境变量 DASHSCOPE_API_KEY
    # 2. 准备好测试文件 test_model.jsonl（在当前目录）
    #
    # 地域配置说明：
    # - 北京地域：https://dashscope.aliyuncs.com/compatible-mode/v1
    # - 新加坡地域：https://dashscope-intl.aliyuncs.com/compatible-mode/v1

    API_KEY="${DASHSCOPE_API_KEY}"
    BASE_URL="https://dashscope.aliyuncs.com/compatible-mode/v1"

    # 如果使用新加坡地域，请将 BASE_URL 替换为：
    # BASE_URL="https://dashscope-intl.aliyuncs.com/compatible-mode/v1"

    # 检查 API Key
    if [ -z "$API_KEY" ]; then
        echo "错误: 请设置环境变量 DASHSCOPE_API_KEY"
        exit 1
    fi

    echo "=== 开始 Batch API 测试 ==="
    echo ""

    # Step 1: 上传文件
    echo "步骤 1: 上传包含请求信息的 JSONL 文件..."
    UPLOAD_RESPONSE=$(curl -s -X POST "${BASE_URL}/files" \
      -H "Authorization: Bearer ${API_KEY}" \
      -F 'file=@test_model.jsonl' \
      -F 'purpose=batch')

    FILE_ID=$(echo $UPLOAD_RESPONSE | grep -o '"id":"[^"]*"' | head -1 | cut -d'"' -f4)
    echo "✓ 文件上传成功，文件ID: ${FILE_ID}"
    echo ""

    # Step 2: 创建 Batch 任务
    echo "步骤 2: 创建 Batch 任务..."
    BATCH_RESPONSE=$(curl -s -X POST "${BASE_URL}/batches" \
      -H "Authorization: Bearer ${API_KEY}" \
      -H "Content-Type: application/json" \
      -d "{\"input_file_id\":\"${FILE_ID}\",\"endpoint\":\"/v1/chat/ds-test\",\"completion_window\":\"24h\"}")

    BATCH_ID=$(echo $BATCH_RESPONSE | grep -o '"id":"[^"]*"' | head -1 | cut -d'"' -f4)
    echo "✓ Batch 任务创建成功，任务ID: ${BATCH_ID}"
    echo ""

    # Step 3: 轮询任务状态
    echo "步骤 3: 等待任务完成..."
    STATUS=""
    POLL_COUNT=0

    while [[ "$STATUS" != "completed" && "$STATUS" != "failed" && "$STATUS" != "expired" && "$STATUS" != "cancelled" ]]; do
        sleep 10
        BATCH_INFO=$(curl -s -X GET "${BASE_URL}/batches/${BATCH_ID}" \
          -H "Authorization: Bearer ${API_KEY}")
        STATUS=$(echo $BATCH_INFO | grep -o '"status":"[^"]*"' | cut -d'"' -f4)
        POLL_COUNT=$((POLL_COUNT + 1))
        echo "  [${POLL_COUNT}] 任务状态: ${STATUS}"
    done

    echo ""
    echo "✓ 任务已完成，最终状态: ${STATUS}"
    echo ""

    # Step 4: 下载结果
    if [[ "$STATUS" == "completed" ]]; then
        echo "步骤 4: 下载结果文件..."

        OUTPUT_FILE_ID=$(echo $BATCH_INFO | grep -o '"output_file_id":"[^"]*"' | cut -d'"' -f4)
        if [[ -n "$OUTPUT_FILE_ID" && "$OUTPUT_FILE_ID" != "null" ]]; then
            echo "  输出文件ID: ${OUTPUT_FILE_ID}"

            RESULT_CONTENT=$(curl -s -X GET "${BASE_URL}/files/${OUTPUT_FILE_ID}/content" \
              -H "Authorization: Bearer ${API_KEY}")

            echo ""
            echo "--- 成功结果（前 500 字符）---"
            echo "${RESULT_CONTENT:0:500}"
            echo "..."
            echo ""
        fi

        ERROR_FILE_ID=$(echo $BATCH_INFO | grep -o '"error_file_id":"[^"]*"' | cut -d'"' -f4)
        if [[ -n "$ERROR_FILE_ID" && "$ERROR_FILE_ID" != "null" ]]; then
            echo "  错误文件ID: ${ERROR_FILE_ID}"

            ERROR_CONTENT=$(curl -s -X GET "${BASE_URL}/files/${ERROR_FILE_ID}/content" \
              -H "Authorization: Bearer ${API_KEY}")

            echo ""
            echo "--- 错误信息 ---"
            echo "${ERROR_CONTENT}"
        fi

        echo ""
        echo "=== 测试成功完成 ==="
    elif [[ "$STATUS" == "failed" ]]; then
        echo ""
        echo "✗ Batch 任务失败"
        echo "任务信息: ${BATCH_INFO}"
        echo ""
        echo "请参考错误码文档: https://help.aliyun.com/zh/model-studio/developer-reference/error-code"
    else
        echo ""
        echo "任务状态: ${STATUS}"
    fi
    ```
  </CodeGroup>
</Accordion>

### 第 3 步： 验证测试结果 <span id="4b17251bccf4r" /> <span id="863a7955b8e7y" />

任务成功完成后，结果文件`result.jsonl`包含固定响应`{"content":"This is a test result."}`：

```json
{"id":"a2b1ae25-21f4-4d9a-8634-99a29926486c","custom_id":"1","response":{"status_code":200,"request_id":"a2b1ae25-21f4-4d9a-8634-99a29926486c","body":{"created":1743562621,"usage":{"completion_tokens":6,"prompt_tokens":20,"total_tokens":26},"model":"batch-test-model","id":"chatcmpl-bca7295b-67c3-4b1f-8239-d78323bb669f","choices":[{"finish_reason":"stop","index":0,"message":{"content":"This is a test result."}}],"object":"chat.completion"}},"error":null}
{"id":"39b74f09-a902-434f-b9ea-2aaaeebc59e0","custom_id":"2","response":{"status_code":200,"request_id":"39b74f09-a902-434f-b9ea-2aaaeebc59e0","body":{"created":1743562621,"usage":{"completion_tokens":6,"prompt_tokens":20,"total_tokens":26},"model":"batch-test-model","id":"chatcmpl-1e32a8ba-2b69-4dc4-be42-e2897eac9e84","choices":[{"finish_reason":"stop","index":0,"message":{"content":"This is a test result."}}],"object":"chat.completion"}},"error":null}
```

## 执行正式任务 <span id="bad4ef1522g5k" /> <span id="759dd0eccesua" />

### 输入文件要求 <span id="0214fa4f9dxb3" /> <span id="bbf6655574dbn" />

- <strong>格式</strong>：UTF-8 编码的 JSONL（每行一个独立JSON对象）
- <strong>规模限制</strong>：单文件最多 50,000 个请求，且不超过 500 MB
- <strong>单行限制</strong>：每个JSON对象不超过 6 MB，且不超过模型上下文长度
- <strong>一致性要求</strong>：同一文件内所有请求须使用相同的模型及思考模式（如适用）
- <strong>唯一标识</strong>：每个请求必须包含文件内唯一的 custom\_id 字段，用于匹配请求与结果

<strong>场景1：文本对话</strong>

示例文件内容：

```json
{"custom_id":"1","method":"POST","url":"/v1/chat/completions","body":{"model":"qwen-plus","messages":[{"role":"system","content":"You are a helpful assistant."},{"role":"user","content":"你好！有什么可以帮助你的吗？"}]}}
{"custom_id":"2","method":"POST","url":"/v1/chat/completions","body":{"model":"qwen-plus","messages":[{"role":"system","content":"You are a helpful assistant."},{"role":"user","content":"What is 2+2?"}]}}
```

<strong>场景2：图像与视频理解</strong>

多模态模型（如 qwen3-vl-plus）支持文件 URL、Base64 编码传入方式。

```json
{"custom_id":"image-url","method":"POST","url":"/v1/chat/completions","body":{"model":"qwen3-vl-plus","messages":[{"role":"user","content":[{"type":"image_url","image_url":{"url":"https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"}},{"type":"text","text":"请描述这张图片"}]}]}}
{"custom_id":"image-base64","method":"POST","url":"/v1/chat/completions","body":{"model":"qwen3-vl-plus","messages":[{"role":"user","content":[{"type":"image_url","image_url":{"url":"data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEA8ADwAAD..."}},{"type":"text","text":"请描述这张图片"}]}]}}
{"custom_id":"video-url","method":"POST","url":"/v1/chat/completions","body":{"model":"qwen3-vl-plus","messages":[{"role":"user","content":[{"type":"video","video":"https://example.com/video.mp4"},{"type":"text","text":"描述这个视频"}]}]}}
{"custom_id":"video-base64","method":"POST","url":"/v1/chat/completions","body":{"model":"qwen3-vl-plus","messages":[{"role":"user","content":[{"type":"video","video":["data:image/jpeg;base64,{frame1}","data:image/jpeg;base64,{frame2}","data:image/jpeg;base64,{frame3}"]},{"type":"text","text":"描述这个视频"}]}]}}
{"custom_id":"multi-image-url","method":"POST","url":"/v1/chat/completions","body":{"model":"qwen3-vl-plus","messages":[{"role":"user","content":[{"type":"image_url","image_url":{"url":"https://example.com/image1.jpg"}},{"type":"image_url","image_url":{"url":"https://example.com/image2.jpg"}},{"type":"text","text":"对比这两张图片"}]}]}}
{"custom_id":"multi-image-base64","method":"POST","url":"/v1/chat/completions","body":{"model":"qwen3-vl-plus","messages":[{"role":"user","content":[{"type":"image_url","image_url":{"url":"data:image/jpeg;base64,{image1_base64}"}},{"type":"image_url","image_url":{"url":"data:image/jpeg;base64,{image2_base64}"}},{"type":"text","text":"对比这两张图片"}]}]}}
```

> 示例中的 Base64 字符串已省略，使用下方 Python 代码生成完整编码即可。

<Accordion title="传入 Base64 编码字符串（以图像为例）">
  1. 将本地文件转换为 Base64 编码：

  ```python
  #  编码函数： 将本地文件转换为 Base64 编码的字符串
  import base64
  def encode_image(image_path):
      with open(image_path, "rb") as image_file:
          return base64.b64encode(image_file.read()).decode("utf-8")

  # 将xxxx/eagle.png替换为你本地图像的绝对路径
  base64_image = encode_image("xxx/eagle.png")
  ```

  2. 构建[Data URL](https://www.rfc-editor.org/rfc/rfc2397)格式：`data:[MIME_type];base64,{base64_image}`；

     1. `MIME_type`需替换为实际的媒体类型，确保与`MIME Type` 的值匹配（如`image/jpeg`、`image/png`）；
     2. `base64_image`为上一步生成的 Base64 字符串。
</Accordion>

<Note>
  完整说明（包括文件限制、MIME 类型、编码方法）请参见[传入本地文件（Base64 编码或文件路径）](/zh/model-studio/vision#d987f8de5395x)。
</Note>

<strong>Batch 推理 JSONL 文件生成工具</strong>

Batch 推理需要构造 JSONL 文件，每行为一个独立请求。以下 Python 脚本可直接运行生成符合格式的 JSONL 文件。

### 使用方式 <span id="h2-usage-china" /> <span id="sec-usage-china" />

1. 将以下代码保存为 `generate_batch_jsonl.py`
2. 修改脚本中 `TASKS` 列表的请求内容
3. 运行 `python generate_batch_jsonl.py`
4. 生成的 `batch_input.jsonl` 文件可直接用于 Batch 推理任务

#### 文本生成模型 JSONL 生成脚本 <span id="h2-script-china" /> <span id="sec-script-china" />

```python expandable
#!/usr/bin/env python3
"""
Batch 推理 JSONL 文件生成工具

支持的地域与模型请参见上方“适用范围”。

文件大小限制：单行 ≤6MB，总文件 ≤500MB，建议 ≤10000 行

使用方式：
  1. 修改下方 TASKS 列表
  2. 运行: python generate_batch_jsonl.py
  3. 输出: batch_input.jsonl
"""
import json

# ═══════════════════════════════════════════════════════
# 配置区：修改以下内容来生成您需要的 JSONL
# ═══════════════════════════════════════════════════════

MODEL = "qwen-max"          # 选择模型（参见上方“适用范围”）
ENABLE_THINKING = False     # 是否开启思考模式（仅部分模型支持）
THINKING_BUDGET = 50        # 思考 token 预算（思考模式下生效）

# 任务列表：每个 dict 代表一条用户消息
TASKS = [
    {"role": "user", "content": "你好！请介绍一下自己。"},
    {"role": "user", "content": "What is 2+2?"},
    {"role": "user", "content": "请用Python写一个快速排序算法"},
]

# ═══════════════════════════════════════════════════════
# 以下为生成逻辑，一般无需修改
# ═══════════════════════════════════════════════════════

def build_request(custom_id: str, model: str, messages: list,
                  enable_thinking: bool = False, thinking_budget: int = 50) -> dict:
    """构造单条 Batch 请求"""
    body = {"model": model, "messages": messages}
    if enable_thinking:
        body["enable_thinking"] = True
        body["thinking_budget"] = thinking_budget
        body["stream"] = True  # 思考模式需开启 stream
    return {
        "custom_id": custom_id,
        "method": "POST",
        "url": "/v1/chat/completions",
        "body": body
    }

def main():
    output_file = "batch_input.jsonl"
    with open(output_file, "w", encoding="utf-8") as f:
        for i, task in enumerate(TASKS, 1):
            messages = [
                {"role": "system", "content": "You are a helpful assistant."},
                task
            ]
            req = build_request(
                custom_id=f"request-{i}",
                model=MODEL,
                messages=messages,
                enable_thinking=ENABLE_THINKING,
                thinking_budget=THINKING_BUDGET
            )
            f.write(json.dumps(req, ensure_ascii=False) + "\n")
    print(f"已生成 {output_file}，共 {len(TASKS)} 条请求")

if __name__ == "__main__":
    main()
```

#### 多模态模型请求格式 <span id="h2-mm-china" /> <span id="sec-multimodal-china" />

多模态模型的 `content` 字段为数组格式，包含媒体对象和文本提问。每行请求中的媒体URL需为同一类型（图片、视频或音频）。将上方脚本中的 `TASKS` 替换为以下格式即可：

```python
# 多模态模型配置示例
MODEL = "qwen3-vl-plus"       # 多模态模型

# 多模态任务：content 为数组格式
TASKS = [
    {"role": "user", "content": [
        {"type": "image_url", "image_url": {"url": "https://example.com/image1.jpg"}},
        {"type": "image_url", "image_url": {"url": "https://example.com/image2.jpg"}},
        {"type": "text", "text": "这些图描绘了什么内容？"}
    ]},
]

# 注意：
# - qwen3-vl-plus 不使用 system 角色，thinking_budget 设置为 500
# - 其他多模态模型包含 system 角色，thinking_budget 设置为 50
# - 思考模式需额外设置 ENABLE_THINKING = True
```

#### 向量模型请求格式 <span id="h2-emb-china" /> <span id="sec-embedding-china" />

向量模型使用 `/v1/embeddings` 接口，请求格式不同于文本生成模型。可使用以下独立脚本生成：

```python
#!/usr/bin/env python3
"""向量模型 Batch JSONL 生成（使用 /v1/embeddings 接口）"""
import json

MODEL = "text-embedding-v3"  # 向量模型，可选型号参见上方“适用范围”

TEXTS = [
    "衣服的质量杠杠的，很漂亮，不枉我等了这么久啊，喜欢，以后还来这里买。",
    "风急天高猿啸哀",
    "The quick brown fox jumps over the lazy dog",
]

output_file = "batch_embedding.jsonl"
with open(output_file, "w", encoding="utf-8") as f:
    for i, text in enumerate(TEXTS, 1):
        req = {
            "custom_id": f"emb-{i}",
            "method": "POST",
            "url": "/v1/embeddings",
            "body": {"model": MODEL, "input": text, "encoding_format": "float"}
        }
        f.write(json.dumps(req, ensure_ascii=False) + "\n")
print(f"已生成 {output_file}，共 {len(TEXTS)} 条请求")
```

### 1. 修改输入文件 <span id="b5ec99a5c1lko" /> <span id="b937fd69b79n2" />

- 可直接修改用于测试的 `test_model.jsonl` 文件，将 model 参数设置为目标正式模型，并设置URL字段：

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50.78%" }} /><col style={{ width: "49.22%" }} /></colgroup><thead><tr><th><p><strong>模型类型</strong></p></th><th><p><strong>url</strong></p></th></tr></thead><tbody><tr><td><p>文本生成/多模态模型</p></td><td><p><code>/v1/chat/completions</code></p></td></tr><tr><td><p>向量模型</p></td><td><p><code>/v1/embeddings</code></p></td></tr></tbody></table>
- 或使用上方的“JSONL 批量生成工具”为正式任务生成一个新的文件。关键是确保 `model` 和 `url` 字段正确。

### 2. 修改快速开始的代码 <span id="9e8d214ec6w42" /> <span id="c9d67eab84owy" />

1. 输入文件路径更改为您的文件名
2. 将 endpoint 参数值修改为与输入文件中URL字段一致的值

### 3. 运行代码并等待结果 <span id="38f1a37efdb21" /> <span id="d180ad81a8g6y" />

任务完成后，成功请求的结果保存在本地 result.jsonl 文件中。如有请求失败，错误详情保存在 error.jsonl 文件中。

- 成功结果（`output_file_id`）：每一行对应一个成功的原始请求，包含 `custom_id` 和 `response`。

```json
{"id":"3a5c39d5-3981-4e4c-97f2-e0e821893f03","custom_id":"req-001","response":{"status_code":200,"request_id":"3a5c39d5-3981-4e4c-97f2-e0e821893f03","body":{"created":1768306034,"usage":{"completion_tokens":654,"prompt_tokens":14,"total_tokens":668},"model":"qwen-plus","id":"chatcmpl-3a5c39d5-3981-4e4c-97f2-e0e821893f03","choices":[{"finish_reason":"stop","index":0,"message":{"role":"assistant","content":"你好！杭州西湖是中国著名的风景名胜区，位于浙江省杭州市西部，因此得名“西湖”。它是中国十大风景名胜之一，也是世界文化遗产（2011年被联合国教科文组织列入《世界遗产名录》），以其秀丽的自然风光与深厚的人文底蕴闻名于世。\n\n### 一、自然景观\n西湖三面环山，一面邻城，湖面面积约6.39平方公里，形似如意，碧波荡漾。湖中被孤山、白堤、苏堤、杨公堤等自然或人工分隔成多个水域，形成“一山二塔三岛三堤”的格局。\n\n主要景点包括：\n- **苏堤春晓**：北宋大文豪苏东坡任杭州知州时主持疏浚西湖，用挖出的淤泥堆筑成堤，后人称为“苏堤”。春天桃红柳绿，景色如画。\n- **断桥残雪**：位于白堤东端，是白蛇传中“断桥相会”的发生地，冬日雪后银装素裹，尤为著名。\n- **雷峰夕照**：雷峰塔在夕阳映照下金光熠熠，曾是“西湖十景”之一。\n- **三潭印月**：湖中小瀛洲上的三座石塔，中秋夜可在塔内点灯，月影、灯光、湖光交相辉映。\n- **平湖秋月**：位于白堤西端，是观赏湖上明月的绝佳地点。\n- **花港观鱼**：以赏花和观鱼著称，园内牡丹、锦鲤相映成趣。\n\n### 二、人文历史\n西湖不仅风景优美，还承载着丰富的历史文化：\n- 自唐宋以来，众多文人墨客如白居易、苏东坡、林逋、杨万里等在此留下诗篇。\n- 白居易曾主持修建“白堤”，疏浚西湖，造福百姓。\n- 西湖周边有众多古迹，如岳王庙（纪念民族英雄岳飞）、灵隐寺（千年古刹）、六和塔、龙井村（中国十大名茶龙井茶的产地）等。\n\n### 三、文化象征\n西湖被誉为“人间天堂”的代表，是中国传统山水美学的典范。它融合了自然美与人文美，体现了“天人合一”的哲学思想。许多诗词、绘画、戏曲都以西湖为题材，成为中国文化的重要符号。\n\n### 四、旅游建议\n- 最佳游览季节：春季（3-5月）桃红柳绿，秋季（9-11月）天高气爽。\n- 推荐方式：步行、骑行（环湖绿道）、乘船游湖。\n- 周边美食：西湖醋鱼、龙井虾仁、东坡肉、片儿川等。\n\n总之，杭州西湖不仅是一处自然美景，更是一座活着的文化博物馆，值得细细品味。如果你有机会到杭州，一定不要错过这个“淡妆浓抹总相宜”的人间仙境。"}}],"object":"chat.completion"}},"error":null}
{"id":"628312ba-172c-457d-ba7f-3e5462cc6899","custom_id":"req-002","response":{"status_code":200,"request_id":"628312ba-172c-457d-ba7f-3e5462cc6899","body":{"created":1768306035,"usage":{"completion_tokens":25,"prompt_tokens":18,"total_tokens":43},"model":"qwen-plus","id":"chatcmpl-628312ba-172c-457d-ba7f-3e5462cc6899","choices":[{"finish_reason":"stop","index":0,"message":{"role":"assistant","content":"春风拂柳绿，  \n夜雨润花红。  \n鸟语林间闹，  \n山川处处同。"}}],"object":"chat.completion"}},"error":null}
```

- 失败请求详情（`error_file_id`）：包含处理失败的请求行信息和错误原因，可参考[错误码](/zh/model-studio/batch-interfaces-compatible-with-openai#97e145aeabbwf)进行排查。

## 具体流程 <span id="fdd1a4b6d86id" /> <span id="bdf0b1d3a39it" />

Batch API使用流程分为四步：上传文件、创建任务、查询任务状态、下载结果。

<Note>
  如果数据已存储在阿里云OSS中，可跳过文件上传步骤，直接在创建 Batch 任务时使用OSS文件路径。详情请参见[使用OSS文件创建 Batch 任务](/zh/model-studio/batch-interfaces-compatible-with-openai#f667257850olj)。
</Note>

<Accordion title="1. 上传文件">
  创建 Batch 任务前，通过文件上传接口上传符合格式要求的 JSONL 文件，获取`file_id`。

  > 上传文件， `purpose` 必须是 `batch` 。

  <Note>
    <strong>复用已有文件 ID：</strong>文件上传后返回的 ID（如 `file-batch-xxx`）可重复使用。如果输入内容不变，无需每次重新上传，直接用已有 ID 创建任务即可：

    ```python
    batch = client.batches.create(
        input_file_id="file-batch-xxx",  # 直接使用已有文件 ID，无需重新上传
        endpoint="/v1/chat/completions",
        completion_window="24h"
    )
    ```

    可通过 `client.files.list(purpose="batch")` 接口查询已上传的 Batch 文件 ID。
  </Note>

  <Tabs>
    <Tab title="OpenAI Python SDK">
      #### 请求示例 <span id="eadaf3f1ca7z5" /> <span id="6042ecfd84t57" />

      ```python
      import os
      from pathlib import Path
      from openai import OpenAI

      client = OpenAI(
          # 若没有配置环境变量，可用阿里云百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
          # 新加坡和北京地域的API Key不同。
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
          # 注意：切换地域时，API Key也需要对应更换
          base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
      )

      # test.jsonl 是一个本地示例文件，purpose必须是batch
      file_object = client.files.create(file=Path("test.jsonl"), purpose="batch")

      print(file_object.model_dump_json())
      ```
    </Tab>

    <Tab title="OpenAI Node.js SDK">
      #### 请求示例 <span id="nodejs-request-title" /> <span id="nodejs-request-example" />

      ```javascript expandable
      /**
       * 阿里云百炼 Batch API - 上传文件
       *
       * 若没有配置环境变量，可在代码中硬编码API Key：apiKey: 'sk-xxx'
       * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
       * 新加坡和北京地域的API Key不同。
       *
       * 安装依赖：npm install openai
       */
      const OpenAI = require('openai');
      const fs = require('fs');

      // 北京地域配置（默认）
      const BASE_URL = 'https://dashscope.aliyuncs.com/compatible-mode/v1';
      // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
      // const BASE_URL = 'https://dashscope-intl.aliyuncs.com/compatible-mode/v1';
      // 注意：切换地域时，API Key也需要对应更换

      const apiKey = process.env.DASHSCOPE_API_KEY;
      if (!apiKey) {
          console.error('错误: 请设置环境变量 DASHSCOPE_API_KEY');
          console.error('或在代码中设置: const apiKey = "sk-xxx";');
          process.exit(1);
      }

      const client = new OpenAI({
          apiKey: apiKey,
          baseURL: BASE_URL
      });

      const fileStream = fs.createReadStream('test.jsonl');
      const fileObject = await client.files.create({
          file: fileStream,
          purpose: 'batch'
      });
      console.log(fileObject.id);
      ```
    </Tab>

    <Tab title="Java（HTTP）">
      #### 请求示例 <span id="java-request-title" /> <span id="java-request-example" />

      ```java expandable
      import java.io.*;
      import java.net.HttpURLConnection;
      import java.net.URL;
      import java.nio.file.Files;
      import java.nio.file.Paths;
      import java.util.Scanner;
      import java.util.regex.Pattern;
      import java.util.regex.Matcher;

      /**
       * 阿里云百炼 Batch API - 上传文件
       *
       * 若没有配置环境变量，可在代码中硬编码API Key：API_KEY = "sk-xxx"
       * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
       * 新加坡和北京地域的API Key不同。
       *
       * 地域配置：
       * - 北京地域：https://dashscope.aliyuncs.com/compatible-mode/v1
       * - 新加坡地域：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
       * 注意：切换地域时，API Key也需要对应更换
       */
      public class BatchAPIUploadFile {

          // 北京地域配置（默认）
          private static final String BASE_URL = "https://dashscope.aliyuncs.com/compatible-mode/v1";
          // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
          // private static final String BASE_URL = "https://dashscope-intl.aliyuncs.com/compatible-mode/v1";
          // 注意：切换地域时，API Key也需要对应更换

          private static String API_KEY;

          public static void main(String[] args) throws Exception {
              API_KEY = System.getenv("DASHSCOPE_API_KEY");
              if (API_KEY == null || API_KEY.isEmpty()) {
                  System.err.println("错误: 请设置环境变量 DASHSCOPE_API_KEY");
                  System.err.println("或在代码中设置: API_KEY = \"sk-xxx\";");
                  System.exit(1);
              }

      String fileId = uploadFile("test.jsonl");
              System.out.println("文件ID: " + fileId);
          }

          // === 工具方法 ===

          private static String uploadFile(String filePath) throws Exception {
              String boundary = "----WebKitFormBoundary" + System.currentTimeMillis();
              URL url = new URL(BASE_URL + "/files");
              HttpURLConnection conn = (HttpURLConnection) url.openConnection();
              conn.setDoOutput(true);
              conn.setRequestMethod("POST");
              conn.setRequestProperty("Authorization", "Bearer " + API_KEY);
              conn.setRequestProperty("Content-Type", "multipart/form-data; boundary=" + boundary);

              try (DataOutputStream out = new DataOutputStream(conn.getOutputStream())) {
                  // 添加 purpose 字段
                  out.writeBytes("--" + boundary + "\r\n");
                  out.writeBytes("Content-Disposition: form-data; name=\"purpose\"\r\n\r\n");
                  out.writeBytes("batch\r\n");

                  // 添加文件
                  out.writeBytes("--" + boundary + "\r\n");
                  out.writeBytes("Content-Disposition: form-data; name=\"file\"; filename=\"" + filePath + "\"\r\n");
                  out.writeBytes("Content-Type: application/octet-stream\r\n\r\n");

                  byte[] fileBytes = Files.readAllBytes(Paths.get(filePath));
                  out.write(fileBytes);
                  out.writeBytes("\r\n");
                  out.writeBytes("--" + boundary + "--\r\n");
              }

              String response = readResponse(conn);
              return parseField(response, "\"id\":\\s*\"([^\"]+)\"");
          }

          private static String readResponse(HttpURLConnection conn) throws Exception {
              int responseCode = conn.getResponseCode();
              InputStream is = (responseCode < 400) ? conn.getInputStream() : conn.getErrorStream();
              try (Scanner scanner = new Scanner(is, "UTF-8").useDelimiter("\\A")) {
                  return scanner.hasNext() ? scanner.next() : "";
              }
          }

          private static String parseField(String json, String regex) {
              Pattern pattern = Pattern.compile(regex);
              Matcher matcher = pattern.matcher(json);
              return matcher.find() ? matcher.group(1) : null;
          }
      }
      ```
    </Tab>

    <Tab title="curl(HTTP)">
      #### 请求示例 <span id="5103e1fa4a10k" /> <span id="552a0018ea51j" />

      ```bash
      # ======= 重要提示 =======
      # 新加坡和北京地域的API Key不同。
      # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1/files
      # === 执行时请删除该注释 ===
      curl -X POST https://dashscope.aliyuncs.com/compatible-mode/v1/files \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --form 'file=@"test.jsonl"' \
      --form 'purpose="batch"'
      ```
    </Tab>
  </Tabs>

  #### 返回示例 <span id="e755ceb45blu0" />

  ```json
  {
      "id": "file-batch-xxx",
      "bytes": 437,
      "created_at": 1742304153,
      "filename": "test.jsonl",
      "object": "file",
      "purpose": "batch",
      "status": "processed",
      "status_details": null
  }
  ```
</Accordion>

<Accordion title="2. 创建 Batch 任务">
  使用上传文件返回的文件ID 或OSS路径 创建 Batch 任务。

  <Tabs>
    <Tab title="OpenAI Python SDK">
      #### 请求示例 <span id="3d1d320515wzr" /> <span id="79338d585616c" />

      ```python
      import os
      from openai import OpenAI

      client = OpenAI(
          # 若没有配置环境变量，可用阿里云百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
          # 新加坡和北京地域的API Key不同。
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
          # 注意：切换地域时，API Key也需要对应更换
          base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
      )

      batch = client.batches.create(
          input_file_id="file-batch-xxx",  # 上传文件返回的id或OSS文件URL或OSS文件资源标识符
          endpoint="/v1/chat/completions",  # 测试模型batch-test-model填写/v1/chat/ds-test，向量模型填写/v1/embeddings，文本生成/多模态模型填写/v1/chat/completions
          completion_window="24h",
          metadata={'ds_name':"任务名称",'ds_description':'任务描述'} # metadata数据，非必填字段，用于创建任务名称、描述
      )
      print(batch)
      ```
    </Tab>

    <Tab title="OpenAI Node.js SDK">
      #### 请求示例 <span id="e1c89debc2dcp" /> <span id="5e32b1d041oge" />

      ```javascript expandable
      /**
       * 阿里云百炼 Batch API - 创建Batch任务
       *
       * 若没有配置环境变量，可在代码中硬编码API Key：apiKey: 'sk-xxx'
       * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
       * 新加坡和北京地域的API Key不同。
       *
       * 安装依赖：npm install openai
       */
      const OpenAI = require('openai');

      // 北京地域配置（默认）
      const BASE_URL = 'https://dashscope.aliyuncs.com/compatible-mode/v1';
      // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
      // const BASE_URL = 'https://dashscope-intl.aliyuncs.com/compatible-mode/v1';
      // 注意：切换地域时，API Key也需要对应更换

      const apiKey = process.env.DASHSCOPE_API_KEY;
      if (!apiKey) {
          console.error('错误: 请设置环境变量 DASHSCOPE_API_KEY');
          console.error('或在代码中设置: const apiKey = "sk-xxx";');
          process.exit(1);
      }

      const client = new OpenAI({
          apiKey: apiKey,
          baseURL: BASE_URL
      });

      const batch = await client.batches.create({
          input_file_id: 'file-batch-xxx',
          endpoint: '/v1/chat/completions',
          completion_window: '24h',
          metadata: {'ds_name': '任务名称', 'ds_description': '任务描述'}
      });
      console.log(batch.id);
      ```
    </Tab>

    <Tab title="Java（HTTP）">
      #### 请求示例 <span id="9ae10342eakn6" /> <span id="118479281b2ev" />

      ```java expandable
      import java.io.*;
      import java.net.HttpURLConnection;
      import java.net.URL;
      import java.util.Scanner;
      import java.util.regex.Pattern;
      import java.util.regex.Matcher;

      /**
       * 阿里云百炼 Batch API - 创建Batch任务
       *
       * 若没有配置环境变量，可在代码中硬编码API Key：API_KEY = "sk-xxx"
       * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
       * 新加坡和北京地域的API Key不同。
       *
       * 地域配置：
       * - 北京地域：https://dashscope.aliyuncs.com/compatible-mode/v1
       * - 新加坡地域：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
       * 注意：切换地域时，API Key也需要对应更换
       */
      public class BatchAPICreateBatch {

          // 北京地域配置（默认）
          private static final String BASE_URL = "https://dashscope.aliyuncs.com/compatible-mode/v1";
          // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
          // private static final String BASE_URL = "https://dashscope-intl.aliyuncs.com/compatible-mode/v1";
          // 注意：切换地域时，API Key也需要对应更换

          private static String API_KEY;

          public static void main(String[] args) throws Exception {
              API_KEY = System.getenv("DASHSCOPE_API_KEY");
              if (API_KEY == null || API_KEY.isEmpty()) {
                  System.err.println("错误: 请设置环境变量 DASHSCOPE_API_KEY");
                  System.err.println("或在代码中设置: API_KEY = \"sk-xxx\";");
                  System.exit(1);
              }

              String jsonBody = "{\"input_file_id\":\"file-batch-xxx\",\"endpoint\":\"/v1/chat/completions\",\"completion_window\":\"24h\",\"metadata\":{\"ds_name\":\"任务名称\",\"ds_description\":\"任务描述\"}}";
      String response = sendRequest("POST", "/batches", jsonBody);
              String batchId = parseField(response, "\"id\":\\s*\"([^\"]+)\"");
              System.out.println("Batch任务ID: " + batchId);
          }

          // === 工具方法 ===

          private static String sendRequest(String method, String path, String jsonBody) throws Exception {
              URL url = new URL(BASE_URL + path);
              HttpURLConnection conn = (HttpURLConnection) url.openConnection();
              conn.setRequestMethod(method);
              conn.setRequestProperty("Authorization", "Bearer " + API_KEY);

              if (jsonBody != null) {
                  conn.setDoOutput(true);
                  conn.setRequestProperty("Content-Type", "application/json");
                  try (OutputStream os = conn.getOutputStream()) {
                      os.write(jsonBody.getBytes("UTF-8"));
                  }
              }

              return readResponse(conn);
          }

          private static String readResponse(HttpURLConnection conn) throws Exception {
              int responseCode = conn.getResponseCode();
              InputStream is = (responseCode < 400) ? conn.getInputStream() : conn.getErrorStream();
              try (Scanner scanner = new Scanner(is, "UTF-8").useDelimiter("\\A")) {
                  return scanner.hasNext() ? scanner.next() : "";
              }
          }

          private static String parseField(String json, String regex) {
              Pattern pattern = Pattern.compile(regex);
              Matcher matcher = pattern.matcher(json);
              return matcher.find() ? matcher.group(1) : null;
          }
      }
      ```
    </Tab>

    <Tab title="curl（HTTP）">
      #### 请求示例 <span id="886baf8136i7m" /> <span id="08d677dcb7kc1" />

      ```bash
      # ======= 重要提示 =======
      # 新加坡和北京地域的API Key不同。
      # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1/batches
      # === 执行时请删除该注释 ===
      curl -X POST https://dashscope.aliyuncs.com/compatible-mode/v1/batches \
        -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
        -H "Content-Type: application/json" \
        -d '{
          "input_file_id": "file-batch-xxx",
          "endpoint": "/v1/chat/completions",
          "completion_window": "24h",
          "metadata":{"ds_name":"任务名称","ds_description":"任务描述"}
        }'
      ```
    </Tab>
  </Tabs>

  #### 输入参数 <span id="7a142728d5hah" /> <span id="5edf361ffd9mt" />

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "24.35%" }} /><col style={{ width: "13.04%" }} /><col style={{ width: "12.03%" }} /><col style={{ width: "9.86%" }} /><col style={{ width: "40.72%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参</strong></p><p><strong>方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>input\_file\_id</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>是</p></td><td><div><p>用于指定文件ID、OSS文件URL或OSS文件资源标识符，作为Batch任务的输入文件。您可以通过以下任一方式提供此参数：</p><ul><li><p><a href="/zh/model-studio/batch-interfaces-compatible-with-openai">准备与上传文件</a>接口返回的文件ID，如<code>file-batch-xxx</code>；</p></li><li><p><a href="/zh/model-studio/batch-interfaces-compatible-with-openai#f667257850olj">使用OSS文件创建 Batch 任务</a>。</p></li></ul></div></td></tr><tr><td><p>endpoint</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>是</p></td><td><p>访问路径，须与输入文件中的URL字段一致。</p><ul><li><p>向量模型填写<code>/v1/embeddings</code></p></li><li><p>测试模型<code>batch-test-model</code>填写<code>/v1/chat/ds-test</code></p></li><li><p>其他模型填写<code>/v1/chat/completions</code></p></li></ul></td></tr><tr><td><p>completion\_window</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>是</p></td><td><p>等待时间，最短 24h，最长 336h，仅支持整数。</p><p>支持"h"和"d"两个单位，如"24h"或"14d"。</p></td></tr><tr><td><p>metadata</p></td><td><p>Map</p></td><td><p>Body</p></td><td><p>否</p></td><td><p>任务扩展元数据，以键值对形式附加额外信息。</p></td></tr><tr><td><p>metadata.ds\_name</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>否</p></td><td><p>任务名称。</p><p>示例：<code>"ds\_name"："Batch任务"</code></p><p>限制：长度不超过100个字符。</p><p>若重复定义该字段，以最后一次传入的值为准。</p></td></tr><tr><td><p>metadata.ds\_description</p></td><td><p>String</p></td><td><p>Body</p></td><td><p>否</p></td><td><p>任务描述。</p><p>示例：<code>"ds\_description"："Batch推理任务测试"</code></p><p>限制：长度不超过200个字符。</p><p>若重复定义该字段，以最后一次传入的值为准。</p></td></tr></tbody></table>

  #### 返回示例 <span id="b4069b9d20wgp" /> <span id="0625b5d6afnnb" />

  ```json expandable
  {
      "id": "batch_xxx",
      "object": "batch",
      "endpoint": "/v1/chat/completions",
      "errors": null,
      "input_file_id": "file-batch-xxx",
      "completion_window": "24h",
      "status": "validating",
      "output_file_id": null,
      "error_file_id": null,
      "created_at": 1742367779,
      "in_progress_at": null,
      "expires_at": null,
      "finalizing_at": null,
      "completed_at": null,
      "failed_at": null,
      "expired_at": null,
      "cancelling_at": null,
      "cancelled_at": null,
      "request_counts": {
          "total": 0,
          "completed": 0,
          "failed": 0
      },
      "metadata": {
          "ds_name": "任务名称",
          "ds_description": "任务描述"
      }
  }
  ```

  #### 返回参数 <span id="32826319168xk" /> <span id="eb6515c507ky0" />

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "30.3%" }} /><col style={{ width: "14.24%" }} /><col style={{ width: "55.46%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p>String</p></td><td><p>本次创建的 Batch 任务 ID。</p></td></tr><tr><td><p>object</p></td><td><p>String</p></td><td><p>对象类型，固定值<code>batch</code>。</p></td></tr><tr><td><p>endpoint</p></td><td><p>String</p></td><td><p>访问路径。</p></td></tr><tr><td><p>errors</p></td><td><p>Map</p></td><td><p>错误信息。</p></td></tr><tr><td><p>input\_file\_id</p></td><td><p>String</p></td><td><p>文件ID <span>或OSS文件URL或OSS文件资源标识符</span>。</p></td></tr><tr><td><p>completion\_window</p></td><td><p>String</p></td><td><p>等待时间，支持最短等待时间24h，最长等待时间336h，仅支持整数。</p><p>支持"h"和"d"两个单位，如"24h"或"14d"。</p></td></tr><tr><td><p>status</p></td><td><p>String</p></td><td><p>任务状态，可选值：validating、failed、in\_progress、finalizing、completed、expired、cancelling、cancelled。</p></td></tr><tr><td><p>output\_file\_id</p></td><td><p>String</p></td><td><p>成功请求的输出文件 ID。</p></td></tr><tr><td><p>error\_file\_id</p></td><td><p>String</p></td><td><p>失败请求的错误文件 ID。</p></td></tr><tr><td><p>created\_at</p></td><td><p>Integer</p></td><td><p>任务创建的 Unix 时间戳（秒）。</p></td></tr><tr><td><p>in\_progress\_at</p></td><td><p>Integer</p></td><td><p>任务开始运行的 Unix 时间戳（秒）。</p></td></tr><tr><td><p>expires\_at</p></td><td><p>Integer</p></td><td><p>任务预计过期的 Unix 时间戳（秒）。</p></td></tr><tr><td><p>finalizing\_at</p></td><td><p>Integer</p></td><td><p>任务开始汇总结果的 Unix 时间戳（秒）。</p></td></tr><tr><td><p>completed\_at</p></td><td><p>Integer</p></td><td><p>任务完成的 Unix 时间戳（秒）。</p></td></tr><tr><td><p>failed\_at</p></td><td><p>Integer</p></td><td><p>任务失败的 Unix 时间戳（秒）。</p></td></tr><tr><td><p>expired\_at</p></td><td><p>Integer</p></td><td><p>任务过期的 Unix 时间戳（秒）。</p></td></tr><tr><td><p>cancelling\_at</p></td><td><p>Integer</p></td><td><p>任务开始取消的 Unix 时间戳（秒）。</p></td></tr><tr><td><p>cancelled\_at</p></td><td><p>Integer</p></td><td><p>任务取消完成的 Unix 时间戳（秒）。</p></td></tr><tr><td><p>request\_counts</p></td><td><p>Map</p></td><td><p>各状态的请求数量统计。</p></td></tr><tr><td><p>metadata</p></td><td><p>Map</p></td><td><p>附加元数据，键值对形式。</p></td></tr><tr><td><p>metadata.ds\_name</p></td><td><p>String</p></td><td><p>任务名称。</p></td></tr><tr><td><p>metadata.ds\_description</p></td><td><p>String</p></td><td><p>任务描述。</p></td></tr></tbody></table>
</Accordion>

<Accordion title="3. 查询 与管理 Batch 任务">
  任务创建后，可通过以下接口查询状态、列出历史任务或取消进行中的任务。

  <Accordion title="查询指定任务状态">
    传入 Batch 任务ID可查询指定任务的详细信息。当前仅支持查询 30 天内创建的 Batch 任务。

    <Tabs>
      <Tab title="OpenAI Python SDK">
        #### 请求示例 <span id="260688ec05utl" /> <span id="b8b4342009tdf" />

        ```python
        import os
        from openai import OpenAI

        client = OpenAI(
            # 若没有配置环境变量，可用阿里云百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
            # 新加坡和北京地域的API Key不同。
            api_key=os.getenv("DASHSCOPE_API_KEY"),
            # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
            # 注意：切换地域时，API Key也需要对应更换
            base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
        )
        batch = client.batches.retrieve("batch_id")  # 将batch_id替换为Batch任务的id
        print(batch)
        ```
      </Tab>

      <Tab title="OpenAI Node.js SDK">
        #### 请求示例 <span id="42ebd5d6cekoa" /> <span id="97066d52630gi" />

        ```javascript expandable
        /**
         * 阿里云百炼 Batch API - 查询单个任务
         *
         * 若没有配置环境变量，可在代码中硬编码API Key：apiKey: 'sk-xxx'
         * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
         * 新加坡和北京地域的API Key不同。
         *
         * 安装依赖：npm install openai
         */
        const OpenAI = require('openai');

        // 北京地域配置（默认）
        const BASE_URL = 'https://dashscope.aliyuncs.com/compatible-mode/v1';
        // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
        // const BASE_URL = 'https://dashscope-intl.aliyuncs.com/compatible-mode/v1';
        // 注意：切换地域时，API Key也需要对应更换

        const apiKey = process.env.DASHSCOPE_API_KEY;
        if (!apiKey) {
            console.error('错误: 请设置环境变量 DASHSCOPE_API_KEY');
            console.error('或在代码中设置: const apiKey = "sk-xxx";');
            process.exit(1);
        }

        const client = new OpenAI({
            apiKey: apiKey,
            baseURL: BASE_URL
        });

        const batch = await client.batches.retrieve('batch_id');
        console.log(batch.status);
        ```
      </Tab>

      <Tab title="Java（HTTP）">
        #### 请求示例 <span id="19846b0c63mq4" /> <span id="fe824e74f4g7l" />

        ```java expandable
        import java.io.*;
        import java.net.HttpURLConnection;
        import java.net.URL;
        import java.util.Scanner;
        import java.util.regex.Pattern;
        import java.util.regex.Matcher;

        /**
         * 阿里云百炼 Batch API - 查询单个任务
         *
         * 若没有配置环境变量，可在代码中硬编码API Key：API_KEY = "sk-xxx"
         * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
         * 新加坡和北京地域的API Key不同。
         *
         * 地域配置：
         * - 北京地域：https://dashscope.aliyuncs.com/compatible-mode/v1
         * - 新加坡地域：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
         * 注意：切换地域时，API Key也需要对应更换
         */
        public class BatchAPIRetrieveBatch {

            // 北京地域配置（默认）
            private static final String BASE_URL = "https://dashscope.aliyuncs.com/compatible-mode/v1";
            // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
            // private static final String BASE_URL = "https://dashscope-intl.aliyuncs.com/compatible-mode/v1";
            // 注意：切换地域时，API Key也需要对应更换

            private static String API_KEY;

            public static void main(String[] args) throws Exception {
                API_KEY = System.getenv("DASHSCOPE_API_KEY");
                if (API_KEY == null || API_KEY.isEmpty()) {
                    System.err.println("错误: 请设置环境变量 DASHSCOPE_API_KEY");
                    System.err.println("或在代码中设置: API_KEY = \"sk-xxx\";");
                    System.exit(1);
                }

                String batchInfo = sendRequest("GET", "/batches/batch_id", null);
                String status = parseField(batchInfo, "\"status\":\\s*\"([^\"]+)\"");
                System.out.println("任务状态: " + status);
            }

            // === 工具方法 ===

            private static String sendRequest(String method, String path, String jsonBody) throws Exception {
                URL url = new URL(BASE_URL + path);
                HttpURLConnection conn = (HttpURLConnection) url.openConnection();
                conn.setRequestMethod(method);
                conn.setRequestProperty("Authorization", "Bearer " + API_KEY);

                if (jsonBody != null) {
                    conn.setDoOutput(true);
                    conn.setRequestProperty("Content-Type", "application/json");
                    try (OutputStream os = conn.getOutputStream()) {
                        os.write(jsonBody.getBytes("UTF-8"));
                    }
                }

                return readResponse(conn);
            }

            private static String readResponse(HttpURLConnection conn) throws Exception {
                int responseCode = conn.getResponseCode();
                InputStream is = (responseCode < 400) ? conn.getInputStream() : conn.getErrorStream();
                try (Scanner scanner = new Scanner(is, "UTF-8").useDelimiter("\\A")) {
                    return scanner.hasNext() ? scanner.next() : "";
                }
            }

            private static String parseField(String json, String regex) {
                Pattern pattern = Pattern.compile(regex);
                Matcher matcher = pattern.matcher(json);
                return matcher.find() ? matcher.group(1) : null;
            }
        }
        ```
      </Tab>

      <Tab title="curl(HTTP)">
        #### 请求示例 <span id="bcef124412b73" /> <span id="483c0e2ac0d0n" />

        ```bash
        # ======= 重要提示 =======
        # 新加坡和北京地域的API Key不同。
        # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1/batches/batch_id
        # === 执行时请删除该注释 ===
        curl --request GET 'https://dashscope.aliyuncs.com/compatible-mode/v1/batches/batch_id' \
         -H "Authorization: Bearer $DASHSCOPE_API_KEY"
        ```
      </Tab>
    </Tabs>

    #### 返回示例 <span id="retrieve-batch-response-title" /> <span id="retrieve-batch-response-example" />

    查询成功后返回 Batch 任务的详细信息。以下为 completed 状态的返回示例：

    ```json expandable
    {
      "id": "batch_abc123",
      "object": "batch",
      "endpoint": "/v1/chat/completions",
      "errors": null,
      "input_file_id": "file-abc123",
      "completion_window": "24h",
      "status": "completed",
      "output_file_id": "file-batch_output-xyz789",
      "error_file_id": "file-batch_error-xyz789",
      "created_at": 1711402400,
      "in_progress_at": 1711402450,
      "expires_at": 1711488800,
      "finalizing_at": 1711405000,
      "completed_at": 1711406000,
      "failed_at": null,
      "expired_at": null,
      "cancelling_at": null,
      "cancelled_at": null,
      "request_counts": {
        "total": 100,
        "completed": 95,
        "failed": 5
      },
      "metadata": {
        "customer_id": "user_123456789",
        "batch_description": "Nightly eval job"
      }
    }
    ```

    返回的JSON对象包含 Batch 任务完整信息，包括任务状态、结果文件 ID、请求统计等。各字段说明见下表。

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.52%" }} /><col style={{ width: "14.41%" }} /><col style={{ width: "60.07%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p>String</p></td><td><p>Batch 任务 ID。</p></td></tr><tr><td><p>status</p></td><td><p>String</p></td><td><p>任务状态，可能的值包括：</p><ul><li><p>validating：正在验证输入文件</p></li><li><p>in\_progress：任务处理中</p></li><li><p>finalizing：处理完成，正在生成输出文件</p></li><li><p>completed：任务成功完成</p></li><li><p>failed：任务因严重错误失败</p></li><li><p>expired：任务在 completion\_window 内未完成而过期</p></li><li><p>cancelling：正在取消任务</p></li><li><p>cancelled：任务已取消</p></li></ul></td></tr><tr><td><p>output\_file\_id</p></td><td><p>String</p></td><td><p>成功结果文件的 ID，任务完成后生成。</p></td></tr><tr><td><p>error\_file\_id</p></td><td><p>String</p></td><td><p>失败结果文件的 ID，任务完成且有失败请求时生成。</p></td></tr><tr><td><p>request\_counts</p></td><td><p>Object</p></td><td><p>请求数量统计对象，包含 total、completed、failed 字段。</p></td></tr></tbody></table>
  </Accordion>

  <Accordion title="查询任务列表">
    可使用 `batches.list()` 方法查询 Batch 任务列表，通过分页机制逐步获取完整的任务列表。

    <Tabs>
      <Tab title="OpenAI Python SDK">
        #### 请求示例 <span id="29740518d7ett" /> <span id="7eb9e8e0a52ry" />

        ```python
        import os
        from openai import OpenAI

        client = OpenAI(
            # 若没有配置环境变量，可用阿里云百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
            # 新加坡和北京地域的API Key不同。
            api_key=os.getenv("DASHSCOPE_API_KEY"),
            # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
            base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
        )
        batches = client.batches.list(after="batch_xxx", limit=2,extra_query={'ds_name':'任务名称','input_file_ids':'file-batch-xxx,file-batch-xxx','status':'completed,expired','create_after':'20250304000000','create_before':'20250306123000'})
        print(batches)
        ```
      </Tab>

      <Tab title="OpenAI Node.js SDK">
        #### 请求示例 <span id="10e57ca3a38he" /> <span id="efbdeeba2e3nc" />

        ```javascript expandable
        /**
         * 阿里云百炼 Batch API - 查询任务列表
         *
         * 若没有配置环境变量，可在代码中硬编码API Key：apiKey: 'sk-xxx'
         * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
         * 新加坡和北京地域的API Key不同。
         *
         * 安装依赖：npm install openai
         */
        const OpenAI = require('openai');

        // 北京地域配置（默认）
        const BASE_URL = 'https://dashscope.aliyuncs.com/compatible-mode/v1';
        // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
        // const BASE_URL = 'https://dashscope-intl.aliyuncs.com/compatible-mode/v1';
        // 注意：切换地域时，API Key也需要对应更换

        const apiKey = process.env.DASHSCOPE_API_KEY;
        if (!apiKey) {
            console.error('错误: 请设置环境变量 DASHSCOPE_API_KEY');
            console.error('或在代码中设置: const apiKey = "sk-xxx";');
            process.exit(1);
        }

        const client = new OpenAI({
            apiKey: apiKey,
            baseURL: BASE_URL
        });

        const batches = await client.batches.list({
            after: 'batch_xxx',
            limit: 2,
            extra_query: {
                'ds_name': '任务名称',
                'input_file_ids': 'file-batch-xxx,file-batch-xxx',
                'status': 'completed,expired',
                'create_after': '20250304000000',
                'create_before': '20250306123000'
            }
        });

        for (const batch of batches.data) {
            console.log(batch.id, batch.status);
        }
        ```
      </Tab>

      <Tab title="Java（HTTP）">
        #### 请求示例 <span id="a492cf12f0lh0" /> <span id="2f83e550a8xvs" />

        ```java expandable
        import java.io.*;
        import java.net.HttpURLConnection;
        import java.net.URL;
        import java.util.Scanner;
        import java.util.regex.Pattern;
        import java.util.regex.Matcher;

        /**
         * 阿里云百炼 Batch API - 查询任务列表
         *
         * 若没有配置环境变量，可在代码中硬编码API Key：API_KEY = "sk-xxx"
         * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
         * 新加坡和北京地域的API Key不同。
         *
         * 地域配置：
         * - 北京地域：https://dashscope.aliyuncs.com/compatible-mode/v1
         * - 新加坡地域：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
         * 注意：切换地域时，API Key也需要对应更换
         */
        public class BatchAPIListBatches {

            // 北京地域配置（默认）
            private static final String BASE_URL = "https://dashscope.aliyuncs.com/compatible-mode/v1";
            // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
            // private static final String BASE_URL = "https://dashscope-intl.aliyuncs.com/compatible-mode/v1";
            // 注意：切换地域时，API Key也需要对应更换

            private static String API_KEY;

            public static void main(String[] args) throws Exception {
                API_KEY = System.getenv("DASHSCOPE_API_KEY");
                if (API_KEY == null || API_KEY.isEmpty()) {
                    System.err.println("错误: 请设置环境变量 DASHSCOPE_API_KEY");
                    System.err.println("或在代码中设置: API_KEY = \"sk-xxx\";");
                    System.exit(1);
                }

                String response = sendRequest("GET", "/batches?after=batch_xxx&limit=2&ds_name=Batch&input_file_ids=file-batch-xxx,file-batch-xxx&status=completed,failed&create_after=20250303000000&create_before=20250320000000", null);
        // 解析 JSON 获取任务列表
                System.out.println(response);
            }

            // === 工具方法 ===

            private static String sendRequest(String method, String path, String jsonBody) throws Exception {
                URL url = new URL(BASE_URL + path);
                HttpURLConnection conn = (HttpURLConnection) url.openConnection();
                conn.setRequestMethod(method);
                conn.setRequestProperty("Authorization", "Bearer " + API_KEY);

                if (jsonBody != null) {
                    conn.setDoOutput(true);
                    conn.setRequestProperty("Content-Type", "application/json");
                    try (OutputStream os = conn.getOutputStream()) {
                        os.write(jsonBody.getBytes("UTF-8"));
                    }
                }

                return readResponse(conn);
            }

            private static String readResponse(HttpURLConnection conn) throws Exception {
                int responseCode = conn.getResponseCode();
                InputStream is = (responseCode < 400) ? conn.getInputStream() : conn.getErrorStream();
                try (Scanner scanner = new Scanner(is, "UTF-8").useDelimiter("\\A")) {
                    return scanner.hasNext() ? scanner.next() : "";
                }
            }

            private static String parseField(String json, String regex) {
                Pattern pattern = Pattern.compile(regex);
                Matcher matcher = pattern.matcher(json);
                return matcher.find() ? matcher.group(1) : null;
            }
        }
        ```
      </Tab>

      <Tab title="curl(HTTP)">
        #### 请求示例 <span id="127c17fef9duj" /> <span id="314b7724e83p2" />

        ```bash
        # ======= 重要提示 =======
        # 新加坡和北京地域的API Key不同。
        # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1/batches?xxx同下方内容xxx
        # === 执行时请删除该注释 ===
        curl --request GET  'https://dashscope.aliyuncs.com/compatible-mode/v1/batches?after=batch_xxx&limit=2&ds_name=Batch&input_file_ids=file-batch-xxx,file-batch-xxx&status=completed,failed&create_after=20250303000000&create_before=20250320000000' \
         -H "Authorization: Bearer $DASHSCOPE_API_KEY"
        ```

        > 将 `after=batch_id` 中的 `batch_id` 替换为实际值， `limit` 参数设置为返回任务的数量， `ds_name` 填写为任务名称片段，input\_file\_ids的值可填写多个文件ID， `status` 填写Batch任务的多个状态， `create_after` 和 `create_before` 的值填写为时间点。
      </Tab>
    </Tabs>

    #### 输入参数 <span id="be637ea3e500d" /> <span id="837b874cbaint" />

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "10.72%" }} /><col style={{ width: "11.96%" }} /><col style={{ width: "13.61%" }} /><col style={{ width: "10.72%" }} /><col style={{ width: "52.99%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>传参方式</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>after</p></td><td><p>String</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>用于分页的游标，值为上一页最后一个任务的ID。</p></td></tr><tr><td><p>limit</p></td><td><p>Integer</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>每页返回的任务数量，范围\[1, 100]，默认20。</p></td></tr><tr><td><p>ds\_name</p></td><td><p>String</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>按任务名称进行模糊匹配。</p></td></tr><tr><td><p>input\_file\_ids</p></td><td><p>String</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>按文件ID筛选，多个ID用逗号分隔，最多20个。</p></td></tr><tr><td><p>status</p></td><td><p>String</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>按任务状态筛选，多个状态用逗号分隔。</p></td></tr><tr><td><p>create\_after</p></td><td><p>String</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>筛选在此时间点之后创建的任务，格式：<code>yyyyMMddHHmmss</code>。</p></td></tr><tr><td><p>create\_before</p></td><td><p>String</p></td><td><p>Query</p></td><td><p>否</p></td><td><p>筛选在此时间点之前创建的任务，格式：<code>yyyyMMddHHmmss</code>。</p></td></tr></tbody></table>

    #### 返回示例 <span id="696b34100bfa0" /> <span id="234fd9d7b7ulm" />

    ```json expandable
    {
      "object": "list",
      "data": [
        {
          "id": "batch_xxx",
          "object": "batch",
          "endpoint": "/v1/chat/completions",
          "errors": null,
          "input_file_id": "file-batch-xxx",
          "completion_window": "24h",
          "status": "completed",
          "output_file_id": "file-batch_output-xxx",
          "error_file_id": null,
          "created_at": 1722234109,
          "in_progress_at": 1722234109,
          "expires_at": null,
          "finalizing_at": 1722234165,
          "completed_at": 1722234165,
          "failed_at": null,
          "expired_at": null,
          "cancelling_at": null,
          "cancelled_at": null,
          "request_counts": {
            "total": 100,
            "completed": 95,
            "failed": 5
          },
          "metadata": {}
        },
        { ... }
      ],
      "first_id": "batch_xxx",
      "last_id": "batch_xxx",
      "has_more": true
    }
    ```

    #### 返回参数 <span id="13ee6b5a0dbrg" />

    <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "18.26%" }} /><col style={{ width: "17.39%" }} /><col style={{ width: "64.35%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>object</p></td><td><p>String</p></td><td><p>类型，固定值list。</p></td></tr><tr><td><p>data</p></td><td><p>Array</p></td><td><p>Batch任务对象，参见创建Batch任务的返回参数。</p></td></tr><tr><td><p>first\_id</p></td><td><p>String</p></td><td><p>当前页第一个 Batch任务 ID。</p></td></tr><tr><td><p>last\_id</p></td><td><p>String</p></td><td><p>当前页最后一个Batch任务 ID。</p></td></tr><tr><td><p>has\_more</p></td><td><p>Boolean</p></td><td><p>是否有下一页。</p></td></tr></tbody></table>
  </Accordion>

  <Accordion title="取消Batch任务">
    取消一个正在进行或排队中的任务。成功调用后，任务状态将变为 cancelling，最终变为 cancelled。在任务被完全取消前，已完成的部分仍会计费。

    <Tabs>
      <Tab title="OpenAI Python SDK">
        #### 请求示例 <span id="548636940bfzd" /> <span id="5c28ec42e7x9u" />

        ```python
        import os
        from openai import OpenAI

        client = OpenAI(
            # 若没有配置环境变量，可用阿里云百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
            # 新加坡和北京地域的API Key不同。
            api_key=os.getenv("DASHSCOPE_API_KEY"),
            # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
            base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
        )
        batch = client.batches.cancel("batch_id")  # 将batch_id替换为Batch任务的id
        print(batch)
        ```
      </Tab>

      <Tab title="OpenAI Node.js SDK">
        #### 请求示例 <span id="849e37800fkyy" /> <span id="20f23a0ef82lg" />

        ```javascript expandable
        /**
         * 阿里云百炼 Batch API - 取消任务
         *
         * 若没有配置环境变量，可在代码中硬编码API Key：apiKey: 'sk-xxx'
         * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
         * 新加坡和北京地域的API Key不同。
         *
         * 安装依赖：npm install openai
         */
        const OpenAI = require('openai');

        // 北京地域配置（默认）
        const BASE_URL = 'https://dashscope.aliyuncs.com/compatible-mode/v1';
        // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
        // const BASE_URL = 'https://dashscope-intl.aliyuncs.com/compatible-mode/v1';
        // 注意：切换地域时，API Key也需要对应更换

        const apiKey = process.env.DASHSCOPE_API_KEY;
        if (!apiKey) {
            console.error('错误: 请设置环境变量 DASHSCOPE_API_KEY');
            console.error('或在代码中设置: const apiKey = "sk-xxx";');
            process.exit(1);
        }

        const client = new OpenAI({
            apiKey: apiKey,
            baseURL: BASE_URL
        });

        const batch = await client.batches.cancel('batch_id');
        console.log(batch.status); // cancelled
        ```
      </Tab>

      <Tab title="Java（HTTP）">
        #### 请求示例 <span id="9797d49ec5l7n" /> <span id="3c83185ab1b94" />

        ```java expandable
        import java.io.*;
        import java.net.HttpURLConnection;
        import java.net.URL;
        import java.util.Scanner;
        import java.util.regex.Pattern;
        import java.util.regex.Matcher;

        /**
         * 阿里云百炼 Batch API - 取消任务
         *
         * 若没有配置环境变量，可在代码中硬编码API Key：API_KEY = "sk-xxx"
         * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
         * 新加坡和北京地域的API Key不同。
         *
         * 地域配置：
         * - 北京地域：https://dashscope.aliyuncs.com/compatible-mode/v1
         * - 新加坡地域：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
         * 注意：切换地域时，API Key也需要对应更换
         */
        public class BatchAPICancelBatch {

            // 北京地域配置（默认）
            private static final String BASE_URL = "https://dashscope.aliyuncs.com/compatible-mode/v1";
            // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
            // private static final String BASE_URL = "https://dashscope-intl.aliyuncs.com/compatible-mode/v1";
            // 注意：切换地域时，API Key也需要对应更换

            private static String API_KEY;

            public static void main(String[] args) throws Exception {
                API_KEY = System.getenv("DASHSCOPE_API_KEY");
                if (API_KEY == null || API_KEY.isEmpty()) {
                    System.err.println("错误: 请设置环境变量 DASHSCOPE_API_KEY");
                    System.err.println("或在代码中设置: API_KEY = \"sk-xxx\";");
                    System.exit(1);
                }

                String response = sendRequest("POST", "/batches/batch_id/cancel", null);
                System.out.println(response);
            }

            // === 工具方法 ===

            private static String sendRequest(String method, String path, String jsonBody) throws Exception {
                URL url = new URL(BASE_URL + path);
                HttpURLConnection conn = (HttpURLConnection) url.openConnection();
                conn.setRequestMethod(method);
                conn.setRequestProperty("Authorization", "Bearer " + API_KEY);

                if (jsonBody != null) {
                    conn.setDoOutput(true);
                    conn.setRequestProperty("Content-Type", "application/json");
                    try (OutputStream os = conn.getOutputStream()) {
                        os.write(jsonBody.getBytes("UTF-8"));
                    }
                }

                return readResponse(conn);
            }

            private static String readResponse(HttpURLConnection conn) throws Exception {
                int responseCode = conn.getResponseCode();
                InputStream is = (responseCode < 400) ? conn.getInputStream() : conn.getErrorStream();
                try (Scanner scanner = new Scanner(is, "UTF-8").useDelimiter("\\A")) {
                    return scanner.hasNext() ? scanner.next() : "";
                }
            }

            private static String parseField(String json, String regex) {
                Pattern pattern = Pattern.compile(regex);
                Matcher matcher = pattern.matcher(json);
                return matcher.find() ? matcher.group(1) : null;
            }
        }
        ```
      </Tab>

      <Tab title="curl(HTTP)">
        #### 请求示例 <span id="c7493d1120esw" /> <span id="8b344918f625z" />

        ```bash
        # ======= 重要提示 =======
        # 新加坡和北京地域的API Key不同。
        # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1/batches/batch_id/cancel
        # === 执行时请删除该注释 ===
        curl --request POST 'https://dashscope.aliyuncs.com/compatible-mode/v1/batches/batch_id/cancel' \
         -H "Authorization: Bearer $DASHSCOPE_API_KEY"
        ```

        > 将 `batch_id` 替换为实际值。
      </Tab>
    </Tabs>

    #### 返回示例 <span id="cancel-batch-response-title" /> <span id="be180a346fgbn" />

    取消任务成功后返回 Batch 任务的详细信息。以下是一个 cancelling 状态的返回示例：

    ```json expandable
    {
      "id": "batch_abc123",
      "object": "batch",
      "endpoint": "/v1/chat/completions",
      "errors": null,
      "input_file_id": "file-abc123",
      "completion_window": "24h",
      "status": "cancelling",
      "output_file_id": null,
      "error_file_id": null,
      "created_at": 1711402400,
      "in_progress_at": 1711402450,
      "expires_at": 1711488800,
      "finalizing_at": null,
      "completed_at": null,
      "failed_at": null,
      "expired_at": null,
      "cancelling_at": 1711403000,
      "cancelled_at": null,
      "request_counts": {
        "total": 100,
        "completed": 23,
        "failed": 1
      },
      "metadata": null
    }
    ```

    > 取消任务后，状态会先变为 `cancelling` ，等待正在执行的请求完成；最终会变为 `cancelled` 。已完成的请求结果仍会保存在输出文件中。
  </Accordion>
</Accordion>

<Accordion title="4. 下载Batch结果文件">
  任务结束后会生成结果文件（output\_file\_id）和可能的错误文件（error\_file\_id），两者均通过相同的文件下载接口获取。

  仅支持下载以`file-batch_output`开头的`file_id`对应的文件。

  <Tabs>
    <Tab title="OpenAI Python SDK">
      您可以通过`content`方法获取Batch任务结果文件内容，并通过`write_to_file`方法将其保存至本地。

      #### 请求示例 <span id="16468fde432bx" /> <span id="32f5c64147ie9" />

      ```python
      import os
      from openai import OpenAI

      client = OpenAI(
          # 若没有配置环境变量，可用阿里云百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
          # 新加坡和北京地域的API Key不同。
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
          # 注意：切换地域时，API Key也需要对应更换
          base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
      )
      content = client.files.content(file_id="file-batch_output-xxx")
      # 打印结果文件内容
      print(content.text)
      # 保存结果文件至本地
      content.write_to_file("result.jsonl")
      ```

      #### 返回示例 <span id="b765cd6fcfqvx" /> <span id="b261ee0f130a0" />

      ```json
      {"id":"c308ef7f-xxx","custom_id":"1","response":{"status_code":200,"request_id":"c308ef7f-0824-9c46-96eb-73566f062426","body":{"created":1742303743,"usage":{"completion_tokens":35,"prompt_tokens":26,"total_tokens":61},"model":"qwen-plus","id":"chatcmpl-c308ef7f-0824-9c46-96eb-73566f062426","choices":[{"finish_reason":"stop","index":0,"message":{"content":"你好！当然可以。无论是需要信息查询、学习资料、解决问题的方法，还是其他任何帮助，我都在这里为你提供支持。请告诉我你需要什么方面的帮助？"}}],"object":"chat.completion"}},"error":null}
      {"id":"73291560-xxx","custom_id":"2","response":{"status_code":200,"request_id":"73291560-7616-97bf-87f2-7d747bbe84fd","body":{"created":1742303743,"usage":{"completion_tokens":7,"prompt_tokens":26,"total_tokens":33},"model":"qwen-plus","id":"chatcmpl-73291560-7616-97bf-87f2-7d747bbe84fd","choices":[{"finish_reason":"stop","index":0,"message":{"content":"2+2 equals 4."}}],"object":"chat.completion"}},"error":null}
      ```
    </Tab>

    <Tab title="OpenAI Node.js SDK">
      您可以通过`content`方法获取Batch任务结果文件内容。

      #### 请求示例 <span id="nodejs-download-title" /> <span id="nodejs-download-example" />

      ```javascript expandable
      /**
       * 阿里云百炼 Batch API - 下载结果文件
       *
       * 若没有配置环境变量，可在代码中硬编码API Key：apiKey: 'sk-xxx'
       * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
       * 新加坡和北京地域的API Key不同。
       *
       * 安装依赖：npm install openai
       */
      const OpenAI = require('openai');
      const fs = require('fs');

      // 北京地域配置（默认）
      const BASE_URL = 'https://dashscope.aliyuncs.com/compatible-mode/v1';
      // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
      // const BASE_URL = 'https://dashscope-intl.aliyuncs.com/compatible-mode/v1';
      // 注意：切换地域时，API Key也需要对应更换

      const apiKey = process.env.DASHSCOPE_API_KEY;
      if (!apiKey) {
          console.error('错误: 请设置环境变量 DASHSCOPE_API_KEY');
          console.error('或在代码中设置: const apiKey = "sk-xxx";');
          process.exit(1);
      }

      const client = new OpenAI({
          apiKey: apiKey,
          baseURL: BASE_URL
      });

      // 下载结果文件
      const content = await client.files.content('file-batch_output-xxx');
      const text = await content.text();
      console.log(text);

      // 保存到本地文件
      fs.writeFileSync('result.jsonl', text);
      console.log('结果已保存到 result.jsonl');
      ```
    </Tab>

    <Tab title="Java（HTTP）">
      您可以通过GET请求到`/files/{file_id}/content`端点获取文件内容。

      #### 请求示例 <span id="java-download-title" /> <span id="java-download-example" />

      ```java expandable
      import java.io.*;
      import java.net.HttpURLConnection;
      import java.net.URL;
      import java.nio.file.Files;
      import java.nio.file.Paths;
      import java.util.Scanner;
      import java.util.regex.Pattern;
      import java.util.regex.Matcher;

      /**
       * 阿里云百炼 Batch API - 下载结果文件
       *
       * 若没有配置环境变量，可在代码中硬编码API Key：API_KEY = "sk-xxx"
       * 但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
       * 新加坡和北京地域的API Key不同。
       *
       * 地域配置：
       * - 北京地域：https://dashscope.aliyuncs.com/compatible-mode/v1
       * - 新加坡地域：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
       * 注意：切换地域时，API Key也需要对应更换
       */
      public class BatchAPIDownloadFile {

          // 北京地域配置（默认）
          private static final String BASE_URL = "https://dashscope.aliyuncs.com/compatible-mode/v1";
          // 如果使用新加坡地域，请将上面的 BASE_URL 替换为：
          // private static final String BASE_URL = "https://dashscope-intl.aliyuncs.com/compatible-mode/v1";
          // 注意：切换地域时，API Key也需要对应更换

          private static String API_KEY;

          public static void main(String[] args) throws Exception {
              API_KEY = System.getenv("DASHSCOPE_API_KEY");
              if (API_KEY == null || API_KEY.isEmpty()) {
                  System.err.println("错误: 请设置环境变量 DASHSCOPE_API_KEY");
                  System.err.println("或在代码中设置: API_KEY = \"sk-xxx\";");
                  System.exit(1);
              }

      // 下载结果文件
      String content = sendRequest("GET", "/files/file-batch_output-xxx/content", null);
      System.out.println(content);

      // 保存到本地文件
              Files.write(Paths.get("result.jsonl"), content.getBytes());
              System.out.println("结果已保存到 result.jsonl");
          }

          // === 工具方法 ===

          private static String sendRequest(String method, String path, String jsonBody) throws Exception {
              URL url = new URL(BASE_URL + path);
              HttpURLConnection conn = (HttpURLConnection) url.openConnection();
              conn.setRequestMethod(method);
              conn.setRequestProperty("Authorization", "Bearer " + API_KEY);

              if (jsonBody != null) {
                  conn.setDoOutput(true);
                  conn.setRequestProperty("Content-Type", "application/json");
                  try (OutputStream os = conn.getOutputStream()) {
                      os.write(jsonBody.getBytes("UTF-8"));
                  }
              }

              return readResponse(conn);
          }

          private static String readResponse(HttpURLConnection conn) throws Exception {
              int responseCode = conn.getResponseCode();
              InputStream is = (responseCode < 400) ? conn.getInputStream() : conn.getErrorStream();
              try (Scanner scanner = new Scanner(is, "UTF-8").useDelimiter("\\A")) {
                  return scanner.hasNext() ? scanner.next() : "";
              }
          }

          private static String parseField(String json, String regex) {
              Pattern pattern = Pattern.compile(regex);
              Matcher matcher = pattern.matcher(json);
              return matcher.find() ? matcher.group(1) : null;
          }
      }
      ```
    </Tab>

    <Tab title="curl(HTTP)">
      您可以通过GET方法，在URL中指定`file_id`来下载Batch任务结果文件。

      #### 请求示例 <span id="6b2e4ba366df3" /> <span id="51706205cdwmd" />

      ```bash
      # ======= 重要提示 =======
      # 新加坡和北京地域的API Key不同。
      # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1/files/file-batch_output-xxx/content
      # === 执行时请删除该注释 ===
      curl -X GET https://dashscope.aliyuncs.com/compatible-mode/v1/files/file-batch_output-xxx/content \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" > result.jsonl
      ```
    </Tab>
  </Tabs>

  #### 返回示例 <span id="72a124ae0elk8" />

  单条响应结果：

  ```json expandable
  {
      "id": "c308ef7f-xxx",
      "custom_id": "1",
      "response": {
          "status_code": 200,
          "request_id": "c308ef7f-0824-9c46-96eb-73566f062426",
          "body": {
              "created": 1742303743,
              "usage": {
                  "completion_tokens": 35,
                  "prompt_tokens": 26,
                  "total_tokens": 61
              },
              "model": "qwen-plus",
              "id": "chatcmpl-c308ef7f-0824-9c46-96eb-73566f062426",
              "choices": [
                  {
                      "finish_reason": "stop",
                      "index": 0,
                      "message": {
                          "content": "你好！当然可以。无论是需要信息查询、学习资料、解决问题的方法，还是其他任何帮助，我都在这里为你提供支持。请告诉我你需要什么方面的帮助？"
                      }
                  }
              ],
              "object": "chat.completion"
          }
      },
      "error": null
  }
  ```

  #### 返回参数 <span id="2b6446177fa80" />

  <table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "23.3%" }} /><col style={{ width: "15.6%" }} /><col style={{ width: "61.1%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td><p>id</p></td><td><p>String</p></td><td><p>请求 ID。</p></td></tr><tr><td><p>custom\_id</p></td><td><p>String</p></td><td><p>用户自定义的 ID。</p></td></tr><tr><td><p>response</p></td><td><p>Object</p></td><td><p>请求结果。</p></td></tr><tr><td><p>status\_code</p></td><td><p>Integer</p></td><td><p>状态码。200表示请求成功。</p></td></tr><tr><td><p>request\_id</p></td><td><p>String</p></td><td><p>服务端为这次请求生成的唯一ID。</p></td></tr><tr><td><p>completion\_tokens</p></td><td><p>Integer</p></td><td><p>模型生成的回复内容（completion）所消耗的Token数量。</p></td></tr><tr><td><p>prompt\_tokens</p></td><td><p>Integer</p></td><td><p>发送给模型的输入内容（<code>prompt</code>）所消耗的Token数量。</p></td></tr><tr><td><p>reasoning\_tokens</p></td><td><p>Integer</p></td><td><p>深度思考模型的思考过程token数。</p></td></tr><tr><td><p>total\_tokens</p></td><td><p>Integer</p></td><td><p>本次调用总共消耗的Token数量。</p></td></tr><tr><td><p>model</p></td><td><p>String</p></td><td><p>本次调用所使用的模型名称。</p></td></tr><tr><td><p>reasoning\_content</p></td><td><p>String</p></td><td><p>深度思考模型的思考过程。</p></td></tr><tr><td><p>error</p></td><td><p>Object</p></td><td><p>错误信息对象。如果API调用成功，该值为<code>null</code>。如果发生错误，这里会包含错误代码和详细的错误信息。</p></td></tr><tr><td><p>error.code</p></td><td><p>String</p></td><td><p>错误行信息和错误原因，可参考<a href="/zh/model-studio/batch-interfaces-compatible-with-openai#97e145aeabbwf">错误码</a>进行排查。</p></td></tr><tr><td><p>error.message</p></td><td><p>String</p></td><td><p>错误信息。</p></td></tr></tbody></table>
</Accordion>

## 进阶功能 <span id="fac624aa57u5x" /> <span id="10c149dbb17w6" />

### 使用OSS文件创建 Batch 任务 <span id="f667257850olj" /> <span id="47e365825e7q4" />

大型文件推荐存储在阿里云OSS中，通过 `input_file_id` 直接引用，避免本地上传限制。

<strong>方式一：使用文件 URL</strong>

将具有公共读权限或预签名授权的OSS文件URL直接作为 `input_file_id`：

```python
batch_job = client.batches.create(
    input_file_id="https://your-bucket.oss-cn-beijing.aliyuncs.com/file.jsonl?Expires=...",
    endpoint="/v1/chat/completions",
    completion_window="24h"
)
```

<Accordion title="获取文件URL">
  1. <strong>OSS 控制台</strong>

     1. 进入<strong>Bucket列表</strong>页面，找到目标Bucket并单击名称；
     2. 在<strong>文件列表</strong>中定位目标文件，单击右侧<strong>详情</strong>按钮；

        > 您可在 <strong>文件列表</strong> 中新建子文件夹如 Batch/20260101 并上传文件  。
     3. 在弹出面板中，单击<strong>复制文件URL</strong>。
  2. <strong>SDK</strong>：生成OSS文件 URL，参考[使用预签名URL下载（Java SDK V1）](https://help.aliyun.com/zh/oss/developer-reference/download-using-a-presigned-url)。
</Accordion>

<strong>方式二：使用资源标识符（推荐）</strong>

1. <strong>完成OSS授权</strong>

   参阅从OSS导入文件配置说明的授权和添加标签步骤。
2. <strong>参数配置</strong>

   使用`oss:{region}:{bucket}/{file_path}`格式的OSS资源标识符：

```python
batch_job = client.batches.create(
    input_file_id="oss:cn-beijing:your-bucket/path/to/file.jsonl",
    endpoint="/v1/chat/completions",
    completion_window="24h"
)
```

<strong>建议</strong>：

- 使用与阿里云百炼服务同地域 Bucket（ `cn-beijing`）可利用阿里云内网传输，降低网络延迟、提升稳定性并避免跨地域流量费用。
- 方式二更安全，基于RAM授权而非公开 URL。

### 配置任务完成通知 <span id="909a9d2f7ahlc" /> <span id="70e8ebf567v4q" />

长时间运行的任务使用轮询会消耗不必要的资源。建议配置异步通知，系统在任务完成后主动通知。

<Note>
  任务完成通知功能仅支持在北京地域配置。
</Note>

- <strong>Callback 回调：</strong>在创建任务时指定一个公网可访问的 URL
- <strong>EventBridge 消息队列：</strong>与阿里云生态深度集成，无需公网 IP

<Accordion title="方式一：Callback 回调">
  创建任务时通过 `metadata` 指定一个公网可访问的 URL。任务完成后，系统向该URL发送包含任务状态的 POST 请求：

  <Tabs>
    <Tab title="OpenAI Python SDK">
      ```python
      import os
      from openai import OpenAI

      client = OpenAI(
          # 若没有配置环境变量，可用阿里云百炼API Key将下行替换为：api_key="sk-xxx"。但不建议在生产环境中直接将API Key硬编码到代码中，以减少API Key泄露风险。
          # 新加坡和北京地域的API Key不同。
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          # 以下是北京地域base_url，如果使用新加坡地域的模型，需要将base_url替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1
          # 注意：切换地域时，API Key也需要对应更换
          base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
      )

      batch = client.batches.create(
          input_file_id="file-batch-xxx",  # 上传文件返回的 id
          endpoint="/v1/chat/completions",  # 向量模型填写"/v1/embeddings",测试模型batch-test-model填写/v1/chat/ds-test,其他模型填写/v1/chat/completions
          completion_window="24h",
          metadata={
                  "ds_batch_finish_callback": "https://xxx/xxx"
                }
      )
      print(batch)
      ```
    </Tab>

    <Tab title="curl(HTTP)">
      #### 请求示例 <span id="b7fd1cf629jol" /> <span id="5044e1f8e2tgd" />

      ```bash
      curl -X POST --location "https://dashscope.aliyuncs.com/compatible-mode/v1/batches" \
          -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
          -H "Content-Type: application/json" \
          -d '{
                "input_file_id": "file-batch-xxxxx",
                "endpoint": "/v1/chat/completions",
                "completion_window": "24h",
                "metadata": {
                  "ds_batch_finish_callback": "https://xxx/xxx"
                }
              }'
      ```
    </Tab>
  </Tabs>
</Accordion>

<Accordion title="方式二：EventBridge 消息队列">
  此方式无需公网 IP，适用于与阿里云其他服务（如函数计算、RocketMQ）集成的场景。

  当 Batch 任务完成时，系统会向阿里云事件总线 EventBridge 发送一个事件。您可以配置 EventBridge 规则来监听此事件，并将其路由到指定目标。

  - 事件源 (Source): `acs.dashscope`
  - 事件类型 (Type): `dashscope:System:BatchTaskFinish`

  相关文档：[路由到消息队列RocketMQ版](https://help.aliyun.com/zh/eventbridge/user-guide/route-events-to-message-queue-for-apache-rocketmq)。
</Accordion>

## 应用于生产环境 <span id="d5e7241e86l13" /> <span id="a97d01906afxv" />

- <strong>文件管理</strong>

  - 定期调用 [OpenAI-File删除文件接口](/zh/model-studio/openai-file-interface#3457ce1d7ezr3)删除不需要的文件，避免达到文件存储上限（10000个文件或100GB）
  - 对于大型文件，推荐将其存储在阿里云OSS中
- <strong>任务监控</strong>

  - 优先使用 Callback 或 EventBridge 异步通知
  - 轮询间隔 > 1分钟，使用指数退避策略
- <strong>错误处理</strong>

  - 实现完整的异常处理机制，涵盖网络错误、API 错误等
  - 下载并分析 `error_file_id` 的错误详情
  - 对于常见错误码，参考[错误码](/zh/model-studio/error-code)进行解决
- <strong>成本优化</strong>

  - 将时效性要求不高的任务迁移到 Batch API
  - 合并小任务到一个批次
  - 合理设置 `completion_window` 提供更多调度灵活性

### 实用工具 <span id="78136c88fck9c" /> <span id="d3a4f85e02ap3" />

<Accordion title="CSV 转 JSONL">
  原始数据存储在 CSV 文件中（第一列为 ID，第二列为内容）时，可使用以下脚本快速生成 Batch 任务所需的 JSONL 文件。

  > 如需调整文件路径或其他参数，请根据实际情况修改代码。

  ```python
  import csv
  import json
  def messages_builder_example(content):
      messages = [{"role": "system", "content": "You are a helpful assistant."}, {"role": "user", "content": content}]
      return messages

  with open("input_demo.csv", "r") as fin:
      with open("input_demo.jsonl", 'w', encoding='utf-8') as fout:
          csvreader = csv.reader(fin)
          for row in csvreader:
              body = {"model": "qwen-plus", "messages": messages_builder_example(row[1])}
              # 选择向量模型进行调用时，url的值需填写"/v1/embeddings",其他模型填写/v1/chat/completions
              request = {"custom_id": row[0], "method": "POST", "url": "/v1/chat/completions", "body": body}
              fout.write(json.dumps(request, separators=(',', ':'), ensure_ascii=False) + "\n")
  ```
</Accordion>

<Accordion title="JSONL 结果转 CSV">
  使用以下脚本可将 `result.jsonl` 文件解析为易于在Excel中分析的 `result.csv` 文件。

  > 如需调整文件路径或其他参数，请根据实际情况修改代码。

  ```python expandable
  import json
  import csv
  columns = ["custom_id",
             "model",
             "request_id",
             "status_code",
             "error_code",
             "error_message",
             "created",
             "content",
             "usage"]

  def dict_get_string(dict_obj, path):
      obj = dict_obj
      try:
          for element in path:
              obj = obj[element]
          return obj
      except:
          return None

  with open("result.jsonl", "r") as fin:
      with open("result.csv", 'w', encoding='utf-8') as fout:
          rows = [columns]
          for line in fin:
              request_result = json.loads(line)
              row = [dict_get_string(request_result, ["custom_id"]),
                     dict_get_string(request_result, ["response", "body", "model"]),
                     dict_get_string(request_result, ["response", "request_id"]),
                     dict_get_string(request_result, ["response", "status_code"]),
                     dict_get_string(request_result, ["error", "error_code"]),
                     dict_get_string(request_result, ["error", "error_message"]),
                     dict_get_string(request_result, ["response", "body", "created"]),
                     dict_get_string(request_result, ["response", "body", "choices", 0, "message", "content"]),
                     dict_get_string(request_result, ["response", "body", "usage"])]
              rows.append(row)
          writer = csv.writer(fout)
          writer.writerows(rows)
  ```

  <strong>Excel 乱码解决</strong>

  - 可使用文本编辑器（如Sublime）将 CSV 文件的编码转换为GBK，然后再用Excel打开。
  - 或在Excel中新建一个Excel文件，并在导入数据时指定正确的编码格式 UTF-8。
</Accordion>

## 接口限流 <span id="4d1c3a85b3nm7" /> <span id="c223f5f44be90" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "40.03%" }} /><col style={{ width: "59.97%" }} /></colgroup><thead><tr><th><p><strong>接口</strong></p></th><th><p><strong>限流（主账号级别）</strong></p></th></tr></thead><tbody><tr><td><p>创建任务</p></td><td><p>1000 次/分钟，最大并行 1000 个</p></td></tr><tr><td><p>查询任务</p></td><td><p>1000 次/分钟</p></td></tr><tr><td><p>查询任务列表</p></td><td><p>100 次/分钟</p></td></tr><tr><td><p>取消任务</p></td><td><p>1000 次/分钟</p></td></tr></tbody></table>

## 计费说明 <span id="fecc98d9514rv" /> <span id="7a913f86cdi6h" />

- <strong>计费单价：</strong> 所有成功请求的输入和输出Token，单价均为对应模型实时推理价格的<strong>50%</strong> ，具体请参见[模型列表](/zh/model-studio/models)。
- <strong>计费范围：</strong>

  - 仅对成功执行的请求计费。
  - 文件解析失败、任务执行失败或行级错误请求均<strong>不产生费用</strong> 。
  - 已取消的任务中，取消前已成功完成的请求仍正常计费。

<Note>
  * 批量推理为独立计费项，支持[AI 通用型节省计划](/zh/model-studio/savings-plan-and-resource-package)，但不支持[预付费](https://common-buy.aliyun.com/?commodityCode=sfm_llminference_spn_public_cn)（节省计划）、[新人免费额度](/zh/model-studio/new-free-quota)等优惠，以及[上下文缓存](/zh/model-studio/context-cache)等功能。
  * 部分模型（如 qwen3.5-plus、qwen3.5-flash）默认开启思考模式，会产生额外的思考tokens，并按输出token价格计费，导致成本增加。建议根据任务复杂度设置enable\_thinking参数以控制成本，具体请参考[深度思考](/zh/model-studio/deep-thinking)。
</Note>

## 错误码 <span id="97e145aeabbwf" /> <span id="6e8489debdu6b" />

调用失败时，请参见[错误码](/zh/model-studio/error-code)进行解决。

## 常见问题 <span id="1b0468f7eeceo" /> <span id="75420d4509fk9" />

1. <strong>如何选择使用 Batch Chat 还是Batch File？</strong>

   处理包含大量请求的单个大文件且可异步获取结果时，选择 Batch File。需要以API同步调用方式高并发提交大量独立对话请求时，选择 Batch Chat。
2. <strong>Batch File调用如何计费？需要单独购买吗？</strong>

   答：Batch 是一种调用方式，采用后付费模式，按成功请求的 Token 用量计费，无需额外购买套餐。
3. <strong>提交的Batch File是按顺序执行的吗？</strong>

   答：不是。系统采用动态调度机制，根据计算资源负载安排任务执行，不保证严格遵循提交顺序。资源紧张时，任务启动和执行可能延迟。
4. <strong>提交的Batch File需要多长时间完成？</strong>

   答：执行时间取决于系统资源分配和任务规模。若任务在设定的 completion\_window 内未完成，状态变为 expired，未处理的请求不再执行，也不产生费用。

   <strong>场景建议：</strong>对时效性有严格要求的场景，建议使用实时调用；对时效性有一定容忍度的大规模数据处理场景，推荐使用 Batch 调用。

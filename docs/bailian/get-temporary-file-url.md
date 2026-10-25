> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 上传本地文件获取临时URL

> 在调用多模态、图像、视频或音频模型时，通常需要传入文件的 URL。为此，阿里云百炼提供了 免费 临时存储空间，您可将本地文件上传至该空间并获得 URL（ 有效期为 48 小时 ）。

<meta property="og:title" content="上传本地文件获取临时URL" />

## 使用限制 <span id="e5231784e4tdc" />

- <strong>文件与模型绑定</strong>：文件上传时必须指定模型名称，且该模型须与后续调用的<strong>模型一致</strong>，不同模型无法共享文件。
- <strong>文件大小限制</strong>：接口上传文件大小不得超过<strong>1GB</strong>，超出限制将导致上传失败。此外，不同模型对输入文件大小有不同限制，超出限制将导致模型调用失败。
- <strong>文件与主账号绑定</strong>：文件上传与模型调用所使用的 API Key 必须<strong>属于同一个阿里云主账号</strong>，且上传的文件仅限该主账号及其对应模型使用，无法被其他主账号或其他模型共享。
- <strong>文件有效期限制</strong>：文件上传后<strong>有效期48小时</strong>，超时后文件将被自动清理，请确保在有效期内完成模型调用。
- <strong>文件使用限制</strong>：文件一旦上传，不可查询、修改或下载，仅能<strong>通过URL参数在模型调用时使用</strong>。
- <strong>文件上传限流</strong>：文件上传凭证接口的调用限流按照“阿里云主账号+模型”维度为<strong>100QPS</strong>，<strong>超出限流将导致请求失败</strong>。

<Tip>
  * 临时 URL 有效期48小时，过期后无法使用，<strong>请勿用于生产环境。</strong>
  * 文件上传凭证接口限流为 100 QPS 且不支持扩容，<strong>请勿用于生产环境、高并发及压测场景。</strong>
  * 生产环境建议使用[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss) 等稳定存储，确保文件长期可用并规避限流问题。
</Tip>

<Note>
  本功能支持<strong>华北2（北京）</strong>和<strong>新加坡</strong>地域。本文代码示例默认以<strong>北京地域</strong>为例；如需使用新加坡地域，请按示例中的注释将请求 URL 中的 `dashscope.aliyuncs.com` 替换为 `dashscope-intl.aliyuncs.com`，并使用新加坡地域的 API Key（两地域 API Key 不同，获取方式见[获取与配置 API Key](/zh/model-studio/get-api-key)）。
</Note>

## 使用方式 <span id="b33e0ac80abq5" />

1. 获取文件 URL：请先通过[步骤一](/zh/model-studio/get-temporary-file-url#a363e01e741gu)上传文件（图片、视频或音频），获取以`oss://` 为前缀的临时 URL。
2. 调用模型：<strong>请务必根据</strong>[<strong>步骤二</strong>](/zh/model-studio/get-temporary-file-url#1c60469225ufa)<strong>使用临时 URL 进行调用</strong>。该步骤不能跳过，否则接口将报错。

## 步骤一：获取临时URL <span id="a363e01e741gu" />

<Tabs>
  <Tab title="方式一：通过代码上传文件">
    本文提供 Python 和 Java 示例代码，简化上传文件操作。您只需<strong>指定模型和待上传的文件</strong>，即可获取临时URL。

    <strong>前提条件</strong>

    在调用前，您需要[获取与配置 API Key](/zh/model-studio/get-api-key)，再[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

    #### 示例代码 <span id="c92b7d87ed5rs" />

    <Tabs>
      <Tab title="Python">
        <strong>环境配置</strong>

        - 推荐使用Python 3.9及以上版本。
        - 请安装必要的依赖包。

        ```bash
        pip install -U requests
        ```

        <strong>输入参数</strong>

        - api\_key：阿里云百炼API KEY。
        - model\_name：指定文件将要用于哪个模型，如`qwen3-vl-plus`。
        - file\_path：待上传的本地文件路径（图片、视频等）。

        ```python
        import os
        import requests
        from pathlib import Path
        from datetime import datetime, timedelta

        def get_upload_policy(api_key, model_name):
            """获取文件上传凭证"""
            # 北京与新加坡地域的 API Key 不同，请使用对应地域的 API Key
            # 以下为北京地域 url，若使用新加坡地域，需将 url 替换为：https://dashscope-intl.aliyuncs.com/api/v1/uploads
            url = "https://dashscope.aliyuncs.com/api/v1/uploads"
            headers = {
                "Authorization": f"Bearer {api_key}",
                "Content-Type": "application/json"
            }
            params = {
                "action": "getPolicy",
                "model": model_name
            }

            response = requests.get(url, headers=headers, params=params)
            if response.status_code != 200:
                raise Exception(f"Failed to get upload policy: {response.text}")

            return response.json()['data']

        def upload_file_to_oss(policy_data, file_path):
            """将文件上传到临时存储OSS"""
            file_name = Path(file_path).name
            key = f"{policy_data['upload_dir']}/{file_name}"

            with open(file_path, 'rb') as file:
                files = {
                    'OSSAccessKeyId': (None, policy_data['oss_access_key_id']),
                    'Signature': (None, policy_data['signature']),
                    'policy': (None, policy_data['policy']),
                    'x-oss-object-acl': (None, policy_data['x_oss_object_acl']),
                    'x-oss-forbid-overwrite': (None, policy_data['x_oss_forbid_overwrite']),
                    'key': (None, key),
                    'success_action_status': (None, '200'),
                    'file': (file_name, file)
                }

                response = requests.post(policy_data['upload_host'], files=files)
                if response.status_code != 200:
                    raise Exception(f"Failed to upload file: {response.text}")

            return f"oss://{key}"

        def upload_file_and_get_url(api_key, model_name, file_path):
            """上传文件并获取URL"""
            # 1. 获取上传凭证，上传凭证接口有限流，超出限流将导致请求失败
            policy_data = get_upload_policy(api_key, model_name)
            # 2. 上传文件到OSS
            oss_url = upload_file_to_oss(policy_data, file_path)

            return oss_url

        # 使用示例
        if __name__ == "__main__":
            # 从环境变量中获取API Key 或者 在代码中设置 api_key = "your_api_key"
            api_key = os.getenv("DASHSCOPE_API_KEY")
            if not api_key:
                raise Exception("请设置DASHSCOPE_API_KEY环境变量")

            # 设置model名称
            model_name="qwen3-vl-plus"

            # 待上传的文件路径
            file_path = "/tmp/cat.png"  # 替换为实际文件路径

            try:
                public_url = upload_file_and_get_url(api_key, model_name, file_path)
                expire_time = datetime.now() + timedelta(hours=48)
                print(f"文件上传成功，有效期为48小时，过期时间: {expire_time.strftime('%Y-%m-%d %H:%M:%S')}")
                print(f"临时URL: {public_url}")
                print("注意：使用oss://形式的临时URL时，必须在HTTP请求头（Header）中显式添加参数：X-DashScope-OssResourceResolve: enable，具体请参考：https://help.aliyun.com/zh/model-studio/get-temporary-file-url#http-call")

            except Exception as e:
                print(f"Error: {str(e)}")
        ```

        <strong>输出示例</strong>

        ```plaintext
        文件上传成功，有效期为48小时，过期时间: 2024-07-18 17:36:15
        临时URL: oss://dashscope-instant/xxx/2024-07-18/xxx/cat.png
        注意：使用oss://形式的临时URL时，必须在HTTP请求头（Header）中显式添加参数：X-DashScope-OssResourceResolve: enable，具体请参考：https://help.aliyun.com/zh/model-studio/get-temporary-file-url#http-call
        ```

        <Tip>
          获取临时 URL 后，调用时<strong>必须</strong>在 HTTP 请求头（Header）中显式添加参数：`X-DashScope-OssResourceResolve: enable`，具体请参见[通过HTTP调用](/zh/model-studio/get-temporary-file-url#d6a1cb0f01h5k)。
        </Tip>
      </Tab>

      <Tab title="Java">
        <strong>环境配置</strong>

        - 推荐使用JDK 1.8及以上版本。
        - 请在Maven项目的`pom.xml`文件中导入以下依赖。

        ```xml
         <dependencies>
                <dependency>
                    <groupId>org.json</groupId>
                    <artifactId>json</artifactId>
                    <version>20230618</version>
                </dependency>
                <dependency>
                    <groupId>org.apache.httpcomponents</groupId>
                    <artifactId>httpclient</artifactId>
                    <version>4.5.13</version>
                </dependency>
                <dependency>
                    <groupId>org.apache.httpcomponents</groupId>
                    <artifactId>httpmime</artifactId>
                    <version>4.5.13</version>
                </dependency>
        </dependencies>
        ```

        <strong>输入参数</strong>

        - apiKey：阿里云百炼API KEY。
        - modelName：指定文件将要用于哪个模型，如`qwen3-vl-plus`。
        - filePath：待上传的本地文件路径（图片、视频等）。

        ```java
        import org.apache.http.client.methods.CloseableHttpResponse;
        import org.apache.http.client.methods.HttpGet;
        import org.apache.http.client.methods.HttpPost;
        import org.apache.http.entity.mime.MultipartEntityBuilder;
        import org.apache.http.entity.ContentType;
        import org.apache.http.impl.client.CloseableHttpClient;
        import org.apache.http.impl.client.HttpClients;
        import org.apache.http.HttpStatus;
        import org.apache.http.util.EntityUtils;
        import org.json.JSONObject;
        import java.io.File;
        import java.io.IOException;
        import java.nio.file.Files;
        import java.nio.file.Path;
        import java.nio.file.Paths;
        import java.time.LocalDateTime;
        import java.time.format.DateTimeFormatter;

        public class PublicUrlHandler {

            // 北京与新加坡地域的 API Key 不同，请使用对应地域的 API Key
            // 以下为北京地域 url，若使用新加坡地域，需将 url 替换为：https://dashscope-intl.aliyuncs.com/api/v1/uploads
            private static final String API_URL = "https://dashscope.aliyuncs.com/api/v1/uploads";

            public static JSONObject getUploadPolicy(String apiKey, String modelName) throws IOException {
                try (CloseableHttpClient httpClient = HttpClients.createDefault()) {
                    HttpGet httpGet = new HttpGet(API_URL);
                    httpGet.addHeader("Authorization", "Bearer " + apiKey);
                    httpGet.addHeader("Content-Type", "application/json");

                    String query = String.format("action=getPolicy&model=%s", modelName);
                    httpGet.setURI(httpGet.getURI().resolve(httpGet.getURI() + "?" + query));

                    try (CloseableHttpResponse response = httpClient.execute(httpGet)) {
                        if (response.getStatusLine().getStatusCode() != 200) {
                            throw new IOException("Failed to get upload policy: " +
                                    EntityUtils.toString(response.getEntity()));
                        }
                        String responseBody = EntityUtils.toString(response.getEntity());
                        return new JSONObject(responseBody).getJSONObject("data");
                    }
                }
            }

            public static String uploadFileToOSS(JSONObject policyData, String filePath) throws IOException {
                Path path = Paths.get(filePath);
                String fileName = path.getFileName().toString();
                String key = policyData.getString("upload_dir") + "/" + fileName;

                HttpPost httpPost = new HttpPost(policyData.getString("upload_host"));
                MultipartEntityBuilder builder = MultipartEntityBuilder.create();

                builder.addTextBody("OSSAccessKeyId", policyData.getString("oss_access_key_id"));
                builder.addTextBody("Signature", policyData.getString("signature"));
                builder.addTextBody("policy", policyData.getString("policy"));
                builder.addTextBody("x-oss-object-acl", policyData.getString("x_oss_object_acl"));
                builder.addTextBody("x-oss-forbid-overwrite", policyData.getString("x_oss_forbid_overwrite"));
                builder.addTextBody("key", key);
                builder.addTextBody("success_action_status", "200");
                byte[] fileContent = Files.readAllBytes(path);
                builder.addBinaryBody("file", fileContent, ContentType.DEFAULT_BINARY, fileName);

                httpPost.setEntity(builder.build());

                try (CloseableHttpClient httpClient = HttpClients.createDefault();
                     CloseableHttpResponse response = httpClient.execute(httpPost)) {
                    if (response.getStatusLine().getStatusCode() != HttpStatus.SC_OK) {
                        throw new IOException("Failed to upload file: " +
                                EntityUtils.toString(response.getEntity()));
                    }
                    return "oss://" + key;
                }
            }

            public static String uploadFileAndGetUrl(String apiKey, String modelName, String filePath) throws IOException {
                JSONObject policyData = getUploadPolicy(apiKey, modelName);
                return uploadFileToOSS(policyData, filePath);
            }

            public static void main(String[] args) {
                // 获取环境变量中的API密钥
                String apiKey = System.getenv("DASHSCOPE_API_KEY");
                if (apiKey == null || apiKey.isEmpty()) {
                    System.err.println("请设置DASHSCOPE_API_KEY环境变量");
                    System.exit(1);
                }
                // 模型名称
                String modelName = "qwen3-vl-plus";
                //替换为实际文件路径
                String filePath = "src/main/resources/tmp/cat.png";

                try {
                    // 检查文件是否存在
                    File file = new File(filePath);
                    if (!file.exists()) {
                        System.err.println("文件不存在: " + filePath);
                        System.exit(1);
                    }

                    String publicUrl = uploadFileAndGetUrl(apiKey, modelName, filePath);
                    LocalDateTime expireTime = LocalDateTime.now().plusHours(48);
                    DateTimeFormatter formatter = DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss");

                    System.out.println("文件上传成功，有效期为48小时，过期时间: " + expireTime.format(formatter));
                    System.out.println("临时URL: " + publicUrl);
                    System.out.println("注意：使用oss://形式的临时URL时，必须在HTTP请求头（Header）中显式添加参数：X-DashScope-OssResourceResolve: enable，具体请参考：https://help.aliyun.com/zh/model-studio/get-temporary-file-url#http-call");
                } catch (IOException e) {
                    System.err.println("Error: " + e.getMessage());
                }
            }
        }
        ```

        <strong>输出示例</strong>

        ```plaintext
        文件上传成功，有效期为48小时，过期时间: 2024-07-18 17:36:15
        临时URL: oss://dashscope-instant/xxx/2024-07-18/xxx/cat.png
        注意：使用oss://形式的临时URL时，必须在HTTP请求头（Header）中显式添加参数：X-DashScope-OssResourceResolve: enable，具体请参考：https://help.aliyun.com/zh/model-studio/get-temporary-file-url#http-call
        ```

        <Tip>
          获取临时 URL 后，调用时<strong>必须</strong>在 HTTP 请求头（Header）中显式添加参数：`X-DashScope-OssResourceResolve: enable`，具体请参见[通过HTTP调用](/zh/model-studio/get-temporary-file-url#d6a1cb0f01h5k)。
        </Tip>
      </Tab>

      <Tab title="DashScope SDK (Python)">
        <strong>前提条件</strong>

        请[安装DashScope Python SDK](/zh/model-studio/install-sdk)，且DashScope Python SDK版本号 >= `1.27.3`。

        DashScope SDK 提供了 `Files` 类，封装了文件上传、查询、删除等操作，免去手工拼接上传凭证与 OSS 表单。

        <Note>
          `Files.upload` 真实签名为 `upload(file_path, purpose)`，其中 `purpose` 取值 `fine_tune`（微调训练文件）或 `inference`（推理临时文件，对应本文场景），<strong>无 `model` 参数</strong>。
        </Note>

        ```python
        import os
        import dashscope
        from dashscope import Files

        # 北京与新加坡地域的 API Key 不同，请使用对应地域的 API Key
        # 从环境变量中获取 API Key
        api_key = os.getenv('DASHSCOPE_API_KEY')

        # 以下为北京地域 url，若使用新加坡地域，需将 url 替换为：https://dashscope-intl.aliyuncs.com/api/v1
        dashscope.base_http_api_url = 'https://dashscope.aliyuncs.com/api/v1'

        # 上传文件（purpose=fine_tune 用于微调训练文件，purpose=inference 用于推理临时文件）
        result = Files.upload(
            file_path='train_data.jsonl',
            purpose='fine_tune',
            api_key=api_key,
        )

        print(result)
        ```

        <strong>输出示例</strong>

        ```plaintext
        {"request_id": "xxx", "code": "", "message": "", "output": {"uploaded_files": [{"name": "train_data.jsonl", "file_id": "file-xxx"}], "failed_uploads": []}}
        ```
      </Tab>

      <Tab title="dashscope CLI">
        ```bash
        export DASHSCOPE_API_KEY="your-api-key"
        # 将 {WorkspaceId} 替换为业务空间ID，cn-beijing 替换为对应地域（新加坡: ap-southeast-1, 美东: us-east-1）
        export DASHSCOPE_HTTP_BASE_URL="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"
        # 列出已上传的文件
        dashscope files list

        # 上传本地文件
        dashscope oss upload -f train_data.jsonl -m qwen-plus
        ```

        <Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>

        完整地域表见 [Base URL 总览](/zh/model-studio/base-url)。
      </Tab>
    </Tabs>
  </Tab>

  <Tab title="方式二：通过命令行工具上传文件">
    对于熟悉命令行的开发者，可使用DashScope提供的命令行工具来上传文件。<strong>执行命令后，即可获取临时URL</strong>。

    #### 前提条件 <span id="64bddcdb76ef1" />

    1. 环境准备：推荐使用 Python 3.9 及以上版本。
    2. 获取API-KEY：在调用前，您需要[获取与配置 API Key](/zh/model-studio/get-api-key)。
    3. 安装SDK：请确保[DashScope Python SDK](/zh/model-studio/install-sdk) 版本不低于 `1.24.0`。执行以下命令进行安装或升级：

    ```bash
    pip install -U dashscope
    ```

    <Tabs>
      <Tab title="方法1：使用环境变量（推荐）">
        此方法更安全，可以避免API-KEY在命令历史或脚本中明文暴露。

        前提条件：请确保已[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

        执行上传命令：

        ```bash
        # 默认使用北京地域，若使用新加坡地域，请先执行：export DASHSCOPE_HTTP_BASE_URL="https://dashscope-intl.aliyuncs.com/api/v1"（新加坡地域 API Key 不同）
        dashscope oss.upload --model qwen3-vl-plus --file cat.png
        ```

        输出示例：

        ```plaintext
        Start oss.upload: model=qwen3-vl-plus, file=cat.png, api_key=None
        Uploaded oss url: oss://dashscope-instant/xxxx/2025-08-01/xxxx/cat.png
        ```

        <Tip>
          获取临时 URL 后，调用时<strong>必须</strong>在 HTTP 请求头（Header）中显式添加参数：`X-DashScope-OssResourceResolve: enable`，具体请参见[通过HTTP调用](/zh/model-studio/get-temporary-file-url#d6a1cb0f01h5k)。
        </Tip>
      </Tab>

      <Tab title="方法2：通过命令行参数指定API-KEY（临时使用）">
        执行上传命令：

        ```bash
        # 默认使用北京地域，若使用新加坡地域，请先执行：export DASHSCOPE_HTTP_BASE_URL="https://dashscope-intl.aliyuncs.com/api/v1"（新加坡地域 API Key 不同）
        dashscope oss.upload --model qwen3-vl-plus --file cat.png --api_key sk-xxxxxxx
        ```

        输出示例：

        ```plaintext
        Start oss.upload: model=qwen3-vl-plus, file=cat.png, api_key=sk-xxxxxxx
        Uploaded oss url: oss://dashscope-instant/xxx/2025-08-01/xxx/cat.png
        ```

        <Tip>
          获取临时 URL 后，调用时<strong>必须</strong>在 HTTP 请求头（Header）中显式添加参数：`X-DashScope-OssResourceResolve: enable`，具体请参见[通过HTTP调用](/zh/model-studio/get-temporary-file-url#d6a1cb0f01h5k)。
        </Tip>
      </Tab>
    </Tabs>

    #### 命令行参数说明 <span id="a33377c69bwdq" />

    <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "14.271357%" }} /><col style={{ width: "10.050251%" }} /><col style={{ width: "50.653266%" }} /><col style={{ width: "25.025126%" }} /></colgroup><thead><tr><th><p><strong>参数</strong></p></th><th><p><strong>是否必须</strong></p></th><th><p><strong>说明</strong></p></th><th><p><strong>示例</strong></p></th></tr></thead><tbody><tr><td><p>oss.upload</p></td><td><p>是</p></td><td><p>dashscope的子命令，用于执行文件上传操作。</p></td><td><p>oss.upload</p></td></tr><tr><td><p>--model</p></td><td><p>是</p></td><td><p>指定文件将要用于哪个模型。</p></td><td><p>qwen3-vl-plus</p></td></tr><tr><td><p>--file</p></td><td><p>是</p></td><td><p>本地文件的路径。可以是相对路径或绝对路径。</p></td><td><p>cat.png，/data/img.jpg</p></td></tr><tr><td><p>--api\_key</p></td><td><p>否</p></td><td><p>阿里云百炼API-KEY。如已配置环境变量，无需填写此参数。</p></td><td><p>sk-xxxx</p></td></tr></tbody></table>
  </Tab>
</Tabs>

## 步骤二：使用临时URL调用模型 <span id="1c60469225ufa" />

#### 使用限制 <span id="788832c151s25" />

- <strong>文件格式</strong>：临时URL须通过上述方式生成，且以 `oss://`为前缀的URL字符串。
- <strong>文件未过期</strong>：文件URL仍在上传后的48小时有效期内。
- <strong>模型一致</strong>：模型调用所使用的模型必须与文件上传时指定的模型完全一致。
- <strong>账号一致</strong>：模型调用的API KEY必须与文件上传时使用的API KEY同属一个阿里云主账号。

#### 方式一：通过HTTP调用 <span id="d6a1cb0f01h5k" />

通过curl、Postman或任何其他HTTP客户端直接调用API，则<strong>必须遵循以下规则</strong>：

<Tip>
  - 使用临时URL，<strong>必须</strong>在请求的<strong>Header</strong>中添加参数：`X-DashScope-OssResourceResolve: enable`。
  - 若缺失此Header，系统将无法解析`oss://`链接，请求将失败，报错信息请参考[错误码](/zh/model-studio/get-temporary-file-url#3b9b15a6a8qkl)。
</Tip>

<Tabs>
  <Tab title="请求示例">
    本示例为调用 qwen3-vl-plus 模型识别图片内容。

    <Note>
      请将 `oss://...`替换为真实的临时 URL，否则请求将失败。
    </Note>

    ```bash
    # 以下为北京地域 url，若使用新加坡地域，需将 url 替换为：https://dashscope-intl.aliyuncs.com/compatible-mode/v1/chat/completions
    curl -X POST https://dashscope.aliyuncs.com/compatible-mode/v1/chat/completions \
    -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
    -H 'Content-Type: application/json' \
    -H 'X-DashScope-OssResourceResolve: enable' \
    -d '{
      "model": "qwen3-vl-plus",
      "messages": [{
          "role": "user",
          "content":
          [{"type": "text","text": "这是什么"},
           {"type": "image_url","image_url": {"url": "oss://dashscope-instant/xxx/2024-07-18/xxxx/cat.png"}}]
        }]
    }'
    ```
  </Tab>

  <Tab title="响应示例">
    ```json
    {
      "choices": [
        {
          "message": {
            "content": "这是一张描绘一只白色猫咪在草地上奔跑的图片。这只猫有蓝色的眼睛，看起来非常可爱和活泼。背景是模糊化的自然景色，强调了主体——那只向前冲跑的小猫。这种摄影技巧称为浅景深（或大光圈效果），它使得前景中的小猫变得清晰而锐利，同时使背景虚化以突出主题并营造出一种梦幻般的效果。整体上这张照片给人一种轻松愉快的感觉，并且很好地捕捉到了动物的行为瞬间。",
            "role": "assistant"
          },
          "finish_reason": "stop",
          "index": 0,
          "logprobs": null
        }
      ],
      "object": "chat.completion",
      "usage": {
        "prompt_tokens": 1253,
        "completion_tokens": 104,
        "total_tokens": 1357
      },
      "created": 1739349052,
      "system_fingerprint": null,
      "model": "qwen3-vl-plus",
      "id": "chatcmpl-cfc4f2aa-22a8-9a94-8243-44c5bd9899bc"
    }
    ```
  </Tab>

  <Tab title="上传的本地图片示例">
    <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5231249371/p915804.png" alt="image" width={250} height={250.817} />
  </Tab>
</Tabs>

#### 方式二：通过DashScope SDK调用 <span id="f0ce0f5987cpa" />

您也可以使用阿里云百炼提供的 Python 或 Java SDK。

- <strong>直接传入 URL</strong>：调用模型 SDK 时，直接将以`oss://`为前缀的URL字符串作为文件参数传入。
- <strong>无需关心 Header</strong>：SDK 会自动添加必需的请求头，无需额外操作。

<strong>注意</strong>：并非所有模型都支持 SDK 调用，请以模型 API 文档为准。

> 不支持 OpenAI SDK。

<Tabs>
  <Tab title="Python">
    <strong>前提条件</strong>

    请[安装DashScope Python SDK](/zh/model-studio/install-sdk)，且DashScope Python SDK版本号 >=`1.24.0`。

    <strong>示例代码</strong>

    本示例为调用 qwen3-vl-plus 模型识别图片内容。此代码示例仅适用于 qwen-vl 和 omni 系列模型。

    <Tabs>
      <Tab title="请求示例">
        <Note>
          请将 image 参数中的 `oss://...`替换为真实的临时 URL，否则请求将失败。
        </Note>

        ```python
        import os
        import dashscope

        messages = [
            {
                "role": "system",
                "content": [{"text": "You are a helpful assistant."}]
            },
            {
                "role": "user",
                "content": [
                    {"image": "oss://dashscope-instant/xxx/2024-07-18/xxxx/cat.png"},
                    {"text": "这是什么"}]
            }]

        # 北京与新加坡地域的 API Key 不同，请使用对应地域的 API Key
        # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
        api_key = os.getenv('DASHSCOPE_API_KEY')

        # 以下为北京地域 url，若使用新加坡地域，需将 url 替换为：https://dashscope-intl.aliyuncs.com/api/v1
        dashscope.base_http_api_url = 'https://dashscope.aliyuncs.com/api/v1'

        response = dashscope.MultiModalConversation.call(
            api_key=api_key,
            model='qwen3-vl-plus',
            messages=messages
        )

        print(response)
        ```
      </Tab>

      <Tab title="响应示例">
        ```json
        {
            "status_code": 200,
            "request_id": "ccd9dcfb-98f0-92bc-xxxxxx",
            "code": "",
            "message": "",
            "output": {
                "text": null,
                "finish_reason": null,
                "choices": [
                    {
                        "finish_reason": "stop",
                        "message": {
                            "role": "assistant",
                            "content": [
                                {
                                    "text": "这是一张一只猫在草地上奔跑的照片。猫的毛色主要是白色，带有浅棕色的斑点，眼睛是蓝色的，显得非常可爱。背景是一个模糊的绿色草地和一些树木，阳光照射下来，给整个画面增添了一种温暖的感觉。猫的姿态显示出它正在快速移动，可能是在追逐什么或只是在享受户外活动的乐趣。整体来看，这是一幅充满活力和生机的图片。"
                                }
                            ]
                        }
                    }
                ]
            },
            "usage": {
                "input_tokens": 1112,
                "output_tokens": 91,
                "input_tokens_details": {
                    "text_tokens": 21,
                    "image_tokens": 1091
                },
                "prompt_tokens_details": {
                    "cached_tokens": 0
                },
                "total_tokens": 1203,
                "output_tokens_details": {
                    "text_tokens": 91
                },
                "image_tokens": 1091
            }
        }
        ```
      </Tab>
    </Tabs>
  </Tab>

  <Tab title="Java">
    <strong>前提条件</strong>

    请[安装DashScope Java SDK](/zh/model-studio/install-sdk)，且DashScope Java SDK版本号 >= `2.21.0`。

    <strong>示例代码</strong>

    本示例为调用 qwen3-vl-plus 模型识别图片内容。此代码示例仅适用于 qwen-vl 和 omni 系列模型。

    <Tabs>
      <Tab title="请求示例">
        <Note>
          请将 `oss://...`替换为真实的临时 URL，否则请求将失败。
        </Note>

        ```java
        import com.alibaba.dashscope.aigc.multimodalconversation.*;
        import com.alibaba.dashscope.common.Role;
        import com.alibaba.dashscope.exception.ApiException;
        import com.alibaba.dashscope.exception.NoApiKeyException;
        import com.alibaba.dashscope.exception.UploadFileException;
        import com.alibaba.dashscope.utils.Constants;
        import com.alibaba.dashscope.utils.JsonUtils;

        import java.util.Arrays;

        public class MultiModalConversationUsage {

            private static final String modelName = "qwen3-vl-plus";

            // 北京与新加坡地域的 API Key 不同，请使用对应地域的 API Key
            // 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
            public static String apiKey = System.getenv("DASHSCOPE_API_KEY");

            public static void simpleMultiModalConversationCall() throws ApiException, NoApiKeyException, UploadFileException {
                // 以下为北京地域 url，若使用新加坡地域，需将 url 替换为：https://dashscope-intl.aliyuncs.com/api/v1
                Constants.baseHttpApiUrl = "https://dashscope.aliyuncs.com/api/v1";
                MultiModalConversation conv = new MultiModalConversation();
                MultiModalMessageItemText systemText = new MultiModalMessageItemText("You are a helpful assistant.");
                MultiModalConversationMessage systemMessage = MultiModalConversationMessage.builder()
                        .role(Role.SYSTEM.getValue()).content(Arrays.asList(systemText)).build();
                MultiModalMessageItemImage userImage = new MultiModalMessageItemImage(
                        "oss://dashscope-instant/xxx/2024-07-18/xxxx/cat.png");
                MultiModalMessageItemText userText = new MultiModalMessageItemText("这是什么");
                MultiModalConversationMessage userMessage =
                        MultiModalConversationMessage.builder().role(Role.USER.getValue())
                                .content(Arrays.asList(userImage, userText)).build();
                MultiModalConversationParam param = MultiModalConversationParam.builder()
                        .model(MultiModalConversationUsage.modelName)
                        .apiKey(apiKey)
                        .message(systemMessage)
                        .vlHighResolutionImages(true)
                        .vlEnableImageHwOutput(true)
        //                .incrementalOutput(true)
                        .message(userMessage).build();
                MultiModalConversationResult result = conv.call(param);
                System.out.print(JsonUtils.toJson(result));

            }

            public static void main(String[] args) {
                try {
                    simpleMultiModalConversationCall();
                } catch (ApiException | NoApiKeyException | UploadFileException /*| IOException*/ e) {
                    System.out.println(e.getMessage());
                }
                System.exit(0);
            }

        }
        ```
      </Tab>

      <Tab title="响应示例">
        ```json
        {
            "requestId": "b6d60f91-4a7f-9257-xxxxxx",
            "usage": {
                "input_tokens": 1112,
                "output_tokens": 91,
                "total_tokens": 1203,
                "image_tokens": 1091,
                "input_tokens_details": {
                    "text_tokens": 21,
                    "image_tokens": 1091
                },
                "output_tokens_details": {
                    "text_tokens": 91
                }
            },
            "output": {
                "choices": [
                    {
                        "finish_reason": "stop",
                        "message": {
                            "role": "assistant",
                            "content": [
                                {
                                    "text": "这是一张一只猫在草地上奔跑的照片。猫的毛色主要是白色，带有浅棕色的斑点，眼睛是蓝色的，显得非常可爱。背景是一个模糊的绿色草地和一些树木，阳光照射下来，给整个画面增添了一种温暖的感觉。猫的姿态显示出它正在快速移动，可能是在追逐什么或只是在享受户外活动的乐趣。整体来看，这是一幅充满活力和生机的图片。"
                                },
                                {
                                    "image_hw": [
                                        [
                                            "924",
                                            "924"
                                        ]
                                    ]
                                }
                            ]
                        }
                    }
                ]
            }
        }
        ```
      </Tab>
    </Tabs>
  </Tab>
</Tabs>

## 附接口说明 <span id="26fd68eaf04kx" />

在上述[获取临时URL](/zh/model-studio/get-temporary-file-url#a363e01e741gu)的两种方式中，代码调用和命令行工具已集成以下三个步骤，简化文件上传操作。以下是各步骤的接口说明。

#### 步骤1：获取文件上传凭证 <span id="32db94982cllx" />

##### 前提条件 <span id="daa30479cemad" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

##### 请求接口 <span id="cd5c7c20fb74b" />

```http
北京地域：GET https://dashscope.aliyuncs.com/api/v1/uploads
新加坡地域：GET https://dashscope-intl.aliyuncs.com/api/v1/uploads
```

<Tip>
  文件上传凭证接口限流为 100 QPS（按“阿里云主账号+模型”维度），且临时存储不可扩容。生产环境或高并发场景请使用[阿里云OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss)等存储服务。
</Tip>

##### 入参描述 <span id="c606afaf252dg" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "11.161218%" }} /><col style={{ width: "13.754228%" }} /><col style={{ width: "8.117249%" }} /><col style={{ width: "7.328072%" }} /><col style={{ width: "36.414882%" }} /><col style={{ width: "23.224352%" }} /></colgroup><thead><tr><th><p><strong>传参方式</strong></p></th><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>必选</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td rowSpan={2}><p>Header</p></td><td><p>Content-Type</p></td><td><p><em>string</em></p></td><td><p>是</p></td><td><p>请求类型：application/json 。</p></td><td><p>application/json</p></td></tr><tr><td><p>Authorization</p></td><td><p><em>string</em></p></td><td><p>是</p></td><td><p>阿里云百炼API Key，例如：Bearer sk-xxx。</p></td><td><p>Bearer sk-xxx</p></td></tr><tr><td rowSpan={2}><p>Params</p></td><td><p>action</p></td><td><p><em>string</em></p></td><td><p>是</p></td><td><p>操作类型，当前场景为<code>getPolicy</code>。</p></td><td><p>getPolicy</p></td></tr><tr><td><p>model</p></td><td><p><em>string</em></p></td><td><p>是</p></td><td><p>需要调用的模型名称。</p></td><td><p>qwen3-vl-plus</p></td></tr></tbody></table>

##### 出参描述 <span id="58595f510c3zl" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "24.731183%" }} /><col style={{ width: "9.569892%" }} /><col style={{ width: "39.569892%" }} /><col style={{ width: "26.129032%" }} /></colgroup><thead><tr><th><p><strong>字段</strong></p></th><th><p><strong>类型</strong></p></th><th><p><strong>描述</strong></p></th><th><p><strong>示例值</strong></p></th></tr></thead><tbody><tr><td><p>request\_id</p></td><td><p><em>string</em></p></td><td><p>本次请求的系统唯一码。</p></td><td><p>7574ee8f-...-11c33ab46e51</p></td></tr><tr><td><p>data</p></td><td><p><em>object</em></p></td><td><p>-</p></td><td><p>-</p></td></tr><tr><td><p>data.policy</p></td><td><p><em>string</em></p></td><td><p>上传凭证。</p></td><td><p>eyJl...1ZSJ9XX0=</p></td></tr><tr><td><p>data.signature</p></td><td><p><em>string</em></p></td><td><p>上传凭证的签名。</p></td><td><p>g5K...d40=</p></td></tr><tr><td><p>data.upload\_dir</p></td><td><p><em>string</em></p></td><td><p>上传文件的目录。</p></td><td><p>dashscope-instant/xxx/2024-07-18/xxxx</p></td></tr><tr><td><p>data.upload\_host</p></td><td><p><em>string</em></p></td><td><p>上传的host地址。</p></td><td><p>[https://dashscope-file-xxx.oss-cn-beijing.aliyuncs.com](https://dashscope-file-xxx.oss-cn-beijing.aliyuncs.com)</p></td></tr><tr><td><p>data.expire\_in\_seconds</p></td><td><p><em>string</em></p></td><td><p>凭证有效期（单位：秒）。</p><div><p>过期后，重新调用本接口获取新的凭证。</p></div></td><td><p>300</p></td></tr><tr><td><p>data.max\_file\_size\_mb</p></td><td><p><em>string</em></p></td><td><p>本次允许上传的最大文件的大小（单位：MB）。</p><p>该值与需要访问的模型相关。</p></td><td><p>100</p></td></tr><tr><td><p>data.capacity\_limit\_mb</p></td><td><p><em>string</em></p></td><td><p>同一个主账号每天上传容量限制（单位：MB）。</p></td><td><p>999999999</p></td></tr><tr><td><p>data.oss\_access\_key\_id</p></td><td><p><em>string</em></p></td><td><p>用于上传的access key。</p></td><td><p>LTAxxx</p></td></tr><tr><td><p>data.x\_oss\_object\_acl</p></td><td><p><em>string</em></p></td><td><p>上传文件的访问权限，<code>private</code>表示私有。</p></td><td><p>private</p></td></tr><tr><td><p>data.x\_oss\_forbid\_overwrite</p></td><td><p><em>string</em></p></td><td><p>文件同名时是否可以覆盖，<code>true</code>表示不可覆盖。</p></td><td><p>true</p></td></tr></tbody></table>

##### 请求示例 <span id="2fcbfbaf30a0f" />

```bash
# 以下为北京地域 url，若使用新加坡地域，需将 url 替换为：https://dashscope-intl.aliyuncs.com/api/v1/uploads
curl --location 'https://dashscope.aliyuncs.com/api/v1/uploads?action=getPolicy&model=qwen3-vl-plus' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY" \
--header 'Content-Type: application/json'
```

<Note>
  若未配置阿里云百炼API Key到环境变量，请将`$DASHSCOPE_API_KEY`替换为实际API Key，例如：`--header "Authorization: Bearer sk-xxx"`。
</Note>

#### 响应示例 <span id="615ec0a5ed3lr" />

```json
{
    "request_id": "52f4383a-c67d-9f8c-xxxxxx",
    "data": {
        "policy": "eyJl...1ZSJ=",
        "signature": "YOUR_SIGNATURE",
        "upload_dir": "dashscope-instant/xxx/2024-07-18/xxx",
        "upload_host": "https://dashscope-file-xxx.oss-cn-beijing.aliyuncs.com",
        "expire_in_seconds": 300,
        "max_file_size_mb": 100,
        "capacity_limit_mb": 999999999,
        "oss_access_key_id": "LTA...",
        "x_oss_object_acl": "private",
        "x_oss_forbid_overwrite": "true"
    }
}
```

#### 步骤2：上传文件至临时存储空间 <span id="9bfba9d00bb8k" />

#### 前提条件 <span id="0d33d16fe2jue" />

- 已获取文件上传凭证。
- 确保文件上传凭证在有效期内，若凭证过期，请重新调用步骤1的接口获取新的凭证。

  > 查看文件上传凭证有效期：步骤1的输出参数`data.expire_in_seconds`为凭证有效期，单位为秒。

#### 请求接口 <span id="6215233bf0bl2" />

```http
POST {data.upload_host}
```

<Note>
  请将\{data.upload\_host}替换为步骤1的输出参数`data.upload_host`对应的值。
</Note>

#### 入参描述 <span id="22dc4afc8cadg" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "9.694415%" }} />

    <col style={{ width: "12.855638%" }} />

    <col style={{ width: "7.586934%" }} />

    <col style={{ width: "7.586934%" }} />

    <col style={{ width: "36.354057%" }} />

    <col style={{ width: "25.922023%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>传参方式</strong>
      </th>

      <th>
        <strong>字段</strong>
      </th>

      <th>
        <strong>类型</strong>
      </th>

      <th>
        <strong>必选</strong>
      </th>

      <th>
        <strong>描述</strong>
      </th>

      <th>
        <strong>示例值</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        Header
      </td>

      <td>
        Content-Type
      </td>

      <td>
        *string*
      </td>

      <td>
        否
      </td>

      <td>
        提交表单必须为`multipart/form-data`。

        在提交表单时，Content-Type会以`multipart/form-data;boundary=xxxxxx`的形式展示。

        > boundary 是自动生成的随机字符串，无需手动指定。若使用 SDK 拼接表单，SDK 也会自动生成该随机值。
      </td>

      <td>
        multipart/form-data; boundary=9431149156168
      </td>
    </tr>

    <tr>
      <td rowSpan={8}>
        form-data
      </td>

      <td>
        OSSAccessKeyId
      </td>

      <td>
        *text*
      </td>

      <td>
        是
      </td>

      <td>
        文件上传凭证接口的输出参数 `data.oss_access_key_id` 的值。
      </td>

      <td>
        LTAm5xxx
      </td>
    </tr>

    <tr>
      <td>
        policy
      </td>

      <td>
        *text*
      </td>

      <td>
        是
      </td>

      <td>
        文件上传凭证接口的输出参数 `data.policy` 的值。
      </td>

      <td>
        g5K...d40=
      </td>
    </tr>

    <tr>
      <td>
        Signature
      </td>

      <td>
        *text*
      </td>

      <td>
        是
      </td>

      <td>
        文件上传凭证接口的输出参数 `data.signature` 的值。
      </td>

      <td>
        YOUR\_SIGNATURE
      </td>
    </tr>

    <tr>
      <td>
        key
      </td>

      <td>
        *text*
      </td>

      <td>
        是
      </td>

      <td>
        文件上传凭证接口的输出参数 `data.upload_dir` 的值拼接上`/ 文件名`。
      </td>

      <td>
        例如，`upload_dir` 为 `dashscope-instant/xxx/2024-07-18/xxx`，需要上传的文件名为 `cat.png`，拼接后的完整路径为：

        `dashscope-instant/xxx/2024-07-18/xxx/cat.png`
      </td>
    </tr>

    <tr>
      <td>
        x-oss-object-acl
      </td>

      <td>
        *text*
      </td>

      <td>
        是
      </td>

      <td>
        文件上传凭证接口的输出参数 `data.x_oss_object_acl` 的值。
      </td>

      <td>
        private
      </td>
    </tr>

    <tr>
      <td>
        x-oss-forbid-overwrite
      </td>

      <td>
        *text*
      </td>

      <td>
        是
      </td>

      <td>
        文件上传凭证接口的输出参数中`data.x_oss_forbid_overwrite` 的值。
      </td>

      <td>
        true
      </td>
    </tr>

    <tr>
      <td>
        success\_action\_status
      </td>

      <td>
        *text*
      </td>

      <td>
        否
      </td>

      <td>
        通常取值为 200，上传完成后接口返回 HTTP code 200，表示操作成功。
      </td>

      <td>
        200
      </td>
    </tr>

    <tr>
      <td>
        file
      </td>

      <td>
        *text*
      </td>

      <td>
        是
      </td>

      <td>
        文件或文本内容。

        <Note>
          - 一次只支持上传一个文件。
          - file必须为最后一个表单域，除file以外的其他表单域并无顺序要求。
        </Note>
      </td>

      <td>
        例如，待上传文件`cat.png`在Linux系统中的存储路径为`/tmp`，则此处应为`file=@"/tmp/cat.png"`。
      </td>
    </tr>
  </tbody>
</table>

#### 出参描述 <span id="ec61f03079vzk" />

调用成功时，本接口无任何参数输出。

#### 请求示例 <span id="daa0012d98vst" />

```bash
curl --location 'https://dashscope-file-xxx.oss-cn-beijing.aliyuncs.com' \
--form 'OSSAccessKeyId="LTAm5xxx"' \
--form 'Signature="Sm/tv7DcZuTZftFVvt5yOoSETsc="' \
--form 'policy="eyJleHBpcmF0aW9 ... ... ... dHJ1ZSJ9XX0="' \
--form 'x-oss-object-acl="private"' \
--form 'x-oss-forbid-overwrite="true"' \
--form 'key="dashscope-instant/xxx/2024-07-18/xxx/cat.png"' \
--form 'success_action_status="200"' \
--form 'file=@"/tmp/cat.png"'
```

#### 步骤3：生成文件URL <span id="7c1b018ab4hwv" />

文件URL拼接逻辑：`oss://` + `key` （步骤2的入参`key`）。该URL有效期为 48 小时。

```plaintext
oss://dashscope-instant/xxx/2024-07-18/xxxx/cat.png
```

## 错误码 <span id="3b9b15a6a8qkl" />

如果接口调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

本文的API还有特定状态码，具体如下所示。

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "13.245033%" }} />

    <col style={{ width: "20.860927%" }} />

    <col style={{ width: "30.684327%" }} />

    <col style={{ width: "35.209713%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>HTTP状态码</strong>
      </th>

      <th>
        <strong>接口错误码（code）</strong>
      </th>

      <th>
        <strong>接口错误信息（message）</strong>
      </th>

      <th>
        <strong>含义说明</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        400
      </td>

      <td>
        invalid\_parameter\_error
      </td>

      <td>
        InternalError.Algo.InvalidParameter: The provided URL does not appear to be valid. Ensure it is correctly formatted.
      </td>

      <td>
        无效URL，请检查URL是否填写正确。

        > 若使用临时文件URL，需确保请求的 Header 中添加了参数 `X-DashScope-OssResourceResolve: enable`。
      </td>
    </tr>

    <tr>
      <td>
        400
      </td>

      <td>
        InvalidParameter.DataInspection
      </td>

      <td>
        The media format is not supported or incorrect for the data inspection.
      </td>

      <td>
        可能的原因有：

        - 请求Header 缺少必要参数，请设置 `X-DashScope-OssResourceResolve: enable`<strong>。</strong>
        - 上传的图片格式不符合模型要求，更多信息请参见[错误码](/zh/model-studio/error-code)。
      </td>
    </tr>

    <tr>
      <td>
        403
      </td>

      <td>
        AccessDenied
      </td>

      <td>
        Invalid according to Policy: Policy expired.
      </td>

      <td>
        文件上传凭证已经过期。

        请重新调用[文件上传凭证接口](/zh/model-studio/get-temporary-file-url#32db94982cllx)生成新凭证。
      </td>
    </tr>

    <tr>
      <td>
        429
      </td>

      <td>
        Throttling.RateQuota
      </td>

      <td>
        Requests rate limit exceeded, please try again later.
      </td>

      <td>
        调用频次触发限流。

        [文件上传凭证接口](/zh/model-studio/get-temporary-file-url#32db94982cllx)限流为 100 QPS（按阿里云主账号 + 模型维度）。触发限流后，建议降低请求频率，或迁移至 OSS 等自有存储服务以规避限制。
      </td>
    </tr>
  </tbody>
</table>

## 常见问题 <span id="b5dbd90a80t4l" />

#### Q：使用`oss://`前缀的 URL 调用时报错，该如何处理？ <span id="8eb407980dybc" />

A：请按以下步骤排查：

1. <strong>检查请求头（Header）</strong>：<br />若您通过 HTTP（如 Postman、curl）直接调用，<strong>必须在</strong>`Header`<strong>中添加参数</strong>`X-DashScope-OssResourceResolve: enable`。未添加该参数会导致服务端无法识别 OSS 内部协议。关于请求头配置，请参见[通过HTTP调用](/zh/model-studio/get-temporary-file-url#d6a1cb0f01h5k)。
2. <strong>检查 URL 有效性</strong>：<br />`oss://` 链接为临时 URL，请确保该链接是48小时内生成的。如果链接已过期，请重新上传文件获取新的 URL。

#### Q：文件上传与模型调用使用的API KEY可以不一样吗？ <span id="72fd224ef9nv0" />

A：文件存储和访问权限基于阿里云主账号管理，API Key 仅为主账号的访问凭证。

因此，同一阿里云主账号下的不同 API Key 可正常使用，不同主账号的 API Key因账号隔离，模型调用无法跨账号读取文件。

请确保文件上传与模型调用使用的 API Key 属于同一阿里云主账号。

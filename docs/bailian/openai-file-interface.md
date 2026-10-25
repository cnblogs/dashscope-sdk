> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# OpenAI文件接口兼容

> 文件上传接口用于上传文件，以便在 Qwen-Long 和 Qwen-Doc-Turbo 模型中进行文档问答与数据提取，或将其用作批量推理任务的输入文件。

<meta property="og:title" content="OpenAI文件接口兼容" />

## 使用方式 <span id="c1e6d7bc04x7q" />

支持通过OpenAI SDK（Python、Java）或HTTP API调用文件接口，包括上传、查询、删除等操作。

### 前提条件 <span id="618e4ed5b9oah" />

- 阿里云百炼API-KEY：[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)
- 使用OpenAI SDK调用服务，您还需安装[OpenAI SDK](/zh/model-studio/install-sdk)。

## 支持的模型 <span id="b75006f61am7t" />

文件ID可用于以下场景：

- <strong>Qwen-Long</strong>：通过文件ID进行长文档问答
- <strong>Qwen-Doc-Turbo</strong>：通过文件ID进行文件内数据提取与问答
- [<strong>批量推理</strong>](/zh/model-studio/batch-interfaces-compatible-with-openai)：通过文件ID上传批量任务输入文件

## 快速开始 <span id="efd2649f09bsz" />

### 上传文件 <span id="a091dc7c8c7be" />

百炼存储空间支持的最大文件数为10000个，总大小不超过100 GB，暂时没有有效期限制。当文件数量或总大小达到任一上限时，新的文件上传请求将会失败。请删除不再需要的文件以释放配额，然后才能继续上传。

<Tabs>
  <Tab title="用于文档分析">
    将purpose指定为`file-extract`，文件格式支持文本文件（ TXT、DOCX、PDF、XLSX、EPUB、MOBI、MD、CSV、JSON），图片文件（BMP、PNG、JPG/JPEG、GIF和PDF扫描件），单个文件最大为 <strong>150 MB</strong>。

    > 关于通过file\_id进行文档分析，请参考[长上下文（Qwen-Long）](/zh/model-studio/long-context-qwen-long)。

    #### 请求示例 <span id="1d076474dd4lf" />

    <CodeGroup dropdown>
      ```python
      import os
      from pathlib import Path
      from openai import OpenAI

      client = OpenAI(
          # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
      )

      # test.txt 是一个本地示例文件
      file_object = client.files.create(file=Path("test.txt"), purpose="file-extract")

      print(file_object.model_dump_json())
      ```

      ```java expandable
      import com.openai.client.OpenAIClient;
      import com.openai.client.okhttp.OpenAIOkHttpClient;
      import com.openai.models.files.*;

      import java.nio.file.Path;
      import java.nio.file.Paths;

      public class Main {
          public static void main(String[] args) {
              // 创建客户端，使用环境变量中的API密钥
              OpenAIClient client = OpenAIOkHttpClient.builder()
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .baseUrl("https://dashscope.aliyuncs.com/compatible-mode/v1")
                      .build();
              // 设置文件路径,请根据实际需求修改路径与文件名
              Path filePath = Paths.get("src/main/java/org/example/test.txt");
              // 创建文件上传参数
              FileCreateParams params = FileCreateParams.builder()
                      .file(filePath)
                      .purpose(FilePurpose.of("file-extract"))
                      .build();

              // 上传文件
              FileObject fileObject = client.files().create(params);
              System.out.println(fileObject);
          }
      }
      ```

      ```bash
      curl -X POST https://dashscope.aliyuncs.com/compatible-mode/v1/files \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --form 'file=@"test.txt"' \
      --form 'purpose="file-extract"'
      ```
    </CodeGroup>

    #### 响应示例 <span id="320c8b1c2cjzm" />

    ```json
    {
        "id": "file-fe-xxx",
        "bytes": 2055,
        "created_at": 1729065448,
        "filename": "test.txt",
        "object": "file",
        "purpose": "file-extract",
        "status": "processed",
        "status_details": null
    }
    ```
  </Tab>

  <Tab title="用于Batch调用">
    将purpose指定为`batch`，输入文件必须是jsonl文件且符合[输入文件大小与格式要求](/zh/model-studio/batch-interfaces-compatible-with-openai)，上传Batch任务的单个文件最大为500 MB。

    > 关于Batch调用的更多用法，请参考[OpenAI兼容-Batch（文件输入）](/zh/model-studio/batch-interfaces-compatible-with-openai)。

    #### 请求示例 <span id="f961be393c3bb" />

    <CodeGroup dropdown>
      ```python
      import os
      from pathlib import Path
      from openai import OpenAI

      client = OpenAI(
          # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
      )

      # test.jsonl 是一个本地示例文件
      file_object = client.files.create(file=Path("test.jsonl"), purpose="batch")

      print(file_object.model_dump_json())
      ```

      ```java expandable
      import com.openai.client.OpenAIClient;
      import com.openai.client.okhttp.OpenAIOkHttpClient;
      import com.openai.models.files.*;

      import java.nio.file.Path;
      import java.nio.file.Paths;

      public class Main {
          public static void main(String[] args) {
              // 创建客户端，使用环境变量中的API密钥
              OpenAIClient client = OpenAIOkHttpClient.builder()
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .baseUrl("https://dashscope.aliyuncs.com/compatible-mode/v1")
                      .build();
              // 设置文件路径,请根据实际需求修改路径与文件名
              Path filePath = Paths.get("src/main/java/org/example/test.txt");
              // 创建文件上传参数
              FileCreateParams params = FileCreateParams.builder()
                      .file(filePath)
                      .purpose(FilePurpose.of("batch"))
                      .build();

              // 上传文件
              FileObject fileObject = client.files().create(params);
              System.out.println(fileObject);
          }
      }
      ```

      ```bash
      curl -X POST https://dashscope.aliyuncs.com/compatible-mode/v1/files \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --form 'file=@"test.jsonl"' \
      --form 'purpose="batch"'
      ```
    </CodeGroup>

    #### 响应示例 <span id="11363c76823pf" />

    ```json
    {
        "id": "file-batch-xxx",
        "bytes": 231,
        "created_at": 1729065815,
        "filename": "test.jsonl",
        "object": "file",
        "purpose": "batch",
        "status": "processed",
        "status_details": null
    }
    ```
  </Tab>

  <Tab title="用于创建调优任务">
    将purpose指定为`fine-tune`，输入文件必须是jsonl文件且符合[输入文件大小与格式要求](/zh/model-studio/model-customization-file-management-service)，上传调优数据集/训练集的单个文件最大为300 MB。

    > 关于使用 API 进行模型调优的更多用法，请参考[模型调优](/zh/model-studio/fine-tuning-api-guide)

    #### 请求示例 <span id="71c92b3f2fpck" />

    <CodeGroup dropdown>
      ```python
      import os
      from pathlib import Path
      from openai import OpenAI

      client = OpenAI(
          # 若没有配置环境变量，请用阿里云百炼API Key将下行替换为：api_key="sk-xxx",
          api_key=os.getenv("DASHSCOPE_API_KEY"),
          base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
      )

      # file.jsonl 是一个本地示例文件
      file_object = client.files.create(file=Path("file.jsonl"), purpose="fine-tune")

      print(file_object.model_dump_json())
      ```

      ```java expandable
      import com.openai.client.OpenAIClient;
      import com.openai.client.okhttp.OpenAIOkHttpClient;
      import com.openai.models.files.*;

      import java.nio.file.Path;
      import java.nio.file.Paths;

      public class Main {
          public static void main(String[] args) {
              // 创建客户端，使用环境变量中的API密钥
              OpenAIClient client = OpenAIOkHttpClient.builder()
                      .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                      .baseUrl("https://dashscope.aliyuncs.com/compatible-mode/v1")
                      .build();
              // 设置文件路径,请根据实际需求修改路径与文件名
              Path filePath = Paths.get("src/main/java/org/example/file.jsonl");
              // 创建文件上传参数
              FileCreateParams params = FileCreateParams.builder()
                      .file(filePath)
                      .purpose(FilePurpose.of("fine-tune"))
                      .build();

              // 上传文件
              FileObject fileObject = client.files().create(params);
              System.out.println(fileObject);
          }
      }
      ```

      ```bash
      curl -X POST https://dashscope.aliyuncs.com/compatible-mode/v1/files \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      --form 'file=@"file.jsonl"' \
      --form 'purpose="fine-tune"'
      ```
    </CodeGroup>
  </Tab>
</Tabs>

### 查询文件信息 <span id="223fc94330wz5" />

通过在retrieve或GET方法中指定`file_id`来查询文件信息。

<Tabs>
  <Tab title="OpenAI Python SDK">
    #### 请求示例 <span id="dc15197eebf5n" />

    ```python
    import os
    from openai import OpenAI

    client = OpenAI(
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
    )

    file = client.files.retrieve(file_id="file-batch-xxx")

    print(file.model_dump_json())
    ```

    #### 返回示例 <span id="50433494402tj" />

    ```json
    {
      "id": "file-batch-xxx",
      "bytes": 27,
      "created_at": 1722480306,
      "filename": "test.txt",
      "object": "file",
      "purpose": "batch",
      "status": "processed",
      "status_details": null
    }
    ```
  </Tab>

  <Tab title="OpenAI Java SDK">
    ```java
    import com.openai.client.OpenAIClient;
    import com.openai.client.okhttp.OpenAIOkHttpClient;
    import com.openai.models.FileObject;
    import com.openai.models.FileRetrieveParams;

    public class Main {
        public static void main(String[] args) {
            // 创建客户端，使用环境变量中的API密钥
            OpenAIClient client = OpenAIOkHttpClient.builder()
                    .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                    .baseUrl("https://dashscope.aliyuncs.com/compatible-mode/v1")
                    .build();
            // 创建文件查询参数,请根据实际需求替换相应的fileId
            FileRetrieveParams params= FileRetrieveParams.builder()
                    .fileId("file-batch-xxx")
                    .build();
            //查询文件
            FileObject fileObject = client.files().retrieve(params);
            System.out.println(fileObject);
        }
    }
    ```
  </Tab>

  <Tab title="HTTP">
    #### 需要配置的endpoint <span id="b11e6f04d7fnh" />

    ```http
    GET https://dashscope-intl.aliyuncs.com/compatible-mode/v1/files/{file_id}
    ```

    #### 请求示例 <span id="e4c57eb780hem" />

    ```bash
    curl -X GET https://dashscope.aliyuncs.com/compatible-mode/v1/files/file-batch-xxx \
    -H "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    #### 返回示例 <span id="fdd2a3cf08cpc" />

    ```json
    {
        "id": "file-batch-xxx",
        "object": "file",
        "bytes": 499719,
        "created_at": 1715935833,
        "filename": "test.txt",
        "purpose": "batch",
        "status": "processed"
    }
    ```
  </Tab>
</Tabs>

### 查询文件列表 <span id="77a1a1d0bdq4n" />

返回所有文件的信息，包括通过上传文件接口上传的文件以及batch任务的结果文件。

<Note>
  此接口支持更多筛选参数，详情请参见[参数说明](/zh/model-studio/openai-file-interface#a9b268386f7x8)。
</Note>

<Tabs>
  <Tab title="OpenAI Python SDK">
    #### 请求示例 <span id="fa31e017a91b4" />

    ```python
    import os
    from openai import OpenAI

    client = OpenAI(
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
    )

    file_stk = client.files.list(after="file-batch-xxx",limit=20)
    print(file_stk.model_dump_json())
    ```

    #### 返回示例 <span id="13956abcefvqf" />

    ```json expandable
    {
      "data": [
        {
          "id": "file-batch-xxx",
          "bytes": 27,
          "created_at": 1722480543,
          "filename": "test.txt",
          "object": "file",
          "purpose": "batch",
          "status": "processed",
          "status_details": null
        },
        {
          "id": "file-batch-yyy",
          "bytes": 431986,
          "created_at": 1718089390,
          "filename": "test.pdf",
          "object": "file",
          "purpose": "batch",
          "status": "processed",
          "status_details": null
        }
      ],
      "object": "list",
      "has_more": false
    }
    ```
  </Tab>

  <Tab title="OpenAI Java SDK">
    ```java
    import com.openai.client.OpenAIClient;
    import com.openai.client.okhttp.OpenAIOkHttpClient;
    import com.openai.models.FileListPage;
    import com.openai.models.FileListParams;

    public class Main {
        public static void main(String[] args) {
            // 创建客户端，使用环境变量中的API密钥
            OpenAIClient client = OpenAIOkHttpClient.builder()
                    .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                    .baseUrl("https://dashscope.aliyuncs.com/compatible-mode/v1")
                    .build();
            // 创建文件列表查询参数
            FileListParams params = FileListParams.builder()
                    .after("file-batch-xxx")
                    .limit(20)
                    .build();
            //查询文件列表
            FileListPage file_stk = client.files().list(params);
            System.out.println(file_stk);
        }
    }
    ```
  </Tab>

  <Tab title="HTTP">
    #### 需要配置的endpoint <span id="f075ec68c96dk" />

    ```http
    GET https://dashscope-intl.aliyuncs.com/compatible-mode/v1/files
    ```

    #### 请求示例 <span id="af7ec16f4d2vd" />

    ```bash
    curl -X GET https://dashscope.aliyuncs.com/compatible-mode/v1/files \
    -H "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    #### 返回示例 <span id="23f7a3f725viz" />

    ```json
    {
        "object": "list",
        "has_more": true,
        "data": [
            {
                "id": "file-batch-xxx",
                "object": "file",
                "bytes": 84889,
                "created_at": 1715569225,
                "filename": "example.txt",
                "purpose": "batch",
                "status": "processed"
            },
            {
                "id": "file-batch-yyy",
                "object": "file",
                "bytes": 722355,
                "created_at": 1715413868,
                "filename": "Agent_survey.pdf",
                "purpose": "batch",
                "status": "processed"
            }
        ]
    }
    ```
  </Tab>
</Tabs>

### 删除文件 <span id="3457ce1d7ezr3" />

通过删除文件接口删除指定`file_id`的文件。可以通过[查询文件列表](/zh/model-studio/openai-file-interface#77a1a1d0bdq4n)接口查询文件信息。

<Tabs>
  <Tab title="OpenAI Python SDK">
    #### 请求示例 <span id="37d7aabdd710j" />

    ```python
    import os
    from openai import OpenAI

    client = OpenAI(
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
    )

    file_object = client.files.delete("file-batch-xxx")
    print(file_object.model_dump_json())
    ```

    #### 返回示例 <span id="91b8e19e964sx" />

    ```json
    {
      "object": "file",
      "deleted": true,
      "id": "file-batch-xxx"
    }
    ```
  </Tab>

  <Tab title="OpenAI Java SDK">
    ```java
    import com.openai.client.OpenAIClient;
    import com.openai.client.okhttp.OpenAIOkHttpClient;
    import com.openai.models.FileDeleteParams;

    public class Main {
        public static void main(String[] args) {
            OpenAIClient client = OpenAIOkHttpClient.builder()
                    .apiKey(System.getenv("DASHSCOPE_API_KEY"))
                    .baseUrl("https://dashscope.aliyuncs.com/compatible-mode/v1")
                    .build();
            FileDeleteParams params = FileDeleteParams.builder()
                    .fileId("file-batch-xxx")
                    .build();

            System.out.println(client.files().delete(params));
        }
    }
    ```
  </Tab>

  <Tab title="HTTP">
    #### 需要配置的endpoint <span id="7b0e148addyle" />

    ```http
    DELETE https://dashscope-intl.aliyuncs.com/compatible-mode/v1/files/{file_id}
    ```

    #### 请求示例 <span id="61f7514f1avj4" />

    ```bash
    curl -X  DELETE https://dashscope.aliyuncs.com/compatible-mode/v1/files/file-batch-xxx \
    -H "Authorization: Bearer $DASHSCOPE_API_KEY"
    ```

    #### 返回示例 <span id="2ac9ed70fa5dy" />

    ```json
    {
      "object": "file",
      "deleted": true,
      "id": "file-batch-oLIon7bzfxELqJBTS5okwC4E"
    }
    ```
  </Tab>
</Tabs>

## 计费说明 <span id="d5235cefad9sn" />

文件上传、存储和查询操作不产生费用。仅在调用模型API时，根据实际使用的输入Token和输出Token进行计费。

## 限流 <span id="c02e8d0489yi3" />

上传文件接口的QPS（每秒请求数）限制为3。查询文件信息、查询文件列表、删除文件接口的QPS总和限制为10。

## 应用于生产环境 <span id="e0515f28b4dol" />

- <strong>定期清理</strong>：定期删除不再使用的文件，避免达到10000个文件上限。
- <strong>状态检查</strong>：上传后检查文件状态，确保`status`为`processed`后再使用。
- <strong>限流检查</strong>：上传文件接口的QPS（每秒请求数）限制为3。查询文件信息、查询文件列表、删除文件接口的QPS总和限制为10。
- <strong>错误处理</strong>：实现完整的异常处理机制，包括网络错误、API错误等。

## 常见问题 <span id="444fba1975ivw" />

### 1. 文件上传后状态一直是"processing"怎么办？ <span id="1-processing-" />

文件处理需要一定时间，通常几秒内完成。如果长时间处于"processing"状态：

- 检查文件格式是否支持
- 检查文件大小是否超过限制
- 使用`retrieve`接口定期查询状态

### 2. 文件ID可以跨账号使用吗？ <span id="2-id-" />

不可以。文件ID仅在生成它的阿里云主账号内有效，不支持跨账号共享。

### 3. 上传的文件会被永久保存吗？ <span id="3-" />

是的，上传的文件会被永久保存在您的阿里云账号下，除非主动删除。建议定期清理不需要的文件。

### 4. 文件上传失败，可能的原因有哪些？ <span id="4-" />

- API Key无效或未配置
- 文件格式不支持
- 文件大小超过限制（file-extract: 150MB，batch: 500MB）
- 已达到文件数量上限（10000个）或总大小上限（100GB）
- 上传文件接口的QPS（每秒请求数）限制为3。

### 5. purpose参数应该如何选择？ <span id="5-purpose-file-extract-batch-" />

- `file-extract`：用于文档分析场景，配合Qwen-Long或Qwen-Doc-Turbo使用
- `batch`：用于批量推理任务，文件必须是符合格式要求的JSONL文件

## 参数说明 <span id="a9b268386f7x8" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "10.16%" }} />

    <col style={{ width: "8.97%" }} />

    <col style={{ width: "7.38%" }} />

    <col style={{ width: "5.91%" }} />

    <col style={{ width: "43.55%" }} />

    <col style={{ width: "24.03%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>接口类别</strong>
      </th>

      <th>
        <strong>参数名</strong>
      </th>

      <th>
        <strong>类型</strong>
      </th>

      <th>
        <strong>必选</strong>
      </th>

      <th>
        <strong>说明</strong>
      </th>

      <th>
        <strong>示例值</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td rowSpan={2}>
        文档上传
      </td>

      <td>
        file
      </td>

      <td>
        *File*
      </td>

      <td>
        是
      </td>

      <td>
        用于指定待上传的文件。
      </td>

      <td>
        Path("test.txt")
      </td>
    </tr>

    <tr>
      <td>
        purpose
      </td>

      <td>
        *String*
      </td>

      <td>
        是
      </td>

      <td>
        用于指定上传文件的用途。每种用途所需的文件格式、大小及内容规范，请参考对应功能文档中的详细要求。当前可选值如下：

        `file-extract`: 用于[Qwen-Long](/zh/model-studio/long-context-qwen-long)与[Qwen-Doc](/zh/model-studio/data-mining-qwen-doc)模型的文档理解与数据提取；

        `batch`: 用于[OpenAI兼容-Batch（文件输入）](/zh/model-studio/batch-interfaces-compatible-with-openai)任务；
      </td>

      <td>
        "file-extract"
      </td>
    </tr>

    <tr>
      <td rowSpan={6}>
        文件查询
      </td>

      <td>
        file\_id
      </td>

      <td>
        *String*
      </td>

      <td>
        是
      </td>

      <td>
        待查询的文件id。
      </td>

      <td>
        "file-fe-xxx"
      </td>
    </tr>

    <tr>
      <td>
        after
      </td>

      <td>
        *String*
      </td>

      <td>
        否
      </td>

      <td>
        [查询文件列表](/zh/model-studio/openai-file-interface#77a1a1d0bdq4n)任务中用于分页的游标。

        参数`after`的取值为当前分页的最后一个file\_id，表示查询该ID之后下一页的数据。

        例如，若本次查询返回了20条数据，且最后一个file\_id是file-batch-xxx，则后续查询时可以设置`after="file-batch-xxx"`，以获取列表的下一页。
      </td>

      <td>
        "file-fe-xxx"
      </td>
    </tr>

    <tr>
      <td>
        create\_before
      </td>

      <td>
        *String*
      </td>

      <td>
        否
      </td>

      <td>
        [查询文件列表](/zh/model-studio/openai-file-interface#77a1a1d0bdq4n)任务中，一个字符串格式的时间戳。用于筛选并返回创建时间<strong>早于</strong>该指定时间点的file\_id。
      </td>

      <td>
        `"20250306123000"`, `"2025-11-12 10:10:10"`, `"2025-11-12"`, `"20251112"`
      </td>
    </tr>

    <tr>
      <td>
        create\_after
      </td>

      <td>
        *String*
      </td>

      <td>
        否
      </td>

      <td>
        [查询文件列表](/zh/model-studio/openai-file-interface#77a1a1d0bdq4n)任务中，一个字符串格式的时间戳。用于筛选并返回创建时间<strong>晚于</strong>该指定时间点的file\_id。
      </td>

      <td>
        `"20250306123000"`, `"2025-11-12 10:10:10"`, `"2025-11-12"`, `"20251112"`
      </td>
    </tr>

    <tr>
      <td>
        purpose
      </td>

      <td>
        *String*
      </td>

      <td>
        否
      </td>

      <td>
        [查询文件列表](/zh/model-studio/openai-file-interface#77a1a1d0bdq4n)任务中根据文件的用途进行筛选，仅返回与指定 purpose（`file-extract`或`batch`）相匹配的file\_id。
      </td>

      <td>
        "batch"
      </td>
    </tr>

    <tr>
      <td>
        limit
      </td>

      <td>
        *Integer*
      </td>

      <td>
        否
      </td>

      <td>
        [查询文件列表](/zh/model-studio/openai-file-interface#77a1a1d0bdq4n)任务中每次查询返回的文件数量，取值范围\[1,2000]，默认2000。
      </td>

      <td>
        2000
      </td>
    </tr>

    <tr>
      <td>
        文件删除
      </td>

      <td>
        file\_id
      </td>

      <td>
        *String*
      </td>

      <td>
        是
      </td>

      <td>
        待删除文件id。
      </td>

      <td>
        "file-fe-xxx"
      </td>
    </tr>

    <tr>
      <td colSpan={6}>
        <strong>响应参数</strong>
      </td>
    </tr>

    <tr>
      <td rowSpan={7}>
        通用响应参数
      </td>

      <td>
        id
      </td>

      <td>
        *String*
      </td>

      <td rowSpan={10}>
        \\
      </td>

      <td>
        文件的标识符

        [删除文件](/zh/model-studio/openai-file-interface#3457ce1d7ezr3)任务中表示成功删除的文件的id。
      </td>

      <td>
        "file-fe-xxx"
      </td>
    </tr>

    <tr>
      <td>
        bytes
      </td>

      <td>
        *Integer*
      </td>

      <td>
        文件大小，单位为字节。
      </td>

      <td>
        81067
      </td>
    </tr>

    <tr>
      <td>
        created\_at
      </td>

      <td>
        *Integer*
      </td>

      <td>
        文件创建时的 Unix 时间戳（秒）。
      </td>

      <td>
        1617981067
      </td>
    </tr>

    <tr>
      <td>
        filename
      </td>

      <td>
        *String*
      </td>

      <td>
        上传的文件名。
      </td>

      <td>
        "text.txt"
      </td>
    </tr>

    <tr>
      <td>
        object
      </td>

      <td>
        *String*
      </td>

      <td>
        对象类型

        [查询文件列表](/zh/model-studio/openai-file-interface#77a1a1d0bdq4n)任务中始终为"list"。

        其余类型任务中始终为"file"。
      </td>

      <td>
        "file"
      </td>
    </tr>

    <tr>
      <td>
        purpose
      </td>

      <td>
        *String*
      </td>

      <td>
        文件的用途，取值有`batch`、`file-extract`、`batch_output`。
      </td>

      <td>
        "file-extract"
      </td>
    </tr>

    <tr>
      <td>
        status
      </td>

      <td>
        *String*
      </td>

      <td>
        文件的当前状态。可能的状态有：`uploaded`（已上传，等待解析）, `processing`（解析中，需等待解析完成后再引用）, `processed`（解析完成，可正常引用调用）, `error`（解析失败）。
      </td>

      <td>
        "processed"
      </td>
    </tr>

    <tr>
      <td rowSpan={2}>
        查询文件列表
      </td>

      <td>
        has\_more
      </td>

      <td>
        *Boolean*
      </td>

      <td>
        是否还有下一页数据。
      </td>

      <td>
        false
      </td>
    </tr>

    <tr>
      <td>
        data
      </td>

      <td>
        *Array*
      </td>

      <td>
        返回的文件列表，列表中每个元素格式与通用相应参数一致。
      </td>

      <td>
        ```json
        [{
         "id": "xxx",
         "bytes": 27,
         "created_at": 1722480543,
         "filename": "test.txt",
         "object": "file",
         "purpose": "batch",
         "status": "processed",
         "status_details": null
         }]
        ```
      </td>
    </tr>

    <tr>
      <td>
        删除文件
      </td>

      <td>
        deleted
      </td>

      <td>
        *Boolean*
      </td>

      <td>
        是否删除成功，true表示删除成功。
      </td>

      <td>
        true
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="efc117f59a2gm" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

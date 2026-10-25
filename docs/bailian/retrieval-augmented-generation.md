> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 知识检索增强工具（下线中）

> Assistant API 支持知识检索增强（RAG）工具，让智能体能够根据您的需求获取外部知识，例如私有产品知识或客户的偏好信息。本文介绍了一个简单的“手机导购”示例，帮助您快速上手RAG工具的基本使用方法。

<meta property="og:title" content="知识检索增强工具（下线中）" />

<Tip>
  Assistant API<strong>下线中</strong>，建议迁移至[Responses API](/zh/model-studio/qwen-api-via-openai-responses)：内置多种工具，并支持多轮上下文管理，可作为替代方案。
</Tip>

## 功能介绍 <span id="e962bd71eel9r" />

在传统的大模型应用中，大模型只能依赖于预先训练时的知识，无法动态地获取外部知识，从而无法回答：

- 时效性问题：在大模型预训练之后发生的事情。例如：对于2023年预训练的大模型，无法回答“2024年中国体育代表队获得的荣誉”。
- 私有知识问题：小范围公开的知识。例如：“公司有哪些规章制度”“谁负责 AI 产品的开发工作？”
- 长文档处理问题：需要从大量文档中获取精确信息。例如：“请你教我怎么部署大模型，以下是几本参考书籍。”

### 与控制台 RAG 应用的功能差异 <span id="8df0f11a85evd" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.35%" }} /><col style={{ width: "32.35%" }} /><col style={{ width: "42.3%" }} /></colgroup><thead><tr><th><p><strong>设置项</strong></p></th><th><p><strong>Assistant API RAG 应用</strong></p></th><th><p><strong>控制台百炼 RAG 应用</strong></p></th></tr></thead><tbody><tr><td><p>知识库相关操作</p></td><td><p>支持</p></td><td><p>支持</p></td></tr><tr><td><p>召回数量</p></td><td><p>支持</p></td><td><p>支持</p></td></tr><tr><td><p>控制台 RAG 应用支持的其他高级设置</p></td><td><p>不支持</p></td><td><p>支持</p></td></tr></tbody></table>

## 快速开始 <span id="2b61042866z9n" />

在这个示例中，您将使用示例知识文件创建一个测试知识库，并构建一个能够帮助用户选购新款手机的智能体（Assistant）。

<Note>
  本示例仅用于创建新知识库。要向已有知识库添加文件，或检索、更新和删除知识库，请参考[知识库API指南](/zh/model-studio/rag-knowledge-base-api-guide)。
</Note>

### 在您开始前 <span id="ad282467a9ydq" />

在开始使用 Assistant RAG 之前，请确保您已经：

- 注册了阿里云账号并开通了百炼服务。
- 安装了必要的 Python 库。

```bash
pip install alibabacloud_bailian20231229==1.8.2 dashscope
```

- 准备好了包含手机信息的文档[百炼系列手机产品介绍.docx](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20240911/hilxdt/%E7%99%BE%E7%82%BC%E7%B3%BB%E5%88%97%E6%89%8B%E6%9C%BA%E4%BA%A7%E5%93%81%E4%BB%8B%E7%BB%8D.docx)，用于创建知识库。
- 此外，您需要设置以下环境变量：

```bash
export ALIBABA_CLOUD_ACCESS_KEY_ID='您的阿里云访问密钥ID'
export ALIBABA_CLOUD_ACCESS_KEY_SECRET='您的阿里云访问密钥密码'
export DASHSCOPE_API_KEY='您的阿里云百炼API密钥'
export WORKSPACE_ID='您的阿里云百炼工作空间ID'
```

<Tip>
  `DASHSCOPE_API_KEY` 与 `ALIBABA_CLOUD_ACCESS_KEY_ID`/`ALIBABA_CLOUD_ACCESS_KEY_SECRET` 必须来自同一阿里云账号，否则 RAG 交互将会失败。
</Tip>

<Accordion title="完整代码示例">
  <CodeGroup>
    ```python 非流式输出 expandable
    import os
    import hashlib
    import requests
    import time
    from alibabacloud_bailian20231229.client import Client as bailian20231229Client
    from alibabacloud_tea_openapi import models as open_api_models
    from alibabacloud_bailian20231229 import models as bailian_20231229_models
    from alibabacloud_tea_util import models as util_models
    from dashscope import Assistants, Messages, Runs, Threads
    import sys
    def check_environment_variables():
        """检查并提示设置必要的环境变量"""
        required_vars = {
            'ALIBABA_CLOUD_ACCESS_KEY_ID': '阿里云访问密钥ID',
            'ALIBABA_CLOUD_ACCESS_KEY_SECRET': '阿里云访问密钥密码',
            'DASHSCOPE_API_KEY': '阿里云百炼API密钥',
            'WORKSPACE_ID': '阿里云百炼工作空间ID'
        }
        missing_vars = []
        for var, description in required_vars.items():
            if not os.environ.get(var):
                missing_vars.append(var)
                print(f"错误：请设置 {var} 环境变量 ({description})")
        if missing_vars:
            print("\n您可以使用以下命令设置环境变量：")
            for var in missing_vars:
                print(f"export {var}='您的{required_vars[var]}'")
            return False
        return True
    # 第一部分：使用阿里云百炼创建知识库
    def create_client() -> bailian20231229Client:
        """
        创建并配置阿里云百炼客户端。
        返回:
            bailian20231229Client: 配置好的客户端。
        """
        config = open_api_models.Config(
            access_key_id=os.environ.get('ALIBABA_CLOUD_ACCESS_KEY_ID'),
            access_key_secret=os.environ.get('ALIBABA_CLOUD_ACCESS_KEY_SECRET')
        )
        config.endpoint = 'bailian.cn-beijing.aliyuncs.com'
        return bailian20231229Client(config)
    def calculate_md5(file_path: str) -> str:
        """
        计算文件的 MD5 哈希值。
        参数:
            file_path (str): 文件路径。
        返回:
            str: 文件的 MD5 哈希值。
        """
        hash_md5 = hashlib.md5()
        with open(file_path, "rb") as f:
            for chunk in iter(lambda: f.read(4096), b""):
                hash_md5.update(chunk)
        return hash_md5.hexdigest()
    def get_file_size(file_path: str) -> int:
        """
        获取文件大小（以字节为单位）。
        参数:
            file_path (str): 文件路径。
        返回:
            int: 文件大小（以字节为单位）。
        """
        return os.path.getsize(file_path)
    def apply_lease(client, category_id, file_name, file_md5, file_size, workspace_id):
        """
        从阿里云百炼服务申请文件上传租约。
        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            category_id (str): 类别 ID。
            file_name (str): 文件名称。
            file_md5 (str): 文件的 MD5 哈希值。
            file_size (int): 文件大小（以字节为单位）。
            workspace_id (str): 业务空间 ID。
        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        request = bailian_20231229_models.ApplyFileUploadLeaseRequest(
            file_name=file_name,
            md_5=file_md5,
            size_in_bytes=file_size,
        )
        runtime = util_models.RuntimeOptions()
        return client.apply_file_upload_lease_with_options(category_id, workspace_id, request, headers, runtime)
    def upload_file(lease_id, upload_url, headers, file_path):
        """
        将文件上传到阿里云百炼服务。
        参数:
            lease_id (str): 租约 ID。
            upload_url (str): 上传 URL。
            headers (dict): 上传请求的头部。
            file_path (str): 文件路径。
        """
        with open(file_path, 'rb') as f:
            file_content = f.read()
        upload_headers = {
            "X-bailian-extra": headers["X-bailian-extra"],
            "Content-Type": headers["Content-Type"]
        }
        response = requests.put(upload_url, data=file_content, headers=upload_headers)
        response.raise_for_status()
    def add_file(client: bailian20231229Client, lease_id: str, parser: str, category_id: str, workspace_id: str):
        """
        将文件添加到阿里云百炼服务。
        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            lease_id (str): 租约 ID。
            parser (str): 用于文件的解析器。
            category_id (str): 类别 ID。
            workspace_id (str): 业务空间 ID。
        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        request = bailian_20231229_models.AddFileRequest(
            lease_id=lease_id,
            parser=parser,
            category_id=category_id,
        )
        runtime = util_models.RuntimeOptions()
        return client.add_file_with_options(workspace_id, request, headers, runtime)
    def describe_file(client, workspace_id, file_id):
        """
        在阿里云百炼服务中描述文件。
        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            workspace_id (str): 业务空间 ID。
            file_id (str): 文件 ID。
        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        runtime = util_models.RuntimeOptions()
        return client.describe_file_with_options(workspace_id, file_id, headers, runtime)
    def create_index(client, workspace_id, file_id, name, structure_type, source_type, sink_type):
        """
        为文件在阿里云百炼服务中创建知识库索引。
        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            workspace_id (str): 业务空间 ID。
            file_id (str): 文件 ID。
            name (str): 知识库索引名称。
            structure_type (str): 知识库的数据类型。
            source_type (str): 数据管理的数据类型。
            sink_type (str): 知识库的向量存储类型。
        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        request = bailian_20231229_models.CreateIndexRequest(
            structure_type=structure_type,
            name=name,
            source_type=source_type,
            sink_type=sink_type,
            document_ids=[file_id]
        )
        runtime = util_models.RuntimeOptions()
        return client.create_index_with_options(workspace_id, request, headers, runtime)
    def submit_index(client, workspace_id, index_id):
        """
        向阿里云百炼服务提交索引任务。
        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            workspace_id (str): 业务空间 ID。
            index_id (str): 索引 ID。
        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        submit_index_job_request = bailian_20231229_models.SubmitIndexJobRequest(
            index_id=index_id
        )
        runtime = util_models.RuntimeOptions()
        return client.submit_index_job_with_options(workspace_id, submit_index_job_request, headers, runtime)
    def get_index_job_status(client, workspace_id, job_id, index_id):
        """
        获取阿里云百炼服务中索引任务的状态。
        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            workspace_id (str): 业务空间 ID。
            job_id (str): 任务 ID。
            index_id (str): 索引 ID。
        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        get_index_job_status_request = bailian_20231229_models.GetIndexJobStatusRequest(
            index_id=index_id,
            job_id=job_id
        )
        runtime = util_models.RuntimeOptions()
        return client.get_index_job_status_with_options(workspace_id, get_index_job_status_request, headers, runtime)
    def create_knowledge_base(
        file_path: str,
        workspace_id: str,
        name: str
    ):
        """
        使用阿里云百炼服务创建知识库。
        参数:
            file_path (str): 文件路径。
            workspace_id (str): 业务空间 ID。
            name (str): 知识库名称。
        返回:
            str or None: 如果成功，返回索引 ID；否则返回 None。
        """
        # 设置默认值
        category_id = 'default'
        parser = 'DASHSCOPE_DOCMIND'
        source_type = 'DATA_CENTER_FILE'
        structure_type = 'unstructured'
        sink_type = 'DEFAULT'
        try:
            # 步骤1：创建客户端
            print("步骤1：创建阿里云百炼客户端")
            client = create_client()
            # 步骤2：准备文件信息
            print("步骤2：准备文件信息")
            file_name = os.path.basename(file_path)
            file_md5 = calculate_md5(file_path)
            file_size = get_file_size(file_path)
            # 步骤3：申请上传租约
            print("步骤3：向阿里云百炼申请上传租约")
            lease_response = apply_lease(client, category_id, file_name, file_md5, file_size, workspace_id)
            lease_id = lease_response.body.data.file_upload_lease_id
            upload_url = lease_response.body.data.param.url
            upload_headers = lease_response.body.data.param.headers
            # 步骤4：上传文件
            print("步骤4：上传文件到阿里云百炼")
            upload_file(lease_id, upload_url, upload_headers, file_path)
            # 步骤5：将文件添加到服务器
            print("步骤5：将文件添加到阿里云百炼服务器")
            add_response = add_file(client, lease_id, parser, category_id, workspace_id)
            file_id = add_response.body.data.file_id
            # 步骤6：检查文件状态
            print("步骤6：检查阿里云百炼中的文件状态")
            while True:
                describe_response = describe_file(client, workspace_id, file_id)
                status = describe_response.body.data.status
                print(f"当前文件状态：{status}")
                if status == 'INIT':
                    print("文件待解析，请稍候...")
                elif status == 'PARSING':
                    print("文件解析中，请稍候...")
                elif status == 'PARSE_SUCCESS':
                    print("文件解析完成！")
                    break
                else:
                    print(f"未知的文件状态：{status}，请联系技术支持。")
                    return None
                time.sleep(5)
            # 步骤7：创建知识文件索引
            print("步骤7：在阿里云百炼中创建知识文件索引")
            index_response = create_index(client, workspace_id, file_id, name, structure_type, source_type, sink_type)
            index_id = index_response.body.data.id
            # 步骤8：提交索引任务
            print("步骤8：向阿里云百炼提交索引任务")
            submit_response = submit_index(client, workspace_id, index_id)
            job_id = submit_response.body.data.id
            # 步骤9：获取索引任务状态
            print("步骤9：获取阿里云百炼索引任务状态")
            while True:
                get_index_job_status_response = get_index_job_status(client, workspace_id, job_id, index_id)
                status = get_index_job_status_response.body.data.status
                print(f"当前索引任务状态：{status}")
                if status == 'COMPLETED':
                    break
                time.sleep(5)
            print("阿里云百炼知识库创建成功！")
            return index_id
        except Exception as e:
            print(f"发生错误：{e}")
            return None
    # 第二部分：在 Assistant API 中使用知识库
    def create_assistant(index_id):
        """创建一个使用指定知识库的 Assistant。"""
        assistant = Assistants.create(
            model='qwen-plus',  # 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
            name='智能手机选购助手',
            description='一个帮助用户选择手机的智能助手。',
            instructions='你是一个手机选购向导，你的任务是帮助用户选择满意的手机。使用提供的知识库来回答用户的问题。以下信息可能对你有帮助：${documents}。',
            tools=[
                {
                    "type": "rag",  # 指定使用RAG（检索增强生成）模式
                    "prompt_ra": {
                        "pipeline_id": [index_id],  # 指定使用的知识库索引ID
                        "multiknowledge_rerank_top_n": 10,  # 多知识源重排序时返回的top N结果数
                        "rerank_top_n": 5,  # 最终重排序后返回的top N结果数
                        "parameters": {
                            "type": "object",
                            "properties": {
                                "query_word": {
                                    "type": "str",
                                    "value": "${documents}"  # 使用动态占位符，将被实际查询内容替换
                                }
                            }
                        }
                    }
                },
            ]
        )
        return assistant.id
    class SuppressPrint:
        def __enter__(self):
            self._original_stdout = sys.stdout
            sys.stdout = open(os.devnull, 'w')
        def __exit__(self, exc_type, exc_val, exc_tb):
            sys.stdout.close()
            sys.stdout = self._original_stdout
    def send_message(thread, assistant, message):
        """向 Assistant 发送消息并获取回复。"""
        print(f"用户: {message}")
        message = Messages.create(thread_id=thread.id, content=message)
        run = Runs.create(thread_id=thread.id, assistant_id=assistant.id)

        with SuppressPrint():
            run_status = Runs.wait(run.id, thread_id=thread.id)

        msgs = Messages.list(thread.id)
        assistant_reply = msgs['data'][0]['content'][0]['text']['value']
        print(f"助手: {assistant_reply}")
    def interact_with_assistant(assistant):
        print("\n步骤3：与 Assistant 交互")
        thread = Threads.create()  # 创建一个新的对话线程
        while True:
            user_input = input("\n请输入您的问题（输入 'quit' 退出）：")
            if user_input.lower() == 'quit':
                break

            max_retries = 3
            for attempt in range(max_retries):
                try:
                    send_message(thread, assistant, user_input)
                    break
                except Exception as e:
                    if attempt < max_retries - 1:
                        print(f"发送消息失败，正在重试... (尝试 {attempt + 2}/{max_retries})")
                    else:
                        print(f"发送消息失败：{str(e)}。请稍后再试。")
        print("感谢使用阿里云百炼 RAG 教程！")
    def main():
        print("欢迎使用阿里云百炼 RAG 教程！")

        if not check_environment_variables():
            return
        start_option = input("请选择开始步骤：\n1. 从头开始（创建知识库和Assistant）\n2. 直接开始与Assistant交互\n请输入选项（1或2）：")
        if start_option == '1':
            # 步骤1：创建知识库
            print("\n步骤1：创建知识库")
            file_path = input("请输入文件路径（包含手机信息的文本文件）：")
            kb_name = input("请输入知识库名称：")
            workspace_id = os.environ.get('WORKSPACE_ID')
            index_id = create_knowledge_base(file_path, workspace_id, kb_name)
            if not index_id:
                print("知识库创建失败，程序退出。")
                return
            print(f"知识库创建成功，索引ID为：{index_id}")
            # 步骤2：创建 Assistant
            print("\n步骤2：创建 Assistant")
            assistant_id = create_assistant(index_id)
            print(f"Assistant 创建成功！Assistant ID: {assistant_id}")
            assistant = Assistants.get(assistant_id)
            if not assistant:
                print("Assistant 创建失败，程序退出。")
                return
            print("Assistant 创建成功！")
        elif start_option == '2':
            # 直接使用已有的 Assistant
            print("\n使用已有的 Assistant")
            assistant_id = input("请输入已有的 Assistant ID：")
            assistant = Assistants.get(assistant_id)
            if not assistant:
                print("无法获取指定的 Assistant，程序退出。")
                return
            print("成功获取 Assistant！")
        else:
            print("无效的选项，程序退出。")
            return
        # 步骤3：与 Assistant 交互
        interact_with_assistant(assistant)
    if __name__ == '__main__':
        main()
    ```

    ```python 流式输出 expandable
    import os
    import hashlib
    import requests
    import time
    from alibabacloud_bailian20231229.client import Client as bailian20231229Client
    from alibabacloud_tea_openapi import models as open_api_models
    from alibabacloud_bailian20231229 import models as bailian_20231229_models
    from alibabacloud_tea_util import models as util_models
    from dashscope import Assistants, Messages, Runs, Threads
    import sys

    def check_environment_variables():
        """检查并提示设置必要的环境变量"""
        required_vars = {
            'ALIBABA_CLOUD_ACCESS_KEY_ID': '阿里云访问密钥ID',
            'ALIBABA_CLOUD_ACCESS_KEY_SECRET': '阿里云访问密钥密码',
            'DASHSCOPE_API_KEY': '阿里云百炼API密钥',
            'WORKSPACE_ID': '阿里云百炼工作空间ID'
        }

        missing_vars = []
        for var, description in required_vars.items():
            if not os.environ.get(var):
                missing_vars.append(var)
                print(f"错误：请设置 {var} 环境变量 ({description})")

        if missing_vars:
            print("\n您可以使用以下命令设置环境变量：")
            for var in missing_vars:
                print(f"export {var}='您的{required_vars[var]}'")
            return False
        return True

    # 第一部分：使用阿里云百炼创建知识库

    def create_client() -> bailian20231229Client:
        """
        创建并配置阿里云百炼客户端。

        返回:
            bailian20231229Client: 配置好的客户端。
        """
        config = open_api_models.Config(
            access_key_id=os.environ.get('ALIBABA_CLOUD_ACCESS_KEY_ID'),
            access_key_secret=os.environ.get('ALIBABA_CLOUD_ACCESS_KEY_SECRET')
        )
        config.endpoint = 'bailian.cn-beijing.aliyuncs.com'
        return bailian20231229Client(config)

    def calculate_md5(file_path: str) -> str:
        """
        计算文件的 MD5 哈希值。

        参数:
            file_path (str): 文件路径。

        返回:
            str: 文件的 MD5 哈希值。
        """
        hash_md5 = hashlib.md5()
        with open(file_path, "rb") as f:
            for chunk in iter(lambda: f.read(4096), b""):
                hash_md5.update(chunk)
        return hash_md5.hexdigest()

    def get_file_size(file_path: str) -> int:
        """
        获取文件大小（以字节为单位）。

        参数:
            file_path (str): 文件路径。

        返回:
            int: 文件大小（以字节为单位）。
        """
        return os.path.getsize(file_path)

    def apply_lease(client, category_id, file_name, file_md5, file_size, workspace_id):
        """
        从阿里云百炼服务申请文件上传租约。

        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            category_id (str): 类别 ID。
            file_name (str): 文件名称。
            file_md5 (str): 文件的 MD5 哈希值。
            file_size (int): 文件大小（以字节为单位）。
            workspace_id (str): 业务空间 ID。

        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        request = bailian_20231229_models.ApplyFileUploadLeaseRequest(
            file_name=file_name,
            md_5=file_md5,
            size_in_bytes=file_size,
        )
        runtime = util_models.RuntimeOptions()
        return client.apply_file_upload_lease_with_options(category_id, workspace_id, request, headers, runtime)

    def upload_file(lease_id, upload_url, headers, file_path):
        """
        将文件上传到阿里云百炼服务。

        参数:
            lease_id (str): 租约 ID。
            upload_url (str): 上传 URL。
            headers (dict): 上传请求的头部。
            file_path (str): 文件路径。
        """
        with open(file_path, 'rb') as f:
            file_content = f.read()
        upload_headers = {
            "X-bailian-extra": headers["X-bailian-extra"],
            "Content-Type": headers["Content-Type"]
        }
        response = requests.put(upload_url, data=file_content, headers=upload_headers)
        response.raise_for_status()

    def add_file(client: bailian20231229Client, lease_id: str, parser: str, category_id: str, workspace_id: str):
        """
        将文件添加到阿里云百炼服务。

        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            lease_id (str): 租约 ID。
            parser (str): 用于文件的解析器。
            category_id (str): 类别 ID。
            workspace_id (str): 业务空间 ID。

        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        request = bailian_20231229_models.AddFileRequest(
            lease_id=lease_id,
            parser=parser,
            category_id=category_id,
        )
        runtime = util_models.RuntimeOptions()
        return client.add_file_with_options(workspace_id, request, headers, runtime)

    def describe_file(client, workspace_id, file_id):
        """
        在阿里云百炼服务中描述文件。

        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            workspace_id (str): 业务空间 ID。
            file_id (str): 文件 ID。

        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        runtime = util_models.RuntimeOptions()
        return client.describe_file_with_options(workspace_id, file_id, headers, runtime)

    def create_index(client, workspace_id, file_id, name, structure_type, source_type, sink_type):
        """
        为文件在阿里云百炼服务中创建知识库索引。

        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            workspace_id (str): 业务空间 ID。
            file_id (str): 文件 ID。
            name (str): 知识库索引名称。
            structure_type (str): 知识库的数据类型。
            source_type (str): 数据管理的数据类型。
            sink_type (str): 知识库的向量存储类型。

        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        request = bailian_20231229_models.CreateIndexRequest(
            structure_type=structure_type,
            name=name,
            source_type=source_type,
            sink_type=sink_type,
            document_ids=[file_id]
        )
        runtime = util_models.RuntimeOptions()
        return client.create_index_with_options(workspace_id, request, headers, runtime)

    def submit_index(client, workspace_id, index_id):
        """
        向阿里云百炼服务提交索引任务。

        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            workspace_id (str): 业务空间 ID。
            index_id (str): 索引 ID。

        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        submit_index_job_request = bailian_20231229_models.SubmitIndexJobRequest(
            index_id=index_id
        )
        runtime = util_models.RuntimeOptions()
        return client.submit_index_job_with_options(workspace_id, submit_index_job_request, headers, runtime)

    def get_index_job_status(client, workspace_id, job_id, index_id):
        """
        获取阿里云百炼服务中索引任务的状态。

        参数:
            client (bailian20231229Client): 阿里云百炼客户端。
            workspace_id (str): 业务空间 ID。
            job_id (str): 任务 ID。
            index_id (str): 索引 ID。

        返回:
            阿里云百炼服务的响应。
        """
        headers = {}
        get_index_job_status_request = bailian_20231229_models.GetIndexJobStatusRequest(
            index_id=index_id,
            job_id=job_id
        )
        runtime = util_models.RuntimeOptions()
        return client.get_index_job_status_with_options(workspace_id, get_index_job_status_request, headers, runtime)

    def create_knowledge_base(
        file_path: str,
        workspace_id: str,
        name: str
    ):
        """
        使用阿里云百炼服务创建知识库。

        参数:
            file_path (str): 文件路径。
            workspace_id (str): 业务空间 ID。
            name (str): 知识库名称。

        返回:
            str or None: 如果成功，返回索引 ID；否则返回 None。
        """
        # 设置默认值
        category_id = 'default'
        parser = 'DASHSCOPE_DOCMIND'
        source_type = 'DATA_CENTER_FILE'
        structure_type = 'unstructured'
        sink_type = 'DEFAULT'

        try:
            # 步骤1：创建客户端
            print("步骤1：创建阿里云百炼客户端")
            client = create_client()

            # 步骤2：准备文件信息
            print("步骤2：准备文件信息")
            file_name = os.path.basename(file_path)
            file_md5 = calculate_md5(file_path)
            file_size = get_file_size(file_path)

            # 步骤3：申请上传租约
            print("步骤3：向阿里云百炼申请上传租约")
            lease_response = apply_lease(client, category_id, file_name, file_md5, file_size, workspace_id)
            lease_id = lease_response.body.data.file_upload_lease_id
            upload_url = lease_response.body.data.param.url
            upload_headers = lease_response.body.data.param.headers

            # 步骤4：上传文件
            print("步骤4：上传文件到阿里云百炼")
            upload_file(lease_id, upload_url, upload_headers, file_path)

            # 步骤5：将文件添加到服务器
            print("步骤5：将文件添加到阿里云百炼服务器")
            add_response = add_file(client, lease_id, parser, category_id, workspace_id)
            file_id = add_response.body.data.file_id

            # 步骤6：检查文件状态
            print("步骤6：检查阿里云百炼中的文件状态")
            while True:
                describe_response = describe_file(client, workspace_id, file_id)
                status = describe_response.body.data.status
                print(f"当前文件状态：{status}")
                if status == 'INIT':
                    print("文件待解析，请稍候...")
                elif status == 'PARSING':
                    print("文件解析中，请稍候...")
                elif status == 'PARSE_SUCCESS':
                    print("文件解析完成！")
                    break
                else:
                    print(f"未知的文件状态：{status}，请联系技术支持。")
                    return None
                time.sleep(5)

            # 步骤7：创建知识文件索引
            print("步骤7：在阿里云百炼中创建知识文件索引")
            index_response = create_index(client, workspace_id, file_id, name, structure_type, source_type, sink_type)
            index_id = index_response.body.data.id

            # 步骤8：提交索引任务
            print("步骤8：向阿里云百炼提交索引任务")
            submit_response = submit_index(client, workspace_id, index_id)
            job_id = submit_response.body.data.id

            # 步骤9：获取索引任务状态
            print("步骤9：获取阿里云百炼索引任务状态")
            while True:
                get_index_job_status_response = get_index_job_status(client, workspace_id, job_id, index_id)
                status = get_index_job_status_response.body.data.status
                print(f"当前索引任务状态：{status}")
                if status == 'COMPLETED':
                    break
                time.sleep(5)

            print("阿里云百炼知识库创建成功！")
            return index_id

        except Exception as e:
            print(f"发生错误：{e}")
            return None

    # 第二部分：在 Assistant API 中使用知识库

    def create_assistant(index_id):
        """创建一个使用指定知识库的 Assistant。"""
        assistant = Assistants.create(
            model='qwen-plus',  # 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
            name='智能手机选购助手',
            description='一个帮助用户选择手机的智能助手。',
            instructions='你是一个手机选购向导，你的任务是帮助用户选择满意的手机。使用提供的知识库来回答用户的问题。以下信息可能对你有帮助：${document1}。',
            tools=[
                {
                    "type": "rag",  # 指定使用RAG（检索增强生成）模式
                    "prompt_ra": {
                        "pipeline_id": [index_id],  # 指定使用的知识库索引ID
                        "multiknowledge_rerank_top_n": 10,  # 多知识源重排序时返回的top N结果数
                        "rerank_top_n": 5,  # 最终重排序后返回的top N结果数
                        "parameters": {
                            "type": "object",
                            "properties": {
                                "query_word": {
                                    "type": "str",
                                    "value": "${document1}"  # 使用动态占位符，将被实际查询内容替换
                                }
                            }
                        }
                    }
                },
            ]
        )
        return assistant.id

    def send_message(thread, assistant, message):
        """向 Assistant 发送消息并获取回复。"""
        print(f"用户: {message}")

        message = Messages.create(thread_id=thread.id, content=message)

        # 启动流式对话，设置stream=True来获取实时响应
        run_iterator = Runs.create(
            thread_id=thread.id,
            assistant_id=assistant.id,
            stream=True
        )

        print("助手: ", end='', flush=True)  # 打印助手标识，但不换行

        try:
            # 处理不同类型的流式响应事件
            for event, data in run_iterator:
                # 当一个步骤完成时打印换行
                if event == 'thread.run.step.completed':
                    print("\n", end='')
                # 处理Assistant的文本消息
                if event == 'thread.message.delta':
                    print(data.delta.content.text.value, end='', flush=True)

        # 异常处理：支持用户中断和错误处理
        except KeyboardInterrupt:
            run_iterator.close()
            print("\n输出被用户中断")
        except Exception as e:
            print(f"\n遇到错误了：{str(e)}")
            run_iterator.close()
        finally:
            print()  # 确保最后有换行

    def interact_with_assistant(assistant):
        print("\n步骤3：与 Assistant 交互")
        thread = Threads.create()  # 创建一个新的对话线程
        while True:
            user_input = input("\n请输入您的问题（输入 'quit' 退出）：")
            if user_input.lower() == 'quit':
                break

            max_retries = 3
            for attempt in range(max_retries):
                try:
                    send_message(thread, assistant, user_input)
                    break
                except Exception as e:
                    if attempt < max_retries - 1:
                        print(f"\n发送消息失败，正在重试... (尝试 {attempt + 2}/{max_retries})")
                        time.sleep(1)  # 添加延迟，避免立即重试
                    else:
                        print(f"\n发送消息失败：{str(e)}。请稍后再试。")

        print("感谢使用阿里云百炼 RAG 教程！")

    def main():
        print("欢迎使用阿里云百炼 RAG 教程！")

        if not check_environment_variables():
            return

        start_option = input("请选择开始步骤：\n1. 从头开始（创建知识库和Assistant）\n2. 直接开始与Assistant交互\n请输入选项（1或2）：")

        if start_option == '1':
            # 步骤1：创建知识库
            print("\n步骤1：创建知识库")
            file_path = input("请输入文件路径（包含手机信息的文本文件）：")
            kb_name = input("请输入知识库名称：")
            workspace_id = os.environ.get('WORKSPACE_ID')

            index_id = create_knowledge_base(file_path, workspace_id, kb_name)
            if not index_id:
                print("知识库创建失败，程序退出。")
                return

            print(f"知识库创建成功，索引ID为：{index_id}")

            # 步骤2：创建 Assistant
            print("\n步骤2：创建 Assistant")
            assistant_id = create_assistant(index_id)
            print(f"Assistant 创建成功！Assistant ID: {assistant_id}")
            assistant = Assistants.get(assistant_id)
            if not assistant:
                print("Assistant 创建失败，程序退出。")
                return

            print("Assistant 创建成功！")

        elif start_option == '2':
            # 直接使用已有的 Assistant
            print("\n使用已有的 Assistant")
            assistant_id = input("请输入已有的 Assistant ID：")
            assistant = Assistants.get(assistant_id)
            if not assistant:
                print("无法获取指定的 Assistant，程序退出。")
                return
            print("成功获取 Assistant！")

        else:
            print("无效的选项，程序退出。")
            return

        # 步骤3：与 Assistant 交互
        interact_with_assistant(assistant)

    if __name__ == '__main__':
        main()
    ```
  </CodeGroup>
</Accordion>

### 创建知识库 <span id="556b8c56e3ry5" />

为了访问知识文档，RAG工具会检索<strong>阿里云百炼知识库</strong>的<strong>知识索引</strong>。上传您的知识文件到百炼知识库，并创建知识索引。以下是两种配置方法，您可以选择任意一种：[通过API上传文件并创建索引](/zh/model-studio/upload-documents-by-calling-api)，或[通过百炼控制台上传文件并创建索引](/zh/model-studio/rag-knowledge-base)。

这里提供了一个代码示例，帮助您一键完成知识库搭建。您需要准备：

- 待上传的文档路径（例如：\~/test.pdf）。

  > 当前 API 仅支持 PDF、DOCX 等非结构化数据上传，结构化数据请使用控制台上传。
- 待创建的知识库名称。（例如：测试知识库）

#### 第一步：创建阿里云百炼客户端 <span id="66d5e53e5cnzt" />

在开始上传文件和创建知识库之前，您需要初始化阿里云百炼的客户端，以完成身份验证和端点配置。您可以按照以下示例配置您的客户端：

```python
def create_client() -> bailian20231229Client:
    """
    创建并配置阿里云百炼客户端。

    返回:
        bailian20231229Client: 配置好的客户端。
    """
    config = open_api_models.Config(
        access_key_id=os.environ.get('ALIBABA_CLOUD_ACCESS_KEY_ID'),
        access_key_secret=os.environ.get('ALIBABA_CLOUD_ACCESS_KEY_SECRET')
    )
    config.endpoint = 'bailian.cn-beijing.aliyuncs.com'
    return bailian20231229Client(config)
```

创建完成后，您将得到一个Client对象，用于后续的 API 调用。

#### 第二步：申请文件上传租约 <span id="6455620b4alx2" />

在上传文件前，您需要申请一个文件上传租约。租约是一个临时的授权，允许您在限定时间内上传文件到阿里云百炼服务。以下是申请文件上传租约的示例代码：

```python
def apply_lease(client, category_id, file_name, file_md5, file_size, workspace_id):
    """
    从阿里云百炼服务申请文件上传租约。

    参数:
        client (bailian20231229Client): 阿里云百炼客户端。
        category_id (str): 类别 ID。
        file_name (str): 文件名称。
        file_md5 (str): 文件的 MD5 哈希值。
        file_size (int): 文件大小（以字节为单位）。
        workspace_id (str): 业务空间 ID。

    返回:
        阿里云百炼服务的响应。
    """
    headers = {}
    request = bailian_20231229_models.ApplyFileUploadLeaseRequest(
        file_name=file_name,
        md_5=file_md5,
        size_in_bytes=file_size,
    )
    runtime = util_models.RuntimeOptions()
    return client.apply_file_upload_lease_with_options(category_id, workspace_id, request, headers, runtime)
```

您可能需要提供文件的 MD5 哈希值和文件大小，本文提供了简单的计算方法以供参考：

```python expandable
import hashlib

def calculate_md5(file_path: str) -> str:
    """
    计算文件的 MD5 哈希值。

    参数:
        file_path (str): 文件路径。

    返回:
        str: 文件的 MD5 哈希值。
    """
    hash_md5 = hashlib.md5()
    with open(file_path, "rb") as f:
        for chunk in iter(lambda: f.read(4096), b""):
            hash_md5.update(chunk)
    return hash_md5.hexdigest()

def get_file_size(file_path: str) -> int:
    """
    获取文件大小（以字节为单位）。

    参数:
        file_path (str): 文件路径。

    返回:
        int: 文件大小（以字节为单位）。
    """
    return os.path.getsize(file_path)
```

申请租约成功后，您将得到用于上传文件的临时请求头和临时URL，下一步骤中将使用这两项来上传文件。

#### 第三步：上传文件到阿里云百炼 <span id="326aefa86689k" />

成功获取上传租约后，使用租约中的临时请求头和临时URL将文件上传至阿里云百炼服务器。

```python
def upload_file(lease_id, upload_url, headers, file_path):
    """
    将文件上传到阿里云百炼服务。

    参数:
        lease_id (str): 租约 ID。
        upload_url (str): 上传 URL。
        headers (dict): 上传请求的头部。
        file_path (str): 文件路径。
    """
    with open(file_path, 'rb') as f:
        file_content = f.read()
    upload_headers = {
        "X-bailian-extra": headers["X-bailian-extra"],
        "Content-Type": headers["Content-Type"]
    }
    response = requests.put(upload_url, data=file_content, headers=upload_headers)
    response.raise_for_status()
```

#### 第四步：添加文件到默认类目 <span id="d39928e78dcbe" />

百炼使用类目管理您上传的文件。因此，您需要将已上传的文件添加到类目中。百炼会自动创建一个默认类目（`category_id='default'`），以下是将文件添加至默认类目的方法。

```python
def add_file(client: bailian20231229Client, lease_id: str, parser: str, category_id: str, workspace_id: str):
    """
    将文件添加到阿里云百炼服务。

    参数:
        client (bailian20231229Client): 阿里云百炼客户端。
        lease_id (str): 租约 ID。
        parser (str): 用于文件的解析器。
        category_id (str): 类别 ID。
        workspace_id (str): 业务空间 ID。

    返回:
        阿里云百炼服务的响应。
    """
    headers = {}
    request = bailian_20231229_models.AddFileRequest(
        lease_id=lease_id,
        parser=parser,
        category_id=category_id,
    )
    runtime = util_models.RuntimeOptions()
    return client.add_file_with_options(workspace_id, request, headers, runtime)
```

完成添加后，阿里云百炼会自动开始解析您的文档。

#### 第五步：查询文件的解析状态 <span id="ccadc65347ur4" />

您可以查询文件的解析状态，当解析完成后，您就可以创建知识库了。以下是查询文件的解析状态的示例代码：

```python
def describe_file(client, workspace_id, file_id):
    """
    在阿里云百炼服务中描述文件。

    参数:
        client (bailian20231229Client): 阿里云百炼客户端。
        workspace_id (str): 业务空间 ID。
        file_id (str): 文件 ID。

    返回:
        阿里云百炼服务的响应。
    """
    headers = {}
    runtime = util_models.RuntimeOptions()
    return client.describe_file_with_options(workspace_id, file_id, headers, runtime)
```

#### 第六步：创建知识库索引 <span id="6ae23545a27w6" />

您可以为文件创建一个知识库索引，以便稍后在 Assistant API 中使用。

```python expandable
def create_index(client, workspace_id, file_id, name, structure_type, source_type, sink_type):
    """
    为文件在阿里云百炼服务中创建知识库索引。

    参数:
        client (bailian20231229Client): 阿里云百炼客户端。
        workspace_id (str): 业务空间 ID。
        file_id (str): 文件 ID。
        name (str): 知识库索引名称。
        structure_type (str): 知识库的数据类型。
        source_type (str): 数据管理的数据类型。
        sink_type (str): 知识库的向量存储类型。

    返回:
        阿里云百炼服务的响应。
    """
    headers = {}
    request = bailian_20231229_models.CreateIndexRequest(
        structure_type=structure_type,
        name=name,
        source_type=source_type,
        sink_type=sink_type,
        document_ids=[file_id]
    )
    runtime = util_models.RuntimeOptions()
    return client.create_index_with_options(workspace_id, request, headers, runtime)
```

#### 第七步：提交索引任务 <span id="bd91071f3595v" />

创建索引后，您需要提交索引任务，让阿里云百炼开始构建知识库索引。

```python
def submit_index(client, workspace_id, index_id):
    """
    向阿里云百炼服务提交索引任务。

    参数:
        client (bailian20231229Client): 阿里云百炼客户端。
        workspace_id (str): 业务空间 ID。
        index_id (str): 索引 ID。

    返回:
        阿里云百炼服务的响应。
    """
    headers = {}
    submit_index_job_request = bailian_20231229_models.SubmitIndexJobRequest(
        index_id=index_id
    )
    runtime = util_models.RuntimeOptions()
    return client.submit_index_job_with_options(workspace_id, submit_index_job_request, headers, runtime)
```

### 创建 Assistant <span id="301dc4410dcds" />

#### 第八步：创建 Assistant <span id="2b292dcd89ypn" />

知识库创建完成后，您可以使用该知识库与Assistant API结合，创建一个智能助手。以下是创建Assistant的示例代码：

```python expandable
def create_assistant(index_id):
    """创建一个使用指定知识库的 Assistant。"""
    assistant = Assistants.create(
        model='qwen-plus',  # 模型列表：https://help.aliyun.com/zh/model-studio/getting-started/models
        name='智能手机选购助手',
        description='一个帮助用户选择手机的智能助手。',
        instructions='你是一个手机选购向导，你的任务是帮助用户选择满意的手机。使用提供的知识库来回答用户的问题。以下信息可能对你有帮助：${documents}。',
        tools=[
            {
                "type": "rag",  # 指定使用RAG（检索增强生成）模式
                "prompt_ra": {
                    "pipeline_id": [index_id],  # 指定使用的知识库ID，可以在第六步：创建知识库索引的返回值中获取，
                                                # 也可以在控制台获取：https://bailian.console.aliyun.com/#/knowledge-base
                    "multiknowledge_rerank_top_n": 10,  # 多知识源重排序时返回的top N结果数
                    "rerank_top_n": 5,  # 最终重排序后返回的top N结果数
                    "parameters": {
                        "type": "object",
                        "properties": {
                            "query_word": {
                                "type": "str",
                                "value": "${documents}"  # 使用动态占位符，将被实际查询内容替换
                            }
                        }
                    }
                }
            },
        ]
    )
    return assistant.id
```

### 创建对话管理 <span id="940bfbdcdbe78" />

#### 第九步：提交消息到 Assistant <span id="1767c3a0e4pkt" />

在创建完 Assistant 之后，您可以通过提交用户消息并获取 Assistant 的回复。以下是向 Assistant 发送消息的代码示例：

```python
from dashscope import Messages, Runs

def send_message(thread, assistant, message):
    """向 Assistant 发送消息并获取回复。"""
    print(f"用户: {message}")

    # 创建用户消息
    message = Messages.create(thread_id=thread.id, content=message)

    # 创建一个运行任务
    run = Runs.create(thread_id=thread.id, assistant_id=assistant.id)

    # 等待运行任务完成
    Runs.wait(run.id, thread_id=thread.id)

    # 获取消息列表
    msgs = Messages.list(thread.id)

    # 输出 Assistant 的回复
    assistant_reply = msgs['data'][0]['content'][0]['text']['value']
    print(f"助手: {assistant_reply}")
```

#### 第十步：创建对话线程并进行交互 <span id="207f34834dtxc" />

Assistant 通过对话线程与用户进行交互。您可以为每个对话创建一个新的线程，发送用户输入并接收回复。以下是管理对话线程并与 Assistant 交互的代码：

```python
from dashscope import Threads

def interact_with_assistant(assistant):
    """与 Assistant 进行对话互动。"""
    print("\n步骤3：与 Assistant 交互")

    # 创建一个新的对话线程
    thread = Threads.create()

    while True:
        user_input = input("\n请输入您的问题（输入 'quit' 退出）：")
        if user_input.lower() == 'quit':
            break

        try:
            # 发送消息并获取回复
            send_message(thread, assistant, user_input)
        except Exception as e:
            print(f"发送消息失败：{str(e)}。请稍后再试。")
```

## 常见问题 <span id="ee5174ef9bzrq" />

1. 是否有可视化的方式创建知识库？是否可以挂载百炼控制台创建的知识库？

   您可以使用百炼控制台[创建和使用知识库](/zh/model-studio/rag-knowledge-base)。无论是控制台还是 API，两种方式创建的知识库都可以用于构建 Assistant API 的 RAG 功能。
2. Assistant API 支持调用多个工具吗？如果 Assistant 同时配置了 RAG 和 Function Call 工具，它们之间是否会产生冲突？

   Assistant API 支持调用多个工具，且Assistant 会决定是否调用某个工具。如果您为 Assistant 配置了多种工具，Assistant 会根据用户输入决定是否调用一种或多种工具。
3. Assistant API 的 RAG 功能支持设置拼装策略吗？

   目前，Assistant API RAG 仅支持设置“多个知识库总共召回的片段数”及“单个知识库召回的片段数”，其他高级功能暂不支持。如需使用“拼装策略”等高级 RAG 功能，建议您使用百炼控制台创建阿里云百炼[智能体应用（Agent 1.0）](/zh/model-studio/single-agent-application)。

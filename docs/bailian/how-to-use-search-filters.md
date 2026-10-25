> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 知识库SearchFilters

> 如果您在请求 Retrieve 接口时返回的结果包含较多干扰信息，可以参考本文示例，在请求时传入SearchFilters设置个性化的检索条件，对语义检索结果进行过滤，以排除与查询Query无关的信息（该方法尤其适合结构化数据）。

<meta property="og:title" content="知识库SearchFilters" />

> 指知识库的[Retrieve](/zh/model-studio/api-bailian-2023-12-29-retrieve)接口。

## 效果对比 <span id="ecd62219e7tve" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "49.63%" }} />

    <col style={{ width: "50.37%" }} />
  </colgroup>

  <thead>
    <tr>
      <th style={{ verticalAlign: "top" }}>
        <strong>请求Retrieve接口（未传入SearchFilters）</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>请求Retrieve接口（传入SearchFilters）</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        Retrieve请求体：

        ```json
        {
          "indexId": "o73yjlxxxx",
          "query": "公司中姓名为张三的员工"
        }
        ```
      </td>

      <td style={{ verticalAlign: "top" }}>
        Retrieve请求体：

        ```json
        {
          "indexId": "o73yjlxxxx",
          "query": "公司中姓名为张三的员工",
          "searchFilters": [
            {
              "姓名": "张三"
            }
          ]
        }
        ```
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        Retrieve请求返回：

        ```text
        object {1}
          nodes [3]
            0 {3}
              metadata {13}
              score: 0.509829580783844
              text: 姓名:张三 年龄:25 岗位:技术员 性别:男
            1 {3}
              metadata {12}
              score: 0.24424360692501068
              text: 姓名:李四 年龄:31 岗位:销售 性别:女
            2 {3}
              metadata {12}
              score: 0.21872329711914062
              text: 姓名:王五 年龄:36 岗位:经理 性别:男
        ```

        传统语义检索返回了一些与查询Query（张三）不太相关的文本切片。
      </td>

      <td style={{ verticalAlign: "top" }}>
        Retrieve接口返回：

        ```text
        ▼ object {1}
          ▼ nodes [1]
            ▼ 0 {3}
              ► metadata {13}
              score : 0.509623825550793
              text : 姓名:张三 年龄:25 岗位:技术员 性别:男
        ```

        通过设置检索条件（SearchFilters），过滤语义检索结果中与查询Query（张三）无关的文本切片。
      </td>
    </tr>
  </tbody>
</table>

## 语法说明 <span id="0b34812d3ds3e" />

SearchFilters可以包含一个或多个子分组（如下方示例包含了两个子分组）。每个子分组由一组或多组Key-Value键值对（检索字段：字段值）组成，对通过用户提示词找到的文本切片进行进一步过滤。数组中的子分组之间采用 <strong>AND</strong> 语义；同一子分组内的多个检索字段默认也采用 <strong>AND</strong> 语义，可通过该子分组的 `_operator` 设置为 <strong>OR</strong>。关于如何使用子分组，请参见[子分组查询示例](/zh/model-studio/how-to-use-search-filters)。

```json
{
  "searchFilters": [
    {
      "姓名": "张三",
      "性别": "男"
    },
    {
      "岗位": "技术员"
    }
  ]
}
```

根据调用方式选择 SearchFilters 语法：

| 调用方式                            | 检索地址                                                                                         | 同一字段的多条件 OR                      |
| ------------------------------- | -------------------------------------------------------------------------------------------- | -------------------------------- |
| OpenAPI Retrieve 及其 SDK         | `POST https://bailian.cn-beijing.aliyuncs.com/{WorkspaceId}/index/retrieve`                  | 不支持嵌套 `_conditions`，仅支持平铺 Map    |
| RAG API                         | `POST https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/indices/rag/index/retrieve` | 支持 `_operator` 和嵌套 `_conditions` |
| Spring AI Alibaba DashScope SDK | `/api/v1/indices/pipeline/{pipeline_id}/retrieve`                                            | 支持 `_operator` 和嵌套 `_conditions` |

<strong>多字段 OR 查询（OpenAPI Retrieve）：</strong>在同一子分组中，将 `_operator` 与检索字段并列，并设为 `OR`（也支持小写 `or`），即可匹配任一字段条件。省略 `_operator` 或设为 `AND` 时，需同时满足该子分组的所有字段条件。

例如，以下条件筛选文档名称（`doc_name`）为“长上下文Qwen-Long”或切片标题（`title`）为“返回结果”的切片。请将字段和值替换为知识库中实际可检索的字段和值。

```json
{
  "searchFilters": [
    {
      "_operator": "OR",
      "doc_name": "长上下文Qwen-Long",
      "title": "返回结果"
    }
  ]
}
```

使用下方完整代码示例中的 Python 客户端时，可将 `retrieve_request.search_filters` 替换为：

```python
retrieve_request.search_filters = [
    {
        "_operator": "OR",
        "doc_name": "长上下文Qwen-Long",
        "title": "返回结果"
    }
]
```

OpenAPI SDK 的 `SearchFilters` 使用字符串值 Map，不支持嵌套 `_conditions`。传入嵌套结构时，请求可能仍返回成功响应，但不会按嵌套条件过滤，结果可能未经过预期筛选。平铺 Map 中的字段名不能重复，因此这种调用方式不能表达同一字段的多个独立条件。

如果还需叠加其他条件，将其放在数组中的另一个子分组中。例如，在上述 OR 子分组后追加 `{"doc_name": "长上下文Qwen-Long"}`，整体条件为“（文档名称为长上下文Qwen-Long OR 切片标题为返回结果）AND 文档名称为长上下文Qwen-Long”。`_operator` 只改变其所在子分组内字段的关系，不改变数组中子分组之间的 AND 关系。

<strong>同一字段的多条件 OR 查询（RAG API 或 Spring AI Alibaba）：</strong>通过 [RAG API](/zh/model-studio/rag-api-overview) 的 JSON 请求体或 Spring AI Alibaba DashScope SDK 调用检索接口时，可以将多个条件放入 `_conditions` 数组，并通过 `_operator` 指定这些条件之间的关系。RAG API 的完整地址为：

```text
POST https://{workspace_id}.cn-beijing.maas.aliyuncs.com/api/v1/indices/rag/index/retrieve
```

Spring AI Alibaba DashScope SDK 使用以下路径：

```text
/api/v1/indices/pipeline/{pipeline_id}/retrieve
```

SAA 应用也可以通过 `RestClient` 或 `WebClient` 直接调用上述 RAG API 地址，不经过 SDK 的 Pipeline 检索路径。两种方式的选择和 Java 集成示例见[通过 Spring AI Alibaba 检索知识库](/zh/model-studio/spring-ai-alibaba-integrate-knowledge-base#saa-retrieval-methods)。

例如，以下条件对应“部门等于 `Corporate`，或部门名称包含 `Research`，或文档类型包含 `Policy`”。`_conditions` 数组允许多个对象使用相同字段名，因此可以组合等值和模糊查询。`department` 和 `document_type` 是示例知识库的自定义字段名，请替换为实际 Schema 中的字段名：

```json
{
  "index_id": "your-index-id",
  "query": "查询制度文档",
  "search_filters": [
    {
      "_operator": "OR",
      "_conditions": [
        { "department": "Corporate" },
        { "department": { "like": "%Research%" } },
        { "document_type": { "like": "%Policy%" } }
      ]
    }
  ]
}
```

使用 Spring AI Alibaba 1.1.2.3 时，在[通过 Spring AI Alibaba 检索知识库](/zh/model-studio/spring-ai-alibaba-integrate-knowledge-base)的 `DashScopeDocumentRetrieverOptions` 中配置相同条件：

```java expandable
List<Map<String, Object>> searchFilters = List.of(Map.of(
    "_operator", "OR",
    "_conditions", List.of(
        Map.of("department", "Corporate"),
        Map.of("department", Map.of("like", "%Research%")),
        Map.of("document_type", Map.of("like", "%Policy%"))
    )
));

DashScopeDocumentRetrieverOptions options = DashScopeDocumentRetrieverOptions.builder()
    .indexName("your-knowledge-base-name")
    .searchFilters(searchFilters)
    .build();
```

`_operator` 只控制其所在 `_conditions` 数组中的条件关系。`search_filters` 中不同子分组之间仍采用 AND 关系。

子分组内的检索字段支持<strong>单值查询</strong>、<strong>多值查询</strong>、<strong>范围查询</strong>、<strong>模糊查询</strong>和<strong>标签（Tag）查询</strong>。

- <strong>单值查询：</strong>字段类型只支持数值（long或double）、字符串（string）。关于如何使用单值查询，请参见[单值查询示例](/zh/model-studio/how-to-use-search-filters)。
- <strong>多值查询：</strong>只支持由纯数值（long或double）或纯字符串（string）组成的数组。关于如何使用多值查询，请参见[多值查询示例](/zh/model-studio/how-to-use-search-filters)。
- <strong>范围查询：</strong>支持<strong>等值查询</strong>和<strong>区间查询</strong>。关于如何使用范围查询，请参见[范围查询示例](/zh/model-studio/how-to-use-search-filters)。

  - <strong>等值查询</strong>：支持 `eq`（等于）、`neq`（不等于） 属性，字段类型支持数值（long或double）和字符串（string）。一个字段不可配置多个值（不区分大小写）。
  - <strong>区间查询</strong>：支持 `gt`（大于）、`gte`（大于等于）、`lt`（小于）、`lte`（小于等于） 属性，字段类型只支持数值（long, double）。
- <strong>模糊查询：</strong>字段类型只支持字符串（string）。支持`like` 属性。关于如何使用模糊查询，请参见[模糊查询示例](/zh/model-studio/how-to-use-search-filters#6a1b236cc0wuo)。
- <strong>标签（Tag）查询：</strong>只支持文档搜索、音视频搜索类知识库。关于如何使用标签查询，请参见[标签（Tag）查询示例](/zh/model-studio/how-to-use-search-filters)。

## 前置步骤 <span id="a3e980e26bvlr" />

- [子账号](/zh/model-studio/application-permission-management-overview)（主账号不需要）需[获取AliyunBailianDataFullAccess策略](/zh/model-studio/grant-data-access-permission-to-ram-user)，并[加入一个业务空间](/zh/model-studio/grant-the-business-space-permission-to-ram-users)，然后才能使用阿里云API操作知识库。

  > 子账号只能操作自己已加入的业务空间中的知识库；主账号可操作所有业务空间下的知识库。
- [获取业务空间ID](/zh/model-studio/use-workspace)。
- 安装了[阿里云百炼SDK](https://api.aliyun.com/api-tools/sdk/bailian?version=2023-12-29\&language=java-tea\&tab=primer-doc)并配置好了环境。
- [获取并配置AccessKey和AccessKey Secret到环境变量](https://help.aliyun.com/zh/sdk/developer-reference/configure-the-alibaba-cloud-accesskey-environment-variable-on-linux-macos-and-windows-systems)。
- 准备员工信息表[员工信息表.xlsx](https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250224/ghwuqu/%E5%91%98%E5%B7%A5%E4%BF%A1%E6%81%AF%E8%A1%A8.xlsx)（包含三条记录），用于创建知识库（下文示例中会用到）。[创建和使用知识库说明](/zh/model-studio/rag-knowledge-base)

  - <strong>知识库配置：</strong>

    - <strong>知识库类型：</strong>数据查询
    - <strong>数据来源：</strong>上传数据表
    - <strong>数据表字段结构：</strong>姓名、性别、岗位（string类型）和年龄（double类型）
    - <strong>索引设置：</strong>所有字段均参与检索与模型回复

    完成配置后，<strong>员工信息知识库</strong>中的<strong>员工表</strong>显示<strong>导入成功</strong>状态，表中包含张三、李四、王五三条示例员工数据。

## 完整代码示例 <span id="c066fe67a242d" />

展开下方折叠面板，查看本文提供的Python和Java完整示例代码（其他语言需参考示例自行实现）。

> 在调用示例代码之前，需[获取AccessKey和AccessKey Secret并配置到环境变量](https://help.aliyun.com/zh/sdk/developer-reference/configure-the-alibaba-cloud-accesskey-environment-variable-on-linux-macos-and-windows-systems)。

<Tabs>
  <Tab title="Python">
    <Accordion title="SearchFiltersFullExample.py">
      ```python expandable
      # 示例代码仅供参考，请勿在生产环境中直接使用
      import json
      import os
      import sys
      from typing import List
      from alibabacloud_bailian20231229 import models as bailian_20231229_models
      from alibabacloud_bailian20231229.client import Client as bailian20231229Client
      from alibabacloud_tea_openapi import models as open_api_models
      from alibabacloud_tea_util.client import Client as UtilClient
      class SearchFiltersFullExample:
          class QueryObject:
              def __init__(self, prefix):
                  self.like = prefix
              def to_dict(self):
                  return {
                      "like": self.like
                  }
          class Range:
              def __init__(self, gte, lte):
                  self.gte = gte
                  self.lte = lte
              def to_dict(self):
                  return {
                      "gte": self.gte,
                      "lte": self.lte
                  }
          def __init__(self):
              pass
          @staticmethod
          def create_client() -> bailian20231229Client:
              """
              使用AK&SK初始化账号Client
              @return: Client
              @throws Exception
              """
              # 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              # 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378659.html。
              config = open_api_models.Config(
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。,
                  access_key_id=os.environ['ALIBABA_CLOUD_ACCESS_KEY_ID'],
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。,
                  access_key_secret=os.environ['ALIBABA_CLOUD_ACCESS_KEY_SECRET']
              )
              # Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = f'bailian.cn-beijing.aliyuncs.com'
              return bailian20231229Client(config)
          @staticmethod
          def sub_group_query() -> None:
              try:
                  client = SearchFiltersFullExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '公司中叫张三的员工'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 分组1查询属性为姓名，值为张三。分组2查询属性为性别，值为女。可替换为实际需要查询的属性和值。
                  retrieve_request.search_filters = [
                      {"姓名": "张三"},
                      {"性别": "女"}
                  ]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
          @staticmethod
          def single_query() -> None:
              try:
                  client = SearchFiltersFullExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '公司中叫张三的员工'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 单值查询属性为姓名，值为张三。可替换为实际需要查询的属性和值。
                  retrieve_request.search_filters = [
                      {"姓名": "张三"}
                  ]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
          @staticmethod
          def multi_query() -> None:
              try:
                  client = SearchFiltersFullExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '公司中所有员工'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 创建一个列表，用于存放多值。
                  names = ["张三", "李四"]
                  # 多值查询属性为姓名，对应上方names列表。值为上方指定的多值“张三”和“李四”。可替换为实际需要查询的属性和值。
                  retrieve_request.search_filters = [{"姓名": json.dumps(names)}]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
          @staticmethod
          def range_query() -> None:
              try:
                  client = SearchFiltersFullExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '公司中所有员工'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 查询属性为年龄，值为范围。可替换为实际需要查询的属性和值。
                  age_range = SearchFiltersFullExample.Range(20, 27)
                  retrieve_request.search_filters = [
                      {"岗位": "技术员"},  # 分组1: 岗位筛选条件
                      {"年龄": json.dumps(age_range.to_dict())}  # 分组2: 年龄范围筛选条件
                  ]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
          @staticmethod
          def wildcard_query() -> None:
              try:
                  client = SearchFiltersFullExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '公司中的男性员工'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 模糊查询属性为岗位，值为技 % 员，此处 % 代表匹配任意字符（包括零个字符）。可替换为实际值。
                  position = SearchFiltersFullExample.QueryObject('技%员')
                  retrieve_request.search_filters = [
                      {"姓名": "张三"},  # 姓名筛选条件。可替换为实际要查询的属性和值。
                      {"岗位": json.dumps(position.to_dict())}  # 岗位（模糊查询）筛选条件。可替换为实际要查询的属性和值。
                  ]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
          @staticmethod
          def tag_query() -> None:
              try:
                  client = SearchFiltersFullExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '请提供一些候选人'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 创建一个列表，用于存放标签。多个标签之间是或（OR）的关系，不是与（AND）的关系。
                  tags = ["A大学", "学生会主席"]
                  retrieve_request.search_filters = [
                      {"tags": json.dumps(tags)}
                  ]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
          @staticmethod
          def tag_query2() -> None:
              try:
                  client = SearchFiltersFullExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '请提供一些候选人'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 创建两个列表，用于存放标签。
                  tag1 = ["A大学"]
                  tag2 = ["体育特长生"]
                  retrieve_request.search_filters = [
                      {"tags": json.dumps(tag1)},
                      {"tags": json.dumps(tag2)}
                  ]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
          @staticmethod
          def main(args: List[str]) -> None:
              SearchFiltersFullExample.sub_group_query()
              SearchFiltersFullExample.single_query()
              SearchFiltersFullExample.multi_query()
              SearchFiltersFullExample.range_query()
              SearchFiltersFullExample.wildcard_query()
              SearchFiltersFullExample.tag_query()
              SearchFiltersFullExample.tag_query2()
      if __name__ == '__main__':
          SearchFiltersFullExample.main(sys.argv[1:])
      ```
    </Accordion>
  </Tab>

  <Tab title="Java">
    <Accordion title="SearchFiltersFullExample.java">
      ```java expandable
      // 示例代码仅供参考，请勿在生产环境中直接使用
      import com.aliyun.bailian20231229.models.RetrieveRequest;
      import com.aliyun.bailian20231229.models.RetrieveResponse;
      import com.google.gson.Gson;
      import com.google.gson.JsonArray;
      import java.util.ArrayList;
      import java.util.HashMap;
      import java.util.List;
      import java.util.Map;
      public class SearchFiltersFullExample {
          /**
           * <b>description</b> :
           * <p>使用AK&SK初始化账号Client</p>
           *
           * @return Client
           * @throws Exception
           */
          public static com.aliyun.bailian20231229.Client createClient() throws Exception {
              // 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              // 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378657.html。
              com.aliyun.teaopenapi.models.Config config = new com.aliyun.teaopenapi.models.Config()
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。
                      .setAccessKeyId(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_ID"))
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
                      .setAccessKeySecret(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_SECRET"));
              // Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = "bailian.cn-beijing.aliyuncs.com";
              return new com.aliyun.bailian20231229.Client(config);
          }
          public static void subgroupQuery() {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("公司中叫张三的员工");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  Map<String, String> map1 = new HashMap<>();
                  // 分组1查询属性为姓名，值为张三。可替换为实际需要查询的属性和值。
                  map1.put("姓名", "张三");
                  Map<String, String> map2 = new HashMap<>();
                  // 分组2查询属性为性别，值为女。可替换为实际需要查询的属性和值。
                  map2.put("性别", "女");
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map1);
                  searchFilters.add(map2);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
          public static void singleQuery() {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("公司中叫张三的员工");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  Map<String, String> map = new HashMap<>();
                  // 单值查询属性为姓名，值为张三。可替换为实际需要查询的属性和值。
                  map.put("姓名", "张三");
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
          public static void multiQuery() {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("公司中所有员工");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  // 创建一个JsonArray，用于存放多值
                  JsonArray array = new JsonArray();
                  array.add("张三");
                  array.add("李四");
                  Map<String, String> map = new HashMap<>();
                  // 多值查询属性为姓名，值为上方指定的多值“张三”和“李四”。可替换为实际需要查询的属性和值。
                  map.put("姓名", array.toString());
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
          public static void rangeQuery() {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("公司中所有员工");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  Map<String, String> map1 = new HashMap<>();
                  // 分组1查询属性为性别，值为男。可替换为实际需要查询的属性和值。
                  map1.put("性别", "男");
                  // 分组2查询属性为年龄，值为范围。可替换为实际需要查询的属性和值。
                  Map<String, String> map2 = new HashMap<>();
                  Range range = new Range(20, 27);
                  map2.put("年龄", new Gson().toJson(range));
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map1);
                  searchFilters.add(map2);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
          public static void wildcardQuery() {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("公司中的男性员工");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  // 分组1查询属性为姓名，值为张三。可替换为实际需要查询的属性和值。
                  Map<String, String> map1 = new HashMap<>();
                  map1.put("姓名", "张三");
                  // 分组2查询属性为岗位，值为技%员，此处%代表匹配任意字符（包括零个字符）。可替换为实际需要查询的属性和值。
                  Map<String, String> map2 = new HashMap<>();
                  QueryObject queryObject = new QueryObject("技%员");
                  map2.put("岗位", new Gson().toJson(queryObject));
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map1);
                  searchFilters.add(map2);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
          public static void tagQuery() {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("请提供一些候选人");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  // 创建一个JsonArray，用于存放标签。多个标签之间是或（OR）的关系，不是与（AND）的关系。
                  JsonArray array = new JsonArray();
                  array.add("A大学");
                  array.add("学生会主席");
                  Map<String, String> map = new HashMap<>();
                  map.put("tags", array.toString());
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
          public static void tagQuery2() {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("请提供一些候选人");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  // 创建一个JsonArray，用于存放标签。
                  JsonArray tag1 = new JsonArray();
                  tag1.add("A大学");
                  JsonArray tag2 = new JsonArray();
                  tag2.add("体育特长生");
                  Map<String, String> map1 = new HashMap<>();
                  map1.put("tags", tag1.toString());
                  Map<String, String> map2 = new HashMap<>();
                  map2.put("tags", tag2.toString());
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map1);
                  searchFilters.add(map2);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
          public static void main(String[] args) {
              subgroupQuery();
              singleQuery();
              multiQuery();
              rangeQuery();
              wildcardQuery();
              tagQuery();
              tagQuery2();
          }
          static class Range {
              double gte;
              double lte;
              public Range(double gte, double lte) {
                  this.gte = gte;
                  this.lte = lte;
              }
          }
          static class QueryObject {
              String like;
              public QueryObject(String prefix) {
                  this.like = prefix;
              }
              public String getLike() {
                  return like;
              }
              public void setLike(String like) {
                  this.like = like;
              }
          }
      }
      ```
    </Accordion>
  </Tab>
</Tabs>

## 快速开始 <span id="638268e59b2m0" />

以下介绍如何使用SearchFilters进行查询。

### 子分组查询示例 <span id="38cb49cb0a02z" />

子分组用于过滤知识库的召回结果，仅返回满足特定条件的文本切片。可将多个子分组（条件）添加到SearchFilters中。子分组之间默认采用 <strong>AND</strong> 语义，且不可更改。

<strong>示例：</strong>检索员工信息知识库，并筛选出`姓名`为<strong>张三</strong>且`性别`为<strong>女</strong>的记录（该知识库中不存在这样的记录）。

```json
{
  "searchFilters": [
    {
      "姓名": "张三"
    },
    {
      "性别": "女"
    }
  ]
}
```

> 展开下方折叠面板查看示例代码。

<Tabs>
  <Tab title="Python">
    <Accordion title="SubGroupQueryExample.py">
      ```python expandable
      # 示例代码仅供参考，请勿在生产环境中直接使用
      import os
      import sys
      from typing import List
      from alibabacloud_bailian20231229 import models as bailian_20231229_models
      from alibabacloud_bailian20231229.client import Client as bailian20231229Client
      from alibabacloud_tea_openapi import models as open_api_models
      from alibabacloud_tea_util.client import Client as UtilClient
      class SubGroupQueryExample:
          def __init__(self):
              pass
          @staticmethod
          def create_client() -> bailian20231229Client:
              """
              使用AK&SK初始化账号Client
              @return: Client
              @throws Exception
              """
              # 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              # 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378659.html。
              config = open_api_models.Config(
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。,
                  access_key_id=os.environ['ALIBABA_CLOUD_ACCESS_KEY_ID'],
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。,
                  access_key_secret=os.environ['ALIBABA_CLOUD_ACCESS_KEY_SECRET']
              )
              # Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = f'bailian.cn-beijing.aliyuncs.com'
              return bailian20231229Client(config)
          @staticmethod
          def main(
                  args: List[str],
          ) -> None:
              try:
                  client = SubGroupQueryExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '公司中叫张三的员工'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 分组1查询属性为姓名，值为张三。分组2查询属性为性别，值为女。可替换为实际需要查询的属性和值。
                  retrieve_request.search_filters = [
                      {"姓名": "张三"},
                      {"性别": "女"}
                  ]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
      if __name__ == '__main__':
          SubGroupQueryExample.main(sys.argv[1:])
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "indexId": "27ubwxxxxx",
        "WorkspaceId":"llm-4u5xpd1xdjxxxxxx",
        "query": "公司中叫张三的员工",
        "searchFilters": [
          {
            "姓名": "张三"
          },
          {
            "性别": "女"
          }
        ]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json
      {
        "code": "Success",
        "data": {
          "nodes": []
        },
        "message": "success",
        "requestId": "5BA30772-xxxx-560C-B1F7-C1DA737A9D80",
        "status": "200",
        "success": true
      }
      ```
    </Accordion>
  </Tab>

  <Tab title="Java">
    <Accordion title="SubGroupQueryExample.java">
      ```java expandable
      // 示例代码仅供参考，请勿在生产环境中直接使用
      import com.aliyun.bailian20231229.models.RetrieveRequest;
      import com.aliyun.bailian20231229.models.RetrieveResponse;
      import com.google.gson.Gson;
      import java.util.ArrayList;
      import java.util.HashMap;
      import java.util.List;
      import java.util.Map;
      public class SubGroupQueryExample {
          /**
           * <b>description</b> :
           * <p>使用AK&SK初始化账号Client</p>
           * @return Client
           *
           * @throws Exception
           */
          public static com.aliyun.bailian20231229.Client createClient() throws Exception {
              // 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              // 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378657.html。
              com.aliyun.teaopenapi.models.Config config = new com.aliyun.teaopenapi.models.Config()
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。
                      .setAccessKeyId(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_ID"))
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
                      .setAccessKeySecret(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_SECRET"));
              // Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = "bailian.cn-beijing.aliyuncs.com";
              return new com.aliyun.bailian20231229.Client(config);
          }
          public static void main(String[] args) {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("公司中叫张三的员工");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  Map<String, String> map1 = new HashMap<>();
                  // 分组1查询属性为姓名，值为张三。可替换为实际需要查询的属性和值。
                  map1.put("姓名","张三");
                  Map<String, String> map2 = new HashMap<>();
                  // 分组2查询属性为性别，值为女。可替换为实际需要查询的属性和值。
                  map2.put("性别","女");
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map1);
                  searchFilters.add(map2);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
      }
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "indexId": "27ubwxxxxx",
        "WorkspaceId":"llm-4u5xpd1xdjxxxxxx",
        "query": "公司中叫张三的员工",
        "searchFilters": [
          {
            "姓名": "张三"
          },
          {
            "性别": "女"
          }
        ]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json
      {
        "code": "Success",
        "data": {
          "nodes": []
        },
        "message": "success",
        "requestId": "5BA30772-xxxx-560C-B1F7-C1DA737A9D80",
        "status": "200",
        "success": true
      }
      ```
    </Accordion>
  </Tab>
</Tabs>

### 单值查询示例 <span id="2fc0d80da7k2f" />

在单值查询时，需要为检索字段传入唯一的值。

<strong>示例：</strong>检索员工信息知识库，并筛选出`姓名`为<strong>张三</strong>的记录。

```json
{
  "searchFilters": [
    {
      "姓名": "张三"
    }
  ]
}
```

> 展开下方折叠面板查看示例代码。

<Tabs>
  <Tab title="Python">
    <Accordion title="SingleQueryExample.py">
      ```python expandable
      # 示例代码仅供参考，请勿在生产环境中直接使用
      import os
      import sys
      from typing import List
      from alibabacloud_bailian20231229 import models as bailian_20231229_models
      from alibabacloud_bailian20231229.client import Client as bailian20231229Client
      from alibabacloud_tea_openapi import models as open_api_models
      from alibabacloud_tea_util.client import Client as UtilClient
      class SingleQueryExample:
          def __init__(self):
              pass
          @staticmethod
          def create_client() -> bailian20231229Client:
              """
              使用AK&SK初始化账号Client
              @return: Client
              @throws Exception
              """
              # 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              # 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378659.html。
              config = open_api_models.Config(
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。,
                  access_key_id=os.environ['ALIBABA_CLOUD_ACCESS_KEY_ID'],
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。,
                  access_key_secret=os.environ['ALIBABA_CLOUD_ACCESS_KEY_SECRET']
              )
              # Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = f'bailian.cn-beijing.aliyuncs.com'
              return bailian20231229Client(config)
          @staticmethod
          def main(
                  args: List[str],
          ) -> None:
              client = SingleQueryExample.create_client()
              # 创建retrieve_request对象。
              retrieve_request = bailian_20231229_models.RetrieveRequest()
              # 必填，可传入用户实际输入的提示词。
              retrieve_request.query = '公司中叫张三的员工'
              # 必填，请传入实际的知识库ID。
              retrieve_request.index_id = '请传入实际的知识库ID'
              # 单值查询属性为姓名，值为张三。可替换为实际需要查询的属性和值。
              retrieve_request.search_filters = [
                  {"姓名": "张三"}
              ]
              try:
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
      if __name__ == '__main__':
          SingleQueryExample.main(sys.argv[1:])
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "indexId": "27ubwxxxxx",
        "WorkspaceId":"llm-4u5xpd1xdjxxxxxx",
        "query": "公司中叫张三的员工",
        "searchFilters": [
          {
            "姓名": "张三"
          }
        ]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "code": "Success",
        "data": {
          "nodes": [
            {
              "metadata": {
                "_rc_v_score": 0.32581159472465515,
                "_q_score": 1,
                "source": "0",
                "_score": 0.4556944966316223,
                "doc_id": "table_xxxx75507aab4bd9a24c18d098b2e8ac_10285263_1",
                "性别": "男",
                "_rc_score": 0,
                "姓名": "张三",
                "doc_name": "员工表",
                "_id": "llm-xxxxpd1xdjqp8itj_27ubwxxxxx_table_200275507aab4bd9a24c18d098b2e8ac_10285263_1",
                "年龄": "25",
                "岗位": "技术员"
              },
              "score": 0.4556944966316223,
              "text": "姓名:张三 年龄:25 岗位:技术员 性别:男"
            }
          ]
        },
        "message": "success",
        "requestId": "2FA4113E-xxxx-59C1-BDB2-5B930D8C9B1C",
        "status": "200",
        "success": true
      }
      ```
    </Accordion>
  </Tab>

  <Tab title="Java">
    <Accordion title="SingleQueryExample.java">
      ```java expandable
      // 示例代码仅供参考，请勿在生产环境中直接使用
      import com.aliyun.bailian20231229.models.RetrieveRequest;
      import com.aliyun.bailian20231229.models.RetrieveResponse;
      import com.google.gson.Gson;
      import java.util.ArrayList;
      import java.util.HashMap;
      import java.util.List;
      import java.util.Map;
      public class SingleQueryExample {
          /**
           * <b>description</b> :
           * <p>使用AK&SK初始化账号Client</p>
           * @return Client
           *
           * @throws Exception
           */
          public static com.aliyun.bailian20231229.Client createClient() throws Exception {
              // 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              // 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378657.html。
              com.aliyun.teaopenapi.models.Config config = new com.aliyun.teaopenapi.models.Config()
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。
                      .setAccessKeyId(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_ID"))
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
                      .setAccessKeySecret(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_SECRET"));
              // Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = "bailian.cn-beijing.aliyuncs.com";
              return new com.aliyun.bailian20231229.Client(config);
          }
          public static void main(String[] args) {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("公司中叫张三的员工");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  Map<String, String> map = new HashMap<>();
                  // 单值查询属性为姓名，值为张三。可替换为实际需要查询的属性和值。
                  map.put("姓名","张三");
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
      }
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "indexId": "27ubwxxxxx",
        "WorkspaceId":"llm-4u5xpd1xdjxxxxxx",
        "query": "公司中叫张三的员工",
        "searchFilters": [
          {
            "姓名": "张三"
          }
        ]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "code": "Success",
        "data": {
          "nodes": [
            {
              "metadata": {
                "_rc_v_score": 0.32581159472465515,
                "_q_score": 1,
                "source": "0",
                "_score": 0.4556944966316223,
                "doc_id": "table_xxxx75507aab4bd9a24c18d098b2e8ac_10285263_1",
                "性别": "男",
                "_rc_score": 0,
                "姓名": "张三",
                "doc_name": "员工表",
                "_id": "llm-xxxxpd1xdjqp8itj_27ubwxxxxx_table_xxxx75507aab4bd9a24c18d098b2e8ac_10285263_1",
                "年龄": "25",
                "岗位": "技术员"
              },
              "score": 0.4556944966316223,
              "text": "姓名:张三 年龄:25 岗位:技术员 性别:男"
            }
          ]
        },
        "message": "success",
        "requestId": "2FA4113E-xxxx-59C1-BDB2-5B930D8C9B1C",
        "status": "200",
        "success": true
      }
      ```
    </Accordion>
  </Tab>
</Tabs>

### 多值查询示例 <span id="41c84d2dc9c0e" />

多值查询允许为检索字段传入多个值，效果类似于SQL中的IN操作符。

<strong>示例：</strong>检索员工信息知识库，并筛选出`姓名`为<strong>张三</strong>或<strong>李四</strong>的记录。

```json
{
  "searchFilters": [
    {
      "姓名": ["张三","李四"]
    }
  ]
}
```

> 展开下方折叠面板查看示例代码。

<Tabs>
  <Tab title="Python">
    <Accordion title="MultiQueryExample.py">
      ```python expandable
      # 示例代码仅供参考，请勿在生产环境中直接使用
      import json
      import os
      import sys
      from typing import List
      from alibabacloud_bailian20231229 import models as bailian_20231229_models
      from alibabacloud_bailian20231229.client import Client as bailian20231229Client
      from alibabacloud_tea_openapi import models as open_api_models
      from alibabacloud_tea_util.client import Client as UtilClient
      class MultiQueryExample:
          def __init__(self):
              pass
          @staticmethod
          def create_client() -> bailian20231229Client:
              """
              使用AK&SK初始化账号Client
              @return: Client
              @throws Exception
              """
              # 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              # 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378659.html。
              config = open_api_models.Config(
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。,
                  access_key_id=os.environ['ALIBABA_CLOUD_ACCESS_KEY_ID'],
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。,
                  access_key_secret=os.environ['ALIBABA_CLOUD_ACCESS_KEY_SECRET']
              )
              # Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = f'bailian.cn-beijing.aliyuncs.com'
              return bailian20231229Client(config)
          @staticmethod
          def main(
                  args: List[str],
          ) -> None:
              try:
                  client = MultiQueryExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '公司中所有员工'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 创建一个列表，用于存放多值。
                  names = ["张三", "李四"]
                  # 多值查询属性为姓名，对应上方names列表。值为上方指定的多值“张三”和“李四”。可替换为实际需要查询的属性和值。
                  retrieve_request.search_filters = [{"姓名": json.dumps(names)}]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
      if __name__ == '__main__':
          MultiQueryExample.main(sys.argv[1:])
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "indexId": "27ubwxxxxx",
        "WorkspaceId":"llm-4u5xpd1xdjxxxxxx",
        "query": "公司中所有员工",
        "searchFilters": [
          {
            "姓名": ["张三", "李四"]
          }
        ]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "code": "Success",
        "data": {
          "nodes": [
            {
              "metadata": {
                "_rc_v_score": 0.3016361088048254,
                "_q_score": 1,
                "source": "0",
                "_score": 0.3322954773902893,
                "doc_id": "table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "性别": "男",
                "_rc_score": 0,
                "姓名": "张三",
                "doc_name": "员工表",
                "_id": "llm-xxxxpd1xdjqp8itj_27ubwxxxxx_table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "年龄": "25.0",
                "岗位": "技术员"
              },
              "score": 0.3322954773902893,
              "text": "姓名:张三 年龄:25.0 岗位:技术员 性别:男"
            },
            {
              "metadata": {
                "_rc_v_score": 0.2531493306159973,
                "_q_score": 0.8392540654998252,
                "source": "0",
                "_score": 0.25632044672966003,
                "doc_id": "table_xxxxad1c331c424780a8023376a73fac_10285263_2",
                "性别": "女",
                "_rc_score": 0,
                "姓名": "李四",
                "doc_name": "员工表",
                "_id": "llm-xxxxpd1xdjqp8itj_27ubwxxxxx_table_xxxxad1c331c424780a8023376a73fac_10285263_2",
                "年龄": "31.0",
                "岗位": "销售"
              },
              "score": 0.25632044672966003,
              "text": "姓名:李四 年龄:31.0 岗位:销售 性别:女"
            }
          ]
        },
        "message": "success",
        "requestId": "1DFE5E9E-xxxx-5C37-8011-8FA2E2875309",
        "status": "200",
        "success": true
      }
      ```
    </Accordion>
  </Tab>

  <Tab title="Java">
    <Accordion title="MultiQueryExample.java">
      ```java expandable
      // 示例代码仅供参考，请勿在生产环境中直接使用
      import com.aliyun.bailian20231229.models.RetrieveRequest;
      import com.aliyun.bailian20231229.models.RetrieveResponse;
      import com.google.gson.Gson;
      import com.google.gson.JsonArray;
      import java.util.ArrayList;
      import java.util.HashMap;
      import java.util.List;
      import java.util.Map;
      public class MultiQueryExample {
          /**
           * <b>description</b> :
           * <p>使用AK&SK初始化账号Client</p>
           * @return Client
           *
           * @throws Exception
           */
          public static com.aliyun.bailian20231229.Client createClient() throws Exception {
              // 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              // 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378657.html。
              com.aliyun.teaopenapi.models.Config config = new com.aliyun.teaopenapi.models.Config()
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。
                      .setAccessKeyId(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_ID"))
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
                      .setAccessKeySecret(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_SECRET"));
              // Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = "bailian.cn-beijing.aliyuncs.com";
              return new com.aliyun.bailian20231229.Client(config);
          }
          public static void main(String[] args) {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("公司中所有员工");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  // 创建一个JsonArray，用于存放多值
                  JsonArray array = new JsonArray();
                  array.add("张三");
                  array.add("李四");
                  Map<String, String> map = new HashMap<>();
                  // 多值查询属性为姓名，值为上方指定的多值“张三”和“李四”。可替换为实际需要查询的属性和值。
                  map.put("姓名", array.toString());
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
      }
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "indexId": "27ubwxxxxx",
        "WorkspaceId":"llm-4u5xpd1xdjxxxxxx",
        "query": "公司中所有员工",
        "searchFilters": [
          {
            "姓名": ["张三", "李四"]
          }
        ]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "code": "Success",
        "data": {
          "nodes": [
            {
              "metadata": {
                "_rc_v_score": 0.3016361088048254,
                "_q_score": 1,
                "source": "0",
                "_score": 0.3322954773902893,
                "doc_id": "table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "性别": "男",
                "_rc_score": 0,
                "姓名": "张三",
                "doc_name": "员工表",
                "_id": "llm-xxxxpd1xdjqp8itj_27ubwxxxxx_table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "年龄": "25.0",
                "岗位": "技术员"
              },
              "score": 0.3322954773902893,
              "text": "姓名:张三 年龄:25.0 岗位:技术员 性别:男"
            },
            {
              "metadata": {
                "_rc_v_score": 0.2531493306159973,
                "_q_score": 0.8392540654998252,
                "source": "0",
                "_score": 0.25632044672966003,
                "doc_id": "table_xxxxad1c331c424780a8023376a73fac_10285263_2",
                "性别": "女",
                "_rc_score": 0,
                "姓名": "李四",
                "doc_name": "员工表",
                "_id": "llm-xxxxpd1xdjqp8itj_27ubwxxxxx_table_xxxxad1c331c424780a8023376a73fac_10285263_2",
                "年龄": "31.0",
                "岗位": "销售"
              },
              "score": 0.25632044672966003,
              "text": "姓名:李四 年龄:31.0 岗位:销售 性别:女"
            }
          ]
        },
        "message": "success",
        "requestId": "1DFE5E9E-xxxx-5C37-8011-8FA2E2875309",
        "status": "200",
        "success": true
      }
      ```
    </Accordion>
  </Tab>
</Tabs>

### 范围查询示例 <span id="b785c3bdc0pnc" />

通过范围查询，可找出检索字段（如年龄）值在指定范围内满足条件的所有记录。

<strong>示例：</strong>检索员工信息知识库，并筛选出`岗位`为<strong>技术员</strong>（单值查询），且`年龄`在<strong>20</strong>至<strong>25</strong>岁之间（范围查询）的记录。

```json
{
  "searchFilters": [
    {
      "岗位": "技术员"
    },
    {
      "年龄": {
        "gte": 20,
        "lte": 25
      }
    }
  ]
}
```

> 展开下方折叠面板查看示例代码。

<Tabs>
  <Tab title="Python">
    <Accordion title="RangeQueryExample.py">
      ```python expandable
      # 示例代码仅供参考，请勿在生产环境中直接使用
      import json
      import os
      import sys
      from typing import List
      from alibabacloud_bailian20231229 import models as bailian_20231229_models
      from alibabacloud_bailian20231229.client import Client as bailian20231229Client
      from alibabacloud_tea_openapi import models as open_api_models
      from alibabacloud_tea_util.client import Client as UtilClient
      class RangeQueryExample:
          class Range:
              def __init__(self, gte, lte):
                  self.gte = gte
                  self.lte = lte
              def to_dict(self):
                  return {
                      "gte": self.gte,
                      "lte": self.lte
                  }
          def __init__(self):
              pass
          @staticmethod
          def create_client() -> bailian20231229Client:
              """
              使用AK&SK初始化账号Client
              @return: Client
              @throws Exception
              """
              # 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              # 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378659.html。
              config = open_api_models.Config(
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。,
                  access_key_id=os.environ['ALIBABA_CLOUD_ACCESS_KEY_ID'],
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。,
                  access_key_secret=os.environ['ALIBABA_CLOUD_ACCESS_KEY_SECRET']
              )
              # Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = f'bailian.cn-beijing.aliyuncs.com'
              return bailian20231229Client(config)
          @staticmethod
          def main(
                  args: List[str],
          ) -> None:
              try:
                  client = RangeQueryExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '公司中所有员工'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 查询属性为年龄，值为范围。可替换为实际需要查询的属性和值。
                  age_range = RangeQueryExample.Range(20, 27)
                  retrieve_request.search_filters = [
                      {"岗位": "技术员"},  # 分组1: 岗位筛选条件
                      {"年龄": json.dumps(age_range.to_dict())}  # 分组2: 年龄范围筛选条件
                  ]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
      if __name__ == '__main__':
          RangeQueryExample.main(sys.argv[1:])
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "indexId": "27ubwxxxxx",
        "WorkspaceId":"llm-4u5xpd1xdjxxxxxx",
        "query": "公司中所有员工",
        "searchFilters": [
          {
            "性别": "男"
          },
          {
            "年龄": "{\"gte\":20.0,\"lte\":27.0}"
          }
        ]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "code": "Success",
        "data": {
          "nodes": [
            {
              "metadata": {
                "_rc_v_score": 0.3016361088048254,
                "_q_score": 1,
                "source": "0",
                "_score": 0.3322954773902893,
                "doc_id": "table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "性别": "男",
                "_rc_score": 0,
                "姓名": "张三",
                "doc_name": "员工表",
                "_id": "llm-xxxxpd1xdjqp8itj_27ubwxxxxx_table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "年龄": "25.0",
                "岗位": "技术员"
              },
              "score": 0.3322954773902893,
              "text": "姓名:张三 年龄:25.0 岗位:技术员 性别:男"
            }
          ]
        },
        "message": "success",
        "requestId": "AE0B5ABC-xxxx-54A1-9ED4-91865B859DF6",
        "status": "200",
        "success": true
      }
      ```
    </Accordion>
  </Tab>

  <Tab title="Java">
    <Accordion title="RangeQueryExample.java">
      ```java expandable
      // 示例代码仅供参考，请勿在生产环境中直接使用
      import com.aliyun.bailian20231229.models.RetrieveRequest;
      import com.aliyun.bailian20231229.models.RetrieveResponse;
      import com.google.gson.Gson;
      import java.util.ArrayList;
      import java.util.HashMap;
      import java.util.List;
      import java.util.Map;
      public class RangeQueryExample {
          /**
           * <b>description</b> :
           * <p>使用AK&SK初始化账号Client</p>
           * @return Client
           *
           * @throws Exception
           */
          public static com.aliyun.bailian20231229.Client createClient() throws Exception {
              // 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              // 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378657.html。
              com.aliyun.teaopenapi.models.Config config = new com.aliyun.teaopenapi.models.Config()
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。
                      .setAccessKeyId(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_ID"))
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
                      .setAccessKeySecret(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_SECRET"));
              // Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = "bailian.cn-beijing.aliyuncs.com";
              return new com.aliyun.bailian20231229.Client(config);
          }
          public static void main(String[] args) {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("公司中所有员工");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  Map<String, String> map1 = new HashMap<>();
                  // 分组1查询属性为性别，值为男。可替换为实际需要查询的属性和值。
                  map1.put("性别", "男");
                  // 分组2查询属性为年龄，值为范围。可替换为实际需要查询的属性和值。
                  Map<String, String> map2 = new HashMap<>();
                  Range range = new Range(20, 27);
                  map2.put("年龄", new Gson().toJson(range));
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map1);
                  searchFilters.add(map2);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
          static class Range {
              double gte;
              double lte;
              public Range(double gte, double lte) {
                  this.gte = gte;
                  this.lte = lte;
              }
          }
      }
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "indexId": "27ubwxxxxx",
        "WorkspaceId":"llm-4u5xpd1xdjxxxxxx",
        "query": "公司中所有员工",
        "searchFilters": [
          {
            "性别": "男"
          },
          {
            "年龄": "{\"gte\":20.0,\"lte\":27.0}"
          }
        ]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "code": "Success",
        "data": {
          "nodes": [
            {
              "metadata": {
                "_rc_v_score": 0.3016361088048254,
                "_q_score": 1,
                "source": "0",
                "_score": 0.3322954773902893,
                "doc_id": "table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "性别": "男",
                "_rc_score": 0,
                "姓名": "张三",
                "doc_name": "员工表",
                "_id": "llm-xxxxpd1xdjqp8itj_27ubwxxxxx_table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "年龄": "25.0",
                "岗位": "技术员"
              },
              "score": 0.3322954773902893,
              "text": "姓名:张三 年龄:25.0 岗位:技术员 性别:男"
            }
          ]
        },
        "message": "success",
        "requestId": "AE0B5ABC-xxxx-54A1-9ED4-91865B859DF6",
        "status": "200",
        "success": true
      }
      ```
    </Accordion>
  </Tab>
</Tabs>

### 模糊查询示例 <span id="6a1b236cc0wuo" />

模糊查询通过指定通配符查找包含特定字符序列的记录，效果类似于SQL中的LIKE操作符。

SearchFilters模糊查询支持以下通配符（和SQL语法一致）：

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>通配符</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>描述</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>%</p></td><td style={{ verticalAlign: "top" }}><p>替代 0 个或多个字符。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>\_</p></td><td style={{ verticalAlign: "top" }}><p>替代一个字符。</p></td></tr></tbody></table>

<strong>示例：</strong>检索员工信息知识库，并筛选出`姓名`为<strong>张三</strong>，`岗位`包含<strong>技</strong>和<strong>员</strong>两个字（可能是技术员，也可能是技术人员等）的记录。

```json
{
  "searchFilters": [
    {
      "姓名": "张三"
    },
    {
      "岗位": {
        "like": "技%员"
      }
    }
  ]
}
```

> 展开下方折叠面板查看示例代码。

<Tabs>
  <Tab title="Python">
    <Accordion title="WildcardQueryExample.py">
      ```python expandable
      # 示例代码仅供参考，请勿在生产环境中直接使用
      import json
      import os
      import sys
      from typing import List
      from alibabacloud_bailian20231229 import models as bailian_20231229_models
      from alibabacloud_bailian20231229.client import Client as bailian20231229Client
      from alibabacloud_tea_openapi import models as open_api_models
      from alibabacloud_tea_util.client import Client as UtilClient
      class WildcardQueryExample:
          class QueryObject:
              def __init__(self, prefix):
                  self.like = prefix
              def to_dict(self):
                  return {
                      "like": self.like
                  }
          def __init__(self):
              pass
          @staticmethod
          def create_client() -> bailian20231229Client:
              """
              使用AK&SK初始化账号Client
              @return: Client
              @throws Exception
              """
              # 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              # 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378659.html。
              config = open_api_models.Config(
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。,
                  access_key_id=os.environ['ALIBABA_CLOUD_ACCESS_KEY_ID'],
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。,
                  access_key_secret=os.environ['ALIBABA_CLOUD_ACCESS_KEY_SECRET']
              )
              # Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = f'bailian.cn-beijing.aliyuncs.com'
              return bailian20231229Client(config)
          @staticmethod
          def main(
                  args: List[str],
          ) -> None:
              try:
                  client = WildcardQueryExample.create_client()
                  # 创建retrieve_request对象。
                  retrieve_request = bailian_20231229_models.RetrieveRequest()
                  # 必填，可传入用户实际输入的提示词。
                  retrieve_request.query = '公司中的男性员工'
                  # 必填，请传入实际的知识库ID。
                  retrieve_request.index_id = '请传入实际的知识库ID'
                  # 模糊查询属性为岗位，值为技 % 员，此处 % 代表匹配任意字符（包括零个字符）。可替换为实际值。
                  position = WildcardQueryExample.QueryObject('技%员')
                  retrieve_request.search_filters = [
                      {"姓名": "张三"},  # 姓名筛选条件。可替换为实际要查询的属性和值。
                      {"岗位": json.dumps(position.to_dict())}  # 岗位（模糊查询）筛选条件。可替换为实际要查询的属性和值。
                  ]
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
      if __name__ == '__main__':
          WildcardQueryExample.main(sys.argv[1:])
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "indexId": "27ubwxxxxx",
        "WorkspaceId":"llm-4u5xpd1xdjxxxxxx",
        "query": "公司中的男性员工",
        "searchFilters": [
          {
            "姓名": "张三"
          },
          {
            "岗位": "{\"like\":\"技%员\"}"
          }
        ]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "code": "Success",
        "data": {
          "nodes": [
            {
              "metadata": {
                "_rc_v_score": 0.41137335930387275,
                "_q_score": 1,
                "source": "0",
                "_score": 0.46098726987838745,
                "doc_id": "table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "性别": "男",
                "_rc_score": 0,
                "姓名": "张三",
                "doc_name": "员工表",
                "_id": "llm-xxxxpd1xdjqp8itj_27ubwxxxxx_table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "年龄": "25.0",
                "岗位": "技术员"
              },
              "score": 0.46098726987838745,
              "text": "姓名:张三 年龄:25.0 岗位:技术员 性别:男"
            }
          ]
        },
        "message": "success",
        "requestId": "FA759FEC-xxxx-50B7-A64D-BE49A7DF56B8",
        "status": "200",
        "success": true
      }
      ```
    </Accordion>
  </Tab>

  <Tab title="Java">
    <Accordion title="WildcardQueryExample.java">
      ```java expandable
      // 示例代码仅供参考，请勿在生产环境中直接使用
      import com.aliyun.bailian20231229.models.RetrieveRequest;
      import com.aliyun.bailian20231229.models.RetrieveResponse;
      import com.google.gson.Gson;
      import java.util.ArrayList;
      import java.util.HashMap;
      import java.util.List;
      import java.util.Map;
      public class WildcardQueryExample {
          /**
           * <b>description</b> :
           * <p>使用AK&SK初始化账号Client</p>
           *
           * @return Client
           * @throws Exception
           */
          public static com.aliyun.bailian20231229.Client createClient() throws Exception {
              // 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              // 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378657.html。
              com.aliyun.teaopenapi.models.Config config = new com.aliyun.teaopenapi.models.Config()
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。
                      .setAccessKeyId(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_ID"))
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
                      .setAccessKeySecret(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_SECRET"));
              // Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = "bailian.cn-beijing.aliyuncs.com";
              return new com.aliyun.bailian20231229.Client(config);
          }
          public static void main(String[] args_) {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("公司中的男性员工");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  // 分组1查询属性为姓名，值为张三。可替换为实际需要查询的属性和值。
                  Map<String, String> map1 = new HashMap<>();
                  map1.put("姓名", "张三");
                  // 分组2查询属性为岗位，值为技%员，此处%代表匹配任意字符（包括零个字符）。可替换为实际需要查询的属性和值。
                  Map<String, String> map2 = new HashMap<>();
                  QueryObject queryObject = new QueryObject("技%员");
                  map2.put("岗位", new Gson().toJson(queryObject));
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map1);
                  searchFilters.add(map2);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
          static class QueryObject {
              String like;
              public QueryObject(String prefix) {
                  this.like = prefix;
              }
              public String getLike() {
                  return like;
              }
              public void setLike(String like) {
                  this.like = like;
              }
          }
      }
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "indexId": "27ubwxxxxx",
        "WorkspaceId":"llm-4u5xpd1xdjxxxxxx",
        "query": "公司中的男性员工",
        "searchFilters": [
          {
            "姓名": "张三"
          },
          {
            "岗位": "{\"like\":\"技%员\"}"
          }
        ]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "code": "Success",
        "data": {
          "nodes": [
            {
              "metadata": {
                "_rc_v_score": 0.41137335930387275,
                "_q_score": 1,
                "source": "0",
                "_score": 0.46098726987838745,
                "doc_id": "table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "性别": "男",
                "_rc_score": 0,
                "姓名": "张三",
                "doc_name": "员工表",
                "_id": "llm-xxxxpd1xdjqp8itj_27ubwxxxxx_table_xxxxad1c331c424780a8023376a73fac_10285263_1",
                "年龄": "25.0",
                "岗位": "技术员"
              },
              "score": 0.46098726987838745,
              "text": "姓名:张三 年龄:25.0 岗位:技术员 性别:男"
            }
          ]
        },
        "message": "success",
        "requestId": "FA759FEC-xxxx-50B7-A64D-BE49A7DF56B8",
        "status": "200",
        "success": true
      }
      ```
    </Accordion>
  </Tab>
</Tabs>

### 标签（Tag）查询示例 <span id="47edaa05ae8ed" />

检索文档搜索、音视频搜索类知识库时，可通过[标签](/zh/model-studio/rag-knowledge-base)筛选文件，提高检索效率与准确性。

<strong>示例：</strong>创建一个文档搜索类知识库，其中包含张三、李四和王五三人的信息。

以上文件分别添加了以下标签：

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>文件</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>标签</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>张三简历</p></td><td style={{ verticalAlign: "top" }}><p><code>A大学</code>、<code>体育特长生</code></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>李四简历</p></td><td style={{ verticalAlign: "top" }}><p><code>B大学</code></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>王五简历</p></td><td style={{ verticalAlign: "top" }}><p><code>B大学</code>、<code>学生会主席</code></p></td></tr></tbody></table>

例如使用SearchFilters查询人才知识库，要求返回文件标签含<strong>A大学</strong>或<strong>学生会主席</strong>的相关文本切片：

> 多个标签之间是逻辑或（OR）的关系，不是逻辑与（AND）的关系。[可通过子分组查询实现“逻辑与”](/zh/model-studio/how-to-use-search-filters)。

```json
{
  "searchFilters": [
    {
      "tags": ["A大学","学生会主席"]
    }
  ]
}
```

> 展开下方折叠面板查看示例代码。

<Tabs>
  <Tab title="Python">
    <Accordion title="TagQueryExample.py">
      ```python expandable
      # 示例代码仅供参考，请勿在生产环境中直接使用
      import json
      import os
      import sys
      from typing import List
      from alibabacloud_bailian20231229 import models as bailian_20231229_models
      from alibabacloud_bailian20231229.client import Client as bailian20231229Client
      from alibabacloud_tea_openapi import models as open_api_models
      from alibabacloud_tea_util.client import Client as UtilClient
      class TagQueryExample:
          def __init__(self):
              pass
          @staticmethod
          def create_client() -> bailian20231229Client:
              """
              使用AK&SK初始化账号Client
              @return: Client
              @throws Exception
              """
              # 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              # 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378659.html。
              config = open_api_models.Config(
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。,
                  access_key_id=os.environ['ALIBABA_CLOUD_ACCESS_KEY_ID'],
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。,
                  access_key_secret=os.environ['ALIBABA_CLOUD_ACCESS_KEY_SECRET']
              )
              # Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = f'bailian.cn-beijing.aliyuncs.com'
              return bailian20231229Client(config)
          @staticmethod
          def main(
                  args: List[str],
          ) -> None:
              client = TagQueryExample.create_client()
              # 创建retrieve_request对象。
              retrieve_request = bailian_20231229_models.RetrieveRequest()
              # 必填，可传入用户实际输入的提示词。
              retrieve_request.query = '请提供一些候选人'
              # 必填，请传入实际的知识库ID。
              retrieve_request.index_id = '请传入实际的知识库ID'
              # 创建一个列表，用于存放标签。多个标签之间是或（OR）的关系，不是与（AND）的关系。
              tags = ["A大学", "学生会主席"]
              retrieve_request.search_filters = [
                  {"tags": json.dumps(tags)}
              ]
              try:
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
      if __name__ == '__main__':
          TagQueryExample.main(sys.argv[1:])
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "IndexId": "8mbtdxxxxx",
        "WorkspaceId": "llm-4u5xpd1xdjxxxxxx",
        "Query": "请提供一些候选人",
        "SearchFilters": [{"tags":["A大学", "学生会主席"]}]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "Code": "Success",
        "Data": {
          "Nodes": [
            {
              "Metadata": {
                "file_path": "https://bailian-datahub-data-prod.oss-cn-beijing.aliyuncs.com/10285263/multimodal/docJson/%E5%BC%A0%E4%B8%89%E7%AE%80%E5%8E%86_1746760910599.json?Expires=1747020348&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE",
                "is_displayed_chunk_content": "true",
                "_rc_v_score": 0.1422617520249937,
                "image_url": [],
                "nid": "ba8a14099f43308734538c29271cc7cd|b99e98835c3c6d8f6496df5a43de0ba5|aa3ed8fc4aae8bbb78872994b01e0fda",
                "_q_score": 0.8935035934804278,
                "source": "0",
                "_score": 0.1736905574798584,
                "title": "",
                "doc_id": "file_e787926158704f95aad6bc967619f176_10285263",
                "content": "姓名：张三性别：男年龄：23",
                "_rc_score": 0,
                "workspace_id": "llm-4u5xpd1xdjxxxxxx",
                "hier_title": "",
                "page_number": [
                  0
                ],
                "doc_name": "张三简历",
                "pipeline_id": "8mbtdxxxxx",
                "_id": "llm-4u5xpd1xdjxxxxxx_8mbtdxxxxx_file_e787926158704f95aad6bc967619f176_10285263_0_0"
              },
              "Score": 0.1736905574798584,
              "Text": "姓名：张三性别：男年龄：23"
            },
            {
              "Metadata": {
                "file_path": "https://bailian-datahub-data-prod.oss-cn-beijing.aliyuncs.com/10285263/multimodal/docJson/%E7%8E%8B%E4%BA%94%E7%AE%80%E5%8E%86_1746760946844.json?Expires=1747020348&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE",
                "is_displayed_chunk_content": "true",
                "_rc_v_score": 0.1592178845871759,
                "image_url": [],
                "nid": "ba8a14099f43308734538c29271cc7cd|d4a048b6799ce07e08430f018af091a0|fe90f8248ea64f70ea37c0df7afcfc12",
                "_q_score": 1,
                "source": "0",
                "_score": 0.15737050771713257,
                "title": "",
                "doc_id": "file_63563df5df66488cb8e28bfff11e40eb_10285263",
                "content": "姓名：王五性别：男年龄：23",
                "_rc_score": 0,
                "workspace_id": "llm-4u5xpd1xdjxxxxxx",
                "hier_title": "",
                "page_number": [
                  0
                ],
                "doc_name": "王五简历",
                "pipeline_id": "8mbtdxxxxx",
                "_id": "llm-4u5xpd1xdjxxxxxx_8mbtdxxxxx_file_63563df5df66488cb8e28bfff11e40eb_10285263_0_0"
              },
              "Score": 0.15737050771713257,
              "Text": "姓名：王五性别：男年龄：23"
            }
          ]
        },
        "Message": "success",
        "RequestId": "12A5F9C6-xxxx-5593-8955-86D52585EE27",
        "Status": 200,
        "Success": true
      }
      ```
    </Accordion>
  </Tab>

  <Tab title="Java">
    <Accordion title="TagQueryExample.java">
      ```java expandable
      // 示例代码仅供参考，请勿在生产环境中直接使用
      import com.aliyun.bailian20231229.models.RetrieveRequest;
      import com.aliyun.bailian20231229.models.RetrieveResponse;
      import com.google.gson.Gson;
      import com.google.gson.JsonArray;
      import java.util.ArrayList;
      import java.util.HashMap;
      import java.util.List;
      import java.util.Map;
      public class TagQueryExample {
          /**
           * <b>description</b> :
           * <p>使用AK&SK初始化账号Client</p>
           *
           * @return Client
           * @throws Exception
           */
          public static com.aliyun.bailian20231229.Client createClient() throws Exception {
              // 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              // 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378657.html。
              com.aliyun.teaopenapi.models.Config config = new com.aliyun.teaopenapi.models.Config()
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。
                      .setAccessKeyId(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_ID"))
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
                      .setAccessKeySecret(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_SECRET"));
              // Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = "bailian.cn-beijing.aliyuncs.com";
              return new com.aliyun.bailian20231229.Client(config);
          }
          public static void main(String[] args) {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("请提供一些候选人");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  // 创建一个JsonArray，用于存放标签。多个标签之间是或（OR）的关系，不是与（AND）的关系。
                  JsonArray array = new JsonArray();
                  array.add("A大学");
                  array.add("学生会主席");
                  Map<String, String> map = new HashMap<>();
                  map.put("tags", array.toString());
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
      }
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "IndexId": "8mbtdxxxxx",
        "WorkspaceId": "llm-4u5xpd1xdjxxxxxx",
        "Query": "请提供一些候选人",
        "SearchFilters": "[{\"tags\":[\\\"A大学\\\", \\\"学生会主席\\\"]}]"
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "Code": "Success",
        "Data": {
          "Nodes": [
            {
              "Metadata": {
                "file_path": "https://bailian-datahub-data-prod.oss-cn-beijing.aliyuncs.com/10285263/multimodal/docJson/%E5%BC%A0%E4%B8%89%E7%AE%80%E5%8E%86_1746760910599.json?Expires=1747020348&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE",
                "is_displayed_chunk_content": "true",
                "_rc_v_score": 0.1422617520249937,
                "image_url": [],
                "nid": "ba8a14099f43308734538c29271cc7cd|b99e98835c3c6d8f6496df5a43de0ba5|aa3ed8fc4aae8bbb78872994b01e0fda",
                "_q_score": 0.8935035934804278,
                "source": "0",
                "_score": 0.1736905574798584,
                "title": "",
                "doc_id": "file_e787926158704f95aad6bc967619f176_10285263",
                "content": "姓名：张三性别：男年龄：23",
                "_rc_score": 0,
                "workspace_id": "llm-4u5xpd1xdjxxxxxx",
                "hier_title": "",
                "page_number": [
                  0
                ],
                "doc_name": "张三简历",
                "pipeline_id": "8mbtdxxxxx",
                "_id": "llm-4u5xpd1xdjxxxxxx_8mbtdxxxxx_file_e787926158704f95aad6bc967619f176_10285263_0_0"
              },
              "Score": 0.1736905574798584,
              "Text": "姓名：张三性别：男年龄：23"
            },
            {
              "Metadata": {
                "file_path": "https://bailian-datahub-data-prod.oss-cn-beijing.aliyuncs.com/10285263/multimodal/docJson/%E7%8E%8B%E4%BA%94%E7%AE%80%E5%8E%86_1746760946844.json?Expires=1747020348&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE",
                "is_displayed_chunk_content": "true",
                "_rc_v_score": 0.1592178845871759,
                "image_url": [],
                "nid": "ba8a14099f43308734538c29271cc7cd|d4a048b6799ce07e08430f018af091a0|fe90f8248ea64f70ea37c0df7afcfc12",
                "_q_score": 1,
                "source": "0",
                "_score": 0.15737050771713257,
                "title": "",
                "doc_id": "file_63563df5df66488cb8e28bfff11e40eb_10285263",
                "content": "姓名：王五性别：男年龄：23",
                "_rc_score": 0,
                "workspace_id": "llm-4u5xpd1xdjxxxxxx",
                "hier_title": "",
                "page_number": [
                  0
                ],
                "doc_name": "王五简历",
                "pipeline_id": "8mbtdxxxxx",
                "_id": "llm-4u5xpd1xdjxxxxxx_8mbtdxxxxx_file_63563df5df66488cb8e28bfff11e40eb_10285263_0_0"
              },
              "Score": 0.15737050771713257,
              "Text": "姓名：王五性别：男年龄：23"
            }
          ]
        },
        "Message": "success",
        "RequestId": "12A5F9C6-xxxx-5593-8955-86D52585EE27",
        "Status": 200,
        "Success": true
      }
      ```
    </Accordion>
  </Tab>
</Tabs>

<strong>示例：</strong>使用SearchFilters查询人才知识库，要求返回文档标签含<strong>A大学</strong>与<strong>体育特长生</strong>的相关文本切片（逻辑与）：

```json
{
  "searchFilters": [
    {
      "tags": ["A大学"]
    },
    {
      "tags": ["体育特长生"]
    }
  ]
}
```

> 展开下方折叠面板查看示例代码。

<Tabs>
  <Tab title="Python">
    <Accordion title="TagQueryExample2.py">
      ```python expandable
      import json
      import os
      import sys
      from typing import List
      from alibabacloud_bailian20231229 import models as bailian_20231229_models
      from alibabacloud_bailian20231229.client import Client as bailian20231229Client
      from alibabacloud_tea_openapi import models as open_api_models
      from alibabacloud_tea_util.client import Client as UtilClient
      class TagQueryExample2:
          def __init__(self):
              pass
          @staticmethod
          def create_client() -> bailian20231229Client:
              """
              使用AK&SK初始化账号Client
              @return: Client
              @throws Exception
              """
              # 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              # 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378659.html。
              config = open_api_models.Config(
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。,
                  access_key_id=os.environ['ALIBABA_CLOUD_ACCESS_KEY_ID'],
                  # 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。,
                  access_key_secret=os.environ['ALIBABA_CLOUD_ACCESS_KEY_SECRET']
              )
              # Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = f'bailian.cn-beijing.aliyuncs.com'
              return bailian20231229Client(config)
          @staticmethod
          def main(
                  args: List[str],
          ) -> None:
              client = TagQueryExample2.create_client()
              # 创建retrieve_request对象。
              retrieve_request = bailian_20231229_models.RetrieveRequest()
              # 必填，可传入用户实际输入的提示词。
              retrieve_request.query = '请提供一些候选人'
              # 必填，请传入实际的知识库ID。
              retrieve_request.index_id = '请传入实际的知识库ID'
              # 创建两个列表，用于存放标签。
              tag1 = ["A大学"]
              tag2 = ["体育特长生"]
              retrieve_request.search_filters = [
                  {"tags": json.dumps(tag1)},
                  {"tags": json.dumps(tag2)}
              ]
              try:
                  # 进行检索，传入业务空间ID和retrieve_request对象。
                  resp = client.retrieve('请传入实际的业务空间ID', retrieve_request)
                  print(UtilClient.to_jsonstring(resp.body))
              except Exception as error:
                  # 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  # 错误 message
                  print(error.message)
      if __name__ == '__main__':
          TagQueryExample2.main(sys.argv[1:])
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "IndexId": "8mbtdxxxxx",
        "WorkspaceId": "llm-4u5xpd1xdjxxxxxx",
        "Query": "请提供一些候选人",
        "SearchFilters": [{"tags":["A大学"]},{"tags":["体育特长生"]}]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "Code": "Success",
        "Data": {
          "Nodes": [
            {
              "Metadata": {
                "file_path": "https://bailian-datahub-data-prod.oss-cn-beijing.aliyuncs.com/10285263/multimodal/docJson/%E5%BC%A0%E4%B8%89%E7%AE%80%E5%8E%86_1746760910599.json?Expires=1747020348&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE",
                "is_displayed_chunk_content": "true",
                "_rc_v_score": 0.1422617520249937,
                "image_url": [],
                "nid": "ba8a14099f43308734538c29271cc7cd|b99e98835c3c6d8f6496df5a43de0ba5|aa3ed8fc4aae8bbb78872994b01e0fda",
                "_q_score": 1,
                "source": "0",
                "_score": 0.1736905574798584,
                "title": "",
                "doc_id": "file_e787926158704f95aad6bc967619f176_10285263",
                "content": "姓名：张三性别：男年龄：23",
                "_rc_score": 0,
                "workspace_id": "llm-4u5xpd1xdjxxxxxx",
                "hier_title": "",
                "page_number": [
                  0
                ],
                "doc_name": "张三简历",
                "pipeline_id": "8mbtdxxxxx",
                "_id": "llm-4u5xpd1xdjxxxxxx_8mbtdxxxxx_file_e787926158704f95aad6bc967619f176_10285263_0_0"
              },
              "Score": 0.1736905574798584,
              "Text": "姓名：张三性别：男年龄：23"
            }
          ]
        },
        "Message": "success",
        "RequestId": "1ED6CECE-xxxx-5B21-91DB-410E0219412A",
        "Status": 200,
        "Success": true
      }
      ```
    </Accordion>
  </Tab>

  <Tab title="Java">
    <Accordion title="TagQueryExample2.java">
      ```java expandable
      import com.aliyun.bailian20231229.models.RetrieveRequest;
      import com.aliyun.bailian20231229.models.RetrieveResponse;
      import com.google.gson.Gson;
      import com.google.gson.JsonArray;
      import java.util.ArrayList;
      import java.util.HashMap;
      import java.util.List;
      import java.util.Map;
      public class TagQueryExample2 {
          /**
           * <b>description</b> :
           * <p>使用AK&SK初始化账号Client</p>
           *
           * @return Client
           * @throws Exception
           */
          public static com.aliyun.bailian20231229.Client createClient() throws Exception {
              // 工程代码泄露可能会导致 AccessKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考。
              // 建议使用更安全的 STS 方式，更多鉴权访问方式请参见：https://help.aliyun.com/document_detail/378657.html。
              com.aliyun.teaopenapi.models.Config config = new com.aliyun.teaopenapi.models.Config()
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_ID。
                      .setAccessKeyId(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_ID"))
                      // 必填，请确保代码运行环境设置了环境变量 ALIBABA_CLOUD_ACCESS_KEY_SECRET。
                      .setAccessKeySecret(System.getenv("ALIBABA_CLOUD_ACCESS_KEY_SECRET"));
              // Endpoint 请参考 https://api.aliyun.com/product/bailian
              config.endpoint = "bailian.cn-beijing.aliyuncs.com";
              return new com.aliyun.bailian20231229.Client(config);
          }
          public static void main(String[] args) {
              try {
                  com.aliyun.bailian20231229.Client client = createClient();
                  // 创建RetrieveRequest对象。
                  RetrieveRequest request = new RetrieveRequest();
                  // 必填，可传入用户实际输入的提示词。
                  request.setQuery("请提供一些候选人");
                  // 必填，请传入实际的知识库ID。
                  request.setIndexId("请传入实际的知识库ID");
                  // 创建一个JsonArray，用于存放标签。
                  JsonArray tag1 = new JsonArray();
                  tag1.add("A大学");
                  JsonArray tag2 = new JsonArray();
                  tag2.add("体育特长生");
                  Map<String, String> map1 = new HashMap<>();
                  map1.put("tags", tag1.toString());
                  Map<String, String> map2 = new HashMap<>();
                  map2.put("tags", tag2.toString());
                  List<Map<String, String>> searchFilters = new ArrayList<>();
                  searchFilters.add(map1);
                  searchFilters.add(map2);
                  request.setSearchFilters(searchFilters);
                  // 进行检索，传入业务空间ID和RetrieveRequest。
                  RetrieveResponse resp = client.retrieve("请传入实际的业务空间ID", request);
                  System.out.println(new Gson().toJson(resp.getBody()));
              } catch (Exception e) {
                  // 知识库检索失败处理。
                  // 此处仅做打印展示，请谨慎对待异常处理，在工程项目中切勿直接忽略异常。
                  System.out.println(e.getMessage());
              }
          }
      }
      ```
    </Accordion>

    <Accordion title="请求示例">
      [请求参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-36)

      ```json
      {
        "IndexId": "8mbtdxxxxx",
        "WorkspaceId": "llm-4u5xpd1xdjxxxxxx",
        "Query": "请提供一些候选人",
        "SearchFilters": [{"tags":["A大学"]},{"tags":["体育特长生"]}]
      }
      ```
    </Accordion>

    <Accordion title="响应示例">
      [返回参数说明](/zh/model-studio/api-bailian-2023-12-29-retrieve#api-detail-41)

      ```json expandable
      {
        "Code": "Success",
        "Data": {
          "Nodes": [
            {
              "Metadata": {
                "file_path": "https://bailian-datahub-data-prod.oss-cn-beijing.aliyuncs.com/10285263/multimodal/docJson/%E5%BC%A0%E4%B8%89%E7%AE%80%E5%8E%86_1746760910599.json?Expires=1747020348&OSSAccessKeyId=YOUR_ACCESS_KEY_ID&Signature=YOUR_SIGNATURE",
                "is_displayed_chunk_content": "true",
                "_rc_v_score": 0.1422617520249937,
                "image_url": [],
                "nid": "ba8a14099f43308734538c29271cc7cd|b99e98835c3c6d8f6496df5a43de0ba5|aa3ed8fc4aae8bbb78872994b01e0fda",
                "_q_score": 1,
                "source": "0",
                "_score": 0.1736905574798584,
                "title": "",
                "doc_id": "file_e787926158704f95aad6bc967619f176_10285263",
                "content": "姓名：张三性别：男年龄：23",
                "_rc_score": 0,
                "workspace_id": "llm-4u5xpd1xdjxxxxxx",
                "hier_title": "",
                "page_number": [
                  0
                ],
                "doc_name": "张三简历",
                "pipeline_id": "8mbtdxxxxx",
                "_id": "llm-4u5xpd1xdjxxxxxx_8mbtdxxxxx_file_e787926158704f95aad6bc967619f176_10285263_0_0"
              },
              "Score": 0.1736905574798584,
              "Text": "姓名：张三性别：男年龄：23"
            }
          ]
        },
        "Message": "success",
        "RequestId": "1ED6CECE-xxxx-5B21-91DB-410E0219412A",
        "Status": 200,
        "Success": true
      }
      ```
    </Accordion>
  </Tab>
</Tabs>

## 相关文档 <span id="33ad6109eajw8" />

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "18%" }} /><col style={{ width: "82%" }} /></colgroup><tbody><tr><td style={{ verticalAlign: "top" }}><p><strong>知识库用户指南</strong></p></td><td style={{ verticalAlign: "top" }}><p>请参见<a href="/zh/model-studio/rag-knowledge-base">创建和使用知识库</a>。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>检索知识库</strong></p></td><td style={{ verticalAlign: "top" }}><p>可调用<a href="/zh/model-studio/api-bailian-2023-12-29-retrieve">Retrieve</a>接口检索知识库并返回文本切片。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><strong>子账号调用</strong></p></td><td style={{ verticalAlign: "top" }}><p>RAM用户（子账号）请先获取阿里云百炼的数据权限再调用<a href="/zh/model-studio/api-bailian-2023-12-29-retrieve">Retrieve</a>接口，请参见<a href="/zh/model-studio/grant-data-access-permission-to-ram-user">授权RAM用户API权限</a>。</p></td></tr></tbody></table>

## 错误码 <span id="efc117f59a2gm" />

如果调用失败并收到报错信息，请参见[错误中心](https://api.aliyun.com/document/bailian/2023-12-29/errorCode)进行解决。

> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 提交解析任务

> 提交文档或音视频解析任务，返回 biz_id 用于异步查询

<meta property="og:title" content="提交解析任务" />

提交文档或音视频解析任务，返回 `biz_id`。文档解析为异步任务：提交文件获得 `biz_id`，再使用该 ID 调用[查询解析结果](/zh/model-studio/parse/parse-result)获取处理状态和结果。

<Note>
  调用前请确保已获取 API Key，详见[鉴权](/zh/model-studio/parse/authentication)。
</Note>

## 请求参数

| 参数                                                                        | 类型        | 必填 | 说明                                                |
| ------------------------------------------------------------------------- | --------- | -- | ------------------------------------------------- |
| `file_url`                                                                | string    | 是  | 待解析文件的 URL。                                       |
| `file_name`                                                               | string    | 否  | 文件名称。协议同时提供 `file_name_extension`，二者按文件信息填写方式二选一。 |
| `file_name_extension`                                                     | string    | 否  | 文件类型。与 `file_name` 二选一。                           |
| `config_id`                                                               | string    | 否  | 配置 ID。例如 `config_xxx`，与内联参数同时传递时内联参数优先生效。         |
| `processing`                                                              | object    | 否  | 处理配置。                                             |
| `processing.user_prompt`                                                  | string    | 否  | 用户自定义 Prompt。                                     |
| `processing.doc_processing_config`                                        | object    | 否  | 文档解析处理配置。                                         |
| `processing.doc_processing_config.page_index`                             | string    | 否  | 解析页数范围。例如 `1-10`。                                 |
| `processing.doc_processing_config.head_foot`                              | boolean   | 否  | 是否解析页眉和页脚。                                        |
| `processing.doc_processing_config.layout_position`                        | boolean   | 否  | 是否返回坐标。                                           |
| `processing.doc_processing_config.image_caption`                          | boolean   | 否  | 是否生成图片描述。                                         |
| `processing.media_processing_config`                                      | object    | 否  | 音视频解析处理配置。                                        |
| `processing.media_processing_config.enable_diarization`                   | boolean   | 否  | 是否进行人声分离。                                         |
| `processing.media_processing_config.enable_synopsis_parse`                | boolean   | 否  | 是否进行剧情解析。                                         |
| `processing.media_processing_config.enable_synopsis_segments`             | boolean   | 否  | 是否生成剧情解析段落。                                       |
| `processing.media_processing_config.enable_synopsis_summary`              | boolean   | 否  | 是否生成剧情解析摘要。                                       |
| `processing.media_processing_config.frame_extraction`                     | object    | 否  | 抽帧配置。                                             |
| `processing.media_processing_config.frame_extraction.mode`                | string    | 否  | 抽帧模式。可选值：`auto`、`frame_rate`                      |
| `processing.media_processing_config.frame_extraction.frame_rate`          | number    | 否  | 抽帧帧率，范围为 `0.2`～`5`                                |
| `processing.media_processing_config.frame_extraction.output_image_width`  | integer   | 否  | 输出帧图像宽度。不指定时使用原始分辨率，取值范围 \[32, 4096]              |
| `processing.media_processing_config.frame_extraction.output_image_height` | integer   | 否  | 输出帧图像高度。不指定时使用原始分辨率，取值范围 \[32, 4096]              |
| `output`                                                                  | object    | 否  | 输出配置。                                             |
| `output.output_file_format`                                               | string\[] | 否  | 输出格式字符串数组。可选值：`markdown`                          |
| `output.layout_table_format`                                              | string    | 否  | Markdown 中表格的输出格式。可选值：`markdown`、`html`           |
| `output.oss_config`                                                       | object    | 否  | OSS 输出配置。                                         |
| `output.oss_config.bucket`                                                | string    | 否  | 托管 OSS Bucket 名称。                                 |
| `output.oss_config.endpoint`                                              | string    | 否  | 托管 OSS Endpoint。                                  |
| `output.oss_config.access_key_id`                                         | string    | 否  | 托管 OSS AccessKey ID。                              |
| `output.oss_config.access_key_secret`                                     | string    | 否  | 托管 OSS AccessKey Secret。                          |
| `output.oss_config.security_token`                                        | string    | 否  | 托管 OSS Security Token。                            |

## 返回结果

| 字段            | 类型     | 说明                           |
| ------------- | ------ | ---------------------------- |
| `request_id`  | string | 请求 ID                        |
| `data.biz_id` | string | 业务任务 ID，用于调用 `/parse/result` |

## 文档解析代码示例

以下示例分别展示两种处理定义， `config_id` 与 `processing` 同时存在时，`processing` 会优先生效。示例中的 URL、任务 ID 和配置 ID 均为脱敏占位值。

<Tabs>
  <Tab title="使用已保存的配置">
    ```text title="通过 config_id 配置参数"
    curl -X POST 'https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/parse-x/parse/submit' \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      -H 'Content-Type: application/json' \
      -d '{
        "file_url": "https://example.com/document.pdf",
        "file_name": "document.pdf",
        "config_id": "config_xxx"
    }'
    ```

    ```json
    {
        "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "data": {
            "biz_id": "parseX-2026xxxx-xxxxxxx"
        }
    }
    ```
  </Tab>

  <Tab title="使用内联配置">
    ```text title="通过 processing 配置参数"
    curl -X POST 'https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/parse-x/parse/submit' \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      -H 'Content-Type: application/json' \
      -d '{
        "file_url": "https://example.com/document.pdf",
        "file_name": "document.pdf",
        "processing": {
            "doc_processing_config": {
                "page_index": "1-10",
                "head_foot": false,
                "layout_position": true
            }
        },
        "output": {
            "output_file_format": [
                "markdown"
            ],
            "layout_table_format": "markdown"
        }
    }'
    ```

    ```json
    {
        "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "data": {
            "biz_id": "parseX-2026xxxx-xxxxxxx"
        }
    }
    ```
  </Tab>
</Tabs>

## 音视频解析代码示例

<Tabs>
  <Tab title="使用已保存的配置">
    ```text title="通过 config_id 配置参数"
    curl -X POST 'https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/parse-x/parse/submit' \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      -H 'Content-Type: application/json' \
      -d '{
        "file_url": "https://docmind-api-cn-hangzhou.oss-cn-hangzhou.aliyuncs.com/static/Wan3.0模型创意视频.mp4",
        "file_name": "test.mp4",
        "config_id": "config_xxx"
    }'
    ```

    ```json
    {
        "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "data": {
            "biz_id": "parseX-Video-2026xxxx-xxxxxxx"
        }
    }
    ```
  </Tab>

  <Tab title="使用内联配置">
    ```text title="通过 processing 配置参数"
    curl -X POST 'https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/parse-x/parse/submit' \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      -H 'Content-Type: application/json' \
      -d '{
        "file_url": "https://docmind-api-cn-hangzhou.oss-cn-hangzhou.aliyuncs.com/static/Wan3.0模型创意视频.mp4",
        "file_name": "test.mp4",
        "processing": {
            "media_processing_config": {
                "enable_diarization": true,
                "enable_synopsis_parse": true,
                "enable_synopsis_segments": true,
                "enable_synopsis_summary": true,
                "frame_extraction": {
                    "mode": "auto",
                    "output_image_width": 1024,
                    "output_image_height": 1024
                }
            }
        }
    }'
    ```

    ```json
    {
        "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "data": {
            "biz_id": "parseX-Video-2026xxxx-xxxxxxx"
        }
    }
    ```
  </Tab>
</Tabs>

<Tip>
  提交后，使用[查询解析结果](/zh/model-studio/parse/parse-result)轮询任务状态并获取解析结果。
</Tip>

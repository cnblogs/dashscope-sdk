> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 查询解析结果

> 查询解析任务的状态、结果或分片

<meta property="og:title" content="查询解析结果" />

调用本接口查询解析任务的状态、结果或分片。`data.status` 为 `processing` 时继续轮询，为 `success` 或 `failed` 时停止。

<Note>
  调用前请确保已获取 API Key，详见[鉴权](/zh/model-studio/parse/authentication)。
</Note>

## 请求参数

| 参数                             | 类型      | 必填 | 说明                                                 |
| ------------------------------ | ------- | -- | -------------------------------------------------- |
| `biz_id`                       | string  | 是  | `/parse/submit` 提交接口返回的业务任务 ID。                    |
| `step_start`                   | integer | 否  | 分片起始位置，默认 `0`。文档结果对应 `layouts`，音视频结果对应 `segments`。 |
| `step_size`                    | integer | 否  | 分片步长，默认 `0`                                        |
| `oss_config`                   | object  | 否  | OSS 输出配置。                                          |
| `oss_config.access_key_id`     | string  | 否  | 托管 OSS AccessKey ID。                               |
| `oss_config.access_key_secret` | string  | 否  | 托管 OSS AccessKey Secret。                           |
| `oss_config.security_token`    | string  | 否  | 托管 OSS Security Token。                             |

## 返回结果

| 字段                                            | 类型      | 说明                                                              |
| --------------------------------------------- | ------- | --------------------------------------------------------------- |
| `request_id`                                  | string  | 请求 ID。                                                          |
| `data.status`                                 | string  | 处理状态：`init` 、`processing`、`success` 或 `failed`。                 |
| `data.processing`                             | number  | 处理进度。                                                           |
| `data.successful_parsing_num`                 | integer | 已处理的 `layouts` 或 `segments` 数量。                                 |
| `data.image_count`                            | integer | 图片数量。                                                           |
| `data.table_count`                            | integer | 表格数量。                                                           |
| `data.paragraph_count`                        | integer | 段落数量。                                                           |
| `data.page_count`                             | integer | 页数。                                                             |
| `data.layouts`                                | array   | 查询分片的布局结果。                                                      |
| `data.output_format_result`                   | array   | 输出格式结果列表。                                                       |
| `data.output_format_result[].output_file_url` | string  | 输出类型对应 URL。                                                     |
| `data.output_format_result[].output_type`     | string  | 输出类型。当值为 `markdown` 时，`output_file_url` 为 Markdown 格式解析结果的 URL。 |
| `data.media_duration`                         | number  | 文件时长（音视频）。                                                      |
| `data.segments`                               | array   | 解析段落结果（音视频）。                                                    |
| `data.synopsis_result`                        | string  | 剧情解析结果（音视频）。                                                    |
| `data.synopsis_segments`                      | array   | 剧情解析分段（音视频）。                                                    |
| `data.synopsis_summary`                       | string  | 剧情解析摘要（音视频）。                                                    |

<Note>
  当状态为 `processing` 时继续查询本接口；当状态为 `failed` 时，结合[错误码](/zh/model-studio/parse/errors)判断失败原因。
</Note>

## 结果查询代码示例

<Tabs>
  <Tab title="文档解析结果查询">
    ```
    curl -X POST 'https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/parse-x/parse/result' \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      -H 'Content-Type: application/json' \
      -d '{
        "biz_id": "parseX-xxx",
        "step_start": 0,
        "step_size": 1
      }'
    ```

    ```json 返回结果示例
    {
      "request_id": "request_xxx",
      "data": {
        "status": "success",
        "processing": 100.0,
        "successful_parsing_num": 10,
        "image_count": 0,
        "table_count": 0,
        "paragraph_count": 10,
        "page_count": 1,
        "layouts": [],
        "output_format_result": [
          {
            "output_file_url": "https://example.com/output/parseX-xxx.md",
            "output_type": "markdown"
          }
        ]
      }
    }
    ```
  </Tab>

  <Tab title="音视频解析结果查询">
    ```
    curl -X POST 'https://{workspaceId}.cn-beijing.maas.aliyuncs.com/api/v2/apps/parse-x/parse/result' \
      -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
      -H 'Content-Type: application/json' \
      -d '{
        "biz_id": "parseX-Video-xxx",
        "step_start": 0,
        "step_size": 100
      }'
    ```

    ```json
    {
        "request_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "data": {
          "status": "success",
          "processing": 100.0,
          "successful_parsing_num": 59,
          "segments": [
            {
              "start_time": 5120.0,
              "file_url": "https://example.com/output/parseX-video-xxx/segments/segment_1_video.mp4",
              "audio_frames": [
                {
                  "start_time": 7000.0,
                  "file_url": "https://example.com/output/parseX-video-xxx/segments/segment_1_audio.mp3",
                  "ASR_info": "来进来有新座吗？",
                  "sentences": [
                    {
                      "speaker_id": 1,
                      "end_time": 10380.0,
                      "begin_time": 7000.0,
                      "text": "来进来有新座吗？"
                    }
                  ],
                  "end_time": 10380.0
                }
              ],
              "end_time": 10380.0,
              "index": 1,
              "uniqueId": "6a00110588ba0646417aa6b3f8d97f76",
              "video_frames": [
                {
                  "start_time": 7500.0,
                  "file_url": "https://example.com/output/parseX-video-xxx/frames/frame_4_0.jpg",
                  "text_info": "门被打开，一位头戴白色棒球帽、身穿绿色工装马甲的年轻男子满脸笑容地伸手握住敲门者的手，热情地说道“来 进来”",
                  "end_time": 7566.666666666666
                },
                {
                  "start_time": 8400.0,
                  "file_url": "https://example.com/output/parseX-video-xxx/frames/frame_5_0.jpg",
                  "text_info": "男子站在采光良好的客厅内，侧身回头并向屋内做出引导的手势，背景中可见书架、玻璃圆桌以及靠窗的蓝色休闲椅",
                  "end_time": 9266.666666666668
                },
                {
                  "start_time": 9933.333333333334,
                  "file_url": "https://example.com/output/parseX-video-xxx/frames/frame_6_0.jpg",
                  "text_info": "一个阳光充足的客厅，铺着绿色地毯，左侧有一个装满书的木制书架，角落里放着一把蓝色扶手椅。前景的玻璃咖啡桌上放着一个披萨盒和一本书，墙上挂着一张海报，旁边有一条垂直的绿色装饰带。",
                  "end_time": 10600.0
                }
              ]
            },
            ...
          ],
          "synopsis_result": "# 视频剧情内容：### 剧情概要\n\n视频....",
          "synopsis_segments": [
            {
              "start_time": 0,
              "file_url": "https://example.com/output/parseX-video-xxx/segments/segment_0_video.mp4",
              "end_time": 5000,
              "index": 0,
              "text": "渔夫帽男子在狭长走廊里开始这段vlog，镜头先扫到他深橄榄色T恤、米白色裤子和浅色背包，随后他戴着米色渔夫帽，露出黑色卷发，对着镜头微笑。他说：“今天没事，去看看朋友们都在干什么？”随后他来到一扇带猫眼的深棕色木门前，抬手敲门。",
              "uniqueId": "b35998c4077d3e8645e4b198cd920e24"
            },
            ...
          ],
          "synopsis_summary": "视频以一段轻松的朋友探访vlog展开：...."
        }
      }
    ```
  </Tab>
</Tabs>

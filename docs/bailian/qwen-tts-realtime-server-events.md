> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 服务端事件

> 本文介绍 Qwen-TTS-Realtime API 的服务端事件。

<meta property="og:title" content="服务端事件" />

> 相关文档：[实时语音合成](/zh/model-studio/realtime-tts-user-guide)。

## error <span id="a5065ac8c1psu" />

不论是遇到客户端错误还是服务端错误，服务端都会响应该事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`error`。

        <strong>error</strong>`object`

        错误详情。

        <Accordion title="属性" defaultOpen>
          <strong>code</strong>*string*

          错误码。

          <strong>message</strong>*string*

          错误信息。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "event_id": "event_QzAVZRVa9hKqM5VOaHunh",
          "type": "error",
          "error": {
            "code": "invalid_value",
            "message": "Session update error: session already started or finished or failed."
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.created <span id="05bb97c283l2n" />

客户端连接到服务端后，响应的第一个事件，该事件返回时会携带服务端对此次连接的默认配置信息。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`session.created`。

        <strong>session</strong>`object`

        会话配置。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          会话ID。

          <strong>object</strong>`string`

          会话服务名。

          <strong>mode</strong>`string`

          交互模式，`server_commit`或`commit`。

          <strong>model</strong>`string`

          使用的模型。

          <strong>voice</strong>`string`

          使用的音色。

          <strong>response\_format</strong>`string`

          音频格式。

          <strong>sample\_rate</strong>`integer`

          音频采样率。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "event_id": "event_xxx",
          "type": "session.created",
          "session": {
            "object": "realtime.session",
            "mode": "server_commit",
            "model": "qwen3-tts-flash-realtime",
            "voice": "Cherry",
            "response_format": "pcm",
            "sample_rate": 24000,
            "id": "sess_xxx"
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.updated <span id="58f078ab61di5" />

接收到客户端的`session.update`请求并正确处理后返回。如果出现错误，则直接返回error事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`session.updated`。

        <strong>session</strong>`object`

        会话配置。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          会话ID。

          <strong>object</strong>`string`

          会话服务名。

          <strong>mode</strong>`string`

          交互模式，`server_commit`或`commit`。

          <strong>model</strong>`string`

          使用的模型。

          <strong>voice</strong>`string`

          使用的音色。

          <strong>response\_format</strong>`string`

          音频格式。

          <strong>sample\_rate</strong>`integer`

          音频采样率。

          <strong>language\_type</strong>`string`

          音频语种。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "event_id": "event_xxx",
          "type": "session.updated",
          "session": {
            "id": "sess_xxx",
            "object": "realtime.session",
            "model": "qwen3-tts-flash-realtime",
            "voice": "Cherry",
            "language_type": "Chinese",
            "mode": "commit",
            "response_format": "pcm",
            "sample_rate": 24000
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_text\_buffer.committed <span id="76abc25665qt8" />

客户端发送`input_text_buffer.commit`事件后，服务端的响应事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`input_text_buffer.committed`。

        <strong>item\_id</strong>`string`

        将创建的用户消息项的 ID。
      </td>

      <td>
        ```json
        {
          "event_id": "event_FC6MA88wS2oEeXkPvWsxX",
          "type": "input_text_buffer.committed",
          "item_id": ""
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_text\_buffer.cleared <span id="7a04edb8840o7" />

客户端发送`input_audio_buffer.clear`事件后，服务端的响应事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`input_text_buffer.cleared`。
      </td>

      <td>
        ```json
        {
            "event_id": "event_1122",
            "type": "input_text_buffer.cleared"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.created <span id="4e06909a7es5k" />

客户端发送`input_text_buffer.commit`事件后，服务端的响应事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`response.created`。

        <strong>response</strong>`object`

        响应详情。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          响应ID。

          <strong>object</strong>`string`

          对象类型，在此事件下固定为`realtime.response`。

          <strong>status</strong>`string`

          响应的最终状态，取值范围：

          - completed
          - failed
          - in\_progress
          - incomplete

          <strong>voice</strong>`string`

          使用的音色。

          <strong>output</strong>`array`

          在此事件下为空。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "event_id": "event_IMnLqDvG6Ahhk7sWV2uOs",
          "type": "response.created",
          "response": {
            "id": "resp_USvBwHktHcz76r6GaIJUV",
            "object": "realtime.response",
            "conversation_id": "",
            "status": "in_progress",
            "voice": "Cherry",
            "output": []
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.output\_item.added <span id="efd58c096b3bv" />

当新的item项需要输出时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`response.output_item.added`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引，目前固定为0。

        <strong>item</strong>`object`

        输出项信息。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          输出项ID。

          <strong>object</strong>`string`

          始终为 `realtime.item`。

          <strong>status</strong>`string`

          输出项的状态。

          <strong>content</strong>`array`

          消息的内容。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "event_id": "event_INDGnGNulaXCrStd9ZM5X",
          "type": "response.output_item.added",
          "response_id": "resp_USvBwHktHcz76r6GaIJUV",
          "output_index": 0,
          "item": {
            "id": "item_FIrYGaNVK3rbIZqeY4QjM",
            "object": "realtime.item",
            "type": "message",
            "status": "in_progress",
            "role": "assistant",
            "content": []
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.content\_part.added <span id="141bec9bb11yi" />

当新的内容项需要输出时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`response.content_part.added`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引，目前固定为0。

        <strong>content\_index</strong>`integer`

        响应输出项中内部部分的索引，目前固定为0。

        <strong>part</strong>`object`

        已完成的内容部分。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`

          内容部分的类型。

          <strong>text</strong>`string`

          内容部分的文本。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "event_id": "event_DigZ95MWN36YYyyjcENoq",
          "type": "response.content_part.added",
          "response_id": "resp_USvBwHktHcz76r6GaIJUV",
          "item_id": "item_FIrYGaNVK3rbIZqeY4QjM",
          "output_index": 0,
          "content_index": 0,
          "part": {
            "type": "audio",
            "text": ""
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio.delta <span id="a2b95643bdvnl" />

当模型增量生成新的audio数据时，系统会返回服务器 response.audio.delta 事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`response.audio.delta`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引，目前固定为0。

        <strong>content\_index</strong>`integer`

        响应输出项中内部部分的索引，目前固定为0。

        <strong>delta</strong>`string`

        模型增量输出的audio数据，使用Base64编码。
      </td>

      <td>
        ```json
        {
          "event_id": "event_B1osWMZBtrEQbiIwW0qHQ",
          "type": "response.audio.delta",
          "response_id": "resp_B1osWTzBb8hO0WsELHgVP",
          "item_id": "item_B1osWH81fXDoyim1T5fsF",
          "output_index": 0,
          "content_index": 0,
          "delta": "base64 audio"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.content\_part.done <span id="5244e1dbc9vw6" />

当新的内容项输出完成时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`response.content_part.done`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引，目前固定为0。

        <strong>content\_index</strong>`integer`

        响应输出项中内部部分的索引，目前固定为0。

        <strong>part</strong>`object`

        已完成的内容部分。

        <Accordion title="属性" defaultOpen>
          <strong>type</strong>`string`

          内容部分的类型。

          <strong>text</strong>`string`

          内容部分的文本。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "event_id": "event_Vo2YUjlYQJ4colH8nVzkU",
          "type": "response.content_part.done",
          "response_id": "resp_USvBwHktHcz76r6GaIJUV",
          "item_id": "item_FIrYGaNVK3rbIZqeY4QjM",
          "output_index": 0,
          "content_index": 0,
          "part": {
            "type": "audio",
            "text": ""
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.output\_item.done <span id="a922404106vzd" />

当新的item输出完成时，服务端返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`response.output_item.done`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引，目前固定为0。

        <strong>item</strong>`object`

        输出项信息。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          输出项ID。

          <strong>object</strong>`string`

          始终为 `realtime.item`。

          <strong>status</strong>`string`

          输出项的状态。

          <strong>content</strong>`array`

          消息的内容。
        </Accordion>
      </td>

      <td>
        ```json
        {
          "event_id": "event_LO6SJRKIQ9NBayyYB8a1A",
          "type": "response.output_item.done",
          "response_id": "resp_USvBwHktHcz76r6GaIJUV",
          "output_index": 0,
          "item": {
            "id": "item_FIrYGaNVK3rbIZqeY4QjM",
            "object": "realtime.item",
            "type": "message",
            "status": "completed",
            "role": "assistant",
            "content": [
              {
                "type": "audio",
                "text": ""
              }
            ]
          }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.audio.done <span id="faa3f9bc106tj" />

当模型生成audio数据完成时，系统会返回服务器 response.audio.done 事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`response.audio.done`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>item\_id</strong>`string`

        消息项ID。

        <strong>output\_index</strong>`integer`

        响应输出项的索引，目前固定为0。

        <strong>content\_index</strong>`integer`

        响应输出项中内部部分的索引，目前固定为0。
      </td>

      <td>
        ```json
        {
          "event_id": "event_LZaOHPzXYMUXGBcVkBmKX",
          "type": "response.audio.done",
          "response_id": "resp_USvBwHktHcz76r6GaIJUV",
          "item_id": "item_FIrYGaNVK3rbIZqeY4QjM",
          "output_index": 0,
          "content_index": 0
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## response.done <span id="91f767e7440pc" />

当响应生成完成时，服务端会返回此事件。该事件中包含的 Response 对象将包含 Response 中的所有输出项，但不包括已返回的原始音频数据。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`response.done`。

        <strong>response\_id</strong>`string`

        响应的ID。

        <strong>response</strong>`object`

        响应详情。

        <Accordion title="属性" defaultOpen>
          <strong>id</strong>`string`

          响应ID。

          <strong>object</strong>`string`

          对象类型，在此事件下固定为`realtime.response`。

          <strong>output</strong>`array`

          响应的输出。

          <strong>usage</strong>`object`

          本次语音合成计费信息。

          <Accordion title="属性" defaultOpen>
            <strong>characters</strong>`integer`

            Qwen3-TTS Realtime计费字符数。
          </Accordion>
        </Accordion>
      </td>

      <td>
        <CodeGroup>
          ```json Qwen3-TTS Realtime expandable
          {
              "event_id": "event_Aemy83XqHFFDDSeJIDn6N",
              "type": "response.done",
              "response": {
                  "id": "resp_LFeR42yXZ9SxUAeXjmyTz",
                  "object": "realtime.response",
                  "conversation_id": "",
                  "status": "completed",
                  "modalities": [
                      "text",
                      "audio"
                  ],
                  "voice": "Cherry",
                  "output": [
                      {
                          "id": "item_Ae1lv2XmRljRSG96L8Zm1",
                          "object": "realtime.item",
                          "type": "message",
                          "status": "completed",
                          "role": "assistant",
                          "content": [
                              {
                                  "type": "audio",
                                  "transcript": ""
                              }
                          ]
                      }
                  ],
                  "usage": {
                      "characters": 25
                  }
              }
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## session.finished <span id="ac332f7d78oxg" />

当所有响应生成完成时，服务端会返回此事件。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`

        服务端事件ID。

        <strong>type</strong>`string`

        事件类型，固定为`session.finished`。
      </td>

      <td>
        ```json
        {
          "event_id": "event_2239",
          "type": "session.finished"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

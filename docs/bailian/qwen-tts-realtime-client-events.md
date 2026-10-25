> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 客户端事件

> 本文介绍 Qwen-TTS Realtime API 的客户端事件。

<meta property="og:title" content="客户端事件" />

> 相关文档：[实时语音合成](/zh/model-studio/realtime-tts-user-guide)。

## session.update <span id="a5065ac8c1psu" />

用于更新会话配置。在WebSocket连接建立成功后，可立即发送此事件作为交互的第一步。如果未发送，系统将使用默认配置。服务端成功处理此事件后，会返回`session.updated`事件作为确认。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`<strong>（必选）</strong>

        客户端生成的唯一事件ID。在单次WebSocket连接会话中必须保持唯一。强烈建议使用 UUID（通用唯一标识符）。

        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为`session.update`。

        <strong>session</strong>`object`（可选）

        会话配置。

        <Accordion title="属性" defaultOpen>
          <strong>voice</strong>`string`<strong>（必选）</strong>

          语音合成所使用的系统音色，参见[支持的音色](/zh/model-studio/realtime-tts-user-guide#bac280ddf5a1u)。

          <strong>mode</strong>`string`（可选）

          交互模式，可选值：

          - `server_commit`（默认）：服务端自动判断合成时机，平衡延迟与质量，推荐大多数场景使用
          - `commit`：客户端手动触发合成，延迟最低，但需自行管理句子完整性

          <strong>language\_type</strong>`string`（可选）

          指定合成音频的语种，默认为 `Auto`。

          - `Auto`：适用无法确定文本的语种或文本包含多种语言的场景，模型会自动为文本中的不同语言片段匹配各自的发音，但无法保证发音完全精准。
          - 指定语种：适用于文本为单一语种的场景，此时指定为具体语种，能显著提升合成质量，效果通常优于 `Auto`。可选值包括：

            - `Chinese`
            - `English`
            - `German`
            - `Italian`
            - `Portuguese`
            - `Spanish`
            - `Japanese`
            - `Korean`
            - `French`
            - `Russian`

          <strong>response\_format</strong>`string`（可选）

          模型输出音频的格式。

          支持的格式：

          - `pcm`（默认）
          - `wav`
          - `mp3`
          - `opus`

          <strong>sample\_rate</strong>`integer`（可选）

          模型输出音频的采样率（Hz）。

          支持的采样率：

          - 8000
          - 16000
          - 24000（默认）
          - 48000

          <strong>speech\_rate</strong>`float`（可选）

          音频的语速。1.0为正常语速，小于1.0为慢速，大于1.0为快速。

          默认值：1.0。

          取值范围：\[0.5, 2.0]。

          <strong>volume</strong>`integer`（可选）

          音频的音量。

          默认值：50。

          取值范围：\[0, 100]。

          <strong>pitch\_rate</strong>`float`（可选）

          合成音频的语调。

          默认值：1.0。

          取值范围：\[0.5, 2.0]。

          <strong>bit\_rate</strong>`integer`（可选）

          指定音频的[码率](https://opus-codec.org/)（kbps）。码率越大，音质越好，音频文件体积越大。仅在音频格式（`response_format`）为`opus`时可用。

          默认值：128。

          取值范围：\[6, 510]。
        </Accordion>
      </td>

      <td>
        ```json
        {
            "event_id": "event_123",
            "type": "session.update",
            "session": {
                "voice": "Cherry",
                "mode": "server_commit",
                "language_type": "Chinese",
                "response_format": "pcm",
                "sample_rate": 24000
            }
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_text\_buffer.append <span id="37be8f579bxvj" />

用于将待合成文本追加到文本缓冲区。在server\_commit模式中，文本将追加到服务端的文本缓冲区；在commit模式中，文本将追加到客户端的文本缓冲区。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`<strong>（必选）</strong>

        客户端生成的唯一事件ID。在单次WebSocket连接会话中必须保持唯一。强烈建议使用 UUID（通用唯一标识符）。

        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为`input_text_buffer.append`。

        <strong>text</strong>`string`<strong>（必选）</strong>

        待合成文本。
      </td>

      <td>
        ```json
        {
          "event_id": "event_B4o9RHSTWobB5OQdEHLTo",
          "type": "input_text_buffer.append",
          "text": "您好，我是千问。"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_text\_buffer.commit <span id="63086fc329sib" />

用于提交用户输入文本缓冲区，从而在对话中创建新的用户消息项。 如果输入的文本缓冲区为空，此事件将产生错误。处于“server\_commit”模式时，用户提交此事件，表示立即合成之前的所有文本，服务器不再缓存文本。处于“commit”模式时，客户端必须提交文本缓冲区才能创建用户消息项。提交输入文本缓冲区不会从模型创建响应，服务器将返回 `input_text_buffer.committed` 事件进行响应。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`<strong>（必选）</strong>

        客户端生成的唯一事件ID。在单次WebSocket连接会话中必须保持唯一。强烈建议使用 UUID（通用唯一标识符）。

        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为`input_text_buffer.commit`。
      </td>

      <td>
        ```json
        {
          "event_id": "event_B4o9RHSTWobB5OQdEHLTo",
          "type": "input_text_buffer.commit"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## input\_text\_buffer.clear <span id="794b88b49ennz" />

用于清除缓冲区中的文本。服务端返回`input_text_buffer.cleared` 事件进行响应。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`<strong>（必选）</strong>

        客户端生成的唯一事件ID。在单次WebSocket连接会话中必须保持唯一。强烈建议使用 UUID（通用唯一标识符）。

        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为`input_text_buffer.clear`。
      </td>

      <td>
        ```json
        {
          "event_id": "event_2728",
          "type": "input_text_buffer.clear"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

## session.finish <span id="53715b556bstd" />

客户端发送 `session.finish` 事件通知服务端不再有文本输入，服务端将剩余音频返回，随后关闭连接。

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        <strong>event\_id</strong>`string`<strong>（必选）</strong>

        客户端生成的唯一事件ID。在单次WebSocket连接会话中必须保持唯一。强烈建议使用 UUID（通用唯一标识符）。

        <strong>type</strong>`string`<strong>（必选）</strong>

        事件类型，固定为`session.finish`。
      </td>

      <td>
        ```json
        {
          "event_id": "event_2239",
          "type": "session.finish"
        }
        ```
      </td>
    </tr>
  </tbody>
</table>

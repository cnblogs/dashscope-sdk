> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# Multimodal-Embedding API详情

> 多模态向量模型将文本、图像和视频转换为同一语义空间中的向量表示，支持跨模态检索、内容分类和语义相似度计算。

<meta property="og:title" content="Multimodal-Embedding API详情" />

## 核心能力 <span id="a6c4aedaf8fee" />

- <strong>跨模态检索</strong>：以文搜图、以图搜视频、以图搜图等跨模态语义搜索。
- <strong>语义相似度计算</strong>：在统一向量空间中衡量不同模态内容之间的语义相似性。
- <strong>内容分类与聚类</strong>：基于语义向量进行智能分组、打标和聚类分析。

> <strong>关键特性</strong> ：所有模态（文本、图片、视频）的向量均位于同一语义空间，可通过余弦相似度等方法直接进行跨模态匹配与比较。模型选型和使用方法详见 [文本与多模态向量化](/zh/model-studio/embedding) 。

## 向量类型说明 <span id="b8c4d9e023wuz" />

多模态向量模型支持两种向量生成方式：

- <strong>多模态独立向量</strong>：为 contents 中的每个输入（文本、图片、视频、多图）分别生成独立向量。例如，输入 1 段文本和 1 张图片，返回 2 个独立向量。适用于逐项对比不同内容的场景，如以图搜图、以文搜图。
- <strong>多模态融合向量</strong>：将 contents 中的所有输入融合为 1 个向量，实现跨模态综合语义表征。适用于需要整体理解多模态内容的场景，如将商品图片和描述文本融合为统一表征进行检索。`qwen3-vl-embedding` 通过设置 `enable_fusion=true` 开启融合模式；`tongyi-embedding-vision-plus-2026-03-06` 和 `tongyi-embedding-vision-flash-2026-03-06` 通过将 text、image、video 放在同一个 content 对象中实现融合。融合向量支持以下组合：

  - 文本 + 图片融合
  - 文本 + 视频融合
  - 多图 + 文本融合（传入多个 image 条目）
  - 图片 + 视频 + 文本混合融合

> `qwen2.5-vl-embedding` 仅支持融合向量，不支持独立向量。 `tongyi-embedding-vision-plus` 和 `tongyi-embedding-vision-flash` 仅支持独立向量。 `tongyi-embedding-vision-plus-2026-03-06` 和 `tongyi-embedding-vision-flash-2026-03-06` 同时支持独立向量和融合向量，融合向量通过将 text、image、video 放在同一个 content 对象中实现。

模型介绍、选型建议和使用方法，请参考[文本与多模态向量化](/zh/model-studio/embedding)。

## 模型概览 <span id="3ef850b9cfgvv" />

<Tabs>
  <Tab title="北京">
    <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "14.3%" }} /><col style={{ width: "14.3%" }} /><col style={{ width: "14.3%" }} /><col style={{ width: "14.3%" }} /><col style={{ width: "14.3%" }} /><col style={{ width: "14.3%" }} /><col style={{ width: "14.2%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>模型名称</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>向量维度</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>文本长度限制</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>图片大小限制</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>视频大小限制</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>单价（每千输入Token）</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>免费额度</strong><a href="/zh/model-studio/new-free-quota#977b13081ab56">（注）</a></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>qwen3-vl-embedding</p></td><td style={{ verticalAlign: "top" }}><p>2560（默认）, 2048, 1536, 1024, 768, 512, 256</p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>32,000 Token</p></td><td style={{ verticalAlign: "top" }}><p>单张大小不超过<strong>10 MB</strong></p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>视频文件大小不超过 <strong>50 MB</strong></p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>图片/视频：0.0018元</p> <p>文本：0.0007元</p></td><td style={{ verticalAlign: "top" }} rowSpan={7}><p>100万Token</p> <p>有效期：自开通百炼/模型发布/申请通过之日起90天（以较晚者为准）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>qwen2.5-vl-embedding</p></td><td style={{ verticalAlign: "top" }}><p>2048, 1024（默认）, 768, 512</p></td><td style={{ verticalAlign: "top" }}><p>单张大小不超过<strong>5 MB</strong></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tongyi-embedding-vision-plus-2026-03-06</p></td><td style={{ verticalAlign: "top" }}><p>1152（默认）, 1024, 512, 256, 128, 64</p></td><td style={{ verticalAlign: "top" }} rowSpan={4}><p>1,024 Token</p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>建议单张大小不超过<strong>5 MB</strong>，最大<strong>10 MB。</strong>支持多图，最多支持输入<strong>64张</strong></p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>视频文件大小不超过 <strong>50 MB</strong></p> <p>且编码类型为H.264/H.265</p></td><td style={{ verticalAlign: "top" }}><p>0.0005元</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tongyi-embedding-vision-flash-2026-03-06</p></td><td style={{ verticalAlign: "top" }}><p>768（默认）, 512, 256, 128, 64</p></td><td style={{ verticalAlign: "top" }}><p>0.00015元</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tongyi-embedding-vision-plus</p></td><td style={{ verticalAlign: "top" }}><p>1152</p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>单张大小不超过<strong>3 MB</strong>。支持多图，最多支持输入<strong>8张</strong></p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>视频文件大小不超过 <strong>10 MB</strong></p></td><td style={{ verticalAlign: "top" }}><p>0.0005元</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tongyi-embedding-vision-flash</p></td><td style={{ verticalAlign: "top" }}><p>768</p></td><td style={{ verticalAlign: "top" }}><p>0.00015元</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>multimodal-embedding-v1</p></td><td style={{ verticalAlign: "top" }}><p>1,024</p></td><td style={{ verticalAlign: "top" }}><p>512 Token</p></td><td style={{ verticalAlign: "top" }}><p>单张大小不超过<strong>3 MB</strong></p></td><td style={{ verticalAlign: "top" }}><p>视频文件大小不超过 <strong>10 MB</strong></p></td><td style={{ verticalAlign: "top" }}><p>图片/视频：0.0009 元</p> <p>文本：0.0007 元</p></td></tr></tbody></table>
  </Tab>

  <Tab title="新加坡">
    <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "23.796549%" }} /><col style={{ width: "11.080836%" }} /><col style={{ width: "11.625795%" }} /><col style={{ width: "18.80109%" }} /><col style={{ width: "18.80109%" }} /><col style={{ width: "15.894641%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>模型名称</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>向量维度</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>文本长度限制</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>图片大小限制</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>视频大小限制</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>单价（每千输入Token）</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p>tongyi-embedding-vision-plus</p></td><td style={{ verticalAlign: "top" }}><p>1152</p></td><td style={{ verticalAlign: "top" }}><p>1,024 Token</p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>最多 <strong>8 张</strong>且单张大小不超过<strong>3 MB</strong></p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>视频文件大小不超过 <strong>10 MB</strong></p></td><td style={{ verticalAlign: "top" }}><p>0.0005元</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tongyi-embedding-vision-flash</p></td><td style={{ verticalAlign: "top" }}><p>768</p></td><td style={{ verticalAlign: "top" }}><p>1,024 Token</p></td><td style={{ verticalAlign: "top" }}><p>0.00015元</p></td></tr></tbody></table>
  </Tab>
</Tabs>

### 输入格式与语种限制： <span id="f1751ee952ly4" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "20.04%" }} />

    <col style={{ width: "20.04%" }} />

    <col style={{ width: "20.04%" }} />

    <col style={{ width: "19.94%" }} />

    <col style={{ width: "19.94%" }} />
  </colgroup>

  <thead>
    <tr>
      <th style={{ verticalAlign: "top" }} colSpan={5}>
        <strong>多模态融合向量模型</strong>
      </th>
    </tr>

    <tr>
      <th style={{ verticalAlign: "top" }}>
        <strong>模型</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>文本</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>图片</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>视频</strong>
      </th>

      <th style={{ verticalAlign: "top" }}>
        <strong>单次请求条数</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        qwen3-vl-embedding
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持中、英、日、韩、法、德等33种主流语言

        <Accordion title="所有支持语言">
          中文、日语、韩语、印尼语、越南语、泰语、英语、法语、德语、俄语、葡萄牙语、西班牙语、意大利语、瑞典语、丹麦语、捷克语、挪威语、荷兰语、芬兰语、土耳其语、波兰语、斯瓦希里语、罗马尼亚语、塞尔维亚语、希腊语、哈萨克语、乌兹别克语、宿务语、阿拉伯语、乌尔都语、波斯语、印地语 / 天城语、希伯来语。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={2}>
        JPEG, PNG, WEBP, BMP, TIFF, ICO, DIB, ICNS, SGI（支持URL或Base64）
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={2}>
        MP4, AVI, MOV（仅支持URL）
      </td>

      <td style={{ verticalAlign: "top" }}>
        一次请求中传入内容元素总数不超过 20。图片数量不超过10，视频数量不超过1。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        qwen2.5-vl-embedding
      </td>

      <td style={{ verticalAlign: "top" }}>
        支持中、英、日、韩、法、德等11种主流语言

        <Accordion title="所有支持语言">
          中文、英语、日语、韩语、法语、德语、俄语、葡萄牙语、西班牙语、意大利语、印尼语
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        一次请求内，图片、文本、视频、融合对象每种类型最多出现 1 次。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }} colSpan={5}>
        <strong>多模态向量模型</strong>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>模型</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>文本</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>图片</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>视频</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>单次请求条数</strong>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        tongyi-embedding-vision-plus-2026-03-06
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={2}>
        支持中、英、日、韩等超30种主流语言

        <Accordion title="所有支持语言">
          中文、日语、韩语、印尼语、越南语、泰语、英语、法语、德语、俄语、葡萄牙语、西班牙语、意大利语、瑞典语、丹麦语、捷克语、挪威语、荷兰语、芬兰语、土耳其语、波兰语、斯瓦希里语、罗马尼亚语、塞尔维亚语、希腊语、哈萨克语、乌兹别克语、宿务语、阿拉伯语、乌尔都语、波斯语、印地语 / 天城语、希伯来语。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={2}>
        JPEG, PNG, WEBP, BMP, TIFF, ICO, DIB, ICNS, SGI（支持URL或Base64）
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={2}>
        MP4, MPEG, MOV, MPG, WEBM, AVI, FLV, MKV（仅支持URL）
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={2}>
        一次请求中传入内容元素总数不超过 20，单次图片总数不超过64，视频数量不超过8。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        tongyi-embedding-vision-flash-2026-03-06
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        tongyi-embedding-vision-plus
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={2}>
        中文与英文
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={2}>
        JPG, PNG, BMP (支持URL或Base64)
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={3}>
        MP4, MPEG, MOV, MPG, WEBM, AVI, FLV, MKV（仅支持URL）
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={2}>
        暂无传入内容元素数量限制，输入内容Token数不超过单批次处理Token数量上限即可。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        tongyi-embedding-vision-flash
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        multimodal-embedding-v1
      </td>

      <td style={{ verticalAlign: "top" }}>
        中文与英文
      </td>

      <td style={{ verticalAlign: "top" }}>
        JPG, PNG, BMP (支持URL或Base64)
      </td>

      <td style={{ verticalAlign: "top" }}>
        一次请求中传入内容元素总数不超过 20；图片、视频各最多 1 条，文本最多 20 条，共享总条数上限。
      </td>
    </tr>
  </tbody>
</table>

> 所有模型均支持 text、image、video 三种输入类型及其组合。 `tongyi-embedding-vision-plus` 、 `tongyi-embedding-vision-flash` 、`tongyi-embedding-vision-plus-2026-03-06` 和 `tongyi-embedding-vision-flash-2026-03-06` 额外支持 `multi_images` 多图序列输入。

### 模型能力对照 <span id="mt02a3b4c5d6e" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /><col style={{ width: "20%" }} /></colgroup><tbody><tr><td style={{ verticalAlign: "top" }}><p><strong>模型</strong></p></td><td style={{ verticalAlign: "top" }}><p><strong>默认维度</strong></p></td><td style={{ verticalAlign: "top" }}><p><strong>向量类型</strong></p></td><td style={{ verticalAlign: "top" }}><p><strong>支持的输入</strong></p></td><td style={{ verticalAlign: "top" }}><p><strong>说明</strong></p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>qwen3-vl-embedding</p></td><td style={{ verticalAlign: "top" }}><p>2560</p></td><td style={{ verticalAlign: "top" }}><p>独立 / 融合</p></td><td style={{ verticalAlign: "top" }}><p>text、image、video、多个 image 条目</p></td><td style={{ verticalAlign: "top" }}><p>通过 <code>enable\_fusion</code> 参数开启融合模式，可将多模态输入融合为 1 个向量</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>qwen2.5-vl-embedding</p></td><td style={{ verticalAlign: "top" }}><p>1024</p></td><td style={{ verticalAlign: "top" }}><p>仅融合</p></td><td style={{ verticalAlign: "top" }}><p>text、image、video</p></td><td style={{ verticalAlign: "top" }}><p>始终返回 1 个融合向量，不支持独立向量，不支持多图输入</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tongyi-embedding-vision-plus-2026-03-06</p></td><td style={{ verticalAlign: "top" }}><p>1152</p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>独立 / 融合</p></td><td style={{ verticalAlign: "top" }} rowSpan={4}><p>text、image、video、multi\_images</p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>基于 Qwen3 底座，支持多分辨率、30+ 语言、融合向量</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tongyi-embedding-vision-flash-2026-03-06</p></td><td style={{ verticalAlign: "top" }}><p>768</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tongyi-embedding-vision-plus</p></td><td style={{ verticalAlign: "top" }}><p>1152</p></td><td style={{ verticalAlign: "top" }} rowSpan={3}><p>仅独立</p></td><td style={{ verticalAlign: "top" }} rowSpan={2}><p>支持 multi\_images 多图序列（最多 8 张）</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>tongyi-embedding-vision-flash</p></td><td style={{ verticalAlign: "top" }}><p>768</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p>multimodal-embedding-v1</p></td><td style={{ verticalAlign: "top" }}><p>1024</p></td><td style={{ verticalAlign: "top" }}><p>text、image、video</p></td><td style={{ verticalAlign: "top" }}><p>不支持 dimension 参数，固定 1024 维</p></td></tr></tbody></table>

## 前提条件 <span id="f8fe5c677bv6q" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。如果通过SDK调用，还需要[安装DashScope SDK](/zh/model-studio/install-sdk)。请将示例代码中的 `DASHSCOPE_API_HOST` 替换为获取的 API Host。

## HTTP调用 <span id="7f18c3fe662s1" />

`POST https://dashscope.aliyuncs.com/api/v1/services/embeddings/multimodal-embedding/multimodal-embedding`

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "54.58%" }} />

    <col style={{ width: "45.42%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ### 请求 <span id="5043fa882c26r" />
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={7}>
        <Tabs>
          <Tab title="多模态独立向量">
            > 以下示例使用 `tongyi-embedding-vision-plus` 模型生成独立向量（每个输入各自生成 1 个向量），也可替换为其他模型名称。其中 `multi_images` 类型仅 `tongyi-embedding-vision-plus` 和 `tongyi-embedding-vision-flash` 支持。`qwen3-vl-embedding` 额外支持融合向量模式，通过设置 `enable_fusion=true` 开启，详见"多模态融合向量"标签页。

            ```bash
            curl --silent --location --request POST 'https://dashscope.aliyuncs.com/api/v1/services/embeddings/multimodal-embedding/multimodal-embedding' \
                --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                --header 'Content-Type: application/json' \
                --data '{
                    "model": "tongyi-embedding-vision-plus",
                    "input": {
                        "contents": [
                            {"text": "多模态向量模型"},
                            {"image": "https://img.alicdn.com/imgextra/i3/O1CN01rdstgY1uiZWt8gqSL_!!6000000006071-0-tps-1970-356.jpg"},
                            {"video": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4"},
                            {"multi_images": [
                                "https://img.alicdn.com/imgextra/i2/O1CN019eO00F1HDdlU4Syj5_!!6000000000724-2-tps-2476-1158.png",
                                "https://img.alicdn.com/imgextra/i2/O1CN01dSYhpw1nSoamp31CD_!!6000000005089-2-tps-1765-1639.png"
                                ]
                              }
                        ]
                    }
                }'
            ```
          </Tab>

          <Tab title="多模态融合向量">
            > `qwen3-vl-embedding`支持融合向量生成，通过设置 `enable_fusion=true` 将所有输入融合为 1 个向量。支持文本+图片、文本+视频、多图+文本、图片+视频+文本等多种融合组合。以下示例展示多图+视频+文本的混合融合。

            ```bash
            curl --location 'https://dashscope.aliyuncs.com/api/v1/services/embeddings/multimodal-embedding/multimodal-embedding' \
                --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                --header 'Content-Type: application/json' \
                --data '{
                    "model": "qwen3-vl-embedding",
                    "input": {
                        "contents": [
                            {"text": "商品描述文本"},
                            {"image": "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png"},
                            {"image": "https://img.alicdn.com/imgextra/i3/O1CN01rdstgY1uiZWt8gqSL_!!6000000006071-0-tps-1970-356.jpg"},
                            {"video": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4"}
                        ]
                    },
                    "parameters": {
                        "enable_fusion": true
                    }
                }'
            ```
          </Tab>

          <Tab title="2026-03-06 快照版本示例">
            > `tongyi-embedding-vision-plus-2026-03-06` 和 `tongyi-embedding-vision-flash-2026-03-06` 是基于 Qwen3 底座的新版模型，支持 `res_level`（多分辨率）和 `max_video_frames`（视频帧数）参数，同时支持独立向量和融合向量生成。

            ```bash
            curl --silent --location --request POST 'https://dashscope.aliyuncs.com/api/v1/services/embeddings/multimodal-embedding/multimodal-embedding' \
                --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                --header 'Content-Type: application/json' \
                --data '{
                    "model": "tongyi-embedding-vision-plus-2026-03-06",
                    "input": {
                        "contents": [
                            {"text": "这是1个视觉多模态表征模型"},
                            {"image": "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png"},
                            {"video": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4"},
                            {"multi_images": [
                                "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png",
                                "https://img.alicdn.com/imgextra/i3/O1CN01rdstgY1uiZWt8gqSL_!!6000000006071-0-tps-1970-356.jpg"
                            ]}
                        ]
                    },
                    "parameters": {
                        "dimension": 1152,
                        "res_level": 1,
                        "max_video_frames": 64
                    }
                }'
            ```

            以下示例展示 2026-03-06 版本的<strong>融合向量</strong>用法：将 text、image、video 放在同一个 content 对象中，模型会将所有输入融合编码为 1 个向量（type 为 `fused`）。

            ```bash
            curl --silent --location --request POST 'https://dashscope.aliyuncs.com/api/v1/services/embeddings/multimodal-embedding/multimodal-embedding' \
                --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
                --header 'Content-Type: application/json' \
                --data '{
                    "model": "tongyi-embedding-vision-plus-2026-03-06",
                    "input": {
                        "contents": [
                            {
                                "text": "这是1个视觉多模态表征模型",
                                "image": "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png",
                                "video": "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4"
                            }
                        ]
                    },
                    "parameters": {
                        "dimension": 1152
                    }
                }'
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求头（Headers） <span id="2319e756381fd" />
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>Content-Type</strong>`string`<strong>（必选）</strong>

        请求内容类型。此参数必须设置为`application/json`。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>Authorization</strong>`string`<strong>（必选）</strong>

        请求身份认证。接口使用阿里云百炼API Key进行身份认证。示例值：Bearer sk-xxxx。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求体（Request Body） <span id="66a778d4fbfub" />
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>model</strong>`string`<strong>（必选）</strong>

        模型名称。设置为[模型概览](/zh/model-studio/multimodal-embedding-api-reference#3ef850b9cfgvv)中的模型名称。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>input</strong>`object`<strong>（必选）</strong>

        输入内容。

        <Accordion title="属性" defaultOpen>
          contents`array`<strong>（必选）</strong>

          待处理的内容列表。每个元素是一个字典或者字符串，用于指定内容的类型和值。格式为\{"模态类型": "输入字符串或图像、视频url"}。支持`text`, `image`, `video`和`multi_images`四种模态类型。

          > `qwen3-vl-embedding` 同时支持融合向量和独立向量生成。在多模态独立向量的基础上增加 bool 类型字段 `enable_fusion`，当 `enable_fusion=true` 时返回融合向量。`qwen2.5-vl-embedding` 仅支持融合向量，不支持独立向量。`tongyi-embedding-vision-plus-2026-03-06` 和 `tongyi-embedding-vision-flash-2026-03-06` 同时支持独立向量和融合向量，通过将 text、image、video 放在同一个 content 对象中生成融合向量（不使用 enable\_fusion 参数）。

          - 文本：key为`text`。value为字符串形式。也可不通过dict直接传入字符串。
          - 图片：key为`image`。value可以是公开可访问的URL，或Base64编码的Data URI。Base64格式为 `data:image/{format};base64,{data}`，其中 `{format}` 是图片格式（如 `jpeg`, `png`），`{data}` 是Base64编码字符串。
          - 多图片：仅`tongyi-embedding-vision-plus`、`tongyi-embedding-vision-flash`、`tongyi-embedding-vision-plus-2026-03-06`与`tongyi-embedding-vision-flash-2026-03-06`模型支持此类型。key为`multi_images`，value是多图序列列表，每条为一个图片，格式要求如上方所示。
          - 视频：key为`video`，value必须是公开可访问的URL。
        </Accordion>

        <strong>parameters</strong> `object` （可选）

        向量处理参数。HTTP调用需包装在parameters对象中，SDK调用可直接使用以下参数。

        <Accordion title="属性" defaultOpen>
          <strong>output\_type</strong> `string` （可选）

          用户指定输出向量表示格式，目前仅支持dense。

          <strong>dimension</strong> `integer` （可选）

          用于用户指定输出向量维度。不同模型支持的值不同：

          - `qwen3-vl-embedding` 支持 2560、2048、1536、1024、768、512、256，默认值为 2560；
          - `qwen2.5-vl-embedding` 支持 2048、1024、768、512，默认值为 1024；
          - `tongyi-embedding-vision-plus` 不支持此参数，固定返回 1152 维向量。
          - `tongyi-embedding-vision-flash` 不支持此参数，固定返回 768 维向量。
          - `tongyi-embedding-vision-plus-2026-03-06` 支持 64、128、256、512、1024、1152，默认值为 1152；
          - `tongyi-embedding-vision-flash-2026-03-06` 支持 64、128、256、512、768，默认值为 768；
          - `multimodal-embedding-v1` 不支持此参数，固定返回 1024 维向量。

          <strong>fps</strong> `float` （可选）

          控制视频的帧数，比例越小，实际抽取的帧数越少，范围为 \[0,1]。默认值为1.0。

          <strong>instruct</strong> `string` （可选）

          添加自定义任务说明，可用于指导模型理解查询意图。建议使用英文撰写，通常可带来约 1%–5% 的效果提升。

          <strong>enable\_fusion</strong> `bool` （可选）

          是否生成融合向量。仅 `qwen3-vl-embedding` 模型支持该参数。设置为 `true` 时，将 contents 中的所有多模态内容融合为 1 个向量；默认为 `false`，各模态独立生成向量。融合向量支持文本+图片、文本+视频、多图+文本（传入多个 image 条目）、图片+视频+文本等组合，适用于需要综合理解多模态内容的检索场景。

          > `tongyi-embedding-vision-plus-2026-03-06` 和 `tongyi-embedding-vision-flash-2026-03-06` 不使用该参数，而是通过将 text、image、video 放在同一个 content 对象中来生成融合向量。

          <strong>res\_level</strong> `integer` （可选）

          指定输入分辨率档位，支持设置 0/1/2/3 四档，对应的单图 token 分别是 127/402/578/1026，默认值为 1（402 token）。仅 `tongyi-embedding-vision-plus-2026-03-06` 和 `tongyi-embedding-vision-flash-2026-03-06` 模型支持该参数。对于 IPC/自驾/视觉文字等图像分辨率敏感的场景，高分辨率（res\_level=3）可提升 5%-10% 效果。

          <strong>max\_video\_frames</strong> `integer` （可选）

          控制视频的最大采样帧数上限，最大不超过 64，默认值为 8。仅 `tongyi-embedding-vision-plus-2026-03-06` 和 `tongyi-embedding-vision-flash-2026-03-06` 模型支持该参数。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

<table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}>
  <colgroup>
    <col style={{ width: "54.58%" }} />

    <col style={{ width: "45.42%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        ### 响应 <span id="9ea3a66e2a0uv" />
      </td>

      <td style={{ verticalAlign: "top" }} rowSpan={6}>
        <Tabs>
          <Tab title="成功响应">
            ```json expandable
            {
                "output": {
                    "embeddings": [
                        {
                            "index": 0,
                            "embedding": [
                                -0.026611328125,
                                -0.016571044921875,
                                -0.02227783203125,
                                ...
                            ],
                            "type": "text"
                        },
                        {
                            "index": 1,
                            "embedding": [
                                0.051544189453125,
                                0.007717132568359375,
                                0.026611328125,
                                ...
                            ],
                            "type": "image"
                        },
                        {
                            "index": 2,
                            "embedding": [
                                -0.0217437744140625,
                                -0.016448974609375,
                                0.040679931640625,
                                ...
                            ],
                            "type": "video"
                        }
                    ]
                },
                "usage": {
                    "input_tokens": 903,
                    "input_tokens_details": {
                        "image_tokens": 896,
                        "text_tokens": 7
                    },
                    "output_tokens": 3,
                    "total_tokens": 906
                },
                "request_id": "1fff9502-a6c5-9472-9ee1-73930fdd04c5"
            }
            ```

            <Note>
              不同模型返回的 `usage` 字段存在差异，请参考以下说明：

              - `tongyi-embedding-vision-*` 系列模型：返回 `input_tokens`（含文本和图片 Token 总和）、`input_tokens_details`（含 `image_tokens` 和 `text_tokens`）、`output_tokens`、`total_tokens`。以上响应示例对应此类模型。
              - `qwen3-vl-embedding`：仅返回 `input_tokens`（仅含文本 Token，包括系统模板 Token）、`image_tokens`、`total_tokens`（= `input_tokens` + `image_tokens`）。不返回 `input_tokens_details` 和 `output_tokens`。示例：
            </Note>

            ```json
            {
                "usage": {
                    "input_tokens": 43,
                    "image_tokens": 1247,
                    "total_tokens": 1290
                }
            }
            ```

            <Note>
              - `qwen2.5-vl-embedding`：仅返回 `input_tokens` 和 `image_tokens`，不返回 `total_tokens`、`input_tokens_details` 和 `output_tokens`。
              - `multimodal-embedding-v1`：返回 `input_tokens`、`image_tokens`、`image_count` 和 `duration`，不返回 `total_tokens`、`input_tokens_details` 和 `output_tokens`。
            </Note>
          </Tab>

          <Tab title="异常响应">
            ```json
            {
                "code":"InvalidApiKey",
                "message":"Invalid API-key provided.",
                "request_id":"fb53c4ec-1c12-4fc4-a580-cdb7c3261fc1"
            }
            ```

            本 API 常见的错误码及排查方向如下。完整错误码请参见[通用错误码](/zh/model-studio/error-code)。

            <table style={{ display: "table", tableLayout: "fixed", width: "100%", overflowWrap: "anywhere" }}><colgroup><col style={{ width: "18%" }} /><col style={{ width: "12%" }} /><col style={{ width: "35%" }} /><col style={{ width: "35%" }} /></colgroup><thead><tr><th style={{ verticalAlign: "top" }}><p><strong>错误码</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>HTTP 状态码</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>触发条件</strong></p></th><th style={{ verticalAlign: "top" }}><p><strong>排查建议</strong></p></th></tr></thead><tbody><tr><td style={{ verticalAlign: "top" }}><p><code>InvalidApiKey</code></p></td><td style={{ verticalAlign: "top" }}><p>401</p></td><td style={{ verticalAlign: "top" }}><p>API Key 无效或已过期，返回 <code>Invalid API-key provided.</code></p></td><td style={{ verticalAlign: "top" }}><p>确认 <code>Authorization</code> 请求头为 <code>Bearer sk-xxx</code> 格式，且使用的是当前账号下有效的 API Key。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><code>BadRequest.EmptyModel</code></p></td><td style={{ verticalAlign: "top" }}><p>400</p></td><td style={{ verticalAlign: "top" }}><p>请求中缺少 <code>model</code> 字段，返回 <code>Required parameter model missing from request.</code></p></td><td style={{ verticalAlign: "top" }}><p>在请求体中补充 <code>model</code> 字段，取值使用本文模型列表中的模型名称。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><code>InvalidParameter</code></p></td><td style={{ verticalAlign: "top" }}><p>400</p></td><td style={{ verticalAlign: "top" }}><p><code>model</code> 取值不存在，返回 <code>Model not exist.</code></p></td><td style={{ verticalAlign: "top" }}><p>核对 <code>model</code> 拼写，并确认该模型在当前地域的模型列表中已列出。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><code>InvalidParameter</code></p></td><td style={{ verticalAlign: "top" }}><p>400</p></td><td style={{ verticalAlign: "top" }}><p><code>input.contents</code> 缺失或为空数组，返回 <code>input can not be empty: input.contents</code> 或 <code>contents input can not be empty: input.contents</code></p></td><td style={{ verticalAlign: "top" }}><p>确认 <code>input.contents</code> 存在且为非空数组，数组中每个元素包含有效的 <code>text</code>、<code>image</code> 或 <code>video</code> 键。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><code>InvalidParameter</code></p></td><td style={{ verticalAlign: "top" }}><p>400</p></td><td style={{ verticalAlign: "top" }}><p><code>dimension</code> 取值不在所选模型支持的向量维度范围内。</p></td><td style={{ verticalAlign: "top" }}><p>按本文模型列表中该模型支持的向量维度取值；<code>multimodal-embedding-v1</code> 不支持 <code>dimension</code> 参数，固定为 1024 维。</p></td></tr><tr><td style={{ verticalAlign: "top" }}><p><code>InternalError.Algo</code></p></td><td style={{ verticalAlign: "top" }}><p>500</p></td><td style={{ verticalAlign: "top" }}><p>请求体结构不完整或 <code>input.contents</code> 元素格式异常，导致服务端处理失败。</p></td><td style={{ verticalAlign: "top" }}><p>检查请求体结构，确认 <code>input</code> 及 <code>input.contents</code> 字段存在且元素格式符合本文请求参数说明；结构无误仍持续报错时，携带 <code>request\_id</code> 联系技术支持。</p></td></tr></tbody></table>
          </Tab>
        </Tabs>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>output</strong>`object`

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>embeddings</strong>`array`

          向量结果列表，每个对象对应输入列表中的一个元素。

          <Accordion title="属性" defaultOpen>
            <strong>index</strong>`int`

            结果在输入列表中的索引。

            <strong>embedding</strong>`array`

            生成的向量数组，维度取决于模型及 `dimension` 参数设置。

            <strong>type</strong>`string`

            结果对应的输入类型。`text`、`image`、`video`、`multi_images` 分别对应文本、图片、视频、多图输入。以下为特殊类型： `fused` 为 `tongyi-embedding-vision-plus-2026-03-06` 和 `tongyi-embedding-vision-flash-2026-03-06` 模型返回的融合向量类型；`fusion` 为 `qwen3-vl-embedding`或`qwen2.5-vl-embedding`模型在融合向量模式下返回的类型；`vl` 为 `qwen3-vl-embedding` 模型在独立向量模式下返回的类型。
          </Accordion>
        </Accordion>
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>request\_id</strong>`string`

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>code</strong>`string`

        请求失败的错误码。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>message</strong>`string`

        请求失败的详细信息。请求成功时不会返回此参数，详情请参见[错误码](/zh/model-studio/error-code)。
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        <strong>usage</strong>`object`

        输出信息统计。

        <Accordion title="属性" defaultOpen>
          <strong>input\_tokens</strong>`int`

          本次请求输入内容的 Token 数目。对于 `qwen3-vl-embedding` 和 `qwen2.5-vl-embedding` 模型，该值仅包含文本 Token（含系统模板 Token），不包含图片/视频 Token；对于 `tongyi-embedding-vision-*` 系列模型，该值包含文本和图片/视频 Token 的总和。

          <strong>input\_tokens\_details</strong>`object`

          输入 Token 的详细分类信息。仅 `tongyi-embedding-vision-*` 系列模型返回此字段，`qwen3-vl-embedding`、`qwen2.5-vl-embedding` 和 `multimodal-embedding-v1` 不返回此字段。

          <Accordion title="属性" defaultOpen>
            <strong>image\_tokens</strong>`int`

            输入内容中图片或视频等<strong>视觉部分</strong>消耗的 Token 数量，<strong>不包含文本</strong>（文本部分见 `text_tokens`）。图片消耗的 Token 数量与输入图片的分辨率有关，分辨率越高消耗的 Token 越多；若输入为视频，系统会先对视频抽帧，再基于抽帧结果计算 Token。

            <strong>text\_tokens</strong>`int`

            输入内容中<strong>文本部分</strong>消耗的 Token 数量（不包含图片或视频等视觉部分）。
          </Accordion>

          <strong>output\_tokens</strong>`int`

          本次请求输出的 Token 数目。仅 `tongyi-embedding-vision-*` 系列模型返回此字段，其他模型不返回此字段。

          <strong>total\_tokens</strong>`int`

          输入与输出的 Token 总数。对于 `qwen3-vl-embedding` 模型，`total_tokens` = `input_tokens` + `image_tokens`。仅 `qwen3-vl-embedding` 和 `tongyi-embedding-vision-*` 系列模型返回此字段，`qwen2.5-vl-embedding` 和 `multimodal-embedding-v1` 不返回此字段。

          <strong>image\_tokens</strong>`int`

          本次请求输入的图片或视频等<strong>视觉部分</strong>消耗的 Token 数量（<strong>不包含文本</strong>）。图片消耗的 Token 数量与输入图片的分辨率有关；系统会对输入视频进行抽帧处理，帧数上限受系统配置控制，随后基于处理结果计算 Token。仅 `qwen3-vl-embedding`、`qwen2.5-vl-embedding` 和 `multimodal-embedding-v1` 返回此字段（作为顶层字段），`tongyi-embedding-vision-*` 系列模型的图片 Token 包含在 `input_tokens_details.image_tokens` 中。

          <strong>image\_count</strong>`int`

          本次请求输入的图片数量。仅 `multimodal-embedding-v1` 返回此字段。

          <strong>duration</strong>`int`

          本次请求输入的视频时长（秒）。仅 `multimodal-embedding-v1` 返回此字段。
        </Accordion>
      </td>
    </tr>
  </tbody>
</table>

## SDK使用 <span id="0b996e6ff5j2a" />

> SDK 的 input 参数对应HTTP请求体中的 input.contents，两者结构 <strong>不一致</strong> 。

### 代码示例 <span id="398d2f2a026yi" />

<Tabs>
  <Tab title="图片向量化示例">
    <Tabs>
      <Tab title="使用图片URL">
        ```python
        import dashscope
        import json
        from http import HTTPStatus
        # 实际使用中请将url地址替换为您的图片url地址
        image = "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png"
        input = [{'image': image}]
        # 调用模型接口
        resp = dashscope.MultiModalEmbedding.call(
            model="tongyi-embedding-vision-plus",
            input=input
        )

        if resp.status_code == HTTPStatus.OK:
            result = {
                "status_code": resp.status_code,
                "request_id": getattr(resp, "request_id", ""),
                "code": getattr(resp, "code", ""),
                "message": getattr(resp, "message", ""),
                "output": resp.output,
                "usage": resp.usage
            }
            print(json.dumps(result, ensure_ascii=False, indent=4))
        ```
      </Tab>

      <Tab title="使用本地图片">
        将本地图片转换为 Base64 格式后进行向量化：

        ```python expandable
        import dashscope
        import base64
        import json
        from http import HTTPStatus
        # 读取图片并转换为Base64,实际使用中请将xxx.png替换为您的图片文件名或路径
        image_path = "xxx.png"
        with open(image_path, "rb") as image_file:
            # 读取文件并转换为Base64
            base64_image = base64.b64encode(image_file.read()).decode('utf-8')
        # 设置图像格式
        image_format = "png"  # 根据实际情况修改，比如jpg、bmp 等
        image_data = f"data:image/{image_format};base64,{base64_image}"
        # 输入数据
        input = [{'image': image_data}]

        # 调用模型接口
        resp = dashscope.MultiModalEmbedding.call(
            model="tongyi-embedding-vision-plus",
            input=input
        )
        if resp.status_code == HTTPStatus.OK:
            result = {
                "status_code": resp.status_code,
                "request_id": getattr(resp, "request_id", ""),
                "code": getattr(resp, "code", ""),
                "message": getattr(resp, "message", ""),
                "output": resp.output,
                "usage": resp.usage
            }
            print(json.dumps(result, ensure_ascii=False, indent=4))
        ```
      </Tab>
    </Tabs>
  </Tab>

  <Tab title="视频向量化示例">
    > 多模态向量化模型目前仅支持以URL形式输入视频文件，暂不支持直接传入本地视频。

    ```python
    import dashscope
    import json
    from http import HTTPStatus
    # 实际使用中请将url地址替换为您的视频url地址
    video = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4"
    input = [{'video': video}]
    # 调用模型接口
    resp = dashscope.MultiModalEmbedding.call(
        model="tongyi-embedding-vision-plus",
        input=input
    )

    if resp.status_code == HTTPStatus.OK:
        result = {
            "status_code": resp.status_code,
            "request_id": getattr(resp, "request_id", ""),
            "code": getattr(resp, "code", ""),
            "message": getattr(resp, "message", ""),
            "output": resp.output,
            "usage": resp.usage
        }
        print(json.dumps(result, ensure_ascii=False, indent=4))
    ```
  </Tab>

  <Tab title="文本向量化示例">
    ```python
    import dashscope
    import json
    from http import HTTPStatus

    text = "通用多模态表征模型示例"
    input = [{'text': text}]
    # 调用模型接口
    resp = dashscope.MultiModalEmbedding.call(
        model="tongyi-embedding-vision-plus",
        input=input
    )

    if resp.status_code == HTTPStatus.OK:
        result = {
            "status_code": resp.status_code,
            "request_id": getattr(resp, "request_id", ""),
            "code": getattr(resp, "code", ""),
            "message": getattr(resp, "message", ""),
            "output": resp.output,
            "usage": resp.usage
        }
        print(json.dumps(result, ensure_ascii=False, indent=4))
    ```
  </Tab>

  <Tab title="融合向量化示例">
    ```python expandable
    import dashscope
    import json
    import os
    from http import HTTPStatus

    # 多模态融合向量：将文本、图片、视频融合成一个融合向量
    # 适用于跨模态检索、图搜等场景
    text = "这是一段测试文本，用于生成多模态融合向量"
    image = "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png"
    video = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4"

    # 输入包含文本、图片、视频，通过 enable_fusion 参数生成融合向量
    input_data = [
        {"text": text},
        {"image": image},
        {"video": video}
    ]

    # 使用 qwen3-vl-embedding 生成融合向量
    resp = dashscope.MultiModalEmbedding.call(
        # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx",
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        model="qwen3-vl-embedding",
        input=input_data,
        enable_fusion=True,
        # 可选参数：指定向量维度（支持 2560、2048、1536、1024、768、512、256，默认 2560）
        # parameters={"dimension": 1024}
    )

    print(json.dumps(resp, ensure_ascii=False, indent=4))
    ```
  </Tab>

  <Tab title="多图融合向量化示例">
    使用 `qwen3-vl-embedding` 将多张图片与文本融合为 1 个向量。传入多个 `image` 条目即可实现多图融合，适用于商品多角度图片与描述文本的综合语义检索。

    ```python expandable
    import dashscope
    import json
    import os
    from http import HTTPStatus

    # 多图+文本融合向量：将多张商品图片和描述文本融合为 1 个向量
    # 适用于商品多角度图片+描述文本的综合语义检索
    text = "白色运动鞋，轻量透气，适合跑步和日常穿着"
    image1 = "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png"
    image2 = "https://img.alicdn.com/imgextra/i3/O1CN01rdstgY1uiZWt8gqSL_!!6000000006071-0-tps-1970-356.jpg"

    # 传入多个 image 条目实现多图融合，enable_fusion=True 将所有输入融合为 1 个向量
    input_data = [
        {"text": text},
        {"image": image1},
        {"image": image2}
    ]

    resp = dashscope.MultiModalEmbedding.call(
        # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx",
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        model="qwen3-vl-embedding",
        input=input_data,
        enable_fusion=True
    )

    print(json.dumps(resp, ensure_ascii=False, indent=4))
    ```
  </Tab>

  <Tab title="2026-03-06 快照版本示例">
    > 以下示例展示如何使用 `tongyi-embedding-vision-plus-2026-03-06` 模型，演示 `res_level` （多分辨率）和 `max_video_frames` （视频帧数）参数的使用。该模型基于 Qwen3 底座，支持 30+ 种语言，同时支持独立向量和融合向量生成。

    ```python expandable
    import dashscope
    import json
    import os
    from http import HTTPStatus

    # tongyi-embedding-vision-plus-2026-03-06 示例
    # 支持 res_level（多分辨率）和 max_video_frames（视频帧数）参数
    image = "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png"
    video = "https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250107/lbcemt/new+video.mp4"
    text = "这是1个视觉多模态表征模型"

    input_data = [
        {"text": text},
        {"image": image},
        {"video": video}
    ]

    # 调用 2026-03-06 快照版本模型
    resp = dashscope.MultiModalEmbedding.call(
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        model="tongyi-embedding-vision-plus-2026-03-06",
        input=input_data,
        dimension=1152,      # 支持 1152/1024/512/256/128/64
        res_level=1,         # 分辨率档位：0/1/2/3，默认 1
        max_video_frames=64  # 视频最大采样帧数，默认 8，最大 64
    )

    if resp.status_code == HTTPStatus.OK:
        result = {
            "status_code": resp.status_code,
            "request_id": getattr(resp, "request_id", ""),
            "output": resp.output,
            "usage": resp.usage
        }
        print(json.dumps(result, ensure_ascii=False, indent=4))
    ```

    2026-03-06 版本的<strong>融合向量</strong>用法：将 text、image、video 放在同一个 content 对象中，模型将所有输入融合为 1 个向量（type 为 `fused`）。

    ```python expandable
    import dashscope
    import json
    import os
    from http import HTTPStatus

    # 融合向量：将 text/image/video 放在同一个 content 对象中
    # 模型会将所有输入融合编码为 1 个向量，type 为 "fused"
    text = "白色运动鞋，轻量透气，适合跑步和日常穿着"
    image = "https://dashscope.oss-cn-beijing.aliyuncs.com/images/256_1.png"

    # 同一对象中的多模态内容会被融合为 1 个向量
    input_data = [
        {"text": text, "image": image}
    ]

    resp = dashscope.MultiModalEmbedding.call(
        api_key=os.getenv("DASHSCOPE_API_KEY"),
        model="tongyi-embedding-vision-plus-2026-03-06",
        input=input_data,
        dimension=1152
    )

    if resp.status_code == HTTPStatus.OK:
        result = {
            "status_code": resp.status_code,
            "request_id": getattr(resp, "request_id", ""),
            "output": resp.output,
            "usage": resp.usage
        }
        print(json.dumps(result, ensure_ascii=False, indent=4))
    ```
  </Tab>

  <Tab title="dashscope CLI">
    ```bash
    export DASHSCOPE_API_KEY="your-api-key"
    # 将 {WorkspaceId} 替换为业务空间ID；该模型仅在北京和新加坡地域提供，使用新加坡时将 cn-beijing 替换为 ap-southeast-1
    export DASHSCOPE_HTTP_BASE_URL="https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1"
    dashscope multimodal-embedding create -m tongyi-embedding-vision-plus --text "你好世界"
    ```

    <Tip>SDK Expert 交互式助手可按自然语言完成同样的开发与排障，见 [DashScope SDK Expert](/zh/model-studio/dashscope-sdk-expert)。</Tip>

    完整地域表见 [Base URL 总览](/zh/model-studio/base-url)。
  </Tab>
</Tabs>

### 输出示例 <span id="81b889218b9g2" />

```json expandable
{
    "status_code": 200,
    "request_id": "40532987-ba72-42aa-a178-bb58b52fb7f3",
    "code": "",
    "message": "",
    "output": {
        "embeddings": [
            {
                "index": 0,
                "embedding": [
                    -0.009490966796875,
                    -0.024871826171875,
                    -0.031280517578125,
                    ...
                ],
                "type": "text"
            }
        ]
    },
    "usage": {
        "input_tokens": 10,
        "input_tokens_details": {
            "image_tokens": 0,
            "text_tokens": 10
        },
        "output_tokens": 1,
        "total_tokens": 11
    }
}
```

## 错误码 <span id="OajFO" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

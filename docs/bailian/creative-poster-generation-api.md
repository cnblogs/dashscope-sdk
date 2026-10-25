> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 创意海报生成API参考

> 本文介绍创意海报生成模型的输入输出参数。根据要求自动生成海报的背景和文字排版，支持多种海报风格。无需设计基础，轻松制作出彩作品，让创意触手可及。

<meta property="og:title" content="创意海报生成API参考" />

<strong>相关指南</strong>：[创意海报生成](/zh/model-studio/creative-poster-generation-overview)

<Tip>
  - 本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
  - wanx-poster-generation-v1 模型当前仅提供<strong>免费体验</strong>，免费额度用完后不可调用且不支持付费，推荐参考[文本生成图像](/zh/model-studio/text-to-image)获取替代方案。
</Tip>

## 模型概览 <span id="a0c22dbf96vss" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "24.366042%" }} />

    <col style={{ width: "23.042999%" }} />

    <col style={{ width: "13.671444%" }} />

    <col style={{ width: "19.294377%" }} />

    <col style={{ width: "19.625138%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td rowSpan={2}>
        <strong>模型名</strong>
      </td>

      <td rowSpan={2}>
        <strong>免费额度</strong>[（查看）](/zh/model-studio/new-free-quota)
      </td>

      <td rowSpan={2}>
        <strong>计费单价</strong>
      </td>

      <td colSpan={2}>
        <strong>限流（含主账号与RAM子账号）</strong>
      </td>
    </tr>

    <tr>
      <td>
        <strong>任务下发接口QPS限制</strong>
      </td>

      <td>
        <strong>同时处理中任务数量</strong>
      </td>
    </tr>

    <tr>
      <td>
        wanx-poster-generation-v1
      </td>

      <td>
        500张
      </td>

      <td>
        目前仅供免费体验。

        > 免费额度用完后不可调用，推荐参考[文本生成图像](/zh/model-studio/text-to-image)获取替代方案。
      </td>

      <td>
        2
      </td>

      <td>
        1
      </td>
    </tr>
  </tbody>
</table>

## 前提条件 <span id="917981085bo9m" />

您需要已[获取与配置 API Key](/zh/model-studio/get-api-key)并[配置API Key到环境变量](/zh/model-studio/configure-api-key-through-environment-variables)。

## HTTP调用 <span id="ffa47ca257gkh" />

为了减少等待时间并且避免请求超时，服务采用异步方式提供。您需要发起两个请求：

- <strong>创建任务</strong>：首先发送一个请求创建文生图任务，该请求会返回任务ID。
- <strong>根据任务ID查询结果</strong>：使用上一步获得的任务ID，查询模型生成的结果。

### 步骤1：创建任务获取任务ID <span id="03e14ae842meh" />

`POST https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        #### 请求头（Headers） <span id="afcd41d2b57zf" />

        <strong>Authorization</strong>*string*<strong>必选</strong>

        推荐您使用阿里云百炼API-Key，也可填DashScope API-Key。例如：Bearer d1xxx2a。

        <strong>X-DashScope-Async</strong>*string*<strong>必选</strong>

        是否使用DashScope异步调用。HTTP只支持异步调用，设置为`enable`。

        <strong>Content-Type</strong>*string*<strong>必选</strong>

        请求内容类型。固定为`application/json`。

        #### 请求体（Request Body） <span id="66a778d4fbfub" />

        <strong>model</strong>*string*<strong>必选</strong>

        调用模型。创意海报生成模型为`wanx-poster-generation-v1`。

        <strong>parameters</strong> *object*<strong>必选</strong>

        其他模型调用参数，只需要输入一个空对象即可。示例：`{}`

        <strong>input</strong>*object*<strong>必选</strong>

        输入图像的基本信息，比如图像URL地址。

        <Accordion title="属性" defaultOpen>
          <strong>generate\_mode</strong> *string*<strong>必选</strong>

          海报生成模式。

          - generate：默认模式。
          - sr：高分辨率模式。
          - hrf：高清修复模式。
          - 只能从\["generate","sr","hrf"]中选择。海报生成的基础模式为"generate"，选择此模式会返回海报图片的url（render\_urls）和与其一一对应的辅助参数（auxiliary\_parameters）。用户可从返回的结果中，选择需要进行分辨率提升（或者高清修复）的海报，通过二次调用，输入选中的海报对应的辅助参数，将generate\_mode设置为"sr"（或者"hrf"），得到对应的高分辨率（高清修复）结果。

          <strong>generate\_num</strong> *Int*可选

          生成的海报数。该参数只在`generate_mode=generate`时有效。

          取值范围：\[1,4]，默认为1。如果设置的值大于4，则默认生成4张图。

          <strong>auxiliary\_parameters</strong> *string*可选

          当generate\_mode为"sr"或"hrf"时为必选项。需要提升分辨率或者高清修复的海报图片对应的辅助参数，数量限制为1。

          <Accordion title="注">
            该字段必须为海报生成服务的"generate"模式所返回的辅助参数。
          </Accordion>

          <strong>title</strong> *string*<strong>必选</strong>

          主标题。最多30个字符。例如，"春节快乐"。

          <strong>sub\_title</strong> *string*可选

          副标题。最多30个字符。例如，“家庭团聚，共享天伦之乐"。

          <strong>body\_text</strong> *string*可选

          正文。最多50个字符。例如："春节是中国最重要的传统节日之一，它象征着新的开始和希望"

          <strong>prompt\_text\_zh</strong> *string*可选

          中文提示词。例如，“小朋友画的可爱的龙，白色背景"

          > 中文和英文提示词至少二选一设置，支持两个参数同时设置。两个字段字符数加起来最多50个字/单词。

          <strong>prompt\_text\_en</strong> *string*可选

          的英文提示词。例如："Children draw a lovely dragon, white background"。

          > 中文和英文提示词至少二选一设置，支持两个参数同时设置。两个字段字符数加起来最多50个字/单词。

          <strong>wh\_ratios</strong> *string*可选

          生成海报的版式。

          - 横版：默认值。
          - 竖版

          <strong>lora\_name</strong> *string*可选

          海报风格名称。默认值为""。

          <Accordion title="海报风格枚举值及图示" defaultOpen>
            - 2D插画1

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817373.jpeg" alt="image.jpeg" width={101} />

            - 2D插画2

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817371.jpeg" alt="image.jpeg" width={106} />

            - 浩瀚星云

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817370.jpeg" alt="image.jpeg" width={102} />

            - 浓郁色彩

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817378.jpeg" alt="image.jpeg" width={106} />

            - 光线粒子

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817368.jpeg" alt="image.jpeg" width={111} />

            - 透明玻璃

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817374.jpeg" alt="image.jpeg" width={113} />

            - 剪纸工艺

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817376.jpeg" alt="image.jpeg" width={112} />

            - 折纸工艺

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817380.jpeg" alt="image.jpeg" width={114} />

            - 中国水墨

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817377.jpeg" alt="image.jpeg" width={116} />

            - 中国刺绣

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817372.jpeg" alt="image.jpeg" width={118} />

            - 真实场景

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817382.jpeg" alt="image.jpeg" width={123} />

            - 2D卡通

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817384.png" alt="image.png" width={125} />

            - 儿童水彩

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817383.jpeg" alt="image.jpeg" width={129} />

            - 赛博背景

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817375.jpeg" alt="image.jpeg" width={138} />

            - 浅蓝抽象

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817381.jpeg" alt="image.jpeg" width={135} />

            - 深蓝抽象

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817379.jpeg" alt="image.jpeg" width={139} />

            - 抽象点线

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817385.jpeg" alt="image.jpeg" width={144} />

            - 童话油画

            <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/5391000271/p817369.jpeg" alt="image.jpeg" width={145} />
          </Accordion>

          <strong>lora\_weight</strong> *float*可选

          海报风格权重，需要与lora\_name参数配合使用。

          取值范围：\[0, 1]，默认值为0.8。取值越接近1，海报风格越明显。

          <strong>ctrl\_ratio</strong> *float*可选

          留白效果权重，用于控制海报留白效果。

          取值范围：\[0, 1]，默认值为0.7。取值越接近1，留白效果越好，但海报背景生成效果可能会受到负面影响。

          <strong>ctrl\_step</strong> *float*可选

          留白步数比例，用于控制海报留白效果。

          取值范围：(0, 1]，默认值为0.7。取值越接近1，留白效果越好，但是海报背景生成效果可能会受到负面影响。

          <strong>creative\_title\_layout</strong> *bool*可选

          标题是否启用创意排版。默认值为false。
        </Accordion>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <Tabs>
          <Tab title="春节快乐海报">
            调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

            ```bash
            curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis' \
            --header 'X-DashScope-Async: enable' \
            --header 'Content-Type: application/json' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY" \
            --data-raw '{
                "model":"wanx-poster-generation-v1",
                "input": {
                    "title":"春节快乐",
                    "sub_title":"家庭团聚，共享天伦之乐",
                    "body_text":"春节是中国最重要的传统节日之一，它象征着新的开始和希望",
                    "prompt_text_zh":"灯笼，小猫，梅花",
                    "wh_ratios":"竖版",
                    "lora_name":"童话油画",
                    "lora_weight":0.8,
                    "ctrl_ratio":0.7,
                    "ctrl_step":0.7,
                    "generate_mode":"generate",
                    "generate_num":1
                },
                "parameters":{}
            }'
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 响应 <span id="cd3063363egdc" />

        <strong>output</strong>*object*

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> *string*

          任务id。

          <strong>task\_status</strong> *string*

          任务状态。

          - PENDING：排队中
          - RUNNING：处理中
          - SUSPENDED：挂起
          - SUCCEEDED：执行成功
          - FAILED：执行失败
        </Accordion>

        <strong>code</strong>*string*

        接口错误码。接口成功请求不会返回该参数。

        <strong>message</strong>*string*

        接口错误信息。接口成功请求不会返回该参数。

        <strong>request\_id</strong>*string*

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <CodeGroup>
          ```json 成功响应
          {
              "output": {
          	"task_id": "d76ec1e8-ea27-4038-8913-xxxxxxxxxxxx",
                  "task_status": "PENDING"
              },
              "request_id": "7574ee8f-38a3-4b1e-9280-11c33ab46e51"
          }
          ```

          ```json 异常响应
          {
              "code":"InvalidApiKey",
              "message":"Invalid API-key provided.",
              "request_id":"fb53c4ec-1c12-4fc4-a580-cdb7c3261fc1"
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

### 步骤2：根据任务ID查询结果 <span id="481754a27e8sb" />

`GET https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}`

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 请求头（Headers） <span id="5043fa882c26r" />

        <strong>Authorization</strong>*string*<strong>必选</strong>

        推荐使用阿里云百炼API-Key，也可填DashScope API-Key。例如：Bearer d1xxx2a。

        #### URL路径参数（Path parameters） <span id="aa6a7e4730pr2" />

        <strong>task\_id</strong>*string*<strong>必选</strong>

        任务id。
      </td>

      <td>
        <Tabs>
          <Tab title="获取任务结果">
            调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

            ```bash
            curl --location --request GET 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/tasks/{task_id}' \
            --header "Authorization: Bearer $DASHSCOPE_API_KEY"
            ```
          </Tab>
        </Tabs>
      </td>
    </tr>
  </tbody>
</table>

<table bordertype="no-border" style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "50%" }} />

    <col style={{ width: "50%" }} />
  </colgroup>

  <tbody>
    <tr>
      <td>
        #### 响应 <span id="fcab464334gzq" />

        <strong>output</strong>*object*

        任务输出信息。

        <Accordion title="属性" defaultOpen>
          <strong>task\_id</strong> *string*

          任务id。

          <strong>task\_status</strong> *string*

          任务状态。

          - PENDING：排队中
          - RUNNING：处理中
          - SUSPENDED：挂起
          - SUCCEEDED：执行成功
          - FAILED：执行失败

          <strong>render\_urls</strong> *list\[string]*

          海报图像URL列表。海报图像URL有效时限为24小时。

          - 当`generate_mode=generate`时，返回1～4张海报图像的URL列表。
          - 当`generate_mode=sr`或者`generate_mode=hrf`时，返回1张海报图像的URL。

          在返回海报图像URL之前，会进行敏感信息检测。如发现敏感内容，将拦截该图像，并替换为`output data may contain inappropriate content`。

          <strong>auxiliary\_parameters</strong> *List\[String]*

          高清修复和提升分辨率的辅助参数，与render\_urls列表的URL一一对应。有效时限为24小时。

          接口返回的auxiliary\_parameters可用于二次调用，将其作为入参，对海报进行分辨率提升或者高清修复。

          在返回海报图像URL之前，会进行敏感信息检测。如发现敏感内容，将拦截该图像，并替换为`output data may contain inappropriate content`。

          <strong>bg\_urls</strong> *List\[String]*

          不含文字的海报背景图URL。有效时限为24小时。

          - 当`generate_mode=generate`时，返回1～4张海报图像的URL列表。
          - 当`generate_mode=sr`或者`generate_mode=hrf`时，返回1张海报图像的URL。

          在返回海报图像URL之前，会进行敏感信息检测。如发现敏感内容，将拦截该图像，并替换为`output data may contain inappropriate content`。

          <strong>task\_metrics</strong> *object*

          任务信息统计指标。

          <Accordion title="属性" defaultOpen>
            <strong>TOTAL</strong> *integer*

            总的任务数。

            <strong>SUCCEEDED</strong> *integer*

            任务状态为成功的任务数。

            <strong>FAILED</strong> *integer*

            任务状态为失败的任务数。
          </Accordion>

          <strong>submit\_time</strong> *string*

          任务提交时间。

          <strong>scheduled\_time</strong> *string*

          任务排期执行时间。

          <strong>end\_time</strong> *string*

          任务完成时间。

          <strong>code</strong>*string*

          接口错误码。接口成功请求不会返回该参数。

          <strong>message</strong>*string*

          接口错误信息。接口成功请求不会返回该参数。
        </Accordion>

        <strong>usage</strong>*object*

        输出信息统计。

        <Accordion title="属性" defaultOpen>
          <strong>image\_count</strong> *integer*

          生成图像的数量。
        </Accordion>

        <strong>request\_id</strong>*string*

        请求唯一标识。可用于请求明细溯源和问题排查。
      </td>

      <td>
        <CodeGroup>
          ```json 任务执行成功
          {
              "request_id": "b67df059-ca6a-9d51-afcd-9b3c4456b1e2",
              "output": {
                  "task_id": "d76ec1e8-ea27-4038-8913-235c88ef0f70",
                  "task_status": "SUCCEEDED",
                  "submit_time": "2024-05-16 13:50:01.247",
                  "scheduled_time": "2024-05-16 13:50:01.354",
                  "end_time": "2024-05-16 13:50:27.795",
                  "render_urls": ["http://vision-poster.oss-cn-shanghai.aliyuncs.com/xxxxxx"],
                  "auxiliary_parameters": ["xxxxxx"],
                  "bg_urls":["http://vision-poster.oss-cn-shanghai.aliyuncs.com/xxxxxx"]
              },
              "usage":{
                "image_count":1
              }
          }
          ```

          ```json 任务执行中
          {
              "request_id":"e5d70b02-ebd3-98ce-9fe8-759d7d7b107d",
              "output":{
                  "task_id":"86ecf553-d340-4e21-af6e-a0c6a421c010",
                  "task_status":"RUNNING",
                  "task_metrics":{
                      "TOTAL":1,
                      "SUCCEEDED":1,
                      "FAILED":0
                  }
              }
          }
          ```

          ```json 任务执行失败
          {
              "request_id": "dccfdf23-b38e-97a6-a07b-f35118c1ada6",
              "output": {
                  "task_id": "4cbabbdf-2c1f-43f4-b983-c2cc47f4c115",
                  "task_status": "FAILED",
                  "submit_time": "2024-05-16 14:15:14.103",
                  "scheduled_time": "2024-05-16 14:15:14.154",
                  "end_time": "2024-05-16 14:15:14.694",
                  "code": "InvalidParameter",
                  "message": "check input data style"
              }
          }
          ```
        </CodeGroup>
      </td>
    </tr>
  </tbody>
</table>

## 错误码 <span id="Tk99S" />

如果模型调用失败并返回报错信息，请参见[错误码](/zh/model-studio/error-code)进行解决。

同时本模型还有如下特定错误码：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "14.330218%" }} /><col style={{ width: "23.831776%" }} /><col style={{ width: "41.588785%" }} /><col style={{ width: "20.249221%" }} /></colgroup><thead><tr><th><p><strong>HTTP状态码</strong></p></th><th><p><strong>接口错误码（code）</strong></p></th><th><p><strong>接口错误信息（message）</strong></p></th><th><p><strong>含义说明</strong></p></th></tr></thead><tbody><tr><td><p>400</p></td><td><p>InvalidParameter</p></td><td><p>check input data style</p></td><td><p>输入参数不满足入参要求</p></td></tr><tr><td><p>500</p></td><td><p>InternalError</p></td><td><p>inference error</p></td><td><p>算法内部错误</p></td></tr></tbody></table>

## 常见问题 <span id="aa98e673c3avi" />

- <strong>如何查询已经提交的所有异步任务？</strong>

  具体操作请参见[管理异步任务](/zh/model-studio/manage-asynchronous-tasks)。
- <strong>创意海报生成API是否支持对已经生成的海报进行二次编辑？</strong>

  不支持对已生成海报的内容（如标题、正文、风格等）进行二次编辑。仅能通过再次调用本API来提升已生成海报的分辨率。如需调整海报的内容，请重新设计并生成新的海报。

  提升已生成海报分辨率的方法为：将上次生成海报时的输入参数`"generate_mode"`的值修改为`sr`或`hrf`，并在输入参数中新增`"auxiliary_parameters"`，其值为上次生成海报时返回的完整参数集，其它参数保持不变。

  调用时请将`{WorkspaceId}`替换为真实的[业务空间ID](/zh/model-studio/regions#h2_migrate_domain)。

```bash
curl --location --request POST 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/text2image/image-synthesis' \
--header 'X-DashScope-Async: enable' \
--header 'Content-Type: application/json' \
--header "Authorization: Bearer $DASHSCOPE_API_KEY" \
--data-raw '{
    "model":"wanx-poster-generation-v1",
    "input": {
        "title":"春节快乐",
        "sub_title":"家庭团聚，共享天伦之乐",
        "body_text":"春节是中国最重要的传统节日之一，它象征着新的开始和希望",
        "prompt_text_zh":"灯笼，小猫，梅花",
        "wh_ratios":"竖版",
        "lora_name":"童话油画",
        "lora_weight":0.8,
        "ctrl_ratio":0.7,
        "ctrl_step":0.7,
        "generate_mode":"hrf",
        "auxiliary_parameters": "WMq4SC4......",
        "generate_num":1
    },
    "parameters":{}
}'
```

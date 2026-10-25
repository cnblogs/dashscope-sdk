> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 万相-图生视频-视频特效列表

> 万相提供视频特效功能， 无需输入提示词，只需指定特效模板 ，即可轻松生成预设的动态效果。

<meta property="og:title" content="万相-图生视频-视频特效列表" />

视频特效列表仅支持在“[图生视频-基于首帧](/zh/model-studio/legacy-image-to-video-api-reference)”和“[图生视频-基于首尾帧](/zh/model-studio/legacy-image-to-video-by-first-and-last-frame-api-reference)”中使用。

## 使用方法 <span id="17153ae3a3x2r" />

视频特效与图生视频的使用方式不同，请遵循以下规则：

- <strong>仅需一张首帧图像</strong>：无论选用的是“基于首帧”还是“基于首尾帧”的模型，<strong>只需要提供一张首帧图像</strong>。
- <strong>无需任何提示词</strong>：在特效模式下，模型会忽略 prompt 字段。建议不传入该参数，专注于选择视觉效果。
- <strong>选择一个模板</strong>：通过 `template` 参数从特效列表中选择一个动态效果。模型将根据模板生成指定的动态效果。

<Note>
  不同的模型支持的特效模板有所不同。请务必参考[特效列表-首帧生视频](/zh/model-studio/wanx-video-effects#89f64eb5a812c)和[特效列表-首尾帧生视频](/zh/model-studio/wanx-video-effects#6e5be7aaa1rc2)，确保所使用的 template 值与模型匹配。
</Note>

<strong>图生视频-基于首帧</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "29.04%" }} />

    <col style={{ width: "17.97%" }} />

    <col style={{ width: "23.96%" }} />

    <col style={{ width: "29.03%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>输入首帧</strong>
      </th>

      <th>
        <strong>输入提示词</strong>
      </th>

      <th>
        <strong>输入template</strong>
      </th>

      <th>
        <strong>输出视频</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/0458180571/p976602.png" alt="wanx-demo-1" />
      </td>

      <td style={{ verticalAlign: "top" }}>
        无需传入
      </td>

      <td style={{ verticalAlign: "top" }}>
        flying

        > 使用“魔法悬浮”特效
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250714/efzagm/demo_flying.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>
  </tbody>
</table>

<strong>图生视频-基于首尾帧</strong>

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "24.87%" }} />

    <col style={{ width: "15.03%" }} />

    <col style={{ width: "15.03%" }} />

    <col style={{ width: "20.2%" }} />

    <col style={{ width: "24.87%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>输入首帧</strong>
      </th>

      <th>
        <strong>输入尾帧</strong>
      </th>

      <th>
        <strong>输入提示词</strong>
      </th>

      <th>
        <strong>输入template</strong>
      </th>

      <th>
        <strong>输出视频</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        <Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8150385571/p1000087.png" alt="首尾帧生视频-视频特效-demo" />
      </td>

      <td style={{ verticalAlign: "top" }}>
        无需传入
      </td>

      <td style={{ verticalAlign: "top" }}>
        无需传入
      </td>

      <td style={{ verticalAlign: "top" }}>
        hanfu-1

        > 使用“唐韵翩然”特效
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250821/qaodio/%E9%A6%96%E5%B0%BE%E5%B8%A7-%E8%A7%86%E9%A2%91%E7%89%B9%E6%95%88-%E7%BB%93%E6%9E%9C.mp4" type="video/mp4" />
        </video>
      </td>
    </tr>
  </tbody>
</table>

<strong>示例代码</strong>

<Tabs>
  <Tab title="图生视频-基于首帧">
    <strong>首帧特效</strong>：对首帧图像生成特效视频。

    ```bash
    # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
    curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/video-generation/video-synthesis' \
        -H 'X-DashScope-Async: enable' \
        -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
        -H 'Content-Type: application/json' \
        -d '{
        "model": "wanx2.1-i2v-turbo",
        "input": {
            "img_url": "https://cdn.translate.alibaba.com/r/wanx-demo-1.png",
            "template": "flying"
        },
        "parameters": {
            "resolution": "720P"
        }
    }'
    ```

    > 完整代码请参见[图生视频-基于首帧](/zh/model-studio/legacy-image-to-video-api-reference)。
  </Tab>

  <Tab title="Python SDK">
    ```python expandable
    from http import HTTPStatus
    from dashscope import VideoSynthesis
    import dashscope
    import os

    # 若没有配置环境变量，请用百炼API Key将下行替换为：api_key="sk-xxx"
    # 获取API Key：https://help.aliyun.com/zh/model-studio/get-api-key
    api_key = os.getenv("DASHSCOPE_API_KEY")

    def sample_async_call():
        # 异步提交视频生成任务
        rsp = VideoSynthesis.async_call(api_key=api_key,
                                    model='wanx2.1-i2v-turbo',
                                    img_url='https://cdn.translate.alibaba.com/r/wanx-demo-1.png',
                                    template='flying',
                                    resolution='720P')
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print("task_id: %s" % rsp.output.task_id)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

        # 查询任务状态
        status = VideoSynthesis.fetch(task=rsp, api_key=api_key)
        if status.status_code == HTTPStatus.OK:
            print(status.output.task_status)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (status.status_code, status.code, status.message))

        # 等待任务完成
        rsp = VideoSynthesis.wait(task=rsp, api_key=api_key)
        print(rsp)
        if rsp.status_code == HTTPStatus.OK:
            print(rsp.output.video_url)
        else:
            print('Failed, status_code: %s, code: %s, message: %s' %
                  (rsp.status_code, rsp.code, rsp.message))

    if __name__ == '__main__':
        sample_async_call()
    ```
  </Tab>

  <Tab title="图生视频-基于首尾帧">
    <strong>首尾帧特效</strong>：只需提供一张首帧图像，无需尾帧图像，即可生成视频特效。

    - 若同时提供first\_frame\_url、last\_frame\_url、template，将忽略last\_frame\_url。
    - 若仅提供last\_frame\_url、template，请求将报错。

    ```bash
    # 以下为华北2（北京）地域的URL，调用时请将 {WorkspaceId} 替换为真实的业务空间ID，各地域的URL不同。
    curl --location 'https://{WorkspaceId}.cn-beijing.maas.aliyuncs.com/api/v1/services/aigc/image2video/video-synthesis' \
        -H 'X-DashScope-Async: enable' \
        -H "Authorization: Bearer $DASHSCOPE_API_KEY" \
        -H 'Content-Type: application/json' \
        -d '{
        "model": "wanx2.1-kf2v-plus",
        "input": {
            "first_frame_url": "https://ty-yuanfang.oss-cn-hangzhou.aliyuncs.com/lizhengjia.lzj/tmp/11.png",
            "template": "hanfu-1"
        },
        "parameters": {
            "resolution": "720P",
            "prompt_extend": true
        }
    }'
    ```

    > 完整代码请参见[图生视频-基于首尾帧](/zh/model-studio/legacy-image-to-video-by-first-and-last-frame-api-reference)。
  </Tab>
</Tabs>

## 特效列表-首帧生视频 <span id="89f64eb5a812c" />

视频特效的使用方式请参见[使用方法](/zh/model-studio/wanx-video-effects#17153ae3a3x2r)。

视频效果取决于模板和输入图片。为获得最佳效果，请先预览模板示例，并参考其“输入图像建议”，再使用您自己的图片进行尝试。

### 通用特效 <span id="85838a9e35vnu" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "11.136364%" }} />

    <col style={{ width: "15.909091%" }} />

    <col style={{ width: "29.015152%" }} />

    <col style={{ width: "20.30303%" }} />

    <col style={{ width: "23.636364%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>特效名称</strong>
      </th>

      <th>
        <strong>template参数值</strong>
      </th>

      <th>
        <strong>示例效果</strong>
      </th>

      <th>
        <strong>支持模型</strong>
      </th>

      <th>
        <strong>输入图像建议</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        解压捏捏
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>squish</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250714/hpeeqn/squish.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持任意主体
        - 建议使用主体突出，与背景有明显区分度的图片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        转圈圈
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>rotation</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/hfysff/rotation.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持任意主体
        - 建议使用主体突出，与背景有明显区分度的图片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        戳戳乐
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>poke</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/qwnrso/poke.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持任意主体
        - 建议使用主体突出，与背景有明显区分度的图片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        气球膨胀
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>inflate</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/hunync/inflate.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持任意主体
        - 建议使用主体突出，与背景有明显区分度的图片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        分子扩散
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>dissolve</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/aioocr/dissolve.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持任意主体
        - 建议使用主体突出，与背景有明显区分度的图片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        热浪融化
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>melt</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/arqqaz/melt.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持任意主体
        - 建议使用主体突出，与背景有明显区分度的图片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        冰淇淋星球
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>icecream</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/dkpsyk/icecream.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持任意主体
        - 建议使用主体突出，与背景有明显区分度的图片
      </td>
    </tr>
  </tbody>
</table>

### 单人特效 <span id="0868f6f028foz" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "10.385523%" }} />

    <col style={{ width: "15.735641%" }} />

    <col style={{ width: "28.481511%" }} />

    <col style={{ width: "21.007081%" }} />

    <col style={{ width: "24.390244%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>特效名称</strong>
      </th>

      <th>
        <strong>template参数值</strong>
      </th>

      <th>
        <strong>示例效果</strong>
      </th>

      <th>
        <strong>支持模型</strong>
      </th>

      <th>
        <strong>输入图像建议</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        时光木马
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>carousel</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/wvanhh/carousel.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        爱你哟
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>singleheart</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/hqqmzv/singleheart.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        摇摆时刻
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>dance1</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250714/fbpcko/dance1.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        头号甩舞
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>dance2</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250714/kvgmro/dance2.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用全身正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        星摇时刻
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>dance3</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250714/aorbwr/dance3.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用全身正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        指感节奏
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>dance4</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/wjqvqe/dance4.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        舞动开关
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>dance5</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/ymgfvm/dance5.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        人鱼觉醒
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>mermaid</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250714/euceoq/mermaid.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        学术加冕
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>graduation</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/jrdoqq/graduation.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        巨兽追袭
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>dragon</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250714/jiwuol/dragon.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        财从天降
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>money</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250714/ewufou/money.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        水母之约
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>jellyfish</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/seyqrh/jellyfish-%E5%9B%BD%E5%86%85.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        瞳孔穿越
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>pupil</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/hhzwvd/pupil-%E5%9B%BD%E5%86%85.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>
  </tbody>
</table>

### 单人或动物特效 <span id="10a28193a1mm8" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "10.291859%" }} />

    <col style={{ width: "16.205837%" }} />

    <col style={{ width: "29.185868%" }} />

    <col style={{ width: "20.506912%" }} />

    <col style={{ width: "23.809524%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>特效名称</strong>
      </th>

      <th>
        <strong>template参数值</strong>
      </th>

      <th>
        <strong>示例效果</strong>
      </th>

      <th>
        <strong>支持模型</strong>
      </th>

      <th>
        <strong>输入图像建议</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        魔法悬浮
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>flying</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/xulldo/flying.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人或动物照片
        - 建议使用全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        赠人玫瑰
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>rose</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/ylbkzy/rose.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人或动物照片
        - 建议使用半身至全身的正面照片，避免露出手部
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        闪亮玫瑰
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>crystalrose</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/bxzdey/crystalrose.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人或动物照片
        - 建议使用半身至全身的正面照片，避免露出手部
      </td>
    </tr>
  </tbody>
</table>

### 双人特效 <span id="4f2b5f26f1jst" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "10.568455%" }} />

    <col style={{ width: "15.772618%" }} />

    <col style={{ width: "27.46197%" }} />

    <col style={{ width: "21.377102%" }} />

    <col style={{ width: "24.819856%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>特效名称</strong>
      </th>

      <th>
        <strong>template参数值</strong>
      </th>

      <th>
        <strong>示例效果</strong>
      </th>

      <th>
        <strong>支持模型</strong>
      </th>

      <th>
        <strong>输入图像建议</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        爱的抱抱
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>hug</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/hcccyz/hug.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持双人照片
        - 建议两人正面看向镜头或相对站立 （面对面），可为半身或全身照
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        唇齿相依
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>frenchkiss</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/sqqrnp/frenchkiss.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持双人照片
        - 建议两人正面看向镜头或相对站立 （面对面），可为半身或全身照
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        双倍心动
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>coupleheart</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250708/nybpmm/coupleheart.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-i2v-plus

        wanx2.1-i2v-turbo
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持双人照片
        - 建议两人正面看向镜头或相对站立 （面对面），可为半身或全身照
      </td>
    </tr>
  </tbody>
</table>

## 特效列表-首尾帧生视频 <span id="6e5be7aaa1rc2" />

视频效果取决于模板和输入图片。为获得最佳效果，请先预览模板示例，并参考其“输入图像建议”，再使用您自己的图片进行尝试。

<Note>
  首尾帧生视频的特效是基于首尾帧模型训练，但实际使用时<strong>仅需传入首帧图像</strong>，无需提供尾帧，效果等同于首帧生成视频。具体请参见[使用方法](/zh/model-studio/wanx-video-effects#17153ae3a3x2r)。
</Note>

### 单人特效 <span id="ee4c06cbd14r1" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "10.385523%" }} />

    <col style={{ width: "15.735641%" }} />

    <col style={{ width: "28.481511%" }} />

    <col style={{ width: "21.007081%" }} />

    <col style={{ width: "24.390244%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <strong>特效名称</strong>
      </th>

      <th>
        <strong>template参数值</strong>
      </th>

      <th>
        <strong>示例效果</strong>
      </th>

      <th>
        <strong>支持模型</strong>
      </th>

      <th>
        <strong>输入图像建议</strong>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td style={{ verticalAlign: "top" }}>
        唐韵翩然
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>hanfu-1</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/kmhqqx/hanfu-1.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-kf2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        机甲变身
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>solaron</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/vtlvoe/solaron-%E5%9B%BD%E5%86%85.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-kf2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        闪耀封面
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>magazine</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/jtregf/magazine-%E5%9B%BD%E5%86%85+%281%29.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-kf2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        机械觉醒
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>mech1</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/wpozjd/mech1-%E5%9B%BD%E5%86%85+%281%29.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-kf2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>

    <tr>
      <td style={{ verticalAlign: "top" }}>
        赛博登场
      </td>

      <td style={{ verticalAlign: "top" }}>
        <strong>mech2</strong>
      </td>

      <td style={{ verticalAlign: "top" }}>
        <video controls>
          <source src="https://help-static-aliyun-doc.aliyuncs.com/file-manage-files/zh-CN/20250814/ctiwhk/mech2-%E5%9B%BD%E5%86%85%281%29.mp4" type="video/mp4" />
        </video>
      </td>

      <td style={{ verticalAlign: "top" }}>
        wanx2.1-kf2v-plus
      </td>

      <td style={{ verticalAlign: "top" }}>
        - 支持单人照片
        - 建议使用半身至全身的正面照片
      </td>
    </tr>
  </tbody>
</table>

## API参考 <span id="102ccf86556nk" />

- API参数说明和完整代码示例，请参见[图生视频-基于首帧](/zh/model-studio/legacy-image-to-video-api-reference)、[图生视频-基于首尾帧](/zh/model-studio/legacy-image-to-video-by-first-and-last-frame-api-reference)。

、

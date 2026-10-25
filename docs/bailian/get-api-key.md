> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取与配置 API Key

> 在控制台获取或创建 API Key 后，您将使用它来安全地调用百炼的模型服务。

<meta property="og:title" content="获取与配置 API Key" />

## 获取 API Key <span id="c38fb45bc6sje" /> <span id="ca06817d4cqro" />

1. 前往阿里云百炼[API Key](https://bailian.console.aliyun.com/cn-beijing/model/settings/api-key)页面，在右上角选择<strong>地域</strong>。每个地域有独立的接入域名、API Key 和模型列表，<strong>不能跨地域混用</strong>，各地域的 API Key 入口和接入域名请参见[各地域接入信息](/zh/model-studio/regions#h2_cc92e61c)。

2. 若已有可用 API Key，直接在列表中复制。若需创建，单击<strong>创建 API Key</strong>，在弹窗中配置以下选项后单击<strong>确定</strong>（若无法创建，请联系超级管理员或业务空间管理员）：

   - <strong>归属账号</strong>：默认选择<strong>阿里云账号（主账号）</strong>（账号名通常为一串数字）。如需为团队成员或应用创建权责独立的 API Key 并结合业务空间做精细化模型访问控制，可选择 [RAM 用户](https://help.aliyun.com/zh/ram/user-guide/overview-of-ram-users)（账号格式 `username@<AccountAlias>.onaliyun.com`）。
   - <strong>归属业务空间</strong>：默认选择<strong>默认业务空间</strong>（数量上限与主账号配额见[时效性与限制](#apikey-limits)）。如需权限隔离、精细化模型访问控制或独立成本核算，请选择非默认业务空间，参见[业务空间管理](/zh/model-studio/permission-management-overview)。
   - <strong>权限</strong>：建议选择<strong>全部</strong>。如需精细控制，可选择<strong>自定义</strong>，配置可访问 IP 和可访问模型范围（数量限制见[时效性与限制](#apikey-limits)）。

3. 点击新创建的 API Key 旁的<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/8412544571/p994217.png" alt="image" className="inline" noZoom />图标复制 API Key。

## 配置 API Key 到环境变量 <span id="20b17d4430qvt" /> <span id="6a82f1a968bhf" />

- <strong>方式一：在</strong>[<strong>第三方工具</strong>](/zh/model-studio/use-chat-client-or-development-tool)<strong>中进行配置</strong>

  如果在 Chatbox 等工具或平台中调用模型，您可能需要输入三个信息：

  - 本文获取的 API Key
  - Base URL：

    - 华北2（北京）：`https://dashscope.aliyuncs.com/compatible-mode/v1`
    - 其他地域：请参见[Base URL 总览](/zh/model-studio/base-url)
  - 模型名称，如 qwen-plus、qwen-max、deepseek-v4-pro 等。

  我们也提供了一些常用工具的配置方法：[Dify](/zh/model-studio/dify)、[Chatbox](/zh/model-studio/chatbox)、[Cline](/zh/model-studio/cline)、[Claude Code](/zh/model-studio/claude-code)、[Postman](/zh/model-studio/first-call-to-image-and-video-api)。
- <strong>方式二：配置 API Key 到环境变量，后续支持 cURL 和代码调用</strong>

  <Tip>
    请勿在客户端代码（如浏览器、移动应用）或不可信环境中配置或使用长期有效的 API Key。可以使用[临时 API Key](/zh/model-studio/generate-temporary-api-key)（最长 1800 秒）。
  </Tip>

  以下示例中的 `<YOUR_API_KEY>` 需替换为真实的 API Key。

  <Tabs>
    <Tab title="Linux系统">
      <Tabs>
        <Tab title="添加永久性环境变量">
          将环境变量设置追加到`~/.bashrc`文件中。

          ```bash
          echo "export DASHSCOPE_API_KEY='<YOUR_API_KEY>'" >> ~/.bashrc
          source ~/.bashrc
          echo "百炼 API Key 永久配置为：${DASHSCOPE_API_KEY:0:10}*********"
          ```
        </Tab>

        <Tab title="添加临时性环境变量">
          ```bash
          export DASHSCOPE_API_KEY='<YOUR_API_KEY>'
          echo "当前会话百炼 API Key 配置为：${DASHSCOPE_API_KEY:0:10}*********"
          ```
        </Tab>

        <Tab title="服务管理器 (systemd)">
          以下操作适用于将应用作为后台服务部署的场景。

          1. <strong>创建环境文件</strong>

          ```bash
          sudo sh -c "echo 'DASHSCOPE_API_KEY=<YOUR_API_KEY>' > /etc/your-app/env"
          sudo chmod 600 /etc/your-app/env # 限制只有root用户有访问权限
          ```

          2. <strong>修改 systemd 服务文件</strong>（如 `/etc/systemd/system/your-app.service`）：

          ```zsh
          [Service]
          # 加载密钥文件
          EnvironmentFile=/etc/your-app/env
          ExecStart=/usr/bin/python /opt/your-app/main.py
          ```

          3. <strong>重载服务</strong>

          ```bash
          sudo systemctl daemon-reload
          sudo systemctl restart your-app
          ```
        </Tab>
      </Tabs>
    </Tab>

    <Tab title="macOS系统">
      <Tabs>
        <Tab title="添加永久性环境变量">
          将环境变量设置追加到`~/.bash_profile`或`~/.zshrc`文件中。

          ```bash
          VAR_VALUE="<YOUR_API_KEY>"; \
          echo "export DASHSCOPE_API_KEY='$VAR_VALUE'" >> ~/.zshrc 2>/dev/null
          echo "export DASHSCOPE_API_KEY='$VAR_VALUE'" >> ~/.bash_profile 2>/dev/null
          source ~/."${SHELL##*/}rc" 2>/dev/null
          echo "百炼 API Key 永久配置为：${DASHSCOPE_API_KEY:0:10}*********"
          ```
        </Tab>

        <Tab title="添加临时性环境变量">
          ```bash
          export DASHSCOPE_API_KEY='<YOUR_API_KEY>'
          echo "当前会话百炼 API Key 配置为：${DASHSCOPE_API_KEY:0:10}*********"
          ```
        </Tab>
      </Tabs>
    </Tab>

    <Tab title="Windows系统">
      在Windows系统中，您可以通过系统属性、CMD或PowerShell配置环境变量。

      <Tabs>
        <Tab title="系统属性">
          系统属性方式注意事项：

          - 环境变量永久生效。
          - 修改系统环境变量需具备管理员权限。
          - 配置后不会立即影响已经打开的命令窗口、IDE或其他正在运行的应用程序，需重启这些程序或打开新的命令行使环境变量生效。

          1. 在Windows系统桌面中按`Win+Q`键，在搜索框中搜索<strong>编辑系统环境变量</strong>，单击打开<strong>系统属性</strong>界面。
          2. 在<strong>系统属性</strong>窗口，单击<strong>环境变量</strong>，然后在<strong>系统变量</strong>区域下单击<strong>新建</strong>，<strong>变量名</strong>填入`DASHSCOPE_API_KEY`，<strong>变量值</strong>填入您的DashScope API Key。
          3. 依次单击三个窗口的<strong>确定</strong>，关闭系统属性配置页面，完成环境变量配置。
          4. 打开CMD（命令提示符）窗口或Windows PowerShell窗口，执行如下命令检查环境变量是否生效。

             - CMD查询命令：

          ```bash
          echo 百炼 API Key 永久配置为：%DASHSCOPE_API_KEY:~0,10%*********
          ```

          - Windows PowerShell查询命令：

          ```powershell
          Write-Host "百炼 API Key 永久配置为：$($env:DASHSCOPE_API_KEY.Substring(0, [Math]::Min(10, $env:DASHSCOPE_API_KEY.Length)))*********"
          ```
        </Tab>

        <Tab title="CMD">
          <Tabs>
            <Tab title="添加永久性环境变量">
              如果您希望API Key环境变量在当前用户的所有新会话中生效，可以按如下操作。

              1. 在CMD中运行以下命令。

              ```bash
              setx DASHSCOPE_API_KEY "<YOUR_API_KEY>"
              ```

              2. 打开一个新的CMD窗口。
              3. 在新的CMD窗口运行以下命令，检查环境变量是否生效。

              ```bash
              echo 百炼 API Key 永久配置为：%DASHSCOPE_API_KEY:~0,10%*********
              ```
            </Tab>

            <Tab title="添加临时性环境变量">
              如果您仅希望在当前会话中使用该环境变量，可以在CMD中运行以下命令。

              ```bash
              set "DASHSCOPE_API_KEY=<YOUR_API_KEY>"
              ```

              您可以在当前会话运行以下命令检查环境变量是否生效。

              ```bash
              echo 当前会话百炼 API Key 配置为：%DASHSCOPE_API_KEY:~0,10%*********
              ```
            </Tab>
          </Tabs>
        </Tab>

        <Tab title="PowerShell">
          <Tabs>
            <Tab title="添加永久性环境变量">
              如果您希望API Key环境变量在当前用户的所有新会话中生效，可以按如下操作。

              1. 在PowerShell中运行以下命令。

              ```powershell height="62"
              [Environment]::SetEnvironmentVariable("DASHSCOPE_API_KEY", "<YOUR_API_KEY>", [EnvironmentVariableTarget]::User)
              ```

              2. 打开一个新的PowerShell窗口。
              3. 在新的PowerShell窗口运行以下命令，检查环境变量是否生效。

              ```powershell
              Write-Host "百炼 API Key 永久配置为：$($env:DASHSCOPE_API_KEY.Substring(0, [Math]::Min(10, $env:DASHSCOPE_API_KEY.Length)))*********"
              ```
            </Tab>

            <Tab title="添加临时性环境变量">
              如果您仅希望在当前会话中使用该环境变量，可以在PowerShell中运行以下命令。

              ```powershell
              $env:DASHSCOPE_API_KEY = "<YOUR_API_KEY>"
              Write-Host "当前会话百炼 API Key 配置为：$($env:DASHSCOPE_API_KEY.Substring(0, [Math]::Min(10, $env:DASHSCOPE_API_KEY.Length)))*********"
              ```
            </Tab>
          </Tabs>
        </Tab>
      </Tabs>
    </Tab>
  </Tabs>

## 时效性与限制 <span id="apikey-limits" /> <span id="7d2ee55621wns" /> <span id="fd72c5c1besqv" />

API Key 没有失效时间限制，若需要有时效性的 API Key，可生成[临时 API Key](/zh/model-studio/generate-temporary-api-key)（最长 1800 秒）。

API Key 的状态随账号操作而变化：

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}>
  <colgroup>
    <col style={{ width: "24.67%" }} />

    <col style={{ width: "29.54%" }} />

    <col style={{ width: "45.78%" }} />
  </colgroup>

  <thead>
    <tr>
      <th>
        <p>
          <strong>触发操作</strong>
        </p>
      </th>

      <th>
        <p>
          <strong>主账号的 API Key</strong>
        </p>
      </th>

      <th>
        <p>
          <strong>RAM 账号的 API Key</strong>
        </p>
      </th>
    </tr>
  </thead>

  <tbody>
    <tr>
      <td>
        <strong>主动删除 API Key</strong>
      </td>

      <td>
        删除后立即失效，不可恢复
      </td>

      <td>
        删除后立即失效，不可恢复
      </td>
    </tr>

    <tr>
      <td>
        <strong>将账号移出业务空间</strong>
      </td>

      <td>
        —
      </td>

      <td>
        移出业务空间后 API Key 失效，重新加入后恢复生效
      </td>
    </tr>

    <tr>
      <td>
        <strong>在</strong>[RAM 控制台](https://ram.console.aliyun.com/roles)<strong>删除账号/角色</strong>
      </td>

      <td>
        —
      </td>

      <td>
        删除后立即失效，不可恢复
      </td>
    </tr>
  </tbody>
</table>

### 数量与权限限制 <span id="apikey-quota" />

API Key 相关限制汇总：

- <strong>API Key 数量</strong>：账号+地域维度最多 50 个，不按业务空间限制；单个主账号最多 20 个业务空间（含默认业务空间）。
- <strong>自定义权限可选模型</strong>：最多 30 个，限于该业务空间已授权的模型。
- <strong>IP 白名单</strong>：最多 20 个地址或网段；IPv6 仅华北2（北京）地域支持；批量输入用英文逗号隔开。
- <strong>临时 API Key</strong>：最长 1800 秒。

## API 调用 <span id="4dd13345796cg" /> <span id="68bf80d8faprx" />

请访问[文本生成](/zh/model-studio/qwen-api-reference)以及更多模型的 API 参考。

OpenAI SDK 支持的模型，请参考[OpenAI 兼容](/zh/model-studio/toolkits-and-frameworks)。

## 常见问题 <span id="e6a57c054bfzy" /> <span id="4ed8cb6687hz6" />

### 无法创建 API Key？ <span id="9603b994a7nx6" />

API Key 的创建需使用[超级管理员](/zh/model-studio/permission-management-overview#982297bd47p3i)或[业务空间管理员](/zh/model-studio/permission-management-overview#c82f37c2033vw)操作。

### 如何管理 API Key 的权限？ <span id="44eed06516tbh" />

API Key 的调用权限完全由其<strong>归属业务空间</strong>决定。<strong>同一空间内的 API Key 权限相同</strong>，无需为不同模型（如文生文、文生图、语音合成）创建不同的 API Key。

- <strong>默认业务空间下的 API Key：</strong>可调用所有[标准模型](/zh/model-studio/models)，以及默认业务空间内的[应用](/zh/model-studio/application-introduction)。
- <strong>子业务空间下的 API Key：</strong>可调用该子业务空间已获得[模型调用授权](/zh/model-studio/permission-management-overview)的标准模型，以及该业务空间内的应用。

<strong>调用在阿里云百炼</strong>[<strong>调优后的模型</strong>](/zh/model-studio/model-training-overview)<strong>：</strong>此类模型部署成功后，仅能用其所在业务空间的API Key调用。

如需在业务空间之外做更细粒度的控制，可在创建或编辑 API Key 时将<strong>权限</strong>切换为<strong>自定义</strong>，配置：

- <strong>IP 白名单规则</strong>：默认放通全部 IPv4，完整规则参见[时效性与限制](#apikey-limits)。
- <strong>访问模型范围</strong>：开关开启后，该 API Key 仅能调用已勾选的模型（限于该业务空间已授权的模型，数量限制参见[时效性与限制](#apikey-limits)）。

### 环境变量已设置，为何代码仍提示找不到 API Key？ <span id="a486eb2dc2p5y" />

具体原因如下：

- 情况一：<strong>没有设置永久性环境变量</strong>。临时环境变量只在当前终端会话中有效，对于已经启动的 IDE 或其他应用程序并不会生效。请参考本文中设置永久性环境变量的方法。
- 情况二：<strong>没有重启IDE、命令行工具或应用</strong>。

  - 通常需要重启 IDE（如 VS Code）或命令行工具，使其能够加载最新的环境变量。
  - 如果在部署应用后设置了环境变量，可能需要重启应用服务，让应用能够重新加载环境变量。
- 情况三：<strong>需要在配置文件添加环境变量</strong>。如果您的应用是通过服务管理器（如 systemd、supervisord）启动的，可能需要在服务管理器的配置文件中添加环境变量。
- 情况四：<strong>用了sudo命令</strong>。如果使用`sudo python xx.py`运行脚本，可能会遗漏当前用户环境变量，因为`sudo`默认不继承所有环境变量。您可采用`sudo -E python xx.py`命令，其中的`-E` 参数确保环境变量被传递。如有权限执行该脚本，可以直接执行 `python xx.py`。

### 单个主账号下最多能创建多少个API Key？ <span id="e2f8ef0826duu" />

每个主账号下最多可创建20个业务空间（包括默认业务空间）；API Key 数量为账号+地域维度 50 个，不按业务空间限制。

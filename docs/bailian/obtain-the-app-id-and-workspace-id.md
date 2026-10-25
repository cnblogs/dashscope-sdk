> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 获取APP ID和Workspace ID

> 通过 API 调用阿里云百炼的应用（ 智能体 、 工作流 ）时，需提供凭证来指定目标应用及其所属的业务空间：

<meta property="og:title" content="获取APP ID和Workspace ID" />

- 应用位于<strong>默认业务空间</strong>，仅需提供 APP ID。
- 应用位于<strong>子业务空间</strong>，需同时提供 APP ID 和 Workspace ID。

本文介绍如何在控制台中快速获取这些凭证。更多业务空间相关内容请参考：[业务空间权限管理](/zh/model-studio/permission-management-overview#dac6676deelh2)。

<Note>
  目前只能通过控制台手动获取APP ID和Workspace ID，不支持通过 API 或命令行工具（CLI）查询。
</Note>

## 获取APP ID <span id="2612f896detsz" />

在通过 API 调用任一应用时，都需要提供其唯一的`APP ID`。

1. 访问[应用管理](https://bailian.console.aliyun.com/#/app-center)界面；
2. 在应用列表中找到目标应用，复制应用的`APP ID`。

## 获取Workspace ID <span id="732535cfc959h" />

`Workspace ID`是业务空间的唯一标识。在调用子业务空间下的应用，或德国（法兰克福）、华北2（北京）、新加坡、中国香港、日本（东京）地域下的模型时，API 请求中才必须包含`Workspace ID`，Workspace ID 是这些地域 [Base URL](/zh/model-studio/regions) 的组成部分。

### 获取当前业务空间 ID <span id="6cc9270e83h9d" />

适用于快速查找当前登录并使用的业务空间的 ID。

1. 访问[阿里云百炼控制台](https://bailian.console.aliyun.com/cn-beijing?spm=5176.29619931.J__Z58Z6CX7MY__Ll8p1ZOR.1.7dd7521cmX1pAh\&tab=model#/model-market)首页，选择目标地域；
2. 再点击页面右上角的<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/9465204771/p1061404.png" alt="image" className="inline" noZoom width={20} />图标；
3. 在弹出的对话框中即可查看并复制<strong>业务空间ID</strong>。

> RAM 子账号默认只能查看其已加入的业务空间的 ID。

### 查询所有业务空间 ID <span id="4c1d1e89ff9lb" />

适用于需要查询或管理主账号下全部业务空间的场景，只能由[超级管理员](/zh/model-studio/permission-management-overview#982297bd47p3i)操作。

1. 使用<strong>主账号</strong>或<strong>授权子账号</strong>登录阿里云百炼控制台；
2. 单击右上角的<Image src="https://help-static-aliyun-doc.aliyuncs.com/assets/img/zh-CN/3646850671/p1005980.png" alt="image" className="inline" noZoom width={20} />图标，前往[业务空间管理](https://bailian.console.aliyun.com/?tab=globalset#/efm/business_management)界面；
3. 在<strong>Workspace ID</strong>列，查看并复制所需业务空间的 ID。

<Tip>
  此操作要求使用主账号，或已授予`AliyunBailianFullAccess`/`AliyunBailianControlFullAccess`权限的 RAM 子账号。若无相应权限，访问页面将提示权限不足。关于如何授权，请参考[管理权限](/zh/model-studio/member-management)。
</Tip>

## 下一步 <span id="dde6ce4a1cgpf" />

获取凭证后，即可参考[工作流与旧版智能体应用 API应用 DashScope API 参考](/zh/model-studio/agent-and-workflow-application-api-reference)或[Responses API](/zh/model-studio/openai-responses-api)文档，构建并发送 API 请求。

## 常见问题 <span id="0302358feb6q6" />

<strong>Q：为什么 RAM 子账号访问“业务空间管理”页面会报错？</strong>

A：需要将 RAM 子账号设置为[超级管理员](/zh/model-studio/permission-management-overview#982297bd47p3i)才能使用该页面。

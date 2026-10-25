> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# FaceChain人物写真生成模型计量计费

<meta property="og:title" content="FaceChain人物写真生成模型计量计费" />

<Tip>
  - 本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

### 计费单元 <span id="58efbdf11bnfc" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>模型服务</strong></p></th><th><p><strong>计费单元</strong></p></th></tr></thead><tbody><tr><td><p>FaceChain人物图像检测</p></td><td><p>限时免费</p></td></tr><tr><td><p>FaceChain人物形象训练</p></td><td><p>训练形象次数</p></td></tr></tbody></table>

### 计费单价 <span id="86233ea11bkxi" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "32.412791%" }} /><col style={{ width: "40.261628%" }} /><col style={{ width: "27.325581%" }} /></colgroup><thead><tr><th><p><strong>模型服务</strong></p></th><th><p><strong>模型名称</strong></p></th><th><p><strong>计费单价</strong></p></th></tr></thead><tbody><tr><td><p>FaceChain人物图像检测</p></td><td><p>facechain-facedetect</p></td><td><p>限时免费</p></td></tr><tr><td><p>FaceChain人物形象训练</p></td><td><p>facechain-finetune</p></td><td><p>2.5元/次</p></td></tr></tbody></table>

### 免费额度 <span id="c566eb611btir" />

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "25.839416%" }} /><col style={{ width: "74.160584%" }} /></colgroup><thead><tr><th><p><strong>模型名称</strong></p></th><th><p><strong>免费额度</strong></p></th></tr></thead><tbody><tr><td><p>facechain-facedetect</p></td><td><p>申请体验通过后，单个阿里云账号（含RAM用户）对模型提供首次使用500张图像生成免费额度，有效期90天。</p></td></tr><tr><td><p>facechain-finetune</p></td><td><p>50次</p><p>有效期：申请通过后90天内</p></td></tr></tbody></table>

<Tip>
  免费额度将在申请体验审批通过后下一个整点生效。例如，您的审批在15:30分通过，您的免费额度将在16:00后生效，将抵扣您16:00后产生的账单费用。
</Tip>

<Note>
  您可以参阅[新人免费额度](/zh/model-studio/new-free-quota)确认您是否具备享有免费额度的资格，并查询免费总额度、剩余额度及到期时间。
</Note>

### 接口限制 <span id="3cbf63d03chaj" />

万相系列模型，均对调用QPS与并发进行限制，以账号（含子账户）为单位，模型间互相独立，限制信息如下。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "28.488372%" }} /><col style={{ width: "24.854651%" }} /><col style={{ width: "24.127907%" }} /><col style={{ width: "22.52907%" }} /></colgroup><thead><tr><th><p><strong>模型服务</strong></p></th><th><p><strong>模型名称</strong></p></th><th><p><strong>任务下发接口QPS限制</strong></p></th><th><p><strong>同时处理中任务数量</strong></p></th></tr></thead><tbody><tr><td><p>FaceChain人物图像检测</p></td><td><p>facechain-facedetect</p></td><td><p>5</p></td><td><p>同步接口无限制</p></td></tr><tr><td><p>FaceChain人物形象训练</p></td><td><p>facechain-finetune</p></td><td><p>2</p></td><td><p>1</p></td></tr></tbody></table>

## 查看账单 <span id="a1932ba11bi0w" />

有关账单详情请前往[费用与成本](https://usercenter2.aliyun.com/finance/expense-report/expense-detail)查询。

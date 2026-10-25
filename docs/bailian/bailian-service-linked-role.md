> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 服务关联角色

> 为实现特定功能，百炼需要通过服务关联角色（SLR）获取对其他云服务（如FC、内容安全等）或云资源的访问权限。当您首次在百炼中授权开通相关功能（如函数计算节点）时， 系统将自动为您创建对应的服务关联角色 。本文为您介绍百炼创建的服务关联角色及其删除办法。

<meta property="og:title" content="服务关联角色" />

## 百炼创建的服务关联角色 <span id="c624e5df408sg" />

> 您可以前往[RAM控制台](https://ram.console.aliyun.com/)的角色管理页面查看所有服务关联角色。

<table style={{ display: "table", tableLayout: "fixed", width: "100%" }}><colgroup><col style={{ width: "50%" }} /><col style={{ width: "50%" }} /></colgroup><thead><tr><th><p><strong>服务关联角色名称</strong></p></th><th><p><strong>服务关联角色描述</strong></p></th></tr></thead><tbody><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#Bd0OF">AliyunServiceRoleForSFMAccessFC</a></p></td><td><p><a href="/zh/model-studio/workflow-application">百炼工作流应用</a><span>和<a href="/zh/model-studio/what-is-process-orchestration-old">流程编排</a></span>通过此服务关联角色访问您在<a href="https://help.aliyun.com/zh/functioncompute/what-is-function-compute">FC</a>中的资源。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#2b75fc8a97g4c">AliyunServiceRoleForSFMDataHubOSSImport</a></p></td><td><p><a href="/zh/model-studio/manage-data">数据管理</a>通过此服务关联角色访问您在<a href="https://help.aliyun.com/zh/oss/user-guide/what-is-oss">OSS</a>中的资源。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#13012d75c0zat">AliyunServiceRoleForAccessOSS</a></p></td><td><p><a href="/zh/model-studio/secure-storage">安全存储空间</a>通过此服务关联角色访问您在<a href="https://help.aliyun.com/zh/oss/user-guide/what-is-oss">OSS</a>中的资源。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#7deade0402e0y">AliyunServiceRoleForSFMAccessADB</a></p></td><td><p><a href="/zh/model-studio/rag-knowledge-base">创建和使用知识库</a><span>和<a href="/zh/model-studio/secure-storage">安全存储空间</a></span>通过此服务关联角色访问您的<a href="https://help.aliyun.com/zh/analyticdb/analyticdb-for-postgresql/product-overview/overview-product-overview">ADB-PG</a>实例。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#e412ddc7eac5w">AliyunServiceRoleForSFMAccessingMNS</a></p></td><td><p><a href="/zh/model-studio/manage-data">数据管理</a>通过此服务关联角色访问<a href="https://help.aliyun.com/zh/mns/product-overview/what-is-mns">MNS</a>队列中的<a href="https://help.aliyun.com/zh/oss/user-guide/what-is-oss">OSS</a>变更消息。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#31e88eb412d6c">AliyunServiceRoleForSFMTelemetry</a></p></td><td><p><a href="/zh/model-studio/agenteval-observation">用量监控与性能分析</a>通过此服务关联角色访问您的<a href="https://help.aliyun.com/zh/opentelemetry/product-overview/what-is-managed-service-for-opentelemetry">OpenTelemetry</a>实例。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#e02fae3cf9x0q">AliyunServiceRoleForSFMAccessingCIP</a></p></td><td><p><a href="/zh/model-studio/application-introduction">百炼应用</a>通过此服务关联角色访问您在<a href="https://help.aliyun.com/zh/document_detail/28417.html">内容安全</a>中的服务或资源。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#558c3f95f9me9">AliyunServiceRoleForSFMAccessSLS</a></p></td><td><p><a href="/zh/model-studio/model-telemetry">模型监控</a>通过此服务关联角色访问您在<a href="https://help.aliyun.com/zh/sls/what-is-log-service">SLS</a>中的资源。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#8890ce2670qza">AliyunServiceRoleForSFMAccessCMS</a></p></td><td><p><a href="/zh/model-studio/model-telemetry">模型监控</a>通过此服务关联角色访问您在<a href="https://help.aliyun.com/zh/cms/cloudmonitor-2-0/what-is-cloud-monitor-2-0">CMS</a>中的资源。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#21281ba873vnj">AliyunServiceRoleForAccessCusOss</a></p></td><td><p>阿里云百炼平台通过此服务关联角色访问您在<a href="https://help.aliyun.com/zh/oss/user-guide/what-is-oss">OSS</a>中的资源，使用该权限操作您的OSS文件，实现百炼平台对OSS操作的托管。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#7f75202592td7">AliyunServiceRoleForSFMConnectorAccessDTS</a></p></td><td><p>阿里云百炼平台通过此服务关联角色访问您在<a href="https://help.aliyun.com/zh/dts/product-overview/what-is-dts">DTS</a>中的资源，使用该权限创建和管理DTS任务，实现从指定数据源接入数据到百炼平台。</p></td></tr><tr><td><p><a href="/zh/model-studio/bailian-service-linked-role#l8g2h4i7j6k9l">AliyunServiceRoleForSFMFineTuning</a></p></td><td><p><a href="/zh/model-studio/fine-tuning">模型调优</a>和<a href="/zh/model-studio/manage-data">数据管理</a>使用此服务关联角色访问您在<a href="https://help.aliyun.com/zh/cpfs/cpfs-product-introduction">CPFS</a>和<a href="https://help.aliyun.com/zh/oss/user-guide/what-is-oss">OSS</a>中的资源。</p></td></tr></tbody></table>

## AliyunServiceRoleForSFMAccessFC <span id="Bd0OF" />

### 应用场景 <span id="20bc0a4ff0evv" />

百炼[工作流应用](/zh/model-studio/workflow-application)和[流程编排](/zh/model-studio/what-is-process-orchestration-old)中的函数计算节点通过此服务关联角色访问您在[FC](https://help.aliyun.com/zh/functioncompute/what-is-function-compute)中的资源。

### 角色及权限说明 <span id="1d89cfddaet6k" />

服务关联角色名称：`AliyunServiceRoleForSFMAccessFC`

关联的系统策略：`AliyunServiceRolePolicyForSFMAccessFC`

该系统策略的权限说明：

```json
{
  "Action": [
    "fc:ListFunctions",
    "fc:InvokeFunction"
  ],
  "Resource": "*",
  "Effect": "Allow"
}
```

### 删除服务关联角色 <span id="d2d6c8b075h56" />

<Warning>
  删除此服务关联角色后，工作流应用和流程编排将无法创建和调用函数计算节点，请谨慎操作！
</Warning>

删除AliyunServiceRoleForSFMAccessFC服务关联角色前，须先删除所有已发布的工作流应用和流程中的函数计算节点（删除后需要重新发布应用或流程）。

如何删除服务关联角色，请参见[服务关联角色](https://help.aliyun.com/zh/ram/user-guide/service-linked-roles#section-b9f-8dv-b5q)。

## AliyunServiceRoleForSFMDataHubOSSImport <span id="2b75fc8a97g4c" />

### 应用场景 <span id="6179e10fae41e" />

百炼[数据管理](/zh/model-studio/manage-data)中的OSS数据导入功能通过此服务关联角色访问您在[OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss)中的资源。

### 角色及权限说明 <span id="bc02b4f688k3o" />

服务关联角色名称：`AliyunServiceRoleForSFMDataHubOSSImport`

关联的系统策略：`AliyunServiceRolePolicyForSFMDataHubOSSImport`

该系统策略的权限说明：

```json expandable
{
  "Version": "1",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": [
        "oss:ListBuckets",
        "oss:GetBucketLocation",
        "oss:GetBucketTagging"
      ],
      "Resource": "*"
    },
    {
      "Effect": "Allow",
      "Action": [
        "oss:DoMetaQuery",
        "oss:GetBucketInfo",
        "oss:GetBucketStat",
        "oss:GetBucketTransferAcceleration",
        "oss:GetCnameToken",
        "oss:GetMetaQueryStatus",
        "oss:GetObject",
        "oss:GetObjectTagging",
        "oss:DescribeRegions",
        "oss:ListObjects",
        "oss:ListObjectVersions"
      ],
      "Resource": "*",
      "Condition": {
        "StringEquals": {
          "oss:BucketTag/bailian-datahub-access": [
            "read"
          ]
        }
      }
    },
    {
      "Action": "ram:DeleteServiceLinkedRole",
      "Resource": "*",
      "Effect": "Allow",
      "Condition": {
        "StringEquals": {
          "ram:ServiceName": "datahub.sfm.aliyuncs.com"
        }
      }
    }
  ]
}
```

### 删除服务关联角色 <span id="500e73fa8esii" />

<Warning>
  删除此服务关联角色后，数据管理将无法访问您在OSS中的资源，请谨慎操作！
</Warning>

删除AliyunServiceRoleForSFMDataHubOSSImport服务关联角色前，请确保数据管理中没有正在进行中的OSS数据导入任务。

如何删除服务关联角色，请参见[服务关联角色](https://help.aliyun.com/zh/ram/user-guide/service-linked-roles#section-b9f-8dv-b5q)。

## AliyunServiceRoleForAccessOSS <span id="13012d75c0zat" />

### 应用场景 <span id="9ee74a70beak3" />

百炼[安全存储空间](/zh/model-studio/secure-storage)通过此服务关联角色访问您在[OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss)中的资源。

### 角色及权限说明 <span id="9e0613cecar6g" />

服务关联角色名称：`AliyunServiceRoleForAccessOSS`

关联的系统策略：`AliyunServiceRolePolicyForAccessOSS`

该系统策略的权限说明：

```json expandable
{
  "Version": "1",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": [
        "oss:ListBuckets",
        "oss:GetBucketLocation",
        "oss:GetBucketTagging"
      ],
      "Resource": [
        "*"
      ],
      "Condition": {}
    },
    {
      "Effect": "Allow",
      "Action": [
        "oss:DoMetaQuery",
        "oss:GetBucketInfo",
        "oss:GetBucketStat",
        "oss:GetBucketTransferAcceleration",
        "oss:GetCnameToken",
        "oss:GetMetaQueryStatus",
        "oss:GetObject",
        "oss:GetObjectTagging",
        "oss:DescribeRegions",
        "oss:ListObjects",
        "oss:ListObjectVersions",
        "oss:PutObjectTagging",
        "oss:DeleteObject",
        "oss:DeleteObjectTagging",
        "oss:PutObject"
      ],
      "Resource": [
        "*"
      ],
      "Condition": {
        "StringEquals": {
          "oss:BucketTag/bailian-safe-workspace-oss-access": [
            "ReadAndWrite"
          ]
        }
      }
    },
    {
      "Action": "ram:DeleteServiceLinkedRole",
      "Resource": "*",
      "Effect": "Allow",
      "Condition": {
        "StringEquals": {
          "ram:ServiceName": "ossaccess.sfm.aliyuncs.com"
        }
      }
    }
  ]
}
```

### 删除服务关联角色 <span id="4ac8d34dccwvp" />

<Warning>
  删除此服务关联角色后，安全存储空间将无法访问您在OSS中的资源，请谨慎操作！
</Warning>

删除AliyunServiceRoleForAccessOSS服务关联角色前，请确保您已在安全存储空间中断开所有OSS连接。如何断开连接，请参见[断开连接](/zh/model-studio/configure-zone-ip)。

如何删除服务关联角色，请参见[服务关联角色](https://help.aliyun.com/zh/ram/user-guide/service-linked-roles#section-b9f-8dv-b5q)。

## AliyunServiceRoleForSFMAccessADB <span id="7deade0402e0y" />

### 应用场景 <span id="ZzSRS" />

百炼[安全存储空间](/zh/model-studio/secure-storage)通过此服务关联角色访问您的 [ADB-PG](https://help.aliyun.com/zh/analyticdb/analyticdb-for-postgresql/product-overview/overview-product-overview) 数据库实例。

### 角色及权限说明 <span id="rzgNX" />

服务关联角色名称：`AliyunServiceRoleForSFMAccessADB`

关联的系统策略：`AliyunServiceRolePolicyForSFMAccessADB`

该系统策略的权限说明：

```json expandable
{
  "Version": "1",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": [
        "gpdb:DescribeDBInstanceAttribute",
        "gpdb:DescribeDBInstances",
        "gpdb:CreateVectorManagerAccount",
        "gpdb:QueryCollectionData",
        "gpdb:UpsertCollectionData",
        "gpdb:DeleteVectorIndex",
        "gpdb:CreateVectorIndex",
        "gpdb:CreateNamespace",
        "gpdb:ListNamespaces",
        "gpdb:DescribeNamespace",
        "gpdb:DeleteNamespace",
        "gpdb:ListCollections",
        "gpdb:GrantCollection",
        "gpdb:DescribeCollection",
        "gpdb:DeleteCollectionData",
        "gpdb:DeleteCollection",
        "gpdb:CreateCollection",
        "gpdb:UpdateCollectionDataMetadata"
      ],
      "Resource": "*"
    },
    {
      "Action": "ram:DeleteServiceLinkedRole",
      "Resource": "*",
      "Effect": "Allow",
      "Condition": {
        "StringEquals": {
          "ram:ServiceName": "adb-access.sfm.aliyuncs.com"
        }
      }
    }
  ]
}
```

### 删除服务关联角色 <span id="hu5DA" />

<Warning>
  删除此服务关联角色后，安全存储空间将无法访问您的ADB-PG实例，请谨慎操作！
</Warning>

删除AliyunServiceRoleForSFMAccessADB服务关联角色前，请确保您已在安全存储空间中断开所有ADB-PG连接。如何断开连接，请参见[断开连接](/zh/model-studio/configure-zone-ip)。

如何删除服务关联角色，请参见[服务关联角色](https://help.aliyun.com/zh/ram/user-guide/service-linked-roles#section-b9f-8dv-b5q)。

## AliyunServiceRoleForSFMAccessingMNS <span id="e412ddc7eac5w" />

### 应用场景 <span id="da982a9d39n0w" />

百炼[数据管理](/zh/model-studio/manage-data)通过此服务关联角色访问[MNS](https://help.aliyun.com/zh/mns/product-overview/what-is-mns)队列中的[OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss)变更消息，以实现对您在OSS中的资源（数据）变更的自动同步。

### 角色及权限说明 <span id="5baab0114an2r" />

服务关联角色名称：`AliyunServiceRoleForSFMAccessingMNS`

关联的系统策略：`AliyunServiceRolePolicyForSFMAccessingMNS`

该系统策略的权限说明：

```json expandable
{
  "Version": "1",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": [
        "mns:GetQueueAttributes",
        "mns:GetSubscriptionAttributes",
        "mns:GetTopicAttributes",
        "mns:ListEventNotifications",
        "mns:GetAccountAttributes",
        "mns:ListEvents",
        "mns:ListProducts",
        "mns:ListQueue",
        "mns:ListSubscriptionByTopic",
        "mns:ListTagResources",
        "mns:ListTopic",
        "mns:ReceiveMessage",
        "mns:DeleteMessage"
      ],
      "Resource": "*"
    },
    {
      "Effect": "Allow",
      "Action": [
        "mns:CreateQueue",
        "mns:DeleteQueue",
        "mns:SetQueueAttributes"
      ],
      "Resource": "acs:mns:*:*:/queues/bailian-oss-event*"
    },
    {
      "Action": "ram:DeleteServiceLinkedRole",
      "Resource": "*",
      "Effect": "Allow",
      "Condition": {
        "StringEquals": {
          "ram:ServiceName": "mns-access.sfm.aliyuncs.com"
        }
      }
    }
  ]
}
```

### 删除服务关联角色 <span id="017940084716w" />

本策略由大模型服务平台百炼定义并使用，请勿修改、删除，或将其授予除服务关联角色之外的任何RAM身份。

## AliyunServiceRoleForSFMTelemetry <span id="31e88eb412d6c" />

### 应用场景 <span id="c050de8ac1qhf" />

[用量监控与性能分析](/zh/model-studio/agenteval-observation)通过此服务关联角色访问您的[OpenTelemetry](https://help.aliyun.com/zh/opentelemetry/product-overview/what-is-managed-service-for-opentelemetry)实例。

### 角色及权限说明 <span id="dd12faabb4n2q" />

服务关联角色名称：`AliyunServiceRoleForSFMTelemetry`

关联的系统策略：`AliyunServiceRolePolicyForSFMTelemetry`

该系统策略的权限说明：

```json expandable
{
    "Version": "1",
    "Statement": [
        {
            "Effect": "Allow",
            "Action": [
                "log:Get*",
                "log:List*",
                "log:Query*"
            ],
            "Resource": [
                "acs:log:*:*:project/proj-xtrace-*",
                "acs:log:*:*:project/proj-xtrace-*/logstore/logstore-tracing",
                "acs:log:*:*:project/proj-xtrace-*/logstore/logstore-tracing/*"
            ]
        },
        {
            "Effect": "Allow",
            "Action": [
                "arms:Describe*",
                "arms:List*",
                "arms:Get*",
                "arms:Search*",
                "arms:Check*",
                "arms:Query*",
                "arms:InitTraceResource"
            ],
            "Resource": "*"
        },
        {
            "Effect": "Allow",
            "Action": [
                "xtrace:Read*",
                "xtrace:Get*",
                "xtrace:Describe*",
                "xtrace:Check*",
                "xtrace:OpenXtraceService"
            ],
            "Resource": "*"
        },
        {
            "Action": "ram:DeleteServiceLinkedRole",
            "Resource": "*",
            "Effect": "Allow",
            "Condition": {
                "StringEquals": {
                    "ram:ServiceName": "telemetry.sfm.aliyuncs.com"
                }
            }
        },
        {
            "Action": "ram:CreateServiceLinkedRole",
            "Resource": "*",
            "Effect": "Allow",
            "Condition": {
                "StringEquals": {
                    "ram:ServiceName": [
                        "arms.aliyuncs.com",
                        "xtrace.aliyuncs.com",
                        "cloudmonitor.aliyuncs.com"
                    ]
                }
            }
        }
    ]
}
```

### 删除服务关联角色 <span id="a1f066c15f7lt" />

本策略由大模型服务平台百炼定义并使用，请勿修改、删除，或将其授予除服务关联角色之外的任何RAM身份。

## AliyunServiceRoleForSFMAccessingCIP <span id="e02fae3cf9x0q" />

### 应用场景 <span id="4a5a976b7cmdx" />

[百炼应用](/zh/model-studio/application-introduction)通过此服务关联角色访问您在[内容安全](https://help.aliyun.com/zh/document_detail/28417.html)中的服务或资源。

### 角色及权限说明 <span id="699abc5188ym2" />

服务关联角色名称：`AliyunServiceRoleForSFMAccessingCIP`

关联的系统策略：`AliyunServiceRolePolicyForSFMAccessingCIP`

该系统策略的权限说明：

```json
{
  "Version": "1",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": "yundun-greenweb:ModerationForBaiLian",
      "Resource": "acs:yundun-greenweb:*:*:*"
    },
    {
      "Action": "ram:DeleteServiceLinkedRole",
      "Resource": "*",
      "Effect": "Allow",
      "Condition": {
        "StringEquals": {
          "ram:ServiceName": "content-security.sfm.aliyuncs.com"
        }
      }
    }
  ]
}
```

### 删除服务关联角色 <span id="097e351dbdh5o" />

本策略由大模型服务平台百炼定义并使用，请勿修改、删除，或将其授予除服务关联角色之外的任何RAM身份。

## AliyunServiceRoleForSFMAccessSLS <span id="558c3f95f9me9" />

### 应用场景 <span id="deacc4ba8fx22" />

百炼[模型监控](/zh/model-studio/model-telemetry)通过此服务关联角色访问您在[SLS](https://help.aliyun.com/zh/sls/what-is-log-service)中的资源。

### 角色及权限说明 <span id="436ecc74a22cr" />

服务关联角色名称：`AliyunServiceRoleForSFMAccessSLS`

关联的系统策略：`AliyunServiceRolePolicyForSFMAccessSLS`

该系统策略的权限说明：

```json expandable
{
  "Version": "1",
  "Statement": [
    {
      "Action": [
        "log:GetProject",
        "log:GetLogs",
        "log:GetLogStoreLogs",
        "log:GetProductDataCollection",
        "log:OpenProductDataCollection",
        "log:CloseProductDataCollection"
      ],
      "Resource": [
        "acs:log:*:*:project/aliyun-product-data-*",
        "acs:log:*:*:project/aliyun-product-data-*/logstore/bailian-*",
        "acs:log:*:*:project/aliyun-product-data-*/logstore/bailian-*/*"
      ],
      "Effect": "Allow"
    },
    {
      "Action": [
        "log:GetSlsService",
        "log:OpenSlsService",
        "log:DescribeService",
        "log:EnableService"
      ],
      "Resource": [
        "acs:log:*:*:service"
      ],
      "Effect": "Allow"
    },
    {
      "Action": "ram:CreateServiceLinkedRole",
      "Resource": "*",
      "Effect": "Allow",
      "Condition": {
        "StringEquals": {
          "ram:ServiceName": [
            "sls-access.sfm.aliyuncs.com",
            "audit.log.aliyuncs.com",
            "middlewarelens.log.aliyuncs.com",
            "securitylens.log.aliyuncs.com",
            "ai-lens.log.aliyuncs.com",
            "storagelens.log.aliyuncs.com"
          ]
        }
      }
    },
    {
      "Action": "ram:DeleteServiceLinkedRole",
      "Resource": "*",
      "Effect": "Allow",
      "Condition": {
        "StringEquals": {
          "ram:ServiceName": "sls-access.sfm.aliyuncs.com"
        }
      }
    }
  ]
}
```

### 删除服务关联角色 <span id="32ca089a63icb" />

本策略由大模型服务平台百炼定义并使用，请勿修改、删除，或将其授予除服务关联角色之外的任何RAM身份。

## AliyunServiceRoleForSFMAccessCMS <span id="8890ce2670qza" />

### 应用场景 <span id="5fd6abf631lrp" />

百炼[模型监控](/zh/model-studio/model-telemetry)通过此服务关联角色访问您在[CMS](https://help.aliyun.com/zh/cms/cloudmonitor-2-0/what-is-cloud-monitor-2-0)中的资源。

### 角色及权限说明 <span id="89ca65b0fed53" />

服务关联角色名称：`AliyunServiceRoleForSFMAccessCMS`

关联的系统策略：`AliyunServiceRolePolicyForSFMAccessCMS`

该系统策略的权限说明：

```json expandable
{
    "Version": "1",
    "Statement": [
        {
            "Action": "cms:GetCmsService",
            "Resource": "acs:cms:*:*:cmsservice/*",
            "Effect": "Allow"
        },
        {
            "Action": "cms:GetPrometheusInstance",
            "Resource": "acs:cms:*:*:prometheusinstance/*",
            "Effect": "Allow"
        },
        {
            "Action": [
                "log:GetProject",
                "log:GetLogs",
                "log:GetLogStoreLogs",
                "log:QueryPrometheusMetrics",
                "log:QueryMetrics"
            ],
            "Resource": [
                "acs:log:*:*:project/aliyun-product-data-*",
                "acs:log:*:*:project/aliyun-product-data-*/logstore/bailian-*",
                "acs:log:*:*:project/aliyun-product-data-*/logstore/bailian-*/*"
            ],
            "Effect": "Allow"
        },
        {
            "Action": "ram:DeleteServiceLinkedRole",
            "Resource": "*",
            "Effect": "Allow",
            "Condition": {
                "StringEquals": {
                    "ram:ServiceName": "cms-access.sfm.aliyuncs.com"
                }
            }
        }
    ]
}
```

### 删除服务关联角色 <span id="db6dd96b2dvzx" />

本策略由大模型服务平台百炼定义并使用，请勿修改、删除，或将其授予除服务关联角色之外的任何RAM身份。

## AliyunServiceRoleForAccessCusOss <span id="21281ba873vnj" />

### 应用场景 <span id="469815b4cfb6u" />

允许阿里云百炼平台访问您的[OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss)，使用该权限操作您的OSS文件，实现百炼平台对OSS操作的托管。

### 角色及权限说明 <span id="2ad97aeed3hiz" />

服务关联角色名称：`AliyunServiceRoleForAccessCusOss`

关联的系统策略：`AliyunServiceRolePolicyForAccessCusOss`

该系统策略的权限说明：

```json expandable
{
  "Version": "1",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": [
        "oss:ListBuckets",
        "oss:GetBucketLocation",
        "oss:GetBucketTagging"
      ],
      "Resource": [
        "*"
      ],
      "Condition": {}
    },
    {
      "Effect": "Allow",
      "Action": [
        "oss:DoMetaQuery",
        "oss:GetBucketInfo",
        "oss:GetBucketStat",
        "oss:GetBucketTransferAcceleration",
        "oss:GetCnameToken",
        "oss:GetMetaQueryStatus",
        "oss:GetObject",
        "oss:GetObjectTagging",
        "oss:DescribeRegions",
        "oss:ListObjects",
        "oss:ListObjectVersions",
        "oss:PutObjectTagging",
        "oss:DeleteObject",
        "oss:DeleteObjectTagging",
        "oss:PutObject",
        "oss:PutBucketCors"
      ],
      "Resource": [
        "*"
      ],
      "Condition": {
        "StringEquals": {
          "oss:BucketTag/bailian-connector-access": [
            "ReadAndWrite"
          ]
        }
      }
    },
    {
      "Action": "ram:DeleteServiceLinkedRole",
      "Resource": "*",
      "Effect": "Allow",
      "Condition": {
        "StringEquals": {
          "ram:ServiceName": "cus-oss-access.sfm.aliyuncs.com"
        }
      }
    }
  ]
}
```

### 删除服务关联角色 <span id="03b8f3735edi9" />

<Warning>
  删除此服务关联角色后，百炼将无法访问您在OSS中的资源，请谨慎操作！
</Warning>

删除AliyunServiceRoleForAccessCusOss服务关联角色前，请确保您已在阿里云百炼平台中断开所有OSS连接。如何断开连接，请参见[断开连接](/zh/model-studio/configure-zone-ip)。

如何删除服务关联角色，请参见[服务关联角色](https://help.aliyun.com/zh/ram/user-guide/service-linked-roles#section-b9f-8dv-b5q)。

## AliyunServiceRoleForSFMConnectorAccessDTS <span id="7f75202592td7" />

### 应用场景 <span id="7d86ef71924k3" />

允许阿里云百炼平台访问您的[DTS](https://help.aliyun.com/zh/dts/product-overview/what-is-dts)，使用该权限创建和管理DTS任务，实现从指定数据源接入数据到百炼平台。

### 角色及权限说明 <span id="b266e3acb01wj" />

服务关联角色名称：`AliyunServiceRoleForSFMConnectorAccessDTS`

关联的系统策略：`AliyunServiceRolePolicyForSFMConnectorAccessDTS`

该系统策略的权限说明：

```json expandable
{
  "Version": "1",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": [
        "dts:DescribeDtsJobDetail",
        "dts:StartDtsJob",
        "dts:StopDtsJob",
        "dts:DeleteDtsJob",
        "dts:SuspendDtsJob"
      ],
      "Resource": "*",
      "Condition": {
        "StringEquals": {
          "acs:ResourceTag/bailian-connector-access": [
            "ReadAndWrite"
          ]
        }
      }
    },
    {
      "Effect": "Allow",
      "Action": [
        "dts:CreateDtsInstance",
        "dts:ConfigureDtsJob",
        "dts:DescribeSchemaList",
        "dts:DescribeStruct",
        "dts:DescribeColumns",
        "dts:RunEndpointLinkTest",
        "dts:TagResources"
      ],
      "Resource": "*"
    },
    {
      "Action": "ram:DeleteServiceLinkedRole",
      "Resource": "*",
      "Effect": "Allow",
      "Condition": {
        "StringEquals": {
          "ram:ServiceName": "dts-access.sfm.aliyuncs.com"
        }
      }
    }
  ]
}
```

### 删除服务关联角色 <span id="5757117d81n4t" />

<Warning>
  删除此服务关联角色后，百炼将无法访问您的DTS，请谨慎操作！
</Warning>

删除AliyunServiceRoleForSFMConnectorAccessDTS服务关联角色前，请确保您已在安全存储空间中断开所有DTS连接。

如何删除服务关联角色，请参见[服务关联角色](https://help.aliyun.com/zh/ram/user-guide/service-linked-roles#section-b9f-8dv-b5q)。

## AliyunServiceRoleForSFMFineTuning <span id="l8g2h4i7j6k9l" />

### 应用场景 <span id="n0i4j6k9l8m1n" />

阿里云百炼[模型调优](/zh/model-studio/fine-tuning)和[数据管理](/zh/model-studio/manage-data)通过此服务关联角色访问您在[CPFS](https://help.aliyun.com/zh/cpfs/cpfs-product-introduction)和[OSS](https://help.aliyun.com/zh/oss/user-guide/what-is-oss)中的资源。

### 角色及权限说明 <span id="s5n9o1p4q3r6s" />

服务关联角色名称：`AliyunServiceRoleForSFMFineTuning`

关联的系统策略：`AliyunServiceRolePolicyForSFMFineTuning`

该系统策略的权限说明：

```
{
    "Version": "1",
    "Statement": [
        {
            "Effect": "Allow",
            "Action": [
                "nas:ClientMount",
                "nas:DescribeCpfsAccessPoints",
                "nas:DescribeFileSystems"
            ],
            "Resource": "*"
        },
        {
            "Action": "ram:DeleteServiceLinkedRole",
            "Resource": "*",
            "Effect": "Allow",
            "Condition": {
                "StringEquals": {
                    "ram:ServiceName": "fine-tuning.sfm.aliyuncs.com"
                }
            }
        },
        {
            "Effect": "Allow",
            "Action": [
                "oss:ListBuckets",
                "oss:GetBucketLocation",
                "oss:GetBucketTagging"
            ],
            "Resource": "*"
        },
        {
            "Effect": "Allow",
            "Action": [
                "oss:DoMetaQuery",
                "oss:GetBucketInfo",
                "oss:GetBucketStat",
                "oss:GetBucketTransferAcceleration",
                "oss:GetCnameToken",
                "oss:GetMetaQueryStatus",
                "oss:GetObject",
                "oss:GetObjectTagging",
                "oss:DescribeRegions",
                "oss:ListObjects",
                "oss:ListObjectVersions",
                "oss:DoMetaQuery",
                "oss:GetMetaQueryStatus"
            ],
            "Resource": "*",
            "Condition": {
                "StringEquals": {
                    "oss:BucketTag/bailian-finetune-access": [
                        "read"
                    ]
                }
            }
        }
    ]
}
```

### 删除服务关联角色 <span id="a3v7w9x2y1z4a" />

<Warning>
  删除此服务关联角色后，阿里云百炼模型调优和数据管理将无法访问您在CPFS和OSS中的资源，请谨慎操作！
</Warning>

删除AliyunServiceRoleForSFMFineTuning服务关联角色前，请确保模型调优中没有正在进行的训练任务，数据管理中没有正在进行的导入和发布任务。

如何删除服务关联角色，请参见[服务关联角色](https://help.aliyun.com/zh/ram/user-guide/service-linked-roles#section-b9f-8dv-b5q)。

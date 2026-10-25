> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# 席位购买与变配

> Token Plan 团队版席位的购买与变配接口。

<meta property="og:title" content="席位购买与变配" />

本组接口用于 Token Plan 团队版席位的购买与变配，属于 BssOpenApi（版本 `2023-09-30`），Endpoint 为 `business.aliyuncs.com`。

下单前可通过 `QueryProductList` 获取商品参数，通过 `DescribePricingModule` 和 `GetSubscriptionPrice`（版本 `2017-12-14`）查询定价。如果 `QueryProductList` 未返回席位商品，可使用 Token Plan 团队版控制台或订单中显示的商品参数。调用成功后生成订单，可通过 `GetOrders` 查询订单状态。

- [创建席位（新购）](/zh/model-studio/create-credit-seat)
- [新增席位](/zh/model-studio/add-credit-seats)
- [升级席位](/zh/model-studio/upgrade-credit-seat)
- [退订席位](/zh/model-studio/reduce-credit-seats)
- [续费席位](/zh/model-studio/renew-credit-seat)

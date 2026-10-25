> ## Documentation Index
> Fetch the complete documentation index at: https://docs.bailian.console.aliyun.com/llms.txt
> Use this file to discover all available pages before exploring further.

# FaceChain人物形象训练快速开始

> 调用FaceChain人物图像检测和人物形象训练API，完成训练图片校验、文件上传及人物形象模型训练。

<meta property="og:title" content="快速开始" />

<Tip>
  - 本文档仅适用于华北2（北京）地域，且必须使用该地域的[API Key](https://bailian.console.aliyun.com/model/settings/api-key)。
</Tip>

## 快速开始 <span id="qR4TM" />

### 前提条件 <span id="o6ecl" />

- 已开通服务并获得API-KEY：[获取与配置 API Key](/zh/model-studio/get-api-key)。

## 功能介绍 <span id="513c072173ew6" />

### 使用流程 <span id="5511a03073g9x" />

1. 开通大模型服务平台，获得API-KEY：[获取与配置 API Key](/zh/model-studio/get-api-key)
2. 开发调用人物图像检测API，进行用户上传图像的质量校验，非必选链路，可以用于产品中进行前置校验，及时提醒用户更换质量不合格的图像。详情参考[人物图像检测API详情](/zh/model-studio/facechain-face-detection-api)；
3. 图像文件打包，上传并管理文件，详情参考[百炼文件管理 API](/zh/model-studio/model-customization-file-management-service)；
4. 开发调用人物形象训练API，进行自定义人物的模型定制，并获得定制模型ID。详情参考[人物形象训练API详情](/zh/model-studio/facechain-finetune-api)；

## 示例代码 <span id="048073907c59x" />

<Note>
  以下代码演示了如何使用FaceChain原生API进行人物形象训练，运行下述代码时：

  - 需要使用您的API-KEY替换示例中的YOUR\_DASHSCOPE\_API\_KEY，代码才能正常运行。
  - 安装相关依赖
  - 请注意，您需要按照代码中注释的提示，替换相关文件路径
</Note>

如果您手头暂时没有合适的照片，您可以下载我们提供的样片：[sample1](https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample1.jpg)、[sample2](https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample2.jpg)、[sample3](https://dashscope.oss-cn-beijing.aliyuncs.com/samples/fine-tune/facechain/sample3.jpg)

### 依赖及代码 <span id="079e11d07cgz7" />

```java
<dependency>
  <groupId>com.alibaba</groupId>
  <artifactId>fastjson</artifactId>
  <version>the-latest-version2</version>
</dependency>
```

```java expandable
package com.alibaba.jing.demo;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.zip.ZipEntry;
import java.util.zip.ZipOutputStream;

import com.alibaba.fastjson.JSONArray;
import com.alibaba.fastjson.JSONObject;

public class FacechainDemo {
    private static final String API_KEY = "YOUR_DASHSCOPE_API_KEY";

    public static void main(String[] args) throws Exception {
        List<String> sourceFileLocalPaths = new ArrayList<>();
        //以两张本地照片打包为本地zip文件为例
        sourceFileLocalPaths.add("此处替换为您的图片文件本地path，例如 /home/admin/sample/sample1.jpeg");
        sourceFileLocalPaths.add("此处替换为您的图片文件本地path，例如 /home/admin/sample/sample2.jpeg");
        String targetZipFileLocalPath = "此处替换为您希望输出的本地zip包path，例如/home/admin/sample/sample.zip";
        //文件打包
        zipFiles(sourceFileLocalPaths, targetZipFileLocalPath);
        //上传文件至dashscope平台
        JSONObject uploadResponse = uploadFile(targetZipFileLocalPath);
        //获取上传文件后生成的file_id
        String fileId = uploadResponse.getJSONObject("data").getJSONArray("uploaded_files").getJSONObject(0).getString(
            "file_id");
        //发起训练任务
        JSONObject finetuneRequest = new JSONObject();
        //固定值
        finetuneRequest.put("model", "facechain-finetune");
        JSONArray fileIds = new JSONArray();
        fileIds.add(fileId);
        finetuneRequest.put("training_file_ids", fileIds);
        JSONObject createFinetuneResponse = sendPostRequest("https://dashscope.aliyuncs.com/api/v1/fine-tunes",
            finetuneRequest.toJSONString());
        //获取创建的训练任务的任务ID
        String jobId = createFinetuneResponse.getJSONObject("output").getString("job_id");
        //轮询任务至成功
        String queryJobUrl = "https://dashscope.aliyuncs.com/api/v1/fine-tunes/" + jobId;
        String finetunedOutput = null;
        do {
            JSONObject jobQueryResponse = sendGetRequest(queryJobUrl);
            String jobStatus = jobQueryResponse.getJSONObject("output").getString("status");
            if ("SUCCEEDED".equals(jobStatus)) {
                finetunedOutput = jobQueryResponse.getJSONObject("output").getString("finetuned_output");
            } else if ("FAILED".equals(jobStatus)) {
                throw new Exception("the job is failed");
            } else {
                System.out.println("the job " + jobId + " is now " + jobStatus);
                Thread.sleep(10 * 1000);
            }
        } while (Objects.isNull(finetunedOutput));
        //finetuned_output即为
        System.out.println("got the facechian resourceId:" + finetunedOutput);
    }

    private static void zipFiles(List<String> sourceFileLocalPaths, String targetZipFileLocalPath) {
        try {
            FileOutputStream fos = new FileOutputStream(targetZipFileLocalPath);
            ZipOutputStream zos = new ZipOutputStream(fos);
            byte[] buffer = new byte[1024];

            for (String sourceFile : sourceFileLocalPaths) {
                File file = new File(sourceFile);
                FileInputStream fis = new FileInputStream(file);
                zos.putNextEntry(new ZipEntry(file.getName()));

                int length;
                while ((length = fis.read(buffer)) > 0) {
                    zos.write(buffer, 0, length);
                }

                fis.close();
                zos.closeEntry();
            }

            zos.close();
            System.out.println("Files compressed successfully!");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public static JSONObject uploadFile(String fileToUpload) {
        StringBuilder responseBuilder = new StringBuilder();
        try {
            URL url = new URL("https://dashscope.aliyuncs.com/api/v1/files");
            HttpURLConnection conn = (HttpURLConnection)url.openConnection();
            conn.setRequestMethod("POST");
            conn.setDoOutput(true);
            // 设置请求头
            conn.setRequestProperty("Authorization", API_KEY);
            conn.setRequestProperty("Content-Type",
                "multipart/form-data; boundary=---------------------------1234567890");
            // 创建文件流
            File file = new File(fileToUpload);
            FileInputStream fis = new FileInputStream(file);
            // 获取输出流
            OutputStream os = conn.getOutputStream();
            // 写入请求体
            os.write(("-----------------------------1234567890\r\n" +
                "Content-Disposition: form-data; name=\"files\"; filename=\"" + file.getName() + "\"\r\n" +
                "Content-Type: application/octet-stream\r\n" +
                "\r\n").getBytes());

            // 写入文件内容
            byte[] buffer = new byte[1024];
            int bytesRead;
            while ((bytesRead = fis.read(buffer)) != -1) {
                os.write(buffer, 0, bytesRead);
            }

            // 写入请求结束标志
            os.write(("\r\n-----------------------------1234567890--\r\n").getBytes());

            // 关闭流
            fis.close();
            os.flush();
            os.close();

            // 获取响应码
            int responseCode = conn.getResponseCode();
            if (responseCode == HttpURLConnection.HTTP_OK) {
                System.out.println("File upload succeeded");
                // 读取响应体
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                String line;
                while ((line = br.readLine()) != null) {
                    responseBuilder.append(line);
                }
                br.close();
            } else {
                System.out.println("File upload failed with response code: " + responseCode);
                // 读取响应体
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getErrorStream()));
                String line;
                while ((line = br.readLine()) != null) {
                    responseBuilder.append(line);
                }
                br.close();
            }
            System.out.println("File uploaded finished,response=" + responseBuilder.toString());
        } catch (Exception e) {
            e.printStackTrace();
        }
        return JSONObject.parseObject(responseBuilder.toString());
    }

    public static JSONObject sendPostRequest(String url, String jsonBody) {
        StringBuilder responseBuilder = new StringBuilder();
        try {
            URL requestUrl = new URL(url);
            HttpURLConnection conn = (HttpURLConnection)requestUrl.openConnection();
            conn.setRequestMethod("POST");

            // 设置请求头
            conn.setRequestProperty("Authorization", API_KEY);
            conn.setRequestProperty("Content-Type", "application/json");

            // 设置请求体
            conn.setDoOutput(true);
            DataOutputStream os = new DataOutputStream(conn.getOutputStream());
            os.writeBytes(jsonBody);
            os.flush();
            os.close();

            // 获取响应码
            int responseCode = conn.getResponseCode();
            if (responseCode == HttpURLConnection.HTTP_OK) {
                // 读取响应体
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                String line;
                while ((line = br.readLine()) != null) {
                    responseBuilder.append(line);
                }
                br.close();
            } else {
                System.out.println("HTTP POST request failed with response code: " + responseCode);
                // 读取响应体
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getErrorStream()));
                String line;
                while ((line = br.readLine()) != null) {
                    responseBuilder.append(line);
                }
                br.close();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        System.out.println("post request response is " + responseBuilder.toString());
        return JSONObject.parseObject(responseBuilder.toString());
    }

    public static JSONObject sendGetRequest(String url) {
        StringBuilder responseBuilder = new StringBuilder();
        try {
            URL requestUrl = new URL(url);
            HttpURLConnection conn = (HttpURLConnection)requestUrl.openConnection();
            conn.setRequestMethod("GET");
            // 设置请求头
            conn.setRequestProperty("Authorization", API_KEY);
            conn.setRequestProperty("Content-Type", "application/json");
            // 获取响应码
            int responseCode = conn.getResponseCode();
            if (responseCode == HttpURLConnection.HTTP_OK) {
                // 读取响应体
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getInputStream()));
                String line;
                while ((line = br.readLine()) != null) {
                    responseBuilder.append(line);
                }
                br.close();
            } else {
                System.out.println("HTTP GET request failed with response code: " + responseCode);
                // 读取响应体
                BufferedReader br = new BufferedReader(new InputStreamReader(conn.getErrorStream()));
                String line;
                while ((line = br.readLine()) != null) {
                    responseBuilder.append(line);
                }
                br.close();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        System.out.println("get request response is " + responseBuilder.toString());
        return JSONObject.parseObject(responseBuilder.toString());
    }
}
```

## 了解更多 <span id="Iv5NB" />

有关FaceChain的详细调用方法，请参见[人物图像检测API详情](/zh/model-studio/facechain-face-detection-api)和[人物形象训练API详情](/zh/model-studio/facechain-finetune-api)。

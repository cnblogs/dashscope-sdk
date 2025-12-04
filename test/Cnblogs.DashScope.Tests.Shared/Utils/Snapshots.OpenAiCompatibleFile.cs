using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class OpenAiCompatibleFile
    {
        public static readonly FileInfo TestFile = new("RawHttpData/test2.txt");
        public static readonly FileInfo TestImage = new("RawHttpData/Lenna.jpg");

        public static readonly RequestSnapshot<DashScopeFile> UploadFileCompatibleNoSse = new(
            "upload-file-compatible",
            new DashScopeFile(
                "file-fe-5d5eb068893f4b5e8551ada4",
                "file",
                7,
                1764499365,
                "test2.txt",
                "file-extract",
                "processed"));

        public static readonly RequestSnapshot<DashScopeFile> GetFileCompatibleNoSse = new(
            "get-file-compatible",
            new DashScopeFile(
                "file-fe-d5c0ea9110bd47afb0505f43",
                "file",
                1314,
                1761480070,
                "file1.txt",
                "file-extract",
                "processed"));

        public static readonly RequestSnapshot<DashScopeOpenAiCompatibleFileList> ListFileCompatibleNoSse = new(
            "list-files-compatible",
            new DashScopeOpenAiCompatibleFileList(
                "list",
                false,
                new List<DashScopeFile>
                {
                    new(
                        "file-fe-d5c0ea9110bd47afb0505f43",
                        "file",
                        1314,
                        1761480070,
                        "file1.txt",
                        "file-extract",
                        "processed"),
                }));

        public static readonly RequestSnapshot<DashScopeDeleteFileResult> DeleteFileCompatibleNoSse = new(
            "delete-file-compatible",
            new DashScopeDeleteFileResult("file", true, "file-fe-d5c0ea9110bd47afb0505f43"));
    }

    public static class File
    {
        public static readonly FileInfo TestFile = new("RawHttpData/test2.txt");
        public static readonly FileInfo TestImage = new("RawHttpData/Lenna.jpg");

        public static readonly RequestSnapshot<DashScopeFileResponse<DashScopeListFilesData>> ListFilesNoSse = new(
            "list-files",
            new DashScopeFileResponse<DashScopeListFilesData>(
                "d4bdceb3-0a87-4025-b226-afb13c5a3442",
                new DashScopeListFilesData(
                    15,
                    1,
                    2,
                    new List<DashScopeFileDetail>
                    {
                        new(
                            "311d3340-1f9b-487c-bd35-481cd5a23855",
                            "test2.txt",
                            "test2",
                            7,
                            "Td5oBnUd0kfuUGMd0zd5Ig==",
                            "2025-11-30 19:37:33",
                            "http://dashscope-file-mgr.oss-cn-beijing.aliyuncs.com/api-fs/1493478651020171/67516/311d3340-1f9b-487c-bd35-481cd5a23855/test2.txt?Expires=1764589060&OSSAccessKeyId=STS.NYUDr5WyfdYrXTGudSB7jFWoH&Signature=xa4nsjmZTcHaOfTXvv3cR7C7vNE%3D&security-token=CAIS1AJ1q6Ft5B2yfSjIr5rgD8iBuqZH05uZWnL2kWQGTrhGqZLEqjz2IHhMdHFqBOwasfQ1nWxY7P0Ylrp6SJtIXleCZtF94oxN9h2gb4fb40tLcHrB08%2FLI3OaLjKm9u2wCryLYbGwU%2FOpbE%2B%2B5U0X6LDmdDKkckW4OJmS8%2FBOZcgWWQ%2FKBlgvRq0hRG1YpdQdKGHaONu0LxfumRCwNkdzvRdmgm4NgsbWgO%2Fks0SD0gall7ZO%2FNiqfcL%2FMvMBZskvD42Hu8VtbbfE3SJq7BxHybx7lqQs%2B02c5onNWwMMv0nZY7CNro01d1VjFqQhXqBFqPW5jvBipO3YmsHv0RFBeOZOSDQE1i1TRm1UcgnAGaHaFd6TUxylurgEJk7zIan5z1gvlRKYWhvQG45hiCYmPtXwQEGpNl5k7MlN5QbLfi8Yf1QXq3esyb6gQz4rK2zRlCpDUvdUGoABOb8SEKtMfDU%2FVSF1VJ7hLFCQxC%2F7xuFKTrsxyK2UMGsgt18A%2Fe8Tq%2BtiBoH94pv%2B1hhJ34C6qRwLdhbhIN31TmYd%2FV9xPc9QFJYZuh49xUXFeapxnuaM8KxoXVc94FBlIy9ccDi%2FG%2BOyNbrtISjtNhJXehcJQ66tMTRxYIVeUA4gAA%3D%3D",
                            "1493478651020171",
                            "cn-beijing",
                            "67516",
                            193320717),
                        new(
                            "8a95f76a-8d79-4d8d-b372-7d0a72493680",
                            "test1.txt",
                            "test1",
                            6,
                            "2wbHjR4kz3CKFM6BybYX7A==",
                            "2025-11-30 19:37:33",
                            "http://dashscope-file-mgr.oss-cn-beijing.aliyuncs.com/api-fs/1493478651020171/67516/8a95f76a-8d79-4d8d-b372-7d0a72493680/test1.txt?Expires=1764589060&OSSAccessKeyId=STS.NYUDr5WyfdYrXTGudSB7jFWoH&Signature=1Gi%2FeChAGNPeFQj2oLszyd7M3ng%3D&security-token=CAIS1AJ1q6Ft5B2yfSjIr5rgD8iBuqZH05uZWnL2kWQGTrhGqZLEqjz2IHhMdHFqBOwasfQ1nWxY7P0Ylrp6SJtIXleCZtF94oxN9h2gb4fb40tLcHrB08%2FLI3OaLjKm9u2wCryLYbGwU%2FOpbE%2B%2B5U0X6LDmdDKkckW4OJmS8%2FBOZcgWWQ%2FKBlgvRq0hRG1YpdQdKGHaONu0LxfumRCwNkdzvRdmgm4NgsbWgO%2Fks0SD0gall7ZO%2FNiqfcL%2FMvMBZskvD42Hu8VtbbfE3SJq7BxHybx7lqQs%2B02c5onNWwMMv0nZY7CNro01d1VjFqQhXqBFqPW5jvBipO3YmsHv0RFBeOZOSDQE1i1TRm1UcgnAGaHaFd6TUxylurgEJk7zIan5z1gvlRKYWhvQG45hiCYmPtXwQEGpNl5k7MlN5QbLfi8Yf1QXq3esyb6gQz4rK2zRlCpDUvdUGoABOb8SEKtMfDU%2FVSF1VJ7hLFCQxC%2F7xuFKTrsxyK2UMGsgt18A%2Fe8Tq%2BtiBoH94pv%2B1hhJ34C6qRwLdhbhIN31TmYd%2FV9xPc9QFJYZuh49xUXFeapxnuaM8KxoXVc94FBlIy9ccDi%2FG%2BOyNbrtISjtNhJXehcJQ66tMTRxYIVeUA4gAA%3D%3D",
                            "1493478651020171",
                            "cn-beijing",
                            "67516",
                            193320718)
                    })));

        public static readonly RequestSnapshot<DashScopeFileResponse<DashScopeFileDetail>> GetFileNoSse = new(
            "get-file",
            new DashScopeFileResponse<DashScopeFileDetail>(
                "1f05e2cb-de12-46f1-872c-ab70aa15e87f",
                new DashScopeFileDetail(
                    "file-fe-5d5eb068893f4b5e8551ada4",
                    "test2.txt",
                    string.Empty,
                    7,
                    "Td5oBnUd0kfuUGMd0zd5Ig==",
                    "2025-11-30 18:42:45",
                    "http://dashscope-file-mgr.oss-cn-beijing.aliyuncs.com/api-fs/1493478651020171/67516/2f2e1a67-e94c-4a00-8e86-6d51d94a8f75/test2.txt?Expires=1764587166&OSSAccessKeyId=STS.NZN1pTGUrUxPQhPveYcaRAYMg&Signature=k7llO9KgcPkeuQicUgwaGR2w%2F4A%3D&security-token=CAIS1AJ1q6Ft5B2yfSjIr5n7esrgqopT4rq7U07hkmUMb%2B5%2BrpzmhTz2IHhMdHFqBOwasfQ1nWxY7P0Ylrp6SJtIXleCZtF94oxN9h2gb4fb4wUfE3vB08%2FLI3OaLjKm9u2wCryLYbGwU%2FOpbE%2B%2B5U0X6LDmdDKkckW4OJmS8%2FBOZcgWWQ%2FKBlgvRq0hRG1YpdQdKGHaONu0LxfumRCwNkdzvRdmgm4NgsbWgO%2Fks0SD0gall7ZO%2FNiqfcL%2FMvMBZskvD42Hu8VtbbfE3SJq7BxHybx7lqQs%2B02c5onNWwMMv0nZY7CNro01d1VjFqQhXqBFqPW5jvBipO3YmsHv0RFBeOZOSDQE1i1TRm1UcgnAGaHaFd6TUxylurgEBon2HKn5z1gvlRKYWhvQG45hiCYmPtXwQEGpNl5k7MlN5QbLfi8Yf1QXq3esyb6gQz4rK%2F1R8LZDUvdUGoABihW5mNOnIIV1zRuddC7OZ50nQwm%2F23uV3Y8WICHL1WbbOqhjkgUlqZdQhBVk4pNlL2QoziUCeSwPJa6o2mvoch%2BIVx5OA48YB9pBa2KYLl%2BAkVNzQVU%2FLtOR2bQQWQwIwtKwJEP107ZOFwwQgLOapGkrawOc7PEdg0Brr71x%2BrEgAA%3D%3D",
                    "1493478651020171",
                    "cn-beijing",
                    "67516",
                    193306194)));

        public static readonly RequestSnapshot<DashScopeFileResponse> DeleteFileNoSse =
            new("delete-file", new("df35151c-0df6-4cad-9912-83ebf8c633a4"));

        public static readonly RequestSnapshot<DashScopeFileResponse<DashScopeUploadFileData>> UploadFileNoSse = new(
            "upload-file",
            new DashScopeFileResponse<DashScopeUploadFileData>(
                "982c4dc2-95a0-4fa5-982e-2732f5f9c011",
                new DashScopeUploadFileData(
                    new List<DashScopeUploadedFileRecord>()
                    {
                        new("ed5a313b-3fdf-4cc9-a0b0-66664af692e1", "test2.txt")
                    },
                    new List<DashScopeFailedUploadRecord>()
                    {
                        new(
                            "test1.txt",
                            "BadRequest.TooMany",
                            "Out of number, <10> of <10> files has been uploaded.")
                    })));
    }
}

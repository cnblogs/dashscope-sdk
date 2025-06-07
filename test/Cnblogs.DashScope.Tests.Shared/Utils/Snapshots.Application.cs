using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Tests.Shared.Utils;

public static partial class Snapshots
{
    public static class Application
    {
        public static readonly RequestSnapshot<ApplicationRequest, ApplicationResponse> SinglePromptNoSse =
            new(
                "application-single-generation-text",
                new ApplicationRequest
                {
                    Input = new ApplicationInput { Prompt = "总结xUnit Test Patterns中的内容" },
                    Parameters = new ApplicationParameters
                    {
                        TopK = 100,
                        TopP = 0.8f,
                        Seed = 1234,
                        Temperature = 0.85f,
                        RagOptions = new ApplicationRagOptions
                        {
                            PipelineIds = new List<string> { "thie5bysoj" }.AsReadOnly(),
                            FileIds = new List<string> { "file_d129d632800c45aa9e7421b30561f447_10207234" }.AsReadOnly()
                        }
                    }
                },
                new ApplicationResponse(
                    "c127bd40-180c-9cfa-b991-f875edd8c310",
                    new ApplicationOutput(
                        "xUnit Test Patterns 提供了一套全面的指南，用于改进测试自动化和重构测试代码。以下是根据提供的文档内容总结的关键点：\n\n1. 测试自动化的目标包括帮助提高产品质量、帮助我们理解被测系统（SUT）、减少（并且不引入）风险、易于运行、编写和维护<ref>[4]</ref>。\n2. 在管理共享fixture方面，文档讨论了访问共享fixture以及触发共享fixture构造的方法<ref>[2]</ref>。\n3. 关于结果验证，文档提供了自我检查测试的方法，验证状态或行为，使用内置断言进行状态验证，以及验证直接输出和替代路径<ref>[2]</ref>。\n4. 当涉及到数据库时，文档提到了与数据库相关的测试问题、没有数据库的测试、数据库测试、存储过程测试、数据访问层测试，并强调确保开发者独立性<ref>[3]</ref>。\n5. 文档还涵盖了测试方法组织策略、测试命名约定、测试套件组织、运行测试组或单个测试、测试代码重用、测试文件组织等内容<ref>[5]</ref>。\n\n这些模式和实践旨在解决测试中的常见问题，如高测试维护成本、不可测试代码最小化、防止生产代码中的错误测试等<ref>[1]</ref>。通过应用这些模式，开发者可以创建更高效、更易于维护的自动化测试。",
                        "stop",
                        "b7250cba47db463ca851dfb4088e71d8",
                        new List<ApplicationOutputThought>
                        {
                            new(
                                null,
                                "agentRag",
                                "知识检索",
                                "rag",
                                "{}",
                                null,
                                "[{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:Visual Summary of the Pattern Language\\n文档类型:[\\\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\",\\\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\"]\\n【正文】:Minimize Untestable Code Buggy Tests Production Bugs Keep Test Logic Out of Production Developers Not Writing Tests Ensure Commensurate Effort and Responsibility High Test Maintenance CostKey to Visual Summary of the Pattern Language Chapter Name Chapter Name Sub-Category, Altemative Pattern Smell Pattern 1Pattern 2from Other Chapter'Cause of Smell Sub-Category variation, of Altemative Pattem十Pattem 1Smell Variation of Pattern used with Pattern leads toi Smell Variation described each other Alternative Pattem 2separatelyVISUAL SUMMARY OF THE PATTERN LANGUAGE\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_thie5bysoj_file_d129d632800c45aa9e7421b30561f447_10207234_1_3\",\"images\":[\"http://docmind-api-cn-hangzhou.oss-cn-hangzhou.aliyuncs.com/1257896666798445/publicDocStructure/docmind-20250315-ee118d3555104aba9200f6cf525bae0a/19.png?Expires=1742655907&OSSAccessKeyId=LTAI5tFEK2uEApeeYzxNMEci&Signature=89B%2FoauA6i4g34LikZ06Z0PUHY4%3D&x-oss-process=image%2Fcrop%2Cx_232%2Cy_610%2Cw_964%2Ch_648\",\"http://docmind-api-cn-hangzhou.oss-cn-hangzhou.aliyuncs.com/1257896666798445/publicDocStructure/docmind-20250315-ee118d3555104aba9200f6cf525bae0a/19.png?Expires=1742655907&OSSAccessKeyId=LTAI5tFEK2uEApeeYzxNMEci&Signature=3VwzeegSrkzfQIcMDz2F3C2bTdg%3D&x-oss-process=image%2Fcrop%2Cx_227%2Cy_1305%2Cw_991%2Ch_302\"],\"referenceIndex\":1,\"score\":0.5756075978279114,\"title\":\"Visual Summary of the Pattern Language\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\",\\\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\"]\\n【正文】:Managing Shared Fixtures...103Accessing Shared Fixtures...103Triggering Shared Fixture Construction...104What's Next?...106Chapter 10. Result Verification...107About This Chapter ...107Making Tests Self-Checking...107Verify State or Behavior?...108State Verification...109Using Built-in Assertions ...110Delta Assertions...111External Result Verification ...111Verifying Behavior...112Procedural Behavior Verification...113Expected Behavior Specification . . ...113CONTENTSReducing Test Code Duplication...114Expected Objects...115Custom Assertions...116Outcome-Describing Verification Method ...117\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_thie5bysoj_file_d129d632800c45aa9e7421b30561f447_10207234_0_33\",\"images\":[],\"referenceIndex\":2,\"score\":0.5756075978279114,\"title\":\"xUnit Test Patterns\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\",\\\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\"]\\n【正文】:?...168Issues with Databases...168Testing without Databases...169Testing the Database...171Testing Stored Procedures...172Testing the Data Access Layer...172Ensuring Developer Independence...173Testing with Databases (Again!)...173What's Next? ...174Chapter 14. A Roadmap to Effective Test Automation ...175About This Chapter...175Test Automation Difficulty .. ...175Roadmap to Highly Maintainable Automated Tests...176Exercise the Happy Path Code ...177Verify Direct Outputs of the Happy Path...178CONTENTSVerify Alternative Paths...178Verify Indirect Output Behavior...179\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_thie5bysoj_file_d129d632800c45aa9e7421b30561f447_10207234_0_37\",\"images\":[],\"referenceIndex\":3,\"score\":0.5697553753852844,\"title\":\"xUnit Test Patterns\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\",\\\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\"]\\n【正文】:?..17viiiCONTENTSChapter 3. Goals of Test Automation ...19About This Chapter...19Why Test?...19Economics of Test Automation20Goals of Test Automation...21Tests Should Help Us Improve Quality...22Tests Should Help Us Understand the SUT. . ...23Tests Should Reduce (and Not Introduce) Risk...23Tests Should Be Easy to Run ...25Tests Should Be Easy to Write and Maintain ...27Tests Should Require Minimal Maintenance asthe System Evolves Around Them ...29What's Next? .29Chapter 4. Philosophy of Test Automation ...31About This Chapter...31Why Is Philosophy Important?...31\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_thie5bysoj_file_d129d632800c45aa9e7421b30561f447_10207234_0_28\",\"images\":[],\"referenceIndex\":4,\"score\":0.5639580488204956,\"title\":\"xUnit Test Patterns\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\",\\\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\"]\\n【正文】:Testcase Class per Class... ...155Testcase Class per Feature. ...156Testcase Class per Fixture...156Choosing a Test Method Organization Strategy...158Test Naming Conventions...158Organizing Test Suites.. . ...160Running Groups of Tests ...160Running a Single Test...161Test Code Reuse...162Test Utility Method Locations ...163TestCase Inheritance and Reuse...163Test File Organization...164Built-in Self-Test...164Test Packages. ...164Test Dependencies ...165What's Next? ...165Chapter 13. Testing with Databases...167About This Chapter...167Testing with Databases...167Why Test with Databases?...168\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_thie5bysoj_file_d129d632800c45aa9e7421b30561f447_10207234_0_36\",\"images\":[],\"referenceIndex\":5,\"score\":0.563438355922699,\"title\":\"xUnit Test Patterns\",\"webSearch\":false}]",
                                null,
                                "{}"),
                            new(
                                null,
                                "api",
                                "长期记忆检索",
                                "memory",
                                "{\"memory_id\":\"ffd8be2352d84c6b9350e91c865b512e\",\"query\":\"总结xUnit Test Patterns中的内容\"}",
                                null,
                                "[]",
                                null,
                                "{\"memory_id\":\"ffd8be2352d84c6b9350e91c865b512e\",\"query\":\"总结xUnit Test Patterns中的内容\"}")
                        },
                        new List<ApplicationDocReference>
                        {
                            new(
                                "1",
                                "Visual Summary of the Pattern Language",
                                "file_d129d632800c45aa9e7421b30561f447_10207234",
                                "xUnit Test Patterns",
                                "【文档名】:xUnit Test Patterns\n【标题】:Visual Summary of the Pattern Language\n文档类型:[\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\",\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\"]\n【正文】:Minimize Untestable Code Buggy Tests Production Bugs Keep Test Logic Out of Production Developers Not Writing Tests Ensure Commensurate Effort and Responsibility High Test Maintenance CostKey to Visual Summary of the Pattern Language Chapter Name Chapter Name Sub-Category, Altemative Pattern Smell Pattern 1Pattern 2from Other Chapter'Cause of Smell Sub-Category variation, of Altemative Pattem十Pattem 1Smell Variation of Pattern used with Pattern leads toi Smell Variation described each other Alternative Pattem 2separatelyVISUAL SUMMARY OF THE PATTERN LANGUAGE\n",
                                new List<string>
                                {
                                    "http://docmind-api-cn-hangzhou.oss-cn-hangzhou.aliyuncs.com/1257896666798445/publicDocStructure/docmind-20250315-ee118d3555104aba9200f6cf525bae0a/19.png?Expires=1742655907&OSSAccessKeyId=LTAI5tFEK2uEApeeYzxNMEci&Signature=89B%2FoauA6i4g34LikZ06Z0PUHY4%3D&x-oss-process=image%2Fcrop%2Cx_232%2Cy_610%2Cw_964%2Ch_648",
                                    "http://docmind-api-cn-hangzhou.oss-cn-hangzhou.aliyuncs.com/1257896666798445/publicDocStructure/docmind-20250315-ee118d3555104aba9200f6cf525bae0a/19.png?Expires=1742655907&OSSAccessKeyId=LTAI5tFEK2uEApeeYzxNMEci&Signature=3VwzeegSrkzfQIcMDz2F3C2bTdg%3D&x-oss-process=image%2Fcrop%2Cx_227%2Cy_1305%2Cw_991%2Ch_302"
                                },
                                null),
                            new(
                                "2",
                                "xUnit Test Patterns",
                                "file_d129d632800c45aa9e7421b30561f447_10207234",
                                "xUnit Test Patterns",
                                "【文档名】:xUnit Test Patterns\n【标题】:xUnit Test Patterns\n文档类型:[\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\",\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\"]\n【正文】:Managing Shared Fixtures...103Accessing Shared Fixtures...103Triggering Shared Fixture Construction...104What's Next?...106Chapter 10. Result Verification...107About This Chapter ...107Making Tests Self-Checking...107Verify State or Behavior?...108State Verification...109Using Built-in Assertions ...110Delta Assertions...111External Result Verification ...111Verifying Behavior...112Procedural Behavior Verification...113Expected Behavior Specification . . ...113CONTENTSReducing Test Code Duplication...114Expected Objects...115Custom Assertions...116Outcome-Describing Verification Method ...117\n",
                                new List<string>(),
                                null),
                            new(
                                "3",
                                "xUnit Test Patterns",
                                "file_d129d632800c45aa9e7421b30561f447_10207234",
                                "xUnit Test Patterns",
                                "【文档名】:xUnit Test Patterns\n【标题】:xUnit Test Patterns\n文档类型:[\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\",\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\"]\n【正文】:?...168Issues with Databases...168Testing without Databases...169Testing the Database...171Testing Stored Procedures...172Testing the Data Access Layer...172Ensuring Developer Independence...173Testing with Databases (Again!)...173What's Next? ...174Chapter 14. A Roadmap to Effective Test Automation ...175About This Chapter...175Test Automation Difficulty .. ...175Roadmap to Highly Maintainable Automated Tests...176Exercise the Happy Path Code ...177Verify Direct Outputs of the Happy Path...178CONTENTSVerify Alternative Paths...178Verify Indirect Output Behavior...179\n",
                                new List<string>(),
                                null),
                            new(
                                "4",
                                "xUnit Test Patterns",
                                "file_d129d632800c45aa9e7421b30561f447_10207234",
                                "xUnit Test Patterns",
                                "【文档名】:xUnit Test Patterns\n【标题】:xUnit Test Patterns\n文档类型:[\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\",\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\"]\n【正文】:?..17viiiCONTENTSChapter 3. Goals of Test Automation ...19About This Chapter...19Why Test?...19Economics of Test Automation20Goals of Test Automation...21Tests Should Help Us Improve Quality...22Tests Should Help Us Understand the SUT. . ...23Tests Should Reduce (and Not Introduce) Risk...23Tests Should Be Easy to Run ...25Tests Should Be Easy to Write and Maintain ...27Tests Should Require Minimal Maintenance asthe System Evolves Around Them ...29What's Next? .29Chapter 4. Philosophy of Test Automation ...31About This Chapter...31Why Is Philosophy Important?...31\n",
                                new List<string>(),
                                null),
                            new(
                                "5",
                                "xUnit Test Patterns",
                                "file_d129d632800c45aa9e7421b30561f447_10207234",
                                "xUnit Test Patterns",
                                "【文档名】:xUnit Test Patterns\n【标题】:xUnit Test Patterns\n文档类型:[\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\",\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\"]\n【正文】:Testcase Class per Class... ...155Testcase Class per Feature. ...156Testcase Class per Fixture...156Choosing a Test Method Organization Strategy...158Test Naming Conventions...158Organizing Test Suites.. . ...160Running Groups of Tests ...160Running a Single Test...161Test Code Reuse...162Test Utility Method Locations ...163TestCase Inheritance and Reuse...163Test File Organization...164Built-in Self-Test...164Test Packages. ...164Test Dependencies ...165What's Next? ...165Chapter 13. Testing with Databases...167About This Chapter...167Testing with Databases...167Why Test with Databases?...168\n",
                                new List<string>(),
                                null)
                        }),
                    new ApplicationUsage(
                        new List<ApplicationModelUsage> { new("qwen-plus", 2591, 290) })));

        public static readonly RequestSnapshot<ApplicationRequest, ApplicationResponse> SinglePromptSse =
            new(
                "application-single-generation-text",
                new ApplicationRequest
                {
                    Input = new ApplicationInput { Prompt = "总结xUnit Test Patterns中的内容" },
                    Parameters = new ApplicationParameters
                    {
                        TopK = 100,
                        TopP = 0.8f,
                        Seed = 1234,
                        Temperature = 0.85f,
                        IncrementalOutput = true,
                        RagOptions = new ApplicationRagOptions
                        {
                            PipelineIds =
                            new List<string> { "thie5bysoj" }.AsReadOnly(), Tags =
                            new List<string> { "xUnit" }.AsReadOnly()
                        }
                    }
                },
                new ApplicationResponse(
                    "44862941-b743-9332-b49f-5f3db75a4873",
                    new ApplicationOutput(
                        "xUnit Test Patterns这本书讨论了测试自动化的目标，指出测试应该帮助我们提高质量<ref>[4]</ref>，理解被测系统(SUT)，减少（而不是引入）风险，并且应该易于运行、编写和维护。书中还提到了一些关于测试哲学的内容，强调了为什么哲学对于测试来说是重要的。\n\n此外，该书涵盖了如何管理共享的测试装置以及访问这些装置的方法<ref>[2]</ref>，并说明了触发共享装置构建的过程。在结果验证方面，书中区分了状态验证与行为验证，并介绍了使用内置断言、增量断言等技术来减少测试代码重复性的策略。\n\n书中也探讨了数据库相关的测试议题，包括没有数据库的情况下进行测试的方法，测试存储过程，数据访问层的测试，以及确保开发者独立性的同时进行数据库测试的重要性<ref>[3]</ref>。\n\n最后，在组织测试用例类方面，书中提出了按类、特性或装置创建测试用例类的不同策略，并讨论了选择测试方法组织策略、命名约定、组织测试套件、运行测试组或单一测试的方式，以及测试代码重用的位置等问题<ref>[5]</ref>。",
                        "stop",
                        "069db8223d514dab91185954dc5108de",
                        null,
                        new List<ApplicationDocReference>
                        {
                            new(
                                "2",
                                "xUnit Test Patterns",
                                "file_d129d632800c45aa9e7421b30561f447_10207234",
                                "xUnit Test Patterns",
                                "【文档名】:xUnit Test Patterns\n【标题】:xUnit Test Patterns\n文档类型:[\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\",\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\"]\n【正文】:Managing Shared Fixtures...103Accessing Shared Fixtures...103Triggering Shared Fixture Construction...104What's Next?...106Chapter 10. Result Verification...107About This Chapter ...107Making Tests Self-Checking...107Verify State or Behavior?...108State Verification...109Using Built-in Assertions ...110Delta Assertions...111External Result Verification ...111Verifying Behavior...112Procedural Behavior Verification...113Expected Behavior Specification . . ...113CONTENTSReducing Test Code Duplication...114Expected Objects...115Custom Assertions...116Outcome-Describing Verification Method ...117\n",
                                new List<string>(),
                                null),
                            new(
                                "3",
                                "xUnit Test Patterns",
                                "file_d129d632800c45aa9e7421b30561f447_10207234",
                                "xUnit Test Patterns",
                                "【文档名】:xUnit Test Patterns\n【标题】:xUnit Test Patterns\n文档类型:[\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\",\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\"]\n【正文】:?...168Issues with Databases...168Testing without Databases...169Testing the Database...171Testing Stored Procedures...172Testing the Data Access Layer...172Ensuring Developer Independence...173Testing with Databases (Again!)...173What's Next? ...174Chapter 14. A Roadmap to Effective Test Automation ...175About This Chapter...175Test Automation Difficulty .. ...175Roadmap to Highly Maintainable Automated Tests...176Exercise the Happy Path Code ...177Verify Direct Outputs of the Happy Path...178CONTENTSVerify Alternative Paths...178Verify Indirect Output Behavior...179\n",
                                new List<string>(),
                                null),
                            new(
                                "4",
                                "xUnit Test Patterns",
                                "file_d129d632800c45aa9e7421b30561f447_10207234",
                                "xUnit Test Patterns",
                                "【文档名】:xUnit Test Patterns\n【标题】:xUnit Test Patterns\n文档类型:[\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\",\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\"]\n【正文】:?..17viiiCONTENTSChapter 3. Goals of Test Automation ...19About This Chapter...19Why Test?...19Economics of Test Automation20Goals of Test Automation...21Tests Should Help Us Improve Quality...22Tests Should Help Us Understand the SUT. . ...23Tests Should Reduce (and Not Introduce) Risk...23Tests Should Be Easy to Run ...25Tests Should Be Easy to Write and Maintain ...27Tests Should Require Minimal Maintenance asthe System Evolves Around Them ...29What's Next? .29Chapter 4. Philosophy of Test Automation ...31About This Chapter...31Why Is Philosophy Important?...31\n",
                                new List<string>(),
                                null),
                            new(
                                "5",
                                "xUnit Test Patterns",
                                "file_d129d632800c45aa9e7421b30561f447_10207234",
                                "xUnit Test Patterns",
                                "【文档名】:xUnit Test Patterns\n【标题】:xUnit Test Patterns\n文档类型:[\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\",\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\"]\n【正文】:Testcase Class per Class... ...155Testcase Class per Feature. ...156Testcase Class per Fixture...156Choosing a Test Method Organization Strategy...158Test Naming Conventions...158Organizing Test Suites.. . ...160Running Groups of Tests ...160Running a Single Test...161Test Code Reuse...162Test Utility Method Locations ...163TestCase Inheritance and Reuse...163Test File Organization...164Built-in Self-Test...164Test Packages. ...164Test Dependencies ...165What's Next? ...165Chapter 13. Testing with Databases...167About This Chapter...167Testing with Databases...167Why Test with Databases?...168\n",
                                new List<string>(),
                                null),
                        }),
                    new ApplicationUsage(
                        new List<ApplicationModelUsage> { new("qwen-max-latest", 2304, 244) })));

        public static readonly RequestSnapshot<ApplicationRequest, ApplicationResponse> SinglePromptWithThoughtsNoSse =
            new(
                "application-single-generation-text-with-thought",
                new ApplicationRequest
                {
                    Input = new ApplicationInput { Prompt = "总结xUnit Test Patterns中的内容" },
                    Parameters = new ApplicationParameters
                    {
                        TopK = 100,
                        TopP = 0.8f,
                        Seed = 1234,
                        Temperature = 0.85f,
                        RagOptions = new ApplicationRagOptions
                        {
                            PipelineIds = new List<string> { "thie5bysoj" }.AsReadOnly(),
                            FileIds = new List<string> { "file_d129d632800c45aa9e7421b30561f447_10207234" }.AsReadOnly()
                        },
                        HasThoughts = true
                    }
                },
                new ApplicationResponse(
                    "b5819020-e5aa-9481-8c9d-e11797f191d8",
                    new ApplicationOutput(
                        "《xUnit Test Patterns》是一本系统介绍单元测试模式与最佳实践的指南，主要帮助开发者设计可维护、高效且可靠的自动化测试。以下是其核心内容的总结：\n\n---\n\n### **核心主题与内容**\n1. **测试模式与反模式**\n   - **关键模式**：如共享夹具管理（Shared Fixtures）、自定义断言（Custom Assertions）、预期对象（Expected Objects）等，用于解决测试代码重复、依赖管理等问题。\n   - **反模式与问题**：如不可测试代码（Untestable Code）、测试逻辑混入生产代码（Test Logic in Production）、高维护成本（High Test Maintenance Cost）等，分析其成因与规避策略。\n\n2. **测试夹具管理**\n   - **共享夹具**：如何构造、触发和访问共享测试环境（如数据库连接），避免测试间的副作用。\n   - **数据库测试**：讨论是否依赖数据库进行测试、如何测试数据访问层、存储过程，以及确保开发者独立性的策略（如使用测试替身）。\n\n3. **测试验证策略**\n   - **状态验证**：通过断言检查系统状态（如使用内置断言、Delta断言）。\n   - **行为验证**：验证系统是否按预期调用方法（如模拟对象、过程式验证）。\n   - **减少代码重复**：通过自定义断言、预期对象和验证方法统一结果检查逻辑。\n\n4. **测试自动化哲学与目标**\n   - **目标**：提升代码质量、降低风险、易于编写和维护测试。\n   - **经济性**：平衡测试投入与收益，优先覆盖关键路径（Happy Path）和替代路径（Alternative Paths）。\n\n5. **测试代码组织**\n   - **结构化策略**：按类（Testcase Class per Class）、功能（Testcase Class per Feature）或夹具（Testcase Class per Fixture）组织测试用例。\n   - **命名与套件管理**：使用清晰命名约定、分组测试套件（Test Suites）和依赖管理。\n\n6. **测试维护与演进**\n   - **最小化维护成本**：通过模式（如测试工具方法、继承复用）适应系统变化。\n   - **自检测试（Built-in Self-Test）**：确保测试本身的可信性。\n\n---\n\n### **书籍结构特点**\n- **问题驱动**：每章围绕具体问题（如“如何处理共享夹具？”）展开，提供模式、替代方案和权衡。\n- **视觉化总结**：通过图表展示模式间的关系，帮助读者理解复杂概念。\n- **实践导向**：结合代码示例与真实场景，指导如何应用模式解决测试中的常见痛点。\n\n---\n\n### **适用场景**\n- 开发中遇到测试代码重复、脆弱测试（Fragile Tests）或高维护成本时。\n- 需要设计复杂测试场景（如数据库交互、异步行为）时。\n- 团队希望建立统一的测试实践与规范时。\n\n通过遵循书中模式，开发者能构建更健壮、可维护的测试体系，最终提升软件质量和开发效率。",
                        "stop",
                        "9d81b84e95f844c29ee825ad8bb647bb",
                        new List<ApplicationOutputThought>
                        {
                            new(
                                null,
                                "agentRag",
                                "知识检索",
                                "rag",
                                "{}",
                                null,
                                "[{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:Visual Summary of the Pattern Language\\n文档类型:[\\\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\",\\\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\"]\\n【正文】:Minimize Untestable Code Buggy Tests Production Bugs Keep Test Logic Out of Production Developers Not Writing Tests Ensure Commensurate Effort and Responsibility High Test Maintenance CostKey to Visual Summary of the Pattern Language Chapter Name Chapter Name Sub-Category, Altemative Pattern Smell Pattern 1Pattern 2from Other Chapter'Cause of Smell Sub-Category variation, of Altemative Pattem十Pattem 1Smell Variation of Pattern used with Pattern leads toi Smell Variation described each other Alternative Pattem 2separatelyVISUAL SUMMARY OF THE PATTERN LANGUAGE\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_thie5bysoj_file_d129d632800c45aa9e7421b30561f447_10207234_1_3\",\"images\":[\"http://docmind-api-cn-hangzhou.oss-cn-hangzhou.aliyuncs.com/1257896666798445/publicDocStructure/docmind-20250315-ee118d3555104aba9200f6cf525bae0a/19.png?Expires=1742716762&OSSAccessKeyId=LTAI5tFEK2uEApeeYzxNMEci&Signature=4CddPVeeyxgrXe5axUspV6zXnS8%3D&x-oss-process=image%2Fcrop%2Cx_232%2Cy_610%2Cw_964%2Ch_648\",\"http://docmind-api-cn-hangzhou.oss-cn-hangzhou.aliyuncs.com/1257896666798445/publicDocStructure/docmind-20250315-ee118d3555104aba9200f6cf525bae0a/19.png?Expires=1742716762&OSSAccessKeyId=LTAI5tFEK2uEApeeYzxNMEci&Signature=hRkFcnwAiV7LSHw69WvJJ6fXEV0%3D&x-oss-process=image%2Fcrop%2Cx_227%2Cy_1305%2Cw_991%2Ch_302\"],\"referenceIndex\":1,\"score\":0.5756075978279114,\"title\":\"Visual Summary of the Pattern Language\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\",\\\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\"]\\n【正文】:Managing Shared Fixtures...103Accessing Shared Fixtures...103Triggering Shared Fixture Construction...104What's Next?...106Chapter 10. Result Verification...107About This Chapter ...107Making Tests Self-Checking...107Verify State or Behavior?...108State Verification...109Using Built-in Assertions ...110Delta Assertions...111External Result Verification ...111Verifying Behavior...112Procedural Behavior Verification...113Expected Behavior Specification . . ...113CONTENTSReducing Test Code Duplication...114Expected Objects...115Custom Assertions...116Outcome-Describing Verification Method ...117\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_thie5bysoj_file_d129d632800c45aa9e7421b30561f447_10207234_0_33\",\"images\":[],\"referenceIndex\":2,\"score\":0.5756075978279114,\"title\":\"xUnit Test Patterns\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\",\\\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\"]\\n【正文】:?...168Issues with Databases...168Testing without Databases...169Testing the Database...171Testing Stored Procedures...172Testing the Data Access Layer...172Ensuring Developer Independence...173Testing with Databases (Again!)...173What's Next? ...174Chapter 14. A Roadmap to Effective Test Automation ...175About This Chapter...175Test Automation Difficulty .. ...175Roadmap to Highly Maintainable Automated Tests...176Exercise the Happy Path Code ...177Verify Direct Outputs of the Happy Path...178CONTENTSVerify Alternative Paths...178Verify Indirect Output Behavior...179\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_thie5bysoj_file_d129d632800c45aa9e7421b30561f447_10207234_0_37\",\"images\":[],\"referenceIndex\":3,\"score\":0.5697553753852844,\"title\":\"xUnit Test Patterns\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\",\\\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\"]\\n【正文】:?..17viiiCONTENTSChapter 3. Goals of Test Automation ...19About This Chapter...19Why Test?...19Economics of Test Automation20Goals of Test Automation...21Tests Should Help Us Improve Quality...22Tests Should Help Us Understand the SUT. . ...23Tests Should Reduce (and Not Introduce) Risk...23Tests Should Be Easy to Run ...25Tests Should Be Easy to Write and Maintain ...27Tests Should Require Minimal Maintenance asthe System Evolves Around Them ...29What's Next? .29Chapter 4. Philosophy of Test Automation ...31About This Chapter...31Why Is Philosophy Important?...31\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_thie5bysoj_file_d129d632800c45aa9e7421b30561f447_10207234_0_28\",\"images\":[],\"referenceIndex\":4,\"score\":0.5639580488204956,\"title\":\"xUnit Test Patterns\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"xUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\",\\\"XUNIT TEST Yoog PATTERNS REFACTORING TEST CODE GERARD)1MESZAROS\\\"]\\n【正文】:Testcase Class per Class... ...155Testcase Class per Feature. ...156Testcase Class per Fixture...156Choosing a Test Method Organization Strategy...158Test Naming Conventions...158Organizing Test Suites.. . ...160Running Groups of Tests ...160Running a Single Test...161Test Code Reuse...162Test Utility Method Locations ...163TestCase Inheritance and Reuse...163Test File Organization...164Built-in Self-Test...164Test Packages. ...164Test Dependencies ...165What's Next? ...165Chapter 13. Testing with Databases...167About This Chapter...167Testing with Databases...167Why Test with Databases?...168\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_thie5bysoj_file_d129d632800c45aa9e7421b30561f447_10207234_0_36\",\"images\":[],\"referenceIndex\":5,\"score\":0.563438355922699,\"title\":\"xUnit Test Patterns\",\"webSearch\":false}]",
                                null,
                                "{}"),
                            new(
                                "好的，我现在需要总结用户提供的xUnit Test Patterns中的内容。首先，用户给了几个文档片段，每个片段都来自同一本书的不同部分。我需要仔细分析这些片段，看看里面提到的关键点和主题。\n\n第一个文档提到了“Visual Summary of the Pattern Language”，里面有几个关键点，比如Minimize Untestable Code，Buggy Tests，Production Bugs，还有测试维护成本高的问题。可能这本书在讲如何设计和管理单元测试的模式，避免常见的问题，比如不可测试的代码和测试逻辑混入生产环境。\n\n第二个文档片段讲的是“Managing Shared Fixtures”和结果验证（Result Verification）。这里提到共享夹具的管理、触发构建，以及如何验证测试结果，包括状态验证和行为验证，还有减少测试代码重复的方法，比如使用Expected Objects和Custom Assertions。这说明书中详细讨论了测试夹具的设计和如何有效验证测试结果，避免重复代码。\n\n第三个文档涉及数据库测试的问题，比如如何测试数据访问层、存储过程，以及确保开发者独立性的策略。这部分可能讨论在测试中使用数据库的挑战和解决方案，例如是否使用数据库进行测试，或者如何隔离测试以避免依赖。\n\n第四个文档提到了测试自动化的目标和哲学，包括测试的经济性、提高质量、理解系统、降低风险、易于运行和维护等。这说明书中不仅提供技术模式，还强调测试策略和哲学的重要性，指导如何有效实施自动化测试。\n\n第五个文档讨论了测试用例的组织策略，比如按类、功能或夹具组织测试用例类，命名约定，测试套件的管理，以及测试代码的重用方法。这部分可能涉及如何结构化测试代码，提高可维护性和可读性。\n\n综合这些片段，xUnit Test Patterns似乎是一本关于单元测试设计和最佳实践的指南，涵盖测试模式、夹具管理、结果验证、数据库测试、测试自动化策略和测试代码组织。重点在于如何编写可维护、可靠且高效的测试，避免常见陷阱，如不可测试的代码、测试逻辑污染生产代码、高维护成本等。书中可能还讨论了如何在不同情境下选择适当的测试策略，例如是否使用共享夹具，如何处理数据库依赖，以及如何组织测试代码结构以提高复用性。\n\n现在需要将这些分析整理成一个简明扼要的总结，突出主要主题和关键点，确保涵盖各个文档片段提到的内容，并指出这本书的整体目的和结构。可能需要分点说明，让用户清晰了解书中的核心内容。",
                                "reasoning",
                                "思考过程",
                                "reasoning",
                                null,
                                null,
                                null,
                                "好的，我现在需要总结用户提供的xUnit Test Patterns中的内容。首先，用户给了几个文档片段，每个片段都来自同一本书的不同部分。我需要仔细分析这些片段，看看里面提到的关键点和主题。\n\n第一个文档提到了“Visual Summary of the Pattern Language”，里面有几个关键点，比如Minimize Untestable Code，Buggy Tests，Production Bugs，还有测试维护成本高的问题。可能这本书在讲如何设计和管理单元测试的模式，避免常见的问题，比如不可测试的代码和测试逻辑混入生产环境。\n\n第二个文档片段讲的是“Managing Shared Fixtures”和结果验证（Result Verification）。这里提到共享夹具的管理、触发构建，以及如何验证测试结果，包括状态验证和行为验证，还有减少测试代码重复的方法，比如使用Expected Objects和Custom Assertions。这说明书中详细讨论了测试夹具的设计和如何有效验证测试结果，避免重复代码。\n\n第三个文档涉及数据库测试的问题，比如如何测试数据访问层、存储过程，以及确保开发者独立性的策略。这部分可能讨论在测试中使用数据库的挑战和解决方案，例如是否使用数据库进行测试，或者如何隔离测试以避免依赖。\n\n第四个文档提到了测试自动化的目标和哲学，包括测试的经济性、提高质量、理解系统、降低风险、易于运行和维护等。这说明书中不仅提供技术模式，还强调测试策略和哲学的重要性，指导如何有效实施自动化测试。\n\n第五个文档讨论了测试用例的组织策略，比如按类、功能或夹具组织测试用例类，命名约定，测试套件的管理，以及测试代码的重用方法。这部分可能涉及如何结构化测试代码，提高可维护性和可读性。\n\n综合这些片段，xUnit Test Patterns似乎是一本关于单元测试设计和最佳实践的指南，涵盖测试模式、夹具管理、结果验证、数据库测试、测试自动化策略和测试代码组织。重点在于如何编写可维护、可靠且高效的测试，避免常见陷阱，如不可测试的代码、测试逻辑污染生产代码、高维护成本等。书中可能还讨论了如何在不同情境下选择适当的测试策略，例如是否使用共享夹具，如何处理数据库依赖，以及如何组织测试代码结构以提高复用性。\n\n现在需要将这些分析整理成一个简明扼要的总结，突出主要主题和关键点，确保涵盖各个文档片段提到的内容，并指出这本书的整体目的和结构。可能需要分点说明，让用户清晰了解书中的核心内容。",
                                null)
                        },
                        null),
                    new ApplicationUsage(
                        new List<ApplicationModelUsage> { new("deepseek-r1", 1129, 1126) })));

        public static readonly RequestSnapshot<ApplicationRequest, ApplicationResponse> SinglePromptWithMemoryNoSse =
            new(
                "application-single-generation-text-with-memory",
                new ApplicationRequest
                {
                    Input = new ApplicationInput
                    {
                        Prompt = "我爱吃面食", MemoryId = "ffd8be2352d84c6b9350e91c865b512e"
                    },
                    Parameters = new ApplicationParameters
                    {
                        TopK = 100,
                        TopP = 0.8f,
                        Seed = 1234,
                        Temperature = 0.85f,
                        HasThoughts = true
                    }
                },
                new ApplicationResponse(
                    "8cea84fe-2770-91b0-a6d1-e1e8ef176fa6",
                    new ApplicationOutput(
                        "那您一定会对面条、馒头或者饺子这些美食很感兴趣呢！如果您有特定的面食问题或者需要推荐相关的菜品，可以告诉我，我很乐意为您提供帮助<ref>[1]</ref>。",
                        "stop",
                        "cd395cb8d4604db786a14555fdcffa1a",
                        new List<ApplicationOutputThought>
                        {
                            new(null, "agentRag", "知识检索", "rag", "{}", null, "[]", null, "{}"),
                            new(
                                null,
                                "api",
                                "长期记忆检索",
                                "memory",
                                "{\"memory_id\":\"ffd8be2352d84c6b9350e91c865b512e\",\"query\":\"我爱吃面食\"}",
                                null,
                                "[\"[2025-3-16 20:47:40 周日] 用户喜欢吃面食。\"]",
                                null,
                                "{\"memory_id\":\"ffd8be2352d84c6b9350e91c865b512e\",\"query\":\"我爱吃面食\"}")
                        },
                        null),
                    new ApplicationUsage(
                        new List<ApplicationModelUsage> { new("qwen-plus", 1201, 43) })));

        public static readonly RequestSnapshot<ApplicationRequest<TestApplicationBizParam>, ApplicationResponse>
            WorkflowNoSse =
                new(
                    "application-workflow",
                    new ApplicationRequest<TestApplicationBizParam>
                    {
                        Input = new ApplicationInput<TestApplicationBizParam>
                        {
                            BizParams = new TestApplicationBizParam("code"), Prompt = "请你跟我这样说"
                        },
                        Parameters = new ApplicationParameters
                        {
                            TopK = 100,
                            TopP = 0.8f,
                            Seed = 1234,
                            Temperature = 0.85f,
                        }
                    },
                    new ApplicationResponse(
                        "10990f51-e2d0-9338-9c52-319af5f4858b",
                        new ApplicationOutput("code", "stop", "5a20b47dac2f43a7b1cbb8924ca66c47", null, null),
                        new ApplicationUsage(null)));

        public static readonly RequestSnapshot<ApplicationRequest<TestApplicationBizParam>, ApplicationResponse>
            WorkflowInDifferentWorkSpaceNoSse =
                new(
                    "application-workflow",
                    new ApplicationRequest<TestApplicationBizParam>
                    {
                        Input = new ApplicationInput<TestApplicationBizParam>
                        {
                            BizParams = new TestApplicationBizParam("code"), Prompt = "请你跟我这样说"
                        },
                        Parameters = new ApplicationParameters
                        {
                            TopK = 100,
                            TopP = 0.8f,
                            Seed = 1234,
                            Temperature = 0.85f,
                        },
                        WorkspaceId = "workspaceId"
                    },
                    new ApplicationResponse(
                        "10990f51-e2d0-9338-9c52-319af5f4858b",
                        new ApplicationOutput("code", "stop", "5a20b47dac2f43a7b1cbb8924ca66c47", null, null),
                        new ApplicationUsage(null)));

        public static readonly RequestSnapshot<ApplicationRequest, ApplicationResponse> ConversationSessionIdNoSse =
            new(
                "application-conversation-generation-session-id",
                new ApplicationRequest
                {
                    Input =
                        new ApplicationInput
                        {
                            Prompt = "总结一下第一本书的内容", SessionId = "9995da2046a04b448dc5a562563f4835"
                        },
                    Parameters = new ApplicationParameters
                    {
                        TopK = 100,
                        TopP = 0.8f,
                        Seed = 1234,
                        Temperature = 0.85f,
                        RagOptions = new ApplicationRagOptions
                        {
                            PipelineIds = new List<string> { "ll6yfcnxjg" }.AsReadOnly(),
                            MetadataFilter = new Dictionary<string, string> { { "docType", "电子书" } }
                        },
                        HasThoughts = true
                    }
                },
                new ApplicationResponse(
                    "703ba252-43c0-9a05-a656-1c2bf03d21dc",
                    new ApplicationOutput(
                        "根据知识库中提供的《xUnit Test Patterns》内容片段，以下是其核心内容的总结：\n\n---\n\n### **《xUnit Test Patterns: Refactoring Test Code》核心内容**\n1. **核心目标**  \n   系统化解决单元测试中的常见问题，提供可复用的测试模式，帮助编写**可维护、高效、可靠**的测试代码。\n\n2. **关键主题**  \n   - **测试代码重构**  \n     - 识别测试代码的\"坏味道\"（Test Smells），例如冗长的测试方法、重复的测试逻辑、脆弱的依赖等。\n     - 提出重构策略，如使用 **Creation Method** 简化对象构造、**Implicit Setup** 隐式初始化测试夹具等。\n   - **测试自动化策略**  \n     - 强调\"测试即代码\"（Test as Code），通过设计模式（如 **Test Double**、**Test Stub**）隔离外部依赖。\n     - 探讨测试与数据库交互的挑战（如事务管理、数据污染），并给出解决方案（如 **Fresh Fixture** 模式）。\n   - **测试验证模式**  \n     - **State Verification**：验证被测对象的状态变化（如属性值）。\n     - **Behavior Verification**：验证对象间的交互行为（如方法调用次数）。\n     - **Custom Assertion**：通过自定义断言提高测试可读性。\n   - **测试组织结构**  \n     - 按类、功能或夹具组织测试用例（如 **Testcase Class per Fixture**）。\n     - 管理测试套件（Test Suites）和测试依赖关系。\n\n3. **典型模式示例**  \n   - **Fixture 管理**  \n     - **Delegated Setup**：将夹具构造逻辑委托给辅助方法。\n     - **Prebuilt Fixture**：预构建共享夹具以提升性能。\n   - **结果验证**  \n     - **Delta Assertion**：仅验证关键变化值，避免全量断言。\n     - **Guard Assertion**：前置条件检查，防止测试误报。\n   - **测试替身（Test Doubles）**  \n     - **Test Stub**：模拟外部依赖的返回值。\n     - **Mock Object**：验证对象间的交互是否符合预期。\n\n4. **实践指导**  \n   - 提出从\"Happy Path\"（正常流程）到复杂场景的测试演进路线。\n   - 强调测试的**独立性**（避免测试间依赖）和**自检能力**（无需人工验证结果）。\n\n---\n\n### **适用场景**\n- 开发人员需解决测试代码**臃肿、脆弱或低效**的问题。\n- 团队需建立**统一、可扩展**的自动化测试规范。\n- 涉及**数据库、外部服务**等复杂依赖的测试设计。\n\n书中内容以模式目录形式呈现，可直接作为工具手册使用。",
                        "stop",
                        "9995da2046a04b448dc5a562563f4835",
                        new List<ApplicationOutputThought>
                        {
                            new(
                                null,
                                "agentRag",
                                "知识检索",
                                "rag",
                                "{}",
                                null,
                                "[{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"电子书\\\"]\\n关键字:[\\\"xUnit\\\"]\\n【正文】:Refactoring a Test...xlvPARTI.The Narratives····.1Chapter 1. A Brief Tour3About This Chapter3The Simplest Test Automation Strategy ThatCould Possibly Work3Development Process4Customer Tests5Unit Tests . . .Design for TestabilityTest Organization···What's Next?Chapter 2. Test Smells . . ..·····9About This Chapter9An Introduction to Test Smells..9What's a Test Smell? . . ...10Kinds of Test Smells ...10What to Do about Smells?..11A Catalog of Smells·...12The Project Smells...12The Behavior Smells. . ...13The Code Smells..16What's Next?..17viiiCONTENTSChapter 3. Goals of Test Automation ...19\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_ll6yfcnxjg_file_d129d632800c45aa9e7421b30561f447_10207234_0_27\",\"images\":[],\"referenceIndex\":1,\"score\":0.5722247362136841,\"title\":\"xUnit Test Patterns\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"电子书\\\"]\\n关键字:[\\\"xUnit\\\"]\\n【正文】:Delegated Setup. ...411Creation Method...415Implicit Setup...424Prebuilt Fixture...429Lazy Setup...435Suite Fixture Setup ...。。。441Setup Decorator...447Chained Tests...454Chapter 21. Result Verification Patterns p·...461State Verification...462Behavior Verification...468Custom Assertion...474Delta Assertion...485Guard Assertion...490Unfinished Test Assertion...494Chapter 22. Fixture Teardown Patterns...499Garbage-Collected Teardown...500CONTENTSAutomated Teardown...503In-line Teardown...509Implicit Teardown...516Chapter 23. Test Double Patterns ...521Test Double...522Test Stub...529\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_ll6yfcnxjg_file_d129d632800c45aa9e7421b30561f447_10207234_0_40\",\"images\":[],\"referenceIndex\":2,\"score\":0.5684536695480347,\"title\":\"xUnit Test Patterns\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"电子书\\\"]\\n关键字:[\\\"xUnit\\\"]\\n【正文】:Testcase Class per Class... ...155Testcase Class per Feature. ...156Testcase Class per Fixture...156Choosing a Test Method Organization Strategy...158Test Naming Conventions...158Organizing Test Suites.. . ...160Running Groups of Tests ...160Running a Single Test...161Test Code Reuse...162Test Utility Method Locations ...163TestCase Inheritance and Reuse...163Test File Organization...164Built-in Self-Test...164Test Packages. ...164Test Dependencies ...165What's Next? ...165Chapter 13. Testing with Databases...167About This Chapter...167Testing with Databases...167Why Test with Databases?...168\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_ll6yfcnxjg_file_d129d632800c45aa9e7421b30561f447_10207234_0_36\",\"images\":[],\"referenceIndex\":3,\"score\":0.5677477717399597,\"title\":\"xUnit Test Patterns\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"电子书\\\"]\\n关键字:[\\\"xUnit\\\"]\\n【正文】:?...168Issues with Databases...168Testing without Databases...169Testing the Database...171Testing Stored Procedures...172Testing the Data Access Layer...172Ensuring Developer Independence...173Testing with Databases (Again!)...173What's Next? ...174Chapter 14. A Roadmap to Effective Test Automation ...175About This Chapter...175Test Automation Difficulty .. ...175Roadmap to Highly Maintainable Automated Tests...176Exercise the Happy Path Code ...177Verify Direct Outputs of the Happy Path...178CONTENTSVerify Alternative Paths...178Verify Indirect Output Behavior...179\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_ll6yfcnxjg_file_d129d632800c45aa9e7421b30561f447_10207234_0_37\",\"images\":[],\"referenceIndex\":4,\"score\":0.5674441456794739,\"title\":\"xUnit Test Patterns\",\"webSearch\":false},{\"content\":\"【文档名】:xUnit Test Patterns\\n【标题】:xUnit Test Patterns\\n文档类型:[\\\"电子书\\\"]\\n关键字:[\\\"xUnit\\\"]\\n【正文】:Managing Shared Fixtures...103Accessing Shared Fixtures...103Triggering Shared Fixture Construction...104What's Next?...106Chapter 10. Result Verification...107About This Chapter ...107Making Tests Self-Checking...107Verify State or Behavior?...108State Verification...109Using Built-in Assertions ...110Delta Assertions...111External Result Verification ...111Verifying Behavior...112Procedural Behavior Verification...113Expected Behavior Specification . . ...113CONTENTSReducing Test Code Duplication...114Expected Objects...115Custom Assertions...116Outcome-Describing Verification Method ...117\\n\",\"dataId\":\"file_d129d632800c45aa9e7421b30561f447_10207234\",\"dataName\":\"xUnit Test Patterns\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_ll6yfcnxjg_file_d129d632800c45aa9e7421b30561f447_10207234_0_33\",\"images\":[],\"referenceIndex\":5,\"score\":0.5672414302825928,\"title\":\"xUnit Test Patterns\",\"webSearch\":false}]",
                                null,
                                "{}"),
                            new(
                                "好的，用户让我总结一下第一本书的内容，也就是《xUnit Test Patterns》。首先，我需要回顾知识库中的相关文档。用户之前已经询问过xUnit相关的书籍，现在需要更深入的信息。\n\n首先，知识库中有多个条目提到了这本书，每个条目都引用了书中的不同章节和内容。例如，文档[1]提到了测试自动化策略、开发流程、客户测试、单元测试和测试组织。文档[2]和[3]提到了具体的模式，如Delegated Setup、Creation Method、Fixture管理、测试验证模式等。文档[4]和[5]讨论了数据库测试、测试自动化路线图以及结果验证的方法。\n\n用户的问题是要总结第一本书的内容，即《xUnit Test Patterns》。根据知识库中的信息，这本书主要围绕测试模式和最佳实践展开，涵盖测试结构、组织、验证、固件管理等方面。需要将这些分散的信息整合起来，形成一个结构化的总结。\n\n接下来，我需要确定这本书的核心主题。从各个文档的正文来看，书中讨论了测试代码的坏味道（Test Smells）、重构测试代码的方法、测试自动化策略、测试固件的管理（如Setup和Teardown模式）、测试验证模式（如状态验证和行为验证）、测试替身（Test Doubles）如Test Stub等。此外，还涉及测试组织结构、测试用例类设计、数据库测试的策略和挑战。\n\n用户可能希望了解这本书的整体框架和关键点，而不仅仅是零散的章节内容。因此，总结时需要分模块或主题来组织信息，例如核心概念、测试模式、实践策略、高级主题等。同时，要突出书中的核心贡献，如对测试模式的分类和解决方案。\n\n需要注意的是，知识库中的信息可能不完整，但可以基于现有内容进行合理推断。例如，文档[1]提到了测试坏味道的分类，文档[2]和[5]详细描述了不同的测试模式和验证方法，文档[3]和[4]讨论了测试组织和数据库测试的挑战。结合这些，可以推断该书系统性地介绍了如何编写可维护、高效的测试代码，解决测试中的常见问题。\n\n最后，需要确保总结简洁明了，涵盖主要章节和关键概念，让用户快速了解这本书的价值和内容结构。可能还需要指出这本书适合的读者群体，如测试工程师、开发人员以及需要提高测试代码质量的团队。",
                                "reasoning",
                                "思考过程",
                                "reasoning",
                                null,
                                null,
                                null,
                                "好的，用户让我总结一下第一本书的内容，也就是《xUnit Test Patterns》。首先，我需要回顾知识库中的相关文档。用户之前已经询问过xUnit相关的书籍，现在需要更深入的信息。\n\n首先，知识库中有多个条目提到了这本书，每个条目都引用了书中的不同章节和内容。例如，文档[1]提到了测试自动化策略、开发流程、客户测试、单元测试和测试组织。文档[2]和[3]提到了具体的模式，如Delegated Setup、Creation Method、Fixture管理、测试验证模式等。文档[4]和[5]讨论了数据库测试、测试自动化路线图以及结果验证的方法。\n\n用户的问题是要总结第一本书的内容，即《xUnit Test Patterns》。根据知识库中的信息，这本书主要围绕测试模式和最佳实践展开，涵盖测试结构、组织、验证、固件管理等方面。需要将这些分散的信息整合起来，形成一个结构化的总结。\n\n接下来，我需要确定这本书的核心主题。从各个文档的正文来看，书中讨论了测试代码的坏味道（Test Smells）、重构测试代码的方法、测试自动化策略、测试固件的管理（如Setup和Teardown模式）、测试验证模式（如状态验证和行为验证）、测试替身（Test Doubles）如Test Stub等。此外，还涉及测试组织结构、测试用例类设计、数据库测试的策略和挑战。\n\n用户可能希望了解这本书的整体框架和关键点，而不仅仅是零散的章节内容。因此，总结时需要分模块或主题来组织信息，例如核心概念、测试模式、实践策略、高级主题等。同时，要突出书中的核心贡献，如对测试模式的分类和解决方案。\n\n需要注意的是，知识库中的信息可能不完整，但可以基于现有内容进行合理推断。例如，文档[1]提到了测试坏味道的分类，文档[2]和[5]详细描述了不同的测试模式和验证方法，文档[3]和[4]讨论了测试组织和数据库测试的挑战。结合这些，可以推断该书系统性地介绍了如何编写可维护、高效的测试代码，解决测试中的常见问题。\n\n最后，需要确保总结简洁明了，涵盖主要章节和关键概念，让用户快速了解这本书的价值和内容结构。可能还需要指出这本书适合的读者群体，如测试工程师、开发人员以及需要提高测试代码质量的团队。",
                                null)
                        },
                        null),
                    new ApplicationUsage(
                        new List<ApplicationModelUsage> { new("deepseek-r1", 1283, 1081) })));

        public static readonly RequestSnapshot<ApplicationRequest, ApplicationResponse> ConversationMessageNoSse =
            new(
                "application-conversation-generation-message",
                new ApplicationRequest
                {
                    Input = new ApplicationInput
                    {
                        Messages =
                            new List<ApplicationMessage>
                            {
                                ApplicationMessage.System("You are a helpful assistant."),
                                ApplicationMessage.User("你是谁？"),
                                ApplicationMessage.Assistant("我是阿里云开发的大规模语言模型，我叫通义千问。"),
                                ApplicationMessage.User("哪些人的主食偏好是米饭？"),
                            }.AsReadOnly(),
                    },
                    Parameters = new ApplicationParameters
                    {
                        TopK = 100,
                        TopP = 0.8f,
                        Seed = 1234,
                        Temperature = 0.85f,
                        RagOptions = new ApplicationRagOptions
                        {
                            PipelineIds = new List<string> { "e6md69132k" }.AsReadOnly(),
                            StructuredFilter = new Dictionary<string, object> { { "年龄", 14 } }
                        },
                        HasThoughts = true
                    }
                },
                new ApplicationResponse(
                    "d42335b3-fcb2-9d11-b651-29562ac02abe",
                    new ApplicationOutput(
                        "米饭作为主食，深受许多国家和地区人们的喜爱。以下是一些以米饭为主食的群体：\n\n1. **中国人**：尤其在南方地区，米饭是大多数家庭的主要食物。从广东、福建到四川、云南，米饭搭配各种菜肴构成了日常饮食的重要部分。\n\n2. **日本人**：日本料理中，白米饭占据核心地位，无论是便当中的小碗饭还是寿司的基础，都体现了米饭在日本饮食文化中的重要性。\n\n3. **韩国人**：韩国家庭餐桌上的“石锅拌饭”、“紫菜包饭”等经典菜品，反映了米饭在韩国饮食习惯里的不可或缺。\n\n4. **东南亚各国居民**（如泰国、越南、菲律宾、印尼等）：这些地区的传统美食几乎都离不开米饭，像泰国香米更是闻名全球，成为该国饮食文化的象征之一。\n\n5. **印度及南亚次大陆部分地区人群**：虽然面饼（Roti/Naan）也很受欢迎，但米饭特别是与咖喱一起食用时，同样是众多印度家庭及其他南亚国家（如孟加拉国、斯里兰卡等）的重要主食选择。\n\n6. **中东部分国家的人们**：尽管面包可能是更普遍的选择，但在一些特定场合或日常饮食中，例如搭配烤肉、炖菜时，米饭同样被广泛使用。\n\n总体而言，由于其易于种植、营养丰富且能够很好地与其他食材结合的特点，米饭成为了上述地区人们世代相传的主要食物来源之一。",
                        "stop",
                        "9e2cf8c81f9a4fbe900a1f04b8522244",
                        new List<ApplicationOutputThought>
                        {
                            new(
                                null,
                                "agentRag",
                                "知识检索",
                                "rag",
                                "{}",
                                null,
                                "[{\"content\":\"【文档名】:用户食物偏好\\n名字:小明\\n主食偏好:面食\\n年龄:14\\n\",\"dataId\":\"table_df4b06e8931545b4b0a65e011087c197_10207234_1\",\"dataName\":\"用户食物偏好\",\"display\":true,\"id\":\"llm-lposod7dkhzvfgmy_e6md69132k_table_df4b06e8931545b4b0a65e011087c197_10207234_1\",\"referenceIndex\":1,\"score\":0.2185690850019455,\"webSearch\":false}]",
                                null,
                                "{}"),
                            new(
                                null,
                                "api",
                                "长期记忆检索",
                                "memory",
                                "{\"memory_id\":\"ffd8be2352d84c6b9350e91c865b512e\",\"query\":\"哪些人的主食偏好是米饭？\"}",
                                null,
                                "[]",
                                null,
                                "{\"memory_id\":\"ffd8be2352d84c6b9350e91c865b512e\",\"query\":\"哪些人的主食偏好是米饭？\"}")
                        },
                        null),
                    new ApplicationUsage(
                        new List<ApplicationModelUsage> { new("qwen-plus", 344, 311) })));
    }
}

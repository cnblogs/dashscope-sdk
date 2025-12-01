using Cnblogs.DashScope.Core;

namespace Cnblogs.DashScope.Sample.Multimodal;

public class OcrFormulaRecognitionSample: MultimodalSample
{
    /// <inheritdoc />
    public override string Description => "OCR Math Formula Recognition Sample";

    /// <inheritdoc />
    public override async Task RunAsync(IDashScopeClient client)
    {
        // upload file
        await using var file = File.OpenRead("math.jpg");
        var ossLink = await client.UploadTemporaryFileAsync("qwen-vl-ocr-latest", file, "math.jpg");
        Console.WriteLine($"File uploaded: {ossLink}");
        var messages =
            new List<MultimodalMessage> { MultimodalMessage.User([MultimodalMessageContent.ImageContent(ossLink)]) };
        var completion = await client.GetMultimodalGenerationAsync(
            new ModelRequest<MultimodalInput, IMultimodalParameters>()
            {
                Model = "qwen-vl-ocr-latest",
                Input = new MultimodalInput { Messages = messages },
                Parameters = new MultimodalParameters()
                {
                    OcrOptions = new MultimodalOcrOptions()
                    {
                        Task = "formula_recognition",
                    }
                }
            });

        Console.WriteLine("LaTeX:");
        Console.WriteLine(completion.Output.Choices[0].Message.Content[0].Text);

        if (completion.Usage != null)
        {
            var usage = completion.Usage;
            Console.WriteLine(
                $"Usage: in({usage.InputTokens})/out({usage.OutputTokens})/image({usage.ImageTokens})/total({usage.TotalTokens})");
        }
    }
}

/*
File uploaded: oss://dashscope-instant/52afe077fb4825c6d74411758cb1ab98/2025-11-29/073293f5-1294-4110-ba64-c614b509d7c6/math.jpg
LaTeX:
```latex
\begin{align*}
\tilde{G}(x) &= \frac{\alpha}{\kappa}x, \quad \tilde{T}_i = T, \quad \tilde{H}_i = \tilde{\kappa}T, \quad \tilde{\lambda}_i = \frac{1}{\kappa}\sum_{j=1}^{m}\omega_j - z_i, \\
L(\{p_n\}; m^n) + L(\{x^n\}, m^n) + L(\{m^n\}; q_n) &= L(m^n; q_n) \\
I^{m_n} - (L+1) &= z + \int_0^1 I^{m_n} - (L)z \leq x_m | L^{m_n} - (L) |^3 \\
&\leq \kappa\partial_1\psi(x) + \frac{\kappa^3}{6}\partial_2^3\psi(x) - V(x) \psi(x) = \int d^3y K(x,y) \psi(y), \\
\int_{B_{\kappa}(0)} I^{m}(w)^2 d\gamma &= \lim_{n\to\infty} \int_{B_{\kappa}(0)} r\psi(w_n)^2 d\gamma = \lim_{n\to\infty} \int_{B_{\kappa}(y_n)} d\gamma \geq \beta > 0,
\end{align*}
```
Usage: in(135)/out(339)/image(107)/total(474)
 */

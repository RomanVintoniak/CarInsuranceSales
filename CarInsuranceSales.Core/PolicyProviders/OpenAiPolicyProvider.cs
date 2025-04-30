using CarInsuranceSales.Core.Configuration;
using CarInsuranceSales.Interfaces;
using Microsoft.Extensions.Options;
using OpenAI.Images;

namespace CarInsuranceSales.PolicyProviders;

public class OpenAiPolicyProvider(IOptions<AppOptions> options) : IPolicyProvider
{
    public async Task<string> GetInsurencePolicy()
    {
        var imageClient = new ImageClient("dall-e-3", options.Value.OpenAiApiKey);

        string prompt = "Prompt will be here";

        ImageGenerationOptions generationOptions = new()
        {
            Quality = GeneratedImageQuality.Standard,
            Size = GeneratedImageSize.W1024xH1024,
            Style = GeneratedImageStyle.Vivid,
            ResponseFormat = GeneratedImageFormat.Uri
        };

        GeneratedImage image = await imageClient.GenerateImageAsync(prompt, generationOptions);

        return image.ImageUri.ToString();
    }
}



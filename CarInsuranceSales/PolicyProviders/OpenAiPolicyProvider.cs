using CarInsuranceSales.Interfaces;
using OpenAI.Images;

namespace CarInsuranceSales.PolicyProviders;

public class OpenAiPolicyProvider : IPolicyProvider
{
    private const string _apiKey = "";
    public async Task<string> GetInsurencePolicy()
    {
        ImageClient client = new("dall-e-3", _apiKey);

        string prompt = "Prompt will be here";

        ImageGenerationOptions options = new()
        {
            Quality = GeneratedImageQuality.Standard,
            Size = GeneratedImageSize.W1024xH1024,
            Style = GeneratedImageStyle.Vivid,
            ResponseFormat = GeneratedImageFormat.Uri
        };

        GeneratedImage image = await client.GenerateImageAsync(prompt, options);

        return image.ImageUri.ToString();
    }
}



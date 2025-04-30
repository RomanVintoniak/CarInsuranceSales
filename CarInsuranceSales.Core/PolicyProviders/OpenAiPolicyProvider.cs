using CarInsuranceSales.Core.Configuration;
using CarInsuranceSales.Interfaces;
using Microsoft.Extensions.Options;
using OpenAI.Images;

namespace CarInsuranceSales.PolicyProviders;

/// <summary>
/// Provides Insurence policy using OpenAI
/// </summary>
/// <param name="options">Application configuration <see cref="AppOptions"/></param>
public class OpenAiPolicyProvider(IOptions<AppOptions> options) : IPolicyProvider
{
    /// <inheritdoc/>
    public async Task<string> GetInsurencePolicy()
    {
        var imageClient = new ImageClient("dall-e-3", options.Value.OpenAiApiKey);

        string prompt = @"Generate a simple dummy auto insurance policy document for demonstration purposes. The policy should be fictional but realistic, and include the following clearly labeled sections:

1. **Policyholder Information** – Include a fictional name, address, phone number, and email.
2. **Policy Details** – Include a fictional policy number, type of insurance (""Auto Insurance – Standard Coverage""), effective start and end dates.
3. **Vehicle Information** – Include a fictional vehicle make, model, year, and VIN.
4. **Coverage Summary** – Outline what is covered (e.g., liability, collision, comprehensive) and include realistic but fictional coverage limits.
5. **Exclusions** – List common exclusions such as intentional damage, racing, or using the vehicle for commercial purposes.
6. **Premium and Payment Terms** – Include a fictional premium amount, payment frequency, and due date.
7. **Claims Process** – Outline step-by-step instructions for filing a claim, including required documentation and contact info.
8. **Customer Service Contact Information** – Include fictional phone number, email, and office hours.

Write the document in a professional and easy-to-read format, suitable for presentation or form testing. The policy price is 100 USD.";

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



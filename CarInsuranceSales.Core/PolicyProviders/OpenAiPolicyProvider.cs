using CarInsuranceSales.Core.Configuration;
using CarInsuranceSales.DataAccess.Repository;
using CarInsuranceSales.Interfaces;
using Microsoft.Extensions.Options;
using OpenAI.Chat;

namespace CarInsuranceSales.PolicyProviders;

/// <summary>
/// Provides Insurence policy using OpenAI
/// </summary>
/// <param name="options">Application configuration <see cref="AppOptions"/></param>
public class OpenAiPolicyProvider(
    IOptions<AppOptions> options,
    IClientRepository clientRepository) : IPolicyProvider
{
    /// <inheritdoc/>
    public async Task<string> GetInsurencePolicy(long chatId)
    {
        var chatClient = new ChatClient(options.Value.OpenAiChatModelName, options.Value.OpenAiApiKey);
        var clientDto = await clientRepository.GetByChatId(chatId);

        string prompt = @$"Generate a simple auto insurance policy document. Try to be brief. The policy should include the following data:

1. Policyholder Information: – CarInsuranceSales.
2. Policy Details: – Type of insurance (""Auto Insurance – Standard Coverage""), Policy price is 100 USD.
3. Client Information:**
    Client name: {clientDto.FirstName} {clientDto.LastName}
    Id number: {clientDto.IdNumber}
    DOB: {clientDto.DateOfBirth.ToString("d")}
    Country code: {clientDto.CountryCode}
4. Coverage Summary: – Outline what is covered (e.g., liability, collision, comprehensive) and include realistic but fictional coverage limits.
5. Exclusions: – List exclusions such as intentional damage, racing.

Write the document in a professional and easy-to-read format, without large line spacing, suitable for presentation or form testing.
Don't add any markdown symbols, farewell text. Don't add any text after Exclusions section";

        ChatCompletion completion = await chatClient.CompleteChatAsync(prompt);

        return completion.Content[0].Text;
    }
}



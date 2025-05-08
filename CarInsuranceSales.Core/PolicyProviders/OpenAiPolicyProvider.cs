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

        string prompt = @$"Generate a simple auto insurance policy document. The policy should include the following data:

1. **Policyholder Information** – CarInsuranceSales.
2. **Policy Details** – Include a fictional policy number, type of insurance (""Auto Insurance – Standard Coverage""), effective start and end dates.
4. **Coverage Summary** – Outline what is covered (e.g., liability, collision, comprehensive) and include realistic but fictional coverage limits.
5. **Exclusions** – List common exclusions such as intentional damage, racing, or using the vehicle for commercial purposes.
6. **Premium and Payment Terms** – Include a fictional premium amount, payment frequency, and due date.
7. **Claims Process** – Outline step-by-step instructions for filing a claim, including required documentation and contact info.
8. **Customer Service Contact Information** – Include fictional phone number, email, and office hours.

Write the document in a professional and easy-to-read format, suitable for presentation or form testing. The policy price is 100 USD.
Include the folowing client information in the end: 
1. Client name: {clientDto.FirstName} {clientDto.LastName}
2. Id number: {clientDto.IdNumber}
3. DOB: {clientDto.DateOfBirth}
4. Country code: {clientDto.CountryCode}
"
        ;

        ChatCompletion completion = await chatClient.CompleteChatAsync(prompt);

        return completion.Content[0].Text;
    }
}



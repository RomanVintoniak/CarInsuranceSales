using CarInsuranceSales.Core.Configuration;
using CarInsuranceSales.Interfaces;
using Microsoft.Extensions.Options;

namespace CarInsuranceSales.PolicyProviders;

/// <summary>
/// Fake policy provider used for testing
/// </summary>
/// <param name="options">Application configuration <see cref="AppOptions"/></param>
public class FakePolicyProvider(IOptions<AppOptions> options) : IPolicyProvider
{
    /// <inheritdoc/>
    public Task<string> GetInsurencePolicy(long chatId)
    {
        return Task.FromResult(options.Value.PolicyDocumentUrl);
    }
}

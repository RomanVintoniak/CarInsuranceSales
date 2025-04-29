using CarInsuranceSales.Interfaces;

namespace CarInsuranceSales.PolicyProviders;

public class FakePolicyProvider : IPolicyProvider
{
    public Task<string> GetInsurencePolicy()
    {
        return Task.FromResult(Constants.PolicyDocumentUrl);
    }
}

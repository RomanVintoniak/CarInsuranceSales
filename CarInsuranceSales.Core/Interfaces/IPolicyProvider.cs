namespace CarInsuranceSales.Interfaces;

public interface IPolicyProvider
{
    Task<string> GetInsurencePolicy();
}

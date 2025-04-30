namespace CarInsuranceSales.Interfaces;

/// <summary>
/// Provides methods to get Insurance policy
/// </summary>
public interface IPolicyProvider
{
    /// <summary>
    /// Generates Insurence policy
    /// </summary>
    /// <returns>Url to generated Insurence policy file</returns>
    Task<string> GetInsurencePolicy();
}

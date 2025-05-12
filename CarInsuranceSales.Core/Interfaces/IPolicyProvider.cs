namespace CarInsuranceSales.Interfaces;

/// <summary>
/// Provides methods to get Insurance policy
/// </summary>
public interface IPolicyProvider
{
    /// <summary>
    /// Generates Insurence policy
    /// </summary>
    /// <param name="chatId">The client's chat ID</param>
    /// <returns>Generated Insurence policy</returns>
    Task<string> GetInsurencePolicy(long chatId);
}

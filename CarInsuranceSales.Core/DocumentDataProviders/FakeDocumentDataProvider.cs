using CarInsuranceSales.Interfaces;
using CarInsuranceSales.Models;

namespace CarInsuranceSales.DocumentDataProviders;

/// <summary>
/// Fake document data provider used for testing
/// </summary>
public class FakeDocumentDataProvider : IDocumentDataProvider
{
    /// <inheritdoc/>
    public Task<DocumentData> GetDocumentData(MemoryStream stream)
    {
        return Task.FromResult<DocumentData>(new()
        {
            FirstName = "Roman",
            LastName = "Vinto",
            IdNumber = "1232131231231231",
            CountryCode = "USA",
            DateOfBirth = "1968-01-06"
        });
    }
}

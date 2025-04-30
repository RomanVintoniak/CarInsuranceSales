using CarInsuranceSales.Interfaces;
using CarInsuranceSales.Models;

namespace CarInsuranceSales.DocumentDataProviders;

public class FakeDocumentDataProvider : IDocumentDataProvider
{
    public Task<DocumentData> GetDocumentData(string documentBase64)
    {
        return Task.FromResult<DocumentData>(new()
        {
            FirstName = "Roman",
            LastName = "Vinto",
            SerialNumber = "1232131231231231",
        });
    }
}

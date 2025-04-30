using CarInsuranceSales.Interfaces;
using CarInsuranceSales.Models;
using Mindee;

namespace CarInsuranceSales.DocumentDataProviders;

public class MindeeDocumentDataProvider(MindeeClient mindeeClient) : IDocumentDataProvider
{
    public Task<DocumentData> GetDocumentData(string documentBase64)
    {
        throw new NotImplementedException();
    }
}

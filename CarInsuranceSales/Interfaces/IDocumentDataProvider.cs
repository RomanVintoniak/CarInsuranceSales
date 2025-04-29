using CarInsuranceSales.Models;

namespace CarInsuranceSales.Interfaces;

public interface IDocumentDataProvider
{
    Task<DocumentData> GetDocumentData(string documentBase64);
}

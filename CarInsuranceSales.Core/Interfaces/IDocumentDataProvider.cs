using CarInsuranceSales.Models;

namespace CarInsuranceSales.Interfaces;

public interface IDocumentDataProvider
{
    Task<DocumentData> GetDocumentData(MemoryStream stream);
}

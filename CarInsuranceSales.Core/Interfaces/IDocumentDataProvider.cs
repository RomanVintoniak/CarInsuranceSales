using CarInsuranceSales.Models;

namespace CarInsuranceSales.Interfaces;

/// <summary>
/// Provides methods to proccess personal documents and return data 
/// </summary>
public interface IDocumentDataProvider
{
    /// <summary>
    /// Retrieves data from the document
    /// </summary>
    /// <param name="stream">Stream containig a document file</param>
    /// <returns><see cref="DocumentData"/></returns>
    Task<DocumentData> GetDocumentData(MemoryStream stream);
}

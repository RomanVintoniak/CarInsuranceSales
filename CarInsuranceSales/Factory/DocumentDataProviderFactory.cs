using CarInsuranceSales.DocumentDataProviders;
using CarInsuranceSales.Interfaces;

namespace CarInsuranceSales.Factory;

public class DocumentDataProviderFactory
{
    public IDocumentDataProvider GetDocumentDataProvider()
    {
        //return new MindeeDocumentDataProvider(new MindeeClient(""));
        return new FakeDocumentDataProvider();
    }
}

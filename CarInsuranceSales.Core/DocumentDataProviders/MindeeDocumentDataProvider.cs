using CarInsuranceSales.Core.Configuration;
using CarInsuranceSales.Interfaces;
using CarInsuranceSales.Models;
using Microsoft.Extensions.Options;
using Mindee;
using Mindee.Exceptions;
using Mindee.Input;
using Mindee.Product.Passport;

namespace CarInsuranceSales.DocumentDataProviders;

public class MindeeDocumentDataProvider : IDocumentDataProvider
{
    private readonly MindeeClient _mindeeClient;

    public MindeeDocumentDataProvider(IOptions<AppOptions> options)
    {
        _mindeeClient = new MindeeClient(options.Value.MindeeApiKey);
    }

    public async Task<DocumentData> GetDocumentData(MemoryStream stream)
    {
        try
        {
            var inputSource = new LocalInputSource(stream.ToArray(), "file.png");

            var response = await _mindeeClient.ParseAsync<PassportV1>(inputSource);

            return new DocumentData(response);

        } catch (Mindee400Exception)
        {
            throw new ApplicationException("There is an issue with the provided file. File types allowed: png, jpg, webp, heic, tiff, pdf.");

        } catch (Mindee429Exception)
        {
            throw new ApplicationException("The server is overloadded at this point, try again later");

        } catch (MindeeException)
        {
            throw new ApplicationException("An error ocured during data parsing");

        } catch (Exception)
        {
            throw new ApplicationException("Technical error occurred, try again later or contact support");
        }
    }
}

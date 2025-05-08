using CarInsuranceSales.DataAccess.Models;
using Mindee.Parsing.Common;
using Mindee.Product.Passport;
using System.Text;

namespace CarInsuranceSales.Models;

/// <summary>
/// Model for holding data retrieved from a personal document
/// </summary>
public class DocumentData
{
    public DocumentData()
    {
        
    }

    public ClientDto ToClientDto(long chatId)
    {
        return new ClientDto()
        {
            IdNumber = IdNumber,
            CountryCode = CountryCode,
            DateOfBirth = DateTime.Parse(DateOfBirth),
            FirstName = FirstName,
            LastName = LastName,
            ChatId = chatId
        };
    }

    public DocumentData(PredictResponse<PassportV1> response)
    {
        var prediction = response.Document.Inference.Prediction;

        IdNumber = prediction.IdNumber.Value;
        CountryCode = prediction.Country.Value;
        FirstName = prediction.GivenNames[0].Value;
        LastName = prediction.Surname.Value;
        DateOfBirth = prediction.BirthDate.Value;
    }

    public string IdNumber { get; set; }
    public string CountryCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DateOfBirth { get; set; }

    public override string ToString()
    {
        StringBuilder result = new StringBuilder();

        result.Append($":Country Code: {CountryCode}\n");
        result.Append($":ID Number: {IdNumber}\n");
        result.Append($":First Name: {FirstName}\n");
        result.Append($":Surname: {LastName}\n");
        result.Append($":Date of Birth: {DateOfBirth}\n");

        return result.ToString();
    }
}

using CarInsuranceSales.DataAccess.Entities;

namespace CarInsuranceSales.DataAccess.Models;

public class ClientDto
{
    public ClientDto()
    {
        
    }
    public ClientDto(Client client)
    {
        Id = client.Id;
        ChatId = client.ChatId;
        IdNumber = client.IdNumber;
        CountryCode = client.CountryCode;
        FirstName = client.FirstName;
        LastName = client.LastName;
        DateOfBirth = client.DateOfBirth;
    }

    public int Id { get; set; }
    public long ChatId { get; set; }
    public string IdNumber { get; set; }
    public string CountryCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

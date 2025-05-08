using CarInsuranceSales.DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace CarInsuranceSales.DataAccess.Entities;

public class Client
{

    public Client(ClientDto client)
    {
        Id = client.Id;
        ChatId = client.ChatId;
        IdNumber = client.IdNumber;
        CountryCode = client.CountryCode;
        FirstName = client.FirstName;
        LastName = client.LastName;
        DateOfBirth = client.DateOfBirth;
    }

    [Key]
    public int Id { get; set; }

    public long ChatId { get; set; }

    [MaxLength(100)]
    public string IdNumber { get; set; }

    [MaxLength(10)]
    public string CountryCode { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; }

    [MaxLength(50)]
    public string LastName { get; set; }

    [MaxLength(50)]
    public DateTime DateOfBirth { get; set; }
}

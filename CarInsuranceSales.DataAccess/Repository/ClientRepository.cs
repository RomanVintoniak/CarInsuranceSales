using CarInsuranceSales.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CarInsuranceSales.DataAccess.Repository;

public class ClientRepository(ApplicationContext dbContext) : IClientRepository
{
    public async Task Add(ClientDto clientDto)
    {
        var client = await dbContext.Clients.FirstOrDefaultAsync(c => c.ChatId == clientDto.ChatId);

        if (client is not null)
        {
            client.IdNumber = clientDto.IdNumber;
            client.LastName = clientDto.LastName;
            client.FirstName = clientDto.FirstName;
            client.CountryCode = clientDto.CountryCode;
            client.DateOfBirth = clientDto.DateOfBirth;
        }
        else
        {
            await dbContext.Clients.AddAsync(new Entities.Client(clientDto));
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<ClientDto> GetByChatId(long chatId)
    {
        var client = await dbContext.Clients.FirstOrDefaultAsync(c => c.ChatId == chatId) 
            ?? throw new ApplicationException("Client not found");

        return new ClientDto(client);
    }
}

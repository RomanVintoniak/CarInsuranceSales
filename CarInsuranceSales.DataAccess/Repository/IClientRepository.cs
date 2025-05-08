using CarInsuranceSales.DataAccess.Models;

namespace CarInsuranceSales.DataAccess.Repository;

public interface IClientRepository
{
    Task Add(ClientDto client);

    Task<ClientDto> GetByChatId(long chatId);
}

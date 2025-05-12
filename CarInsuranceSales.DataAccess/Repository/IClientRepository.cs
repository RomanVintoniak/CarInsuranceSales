using CarInsuranceSales.DataAccess.Models;

namespace CarInsuranceSales.DataAccess.Repository;

/// <summary>
/// Provides methods for interacting with client data in the database
/// </summary>
public interface IClientRepository
{
    /// <summary>
    /// Adds a new client record to the database
    /// </summary>
    /// <param name="client">The client's data to be added</param>
    Task Add(ClientDto client);

    /// <summary>
    /// Retrieves a client record from the database by chat id
    /// </summary>
    /// <param name="chatId">The client's chat ID</param>
    /// <returns>The client's data</returns>
    Task<ClientDto> GetByChatId(long chatId);
}

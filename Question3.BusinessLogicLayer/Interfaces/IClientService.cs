using Question3.BusinessLogicLayer.Interfaces.Base;
using Question3.DataAccessLayer.Entities;
using Core.Presentation.Models.DataTransferObjects;


namespace Question3.BusinessLogicLayer.Interfaces
{
    public interface IClientService : IGenericService<ClientDto, ClientEntity>
    {
       

         Task<bool> AddOrUpdate(ClientDto client);
         Task<bool> Archive(IEnumerable<Guid> identifiers);
    }
}

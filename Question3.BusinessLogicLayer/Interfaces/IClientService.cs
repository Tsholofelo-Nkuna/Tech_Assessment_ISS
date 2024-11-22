using Question3.BusinessLogicLayer.Interfaces.Base;
using Question3.DataAccessLayer.Entities;
using Question3.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3.BusinessLogicLayer.Interfaces
{
    public interface IClientService : IGenericService<ClientDto, ClientEntity>
    {

    }
}

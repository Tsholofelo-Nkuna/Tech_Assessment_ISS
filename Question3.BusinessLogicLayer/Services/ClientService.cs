using Question3.BusinessLogicLayer.Interfaces;
using Question3.BusinessLogicLayer.Services.Base;
using Question3.DataAccessLayer;
using Question3.DataAccessLayer.Entities;
using Question3.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3.BusinessLogicLayer.Services
{
    public class ClientService : GenericService<ClientDto, ClientEntity>, IClientService
    {
        public ClientService(WebDbContext dbContext) : base(dbContext)
        {
        }
    }
}

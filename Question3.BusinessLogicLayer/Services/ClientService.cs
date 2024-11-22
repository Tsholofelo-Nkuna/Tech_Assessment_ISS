using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<ClientDto> Get(ClientDto filter)
        {
            var query = this._entitySet.AsNoTracking();
            if (filter.Archived)
            {
                query = query.Where(x => x.Archived);
            }
            else
            {
                query = query.Where(x => !x.Archived);
            }

            if(filter.CompanyName?.Trim() is string validCompanyName and { Length: > 0 })
            {
                query = query.Where(x => x.CompanyName.Contains(filter.CompanyName));
            }

            return this.mapper.Map<List<ClientDto>>(query.OrderByDescending(x => x.CreatedOn).ToList());
        }
    }
}

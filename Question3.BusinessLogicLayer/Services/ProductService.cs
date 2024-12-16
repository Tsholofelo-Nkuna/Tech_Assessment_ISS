using AutoMapper;
using ClientManagement.DataAccessLayer.Entities;
using Core.Presentation.Models.Models.DataTransferObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Question3.BusinessLogicLayer.Services.Base;
using Question3.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.BusinessLogicLayer.Services
{
    public class ProductService : GenericService<ProductDto, ProductEntity>
    {
        public ProductService(WebDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            
        }

        public override Task<IEnumerable<ProductDto>> Get(ProductDto filter)
        {
            var query = this._entitySet.Where(x => true).AsNoTracking();
            if (filter.Archived)
            {
                query = query.Where(x => x.Archived);
            }
            else
            {
               query =  query.Where(x => !x.Archived);
            }

            if (filter.Id != Guid.Empty) { 
              query = query.Where(x => x.Id == filter.Id);
            }

            if (!string.IsNullOrWhiteSpace(filter.Name)) {
                query = query.Where(x => x.Name.Contains(filter.Name));
            }

            var result =  query.OrderByDescending(x => x.CreatedOn).ToList();

            return  Task.FromResult<IEnumerable<ProductDto>>(this._mapper.Map<List<ProductDto>>(result));
        }
    }
}

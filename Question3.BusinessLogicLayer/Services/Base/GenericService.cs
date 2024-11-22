using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Question3.BusinessLogicLayer.Interfaces.Base;
using Question3.BusinessLogicLayer.Models.DataTransferObjects.Base;
using Question3.DataAccessLayer;
using Question3.DataAccessLayer.Entities.Base;
using Question3.Models.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Question3.BusinessLogicLayer.Services.Base
{
    public class GenericService<TDto, TEntity> : IGenericService<TDto, TEntity> where TEntity : BaseEntity where TDto : BaseDto
    {
        protected readonly WebDbContext _dbContext;
        protected readonly DbSet<TEntity> _entitySet;
        protected readonly IMapper mapper;
        public GenericService(WebDbContext dbContext)
        {
            _dbContext = dbContext;
            _entitySet = _dbContext.Set<TEntity>();
            mapper = new MapperConfiguration(config =>
            {
                config.CreateMap<TDto, TEntity>().ReverseMap();
            }).CreateMapper();
        }
        public virtual async Task<bool> Delete(IEnumerable<Guid> identifiers)
        {
            var removed = _entitySet.Where(x => identifiers.Contains(x.Id)).ToList();
            _entitySet.RemoveRange(removed);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<IEnumerable<TDto>> Get(Expression<Func<TEntity, bool>> filter)
        {
            var list = await _entitySet.Where(filter).AsNoTracking().OrderByDescending(x=> x.CreatedOn).ToListAsync();
            return mapper.Map<List<TDto>>(list);
        }

        public virtual async Task<bool> Insert(List<TDto> inserted)
        {
            var toBeInserted = mapper.Map<List<TEntity>>(inserted);
            _entitySet.AddRange(toBeInserted);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Update(List<TDto> updates)
        {
            var toBeUpdated = mapper.Map<List<TEntity>>(updates);
            _entitySet.UpdateRange(toBeUpdated);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}

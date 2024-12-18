using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.BusinessLogicLayer.Interfaces.Base
{
    public interface IGenericService<TDto, TEntity> 
    {
        Task<IEnumerable<TDto>> Get(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TDto>> Get(TDto filter);
        Task<bool> Delete(IEnumerable<Guid> identifiers);
        Task<bool> Update(List<TDto> updates);
        Task<bool> Insert(List<TDto> inserted);
        Task<bool> AddOrUpdate(List<TDto> payload);
    }
}

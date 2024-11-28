

using Core.Presentation.Models.DataTransferObjects.Base;
using System.Dynamic;

namespace Core.Presentation.Models.Interfaces.Base
{
    public interface IGenericListViewModel<TRecordType> where TRecordType: BaseDto
    {
        public IEnumerable<TRecordType> ViewModelState { get; set; }
        public void Set(Guid id, string propName, object? value);
        public dynamic Get(Guid id, string propName, Type valueType);
    }
}

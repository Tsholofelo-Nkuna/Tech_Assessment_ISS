using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation.Models.Models.Base
{
    public class GenericListViewModel<TRecordType> : IGenericListViewModel<TRecordType> where TRecordType: BaseDto, new()
    {
        private GenericViewModel<TRecordType> _genericViewModel = new GenericViewModel<TRecordType>(new TRecordType());
       
        public IEnumerable<TRecordType> ViewModelState { get; set; }
        public GenericListViewModel(IEnumerable<TRecordType> state) {
           this.ViewModelState = state;
        }
      

        public dynamic Get(Guid id, string propName, Type valueType)
        {
           var source = this.ViewModelState.FirstOrDefault(x => x.Id == id);
           if(source is TRecordType validSource)
            {
                _genericViewModel.ViewModelState = source;
                return _genericViewModel.Get(propName,valueType);
            }
            else
            {
                return default;
            }
        }

        public void Set(Guid id, string propName, object? value)
        {
           var target = this.ViewModelState.FirstOrDefault(y => y.Id == id);
            if(target is TRecordType validTarget)
            {
                _genericViewModel.ViewModelState = target;
                _genericViewModel.Set(propName, value);
            }

        }
    }
}

using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.ViewComponents.Components.Base;
using System.Globalization;

namespace Core.Presentation.ViewComponents.Components
{
    public partial class TableComponent<TRecordType> : GenericComponentBase<TableComponentViewModel<TRecordType>, TRecordType>
        where TRecordType : BaseDto, new()

    {
        public string ViewLink(Guid recordId) { 
            
            var baseUri = !String.IsNullOrEmpty(this.ViewModel.ViewController) ? "/" : string.Empty;
            var uri = $"{baseUri}{this.ViewModel.ViewController}/{this.ViewModel.ViewAction}/{recordId}";
            if(this.ViewModel is IFilterable<BaseDto> filterableViewModel)
            {
                uri += filterableViewModel.QueryString(filterableViewModel.Filter);
            }
            return uri;
        }

        public TableComponent() { }



    }
}

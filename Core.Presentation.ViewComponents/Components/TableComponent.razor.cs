using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.ViewComponents.Components.Base;


namespace Core.Presentation.ViewComponents.Components
{
    public partial class TableComponent<TRecordType> : GenericComponentBase<TableComponentViewModel<TRecordType>, TRecordType>
        where TRecordType : BaseDto, new()
       
    {
        //[Parameter]
        //public TableComponentViewModel<TRecordType> ViewModel { get; set; } = new TableComponentViewModel<TRecordType>(Enumerable.Empty<TRecordType>());
        public TableComponent() { }
    }
}

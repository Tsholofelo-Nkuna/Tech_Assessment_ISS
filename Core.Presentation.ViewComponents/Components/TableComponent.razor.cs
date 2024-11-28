using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects.Base;
using Microsoft.AspNetCore.Components;

namespace Core.Presentation.ViewComponents.Components
{
    public partial class TableComponent<TRecordType> where TRecordType : BaseDto, new()
    {
       [Parameter]
       public TableComponentViewModel<TRecordType> Model { get; set; } = new TableComponentViewModel<TRecordType>(Enumerable.Empty<TRecordType>());
     
    }
}

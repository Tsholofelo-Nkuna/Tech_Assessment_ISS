using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.ViewComponents.Components.Base;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace Core.Presentation.ViewComponents.Components
{
  
    public partial class TableComponent<TRecordType> : GenericComponentBase<TableComponentViewModel<TRecordType>, TRecordType>
        where TRecordType : BaseDto, new()

    {
        [Parameter]
        public Func<Guid, Task>? OnViewClick { get; set; }
        public void OnView(Guid recordId) {
            this.NavManager.NavigateTo($"{this.ViewModel.ViewController}/{this.ViewModel.ViewAction}/{recordId}");
            OnViewClick?.Invoke(recordId);
           
        }

        public TableComponent() { }



    }
}

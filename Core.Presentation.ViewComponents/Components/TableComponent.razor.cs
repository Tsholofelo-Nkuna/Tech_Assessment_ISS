using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.ViewComponents.Components.Base;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Net.Http.Json;

namespace Core.Presentation.ViewComponents.Components
{
  
    public partial class TableComponent<TRecordType> : GenericComponentBase<TableComponentViewModel<TRecordType>, TRecordType>
        where TRecordType : BaseDto, new()

    {
        [Parameter]
        public Func<Guid, Task>? OnViewClick { get; set; }
        [Parameter]
        public Func<bool, Task>? OnDeleteClick { get; set; }
        [Parameter]
        public Func<bool , Task>? OnArchiveClick { get; set; }
        public void OnView(Guid recordId) {
            this.NavManager.NavigateTo($"{this.ViewModel.ViewController}/{this.ViewModel.ViewAction}/{recordId}");
            OnViewClick?.Invoke(recordId);
        }

        public async Task OnDelete(Guid id)
        {
            var response = await this.AppApi.GetAsync($"api/{this.ViewModel.DeleteController}/{this.ViewModel.DeleteAction}/{id}");
            if (response.IsSuccessStatusCode)
            {
                OnDeleteClick?.Invoke(await response.Content.ReadFromJsonAsync<bool>());
            }
        }

        public async Task OnArchive(Guid id)
        {
            var response = await this.AppApi.GetAsync($"api/{this.ViewModel.ArchiveController}/{this.ViewModel.ArchiveAction}/{id}");
            if (response.IsSuccessStatusCode) { 
              OnArchiveClick?.Invoke(await response.Content.ReadFromJsonAsync<bool>());
            }

        }

        public TableComponent() { }



    }
}

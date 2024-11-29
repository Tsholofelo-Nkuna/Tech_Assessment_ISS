using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Razor;
using Question3.BusinessLogicLayer.Interfaces;

namespace ClientManagement.Presentation.Web.Components.Pages.Clients
{
    public partial class Index
    {
        [Inject] private IClientService ClientService { get; set; }
        public ClientsViewModel ViewModel { get; set; } = new ClientsViewModel();
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var client = this.ViewModel.SearchFormComponentViewModel.ViewModelState.FirstOrDefault();
            if (client is ClientDto validCLient)
            {
                ViewModel.TableConfig.ViewModelState = ClientService.Get(validCLient);
            }
            
        }



    }
}

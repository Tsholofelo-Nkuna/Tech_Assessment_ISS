using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.ViewComponents.Components.Base;
using Microsoft.AspNetCore.Components;
using Question3.BusinessLogicLayer.Interfaces;

namespace ClientManagement.Presentation.Web.Components.Pages.Clients
{
    public partial class Details : GenericComponentBase<ClientDetailsViewModel, ClientDto>
    {

        [Inject]
        public IClientService ClientService { get; set; }
        [Parameter]
         public Guid Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.ViewModel.ViewModelState = this.ClientService.Get(new ClientDto { Id = Id });
            this.ViewModel.PrimaryContactPersonFormViewModel.ViewModelState = this.ViewModel.ViewModelState;
            this.ViewModel.DetailsFormViewModel.ViewModelState = this.ViewModel.ViewModelState;
           
            await base.OnInitializedAsync();
        }
    }
}

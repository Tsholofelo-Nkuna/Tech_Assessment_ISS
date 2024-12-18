using ClientManagement.Presentation.Web.Components.Layout;
using ClientManagement.Presentation.Web.Components.Pages.Clients.State.Details;
using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.ViewComponents.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ClientManagement.BusinessLogicLayer.Interfaces;
using System.Net.Http.Json;

namespace ClientManagement.Presentation.Web.Components.Pages.Clients
{
    public partial class Details : GenericComponentBase<ClientDetailsViewModel, ClientDto>
    {

        [Inject]
        public IClientService ClientService { get; set; }
        [Parameter]
         public Guid Id { get; set; }
        [SupplyParameterFromQuery]
        public bool Archived { get; set; }
        [Inject]
        public DetailsStateManager StateManager { get; set; }

        public ClientDto DetailsFilter => this.ViewModel.ViewModelState.FirstOrDefault() ?? new ClientDto();
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.BaseUrl = "api/clients";
            await this.GetData(new ClientDto { Id = Id, Archived = this.Archived });
           
        }

        public async Task GetData(ClientDto filter)
        {
            var response = await this.AppApi.GetAsync($"{this.BaseUrl}/GetByArchive/{filter.Id}?archived={filter.Archived}");
            if (response.IsSuccessStatusCode) { 
              var result  = await response.Content.ReadFromJsonAsync<ClientDto?>();
              this.ViewModel.ViewModelState = new[] { result ?? new ClientDto() };
              this.ViewModel.PrimaryContactPersonFormViewModel.ViewModelState = this.ViewModel.ViewModelState;
              this.ViewModel.DetailsFormViewModel.ViewModelState = this.ViewModel.ViewModelState;
              StateHasChanged();
            }
        }

        public async Task OnSaveClientDetails(IEnumerable<ClientDto> clientDetailUpdates)
        {
            if (clientDetailUpdates.Any()) {
                var detailUpdateReponse = await this.AppApi.PostAsJsonAsync<ClientDto>(this.BaseUrl, clientDetailUpdates.FirstOrDefault()!);
                if (detailUpdateReponse.IsSuccessStatusCode && (await detailUpdateReponse.Content.ReadFromJsonAsync<bool>()))
                {
                    await this.GetData(this.DetailsFilter);
                }
            }  
          
        }

        public async Task OnSaveClientContacts(IEnumerable<ClientDto> clientContactUpdates)
        {
            if (clientContactUpdates.Any())
            {
                var contactUpdateResponse = await this.AppApi.PostAsJsonAsync<ClientDto>(this.BaseUrl, clientContactUpdates.FirstOrDefault()!);
                if(contactUpdateResponse.IsSuccessStatusCode && await contactUpdateResponse.Content.ReadFromJsonAsync<bool>())
                {
                    await this.GetData(this.DetailsFilter);
                }
            }
         
           
        }
    }
}

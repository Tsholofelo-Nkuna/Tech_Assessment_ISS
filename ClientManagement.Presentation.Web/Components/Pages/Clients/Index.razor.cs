using ClientManagement.Presentation.Web.Components.Pages.Clients.State.Index;
using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.ViewComponents.Components;
using Core.Presentation.ViewComponents.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.JSInterop;
using Question3.BusinessLogicLayer;
using Question3.BusinessLogicLayer.Interfaces;

namespace ClientManagement.Presentation.Web.Components.Pages.Clients
{
    public partial class Index : GenericComponentBase<ClientsViewModel, ClientDto>
    {
       
        [SupplyParameterFromForm(FormName = "NewClientDetails")]
        public ClientDto? NewClientDetails { get; set; }
        public TableComponent<ClientDto> ClientsTable { get; set; }
        [Inject]
        public IndexStateManager StateManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
          
            await base.OnInitializedAsync();
            this.BaseUrl = "api/client"; 
            if(this.StateManager.Get<ClientDto?>(nameof(IndexState.NewClientToBeAdded)) is ClientDto clientToAdd)
            {
                var added =  await AddNewClient(clientToAdd);
                if (added)
                {
                    this.StateManager.Set<ClientDto?>(nameof(IndexState.NewClientToBeAdded), null);
                }
                else
                {
                    //Display error message
                }
            }
            this.GetData(this.SearchFormFilters);

        }

        public  async Task<bool> AddNewClient(ClientDto newClient)
        {
            var response = await this.AppApi.PostAsJsonAsync<ClientDto>(this.BaseUrl, newClient);
            return response.IsSuccessStatusCode && (await response.Content.ReadFromJsonAsync<bool>());
        }
        private async void GetData(ClientDto filters)
        {
            try
            {

                var url = $"{this.BaseUrl}/Get";
                var response = await this.AppApi.PostAsJsonAsync(url, filters);
                if (response.IsSuccessStatusCode)
                {
                    ViewModel.TableConfig.ViewModelState =  (await response.Content.ReadFromJsonAsync<IEnumerable<ClientDto>>()) ?? Enumerable.Empty<ClientDto>();
                    StateHasChanged();
                }
               
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task OnNewClientFormSubmitted(ClientDto? details, ClientDto? primaryContact)
        {
           
            
            if (details is ClientDto newC && primaryContact is ClientDto contactInfo 
                && this.ViewModel.PrimaryContactFormViewModel.IsValid 
                && this.ViewModel.NewClientFormViewModel.IsValid)
            {
                newC.PrimaryContactName = contactInfo.PrimaryContactName;
                newC.PrimaryContactPhone = contactInfo.PrimaryContactPhone;
                newC.PrimaryContactEmail = contactInfo.PrimaryContactEmail;
                this.StateManager.Set(nameof(IndexState.NewClientToBeAdded), newC);
            }
           
        }

        public  Task OnSubmitSearchFilters(IEnumerable<ClientDto> searchFilters)
        {
            this.GetData(this.SearchFormFilters);
            return Task.CompletedTask;
        }

        public  Task OnViewTableRecord(Guid recordId)
        {
            this.NavManager.NavigateTo($"{this.ViewModel.TableConfig.ViewController}/{this.ViewModel.TableConfig.ViewAction}/{recordId}?{nameof(ClientDto.Archived)}={this.SearchFormFilters.Archived}");
            return Task.CompletedTask;
        }

        public Task OnArchiveTableRecord(bool archived)
        {
            if (archived) { 
               this.GetData(SearchFormFilters);
            }

            return Task.CompletedTask;
        }

        public Task OnDeleteTableRecord(bool deleted)
        {
            if (deleted)
            {
                this.GetData(this.SearchFormFilters);
            }
            return Task.CompletedTask;
        }
        public ClientDto SearchFormFilters {
            get {
             return this.ViewModel.SearchFormComponentViewModel.ViewModelState?.FirstOrDefault() ?? new ClientDto();
            } 
           
        } 
    }
}

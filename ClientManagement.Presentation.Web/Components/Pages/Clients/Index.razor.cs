using ClientManagement.Presentation.Web.Components.Pages.Clients.State.Index;
using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.ViewComponents.Components;
using Core.Presentation.ViewComponents.Components.Base;
using Microsoft.AspNetCore.Components;

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
            this.BaseUrl = "api/clients";
            var newClientIsValid = this.StateManager.Get<bool>(nameof(IndexState.NewClientFormIsValid));
            var newlyAddedClient = this.StateManager.Get<ClientDto?>(nameof(IndexState.NewClientToBeAdded));
            if (newlyAddedClient is ClientDto clientToAdd 
                && newClientIsValid)
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
            } else if (newlyAddedClient is ClientDto c
                && ! newClientIsValid)
            {
                this.ViewModel.NewClientFormViewModel.ViewModelState = new[] {c};
                this.ViewModel.PrimaryContactFormViewModel.ViewModelState = new[] { c };
                this.ViewModel.ModalViewModel.Show = true;
                this.ViewModel.NewClientFormViewModel.WasValidated = true;
                this.ViewModel.PrimaryContactFormViewModel.WasValidated = true;
              
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
           
            
            if (details is ClientDto newC && primaryContact is ClientDto contactInfo)
            {
                newC.PrimaryContactName = contactInfo.PrimaryContactName;
                newC.PrimaryContactPhone = contactInfo.PrimaryContactPhone;
                newC.PrimaryContactEmail = contactInfo.PrimaryContactEmail;
                this.StateManager.Set(nameof(IndexState.NewClientToBeAdded), newC);
              
            }
            var contactFormIsValid = this.ViewModel.PrimaryContactFormViewModel.IsValid;
            var detailsFormIsValid = this.ViewModel.NewClientFormViewModel.IsValid;
            if (contactFormIsValid && detailsFormIsValid)
            {
                this.StateManager.Set(nameof(IndexState.NewClientFormIsValid), true);
            }
            else
            {
                this.StateManager.Set(nameof(IndexState.NewClientFormIsValid), false);
            }
           
        }
   
        public  Task OnSubmitSearchFilters(IEnumerable<ClientDto> searchFilters)
        {
            this.GetData(this.SearchFormFilters);
            return Task.CompletedTask;
        }

        public Task OnCreateNewTableRecord()
        {
            this.ViewModel.ModalViewModel.Show = true;
            StateHasChanged();
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

using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.ViewComponents.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;
using Question3.BusinessLogicLayer.Interfaces;

namespace ClientManagement.Presentation.Web.Components.Pages.Clients
{
    public partial class Index : GenericComponentBase<ClientsViewModel, ClientDto>
    {
       
        [SupplyParameterFromForm(FormName = "NewClientDetails")]
        public ClientDto? NewClientDetails { get; set; }
      
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.BaseUrl = "api/client";
            this.GetData();
           
        }


        private async void GetData()
        {
            try
            {

                var qBuilder = new QueryBuilder();
                var filters = this.ViewModel.SearchFormComponentViewModel.ViewModelState.FirstOrDefault() ?? new ClientDto();
               
                var qString = qBuilder.ToString();
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
                var response = await this.AppApi.PostAsJsonAsync(this.BaseUrl, newC);
                if (response.IsSuccessStatusCode &&  await response.Content.ReadFromJsonAsync<bool>())
                {
                    this.GetData();
                }

            }
           
        }

        public async Task OnSubmitSearchFilters(IEnumerable<ClientDto> searchFilters)
        {
            this.GetData();
        }

        public ClientDto SearchFormFilters {
            get {
             return this.ViewModel.SearchFormComponentViewModel.ViewModelState?.FirstOrDefault() ?? new ClientDto();
            } 
           
        } 
    }
}

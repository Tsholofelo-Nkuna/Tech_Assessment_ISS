﻿using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.ViewComponents.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Razor;
using Question3.BusinessLogicLayer.Interfaces;

namespace ClientManagement.Presentation.Web.Components.Pages.Clients
{
    public partial class Index : GenericComponentBase<ClientsViewModel, ClientDto>
    {
        [Inject] private IClientService ClientService { get; set; }
     
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
           
                ViewModel.TableConfig.ViewModelState = ClientService
                .Get(this.ViewModel.SearchFormComponentViewModel.ViewModelState.FirstOrDefault() ?? new ClientDto());
            ViewModel.SearchFormComponentViewModel.AddViewModelStateChangeListener(this.OnSearchFormModelStateChange);
            
        }

        public Task OnSearchFormModelStateChange(IEnumerable<ClientDto> searchState) { 
            ViewModel.TableConfig.ViewModelState = ClientService.Get(searchState.FirstOrDefault() ?? new ClientDto());
            StateHasChanged();
            return Task.CompletedTask;
        }



    }
}
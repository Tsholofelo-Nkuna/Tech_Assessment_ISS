﻿
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.Models.Base;

namespace Core.Presentation.Models
{
    public class ClientDetailsViewModel : GenericListViewModel<ClientDto>
    {
        private readonly ClientsViewModel _clientsViewModel = new ClientsViewModel();
        public ClientDetailsViewModel(): this(Enumerable.Empty<ClientDto>()) {  }
        public ClientDetailsViewModel(IEnumerable<ClientDto> client): base(client) {

            this.DetailsFormViewModel = new(Enumerable.Empty<ClientDto>().Append(new ClientDto()), "ClientDetailsForm");
            this.PrimaryContactPersonFormViewModel = new(Enumerable.Empty<ClientDto>().Append(new ClientDto()), "ClientDetailsContactForm");
            this.DetailsFormViewModel.CollapseFooter = false;
            this.PrimaryContactPersonFormViewModel.CollapseFooter = false;
            this.PrimaryContactPersonFormViewModel.IncludeFormStatePropsAsHidden = true;
            this.DetailsFormViewModel.IncludeFormStatePropsAsHidden = true;
            this.DetailsFormViewModel.Fields = _clientsViewModel.NewClientFormViewModel.Fields;
            this.PrimaryContactPersonFormViewModel.Fields = _clientsViewModel.PrimaryContactFormViewModel.Fields;
            this.DetailsFormViewModel.ColClass = "col-12";
           
        }

        public FormComponentViewModel<ClientDto> DetailsFormViewModel { get; set; }
        public FormComponentViewModel<ClientDto> PrimaryContactPersonFormViewModel { get; set; }
        public bool ShowNewClientModal {  get; set; }
    }
}

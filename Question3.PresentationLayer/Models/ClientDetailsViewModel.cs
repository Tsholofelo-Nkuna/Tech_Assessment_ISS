using Question3.BusinessLogicLayer.Models.DataTransferObjects;
using Question3.DataAccessLayer.Migrations;
using Question3.Models.DataTransferObjects;
using System.Diagnostics;

namespace Question3.PresentationLayer.Models
{
    public class ClientDetailsViewModel
    {
        private readonly ClientsViewModel _clientsViewModel = new ClientsViewModel();
        public ClientDetailsViewModel(ClientDto client) {
            this.ClientDto = client;
            this.DetailsFormViewModel = this._clientsViewModel.NewClientFormViewModel;
            this.PrimaryContactPersonFormViewModel = this._clientsViewModel.PrimaryContactFormViewModel;
            this.PrimaryContactPersonFormViewModel.FormState = this.ClientDto;
            this.DetailsFormViewModel.FormState = this.ClientDto;
            this.DetailsFormViewModel.CollapseFooter = false;
            this.PrimaryContactPersonFormViewModel.CollapseFooter = false;
            this.PrimaryContactPersonFormViewModel.IncludeFormStatePropsAsHiddent = true;
            this.DetailsFormViewModel.IncludeFormStatePropsAsHiddent = true;
            var primaryContact = this.ClientDto.ContactPerson
                .Where(x => x.IsPrimaryContact).FirstOrDefault();
            if(primaryContact is ContactPersonDto contactPersonDto)
            {
                this.ClientDto.PrimaryContactPhone = contactPersonDto.Phone; 
                this.ClientDto.PrimaryContactName = contactPersonDto.Name;
                this.ClientDto.PrimaryContactEmail = contactPersonDto.Email;
            }
        }
        public ClientDto ClientDto { get; set; }
        public FormComponentViewModel DetailsFormViewModel { get; set; }

        public FormComponentViewModel PrimaryContactPersonFormViewModel { get; set; }
    }
}

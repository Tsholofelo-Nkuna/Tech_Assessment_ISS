
using Core.Presentation.Models.DataTransferObjects;

namespace Core.Presentation.Models
{
    public class ClientDetailsViewModel
    {
        private readonly ClientsViewModel _clientsViewModel = new ClientsViewModel();
        public ClientDetailsViewModel(ClientDto client) {
            this.ClientDto = client;
            this.DetailsFormViewModel = this._clientsViewModel.NewClientFormViewModel;
            this.PrimaryContactPersonFormViewModel = this._clientsViewModel.PrimaryContactFormViewModel;
            this.PrimaryContactPersonFormViewModel.ViewModelState = this.ClientDto;
            this.DetailsFormViewModel.ViewModelState = this.ClientDto;
            this.DetailsFormViewModel.CollapseFooter = false;
            this.PrimaryContactPersonFormViewModel.CollapseFooter = false;
            this.PrimaryContactPersonFormViewModel.IncludeFormStatePropsAsHidden = true;
            this.DetailsFormViewModel.IncludeFormStatePropsAsHidden = true;
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
        public FormComponentViewModel<ClientDto> DetailsFormViewModel { get; set; }

        public FormComponentViewModel<ClientDto> PrimaryContactPersonFormViewModel { get; set; }
    }
}

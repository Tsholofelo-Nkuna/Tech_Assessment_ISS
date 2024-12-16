
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.Models.Base;
using Microsoft.AspNetCore.Http.Extensions;
using Core.Presentation.Models.Validators.Base;
using Core.Presentation.Models.Validators;
using Core.Presentation.Models.DataTransferObjects.Base;


namespace Core.Presentation.Models
{
    public class ClientsViewModel : GenericListViewModel<ClientDto>
    {
        public ClientsViewModel() : this(Enumerable.Empty<ClientDto>()){ }
        public ClientsViewModel(IEnumerable<ClientDto> clients): base(clients)
        {
            this.TableConfig.Filter = this.SearchFormComponentViewModel.ViewModelState.FirstOrDefault() ?? new ClientDto();
        }
        public FormComponentViewModel<ClientDto> SearchFormComponentViewModel { get; set; } = new FormComponentViewModel<ClientDto>(Enumerable.Empty<ClientDto>().Append(new ClientDto()), "ClientSearchForm")
        {
            ColClass = "col-4",
            SubmitButtonText = "Filter",
            Fields = new(){

                new InputFieldViewModel<ClientDto>(
                    nameof(ClientDto.CompanyName),
                    "Company Name"
                    ),

                new InputFieldViewModel<ClientDto>(
                    nameof(ClientDto.Archived),
                    "Show Archived Only"
                    
                    )
                {
                    ControlType = ControlType.Checkbox,
                }
            },
            ActionName = string.Empty, //nameof(HomeController.Index),
            ControllerName = "Home",
            FormName = "ClientSearchForm"
        };
       
        public TableComponentViewModel<ClientDto> TableConfig { get; set; } = new TableComponentViewModel<ClientDto>(new List<ClientDto>()){
            ColumnConfigs = new List<TableComponentColumnConfig>()
            {
                new TableComponentColumnConfig() { Index = nameof(ClientDto.CompanyName), Name = "Company name"},
                new TableComponentColumnConfig() { Index = nameof(ClientDto.TradingAs), Name = "Trading as"},
                new TableComponentColumnConfig() { Index = nameof(ClientDto.LandlineNumber), Name = "Landline no."},
                new TableComponentColumnConfig() { Index = nameof(ClientDto.Province), Name = "Province"},
                new TableComponentColumnConfig() { Index = nameof(ClientDto.Address), Name = "Address"}
            },
            DeleteAction = "Delete",//nameof(HomeController.Delete),
            DeleteController = "Clients",
            ArchiveAction = "Archive", //nameof(HomeController.Archive),
            ArchiveController = "Clients",
            ViewAction = "Details",//nameof(HomeController.Details),
            ViewController = "Clients",
            ShowCreateNewButton = true
        };

        public FormComponentViewModel<ClientDto> NewClientFormViewModel { get; set; } = new(Enumerable.Empty<ClientDto>().Append(new ClientDto()), "NewClientForm")
        {
            FormName = "NewClientDetails",
            Fields = new()
            {

                new InputFieldViewModel<ClientDto>(
                    nameof(ClientDto.CompanyName), 
                    "Company Name",
                    new ValidatorBase<ClientDto>(new ValidatorFn[] { Validators.Validators.Required()}, nameof(ClientDto.CompanyName))
                    ),
                new InputFieldViewModel<ClientDto>(
                    nameof(ClientDto.TradingAs),
                    "Trading As",
                    new ValidatorBase<ClientDto>(new ValidatorFn[] { Validators.Validators.Required()}, nameof(ClientDto.TradingAs))
                    ),
                 new InputFieldViewModel<ClientDto>(
                    nameof(ClientDto.LandlineNumber),
                    "Landline No",
                    new ValidatorBase<ClientDto>(new ValidatorFn[] { Validators.Validators.Required()}, nameof(ClientDto.LandlineNumber))
                    ),
                 new InputFieldViewModel<ClientDto>(
                    nameof(ClientDto.Province),
                    "Province",
                    new ValidatorBase<ClientDto>(new ValidatorFn[] { Validators.Validators.Required()}, nameof(ClientDto.Province))
                    ),
                 new InputFieldViewModel<ClientDto>(
                    nameof(ClientDto.Address),
                    "Address",
                    new ValidatorBase<ClientDto>(new ValidatorFn[] { Validators.Validators.Required()}, nameof(ClientDto.Address))
                    ),
            },
            ColClass = "col-12",
            ActionName = "/",//nameof(HomeController.AddOrUpdate),
            ControllerName = "Home",
            CollapseFooter = true,
        };
        public FormComponentViewModel<ClientDto> PrimaryContactFormViewModel { get; set; } = new (Enumerable.Empty<ClientDto>().Append(new ClientDto()), "ClientContactForm")
        {
            Fields = new()
            {

                new InputFieldViewModel<ClientDto>(
                    nameof(ClientDto.PrimaryContactName),
                    "Name",
                    new ValidatorBase<ClientDto>(new ValidatorFn[] { Validators.Validators.Required()}, nameof(ClientDto.PrimaryContactName))
                    ) { Type = "text"},
                new InputFieldViewModel<ClientDto>(
                    nameof(ClientDto.PrimaryContactEmail),
                    "Email",
                    new ValidatorBase<ClientDto>(new ValidatorFn[] { Validators.Validators.Required()}, nameof(ClientDto.PrimaryContactEmail))
                    ) {Type = "email"},
                new InputFieldViewModel<ClientDto>(
                    nameof(ClientDto.PrimaryContactPhone),
                    "Contact",
                    new ValidatorBase<ClientDto>(new ValidatorFn[] { Validators.Validators.Required()}, nameof(ClientDto.PrimaryContactPhone))
                    ){ Type = "text"},
            },
            ColClass = "col-12",
            ActionName = string.Empty, //nameof(HomeController.AddOrUpdate),
            ControllerName = "Home",
          
            CollapseFooter = true,
            FormName = "NewClientPrimaryContactForm"
            
        };
        public ModalViewModel<ClientDto> ModalViewModel { get; set; } = new ModalViewModel<ClientDto>();
   
      
    }
}

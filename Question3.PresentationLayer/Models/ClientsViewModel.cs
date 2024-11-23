using Question3.Models.DataTransferObjects;
using Question3.PresentationLayer.Controllers;
using System.Globalization;

namespace Question3.PresentationLayer.Models
{
    public class ClientsViewModel
    {
        public FormComponentViewModel SearchFormComponentViewModel { get; set; } = new()
        {
            ColClass = "col-4",
            SubmitButtonText = "Filter",
            Fields = new(){
                new InputFieldViewModel()
                {
                    Label = "Company Name",
                    Name = "CompanyName",
                },
                new InputFieldViewModel()
                {
                    Label = "Show Archived Only",
                    Name = "Archived",
                    ControlType = ControlType.Checkbox,
                }
            },
            HttpMethod = HttpMethods.Get,
            ActionName = nameof(HomeController.Index),
            ControllerName = "Home",
            FormState = new ClientDto()
        };
       

        public TableComponentViewModel TableConfig { get; set; } = new() {
            ColumnConfigs = new List<TableComponentColumnConfig>()
            {
                new TableComponentColumnConfig() { Index = nameof(ClientDto.CompanyName), Name = "Company name"},
                new TableComponentColumnConfig() { Index = nameof(ClientDto.TradingAs), Name = "Trading as"},
                new TableComponentColumnConfig() { Index = nameof(ClientDto.LandlineNumber), Name = "Landline no."},
                new TableComponentColumnConfig() { Index = nameof(ClientDto.Province), Name = "Province"},
                new TableComponentColumnConfig() { Index = nameof(ClientDto.Address), Name = "Address"}
            },
            DeleteAction = nameof(HomeController.Delete),
            DeleteController = "Home",
            ArchiveAction = nameof(HomeController.Archive),
            ArchiveController = "Home",
            ViewAction = nameof(HomeController.Details),
            ViewController = "Home",
            
        };

        public FormComponentViewModel NewClientFormViewModel { get; set; } = new()
        {
            Fields = new()
            {

                new InputFieldViewModel()
                {
                    Label = "Company Name",
                    Name = nameof(ClientDto.CompanyName),
                    Required = true,
                },
                new InputFieldViewModel()
                {
                    Label = "Trading As",
                    Name = nameof(ClientDto.TradingAs),
                    Required = true,
                },
                new InputFieldViewModel()
                {
                    Label = "Landline No",
                    Name = nameof(ClientDto.LandlineNumber),
                    Required = true,
                },
                new InputFieldViewModel()
                {
                    Label = "Province",
                    Name = nameof(ClientDto.Province),
                    Required = true,
                },
                new InputFieldViewModel()
                {
                    Label = "Address",
                    Name = nameof(ClientDto.Address),
                    Required = true,
                }
            },
            ColClass = "col-12",
            ActionName = nameof(HomeController.AddOrUpdate),
            ControllerName = "Home",
            HttpMethod = HttpMethods.Post,
            FormState = new ClientDto(),
            CollapseFooter = true,
        };
        public FormComponentViewModel PrimaryContactFormViewModel { get; set; } = new()
        {
            Fields = new()
            {

                new InputFieldViewModel()
                {
                    Label = "Name",
                    Name = nameof(ClientDto.PrimaryContactName),
                    Required = true,
                },
                new InputFieldViewModel()
                {
                    Label = "Email",
                    Name = nameof(ClientDto.PrimaryContactEmail),
                    Required = true,
                },
                new InputFieldViewModel()
                {
                    Label = "Contact",
                    Name = nameof(ClientDto.PrimaryContactPhone),
                    Required = true,
                }
            },
            ColClass = "col-12",
            ActionName = nameof(HomeController.AddOrUpdate),
            ControllerName = "Home",
            HttpMethod = HttpMethods.Post,
            FormState = new ClientDto(),
            CollapseFooter = true,
        };
    }
}

﻿
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Models;

namespace Core.Presentation.Models
{
    public class ClientsViewModel
    {
        public FormComponentViewModel<ClientDto> SearchFormComponentViewModel { get; set; } = new FormComponentViewModel<ClientDto>(new ClientDto())
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
            HttpMethod = "get",
            ActionName = string.Empty, //nameof(HomeController.Index),
            ControllerName = "Home",
           
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
            DeleteController = "Home",
            ArchiveAction = "Archive", //nameof(HomeController.Archive),
            ArchiveController = "Home",
            ViewAction = "Details",//nameof(HomeController.Details),
            ViewController = "Home",
        };

        public FormComponentViewModel<ClientDto> NewClientFormViewModel { get; set; } = new FormComponentViewModel<ClientDto>(new ClientDto())
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
            ActionName = string.Empty,//nameof(HomeController.AddOrUpdate),
            ControllerName = "Home",
            HttpMethod = "post",
           
            CollapseFooter = true,
        };
        public FormComponentViewModel<ClientDto> PrimaryContactFormViewModel { get; set; } = new FormComponentViewModel<ClientDto>(new ClientDto())
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
            ActionName = string.Empty, //nameof(HomeController.AddOrUpdate),
            ControllerName = "Home",
            HttpMethod = "post",
            CollapseFooter = true,
        };
        public ModalViewModel ModalViewModel { get; set; } = new ModalViewModel();
    }
}

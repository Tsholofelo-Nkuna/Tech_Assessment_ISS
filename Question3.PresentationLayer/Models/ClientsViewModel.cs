using Question3.Models.DataTransferObjects;

namespace Question3.PresentationLayer.Models
{
    public class ClientsViewModel
    {
        public List<InputFieldViewModel> searchFields { get; set; } = new List<InputFieldViewModel>()
        {
            new InputFieldViewModel()
            {
                Label = "Company Name",
                Name = "CompanyName",
            },
            new InputFieldViewModel()
            {
                Label = "Show Archived Only",
                Name = "ShowArchivedOnly",
                ControlType = ControlType.Checkbox,
            }
        };

        public List<ClientDto> clients { get; set; } = new List<ClientDto>();
    }
}

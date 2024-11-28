using Core.Presentation.Models;

namespace ClientManagement.Presentation.Web.Components.Pages
{
    public partial class Home
    {
        public ClientsViewModel ViewModel { get; set; } = new ClientsViewModel();
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}

using Core.Presentation.Models;
using Core.Presentation.Models.Models;
using Core.Presentation.Models.Models.DataTransferObjects;
using Core.Presentation.ViewComponents.Components.Base;

namespace ClientManagement.Presentation.Web.Components.Pages.Products
{
    public partial class Index : GenericComponentBase<ProductsViewModel, ProductDto>
    {
        public Task OnCreateNewProduct()
        {
            this.ViewModel.NewProductModalViewModel.Show = true;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}

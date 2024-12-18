using ClientManagement.Presentation.Web.Components.Pages.Products.State;
using Core.Presentation.Models;
using Core.Presentation.Models.Models;
using Core.Presentation.Models.Models.DataTransferObjects;
using Core.Presentation.ViewComponents.Components.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Extensions;

namespace ClientManagement.Presentation.Web.Components.Pages.Products
{
    public partial class Index : GenericComponentBase<ProductsViewModel, ProductDto>
    {
        public ProductDto SearchFormFilters => 
            this.ViewModel.ProductSearchViewModel.ViewModelState.FirstOrDefault() ?? new ProductDto();
        [Inject]
        public ProductStateManager StateManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
             await base.OnInitializedAsync();
             this.BaseUrl = "api/products";
            if (this.StateManager != null && this.StateManager.Get<ProductDto>(nameof(ProductState.NewProductToCreate)) is ProductDto newProductToCreate) {
                var response = await this.AppApi
                       .PostAsJsonAsync($"{this.BaseUrl}", newProductToCreate);
                if (response.IsSuccessStatusCode && await response.Content.ReadFromJsonAsync<bool>())
                {
                    ProductDto? productResetVal = null;
                    this.StateManager.Set(nameof(ProductState.NewProductToCreate), productResetVal);
                }
            }
             this.ViewModel.ProductTableViewModel.ViewModelState = await this.GetData(this.SearchFormFilters);
             StateHasChanged();
        }

        public async Task<IEnumerable<ProductDto>> GetData(ProductDto filters)
        {
            var response = await this.AppApi.PostAsJsonAsync($"{this.BaseUrl}/Get", filters);
            if (response.IsSuccessStatusCode) { 
                var responseResult = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
               return responseResult ?? Enumerable.Empty<ProductDto>();
                
            }
            else
            {
                return Enumerable.Empty<ProductDto>();
            }
        }
        public Task OnCreateNewProduct()
        {
            this.ViewModel.NewProductModalViewModel.Show = true;
            StateHasChanged();
            return Task.CompletedTask;
        }

        public Task OnSaveNewProduct(IEnumerable<ProductDto> products)
        {
            this.StateManager.Set(nameof(ProductState.NewProductToCreate), products.FirstOrDefault());
            return Task.CompletedTask;
        }

        public async Task OnDeleteProduct(bool deleted)
        {
            if (deleted)
            {
                this.ViewModel.ProductTableViewModel.ViewModelState = await this.GetData(this.SearchFormFilters);
                StateHasChanged();
            }
            
        }
    }
}

using Core.Presentation.Models.Base;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation.Models.Models
{
    public class ProductsViewModel : GenericListViewModel<ProductDto>
    {
        public ProductsViewModel(): this(Enumerable.Empty<ProductDto>().Append(new ProductDto())) { }
        public ProductsViewModel(IEnumerable<ProductDto> state) : base(state)
        {
        }

        public FormComponentViewModel<ProductDto> ProductSearchViewModel { get; set; } =
        new FormComponentViewModel<ProductDto>(Enumerable.Empty<ProductDto>().Append(new ProductDto()), "Product-Search-Form")
        {
            ActionName = "Get",
            ControllerName = "Products",
            Fields = new List<InputFieldViewModel<ProductDto>>
            {
                new InputFieldViewModel<ProductDto>(nameof(ProductDto.Name), "Name")
                {
                    ControlType = ControlType.Input,
                    Type = "text", 
                },
                 new InputFieldViewModel<ProductDto>(nameof(ProductDto.Archived), "Show Archived Only")
                {
                    ControlType = ControlType.Checkbox,  
                }
            },
            ColClass = "col-4"
        };

        public TableComponentViewModel<ProductDto> ProductTableViewModel { get; set;  } =
            new TableComponentViewModel<ProductDto>
            {
                DeleteAction = "Delete",
                DeleteController = "Products",
                ArchiveController = "Products",
                ArchiveAction = "Archive",
                ColumnConfigs = new List<TableComponentColumnConfig>
                {
                    new TableComponentColumnConfig
                    {
                        Name = "Name",
                        Index = nameof(ProductDto.Name),
                    },
                    new TableComponentColumnConfig
                    {
                        Name = "Description",
                        Index = nameof(ProductDto.Description),
                    },
                     new TableComponentColumnConfig
                    {
                        Name = "Price",
                        Index = nameof(ProductDto.Price),
                    }
                },
                ShowCreateNewButton = true
            };

        public FormComponentViewModel<ProductDto> NewProductFormViewModel { get; set; } =
            new FormComponentViewModel<ProductDto>(Enumerable.Empty<ProductDto>().Append(new ProductDto()), "New-Product-Form")
            {
                Fields = new List<InputFieldViewModel<ProductDto>>
                {
                    new InputFieldViewModel<ProductDto>(nameof(ProductDto.Name), "Name")
                    {
                        ControlType = ControlType.Input,
                        Type = "text",
                    },
                     new InputFieldViewModel<ProductDto>(nameof(ProductDto.Description), "Description")
                    {
                        ControlType = ControlType.Input,
                        Type = "text"
                    },
                      new InputFieldViewModel<ProductDto>(nameof(ProductDto.Price), "Price")
                    {
                        ControlType = ControlType.Input,
                        Type = "number"
                    }
                },
                ColClass = "col-12",
                
            };
        public ModalViewModel<BaseDto> NewProductModalViewModel { get; set; } =
            new ModalViewModel<BaseDto>()
            {
                Title = "New Product"
            };
        
    }
}

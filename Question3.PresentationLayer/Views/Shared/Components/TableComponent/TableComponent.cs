using Microsoft.AspNetCore.Mvc;
using Question3.PresentationLayer.Models;

namespace Question3.PresentationLayer.Views.Shared.Components.TableComponent
{
    public class TableComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(TableComponentViewModel options )
        {
            return Task.FromResult<IViewComponentResult>(View(options));
        }
    }
}

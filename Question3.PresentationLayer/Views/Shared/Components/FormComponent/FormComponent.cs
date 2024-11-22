using Microsoft.AspNetCore.Mvc;
using Question3.PresentationLayer.Models;

namespace Question3.PresentationLayer.Views.Shared.Components.FormComponent
{
    public class FormComponent: ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(FormComponentViewModel options)
        {
          
            return Task.FromResult<IViewComponentResult>(View(options));
        }
    }
}

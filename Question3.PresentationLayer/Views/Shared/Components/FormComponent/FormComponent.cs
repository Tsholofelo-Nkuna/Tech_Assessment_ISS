using Microsoft.AspNetCore.Mvc;
using Question3.PresentationLayer.Models;

namespace Question3.PresentationLayer.Views.Shared.Components.FormComponent
{
    public class FormComponent: ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(List<InputFieldViewModel> inputFields, string colClass, string submitButtonText)
        {
          
            return Task.FromResult<IViewComponentResult>(View(new FormComponentViewModel { ColClass = colClass, Fields = inputFields, SubmitButtonText = submitButtonText}));
        }
    }
}

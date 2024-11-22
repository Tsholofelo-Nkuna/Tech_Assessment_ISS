using System.Globalization;

namespace Question3.PresentationLayer.Models
{
    public class FormComponentViewModel
    {
        public List<InputFieldViewModel> Fields { get; set; } = new List<InputFieldViewModel>();
        public string ColClass { get; set; } = "col-4";
        public string SubmitButtonText = "Submit";
        public string ActionName = string.Empty;
        public string ControllerName = string.Empty;
        public string HttpMethod = HttpMethods.Post;
        public object? FormState { get; set; } = null;
        public string GetValue(string propName)
        {
           return this.FormState?.GetType().GetProperty(propName)?.GetValue(this.FormState)?.ToString() ?? string.Empty;
        }

        public void SetValue(string propName, string propValue) { 
           this.FormState?.GetType().GetProperty(propName)?.SetValue(this.FormState, propValue);
        }

    }
}

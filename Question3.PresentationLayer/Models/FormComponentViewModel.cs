using AutoMapper.Internal;
using Question3.Models.DataTransferObjects.Base;
using System.Globalization;
using System.Reflection;

namespace Question3.PresentationLayer.Models
{
    public class FormComponentViewModel
    {
        public List<InputFieldViewModel> Fields { get; set; } = new List<InputFieldViewModel>();
        public string ColClass { get; set; } = "col-4";
        public string SubmitButtonText { get; set; } = "Submit";
        public string ActionName { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = HttpMethods.Post;
        public bool CollapseFooter { get; set; } = false;
        public BaseDto FormState { get; set; } = new BaseDto();
        public string GetValue(string propName)
        {
           return this.FormState?.GetType().GetProperty(propName)?.GetValue(this.FormState)?.ToString() ?? string.Empty;
        }

        public IEnumerable<KeyValuePair<string, object?>> FormStateAsKeyValuePair ()
        {
           return this.FormState
                .GetType()
                .GetProperties()
                .Where(prop => prop.CanRead && prop.GetGetMethod() is MethodInfo and { IsPublic : true})
                .Select(prop => new KeyValuePair<string, object?>(prop.Name, prop.GetValue(this.FormState)));
        }

        public void SetValue(string propName, string propValue) { 
           this.FormState?.GetType().GetProperty(propName)?.SetValue(this.FormState, propValue);
        }

    }
}

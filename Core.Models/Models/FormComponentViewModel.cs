
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Models.Base;
using System.Reflection;

namespace Core.Presentation.Models
{
   
    public class FormComponentViewModel<TFormState>: GenericViewModel<TFormState> where TFormState: BaseDto, new()
    {
        public FormComponentViewModel(TFormState formState): base(formState) { }
     
        public List<InputFieldViewModel> Fields { get; set; } = new List<InputFieldViewModel>();
        public string ColClass { get; set; } = "col-4";
        public string SubmitButtonText { get; set; } = "Submit";
        public string ActionName { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = "post";
        public bool CollapseFooter { get; set; } = false;
        public bool IncludeFormStatePropsAsHidden {  get; set; }

        public IEnumerable<KeyValuePair<string, object?>> FormStateAsKeyValuePair ()
        {
           return this.ViewModelState
                .GetType()
                .GetProperties()
                .Where(prop => prop.CanRead && prop.GetGetMethod() is MethodInfo and { IsPublic : true})
                .Select(prop => new KeyValuePair<string, object?>(prop.Name, prop.GetValue(this.ViewModelState)));
        }

    }
}

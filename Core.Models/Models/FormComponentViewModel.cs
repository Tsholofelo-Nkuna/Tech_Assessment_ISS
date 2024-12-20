
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Base;
using System.Reflection;

namespace Core.Presentation.Models
{

    public class FormComponentViewModel<TRecordType> : GenericListViewModel<TRecordType> where TRecordType : BaseDto, new()
    {
        public FormComponentViewModel() : this(Enumerable.Empty<TRecordType>(), string.Empty)
        {

        }
        public FormComponentViewModel(IEnumerable<TRecordType> vModelState, string formName) : base(vModelState) {
            this.FormName = formName;
        }

        public List<InputFieldViewModel<TRecordType>> Fields { get; set; } = new List<InputFieldViewModel<TRecordType>>();
        public string ColClass { get; set; } = "col-4";
        public string SubmitButtonText { get; set; } = "Submit";
        public string ActionName { get; set; } = string.Empty;
        public string FormName { get; set; }
        public string ControllerName { get; set; } = string.Empty;
        [Obsolete("Do not use.")]
        public string HttpMethod { get; set; } = "post";
        public bool CollapseFooter { get; set; }
        public bool IncludeFormStatePropsAsHidden { get; set; }
        public bool WasValidated { get; set; }
        public bool IsValid
        {
            get
            {
                var validators = this.Fields
                    .Where(x => x.Validator != null)
                    .Select(x => x.Validator);
                    
                if (validators.Any())
                {
                   var validationResults = this.ViewModelState
                        .SelectMany(record =>
                        {
                            return validators.Select(validatorFn => validatorFn?.Validate(record));
                        });
                    return validationResults.All(r => r is null);
                }
                else
                {
                    return false;
                }
            }
        }

    }
}


using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Models.Base;
using System.Reflection;

namespace Core.Presentation.Models
{
   
    public class FormComponentViewModel<TRecordType>: GenericListViewModel<TRecordType> where TRecordType : BaseDto, new()
    {
       public FormComponentViewModel(): base(Enumerable.Empty<TRecordType>())
        {

        }
        public FormComponentViewModel(IEnumerable<TRecordType> vModelState): base(vModelState) { }
     
        public List<InputFieldViewModel> Fields { get; set; } = new List<InputFieldViewModel>();
        public string ColClass { get; set; } = "col-4";
        public string SubmitButtonText { get; set; } = "Submit";
        public string ActionName { get; set; } = string.Empty;
        public string ControllerName { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = "post";
        public bool CollapseFooter { get; set; } = true;
        public bool IncludeFormStatePropsAsHidden {  get; set; }

    }
}

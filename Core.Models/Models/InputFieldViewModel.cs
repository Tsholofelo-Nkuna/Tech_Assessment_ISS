using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Interfaces.Base;
using Core.Presentation.Models.Validators.Base;

namespace Core.Presentation.Models
{
   public enum ControlType
    {
        Input,
        Select,
        Checkbox
    }

    public static class InputType
    {
        public static string Text = "text";
        public static string Checkbox = "checkbox";
        public static string Email = "email";
        public static string Password = "password";
    }
    public class InputFieldViewModel<TRecord> where TRecord: BaseDto, new()
    {
        public InputFieldViewModel(string name, string label, IValidatorBase<TRecord>? validator = null) {
            this.Name = name;
            this.Label = label;
            this.Validator = validator;
        }
        public ControlType ControlType { get; set; } = ControlType.Input;
        public string Label { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty ;
        [Obsolete("Stop using, use the built in Validator instead")]
        public bool Required { get; set; }
        public IValidatorBase<TRecord>? Validator { get; set; }

        public string Type { get; set; } = InputType.Text;


    }
}

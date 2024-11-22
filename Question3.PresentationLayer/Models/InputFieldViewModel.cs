namespace Question3.PresentationLayer.Models
{
   public enum ControlType
    {
        Input,
        Select,
        Checkbox
    }
    public class InputFieldViewModel
    {
        public ControlType ControlType { get; set; } = ControlType.Input;
        public string Label { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty ;
        public string Value { get; set; } = string.Empty ;
    }
}

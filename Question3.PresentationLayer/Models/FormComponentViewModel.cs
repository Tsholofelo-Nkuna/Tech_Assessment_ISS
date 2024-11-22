namespace Question3.PresentationLayer.Models
{
    public class FormComponentViewModel
    {
        public List<InputFieldViewModel> Fields { get; set; } = new List<InputFieldViewModel>();
        public string ColClass { get; set; } = "col-4";

        public string SubmitButtonText = "Submit";
    }
}

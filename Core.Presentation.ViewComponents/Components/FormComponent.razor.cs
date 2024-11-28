using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects.Base;
using Microsoft.AspNetCore.Components;


namespace Core.Presentation.ViewComponents.Components
{
    public partial class FormComponent<TState> where TState : BaseDto, new()
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            //this.Model.AddStateChangeListener(OnFormStateChanged); 
        }

        [Parameter]
        public FormComponentViewModel<TState> Model { get; set; } = new FormComponentViewModel<TState>(new TState());


        public void FormSubmitClick()
        {
           
        }

        public void OnFormStateChanged(TState newState)
        {
          
        }

        //public string Get(string key)
        //{
        //    var returned = this.Model.Get(key);
        //    return 
        //}

        ~FormComponent()
        {
            //this.Model.ClearStateChangeListeners();
        }
    }
}

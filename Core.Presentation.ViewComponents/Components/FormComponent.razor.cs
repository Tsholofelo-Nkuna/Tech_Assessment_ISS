using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.ViewComponents.Components.Base;
using Core.Presentation.ViewComponents.Interfaces;
using Core.Presentation.ViewComponents.Interfaces.Base;
using Microsoft.AspNetCore.Components;

namespace Core.Presentation.ViewComponents.Components
{
    public partial class FormComponent<TRecord>: GenericComponentBase<FormComponentViewModel<TRecord>, TRecord> where TRecord : BaseDto, new()
    {

        [Parameter] 
        public EventCallback<IEnumerable<TRecord>> ViewModelStateChanged { get; set; } = new EventCallback<IEnumerable<TRecord>>();
        [Parameter]
        public EventCallback<IEnumerable<TRecord>> OnFormSubmitClick { get; set; } = new EventCallback<IEnumerable<TRecord>>();
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.ViewModel.AddViewModelStateChangeListener(this.OnViewModelStateChanged); 
        }
        public void FormSubmitClick()
        {
        }

        public override async Task OnViewModelStateChanged(IEnumerable<TRecord> update)
        {
            await base.OnViewModelStateChanged(update);
            await ViewModelStateChanged.InvokeAsync(update);
            
        }

        public async Task OnFormSubmit()
        {
            await OnFormSubmitClick.InvokeAsync(this.ViewModel.ViewModelState);
        }


        //public string Get(string key)
        //{
        //    var returned = this.Model.Get(key);
        //    return 
        //}

        ~FormComponent()
        {
           this.ViewModel.ClearViewModelStateChangeListeners();
        }
    }
}

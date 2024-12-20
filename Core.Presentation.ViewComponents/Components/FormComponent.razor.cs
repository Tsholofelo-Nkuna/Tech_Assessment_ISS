using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.ViewComponents.Components.Base;
using Core.Presentation.ViewComponents.Interfaces;
using Core.Presentation.ViewComponents.Interfaces.Base;
using Microsoft.AspNetCore.Components;
using System;

namespace Core.Presentation.ViewComponents.Components
{
    public partial class FormComponent<TRecord>: GenericComponentBase<FormComponentViewModel<TRecord>, TRecord> where TRecord : BaseDto, new()
    {

        [Parameter] 
        public EventCallback<IEnumerable<TRecord>> ViewModelStateChanged { get; set; } = new EventCallback<IEnumerable<TRecord>>();
        [Parameter]
        public Func<IEnumerable<TRecord>, Task>? OnFormSubmitClick { get; set; }

        [Parameter]
        public Action<KeyValuePair<string, string>?>? OnValidationFail { get; set; }
        public bool IsValid => this.ViewModel.IsValid;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.ViewModel.AddViewModelStateChangeListener(this.OnViewModelStateChanged); 
        }
      

        public override async Task OnViewModelStateChanged(IEnumerable<TRecord> update)
        {
            await base.OnViewModelStateChanged(update);
            await ViewModelStateChanged.InvokeAsync(update);
            
        }

        public void OnInputChange(Guid id, InputFieldViewModel<TRecord> field, object? value)
        {
            ViewModel.Set(id, field.Name, value);
            
           if(field.Validator is not null)
            {
                var validationResult = field.Validator.Validate(this.ViewModel?.ViewModelState?.FirstOrDefault());
                if(validationResult is null)
                {
                    //validation succeeded;
                }
                else
                {
                   
                    if(this.OnValidationFail is not null)
                    {
                        this.OnValidationFail(validationResult);
                    }
                }
                this.ViewModel.WasValidated = true;
            }
            else
            {
                ViewModel.Set(id, field.Name, value);
            }
        }

        public Task OnFormSubmit()
        {
            
            OnFormSubmitClick?.Invoke(this.ViewModel.ViewModelState);
            return Task.CompletedTask;
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

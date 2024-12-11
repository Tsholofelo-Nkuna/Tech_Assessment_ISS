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
        public bool IsValid { 
            get
            {
                var result = Enumerable.Empty<KeyValuePair<string, string>?>();
                foreach(var rec in this.ViewModel.ViewModelState)
                {
                    foreach(var fieldConfig in this.ViewModel.Fields)
                    {
                        if (fieldConfig.Validator is not null)
                        {
                            result.Append(fieldConfig.Validator.Validate(rec));
                        }
                        
                    }
                    
                }
                return result.All(x => x == null);  
            }
        }
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
           this.ViewModel.IsValid = this.IsValid;
           if(field.Validator is not null)
            {
                var validationResult = field.Validator.Validate(this.ViewModel?.ViewModelState?.FirstOrDefault());
                if(validationResult is null)
                {
                    ViewModel.Set(id, field.Name, value);
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
            this.ViewModel.IsValid = this.IsValid;  
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

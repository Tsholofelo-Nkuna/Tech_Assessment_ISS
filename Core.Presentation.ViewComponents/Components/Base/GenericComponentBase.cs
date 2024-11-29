﻿using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Interfaces.Base;
using Core.Presentation.Models.Models.Base;
using Core.Presentation.ViewComponents.Interfaces.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Presentation.ViewComponents.Components.Base
{
    public abstract class GenericComponentBase<TViewModel, TRecordType> : ComponentBase, IGenericComponentBase<TViewModel, TRecordType>
        where TViewModel : IGenericListViewModel<TRecordType>, new()
        where TRecordType : BaseDto, new()
    {
       
        public GenericComponentBase() : this(new TViewModel()) { }
        public GenericComponentBase(TViewModel viewModel)
        {
            ViewModel = viewModel;
            viewModel.OnViewModelStateChangedEvent += this.OnViewModelStateChanged;

        }
        [Parameter]
        public TViewModel ViewModel { get; set; }
        [Inject]
        public NavigationManager? NavManager { get; set; }
        public void OnNavigate(string controllerName, string actionName, Guid stateId)
        {
            var baseUrl = string.IsNullOrEmpty(controllerName) ? "/" : "";
            NavManager.NavigateTo($"{controllerName}/{actionName}/{stateId}");
        }

        public virtual Task OnViewModelStateChanged(IEnumerable<TRecordType> update)
        {
           return Task.CompletedTask;
        }


        public IEnumerable<KeyValuePair<string, object?>> RecordAsKeyValuePairs(TRecordType record) =>
             typeof(TRecordType)
                .GetProperties()
                .Where(x => x is PropertyInfo { CanRead: true } and { GetMethod.IsPublic: true })
                .Select(pInfo => new KeyValuePair<string, object>(pInfo.Name, pInfo.GetValue(record)));

        ~GenericComponentBase()
        {
            this.ViewModel.OnViewModelStateChangedEvent-= this.OnViewModelStateChanged;
        }

    }
}

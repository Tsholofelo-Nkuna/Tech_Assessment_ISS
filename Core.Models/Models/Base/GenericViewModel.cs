using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Interfaces.Base;
using Core.Presentation.ViewComponents.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Core.Presentation.Models.Models.Base
{
    public class GenericViewModel<TState> : IGenericViewModel<TState> where TState : BaseDto, new()
    {
        public event IGenericViewModel<TState>.ViewModelStateChangeDelegate? ViewModelStateChangeEvent;
        public GenericViewModel(TState viewModelState) { 
            this.ViewModelState = viewModelState;
        }

        public TState ViewModelState { get; set; }
        public dynamic Get(string propName, Type propType)
        {
            var propVal = this.ViewModelState.GetType()?.GetProperty(propName)?.GetValue(this.ViewModelState);
            return this.ConvertSource(propType, propVal);
        }

        public void Set<TSource>(string propName, TSource propValue)
        {
            var prop = this.ViewModelState?.GetType().GetProperty(propName);
           
            if (prop is PropertyInfo propInfo and { CanWrite: true, SetMethod.IsPublic: true }
            && (this.ViewModelState is not null) && propValue is object)
            {
                propInfo.SetValue(this.ViewModelState, this.ConvertSource(propInfo.PropertyType, propValue));
                if (this.ViewModelStateChangeEvent != null)
                {
                    this.ViewModelStateChangeEvent(this.ViewModelState);
                }
            }
        }

        public dynamic? ConvertSource<TTarget>(TTarget targetType, object? source) where TTarget : Type 
        {
           var stringType = typeof(string).FullName;
           var intType = typeof(int).FullName;  
           var boolType = typeof(bool).FullName;
           var targetTypeName = targetType.FullName;
           if(targetTypeName == stringType && source is object validSource)
            {
                return  Convert.ToString(validSource);
            }
            else if (targetTypeName == boolType && source is object validBoolSource)
            {
                return Convert.ToBoolean(validBoolSource);
            }
            else if (targetTypeName == intType && source is object validIntSource)
            {
                return Convert.ToInt32(validIntSource);
            }
            else
            {
                return null;
            }
        }

        public void AddStateChangeListener(IGenericViewModel<TState>.ViewModelStateChangeDelegate stateChangeListener)
        {
            this.ViewModelStateChangeEvent += stateChangeListener;
        }

        public void ClearStateChangeListeners()
        {
            this.ViewModelStateChangeEvent = null;
        }

        public void Set(string key, object? val, IBindToSetterDelegateBase<string, object?, TState>.BindToSetterDelegate setter)
        {
            setter(key, val, this.ViewModelState);
        }

        public Action<TKey> Set<TKey, TValue>(TValue value) 
        {
            return (TKey key) =>
            {
                this.Set(key as string, value);
            };
        }


    }
}

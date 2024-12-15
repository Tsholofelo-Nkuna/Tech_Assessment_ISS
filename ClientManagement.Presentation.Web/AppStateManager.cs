using ClientManagement.Presentation.Web.Interfaces;
using System.Reflection;

namespace ClientManagement.Presentation.Web
{
    public class AppStateManager<TState> : IAppStateManager<TState> where TState : new()
    {
        public TState State { get; init; } = new TState();

        public TReturn? Get<TReturn>(string propName)
        {
          var val =  State?.GetType()?.GetProperty(propName)?.GetValue(State);
          if(val is TReturn validReturn)
          {
                return validReturn;
          }
          else
          {
               return default;
          }
        }

        public TState Set<TTarget>(string propName, TTarget value)
        {
            var prop = State?.GetType()?.GetProperty(propName);
            if (prop is PropertyInfo and { SetMethod.IsPublic : true} propInfo && propInfo.PropertyType == typeof(TTarget)) { 
               propInfo.SetValue(State, value);
            }
            return State;
        }
    }
}

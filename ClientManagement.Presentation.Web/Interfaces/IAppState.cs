﻿namespace ClientManagement.Presentation.Web.Interfaces
{
    public interface IAppStateManager<TState> where TState :  new()
    {
        public TState State { get; init; }
        public TState Set<TTarget>(string propName, TTarget value);
        public TReturn? Get<TReturn>(string propName);
    }
}
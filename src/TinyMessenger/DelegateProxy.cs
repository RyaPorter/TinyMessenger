using System;
using System.Collections.Generic;

namespace TinyMessenger
{
    public class DelegateProxy : ITinyProxy, IDisposable
    {
        Dictionary<Type, List<object>> Subscribers { get; set; } = new Dictionary<Type, List<object>>();

        public void Send<T>(T message)
        {
            var subs = this.Subscribers[typeof(T)];

            foreach (var sub in subs)
            {
                (sub as Action<T>)?.Invoke(message);
            }
        }

        public void Listen<T>(Action<T> callback)
        {
            List<object> registeredSubs;
            if (this.Subscribers.TryGetValue(typeof(T), out registeredSubs))
            {
                registeredSubs.Add(callback);
            }
            else
            {
                this.Subscribers.Add(typeof(T), new List<object>() { callback });
            }

        }

        public void Dispose()
        {
            foreach (var sub in this.Subscribers)
            {
                sub.Value.Clear();
            }
        }
    }
}
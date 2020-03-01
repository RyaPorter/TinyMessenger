using System;
using System.Linq;
using System.Collections.Generic;

namespace TinyMessenger
{

    public interface ITinyProxy
    {
        void Send<T>(T message);

        void Listen<T>(Action<T> callback);
    }

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

    public class TinyMessenger : IDisposable
    {

        public TinyMessenger()
        {
            this.MessageProxy = new DelegateProxy();
        }

        public TinyMessenger(ITinyProxy messageProxy)
        {
            this.MessageProxy = messageProxy;
        }

        ITinyProxy MessageProxy { get; set; }

        public void Send<T>(T message)
        {
            this.MessageProxy.Send<T>(message);
        }

        public void Listen<T>(Action<T> callback)
        {
            this.Listen<T>(callback);
        }

        public void Dispose()
        {
            if (this.MessageProxy is IDisposable)
            {
                (this.MessageProxy as IDisposable).Dispose();
            }
        }

    }
}
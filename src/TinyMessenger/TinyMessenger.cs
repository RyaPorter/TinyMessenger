using System;
using System.Linq;
using System.Collections.Generic;
using TinyMessenger;

namespace TinyMessenger
{

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
            this.MessageProxy.Listen<T>(callback);
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
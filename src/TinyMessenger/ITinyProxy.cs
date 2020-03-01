using System;

namespace TinyMessenger
{
    public interface ITinyProxy
    {
        void Send<T>(T message);

        void Listen<T>(Action<T> callback);
    }
}
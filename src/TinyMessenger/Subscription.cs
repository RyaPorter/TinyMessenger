using System;

namespace TinyMessenger
{
    internal class Subscription<T> : ISubscription
    {
        public string Channel { get; set; }
        public Type SubscriptionType { get; set; }

        public Action<CallbackContext<T>, T> Handler { get; set; }

        public void InvokePayload(object callbackContext = null, object args = null)
        {
            if (args is T && callbackContext is CallbackContext<T>)
            {
                Handler?.Invoke((CallbackContext<T>)callbackContext, (T)args);
            }
        }
    }
}
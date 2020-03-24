using System;

namespace TinyMessenger
{

    internal class Subscription<TPayload> : ISubscription
    {
        public string Channel { get; set; }
        public Type SubscriptionType { get; set; }

        public Action<TPayload> Callback { get; set; }

        public void InvokePayload(object args = null)
        {
            if (args is TPayload)
            {
                Callback?.Invoke((TPayload)args);
            }
        }
    }
}
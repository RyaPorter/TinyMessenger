using System;

namespace TinyMessenger
{

    internal interface ISubscription
    {
        string Channel { get; set; }
        Type SubscriptionType { get; set; }

        void InvokePayload(object args = null);
    }
}
using System;
using System.Reflection;

namespace TinyMessenger
{

    internal interface ISubscription
    {
        string Channel { get; set; }
        Type SubscriptionType { get; set; }

        void InvokePayload(object callbackContext = null, object args = null);
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;

namespace TinyMessenger
{

    internal class SubscriptionChannel
    {
        public string ChannelName { get; set; }
        public IDictionary<Type, IList<ISubscription>> ChannelSubscriptions { get; } = new Dictionary<Type, IList<ISubscription>>();
    }
}
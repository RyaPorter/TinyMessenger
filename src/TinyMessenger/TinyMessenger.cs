using System;
using System.Collections.Generic;

namespace TinyMessenger
{

    public class TinyMessenger
    {

        IDictionary<string, SubscriptionChannel> SubscriptionChannels { get; } = new Dictionary<string, SubscriptionChannel>();

        public void Send<T>(string channel, T message)
        {
            SubscriptionChannel subscriptionChannel = null;

            if (this.SubscriptionChannels.TryGetValue(channel, out subscriptionChannel))
            {
                if (subscriptionChannel.ChannelSubscriptions.TryGetValue(typeof(T), out IList<ISubscription> subscriptions))
                {
                    foreach (var sub in subscriptions)
                    {
                        sub.InvokePayload(message);
                    }
                }
            }
        }

        public void Subscribe<T>(string channel, Action<T> callback)
        {
            var sub = new Subscription<T>()
            {
                Channel = channel,
                SubscriptionType = typeof(T),
                Callback = callback
            };

            this.Subscribe<T>(channel, sub);
        }

        void Subscribe<T>(string channel, ISubscription sub)
        {

            SubscriptionChannel subscriptionChannel = null;

            if (!this.SubscriptionChannels.TryGetValue(channel, out subscriptionChannel))
            {
                var subChannel = new SubscriptionChannel() { ChannelName = channel };
                subscriptionChannel = subChannel;
                this.SubscriptionChannels.Add(channel, subChannel);
            }

            if (subscriptionChannel.ChannelSubscriptions.TryGetValue(typeof(T), out IList<ISubscription> subscription))
            {
                subscription?.Add(sub);
            }
            else
            {
                var subs = new List<ISubscription>();
                subs.Add(sub);
                subscriptionChannel.ChannelSubscriptions.Add(typeof(T), subs);
            }
        }
    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using TinyMessenger;

namespace TinyMessenger
{

    internal interface ISubscription
    {
        string Channel { get; set; }
        Type SubscriptionType { get; set; }

        void InvokePayload(object args = null);
    }

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

    internal class SubscriptionChannel
    {
        public string ChannelName { get; set; }
        public IDictionary<Type, IList<ISubscription>> ChannelSubscriptions { get; } = new Dictionary<Type, IList<ISubscription>>();
    }

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
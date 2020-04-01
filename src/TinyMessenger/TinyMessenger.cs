using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace TinyMessenger
{

    public class TinyMessenger
    {
        IDictionary<string, SubscriptionChannel> SubscriptionChannels { get; } = new Dictionary<string, SubscriptionChannel>();

        IDictionary<MethodBase, ICallback> Callbacks { get; } = new Dictionary<MethodBase, ICallback>();

        public void Send<T>(string channel, T message)
        {
            foreach (var sub in this.GetSubscriptions(channel, message))
            {
                sub.InvokePayload(new CallbackContext<T>((a, b) => { }), message);
            }
        }

        public void Send<T>(string channel, T message, Action<T, bool> callback)
        {
            foreach (var sub in this.GetSubscriptions(channel, message))
            {
                var cx = new CallbackContext<T>(callback);
                sub.InvokePayload(cx, message);
            }
        }

        public async Task SendAsync<T>(string channel, T message)
        {
            foreach (var sub in this.GetSubscriptions(channel, message))
            {
                Func<Task> t = async () =>
                {
                    await Task.Yield();
                    sub.InvokePayload(new CallbackContext<T>((a, b) => { }), message);
                };
                await t().ConfigureAwait(false);
            }
        }

        public async Task SendAsync<T>(string channel, T message, Action<T, bool> callback)
        {
            foreach (var sub in this.GetSubscriptions(channel, message))
            {
                Func<Task> t = async () =>
                {
                    await Task.Yield();
                    var cx = new CallbackContext<T>(callback);

                    sub.InvokePayload(cx, message);
                };
                await t().ConfigureAwait(false);
            }
        }

        public void Subscribe<T>(string channel, Action<CallbackContext<T>, T> handler)
        {
            var sub = new Subscription<T>()
            {
                Channel = channel,
                SubscriptionType = typeof(T),
                Handler = handler
            };

            this.Subscribe<T>(channel, sub);
        }

        public void Subscribe<T>(string channel, Action<T> handler)
        {
            var sub = new Subscription<T>()
            {
                Channel = channel,
                SubscriptionType = typeof(T),
                Handler = (callback, m) =>
                {
                    handler?.Invoke(m);
                    callback?.RaiseCallback(m, true);
                }
            };

            this.Subscribe<T>(channel, sub);
        }

        void Subscribe<T>(string channel, ISubscription sub)
        {
            SubscriptionChannel subscriptionChannel = null;
            subscriptionChannel = GetChannel(channel);

            if (subscriptionChannel.ChannelSubscriptions.TryGetValue(typeof(T), out IList<ISubscription> subscription))
            {
                subscription?.Add(sub);
                return;
            }

            var subs = new List<ISubscription>();
            subs.Add(sub);
            subscriptionChannel.ChannelSubscriptions.Add(typeof(T), subs);
        }

        private SubscriptionChannel GetChannel(string channel)
        {
            SubscriptionChannel subscriptionChannel;
            if (!this.SubscriptionChannels.TryGetValue(channel, out subscriptionChannel))
            {
                var subChannel = new SubscriptionChannel() { ChannelName = channel };
                subscriptionChannel = subChannel;
                this.SubscriptionChannels.Add(channel, subChannel);
            }

            return subscriptionChannel;
        }

        IEnumerable<ISubscription> GetSubscriptions<T>(string channel, T message)
        {
            SubscriptionChannel subscriptionChannel = null;

            lock (this.SubscriptionChannels)
            {
                if (this.SubscriptionChannels.TryGetValue(channel, out subscriptionChannel))
                {
                    return GetSubscriptionFromChannel<T>(subscriptionChannel);
                }
            }

            return default;
        }

        IEnumerable<ISubscription> GetSubscriptionFromChannel<T>(SubscriptionChannel subscriptionChannel)
        {
            lock (subscriptionChannel.ChannelSubscriptions)
            {
                if (subscriptionChannel.ChannelSubscriptions.TryGetValue(typeof(T), out IList<ISubscription> subscriptions))
                {
                    foreach (var sub in subscriptions)
                    {
                        yield return sub;
                    }
                }
            }
        }
    }
}

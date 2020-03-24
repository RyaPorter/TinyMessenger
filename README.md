# TinyMessenger

![MacOS](https://github.com/RyaPorter/TinyMessenger/workflows/MacOS/badge.svg?branch=master)
![Windows](https://github.com/RyaPorter/TinyMessenger/workflows/Windows/badge.svg?branch=master)
![Ubuntu](https://github.com/RyaPorter/TinyMessenger/workflows/Ubuntu/badge.svg?branch=master)

`TinyMessenger` serves as a lightweight messaging framework that is simple and easy to use, but offers a robust way of dispatching messages to intended subscribers, thereby allowing the decoupling of application logic.

`TinyMessenger` has the notion of typed message subscriptions and message channels. These two mechanisms serve as the primary method of dispatching the right message to the right subscriber. Message `Channels` allow the separation of two receivers who have subscribed via the same message type, but may not necessarily be interested in each and every message of that type. A situation that can arise when two object instances are dispatching the same message but who's subscribers should only be notified of one of the instances.

Using the messenger is simple. An instance of the `TinyMessenger` can be used to subscribe to all message on a channel of the specified type:
``` csharp
    messenger.Subscribe<StatusMessage>("ch1", (m) =>
    {
        state = m.State;
    });
```

A message can then be sent to this channel:
``` csharp
    messenger.Send("ch1", new StatusMessage() { State = "success" });
```
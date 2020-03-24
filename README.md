# TinyMessenger

![MacOS](https://github.com/RyaPorter/TinyMessenger/workflows/MacOS/badge.svg?branch=master)
![Windows](https://github.com/RyaPorter/TinyMessenger/workflows/Windows/badge.svg?branch=master)
![Ubuntu](https://github.com/RyaPorter/TinyMessenger/workflows/Ubuntu/badge.svg?branch=master)

`TinyMessenger` is yet another messaging framework, but with the sole goal to be as a lightweight, simple and as easy to use as possible. It offers a rather simple API, with only two overloaded methods.
Message channels and typed message subscriptions serve as the primary mechanisms of dispatching the right message to the right subscriber.

Using the messenger is simple. An instance of the `TinyMessenger` can be used to subscribe to all messages on a channel of the specified type:
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
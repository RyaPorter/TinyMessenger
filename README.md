![MacOS](https://github.com/RyaPorter/TinyMessenger/workflows/MacOS/badge.svg?branch=master)
![Windows](https://github.com/RyaPorter/TinyMessenger/workflows/Windows/badge.svg?branch=master)
![Ubuntu](https://github.com/RyaPorter/TinyMessenger/workflows/Ubuntu/badge.svg?branch=master)

# TinyMessenger

`TinyMessenger` is yet another messaging framework, but with the sole goal to be as a lightweight, simple and as easy to use as possible. It offers a rather simple API, with only two overloaded methods.

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



# TinyMessenger
`TinyMessenger` is yet another messaging framework, but with the sole goal to be as a lightweight, simple and as easy to use as possible. It offers a rather simple API, with only two overloaded methods.

## Getting Started

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

### Prerequisites
The main project, `TinyMessenger.csproj`, targets `.Net Standard` so version Core2.0 and Framework4.6.1 or greater are required.

```
.Net Core 2.0
.Net Framework 4.6.1
```

### Installing

Just clone the repo and run `dotnet build` from within the `src` directory.

## Running the tests

Run `dotnet test` from within the `src` directory.

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc


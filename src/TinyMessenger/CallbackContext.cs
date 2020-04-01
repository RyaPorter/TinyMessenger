
using System;
using System.Threading.Tasks;

public class CallbackContext<T>
{

    internal CallbackContext(Action<T, bool> callback) => this.Callback = callback;

    Action<T, bool> Callback { get; }

    public async Task RaiseCallbackAsync(T arg, bool success)
    {
        Func<Task> task = async () =>
        {
            await Task.Yield();
            this.Callback?.Invoke(arg, success);
        };
        await task();
    }

    public void RaiseCallback(T arg, bool success)
    {
        this.Callback?.Invoke(arg, success);
    }
}
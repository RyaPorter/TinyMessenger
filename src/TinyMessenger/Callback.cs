using System;
using System.Reflection;

public class Callback<T> : ICallback
{
    public Action<T, bool> action { get; set; }

    public MethodBase GetMethod()
    {
        return action.Method as MethodBase;
    }

    public void InvokeCallback(object arg, bool success)
    {
        if (arg is T) { action.Invoke((T)arg, success); }
    }
}
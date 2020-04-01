

using System.Reflection;

internal interface ICallback
{
    void InvokeCallback(object arg, bool success);

    MethodBase GetMethod();
}
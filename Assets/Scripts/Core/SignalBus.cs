using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalBus : Singleton<SignalBus>
{
    public delegate void SignalHandler<T>(T signal)
        where T : Signal;

    private Dictionary<Type, Delegate> signals = new Dictionary<Type, Delegate>();

    public void Register<T>(SignalHandler<T> handler)
        where T : Signal
    {
        Type signalType = typeof(T);
        if (!signals.ContainsKey(signalType))
        {
            signals[signalType] = handler;
        }
        else
        {
            var currentHandler = signals[signalType] as SignalHandler<T>;
            signals[signalType] = currentHandler + handler;
        }
    }

    public void Unregister<T>(SignalHandler<T> handler)
        where T : Signal
    {
        Type signalType = typeof(T);
        if (signals.ContainsKey(signalType))
        {
            var currentHandler = signals[signalType] as SignalHandler<T>;
            signals[signalType] = currentHandler - handler;
        }
    }

    public void FireSignal<T>(T signal)
        where T : Signal
    {
        Type signalType = typeof(T);
        if (signals.ContainsKey(signalType))
        {
            var currentHandler = signals[signalType] as SignalHandler<T>;
            currentHandler?.Invoke(signal);
        }
    }
}

public abstract class Signal
{ }
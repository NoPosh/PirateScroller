using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGame.Core.EventBus
{
    public class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> listeners = new();

        public static void Subscribe<TEvent>(Action<TEvent> callback)
            => SubscribeDelegate<TEvent>(callback);

        public static void Subscribe<TEvent>(Action callback)
            => SubscribeDelegate<TEvent>(callback);


        public static void Unsubscribe<TEvent>(Action<TEvent> callback)
           => UnsubscribeDelegate<TEvent>(callback);


        public static void Unsubscribe<TEvent>(Action callback)
           => UnsubscribeDelegate<TEvent>(callback);


        public static void Raise<T>(T context) => InvokeDelegate<T>(context);

        public static void Raise<T>() where T : new()
        {
            T context = new();
            InvokeDelegate(context);
        }

        private static void InvokeDelegate<TEvent>(TEvent context)
        {
            Type type = typeof(TEvent);
            if (!listeners.TryGetValue(type, out List<Delegate> callbacks))
            {
                return;
            }
            foreach (Delegate callback in callbacks)
            {
                if (callback is Action action)
                {
                    action();
                }
                else if (callback is Action<TEvent> actionWithArg)
                {
                    actionWithArg(context);
                }
            }
        }

        private static void SubscribeDelegate<TEvent>(Delegate callback)
        {
            Type type = typeof(TEvent);
            if (!listeners.TryGetValue(type, out List<Delegate> value))
            {
                value = new List<Delegate>();
                listeners[type] = value;
            }
            value.Add(callback);
        }

        private static void UnsubscribeDelegate<TEvent>(Delegate callback)
        {
            Type type = typeof(TEvent);
            if (listeners.TryGetValue(type, out List<Delegate> callbacks))
            {
                _ = callbacks.Remove(callback);
                if (callbacks.Count == 0)
                {
                    _ = listeners.Remove(type);
                }
            }
        }
    }
}

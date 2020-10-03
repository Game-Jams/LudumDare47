using System;
using System.Collections.Generic;
using UnityEngine;

namespace Observable
{
    public static class Observable
    {
        private static Dictionary<Type, List<Tuple<int, Action<IObserverParams>>>> _listeners = new Dictionary<Type, List<Tuple<int, Action<IObserverParams>>>>();

        public static void Subscribe<TObserver, TParameter>(this TObserver observer)
            where TObserver : IObserver<TObserver, TParameter>
            where TParameter : IObserverParams
        {
            var type = typeof(TObserver);

            if (!_listeners.ContainsKey(type))
            {
                _listeners[type] = new List<Tuple<int, Action<IObserverParams>>>();
            }

            Action<TParameter> callback = observer.Completed;

            Action<IObserverParams> action = p => callback((TParameter)p);

            _listeners[type].Add(new Tuple<int, Action<IObserverParams>>(callback.GetHashCode(), action));
        }

        public static void Subscribe<TObserver>(this TObserver observer)
            where TObserver : IObserver<TObserver, EmptyParams>
        {
            var type = typeof(TObserver);

            if (!_listeners.ContainsKey(type))
            {
                _listeners[type] = new List<Tuple<int, Action<IObserverParams>>>();
            }

            Action<EmptyParams> callback = observer.Completed;

            Action<IObserverParams> action = p => callback((EmptyParams)p);

            _listeners[type].Add(new Tuple<int, Action<IObserverParams>>(callback.GetHashCode(), action));
        }

        public static void Unsubscribe<TObserver, TParameter>(this TObserver observer)
            where TObserver : IObserver<TObserver, TParameter>
            where TParameter : IObserverParams
        {
            var type = typeof(TObserver);

            if (!_listeners.ContainsKey(type))
            {
                Debug.LogWarning($"No subscribes for type {type}");
                return;
            }

            Action<TParameter> method = observer.Completed;

            var hash = method.GetHashCode();

            var callbacks = _listeners[type];

            for (var i = 0; i < callbacks.Count; i++)
            {
                Tuple<int, Action<IObserverParams>> callback = callbacks[i];

                if (callback.Item1 == hash)
                {
                    callbacks.RemoveAt(i);
                    break;
                }
            }
        }

        public static void Unsubscribe<TObserver>(this TObserver observer)
            where TObserver : IObserver<TObserver, EmptyParams>
        {
            var type = typeof(TObserver);

            if (!_listeners.ContainsKey(type))
            {
                Debug.LogWarning($"No subscribes for type {type}");
                return;
            }

            Action<EmptyParams> method = observer.Completed;

            var hash = method.GetHashCode();

            var callbacks = _listeners[type];

            for (var i = 0; i < callbacks.Count; i++)
            {
                Tuple<int, Action<IObserverParams>> callback = callbacks[i];

                if (callback.Item1 == hash)
                {
                    callbacks.RemoveAt(i);
                    break;
                }
            }
        }

        public static void NotifyListeners<TObserver, TParams>(this IObserverNotify<TObserver, TParams> notificationSender, TParams parameters)
            where TObserver : IObserver<TObserver, TParams>
            where TParams : IObserverParams
        {
            var type = typeof(TObserver);

            if (!_listeners.ContainsKey(type))
            {
                Debug.LogWarning($"No listeners for type {type}");
                return;
            }

            var callbacks = new List<Tuple<int, Action<IObserverParams>>>(_listeners[type]);

            foreach (var callback in callbacks)
            {
                callback.Item2(parameters);
            }
        }

        public static void NotifyListeners<TObserver>(this IObserverNotify<TObserver, EmptyParams> notificationSender)
            where TObserver : IObserver<TObserver, EmptyParams>
        {
            var type = typeof(TObserver);

            if (!_listeners.ContainsKey(type))
            {
                Debug.LogWarning($"No listeners for type {type}");
                return;
            }

            var callbacks = new List<Tuple<int, Action<IObserverParams>>>(_listeners[type]);

            foreach (var callback in callbacks)
            {
                callback.Item2(EmptyParams.Empty);
            }
        }

        public static void NotifyListeners<TObserver>(this IObserverNotifyEmpty<TObserver> notificationSender)
            where TObserver : IObserver<TObserver, EmptyParams>
        {
            var type = typeof(TObserver);

            if (!_listeners.ContainsKey(type))
            {
                Debug.LogWarning($"No listeners for type {type}");
                return;
            }

            var callbacks = new List<Tuple<int, Action<IObserverParams>>>(_listeners[type]);

            foreach (var callback in callbacks)
            {
                callback.Item2(EmptyParams.Empty);
            }
        }
    }
}
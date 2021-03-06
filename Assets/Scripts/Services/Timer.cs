﻿using System;
using System.Collections.Generic;
using Observable;
using TMPro;
using UnityEngine;

#pragma warning disable CS0649
namespace Scripts.Services
{
    internal sealed class Timer : MonoBehaviour, ISessionStartedListener
    {
        private class ActionWithTime : IComparable<ActionWithTime>
        {
            public float Time { get; }
            public Action Action { get; }

            public ActionWithTime(float time, Action action)
            {
                Time = time;
                Action = action;
            }

            public int CompareTo(ActionWithTime other) => Time.CompareTo(other.Time);
        }

        public static Timer Instance { get; private set; }

        [SerializeField] private TMP_Text _textElement;

        private List<ActionWithTime> _actionsWithTime = new List<ActionWithTime>();
        private Action _update;

        private float _currentTime;

        private void Awake()
        {
            Instance = this;

            this.Subscribe<ISessionStartedListener>();
        }

        private void Update()
        {
            _update?.Invoke();
        }

        private void OnDestroy()
        {
            this.Unsubscribe<ISessionStartedListener>();
        }

        private void UpdateIteration()
        {
            _currentTime += Time.deltaTime;
            _textElement.text = TimeSpan.FromSeconds(_currentTime).ToString("mm':'ss");
            InvokePlannedActions();
        }

        private void InvokePlannedActions()
        {
            for (int i = 0; i < _actionsWithTime.Count; i++)
            {
                if (_actionsWithTime[i].Time <= _currentTime)
                {
                    _actionsWithTime[i].Action?.Invoke();
                    _actionsWithTime.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
        }

        public void RegisterActionAbsoluteTime(float time, Action action)
        {
            if (action != null)
            {
                _actionsWithTime.Add(new ActionWithTime(time, action));
                _actionsWithTime.Sort();
            }
        }

        public void RegisterActionRelativeTime(float time, Action action)
        {
            if (action != null)
            {
                _actionsWithTime.Add(new ActionWithTime(_currentTime + time, action));
                _actionsWithTime.Sort();
            }
        }

        void IObserver<ISessionStartedListener, EmptyParams>.Completed(EmptyParams parameters)
        {
            _update += UpdateIteration;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Services
{
    public class Timer : MonoBehaviour
    {
        public static Timer Instance { get; private set; }

        private List<ActionWithTime> _actionsWithTime = new List<ActionWithTime>();
        [SerializeField]
        private float _currentTime = 0.0f;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            CheckTime();
        }

        private void CheckTime()
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
    }
}

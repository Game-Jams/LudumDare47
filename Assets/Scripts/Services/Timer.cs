using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.UI;

namespace Scripts.Services
{
    public class Timer : MonoBehaviour
    {
        public static Timer Instance { get; private set; }

        private SortedList<float, Action> _actionsWithTime = new SortedList<float, Action>();
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
            foreach (float actionTime in _actionsWithTime.Keys)
            {
                if (actionTime <= _currentTime)
                {
                    _actionsWithTime[actionTime]?.Invoke();
                    _actionsWithTime.Remove(actionTime);
                }
                return;
            }
        }

        public void RegisterActionAbsoluteTime(float time, Action action)
        {
            if (action != null)
            {
                _actionsWithTime.Add(time, action);
            }
        }

        public void RegisterActionRelativeTime(float time, Action action)
        {
            if (action != null)
            {
                _actionsWithTime.Add(_currentTime + time, action);
            }
        }
    }
}

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

        private SortedList<float, Action> _triggersWithTime = new SortedList<float, Action>();
        private float _timer = 0.0f;

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            CheckTime();
        }

        private void CheckTime()
        {
            int index = _triggersWithTime.IndexOfKey(_timer);
            if (index > -1)
            {
                _triggersWithTime[index]?.Invoke();
                _triggersWithTime.RemoveAt(index);
            }
        }

        public void RegisterTrigger(float time, Action trigger)
        {
            if (trigger != null && !_triggersWithTime.ContainsKey(time))
            {
                _triggersWithTime.Add(_timer + time, trigger);
            }
        }
    }
}

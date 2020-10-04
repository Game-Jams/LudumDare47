using System;
using UnityEngine;

#pragma warning disable CS0649
namespace Scripts.Basis
{
    internal sealed class UpDownAnimation : MonoBehaviour
    {
        [SerializeField] private float _range;
        [SerializeField] private float _amplitude;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private float _currentTime;

        private void Start()
        {
            _startPosition = transform.position;
            _targetPosition = _startPosition + Vector3.up * _range;
        }

        private void Update()
        {
            if (transform.position == _targetPosition)
            {
                ReverseTarget();
            }
            MoveToTarget();
        }

        private void ReverseTarget()
        {
            var temp = _targetPosition;
            _targetPosition = _startPosition;
            _startPosition = temp;
            _currentTime = 0;
        }

        private void MoveToTarget()
        {
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, EasingInOutSine(_currentTime / _amplitude));
            _currentTime += Time.deltaTime;
        }

        private float EasingInOutSine(float x) => -((float)Math.Cos(Math.PI * x) - 1) / 2;

        private float EasingInOutQuart(float x) => x < 0.5 ? 8 * x * x * x * x : 1 - (float)Math.Pow(-2 * x + 2, 4) / 2;

        private float EasingInOutQuad(float x) => x < 0.5 ? 2 * x * x : 1 - (float)Math.Pow(-2 * x + 2, 2) / 2;

        private float EasingInOutCubic(float x) => x < 0.5 ? 4 * x * x * x : 1 - (float)Math.Pow(-2 * x + 2, 3) / 2;
    }
}

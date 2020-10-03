using System;
using UnityEngine;

#pragma warning disable CS0649
namespace Player.Control
{
    [RequireComponent(typeof(CharacterController))]
    internal sealed class Controller : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private float _rotationSpeed;

        private CharacterController _character;
        private Transform _transform;

        private void Start()
        {
            _character = GetComponent<CharacterController>();
            _transform = transform;
        }

        private void Update()
        {
            if (Input.anyKey)
            {
                Move();
            }
        }

        private void Move()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(-horizontal, 0f, -vertical).normalized;

            if (direction != default)
            {
                RotateToMoveDirection(direction);

                _character.Move(direction * _speed - Vector3.down * _fallSpeed);
            }
        }

        private void RotateToMoveDirection(Vector3 desireDirection)
        {
            Quaternion currentRotation = _transform.rotation;

            float angel = GetAngel(desireDirection) * Mathf.Rad2Deg;
            Quaternion desireRotation = Quaternion.Euler(Vector3.up * angel);

            _transform.rotation = Quaternion.Lerp(currentRotation, desireRotation, _rotationSpeed);
        }

        private float GetAngel(Vector3 direction)
        {
            if (direction.z == 0)
            {
                return direction.x > 0f ? -(float)Math.PI / 2f : (float)Math.PI / 2f;
            }

            if (direction.x == 0)
            {
                return direction.z > 0 ? (float)Math.PI : 0f;
            }

            return Mathf.Atan(direction.x / direction.z);
        }
    }
}
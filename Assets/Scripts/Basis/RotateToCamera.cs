using UnityEngine;

namespace Basis
{
    internal sealed class RotateToCamera : MonoBehaviour
    {
        private Transform _cameraTransform;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;

            transform.rotation = _cameraTransform.rotation;
        }
    }
}
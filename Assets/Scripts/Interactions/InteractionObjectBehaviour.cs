﻿using Player;
using UnityEngine;

#pragma warning disable CS0649
namespace Interactions
{
    [RequireComponent(typeof(Collider))]
    internal abstract class InteractionObjectBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _activityIndicator;

        protected PlayerInventory _playerInventory;

        protected bool _inInteractiveZone;

        private bool _isDisabled;

        public bool IsDisabled
        {
            get => _isDisabled;
            set
            {
                _isDisabled = value;

                if (_isDisabled)
                {
                    SetActive(isActive: false);
                }
            }
        }

        private void Awake()
        {
            SetActive(false);
        }

        private void Update()
        {
            if (!_isDisabled && _inInteractiveZone && HasCanInteract(_playerInventory) && Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out PlayerInventory playerInventory))
            {
                _inInteractiveZone = true;
                _playerInventory = playerInventory;

                ChangeActiveState();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _inInteractiveZone = false;

            SetActive(false);
        }

        protected abstract bool NeedActivation(PlayerInventory playerInventory);

        protected abstract bool HasCanInteract(PlayerInventory playerInventory);

        protected abstract void Interact();

        protected void ChangeActiveState()
        {
            SetActive(NeedActivation(_playerInventory) && !_isDisabled);
        }

        protected virtual void SetActive(bool isActive)
        {
            _activityIndicator.SetActive(isActive && !_isDisabled);
        }
    }
}
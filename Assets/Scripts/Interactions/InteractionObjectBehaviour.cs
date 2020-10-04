using Player;
using UnityEngine;

#pragma warning disable CS0649
namespace Interactions
{
    [RequireComponent(typeof(Collider))]
    internal abstract class InteractionObjectBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject _activityIndicator;

        protected PlayerInventory _playerInventory;

        protected bool _isInteractive;

        private void Awake()
        {
            SetActive(false);
        }

        private void Update()
        {
            if (_isInteractive && Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInventory playerInventory))
            {
                _playerInventory = playerInventory;

                ChangeActiveState();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            SetActive(false);
        }

        protected abstract bool NeedActivation(PlayerInventory playerInventory);

        protected abstract void Interact();

        protected void ChangeActiveState()
        {
            SetActive(NeedActivation(_playerInventory));
        }

        protected virtual void SetActive(bool isActive)
        {
            _isInteractive = isActive;
            _activityIndicator.SetActive(isActive);
        }
    }
}
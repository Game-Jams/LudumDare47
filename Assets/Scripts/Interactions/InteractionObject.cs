using Player;
using UnityEngine;

#pragma warning disable CS0649
namespace Interactions
{
    [RequireComponent(typeof(SphereCollider))]
    internal sealed class InteractionObject : MonoBehaviour
    {
        [SerializeField] private GameObject _activityIndicator;
        [SerializeField] private InteractionData _interactionData;

        private bool _isActive;

        private void Awake()
        {
            SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInventory playerInventory))
            {
                TryToActivate(playerInventory);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            SetActive(false);
        }

        private void TryToActivate(PlayerInventory playerInventory)
        {
            if (_interactionData.ItemForActivate == playerInventory.Item)
            {
                SetActive(true);
            }
        }

        private void SetActive(bool isActive)
        {
            _isActive = isActive;
            _activityIndicator.SetActive(isActive);
        }
    }
}
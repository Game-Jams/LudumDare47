using Interactions;
using Player;
using TMPro;
using UnityEngine;

#pragma warning disable CS0649
namespace NPC
{
    internal sealed class Npc : InteractionObjectBehaviour
    {
        [SerializeField] private TextMeshPro _dialogText;
        [SerializeField] private GameObject _dialogObject;

        [Space(10f)]
        [SerializeField] private InteractionData[] _interactionData;

        private int _currentStateIndex;

        private InteractionData InteractionData => _interactionData.Length == _currentStateIndex ? default : _interactionData[_currentStateIndex];

        protected override bool NeedActivation(PlayerInventory playerInventory)
        {
            bool alwaysActive = InteractionData.AlwaysIsInteract;
            bool playerHasItem = InteractionData.ItemForInteract == playerInventory.Item;

            return alwaysActive || playerHasItem;
        }

        protected override bool HasCanInteract(PlayerInventory playerInventory)
        {
            return InteractionData.AlwaysIsInteract || InteractionData.ItemForInteract == playerInventory.Item;
        }

        protected override void Interact()
        {
            Debug.Log($"InteractionType: {InteractionData.InteractionType}");

            if (!InteractionData.AlwaysIsInteract)
            {
                _playerInventory.Item = default;
                Debug.Log($"I took the item: {InteractionData.ItemForInteract}");
            }

            if (InteractionData.HasItemForReceive)
            {
                _playerInventory.Item = InteractionData.ItemForReceive;
                Debug.Log($"I gave the item: {InteractionData.ItemForReceive}");
            }

            UpdateDialog();

            _currentStateIndex++;

            ChangeActiveState();
        }

        protected override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            UpdateDialog();
        }

        private void UpdateDialog()
        {
            bool dialogIsActive = InteractionData.HasMessage && _inInteractiveZone;

            _dialogObject.SetActive(dialogIsActive);

            if (dialogIsActive)
            {
                _dialogText.text = InteractionData.Message;
            }
        }
    }
}
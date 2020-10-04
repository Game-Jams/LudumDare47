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
            bool alwaysActive = InteractionData.AlwaysIsActive;
            bool playerHasItem = InteractionData.ItemForActivate == playerInventory.Item;

            return alwaysActive || playerHasItem;
        }

        protected override void Interact()
        {
            Debug.Log($"InteractionType: {InteractionData.InteractionType}");

            if (InteractionData.ItemForActivate != default)
            {
                _playerInventory.Item = default;
                Debug.Log($"I took the item: {InteractionData.ItemForActivate}");
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
            bool hasDialog = InteractionData.InteractionType == InteractionType.Dialog;
            bool dialogIsActive = hasDialog && _isInteractive;

            _dialogObject.SetActive(dialogIsActive);

            if (dialogIsActive)
            {
                _dialogText.text = InteractionData.Message;
            }
        }
    }
}
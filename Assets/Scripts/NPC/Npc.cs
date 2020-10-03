using Interactions;
using Player;
using UnityEngine;

#pragma warning disable CS0649
namespace NPC
{
    internal sealed class Npc : InteractionObjectBehaviour
    {
        [SerializeField] private InteractionData[] _interactionData;

        private int _currentStateIndex;

        private InteractionData InteractionData => _interactionData[_currentStateIndex];

        protected override bool NeedActivation(PlayerInventory playerInventory)
        {
            if (_interactionData.Length == _currentStateIndex)
            {
                return false;
            }

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

            if (InteractionData.InteractionType == InteractionType.Dialog)
            {
                Debug.Log($"{InteractionData.Message}");
            }

            _currentStateIndex++;

            ChangeActiveState();
        }
    }
}
using Player;
using UnityEngine;

#pragma warning disable CS0649
namespace Interactions
{
    internal sealed class Item : InteractionObjectBehaviour
    {
        [SerializeField] private InteractionData _interactionData;

        protected override bool NeedActivation(PlayerInventory playerInventory)
        {
            return _interactionData.ItemForActivate == playerInventory.Item;
        }

        protected override void Interact()
        {
            _playerInventory.Item = _interactionData.ItemForReceive;

            Destroy(gameObject);
        }
    }
}
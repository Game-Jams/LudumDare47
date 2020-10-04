using Player;
using UnityEngine;

#pragma warning disable CS0649
namespace Interactions
{
    internal sealed class SimpleInteraction : InteractionObjectBehaviour
    {
        [SerializeField] private InteractionData _interaction;

        protected override bool NeedActivation(PlayerInventory playerInventory)
        {
            return _interaction.AlwaysIsInteract || _interaction.ItemForInteract == playerInventory.Item;
        }

        protected override bool HasCanInteract(PlayerInventory playerInventory)
        {
            return _interaction.AlwaysIsInteract || _interaction.ItemForInteract == playerInventory.Item;
        }

        protected override void Interact()
        {
            if (_interaction.HasItemForReceive)
            {
                _playerInventory.Item = _interaction.ItemForReceive;
            }
            
            Destroy(gameObject);
        }
    }
}
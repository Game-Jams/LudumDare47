﻿using Player;
using UnityEngine;

#pragma warning disable CS0649
namespace Interactions
{
    internal sealed class SimpleInteraction : InteractionObjectBehaviour
    {
        [SerializeField] private InteractionData _interactionData;

        protected override bool NeedActivation(PlayerInventory playerInventory)
        {
            return _interactionData.AlwaysIsInteract || _interactionData.ItemForInteract == playerInventory.Item;
        }

        protected override bool HasCanInteract(PlayerInventory playerInventory)
        {
            return _interactionData.ItemForInteract == playerInventory.Item;
        }

        protected override void Interact()
        {
            _playerInventory.Item = _interactionData.ItemForReceive;

            Destroy(gameObject);
        }
    }
}
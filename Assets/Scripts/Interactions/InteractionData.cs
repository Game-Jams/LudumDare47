using System;
using Items;
using UnityEngine;

namespace Interactions
{
    [Serializable]
    public struct InteractionData
    {
        [SerializeField] private InteractionType _interactionType;
        [SerializeField] private ItemType _itemForActivate;
        [SerializeField] private ItemType _itemForReceive;

        public InteractionType InteractionType => _interactionType;
        public ItemType ItemForActivate => _itemForActivate;
        public ItemType ItemForReceive => _itemForReceive;
    }
}
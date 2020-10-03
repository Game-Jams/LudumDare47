using System;
using Items;
using Sirenix.OdinInspector;
using UnityEngine;

#pragma warning disable CS0649
namespace Interactions
{
    [Serializable]
    public struct InteractionData
    {
        [SerializeField] private InteractionType _interactionType;

        [Space(5f)]
        [SerializeField] private bool _alwaysIsActive;

        [ShowIf("_alwaysIsActive")]
        [SerializeField] private bool _withoutItemForReceive;

        [Space(5f)]
        [HideIf("_alwaysIsActive")]
        [SerializeField] private ItemType _itemForActivate;

        [Space(5f)]
        [HideIf("_withoutItemForReceive")]
        [SerializeField] private ItemType _itemForReceive;

        [Space(10f)] 
        [ShowIf("_interactionType", InteractionType.Dialog)] 
        [TextArea(1, 4)]
        [SerializeField]
        private string _message;

        public InteractionType InteractionType => _interactionType;
        public ItemType ItemForActivate => _itemForActivate;
        public ItemType ItemForReceive => _itemForReceive;

        public bool AlwaysIsActive => _alwaysIsActive;
        public bool HasItemForReceive => !_withoutItemForReceive;

        public string Message => _message;
    }
}
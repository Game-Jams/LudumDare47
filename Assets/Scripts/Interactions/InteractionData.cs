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
        [SerializeField] private bool _hasMessage;

        [Space(5f)]
        [HideIf("_alwaysIsInteract")]
        [SerializeField] private ItemType _itemForInteract;

        [ShowIf("_hasItemForReceive")]
        [SerializeField] private ItemType _itemForReceive;

        [Space(5f)]
        [SerializeField] private bool _alwaysIsInteract;
        [SerializeField] private bool _hasItemForReceive;

        [Space(10f)] 
        [ShowIf("_hasMessage")] 
        [TextArea(1, 4)]
        [SerializeField]
        private string _message;

        public InteractionType InteractionType => _interactionType;
        public ItemType ItemForInteract => _itemForInteract;
        public ItemType ItemForReceive => _itemForReceive;

        public bool AlwaysIsInteract => _alwaysIsInteract;
        public bool HasItemForReceive => _hasItemForReceive;
        public bool HasMessage => _hasMessage;

        public string Message => _message;
    }
}
﻿using System;
using Interactions;
using Player;
using TMPro;
using UnityEngine;

#pragma warning disable CS0649
namespace NPC
{
    internal sealed class NpcInteraction : InteractionObjectBehaviour
    {
        public event Action InteractionEnded;

        [Space(5f)]
        [SerializeField] private NpcInteractionData _npcInteractionData;

        [Space(5f)]
        [SerializeField] private TextMeshPro _dialogText;
        [SerializeField] private GameObject _dialogObject;

        private int _currentStateIndex;

        private InteractionData[] Interactions => _npcInteractionData.Interactions;

        private InteractionData Interaction =>
            Interactions.Length == _currentStateIndex ? default : Interactions[_currentStateIndex];

        protected override bool NeedActivation(PlayerInventory playerInventory)
        {
            bool alwaysActive = Interaction.AlwaysIsInteract;
            bool playerHasItem = Interaction.ItemForInteract == playerInventory.Item;

            return alwaysActive || playerHasItem;
        }

        protected override bool HasCanInteract(PlayerInventory playerInventory)
        {
            return Interaction.AlwaysIsInteract || Interaction.ItemForInteract == playerInventory.Item;
        }

        protected override void Interact()
        {
            Debug.Log($"InteractionType: {Interaction.InteractionType}");

            UpdatePlayerInventory();
            UpdateDialog();
            UpdateStateIndex();

            ChangeActiveState();
        }

        protected override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            UpdateDialog();
        }

        private void UpdatePlayerInventory()
        {
            if (!Interaction.AlwaysIsInteract)
            {
                _playerInventory.Item = default;
                Debug.Log($"I took the item: {Interaction.ItemForInteract}");
            }

            if (Interaction.HasItemForReceive)
            {
                _playerInventory.Item = Interaction.ItemForReceive;
                Debug.Log($"I gave the item: {Interaction.ItemForReceive}");
            }
        }

        private void UpdateDialog()
        {
            bool dialogIsActive = Interaction.HasMessage && _inInteractiveZone;

            _dialogObject.SetActive(dialogIsActive);

            if (dialogIsActive)
            {
                _dialogText.text = Interaction.Message;
            }
        }

        private void UpdateStateIndex()
        {
            _currentStateIndex++;

            if (_currentStateIndex == Interactions.Length)
            {
                InteractionEnded?.Invoke();
            }
        }
    }
}
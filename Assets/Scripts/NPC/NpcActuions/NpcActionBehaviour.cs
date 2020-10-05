using System;
using UnityEngine;

namespace NPC
{
    internal abstract class NpcActionBehaviour
    {
        public event Action ActionEnded;

        protected GameObject _owner;

        public NpcActionBehaviour(GameObject owner)
        {
            _owner = owner;
        }

        public abstract void StartAction();

        public abstract void UpdateState();

        protected void EndAction()
        {
            ActionEnded?.Invoke();
        }
    }
}
using GameActions;
using Observable;
using UnityEngine;
using System;

namespace NPC
{
    internal sealed class ActionSequence : MonoBehaviour, IGameActionInvokedListener
    {
        public event Action Initialized;
        public event Action Disabled;

        [Header("Triggers")]
        [SerializeField] private GameAction _forInitialization;
        [SerializeField] private GameAction _forWrongAction;
        [SerializeField] private GameAction _forRightAction;

        private bool _isInitialized;

        private bool _wrongActionPlanned;
        private bool _rightActionPlanned;

        private void Start()
        {
            if (_forInitialization == 0)
            {
                _isInitialized = true;
            }

            this.Subscribe<IGameActionInvokedListener, GameActionParams>();
        }

        private void OnDestroy()
        {
            this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
        }

        public void InvokeInitialAction()
        {
            _isInitialized = true;
        }

        public void InvokeWrongAction()
        {

        }

        public void InvokeRightAction()
        {

        }

        private void InvokePlannedActions()
        {
            if (_wrongActionPlanned)
            {
                InvokeWrongAction();
                Disable();
                return;
            }

            if (_rightActionPlanned)
            {
                InvokeRightAction();
                Disable();
            }
        }

        private void Disable()
        {
            this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
        }

        void IObserver<IGameActionInvokedListener, GameActionParams>.Completed(GameActionParams parameters)
        {
            _wrongActionPlanned = parameters.Action == _forWrongAction;
            _rightActionPlanned = parameters.Action == _forRightAction;

            if (!_isInitialized && parameters.Action == _forInitialization)
            { 
                InvokeInitialAction();
            }

            InvokePlannedActions();
        }
    }
}
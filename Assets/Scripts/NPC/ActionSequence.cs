using GameActions;
using Observable;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace NPC
{
    internal sealed class ActionSequence : MonoBehaviour, IGameActionInvokedListener
    {
        [Serializable]
        private struct GameActionData
        {
            [SerializeField] private GameActionType _actionType;
            [SerializeField] private GameAction _onEndedEvent;

            [SerializeField, ShowIf("_actionType", GameActionType.Move)]
            private Transform _target;

            public GameActionType ActionType => _actionType;
            public GameAction OnEndedEvent => _onEndedEvent;
            public Transform Target => _target;
        }

        public event Action Initialized;
        public event Action Disabled;

        [Header("Triggers")]
        [SerializeField] private GameAction _forInitialization;
        [SerializeField] private GameAction _forWrongAction;
        [SerializeField] private GameAction _forRightAction;

        [Header("Actions")] 
        [SerializeField] private GameActionData _initializeActionData;
        [SerializeField] private GameActionData _wrongActionData;
        [SerializeField] private GameActionData _rightActionData;

        private NpcActionBehaviour _initializeAction;
        private NpcActionBehaviour _wrongAction;
        private NpcActionBehaviour _rightAction;

        private bool _isInitialized;
        private bool _isDisabled;

        private bool _wrongActionPlanned;
        private bool _rightActionPlanned;

        private void Start()
        {
            if (_forInitialization == 0)
            {
                _isInitialized = true;
                Initialized?.Invoke();
            }

            this.Subscribe<IGameActionInvokedListener, GameActionParams>();
        }

        private void Update()
        {
            _initializeAction?.UpdateState();
            _wrongAction?.UpdateState();
            _rightAction?.UpdateState();
        }

        private void OnDestroy()
        {
            this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
        }

        public void InvokeInitialAction()
        {
            _isInitialized = true;

            _initializeAction = new NpcMoveAction(gameObject, _initializeActionData.Target);
            _initializeAction.StartAction();

            _initializeAction.ActionEnded += () =>
            {
                Initialized?.Invoke();
            };
        }

        public void InvokeWrongAction()
        {
            if (!_wrongActionPlanned)
            {
                _wrongActionPlanned = true;
                InvokePlannedActions();
            }

            _wrongAction = new NpcMoveAction(gameObject, _wrongActionData.Target);
            _wrongAction.StartAction();
        }

        public void InvokeRightAction()
        {
            if (!_rightActionPlanned)
            {
                _rightActionPlanned = true;
                InvokePlannedActions();
            }

            _rightAction = new NpcMoveAction(gameObject, _rightActionData.Target);
            _rightAction.StartAction();
        }

        private void InvokePlannedActions()
        {
            if (_isDisabled)
            {
                return;
            }

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
            _isDisabled = true;
            Disabled?.Invoke();
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
﻿using Observable;
using UnityEngine;
using UnityEngine.Playables;

namespace GameActions
{
    [RequireComponent(typeof(PlayableDirector))]
    public class HangController : MonoBehaviour, IGameActionInvokedListener,
        IObserverNotify<IGameActionInvokedListener, GameActionParams>
    {
        private PlayableDirector _director;

        private void Awake()
        {
            _director = GetComponent<PlayableDirector>();

            this.Subscribe<IGameActionInvokedListener, GameActionParams>();
        }

        private void OnDestroy()
        {
            this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
        }

        void IObserver<IGameActionInvokedListener, GameActionParams>.Completed(GameActionParams parameters)
        {
            switch (parameters.Action)
            {
                case GameAction.SheriffSavesGranny:
                    this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
                    break;
                case GameAction.GrannyHangStart:
                    _director.Play();

                    this.Unsubscribe<IGameActionInvokedListener, GameActionParams>();
                    break;
            }
        }

        public void Lose()
        {
            this.NotifyListeners(new GameActionParams(GameAction.GrannyHanged));
        }
    }
}

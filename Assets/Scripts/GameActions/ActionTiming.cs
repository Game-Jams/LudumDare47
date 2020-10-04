using System;
using UnityEngine;

namespace GameActions
{
    [Serializable]
    internal struct ActionTiming
    {
        [SerializeField] private GameAction _gameAction;
        [SerializeField] private float _timeOfActivation;

        public GameAction GameAction => _gameAction;
        public float TimeOfActivation => _timeOfActivation;
    }
}
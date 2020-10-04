using GameActions;

namespace Observable
{
    public struct GameActionParams : IObserverParams
    {
        public GameAction Action { get; }

        public GameActionParams(GameAction action) => Action = action;
    }
}
using Items;

namespace Observable
{
    public readonly struct ItemReceivesParams : IObserverParams
    {
        public ItemType Item { get; }

        public ItemReceivesParams(ItemType item) => Item = item;
    }
}
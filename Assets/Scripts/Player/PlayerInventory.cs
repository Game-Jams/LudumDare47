using Items;
using Observable;
using UnityEngine;

namespace Player
{
    public class PlayerInventory : MonoBehaviour, IObserverNotify<IItemReceivedListener, ItemReceivesParams>
    {
        private ItemType _item;

        public ItemType Item
        {
            get => _item;

            set
            {
                this.NotifyListeners(new ItemReceivesParams(value));

                _item = value;
            }
        }
    }
}
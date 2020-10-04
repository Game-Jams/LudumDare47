using Observable;
using TMPro;
using UnityEngine;

#pragma warning disable CS0649
namespace UI
{
    public class InventoryUI : MonoBehaviour, IItemReceivedListener
    {
        [SerializeField] private TextMeshProUGUI _itemName;

        private void Start()
        {
            this.Subscribe<IItemReceivedListener, ItemReceivesParams>();
        }

        private void OnDestroy()
        {
            this.Unsubscribe<IItemReceivedListener, ItemReceivesParams>();
        }

        void IObserver<IItemReceivedListener, ItemReceivesParams>.Completed(ItemReceivesParams parameters)
        {
            _itemName.text = parameters.Item.ToString();
        }
    }
}
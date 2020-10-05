using System;
using System.Linq;
using Items;
using Observable;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable CS0649
namespace UI
{
    public class InventoryUI : MonoBehaviour, IItemReceivedListener
    {
        [Serializable]
        private struct ItemImage
        {
            public ItemType ItemType;
            public Sprite Sprite;
        }

        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private Image _image;

        [SerializeField] private ItemImage[] _itemImages;

        private void Start()
        {
            SetItem(ItemType.None);

            this.Subscribe<IItemReceivedListener, ItemReceivesParams>();
        }

        private void OnDestroy()
        {
            this.Unsubscribe<IItemReceivedListener, ItemReceivesParams>();
        }

        void IObserver<IItemReceivedListener, ItemReceivesParams>.Completed(ItemReceivesParams parameters)
        {
            SetItem(parameters.Item);
        }

        private void SetItem(ItemType itemType)
        {
            _itemName.text = itemType.ToString();

            _image.sprite = _itemImages.First(item => item.ItemType == itemType).Sprite;
        }
    }
}
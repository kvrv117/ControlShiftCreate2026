using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CSC2026
{
    public class ItemCell : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _image;

        private Item _item;
        private Inventory _inventory;

        private ItemPlayer _player;

        public void Initialize(Item t, Inventory inventory)
        {
            _item = t;
            _inventory = inventory;

            _image.sprite = t.Sprite;
            _player = GetComponentInParent<ItemPlayer>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _inventory.Descript(_item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inventory.StopDescript();
        }

        public void HideItem()
        {
            _image.gameObject.SetActive(false);
        }

        public void ShowItem()
        {
            _image.gameObject.SetActive(true);
        }

        public Item GetItem()
        {
            return _item;
        }

        public void Remove()
        {
            _inventory.RemoveCell(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(_player != null)
            {
                _inventory.StopDescript();
                _player.StartPlaying(_item, this);
            }
        }
    }
}

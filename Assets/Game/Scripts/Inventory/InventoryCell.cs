using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CSC2026
{
    public class InventoryCell : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _image;
        private UnitType _unit;

        private UnitPlayer _player;

        private void Start()
        {
            _player = GetComponentInParent<UnitPlayer>();
        }

        public void Initialize(UnitType unit)
        {
            _unit = unit;
            _image.sprite = Units.GetUnitSprite(_unit);
            ShowItem();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(_player != null)
            {
                _player.StartPlaying(_unit, this);
            }
        }

        public void HideItem()
        {
            _image.gameObject.SetActive(false);
        }

        public void ShowItem()
        {
            _image.gameObject.SetActive(true);
        }

        public UnitType GetUnit()
        {
            return _unit;
        }

        public void Remove()
        {
            GetComponentInParent<Inventory>().RemoveCell(this);
        }
    }
}

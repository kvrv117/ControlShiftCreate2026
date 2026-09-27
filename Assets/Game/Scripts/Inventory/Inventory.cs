using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CSC2026
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private RectTransform _unitsContent;
        [SerializeField] private UnitCell _unitCell;

        [SerializeField] private RectTransform _itemsContent;
        [SerializeField] private ItemCell _itemCell;

        [SerializeField] private List<UnitType> _units;
        [SerializeField] private List<Item> _items;

        [SerializeField] private ItemDescription _description;

        private List<UnitCell> _unitCells;
        private List<ItemCell> _itemCells;

        private void Start()
        {
            _unitCells = new List<UnitCell>();
            _itemCells = new List<ItemCell>();

            RebuildInventory();
        }

        private void RebuildInventory()
        {
            if (_unitCells.Count < _units.Count)
            {
                int diff = _units.Count - _unitCells.Count;
                for (int i = 0; i < diff; i++)
                {
                    _unitCells.Add(Instantiate(_unitCell, _unitsContent));
                }
            }

            for (int i = 0; i < _unitCells.Count; i++)
            {
                _unitCells[i].gameObject.SetActive(true);
                _unitCells[i].Initialize(_units[i]);
            }


            if (_itemCells.Count < _items.Count)
            {
                int diff = _items.Count - _itemCells.Count;
                for (int i = 0; i < diff; i++)
                {
                    _itemCells.Add(Instantiate(_itemCell, _itemsContent));
                }
            }

            for (int i = 0; i < _itemCells.Count; i++)
            {
                _itemCells[i].gameObject.SetActive(true);
                _itemCells[i].Initialize(_items[i], this);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(_unitsContent);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_itemsContent);
        }

        public void RemoveCell(UnitCell cell)
        {
            _units.Remove(cell.GetUnit());
            _unitCells.Remove(cell);
            Destroy(cell.gameObject);
        }

        public void RemoveCell(ItemCell cell)
        {
            _items.Remove(cell.GetItem());
            _itemCells.Remove(cell);
            Destroy(cell.gameObject);
        }

        public (UnitType, Vector3) PopUnit()
        {
            UnitCell chosen = _unitCells[Random.Range(0, _unitCells.Count)];
            UnitType chosenUnit = chosen.GetUnit();
            Vector3 pos = chosen.transform.position;

            _units.Remove(chosenUnit);
            _unitCells.Remove(chosen);
            Destroy(chosen.gameObject);

            return (chosenUnit, pos);
        }

        public void AddUnit(UnitType unit)
        {
            _units.Add(unit);
            RebuildInventory();
        }
        public (Item, Vector3) PopItem()
        {
            ItemCell chosen = _itemCells[Random.Range(0, _itemCells.Count)];
            Item chosenUnit = chosen.GetItem();
            Vector3 pos = chosen.transform.position;

            _items.Remove(chosenUnit);
            _itemCells.Remove(chosen);
            Destroy(chosen.gameObject);

            return (chosenUnit, pos);
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
            RebuildInventory();
        }

        public void Descript(Item i)
        {
            _description.Descript(i);
        }

        public void StopDescript()
        {
            _description.StopDescript();
        }
    }
}

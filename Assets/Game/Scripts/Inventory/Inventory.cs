using System.Collections.Generic;
using UnityEngine;

namespace CSC2026
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private InventoryCell _cell;
        [SerializeField] private List<UnitType> _units;

        private List<InventoryCell> _cells;

        private void Start()
        {
            _cells = new List<InventoryCell>();

            RebuildInventory();
        }

        private void RebuildInventory()
        {
            if(_cells.Count < _units.Count)
            {
                int diff = _units.Count - _cells.Count;
                for (int i = 0; i < diff; i++)
                {
                    _cells.Add(Instantiate(_cell, _content.transform));
                }
            }

            for(int i = 0; i < _cells.Count; i++)
            {
                if(i < _units.Count)
                {
                    _cells[i].gameObject.SetActive(true);
                    _cells[i].Initialize(_units[i]);
                }
                else
                {
                    _cells[i].gameObject.SetActive(false);
                }
            }
        }

        public void RemoveCell(InventoryCell cell)
        {
            _units.Remove(cell.GetUnit());
            _cells.Remove(cell);
            Destroy(cell.gameObject);
        }

        public UnitType PopItem()
        {
            InventoryCell chosen = _cells[Random.Range(0, _cells.Count)];
            UnitType chosenUnit = chosen.GetUnit();

            _units.Remove(chosenUnit);
            _cells.Remove(chosen);
            Destroy(chosen.gameObject);

            return chosenUnit;
        }
    }
}

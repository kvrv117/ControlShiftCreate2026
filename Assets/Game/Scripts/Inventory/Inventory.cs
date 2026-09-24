using System.Collections.Generic;
using UnityEngine;

namespace CSC2026
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private InventoryCell _cell;
        [SerializeField] private List<UnitType> units;

        private List<InventoryCell> _cells;

        private void Start()
        {
            _cells = new List<InventoryCell>();

            RebuildInventory();
        }

        private void RebuildInventory()
        {
            if(_cells.Count < units.Count)
            {
                int diff = units.Count - _cells.Count;
                for (int i = 0; i < diff; i++)
                {
                    _cells.Add(Instantiate(_cell, _content.transform));
                }
            }

            for(int i = 0; i < _cells.Count; i++)
            {
                if(i < units.Count)
                {
                    _cells[i].gameObject.SetActive(true);
                    _cells[i].Initialize(units[i]);
                }
                else
                {
                    _cells[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

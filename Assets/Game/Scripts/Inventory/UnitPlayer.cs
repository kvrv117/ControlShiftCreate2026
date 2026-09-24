using UnityEngine;

namespace CSC2026
{
    public class UnitPlayer : MonoBehaviour
    {
        [SerializeField] private PlayingUnit _playingUnit;

        private InventoryCell _playingCell;

        public void StartPlaying(UnitType unit, InventoryCell cell)
        {
            _playingCell = cell;

            _playingUnit.Initialize(unit);
            _playingCell.HideItem();
            _playingUnit.gameObject.SetActive(true);

            GameInput.OnMouseUp += TryPlay;
        }

        private void TryPlay()
        {
            GameInput.OnMouseUp -= TryPlay;

            _playingCell.ShowItem();
            _playingUnit.gameObject.SetActive(false);
        }
    }
}

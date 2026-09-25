using UnityEngine;

namespace CSC2026
{
    public class UnitPlayer : MonoBehaviour
    {
        [SerializeField] private MovesManager _moves;
        [SerializeField] private PlayingUnit _playingUnit;
        
        private bool _canPlay = true;
        private InventoryCell _playingCell;

        public void StartPlaying(UnitType unit, InventoryCell cell)
        {
            if(!_canPlay)
            {
                return;
            }

            _playingCell = cell;

            _playingUnit.Initialize(unit);
            _playingCell.HideItem();
            _playingUnit.gameObject.SetActive(true);

            GameInput.OnMouseUp += TryPlay;
        }

        private void TryPlay()
        {
            GameInput.OnMouseUp -= TryPlay;
            _playingUnit.gameObject.SetActive(false);

            if(Mathf.Abs(GameInput.Position.y) < 2.55f)
            {
                _moves.SetPlayerUnit(_playingCell.GetUnit());
                _playingCell.Remove();
            }
            else
            {
                _playingCell.ShowItem();
            }
        }

        public void Block()
        {
            _canPlay = false;
        }

        public void UnBlock()
        {
            _canPlay = true;
        }
    }
}

using UnityEngine;

namespace CSC2026
{
    public class ItemPlayer : MonoBehaviour
    {
        [SerializeField] private MovesManager _moves;
        [SerializeField] private PlayingItem _playingItem;

        private bool _canPlay = true;
        private ItemCell _playingCell;

        public void StartPlaying(Item item, ItemCell cell)
        {
            if (!_canPlay)
            {
                return;
            }

            _playingCell = cell;

            _playingItem.Initialize(item);
            _playingCell.HideItem();
            _playingItem.gameObject.SetActive(true);

            GameInput.OnMouseUp += TryPlay;
        }

        private void TryPlay()
        {
            GameInput.OnMouseUp -= TryPlay;
            _playingItem.gameObject.SetActive(false);

            if (Mathf.Abs(GameInput.Position.y) < 2.55f)
            {
                _moves.MakePlayerItemMove(_playingCell.GetItem(), _playingItem.transform.position);
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

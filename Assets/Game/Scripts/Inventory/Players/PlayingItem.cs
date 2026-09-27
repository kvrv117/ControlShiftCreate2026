using UnityEngine;

namespace CSC2026
{
    public class PlayingItem : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public void Initialize(Item i)
        {
            Update();

            if(_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }

            _spriteRenderer.sprite = i.Sprite;
        }

        private void Update()
        {
            transform.position = GameInput.Position;
        }
    }
}

using UnityEngine;

namespace CSC2026
{
    public class PlayingUnit : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public void Initialize(UnitType unit)
        {
            Update();

            if(_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }

            _spriteRenderer.sprite = Units.GetUnitSprite(unit);
        }

        private void Update()
        {
            transform.position = GameInput.Position;
        }
    }
}

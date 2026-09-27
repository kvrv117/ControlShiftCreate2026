using DG.Tweening;
using UnityEngine;

namespace CSC2026
{
    public class ItemGiveAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _from;
        [SerializeField] private Transform _to;

        private SpriteRenderer _sr;

        public Tween GetMoveTween(UnitType unit, float jump, float time)
        {
            gameObject.SetActive(true);

            if(_sr == null)
            {
                _sr = GetComponent<SpriteRenderer>();
            }

            _sr.sprite = Units.GetUnitSprite(unit);
            transform.position = _from.position;

            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOJump(_to.position, jump, 1, time).SetEase(Ease.Linear));
            seq.Join(transform.DOScale(Vector3.one, time).From(Vector3.zero));
            seq.AppendCallback(() => { gameObject.SetActive(false); });

            return seq;
        }

        public Tween GetMoveTween(Item item, float jump, float time)
        {
            gameObject.SetActive(true);

            if (_sr == null)
            {
                _sr = GetComponent<SpriteRenderer>();
            }

            _sr.sprite = item.Sprite;
            transform.position = _from.position;

            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOJump(_to.position, jump, 1, time).SetEase(Ease.Linear));
            seq.Join(transform.DOScale(Vector3.one, time).From(Vector3.zero));
            seq.AppendCallback(() => { gameObject.SetActive(false); });

            return seq;
        }
    }
}

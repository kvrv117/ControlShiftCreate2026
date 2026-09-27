using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CSC2026
{
    public class ItemDescription : MonoBehaviour
    {
        private CanvasGroup _group;

        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _text;

        private void Start()
        {
            _group = GetComponent<CanvasGroup>();
        }

        public void Descript(Item i)
        {
            _group.DOKill();

            _group.DOFade(1f, 0.15f);

            _name.text = i.Name;
            _text.text = i.Description;
        }

        public void StopDescript()
        {
            _group.DOKill();
            _group.DOFade(0f, 0.15f);
        }
    }
}

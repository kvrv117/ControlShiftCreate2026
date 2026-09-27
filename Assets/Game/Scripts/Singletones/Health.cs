using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CSC2026
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private Image _image;

        [SerializeField] private int _currentHealth;
        [SerializeField] private int _maxHealth;

        public void ChangeHealth(int amount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);

            _image.DOFillAmount((float)_currentHealth / _maxHealth, 0.1f);
        }
    }
}

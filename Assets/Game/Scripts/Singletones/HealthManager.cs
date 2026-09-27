using UnityEngine;

namespace CSC2026
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] private Health _playerHealth;
        [SerializeField] private Health _enemyHealth;

        private static HealthManager _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static void ChangeEnemyHealth(int amount)
        {
            _instance._enemyHealth.ChangeHealth(amount);
        }

        public static void ChangePlayerHealth(int amount)
        {
            _instance._playerHealth.ChangeHealth(amount);
        }
    }
}

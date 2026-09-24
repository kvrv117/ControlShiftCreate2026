using UnityEngine;
using AYellowpaper.SerializedCollections;

namespace CSC2026
{
    public class Units : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<UnitType, Sprite> Sprites;

        private static Units _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static Sprite GetUnitSprite(UnitType type)
        {
            return _instance.Sprites[type];
        }
    }

    public enum UnitType
    {
        Rock,
        Paper,
        Scisors
    }
}

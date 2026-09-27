using UnityEngine;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;

namespace CSC2026
{
    public class Units : MonoBehaviour
    {
        [SerializeField] private UnitType[] _unitsPool;
        [SerializeField] private Item[] _itemsPool;

        private List<UnitType> _openedItems;

        [SerializeField] private SerializedDictionary<UnitType, Sprite> Sprites;

        private static Units _instance;

        private void Awake()
        {
            _openedItems = new List<UnitType>() { UnitType.Rock, UnitType.Paper, UnitType.Scisors};

            _instance = this;
        }

        public static Sprite GetUnitSprite(UnitType type)
        {
            return _instance.Sprites[type];
        }

        public static UnitType GetRandomUnit()
        {
            UnitType chosed = _instance._unitsPool[Random.Range(0, _instance._unitsPool.Length)];

            if (!_instance._openedItems.Contains(chosed))
            {
                UnitType[] winnerLoser = _instance._openedItems.Where(u => u != chosed).Distinct().OrderBy(_ => Random.Range(0f, 1f)).Take(2).ToArray();

                Rule rule1 = new Rule(winnerLoser[0], chosed);
                Rule rule2 = new Rule(chosed, winnerLoser[1]);

                Rules.AddRule(rule1);
                Rules.AddRule(rule2);

                _instance._openedItems.Add(chosed);
            }

            return chosed;
        }

        public static (UnitType, UnitType) GetTwoRandomUnit()
        {
            UnitType[] winnerLoser = _instance._openedItems.OrderBy(_ => Random.Range(0f, 1f)).Take(2).ToArray();

            return (winnerLoser[1], winnerLoser[0]);
        }

        public static Item GetRandomItem()
        {
            return _instance._itemsPool[Random.Range(0, _instance._itemsPool.Length)];
        }
    }

    public enum UnitType
    {
        Rock,
        Paper,
        Scisors,
        Pencil,
        Fire,
        Water
    }
}

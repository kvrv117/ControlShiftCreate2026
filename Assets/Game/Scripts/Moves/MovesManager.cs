using UnityEngine;
using System.Collections;

namespace CSC2026
{
    public class MovesManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _playerUnit;
        [SerializeField] private SpriteRenderer _enemyUnit;

        [SerializeField] private UnitPlayer _unitPlayer;
        [SerializeField] private Inventory _enemyInventory;

        public void SetPlayerUnit(UnitType playerUnit)
        {
            _unitPlayer.Block();

            _playerUnit.gameObject.SetActive(true);
            _playerUnit.sprite = Units.GetUnitSprite(playerUnit);

            UnitType enemyUnit = _enemyInventory.PopItem();

            _enemyUnit.gameObject.SetActive(true);
            _enemyUnit.sprite = Units.GetUnitSprite(enemyUnit);
        }

        private IEnumerator Fight()
        {
            yield return new WaitForSeconds(1.5f);
            _unitPlayer.UnBlock();
        }
    }
}
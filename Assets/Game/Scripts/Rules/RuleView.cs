using UnityEngine;
using UnityEngine.UI;

namespace CSC2026
{
    public class RuleView : MonoBehaviour
    {
        [SerializeField] private Image _winner;
        [SerializeField] private Image _loser;

        public void Intialize(Rule r)
        {
            _winner.sprite = Units.GetUnitSprite(r.Winner);
            _loser.sprite  = Units.GetUnitSprite(r.Loser);
        }
    }
}

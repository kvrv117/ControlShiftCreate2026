using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

namespace CSC2026
{
    public class Rules : MonoBehaviour
    {
        [SerializeField] private List<Rule> _rules;

        public static event UnityAction OnRulesChanged;        
        private static Rules _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static List<Rule> GetRules()
        {
            return _instance._rules;
        }

        public static void AddRule(Rule rule)
        {
            _instance._rules.Add(rule);
            OnRulesChanged?.Invoke();
        }

        public static void UpsideDown()
        {
            for(int i = 0; i < _instance._rules.Count; i++)
            {
                _instance._rules[i] = UpsideDown(_instance._rules[i]);
            }

            OnRulesChanged?.Invoke();
        }

        public static void Replace(UnitType a, UnitType b)
        {
            for(int i = 0; i < _instance._rules.Count; i++)
            {
                Rule r = _instance._rules[i];
                if(r.Winner == a)
                {
                    r.Winner = b;
                }
                if(r.Winner == b)
                {
                    r.Winner = a;
                }
                if(r.Loser == a)
                {
                    r.Loser = b;
                }
                if(r.Loser == b)
                {
                    r.Loser = a;
                }

                _instance._rules[i] = r;
            }

            OnRulesChanged?.Invoke();
        }

        private static Rule UpsideDown(Rule r)
        {
            return new Rule(r.Loser, r.Winner);
        }
    }

    [System.Serializable]
    public struct Rule
    {
        public UnitType Winner;
        public UnitType Loser;

        public Rule(UnitType winner, UnitType loser)
        {
            Winner = winner;
            Loser = loser;
        }

        public static bool operator ==(Rule left, Rule right)
        {
            return left.Winner == right.Winner && left.Loser == right.Loser;
        }

        public static bool operator !=(Rule left, Rule right)
        {
            return left.Winner != right.Winner || left.Loser != right.Loser;
        }
    }
}

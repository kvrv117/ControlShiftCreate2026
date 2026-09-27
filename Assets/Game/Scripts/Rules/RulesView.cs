using System.Collections.Generic;
using UnityEngine;

namespace CSC2026
{
    public class RulesView : MonoBehaviour
    {
        [SerializeField] private RuleView _view;
        [SerializeField] private RectTransform _content;

        private List<RuleView> _spawnedViews;

        private void Start()
        {
            _spawnedViews = new List<RuleView>();

            UpdateViews();
        }

        private void OnEnable()
        {
            Rules.OnRulesChanged += UpdateViews;
        }

        private void OnDisable()
        {
            Rules.OnRulesChanged -= UpdateViews;
        }

        private void UpdateViews()
        {
            List<Rule> rules = Rules.GetRules();

            if(rules.Count > _spawnedViews.Count)
            {
                int diff = rules.Count - _spawnedViews.Count;
                for (int i = 0; i < diff; i++)
                {
                    _spawnedViews.Add(Instantiate(_view, _content));
                }
            }

            for(int i = 0; i < rules.Count; i++)
            {
                _spawnedViews[i].Intialize(rules[i]);
            }
        }
    }
}

using AYellowpaper.SerializedCollections;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


namespace CSC2026
{
    public class MovesManager : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<FightResult, Transform> _endText;

        [SerializeField] private UnitPlayer _unitPlayer;
        [SerializeField] private ItemPlayer _itemPlayer;

        [SerializeField] private Inventory _playerInventory;
        [SerializeField] private Inventory _enemyInventory;

        [Header("Animation")]
        [SerializeField] private SpriteRenderer _playerUnitView;
        [SerializeField] private SpriteRenderer _enemyUnitView;

        [SerializeField] private SpriteRenderer _itemView;

        [SerializeField] private RectTransform _itemsPlayerScroll;
        [SerializeField] private RectTransform _itemsEnemyScroll;

        [SerializeField] private ItemGiveAnimation _playerUnitGive;
        [SerializeField] private ItemGiveAnimation _playerItemGive;
        [SerializeField] private ItemGiveAnimation _enemyUnitGive;
        [SerializeField] private ItemGiveAnimation _enemyItemGive;

        private UnitType _player;
        private UnitType _enemy;

        private Vector3 _playerFightPos;
        private Vector3 _enemyFightPos;
        private Vector3 _playerAwayPos;
        private Vector3 _enemyAwayPos;

        private void Start()
        {
            _playerFightPos = _playerUnitView.transform.position;
            _enemyFightPos = _enemyUnitView.transform.position;

            _playerAwayPos = _playerFightPos - Vector3.up * 5f;
            _enemyAwayPos = _enemyFightPos + Vector3.up * 5f;
        }

        #region Unit Play
        public void MakePlayerMove(UnitType playerUnit, Vector3 playerPos)
        {
            Vector3 enemyPos;

            _player = playerUnit;
            (_enemy, enemyPos) = _enemyInventory.PopUnit();

            _unitPlayer.Block();

            _playerUnitView.gameObject.SetActive(true);
            _playerUnitView.sprite = Units.GetUnitSprite(_player);
            _playerUnitView.transform.position = playerPos;

            _enemyUnitView.gameObject.SetActive(true);
            _enemyUnitView.sprite = Units.GetUnitSprite(_enemy);
            _enemyUnitView.transform.position = enemyPos;

            Fight();
        }

        private void Fight()
        {
            FightResult result = GetWinner(_player, _enemy);

            Sequence moveToFight = DOTween.Sequence();

            moveToFight.Append(_playerUnitView.transform.DOMove(_playerFightPos, 0.4f));
            moveToFight.Join(_enemyUnitView.transform.DOMove(_enemyFightPos, 0.4f));

            Sequence battle = DOTween.Sequence();

            battle.Append(_playerUnitView.transform.DOMove(Vector3.zero, 0.2f)).SetEase(Ease.OutSine);
            battle.Join(_enemyUnitView.transform.DOMove(Vector3.zero, 0.2f)).SetEase(Ease.OutSine);
            
            Sequence goAway = DOTween.Sequence();

            goAway.AppendCallback(() => { _endText[result].gameObject.SetActive(true); }).Append(_endText[result].DOScale(Vector3.one, 0.15f).From(Vector3.zero));

            if(result == FightResult.Win)
            {
                goAway.Join(BuildWinTween(_playerUnitView.transform, _playerAwayPos));
                goAway.Join(BuildLostTween(_enemyUnitView.transform, _enemyAwayPos));
                goAway.JoinCallback(() => { HealthManager.ChangeEnemyHealth(-1); });
            }
            else if(result == FightResult.Lose)
            {
                goAway.Join(BuildLostTween(_playerUnitView.transform, _playerAwayPos));
                goAway.Join(BuildWinTween(_enemyUnitView.transform, _enemyAwayPos));
                goAway.JoinCallback(() => { HealthManager.ChangePlayerHealth(-1); });
            }
            else
            {
                goAway.Join(BuildLostTween(_playerUnitView.transform, _playerAwayPos));
                goAway.Join(BuildLostTween(_enemyUnitView.transform, _enemyAwayPos));
            }

            goAway.Append(_endText[result].DOScale(Vector3.zero, 0.15f).From(Vector3.one)).AppendCallback(() => { _endText[result].gameObject.SetActive(false); });

            Sequence fight = DOTween.Sequence();

            fight.Append(moveToFight).Append(battle).Append(goAway).AppendCallback(StopFight);

            fight.Play();
        }

        private Tween BuildWinTween(Transform t, Vector3 goAwayPos)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(t.DOMove(goAwayPos, 0.75f));

            return seq;
        }

        private Tween BuildLostTween(Transform t, Vector3 goAwayPos)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(t.DOMove(goAwayPos, 0.5f));
            seq.Join(t.DORotate(new Vector3(0f, 0f, 720f), 0.5f, RotateMode.FastBeyond360));

            return seq;
        }

        private FightResult GetWinner(UnitType player, UnitType enemy)
        {
            if(player == enemy)
            {
                return FightResult.Tie;
            }

            List<Rule> rules = Rules.GetRules();
            Rule win = new Rule(player, enemy);
            Rule lose = new Rule(enemy, player);

            foreach (Rule rule in rules) 
            {
                if(win == rule)
                {
                    return FightResult.Win;
                }

                if(lose == rule)
                {
                    return FightResult.Lose;
                }
            }

            return FightResult.Tie;
        }

        private void StopFight()
        {
            _playerUnitView.gameObject.SetActive(false);
            _enemyUnitView.gameObject.SetActive(false);

            UnitType playerUnit = Units.GetRandomUnit();
            UnitType enemyUnit = Units.GetRandomUnit();
            Item playerItem = Units.GetRandomItem();
            Item enemyItem = Units.GetRandomItem();

            Sequence itemAnimation = DOTween.Sequence();

            itemAnimation.Append(_playerUnitGive.GetMoveTween(playerUnit, 2f, 0.5f));
            itemAnimation.Join(_enemyUnitGive.GetMoveTween(enemyUnit, -2f, 0.5f));

            itemAnimation.AppendCallback(() => { GiveUnits(playerUnit, enemyUnit); });

            itemAnimation.Append(_playerItemGive.GetMoveTween(playerItem, 2f, 0.5f));
            itemAnimation.Join(_enemyItemGive.GetMoveTween(enemyItem, -2f, 0.5f));

            itemAnimation.AppendCallback(() => { GiveItems(playerItem, enemyItem); ShowItemsInventories(); });

            itemAnimation.Play();
        }
        #endregion

        public void MakePlayerItemMove(Item item, Vector3 playerPos)
        {
            _itemPlayer.Block();

            UseItems(item, playerPos);
        }

        private void UseItems(Item playerItem, Vector3 playerPos)
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(UseItem(playerItem, playerPos, UserType.Player));
            seq.Append(UseItem(null, Vector3.zero, UserType.Enemy));

            seq.Append(_itemsPlayerScroll.DOAnchorPosY(-512, 0.4f));
            seq.Join(_itemsEnemyScroll.DOAnchorPosY(512, 0.4f));
            seq.AppendCallback(() => { _unitPlayer.UnBlock(); });

            seq.Play();
        }

        private Tween UseItem(Item item, Vector3 pos, UserType user)
        {
            Sequence seq = DOTween.Sequence();

            seq.AppendCallback(() => {
                if (user == UserType.Enemy)
                {
                    (item, pos) = _enemyInventory.PopItem();
                }
                ResetItemView(pos, item.Sprite);
            });
            seq.Append(_itemView.transform.DOMove(Vector3.zero, 0.3f));
            seq.Append(_itemView.transform.DOScale(Vector3.one * 1.1f, 0.1f));
            seq.AppendCallback(() => { item.Use(user); });
            seq.Append(_itemView.transform.DOScale(Vector3.zero, 0.2f));

            return seq;
        }

        private void ResetItemView(Vector3 pos, Sprite sprite)
        {
            _itemView.gameObject.SetActive(true);
            _itemView.transform.localScale = Vector3.one;
            _itemView.transform.position = pos;
            _itemView.sprite = sprite;
        }

        private void GiveUnits(UnitType playerUnit, UnitType enemyUnit)
        {
            _playerInventory.AddUnit(playerUnit);
            _enemyInventory.AddUnit(enemyUnit);
        }

        private void GiveItems(Item playerItem, Item enemyItem)
        {
            _playerInventory.AddItem(playerItem);
            _enemyInventory.AddItem(enemyItem);
        }

        private void ShowItemsInventories()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(_itemsPlayerScroll.DOAnchorPosY(0, 0.2f));
            seq.Join(_itemsEnemyScroll.DOAnchorPosY(0, 0.2f));
            seq.AppendCallback(() => { _itemPlayer.UnBlock(); });

            seq.Play();
        }

        public void ShowUnitsInventory()
        {
            if(_itemsPlayerScroll.anchoredPosition.y != 0)
            {
                ItemsScrollTween(_itemsEnemyScroll, 0);
                ItemsScrollTween(_itemsPlayerScroll, 0);
            }
            else
            {
                ItemsScrollTween(_itemsEnemyScroll, 512);
                ItemsScrollTween(_itemsPlayerScroll, -512);
            }
        }

        private void ItemsScrollTween(RectTransform scroll, float y)
        {
            scroll.DOKill();
            scroll.DOAnchorPosY(y, 0.2f);
        }
    }

    public enum FightResult {Win, Tie, Lose}
}
using UnityEngine;
using UnityEngine.EventSystems;

public enum BattleCardState
{
    inHand, inBlock
}
public class BattleCard : MonoBehaviour, IPointerDownHandler
{
    public int playerID;
    public BattleCardState state = BattleCardState.inHand;

    public int AttackCount;
    private int attackCount;
    public void OnPointerDown(PointerEventData eventData)
    {
        //当在手牌点击时，发起召唤请求
        if (GetComponent<CardDisplay>().card is MonsterCard)
        {
            if (state == BattleCardState.inHand)
            {
                BattleManager.instance.SummonRequest(playerID, gameObject);
            }
            else if (state == BattleCardState.inBlock && attackCount>0)//当在场上点击时，发起攻击请求
            {
                BattleManager.Instance.AttackRequest(playerID, gameObject);
            }

            
        }   
    }
    public void ResetAttack()
    {
        attackCount =AttackCount;
    }
    public void CostAttackCount()
    {
        attackCount--;
    }
}


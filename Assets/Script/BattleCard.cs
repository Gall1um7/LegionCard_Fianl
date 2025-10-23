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
        //�������Ƶ��ʱ�������ٻ�����
        if (GetComponent<CardDisplay>().card is MonsterCard)
        {
            if (state == BattleCardState.inHand)
            {
                BattleManager.instance.SummonRequest(playerID, gameObject);
            }
            else if (state == BattleCardState.inBlock && attackCount>0)//���ڳ��ϵ��ʱ�����𹥻�����
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


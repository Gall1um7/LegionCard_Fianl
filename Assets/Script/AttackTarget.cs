using UnityEngine;
using UnityEngine.EventSystems;

public class AttackTarget : MonoBehaviour,IPointerClickHandler
{
    public bool attackable;
   
    public void OnPointerClick(PointerEventData eventData)
    {
        if (attackable)
        {
            BattleManager.Instance.Attackconfirm(gameObject);              

        }
    }
    public void ApplyDamage(int _damage)
    {
        MonsterCard monster = GetComponent<CardDisplay>().card as MonsterCard;
        monster.healthPoint -= _damage;
        if(monster.healthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

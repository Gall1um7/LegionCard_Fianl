using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IPointerDownHandler
{
    public GameObject card;
    public GameObject SummonBlock;
    public GameObject AttackBlock;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (SummonBlock.activeInHierarchy)
        {
            BattleManager.Instance.SummonConfirm(transform);
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

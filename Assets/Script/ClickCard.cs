using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
public enum CardState
{
    Library,Deck
}
public class ClickCard : MonoBehaviour,IPointerDownHandler
{
    private DeckManager DeckManager;
    //private PlayerData PlayerData;

    public CardState State;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        DeckManager = GameObject.Find("DeckManager").GetComponent<DeckManager>();
        //PlayerData = GameObject.Find("DataManager").GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        int id = this.GetComponent<CardDisplay>().card.id;
        DeckManager.UpdateCard(State, id);
    }
}

using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CardStore : MonoBehaviour
{
    public TextAsset cardData;
    public List<Card> cardList = new List<Card>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //LoadCardData();
        //TestLoad();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadCardData()
    {
        //Debug.Log("Loadcarddata");
        string[] dataRow = cardData.text.Split('\n');
        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(',');
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "monster")
            {
                //新建怪兽卡
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                int atk = int.Parse(rowArray[3]);
                int health= int.Parse(rowArray[4]);
                MonsterCard monsterCard = new MonsterCard( id, name, atk, health);
                cardList.Add(monsterCard );

                //Debug.Log("Save:"+ monsterCard.cardName);
            }
            else if (rowArray[0] == "spell")
            {
                //新建魔法卡
                int id = int.Parse(rowArray[1]);
                string name = rowArray[2];
                string effect = rowArray[3];
                SpellCard spellCard = new SpellCard( id, name, effect );
                cardList.Add(spellCard );
            }
        }
        
    }
    public void TestLoad()
    {
        foreach (var item in cardList)
        {
            Debug.Log( "CARD: " + item.id.ToString() +item.cardName);
        }
    }

    public Card RandomCard()
    {
        Card card = cardList[Random.Range(0, cardList.Count)];
        return card;
    }
    public Card CopyCard(int _id)
    {
        Card copyCard = new Card(_id, cardList[_id].cardName);
        if (cardList[_id] is MonsterCard)
        {
         var monstercard = cardList[_id] as MonsterCard;
         copyCard = new MonsterCard(_id, monstercard.cardName, monstercard.attack, monstercard.healthPointMax);
        }
        else if (cardList[_id] is SpellCard)
        {
            var spellcard = cardList[_id] as SpellCard;
            copyCard = new SpellCard(_id, spellcard.cardName,spellcard.effect);
        }
        //其他卡牌类型用elseif加到这里
            return copyCard;
    }
}

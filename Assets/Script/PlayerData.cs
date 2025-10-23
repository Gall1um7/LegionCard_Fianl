using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class PlayerData : MonoBehaviour
{
    public CardStore cardStore;
    public int playerCoins;
    public int[] playerCards;
    public int[] playerDeck;
    public TextAsset playerData;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        //Debug.Log("Player Data Start");

        cardStore.LoadCardData();
        LoadPlayerData();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlayerData()
    {
        playerCards = new int[cardStore.cardList.Count];
        playerDeck = new int[cardStore.cardList.Count];

        string[] dataRow = playerData.text.Split('\n');
        foreach (var row in dataRow)
        {
            // �������л���Ч��
            if (string.IsNullOrWhiteSpace(row)) continue;

            string[] rowArray = row.Trim().Split(',');

            // �����ջ�ע����
            if (rowArray.Length == 0 || rowArray[0].Trim() == "#" || rowArray[0].Trim() == "") continue;

            // coins ��
            if (rowArray[0].Trim() == "coins")
            {
                if (rowArray.Length > 1 && int.TryParse(rowArray[1].Trim(), out int coins))
                    playerCoins = coins;
                else
                    Debug.LogWarning($"[PlayerData] coins �и�ʽ����: {row}");
            }
            // card ��
            else if (rowArray[0].Trim() == "card")
            {
                if (rowArray.Length > 2 &&
                    int.TryParse(rowArray[1].Trim(), out int id) &&
                    int.TryParse(rowArray[2].Trim(), out int num))
                {
                    if (id >= 0 && id < playerCards.Length)
                        playerCards[id] = num;
                }
                else
                {
                    Debug.LogWarning($"[PlayerData] card �и�ʽ����: {row}");
                }
            }
            // deck ��
            else if (rowArray[0].Trim() == "deck")
            {
                if (rowArray.Length > 2 &&
                    int.TryParse(rowArray[1].Trim(), out int id) &&
                    int.TryParse(rowArray[2].Trim(), out int num))
                {
                    if (id >= 0 && id < playerDeck.Length)
                        playerDeck[id] = num;
                }
                else
                {
                    Debug.LogWarning($"[PlayerData] deck �и�ʽ����: {row}");
                }
            }
        }
    }
    public void SavePlayerData() 
    {
        string path = Application.dataPath + "/Datas/playerdata.csv";


        List<string> datas = new List<string>();
        datas.Add("coins, " + playerCoins.ToString());
        for (int i = 0; i < playerCards.Length; i++)
        {
            if (playerCards[i] != 0)
            {
                datas.Add("card," + i.ToString() + "," + playerCards[i].ToString());
            }
        }
        //���濨��
        for (int i = 0; i < playerDeck.Length; i++)
        {
            if (playerDeck[i] != 0) 
            {
                datas.Add("deck,"+ i.ToString()+ "," + playerDeck[i].ToString());
            }
        }
        //��������
        File.WriteAllLines(path, datas);
    //Debug,Log(datas);
    }
    
    
        
}

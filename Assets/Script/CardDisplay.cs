using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CardDisplay : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI effectText;

    public Image Draw;
    public Image backgroundImage;
    public Card card;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowCard()
    {
        nameText.text = card.cardName;
        if(card is MonsterCard)
        {
            var monster = card as MonsterCard;
            attackText.text = monster.attack.ToString();
            healthText.text = monster.healthPoint.ToString();

            effectText.gameObject.SetActive(false);
        }
        else if(card is SpellCard)
        {
            var spell = card as SpellCard;
            effectText.text = spell.effect.ToString();
            attackText.gameObject.SetActive(false);
            healthText.gameObject.SetActive(false);
        }
    }
}

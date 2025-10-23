using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PhaseDisplayer : MonoBehaviour
{
   
    public TextMeshProUGUI phaseText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BattleManager.Instance.phaseChangeEvent.AddListener(UpdateText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateText()
    {
        phaseText.text = BattleManager.Instance.GamePhase.ToString();
    }
}

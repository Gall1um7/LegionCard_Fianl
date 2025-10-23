using NUnit.Framework;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine.Events;
public enum GamePhase
{
    gameStart,playerDraw,playerAction,enemyDraw,enemyAction
}

public class BattleManager : Monosingleton<BattleManager>
{
    public static BattleManager instance;
    public PlayerData playerData;
    public PlayerData enemyData;//����


    public List<Card> playerDeckList = new List<Card>();
    public List<Card> enemyDeckList = new List<Card>();//����

    public GameObject cardPrefab;//����

    public Transform playerHand;
    public Transform enemyHand;//����

    public GameObject[] playerBlocks;
    public GameObject[] enemyBlocks;//����

    public GameObject playerIcon;
    public GameObject enemyIcon;//ͷ��



    public GamePhase GamePhase = GamePhase.gameStart;

    public UnityEvent phaseChangeEvent = new UnityEvent();

    public int[] SummonCountMax = new int[2];//0 player, 1 enemy
    private int[] SummonCounter = new int[2];

    //�ٻ���������
    private GameObject waitingMonster;
    private int waitingPlayer;
    public GameObject ArrowPrefab;
    private GameObject arrow;

    public Transform Canvas;

    private GameObject attackingMonster;
    private int attackingPlayer;
    public GameObject attackArrow;



    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            DestroyArrow();
            waitingMonster = null;
            CloseBlocks();
        }
    }
    //��Ϸ����
    //��ʼ��Ϸ���������ݣ�����ϴ�ƣ���ʼ����
    //�غϽ�������Ϸ�׶�
    public void GameStart()
    {
        //��ȡ����
        ReadDeck();

        //����ϴ��
        ShuffletDeck(0);
        ShuffletDeck(1);

        //��ҳ鿨3�����˳鿨3
        DrawCard(0, 3);
        DrawCard(1, 3);

        NextPhase();

        SummonCounter = SummonCountMax;
    }
    public void ReadDeck()
    {
        //������ҿ���
        for (int i = 0; i < playerData.playerDeck.Length; i++)
        {
            if (playerData.playerDeck[i] != 0)
            {
                int count = playerData.playerDeck[i];
                for (int j = 0; j < count; j++) 
                {
                
                    playerDeckList.Add(playerData.cardStore.CopyCard(i));
                }
            }
        }
        
        //���ص��˿���
        for (int i = 0; i < enemyData.playerDeck.Length; i++)
        {
            Debug.Log(enemyData.playerDeck[i] );
            if (enemyData.playerDeck[i] != 0)
            {
                Debug.Log("enemyData");
                int count = enemyData.playerDeck[i];
                for (int j = 0; j < count; j++)
                { 
                    Debug.Log("load enemy card");
                    enemyDeckList.Add(enemyData.cardStore.CopyCard(i));
                }
            }
        }
    }

    public void ShuffletDeck(int _player)//0Ϊ���,1Ϊ����
    {
        List<Card> shuffletDeck = new List<Card>();
        if (_player == 0)
        {
            shuffletDeck = playerDeckList;
            Debug.Log("Player deck - " + shuffletDeck.Count);
        }
        else if (_player == 1)
        {
            shuffletDeck = enemyDeckList;
            Debug.Log("Enemy deck - " + shuffletDeck.Count);
        }

        for (int i = 0; i < shuffletDeck.Count; i++)
        { 
            int rad = Random.Range(i, shuffletDeck.Count);
            Card temp = shuffletDeck[i];
            shuffletDeck[i] = shuffletDeck[rad];
            shuffletDeck[rad]= temp;
        }
    }

    public void OnPlayerDraw()
    {
        if(GamePhase == GamePhase.playerDraw)
        {
           DrawCard(0, 1);
        }
        NextPhase();
    }
    public void OnEnemyDraw()
    {
        if (GamePhase == GamePhase.enemyDraw)
        {
           DrawCard(1, 1);
        }
        NextPhase();
    }
    public void DrawCard(int _player, int _count)
    {
        List<Card> drawDeck = new List<Card>();
        Transform hand = transform;
        if (_player == 0)
        {
            drawDeck = playerDeckList;
            hand = playerHand;
        }
        else if (_player == 1) 
        {
            drawDeck= enemyDeckList;
            hand = enemyHand;
        }
        for(int i = 0; i < _count; i++) 
        {
            GameObject card = Instantiate(cardPrefab, hand);
            card.GetComponent<CardDisplay>().card = drawDeck[0];
            card.GetComponent<BattleCard>().playerID = _player;
           
            drawDeck.RemoveAt(0);
        }
    }

    public void OnClickTurnEnd()
    {
        TurnEnd();
    }
    public void TurnEnd()
    {
        if(GamePhase == GamePhase.playerAction)
        {
            NextPhase();
        }
        else if (GamePhase == GamePhase.enemyAction)
        {
            NextPhase();
        }
    }
    public void NextPhase()
    {
        if((int)GamePhase==System.Enum.GetNames(GamePhase.GetType()).Length-1)
        {
            GamePhase = GamePhase.playerDraw;
        }
        else
        {
            GamePhase++;
        }
            phaseChangeEvent.Invoke();
        
    }
    /// <summary>
    /// �����ٻ�����Issue a summon request
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_monster"></param>
    public void SummonRequest(int _player,GameObject _monster)
    {
        GameObject[] blocks;
        bool hasEmptyBlock = false;
        if (_player == 0 && GamePhase == GamePhase.playerAction)
        {
            blocks = playerBlocks;
        }
      
        else if (_player == 1 && GamePhase == GamePhase.enemyAction)
        {
                blocks = enemyBlocks;
        }
        else
        {
            return;
        }
        if (SummonCounter[_player] > 0)
        {
            foreach (var block in blocks)
            {
                if (block.GetComponent<Block>().card == null)
                {
                    
                    block.GetComponent<Block>().SummonBlock.SetActive(true);//�ȴ��ٻ���ʾ
                    hasEmptyBlock = true;
                }
            }
        }
        if (hasEmptyBlock)
        {
            waitingMonster = _monster;
            waitingPlayer = _player;
            CreatArrow(_monster.transform, ArrowPrefab);
        }
    }
    /// <summary>
    /// �ٻ�ȷ��Summoning Confirmation
    /// </summary>
    /// <param name="_block"></param>
    public void SummonConfirm(Transform _block)
    {
        Summon(waitingPlayer,waitingMonster, _block);
        CloseBlocks();
        DestroyArrow();
       
    }
    public void Summon(int _player, GameObject _monster, Transform _block)
    {
        _monster.transform.SetParent(_block);
        _monster.transform.localPosition = Vector3.zero;
        _monster.GetComponent<BattleCard>().state = BattleCardState.inBlock;
        _block.GetComponent<Block>().card = _monster;
        SummonCounter[_player] --;

        MonsterCard mc = _monster.GetComponent<CardDisplay>().card as MonsterCard;
        _monster.GetComponent<BattleCard>().AttackCount = mc.attackTime;
        _monster.GetComponent<BattleCard>().ResetAttack();
    }

    public void AttackRequest(int _player, GameObject _monster)
    {
        GameObject[] blocks;
        bool hasMonsterBlock = false;
        if (_player == 0 && GamePhase == GamePhase.playerAction)
        {
            blocks = enemyBlocks;
        }
        else if (_player == 1 && GamePhase == GamePhase.enemyAction)
        {
            blocks = playerBlocks;
        }
        else
        {
            return;
        }
        foreach (var block in blocks)
        {
            if (block.GetComponent<Block>().card != null)
            {
                block.GetComponent<Block>().AttackBlock.SetActive(true);//�ȴ�������ʾ
                block.GetComponent<Block>().card.GetComponent<AttackTarget>().attackable = true;
                hasMonsterBlock = true;
            }
        }
        if (hasMonsterBlock) 
        { 
            attackingMonster = _monster;
            attackingPlayer = _player;
            CreatArrow(_monster.transform, attackArrow);
        }
    }
    /// <summary>
    /// ��������attack monster
    /// </summary>
    /// <param name="_target">����������attacked monster</param>
    public void Attackconfirm(GameObject _target)
    {
        Attack(attackingMonster, _target);
        DestroyArrow();
        CloseBlocks();
        GameObject[] blocks;
        if (attackingPlayer == 0)
        {
            blocks = enemyBlocks;
        }
        else
        {
            blocks = playerBlocks;
        }
        foreach ( var block in blocks)
        {
            if(block.GetComponent<Block>().card != null)
            {
                block.GetComponent<Block>().card.GetComponent<AttackTarget>().attackable = false;
            }
        }
    }
    public void Attack(GameObject _attacker,GameObject _target)
    {
        MonsterCard monster = _attacker.GetComponent<CardDisplay>().card as MonsterCard;
        _target.GetComponent<AttackTarget>().ApplyDamage(monster.attack);
        _attacker.GetComponent<BattleCard>().CostAttackCount();
        _target.GetComponent<CardDisplay>().ShowCard();
    }

    public void CreatArrow(Transform _startPoint, GameObject _prefab)
    {
        DestroyArrow();
        arrow = GameObject.Instantiate(_prefab, _startPoint);
        arrow.GetComponent<Arrow>().SetStartPoint(new Vector2(_startPoint.position.x, _startPoint.position.y));
    }
    public void DestroyArrow()
    {
        Destroy(arrow);
        Destroy(attackArrow);
    }

    public void CloseBlocks()
    {
        
        foreach (var block in playerBlocks)
        {
            block.GetComponent<Block>().SummonBlock.SetActive(false);//�ر��ٻ���ʾ
            block.GetComponent<Block>().AttackBlock.SetActive(false);//�رչ�����ʾ
        }
        foreach (var block in enemyBlocks)
        {
            block.GetComponent<Block>().SummonBlock.SetActive(false);//�ر��ٻ���ʾ
            block.GetComponent<Block>().AttackBlock.SetActive(false);//�رչ�����ʾ
        }
    }

}

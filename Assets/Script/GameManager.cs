using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager ins;
    public GameObject[] Status;
    public Text[] swordmanTxt, priestTxt, witchTxt;
    public GameObject[] players;

    public GameObject Monster1, Monster2, Monster3;
    public List<GameObject> L_Monster = new List<GameObject>();
    public Slider turn;
    public TextMeshProUGUI turntxt;
    public float turnTime = 10;
    CoolTime ct;
    public bool playerTurn = true;
    public bool MonsterTurn = false;
    public bool CurrTurn = false;
    public Dictionary<string, GameObject> statusValue = new Dictionary<string, GameObject>();
    private void Awake()
    {
        ins = this;
        ct = new CoolTime();
    }
    // Start is called before the first frame update
    void Start()
    {
        statusValue.Add("�˻�",players[0]);
        statusValue.Add("����", players[1]);
        statusValue.Add("������", players[2]);

        Status = GameObject.FindGameObjectsWithTag("Status");

        swordmanTxt = Status[2].GetComponentsInChildren<Text>();
        priestTxt = Status[1].GetComponentsInChildren<Text>();
        witchTxt = Status[0].GetComponentsInChildren<Text>();
        L_Monster.Add(Monster1);
        L_Monster.Add(Monster2);
        L_Monster.Add(Monster3);
    }

    // Update is called once per frame
    void Update()
    {
        StatusShow();
        turn.value = ct.Timer(turnTime);
        if(turn.value == 0)
        {
            playerTurn = !playerTurn;
            CurrTurn = playerTurn;
            if (playerTurn)
            {
                turntxt.text = "Player Turn";
                MonsterTurn = false;
            }
            else
            {
                turntxt.text = "Monster Turn";
                MonsterTurn = true;
                
            }
            
        }
        Debug.Log("CurrTurn : " + CurrTurn);
        //StatusView("�˻�", statusValue, swordmanTxt);
        //StatusView("����", statusValue, priestTxt);
        //StatusView("������", statusValue, witchTxt);
    }
    /*void StatusView(string Key, Dictionary<string,GameObject> keys, Text[] txt)
    {
        GameObject player = keys[Key];
        Status st = player.GetComponent<Player>().status;
        txt[0].text = st.Pname;
        txt[1].text = "���� : " + st.Level.ToString();
        txt[2].text = "����ġ : " + st.Exp.ToString();
        txt[3].text = "HP : " + (st.Hp).ToString() + " / " + (st.MaxHp).ToString();
        txt[4].text = "MP : " + (st.Mp).ToString() + " / " + (st.MaxMp).ToString();
    }*/
    void StatusShow()
    {
        if (statusValue.ContainsKey("�˻�"))
        {
            Player p = statusValue["�˻�"].GetComponent<Player>();

            if(p != null)
            {
                swordmanTxt[0].text = p.status.Pname;
                swordmanTxt[1].text = "���� :       " + p.status.Level.ToString();
                swordmanTxt[2].text = "����ġ :     " + p.status.Exp.ToString();
                swordmanTxt[3].text = "HP :         " + (p.status.Hp).ToString() + " / " + (p.status.MaxHp).ToString();
                swordmanTxt[4].text = "MP :         " + (p.status.Mp).ToString() + " / " + (p.status.MaxMp).ToString();
            }
        }
        if (statusValue.ContainsKey("����"))
        {
            Player p = statusValue["����"].GetComponent<Player>();

            if (p != null)
            {
                priestTxt[0].text = p.status.Pname;
                priestTxt[1].text = "���� :       " + p.status.Level.ToString();
                priestTxt[2].text = "����ġ :     " + p.status.Exp.ToString();
                priestTxt[3].text = "HP :         " + (p.status.Hp).ToString() + " / " + (p.status.MaxHp).ToString();
                priestTxt[4].text = "MP :         " + (p.status.Mp).ToString() + " / " + (p.status.MaxMp).ToString();
            }
        }
        if (statusValue.ContainsKey("������"))
        {
            Player p = statusValue["������"].GetComponent<Player>();

            if (p != null)
            {
                witchTxt[0].text = p.status.Pname;
                witchTxt[1].text = "���� :       " + p.status.Level.ToString();
                witchTxt[2].text = "����ġ :     " + p.status.Exp.ToString();
                witchTxt[3].text = "HP :         " + (p.status.Hp).ToString() + " / " + (p.status.MaxHp).ToString();
                witchTxt[4].text = "MP :         " + (p.status.Mp).ToString() + " / " + (p.status.MaxMp).ToString();
            }
        }
    }
}

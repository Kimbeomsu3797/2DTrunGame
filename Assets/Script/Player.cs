using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Status status;

    GameObject[] monster;
    Rigidbody2D rig;
    public bool home = true;
    public bool back = false;
    public Vector3 oriPos;
    Animator ani;
    public int HP;
    public int MaxHP;
    public GameObject MagicAura;
    public Transform T_MagicAura;
    public GameObject Explosion;
    //public TextMeshProUGUI dmgTxt;
   // public Animator dmgAnim;
    //public GameObject dmgtxt;
    public GameObject DamageCanvas;
    TextMeshProUGUI TMPdamage;
    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.FindGameObjectsWithTag("Monster");
        rig = GetComponent<Rigidbody2D>();
        oriPos = transform.position;
        ani = GetComponent<Animator>();
        HP = status.Hp;
        MaxHP = status.MaxHp;
        //dmgAnim = dmgtxt.GetComponent<Animator>();
        //dmgTxt = dmgtxt.GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(back == true)
        {
            rig.MovePosition(Vector3.Lerp(transform.position, oriPos, 20 * Time.deltaTime));
            if(Vector3.Distance(transform.position, oriPos) <= 0.5f)
            {
                transform.position = oriPos;
                home = true;
            }
        }
    }
    //플레이어에게 (물리)충돌넣기 - rigidbody랑 boxcollider있음 trigger로 체크되어있으니 아마 ontrigger로 데미지 받을듯
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    public void NomalAttack()
    {
        
        if(GameManager.ins.CurrTurn == false)
        {
            StartCoroutine("NomalAttackCT");
        }
    }
    public void SpecialAttack()
    {
        if(GameManager.ins.CurrTurn == false && home)
        {
            StartCoroutine(SpecialAttackCT());
        }
    }
    IEnumerator SpecialAttackCT()
    {
        int r = Random.Range(0, monster.Length);
        Instantiate(MagicAura, T_MagicAura.position, T_MagicAura.rotation);
        yield return new WaitForSeconds(2.5f);

        if(monster[r] != null)
        {
            if (!status.Job.Equals("프리스트"))
            {
                Instantiate(Explosion, monster[r].transform.position + Vector3.up * 0.8f, Quaternion.identity);
            }
            else
            {
                GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
                int i = Random.Range(0, player.Length);
                Instantiate(Explosion, player[i].transform.position + Vector3.up * 0.8f, Quaternion.identity);
            }
        }
    }
    IEnumerator NomalAttackCT()
    {
        
        monster = GameObject.FindGameObjectsWithTag("Monster");
        back = false;
        int r = Random.Range(0, monster.Length);

        while (true)
        {
            if (monster[r] != null)
            {
                home = false;
                rig.MovePosition(Vector3.Lerp(transform.position, monster[r].transform.position, 20 * Time.deltaTime));
                if (Vector3.Distance(transform.position, monster[r].transform.position) <= 0.5f)
                {
                    ani.SetTrigger("attack");
                    Sound();
                    monster[r].GetComponent<Monster>().Damage(status.Attack);
                    //dmgTxt.rectTransform.position = monster[r].transform.position;
                    //dmgTxt.text = status.Attack.ToString();
                    yield return new WaitForSeconds(0.3f);
                    back = true;
                    break;
                }
            }
            
            yield return null;
        }
    }
    public void Sound()
    {
        if(status.Job == "검사")
        {
            SoundManger.instance.PlayAttackSound(8);
        }
        if (status.Job == "프리스트")
        {
            SoundManger.instance.PlayAttackSound(4);
        }
        if (status.Job == "마법사")
        {
            SoundManger.instance.PlayAttackSound(2);
        }
    }
    public void Damage(int attDamage)
    {
        HP -= attDamage;
        ani.SetTrigger("damage");
        GameObject go = Instantiate(DamageCanvas, transform.position, Quaternion.identity);
        go.transform.SetParent(transform);
        TMPdamage = go.GetComponentInChildren<TextMeshProUGUI>();
        TMPdamage.text = " " + attDamage;
        if (HP <= 0)
        {
            GameManager.ins.statusValue.Remove(status.Job);
            Destroy(gameObject);
        }
        
    }
}

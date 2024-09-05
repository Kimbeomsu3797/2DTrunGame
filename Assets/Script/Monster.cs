using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Monster : MonoBehaviour
{
    public MonsterData monData;
    GameObject[] player;
    Rigidbody2D rig;
    public bool back = false;
    public Vector3 oriPos;
    Animator anim;

    public int HP;
    public int MaxHP;

    public GameObject DamageCanvas;
    TextMeshProUGUI TMPdamage;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        //player = GameObject.FindGameObjectsWithTag("player");
        oriPos = transform.position;
        anim = GetComponent<Animator>();
        HP = monData.Hp;
        MaxHP = monData.MaxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Damage(int attValue)
    {
        HP -= attValue;
        anim.SetTrigger("damage");
        GameObject go = Instantiate(DamageCanvas, transform.position, Quaternion.identity);
        go.transform.SetParent(transform);
        TMPdamage = go.GetComponentInChildren<TextMeshProUGUI>();
        TMPdamage.text = " " + attValue;
        if (HP <= 0)
        {
            GameManager.ins.L_Monster.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    public void NomalAttack()
    {
        StartCoroutine("NomalAttackCT");
    }
    IEnumerator NomalAttackCT()
    {

        player = GameObject.FindGameObjectsWithTag("player");
        back = false;
        int r = Random.Range(0, player.Length);

        while (true)
        {
            if (player[r] != null)
            {

                rig.MovePosition(Vector3.Lerp(transform.position, player[r].transform.position, 20 * Time.deltaTime));
                if (Vector3.Distance(transform.position, player[r].transform.position) <= 0.5f)
                {
                    anim.SetTrigger("attack");
                    player[r].GetComponent<Player>().Damage(monData.Attack);
                    yield return new WaitForSeconds(0.3f);
                    back = true;
                    GameManager.ins.attCheck = true;
                    break;
                }
            }

            yield return null;
        }
    }
}

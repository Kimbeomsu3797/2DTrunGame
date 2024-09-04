using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Status status;

    GameObject[] monster;
    Rigidbody2D rig;
    public bool home = true;
    public bool back = false;
    public Vector3 oriPos;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.FindGameObjectsWithTag("Monster");
        rig = GetComponent<Rigidbody2D>();
        oriPos = transform.position;
        ani = GetComponent<Animator>();
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

                    monster[r].GetComponent<Monster>().Damage(status.Attack);
                    yield return new WaitForSeconds(0.3f);
                    back = true;
                    break;
                }
            }
            
            yield return null;
        }
    }
}

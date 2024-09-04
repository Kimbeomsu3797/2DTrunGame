using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTime
{
    
    public float cooltime;

    float coolCnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        coolCnt = Time.time;
    }
    public float Timer(float t)
    {
        cooltime += Time.deltaTime;

        if(coolCnt + t <= Time.time)
        {
            coolCnt = Time.time;

            cooltime = 0;
        }
        return cooltime;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

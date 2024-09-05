using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManger : MonoBehaviour
{
    public static SoundManger instance;

    AudioSource myAudio;
    public AudioClip[] AttackSound;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        
    }
    public void PlayAttackSound(int num)
    {
        myAudio.PlayOneShot(AttackSound[num]);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitch : MonoBehaviour
{

    [SerializeField]
    AudioClip bossMusic;
    [SerializeField]
    AudioClip levelMusic;
    [SerializeField]
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayMusicBoss()
    {
       audio.clip= bossMusic;
       audio.Play();
    }
    public void PlayLevelMusic()
    {
        audio.clip=levelMusic;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateBoss : MonoBehaviour
{
    [SerializeField]
    Enemy bossEnemy;
    [SerializeField]
    Slider bossHealthBar;
    [SerializeField]
    MusicSwitch musicSwitcher;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Activation()
    {
        bossEnemy.Activate();
        bossHealthBar.gameObject.SetActive(true);
        musicSwitcher.PlayMusicBoss();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy Activation!");
            Activation();
            Destroy(this.gameObject);
        }
    }
}

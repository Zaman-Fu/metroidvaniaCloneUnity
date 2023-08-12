using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Spike Trap Collision with something!");
        //if it's player, have it take some damage
        if (collision.gameObject.GetComponent<CharacterController2D>() is CharacterController2D)
        {
            CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
            if (!player.IsIntangible())
            {
                player.TakeDamage(damage, gameObject);
            }

        }
    }
}

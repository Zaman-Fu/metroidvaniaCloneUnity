using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected bool picked=false;
    protected abstract void OnPickup(CharacterController2D character);





    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<CharacterController2D>() is CharacterController2D)
        {
            if(!picked)
            {
                OnPickup(collision.gameObject.GetComponent<CharacterController2D>());
                picked = true;
            }
            

            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterController2D>() is CharacterController2D)
        {
            if (!picked)
            {
                OnPickup(collision.gameObject.GetComponent<CharacterController2D>());
                picked = true;
            }


            Destroy(gameObject);
        }
    }


    /// <summary>
    /// In order to clean up, any pickup that remains in play for too long will be destroyed. This coroutine should be called on Start for every pickup object
    /// </summary>
    /// <returns></returns>
    protected IEnumerator SDTimer()
    {
        while(true)
        {
            yield return new WaitForSeconds(10.0f);
            Destroy(gameObject);
        }
    }

}

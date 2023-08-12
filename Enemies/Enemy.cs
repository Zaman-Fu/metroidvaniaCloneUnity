using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The papa of all enemy entities in the game. If it's a destructible hazard, we might as well classify it as an enemy also.
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int damage;
    [SerializeField] protected GameObject deathEffect;//the animation that will play as the enemy dissipates.
    [SerializeField] protected GameObject itemSpawn;// the Item that a given enemy will spawn
    [SerializeField] protected int dropChance; // the chance, in percentage, for an enemy to drop any item

    protected AudioSource audioSource; //audio source for various sound effects
    protected SpriteRenderer spriteRenderer; //Sprite renderer for minor visual effects
    
    protected bool activated; //for enemies who may need to be activated by a trigger in order to beging their moves.
    protected bool isDying;//for enemies who are set to die and be destroyed.


    //called for every update
    protected abstract void Move();

    //Called within the Move function in case the conditions for an attack are met. Some enemies have it empty, and so all is well
    protected abstract void Attack();

    /// <summary>
    /// The simplest way to take damage
    /// </summary>
    public virtual void TakeDamage(int damageTaken,Vector3 charPos)
    {
        health -= damageTaken;
        ChangeColour();
        StartCoroutine("DamageEffect");
        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// be removed from play, cease to exist, be consigned to the void between realities for eternity.
    /// </summary>
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Activate()
    {
        activated = true;
    }
    //The object is destroyed so there's no sense in having a sound source on it. Next best plan, maybe call to a static method to tell a manager to play the object?
    protected virtual void PlayDeathSound()
    {
        Debug.Log("Playing death sound");
       
    }

    protected virtual void ChangeColour()
    {
        Debug.Log("Change colours");
        if (spriteRenderer.color == Color.white)
        {
            spriteRenderer.color = Color.red;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
    protected IEnumerator DamageEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            ChangeColour();
            break;
        }
    }
  

}

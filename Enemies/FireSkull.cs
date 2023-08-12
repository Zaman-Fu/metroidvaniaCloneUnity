using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// This dude just flies around the stage unbothered by any obstacle. He flies forward at a sine shaped trajectory.
/// Only interaction is between it and player. He either dies on one hit, or hurts the player as it passes through.
/// </summary>
public class FireSkull : Enemy
{
    



    //x speed
    [SerializeField] float speed;
    //Amplitude. Limit within y 
    [SerializeField] float A;
    //frequency. How fast skull dude occilates within A range.
    [SerializeField] float f;
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    int flipFactor = 1;
 
    
    Vector2 spawnpos; //the spawn position
    void Start()
    {
        spawnpos = gameObject.transform.position;
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDying && activated)
            Move();   
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("FireSkull Collision with something!");
        //if it's player, have it take some damage
        if(collision.gameObject.GetComponent<CharacterController2D>() is CharacterController2D)
        {
            CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
            if (!player.IsIntangible())
            {
                player.TakeDamage(damage,gameObject);
            }
            
        }
       


    }


    protected override void Move()
    {
        if(spriteRenderer.flipX)
        {
            flipFactor = -1;
        }
        else
        {
            flipFactor = 1;
        }
        float x = gameObject.transform.position.x + speed * Time.deltaTime*flipFactor;
        float y = A * Mathf.Sin(2 * Mathf.PI * f * Time.time) +spawnpos.y;

        gameObject.transform.position = new Vector2(x, y);

    }

    /// <summary>
    /// Called when enemy makes contact with something
    /// </summary>
    public override void TakeDamage(int damageTaken, Vector3 charPos)
    {
        health -= damageTaken;
        ChangeColour();
        StartCoroutine("DamageEffect");
        if (health<=0)
        {
            Die();

        }
    }
    
    /// <summary>
    /// this is the end, beautiful friend...
    /// </summary>
    protected override void Die()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        //Disable contact hitbox
        Destroy(gameObject.GetComponent<Collider2D>());
        animator.SetBool("isDying", true);
        isDying = true;

        //Roll on item spawn.
        int roll=Random.Range(0, 100);
        Debug.Log("Roll for loot drop: " + roll);
        if(roll<=dropChance)
        {
            Instantiate(itemSpawn, transform.position, Quaternion.identity);
        }
        
        StartCoroutine("DeathCountdown");

    }

    //No attack needed. dude just floats around!
    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator DeathCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(deathEffect,transform.position,Quaternion.identity);
            PlayDeathSound();
            Destroy(gameObject);
            break;
        }
    }

    /// <summary>
    /// When enemy goes out of view, a 3 sec countdown will start , if it is not visibile by then, it is destroyed without being killed.
    /// </summary>
    /// <returns></returns>
    IEnumerator OOVDeathCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            
            Destroy(gameObject);
            break;
        }
    }

    private void OnBecameInvisible()
    {
        if(!isDying)
            StartCoroutine("OOVDeathCountdown");
    }
    private void OnBecameVisible()
    {
        StopCoroutine("OOVDeathCountdown");
        activated = true;
    }
}

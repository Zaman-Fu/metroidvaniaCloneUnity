using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hound : Enemy
{
    Rigidbody2D rb;
    Animator animator;
    int flipDir =-1;
    float speed = 20;
    Vector3 m_velocity_zero = Vector3.zero;
    private void Awake()
    {
        activated = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        
        if(GetComponent<SpriteRenderer>().flipX)
        {
            flipDir *= -1;
        }
    }
    
    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(speed*flipDir *Time.deltaTime * 10f, rb.velocity.y);
        // And then smoothing it out and applying it to the character
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity,  ref m_velocity_zero, .05f);
    }

    protected override void Die()
    {
        isDying = true;
        //Roll on item spawn.
        int roll = Random.Range(0, 100);
        Debug.Log("Roll for loot drop: " + roll);
        if (roll <= dropChance)
        {
            Instantiate(itemSpawn, transform.position, Quaternion.identity);
        }

        Instantiate(deathEffect, transform.position, Quaternion.identity);

        base.Die();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (activated)
            Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hound Collision with something!");
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
    

    public override void Activate()
    {
        Debug.Log("Activating Hound");
        animator.SetBool("isActivated", true);
        activated = true;
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
    }


}

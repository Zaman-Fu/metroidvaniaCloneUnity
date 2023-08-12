using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Enemy
{
    Animator animator;
    [SerializeField] GameObject breathAttack;
    Transform playerTransform;
    Vector2 distance;
    Vector3 attackOffset= new Vector3(-1.2f,-0.8f,0);
    Rigidbody2D rb;
    bool isAttacking = false;
    bool takingDamage = false;
    float speed =0.5f;
    bool flipped; //Assuming default facing right.
    // Start is called before the first frame update
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    void Start()
    {
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(activated && !isAttacking && !takingDamage)
        {
            Move();
        }
    }


    protected override void Attack()
    {
        animator.SetTrigger("Attack");
        isAttacking = true;
        StartCoroutine("AttackDelay");
        StartCoroutine("AttackCooldown");        
    }

    

    /// <summary>
    /// Demon's AI summary: on cam visibility, activate. Then follow player around until you reach a distance above him, then rain fire upon the player after a short delay.
    /// After a cooldown of the attack, do it again.
    /// </summary>
    protected override void Move()
    {
        //first, calculate distance from player to demon
        distance = playerTransform.position - gameObject.transform.position;
        if(distance.x>0 && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
            attackOffset.x *= -1;
        }
        else if(distance.x<0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
            attackOffset.x *= -1;
        }
        //Debug.Log("current distance: " + distance);
        //next, flip the enemy if he needs to be flipped (depends on if x is negative or positive);

        //now move the enemy on a function between enemy speed and time.deltatime, towards the enemy, IF enemy is not attacking or not in range for attacking.
        if((Mathf.Abs(distance.x)>1.2 || Mathf.Abs(distance.y)>1.2))
        {
            transform.position = Vector3.Lerp(transform.position, playerTransform.position, speed * Time.deltaTime);
        }
        else
        {
           Attack();
        }


        //if enemy is in range for attacking, attack.

    }

    protected override void Die()
    {
        StopAllCoroutines();
        gameObject.GetComponent<Collider2D>().enabled = false;
        isDying = true;
        activated = false;
        animator.SetBool("isDying",true);
        StartCoroutine("DeathDelay");

        int roll = Random.Range(0, 100);
        Debug.Log("Roll for loot drop: " + roll);
        if (roll <= dropChance)
        {
            Instantiate(itemSpawn, transform.position, Quaternion.identity);
        }

    }

    private void OnBecameVisible()
    {
        activated = true;
        Debug.Log("Enemy Activated");
    }
    public override void TakeDamage(int damageTaken, Vector3 charPos)
    {
        health -= damageTaken;
        ChangeColour();
        StartCoroutine("DamageEffect");
        if (health <= 0)
        {
            Die();

        }
        else
        {
            takingDamage = true;
            StartCoroutine("DamageCooldown");
            Vector2 dirAwayFromPlayer = gameObject.transform.position - charPos;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(dirAwayFromPlayer.normalized * 3f, ForceMode2D.Impulse);
        }    
    }


    IEnumerator DamageCooldown()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            takingDamage = false;
        }
    }
    IEnumerator AttackDelay()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.8f);
            GameObject breath=Instantiate(breathAttack, transform.position + attackOffset, Quaternion.identity);

            if (spriteRenderer.flipX)
                breath.GetComponent<SpriteRenderer>().flipX = true;
            break;
        }
        
    }

    IEnumerator AttackCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            isAttacking = false;
            break;
        }

    }
    IEnumerator DeathDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            break;
        }
    }
}

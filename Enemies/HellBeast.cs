using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HellBeast : Enemy
{
    public Slider HealthSlider; //Showing boss health. Castlevania bosses don't really do that, but I'd like to.
    bool enrage=false; // the enrage counter. it triggers after the first death
    Animator animator;
    Rigidbody2D rb;


    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //Initialize slider
        HealthSlider.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {

    }
    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        //teleport to one of the pre-determined locations.
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// The boss is too bulky to be moved around by any strike. Thus, no knockback will be taken.
    /// </summary>
    /// <param name="damageTaken"></param>
    /// <param name="charPos">useless in this override</param>
    public override void TakeDamage(int damageTaken, Vector3 charPos)
    {
        health -= damageTaken;
        HealthUpdate();
        ChangeColour();
        StartCoroutine("DamageEffect");
        if (health <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        if(enrage)
        {
            //truly die, explosion and all
        }
        else
        {
            //TODO: Alternative. Spawn a new, already enraged hellbeast (Make 2 bosses, in other words)
            health = 100;
            HealthUpdate();
            enrage = true;
        }
    }
    private void HealthUpdate()
    {
        HealthSlider.value = health;
    }
    



    //Coroutine to decide teleport location

    //Coroutine to decide attack pattern +cooldown to next cycle of relocation-attack
}

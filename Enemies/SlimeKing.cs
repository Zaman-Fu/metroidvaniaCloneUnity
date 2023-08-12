using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeKing : Enemy
{
    bool readyToMove=true;
    Rigidbody2D rb;
    [SerializeField]
    List<Transform> platTransforms;
    [SerializeField]
    Transform roofTransform;
    [SerializeField]
    GameObject fireballsDiagonal;
    [SerializeField]
    GameObject fireballsCardinal;
    [SerializeField]
    Animator animator;
    [SerializeField] Slider slider;
    [SerializeField] GameObject spawnerGroup;
    Vector3 destination;
    int patternID;//0 is teleport and shoot, 1 is teleport and fall.
    bool isDamaging;

    // Start is called before the first frame update
    void Start()
    {
        
        activated = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        slider.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(activated&&readyToMove)
        {
            Move();
        }
    }
    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// The main logic runs here. If the slime is ready to move, it shall decide on its routines at random.
    /// FIRST CYCLE: Decide between reposition and fire, reposition and summon, or slam from above attack.
    /// SECOND CYCLE: TO be programmed at a later date
    /// </summary>
    protected override void Move()
    {
        Debug.Log("SlimeKing Moving");
        readyToMove = false;
        patternID = Random.Range(0, 99);
        Debug.Log(patternID);
        if(patternID<=40)
        {
            
            StartCoroutine("RelocatePlat");
        }
        else if(patternID<=80)
        {
            StartCoroutine("RelocateCeiling");
        }
        else
        {
            StartCoroutine("RelocateCenter");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Slime King Collision with something!");
        if(collision.collider.gameObject.CompareTag("Player") && isDamaging)
        {
            CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
            if (!player.IsIntangible())
            {
                player.TakeDamage(damage, gameObject);
            }
        }

    }

    public override void TakeDamage(int damageTaken, Vector3 charPos)
    {
        if(isDying)
        {
            return;
        }
        health -= damageTaken;
        ChangeColour();
        HealthUpdate();
        StartCoroutine("DamageEffect");
        audioSource.Play();
        if (health <= 0)
        {
            Die();

        }
    }
    protected override void Die()
    {
        StopAllCoroutines();
        animator.speed = 1;
        animator.Play("SlimeKing_crownloss");
        ChangeColour();
        StartCoroutine("SlimeDefeat");
        isDying = true;
        readyToMove = false;
    }
    private void HealthUpdate()
    {
        slider.value = health;
    }



    IEnumerator RelocatePlat()
    {
        animator.Play("SlimeKing_sink");
        //Select platform reappearance
        
        int pos = Random.Range(0, 3);
        while(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name!="SlimeKing_sink")
        {
            yield return null;
        }
        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime<0.9f)
        {
            yield return null;
        }

        //teleport
        Debug.Log("Relocating to platform: " + pos.ToString() );
        gameObject.transform.position = platTransforms[pos].position;

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        animator.speed = 0;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        animator.speed = 1;

        

        StartCoroutine("ShootFire");

    }

    IEnumerator ShootFire()
    {
        //Waiting for the rest of the animations to play to completion
        yield return new WaitForSeconds(0.5f);

        animator.Play("SlimeKing_shoot");
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f)
        {
            yield return null;
        }

        if (Random.Range(0, 2)<1)
        {
            Instantiate(fireballsCardinal,gameObject.transform.position,Quaternion.Euler(0,0,0));
        }
        else
        {
            Instantiate(fireballsCardinal,gameObject.transform.position, Quaternion.Euler(0, 0, 45));
        }


        yield return new WaitForSeconds(2.0f);
        Debug.Log("Ready to move");
        readyToMove = true;
        yield return null;

    }
    IEnumerator RelocateCeiling()
    {
        rb.gravityScale = 0;
        animator.Play("SlimeKing_sink");

        while (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "SlimeKing_sink")
        {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
        {
            yield return null;
        }
        gameObject.transform.position = roofTransform.position;

        yield return new WaitForSeconds(1.0f);



        StartCoroutine("Falling");
    }

    IEnumerator Falling()
    {
        gameObject.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
       
        rb.gravityScale = 1;
        isDamaging = true;
        animator.Play("SlimeKing_falling");
        yield return new WaitForSeconds(0.1f);
        while(Mathf.Abs( rb.velocity.y)>0)
        {
            yield return null;
        }
        animator.Play("SlimeKing_landing");

        yield return new WaitForSeconds(0.2f);
        isDamaging = false; 
        yield return new WaitForSeconds(1.5f);
        readyToMove = true;
    }
    IEnumerator RelocateCenter()
    {
        animator.Play("SlimeKing_sink");
        //Select platform reappearance

        int pos = Random.Range(0, 3);
        while (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "SlimeKing_sink")
        {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f)
        {
            yield return null;
        }

        //teleport
        Debug.Log("Relocating to platform: " + pos.ToString());
        gameObject.transform.position = platTransforms[1].position;

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        animator.speed = 0;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        animator.speed = 1;



        StartCoroutine("Summoning");
    }

    IEnumerator Summoning()
    {
        //Waiting for the rest of the animations to play to completion
        yield return new WaitForSeconds(0.5f);

        animator.Play("SlimeKing_crownlevitate");
        yield return new WaitForSeconds(2.0f);
        foreach (Transform spawner in spawnerGroup.transform)
        {
            spawner.gameObject.GetComponent<EnemySpawner>().SpawnEnemy();
        }
        yield return new WaitForSeconds(2.0f);
        readyToMove = true;
    }

    IEnumerator SlimeDefeat()
    {
        yield return new WaitForSeconds(2.0f);
        GeneralManager.GameOver(true);
    }
}

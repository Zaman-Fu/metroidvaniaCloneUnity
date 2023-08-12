using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    float horizontalMove = 0f;
    bool jumpBool=false;
    bool crouchBool = false;
    bool attackReady=true;
    bool spellReady =true;
    bool landReady = true; //Ready to detect if player is landing
    public CharacterController2D controller;
    public float runSpeed = 30f;
    
    public Animator animator;

    //Attacking Function
    public GameObject attackPoint;
    public LayerMask attackableLayers;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {




        horizontalMove =Input.GetAxisRaw("Horizontal")*runSpeed;

        //TODO: is it right to delegate this to the movement script instead of the controller script? should there be a separate animation controller script after all?
        animator.SetFloat("speed", Mathf.Abs(horizontalMove)); 
       //Handle jump
        if(Input.GetButtonDown("Jump"))
        {
            jumpBool = true;
            animator.SetBool("isJumping", true);

        }

        //Handle crouch
        if (Input.GetButtonDown("Crouch"))
        {
            crouchBool = true;
        }
        else if(Input.GetButtonUp("Crouch"))
        {
            crouchBool = false;
        }

        //Handle attack action
        if(Input.GetButtonDown("Attack1"))
        {
            //TODO: start a timing subroutine instead to delay the attack hitbox coming out for about 0.2 seconds (experiment to find out)
            if(attackReady)
            {
                Attack();
            }
            
        }

        if(Input.GetButtonDown("Magic1"))
        {
            if(spellReady)
            {
                Cast();
            }
        }

        ///DEBUG SPACE
        ///

       
    }

    private void FixedUpdate()
    {
        //Play movement script every time.
        controller.Move(horizontalMove*Time.fixedDeltaTime,crouchBool,jumpBool);
        jumpBool = false;
    }

    public void OnLanding()
    {
        if (landReady)
        {
            animator.SetBool("isJumping", false);
            Debug.Log("has landed");
        }
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }


    /// <summary>
    /// Throw out that hitbox, my man.
    /// </summary>
    public void Attack()
    {
        Debug.Log("Setting Attack trigger to true");
        animator.SetTrigger("Attack");
        //animator.SetBool("isAttacking",true);
        //StartCoroutine("AttackAnimationOver");

        //TODO: This is actually terrible. It's only active for one frame, fuck it. New plan. have a trigger collider teleport from one side to another depending on where player is facing
        //and then activate the trigger, and THEN the trigger applies the attack to the objects.
        /*Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.transform.position,new Vector2(0.6f,0.8f),0f,attackableLayers);

        
        if(hitEnemies.Length>0)
        {
            controller.Attack(hitEnemies);
        }*/



        //ALTERNATIVE ATTACK METHOD: hitbox activation
        //controller.ActivateHitbox();
        StartCoroutine("HitboxDelay");
        StartCoroutine("AttackCooldown");
        attackReady = false;

    }

    public void Cast()
    {
        
            Debug.Log("Setting Cast trigger to true;");
            animator.SetTrigger("Cast");


            controller.Cast();
            StartCoroutine("CastCooldown");
       
        
    }

    public void Die()
    {
        Debug.Log("DEATH!");
        //Turn on flag to play death anim
        animator.SetBool("isDead", true);
        StartCoroutine("DeathRoutine");
        
        

        //Send death countdown to Character controller to cease detection operations in turn? No, the controller is the first one to know of the death, let it handle itself.

        //Cease script cycling

        enabled = false;
        

    }

    /// <summary>
    /// This one was made for attack only, but maybe we should keep it in mind for other animations too... 
    /// NOTE: It might be useless now. consider deleting.
    /// </summary>
    private IEnumerator AttackAnimationOver()
    {
        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime<0.99)
        {
            yield return 0;
        }

        Debug.Log("Returning trigger to false");
        animator.SetBool("isAttacking", false);
        yield return 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(attackPoint.transform.position, new Vector3(0.6f, 0.8f, 1));
    }

    //don't start hitting right as the animation begins running, do it at some later time
    //ALTERNATIVE: Use the normalizedTime of the animation instead.
    IEnumerator HitboxDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.15f);
            controller.ActivateHitbox();
            break;
        }
    }

    IEnumerator AttackCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            attackReady = true;
            break;
        }
    }

    //TODO: each spell might have different cooldowns. Consider that for a moment.
    IEnumerator CastCooldown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            spellReady = true;
            break;
        }
    }
    IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        StopAllCoroutines();
        GeneralManager.GameOver(false);
    }
    
   
}

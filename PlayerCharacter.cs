using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    //Character is now allowed to move beyond this speed
    private float maxSpeed=3.0f;
    //If true, character is airborne.
    private bool isAirborne;
    public float movespeed=10.0f;
    //Force applied  upon jump;
    public float jumpforce = 2.0f;
    //Gravity mod to speed up falling
    private float gravityModifier=1.5f;
    //Gravity mod to speed up falling on short jump
    private float lowjumpGravityModifier=2.0f;

    float movement;
    bool jumpBool;
    bool jumpBoolHold;
    bool isGrounded;

    private Rigidbody2D _rigidbody;
    private RunModule runmod;
    private JumpModule jumpmod;
    // Start is called before the first frame update

    //DEBUG variables

    public float Pvelocity;
        /// <summary>
        /// No use for awake script yet
        /// </summary>
    private void Awake()
    {
        
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        runmod = GetComponent<RunModule>();
        jumpmod = GetComponent<JumpModule>();
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUG VALUES

        //poll controls

        InputPoll();


       




        Pvelocity = _rigidbody.velocity.x;
    }

    private void FixedUpdate()
    {

        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (gravityModifier - 1) * Time.deltaTime;
        }
        else if (_rigidbody.velocity.y > 0 && !jumpBoolHold)
        {
            _rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowjumpGravityModifier - 1) * Time.deltaTime;
        }

        //Single jump for now. Double Jump should probably use 2 variables for jump and double  jump, refreshed on a ground collision event.
        if (jumpBool && _rigidbody.velocity.y < 0.0001)
        {
            Debug.Log("You jumped");
            _rigidbody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }



        if (Mathf.Abs(_rigidbody.velocity.x) < maxSpeed && Mathf.Abs(movement) > 0.1)
        {



            _rigidbody.AddForce(new Vector2(movement, 0) * movespeed, ForceMode2D.Impulse);



        }


    }

    private void InputPoll()
    {
        movement = Input.GetAxis("Horizontal");
        jumpBool = (Input.GetButtonDown("Jump"));
        jumpBoolHold = (Input.GetButton("Jump"));
    }
}

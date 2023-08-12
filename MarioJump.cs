using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioJump : JumpModule
{
    public override void Jump(Rigidbody2D rigidbody, bool BoolJump, bool BoolJumpHold)
    {

        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (gravityModifier - 1) * Time.deltaTime;
        }
        else if (rigidbody.velocity.y > 0 && !BoolJumpHold)
        {
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowjumpGravityModifier - 1) * Time.deltaTime;
        }

        //Single jump for now. Double Jump should probably use 2 variables for jump and double  jump, refreshed on a ground collision event.
        if (BoolJump && rigidbody.velocity.y < 0.0001)
        {
            Debug.Log("You jumped");
            rigidbody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
        }
    }
}

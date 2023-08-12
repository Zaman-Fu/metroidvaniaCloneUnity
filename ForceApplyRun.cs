using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceApplyRun : RunModule
{
    public override void Run(Rigidbody2D rigidbody, float axis)
    {
        if (Mathf.Abs(rigidbody.velocity.x) < maxSpeed && Mathf.Abs(axis) > 0.1)
        {
            //Initial Dash
            if(rigidbody.velocity.x==0)
            {
               // rigidbody.AddForce(new Vector2(axis, 0)*speed*10,ForceMode2D.Impulse);
            }
            else
            {
                //rigidbody.AddForce(new Vector2(axis, 0) * speed);
            }

            rigidbody.AddForce(new Vector2(axis, 0) * speed *Time.deltaTime);


            Debug.Log("Moving towards "+ axis);
        }
    }

}

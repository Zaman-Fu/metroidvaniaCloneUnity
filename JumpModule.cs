using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This is a parent class that will sire all different jumping applications
/// </summary>
public abstract class JumpModule : MonoBehaviour
{
    public float jumpforce = 2.0f;
    //Gravity mod to speed up falling
    protected float gravityModifier = 1.5f;
    //Gravity mod to speed up falling on short jump
    protected float lowjumpGravityModifier = 2.0f;

    public abstract void Jump(Rigidbody2D rigidbody,bool BoolJump, bool BoolJumpHold);
    
}

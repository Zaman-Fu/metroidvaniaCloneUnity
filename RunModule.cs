using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Parent of all running functions
/// </summary>
public abstract class RunModule : MonoBehaviour
{
    public float speed;
    public float maxSpeed;

    public abstract void Run(Rigidbody2D rigidbody, float axis);
}

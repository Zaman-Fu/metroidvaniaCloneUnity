using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles the permeability of certain platforms.
/// </summary>
public class PermeablePlatform : MonoBehaviour
{

    PlatformEffector2D selfEffector;
    bool effectorReversed = false;
    // Start is called before the first frame update
    void Start()
    {
        selfEffector = gameObject.GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EffectorReverse()
    {
        Debug.Log("Effector Reversed");
        if(selfEffector.rotationalOffset==0)
        {
            selfEffector.rotationalOffset = 180f;
            effectorReversed = true;
        }
        else
        {
            selfEffector.rotationalOffset = 0;
            effectorReversed = false;
        }
        
    }

    //
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(effectorReversed)
        {
            EffectorReverse();
        }
    }
}

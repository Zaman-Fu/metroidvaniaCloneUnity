using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    AudioSource audioSource;
    void Start()
    {
        //get animator
        animator = gameObject.GetComponent<Animator>();
        //Start coroutine to destroy gameobject when animation is complete
        StartCoroutine("ExplosionEnd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //When animation runs to its last frame, the object will selfdestruct
    IEnumerator ExplosionEnd()
    {
        while(animator.GetCurrentAnimatorStateInfo(0).normalizedTime<1)
        {
            yield return 0;
        }
        Destroy(gameObject);
        yield return 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This object exists to align the fireballs in a pattern, then die
/// </summary>
public class FireballParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.transform.DetachChildren();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

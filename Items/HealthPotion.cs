using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickup
{
    protected override void OnPickup(CharacterController2D character)
    {
        character.ChangeHealth(20);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SDTimer");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePickup : Pickup
{
    Vector3 origin;
    
    protected override void OnPickup(CharacterController2D character)
    {
        character.ChangeSpell(typeof(AxeSpell), MagicType.Axe);
    }


    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine("SDTimer");
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float y = 0.1f * Mathf.Sin(2 * Mathf.PI * 1f * Time.time);
        transform.position = origin + new Vector3(0, y, 0);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSpell : Spell
{
    

    private void Awake()
    {
        baseDamage = 10;
        manaCost = 20;
        //TODO: find proper offset;
        castOffset = new Vector2(0, 0);
        Debug.Log("Initializing projectile");
        projectilePrefab = (GameObject)Resources.Load("AxeProjectile");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Cast(bool facingRight, float spellModifier, Vector2 ppos)
    {
        GameObject spawnedKnife = Instantiate(projectilePrefab, ppos, Quaternion.identity);

        //flip if not facing right
        if (!facingRight)
            spawnedKnife.GetComponent<AxeProjectile>().Flip();
    }
}

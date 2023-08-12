using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpell : Spell
{
    

    private void Awake()
    {
        baseDamage = 5;
        manaCost = 10;
        //TODO: find proper offset;
        castOffset = new Vector2(0, 0);
        projectilePrefab = (GameObject)Resources.Load("Knife");


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// This is the knife spell cast. From an offset near the player, instantiate the object 
    /// </summary>
    /// <param name="facingRight"></param>
    /// <param name="spellModifier"></param>
    public override void Cast(bool facingRight, float spellModifier,Vector2 ppos)
    {
        GameObject spawnedKnife= Instantiate(projectilePrefab,ppos,Quaternion.identity);

        //flip if not facing right
        if(!facingRight)
            spawnedKnife.GetComponent<KnifeProjectile>().Flip();
    }

}

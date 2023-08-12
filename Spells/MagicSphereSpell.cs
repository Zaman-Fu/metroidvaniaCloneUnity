using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphereSpell : Spell
{
    
    private void Awake()
    {
        baseDamage = 10;
        manaCost = 20;
        //TODO: find proper offset;
        castOffset = new Vector2(0, 0);
        Debug.Log("Initializing projectile");
        projectilePrefab = (GameObject)Resources.Load("MagicBallHoming");

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

        //no need to flip, it's a ball...
    }
}

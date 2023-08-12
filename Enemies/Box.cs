using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ok... so the box is not really an enemy, but I don't want to make an entire hierarchy of breakable objects for this... so we'll treat it as an enemy.
/// </summary>
public class Box : Enemy
{
    //box is pacifist. it does not attack
    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }
    //box is lazy, it does not move
    protected override void Move()
    {
        throw new System.NotImplementedException();
    }


    public override void TakeDamage(int damageTaken, Vector3 charPos)
    {
        Debug.Log("Box Taking Damage: " +damageTaken);
        health -= damageTaken;
        if (health <= 0)
        {
            Die();

        }
    }

    protected override void Die()
    {


        int roll = Random.Range(0, 100);
        Debug.Log("Roll for loot drop: " + roll);
        //Box has guaranteed drop
        if (roll <= 100)
        {
            Instantiate(itemSpawn, transform.position, Quaternion.identity);
        }
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

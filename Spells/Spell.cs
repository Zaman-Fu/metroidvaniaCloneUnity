using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// the father of all spell objects
/// cannot be instantiated, is here for inheritance purposes only
/// </summary>
public abstract class Spell : MonoBehaviour
{
    //basic attributes
    protected int baseDamage;
    protected int manaCost;
    

    //to handle switched sides for projectile spawning
    [SerializeField]protected Vector2 castOffset;

    //The object that will spawn upon casting
    [SerializeField] protected GameObject projectilePrefab;

    //getters for basic attributes
    public int ManaCost
    {
        get { return manaCost; }
    }
    public int BaseDamage
    {
        get { return baseDamage; }
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="facingRight">knowing where player is facing to flip our spell or projectile sprites accordingly</param>
    /// <param name="spellModifier">in case we need to modify the damage due to item powerups etc</param>
    /// <param name="ppos">position of the player during cast</param>
    public abstract void Cast(bool facingRight, float spellModifier, Vector2 ppos);
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A hitbox object tied to hitboxes that hurt enemies.
/// </summary>
public class Hitbox : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage=5;

    private void Start()
    {
        StartCoroutine("DelCountdown");
    }

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Debug.Log("ENEMY HIT");
            enemy.TakeDamage(Damage,gameObject.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.otherCollider.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            Debug.Log("ENEMY HIT");
            enemy.TakeDamage(Damage,gameObject.transform.position);
        }
    }

    IEnumerator DelCountdown()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
}

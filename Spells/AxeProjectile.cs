using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int orientation = 1; //1 facing right, -1 facing left
    [SerializeField] int damage = 10;
    [SerializeField] float speed = 10; //The speed of the knife is unchanging
    Vector3 initialPos;
    Rigidbody2D rb;
    void Start()
    {
        //keep track of initial position for destruction
        initialPos = gameObject.transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0.5f*orientation,1)*speed,ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(initialPos.y - gameObject.transform.position.y) > 20f)
        {
            DistanceDestroy();
        }
        transform.Rotate(new Vector3(0, 0, -200f) * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("AXE Collision!");
        
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage,gameObject.transform.position);
        }
        else if(collision.gameObject.CompareTag("Ground"))
        {
            DistanceDestroy();
        }
    }

    public void Flip()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        orientation = -1;

    }

    public void DistanceDestroy()
    {

        Destroy(gameObject);
    }
}

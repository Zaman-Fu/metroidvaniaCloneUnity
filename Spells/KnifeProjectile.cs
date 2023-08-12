using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int orientation = 1; //1 facing right, -1 facing left
    [SerializeField] int damage = 5;
    [SerializeField] float speed =4; //The speed of the knife is unchanging
    Vector3 initialPos;
    void Start()
    {
        //keep track of initial position for destruction
        initialPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(initialPos.x-gameObject.transform.position.x)>5f)
        {
            DistanceDestroy();
        }
        gameObject.transform.position += new Vector3(speed*Time.deltaTime*orientation, 0, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Knife Collision!");
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy is Enemy)
        {
            enemy.TakeDamage(damage,gameObject.transform.position);
        }
    }

    public void Flip()
    {
        gameObject.GetComponent<SpriteRenderer>().flipX=true;
        orientation = -1;
        
    }

    public void DistanceDestroy()
    {
        
        Destroy(gameObject);
    }
}

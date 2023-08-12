using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSphereProjectile : MonoBehaviour
{
    public LayerMask targetMask; //What layer of entity we are targetting.
    bool homePhase;//if it's currently homing on a target.
    float speed=8f;//movespeed of projectile.
    int damage=10;//damage dealt by projectile.
    Vector3 targetDir;//the target position by the end of the homing phase.
    Transform target;
    Vector3 iniPos;
    // Start is called before the first frame update
    void Start()
    {
        homePhase = true;
        iniPos = transform.position;

        StartCoroutine("HomingWait");
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy if it goes too far
        if(homePhase)
        {
            gameObject.transform.position += Vector3.up * 0.1f * speed * Time.deltaTime;
        }
        else
        {
            gameObject.transform.position += targetDir * speed * Time.deltaTime;
            Vector3 distance = iniPos - gameObject.transform.position;
            //Debug.Log(distance.x + " AND " + distance.y);
            if (Mathf.Abs(distance.x) > 10f || Mathf.Abs(distance.y) > 10f)
            {
                Destroy(gameObject);
            }

        }
        

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ball Collision!");
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy is Enemy)
        {
            enemy.TakeDamage(damage, gameObject.transform.position);
        }
    }
   private void EnemyDetect()
    {
        //Overlap Circle
        //Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 6f, 9);
        //Overlap Rect
        Debug.Log("Detecting Enemies");
        Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position, new Vector2(10f, 5f),0f,targetMask);
        Debug.Log(enemies.Length);
        //If enemies are found, track nearest one, otherwise, selfdestruct.
        if(enemies.Length!=0)
        {
            //Enemies
            Collider2D nearest = enemies[0];
            float ndistance = Vector3.Distance(transform.position, nearest.transform.position);
            foreach (Collider2D enemy in enemies)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < ndistance)
                {
                    nearest = enemy;
                    ndistance = distance;
                }
            }
            target = nearest.transform;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    IEnumerator HomingWait()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            EnemyDetect();
            if(target!=null)
            {
                //find the direction the ball needs to go to
                targetDir = (target.position- gameObject.transform.position).normalized;
                homePhase = false;
            }
            break;
        }
    }
}

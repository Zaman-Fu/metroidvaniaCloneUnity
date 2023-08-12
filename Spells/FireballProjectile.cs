using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It goes forward (rotation relative) until it meets the player or travels a distance of 30 units.
/// </summary>
public class FireballProjectile : MonoBehaviour
{
    [SerializeField]
    int damage=10;
    float speed = 5f;
    Vector3 iniPos; //Initial position for fireball
    // Start is called before the first frame update
    void Start()
    {
        iniPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.position += gameObject.transform.right * speed * Time.deltaTime;
        Vector3 distance = iniPos - gameObject.transform.position;
        //Debug.Log(distance.x + " AND " + distance.y);
        if(Mathf.Abs(distance.x)>30f || Mathf.Abs(distance.y)>30f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Fireball Collision!");

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterController2D>().TakeDamage(damage,gameObject);
            //You done your job, brave fireball, now rest...
            Destroy(gameObject);
        }

        
    }
}

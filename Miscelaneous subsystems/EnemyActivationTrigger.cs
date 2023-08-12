using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivationTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Enemy enemy;
    void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entered enemy's field of vision");
            enemy.Activate();
            Destroy(gameObject);
        }
        
    }
}

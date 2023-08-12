using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;
    [SerializeField] float cooldown;
    [SerializeField] Collider2D destroyTrigger;
    [SerializeField ]bool isFlipped;
    // Start is called before the first frame update
    void Start()
    {
        if(cooldown>0)
        {
            StartCoroutine("SpawnRoutine");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(cooldown);
        }
        
        
    }

    public void SpawnEnemy()
    {
        GameObject enemyInstance =Instantiate(enemyObject, gameObject.transform.position, Quaternion.identity);
        enemyInstance.GetComponent<Enemy>().Activate();
        enemyInstance.GetComponent<SpriteRenderer>().flipX = isFlipped;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player touched the destroy Trigger of the spawner, destroy the spawner
      if(collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}

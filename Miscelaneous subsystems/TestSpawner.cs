using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{

    [SerializeField]  List<GameObject> prefabs;
    GameObject spawnedObject;
    [SerializeField] Vector3 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnPrefab(int prefID)
    {
        spawnedObject =  Instantiate(prefabs[prefID], spawnPos, Quaternion.identity);
        
        if(spawnedObject.GetComponent<Enemy>()!=null)
            {
            spawnedObject.GetComponent<Enemy>().Activate();
        }
    }
}

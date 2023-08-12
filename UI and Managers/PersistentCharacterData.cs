using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCharacterData : MonoBehaviour
{
    // Start is called before the first frame update
    Spell playerSpell;
    int currentHealth;
    int currentMana;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetData(Spell spell,int hp,int mp)
    {

    }
}

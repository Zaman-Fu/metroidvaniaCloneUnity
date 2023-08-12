using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This platform will shake when touched, and fall after exactly 0.9 seconds.
/// </summary>
public class FallingPlatform : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        animator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(initialPos,gameObject.transform.position)>=15)
        {
            Debug.Log("Destroying platform");
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Contact!");
        if(collision.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("falling platform");
            StartCoroutine("PlatformCollapse");
        }
    }


    IEnumerator PlatformCollapse()
    {
        animator.speed = 1;
        yield return new WaitForSeconds(0.9f);
        animator.speed = 0;
        gameObject.AddComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}

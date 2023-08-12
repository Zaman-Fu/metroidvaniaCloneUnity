using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField]
    Vector3 destination;
    [SerializeField]
    CinemachineConfiner confiner;
    [SerializeField]
    PolygonCollider2D newConfines;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            confiner.m_BoundingShape2D = newConfines;
            collision.gameObject.transform.position = destination;
        }
    }
}

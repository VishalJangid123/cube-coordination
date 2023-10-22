using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private Transform destination;

    [SerializeField] private PlayerController player;

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Vector2.Distance(player.transform.position, transform.position) > 0.3f)
        {
            player.MoveToDestination(destination.position);
        }
    }

   
}

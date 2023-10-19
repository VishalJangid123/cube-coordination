using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private Transform destination;

    Player player;
    PlayerMirror playerMirror;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        playerMirror = FindObjectOfType<PlayerMirror>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(Vector2.Distance(player.transform.position, transform.position) > 0.3f)
        {
            player.MoveToDestination(destination.position);
        }
    }

   
}

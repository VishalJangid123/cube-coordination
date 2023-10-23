using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMirror : PlayerController
{
    protected override void HandleMovement(Vector2 moveDir)
    {
        rb.velocity = moveDir;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        playerReached = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerReached = false;
    }
}

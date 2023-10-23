using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerController
{

    protected override void HandleMovement(Vector2 moveDir)
    {
        rb.velocity = moveDir;
        if (moveDir != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, moveDir);
            Quaternion rot = Quaternion.RotateTowards(transform.rotation, targetRotation, 5f * Time.deltaTime);
            rb.MoveRotation(rot);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "WinArea")
        {
            playerReached = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerReached = false;

    }
}

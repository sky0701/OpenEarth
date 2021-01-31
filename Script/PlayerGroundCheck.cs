using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    PlayerMovementScript playerMovement;

    void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovementScript>();
    }
   void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Debug.Log("바닥이예요");
            playerMovement.SetGroundedState(true);
        }
        else
        {
            Debug.Log("바닥이아니예요");
            playerMovement.SetGroundedState(false);
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Debug.Log("바닥이예요");
            playerMovement.SetGroundedState(true);
        }
        else
        {
            Debug.Log("바닥이아니예요");
            playerMovement.SetGroundedState(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("바닥이아니예요");
        playerMovement.SetGroundedState(false);
    }
}

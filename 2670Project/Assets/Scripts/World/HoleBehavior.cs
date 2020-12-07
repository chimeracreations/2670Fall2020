using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleBehavior : MonoBehaviour
{
    public PlayerData player;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    private float exitCount;
    public bool playerFrozen;
   
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            exitCount = 0;
            if (player.controller.isGrounded == false && player.movement.y < -1.5f)
            {
                while (player.controller.isGrounded == false && exitCount < 5)
                {
                    player.canControl = false;
                    player.movement.x = 0;
                    player.movement.y += -0.1f;
                    player.movement.z = 0;
                    player.controller.Move(player.movement * Time.deltaTime);
                    exitCount += Time.deltaTime;
                    yield return wffu;
                }
                if (!playerFrozen) player.canControl = true; 
            }
            else player.canControl = true;
        }
    }

}

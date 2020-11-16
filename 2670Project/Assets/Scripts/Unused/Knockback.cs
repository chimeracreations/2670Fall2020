using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterMover))]
public class Knockback : MonoBehaviour
{
    public float pushPower = 10.0f;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    public PlayerData player;
    

    private IEnumerator KnockBack (ControllerColliderHit hit, Rigidbody body)
    {
        player.canControl = false;
        var i = 2f;
       
        player.movement = -hit.moveDirection;
        player.movement.y = -1;
        while (i > 0)
        {
            yield return wffu;
            i -= 0.1f;
            player.controller.Move((player.movement) * Time.deltaTime);
            
            var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            var forces = pushDir * pushPower;
            body.AddForce(forces);
        }
        player.movement = Vector3.zero;
        player.canControl = true;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
        {
            return;
        }
    
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }
        StartCoroutine(KnockBack(hit, body));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterMover))]
public class Knockback : MonoBehaviour
{
    public float pushPower = 10.0f;
    private readonly WaitForFixedUpdate wffu = new WaitForFixedUpdate();
    private CharacterMover mover;
    
    private void Start() 
    {
        mover = GetComponent<CharacterMover>(); 
    }

    private IEnumerator KnockBack (ControllerColliderHit hit, Rigidbody body)
    {
        mover.canControl = false;
        var i = 2f;
       
        mover.movement = -hit.moveDirection;
        mover.movement.y = -1;
        while (i > 0)
        {
            yield return wffu;
            i -= 0.1f;
            mover.controller.Move((mover.movement) * Time.deltaTime);
            
            var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            var forces = pushDir * pushPower;
            body.AddForce(forces);
        }
        mover.movement = Vector3.zero;
        mover.canControl = true;
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
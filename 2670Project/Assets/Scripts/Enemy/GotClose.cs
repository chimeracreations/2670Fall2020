using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotClose : MonoBehaviour
{
    private EnemyMovement enemyFarCollider;


    // Start is called before the first frame update
    void Start()
    {
        enemyFarCollider = transform.parent.GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        enemyFarCollider.canHunt = true;
    }


}
